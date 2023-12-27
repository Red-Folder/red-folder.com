using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using RedFolder.Services;
using RedFolder.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;
using Red_Folder.com.Services;
using RedFolder.Models;
using System.Net.Http;
using RedFolder.Podcast;
using Microsoft.Extensions.Hosting;
using RedFolder.Blog;

namespace RedFolder
{
    public class Startup
    {

        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_configuration);
            services.AddSingleton(_hostingEnvironment);

            services.AddApplicationInsightsTelemetry(_configuration);

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddDbContext<RepoContext>();

            services.AddControllersWithViews()
                    .AddRazorRuntimeCompilation()
                    .AddXmlSerializerFormatters();

            services.AddLogging();

            services.AddScoped<IRepoRepository, RepoRepository>();
            services.AddScoped<IArticleRepository, ArticleRepository>();

            var blogRepo = new BlogRepository(_configuration["BlogUrl"]);
            services.AddSingleton<IBlogRepository>(blogRepo);
            services.AddSingleton<IRedirectRepository>(new RedirectRepository(new System.Collections.Generic.List<IRedirectRepository> { blogRepo }));

            var activityRepo = new ActivityRepository(_configuration["ActivityUrl"], _configuration["ActivityCode"]);
            services.AddSingleton<IActivityRepository>(activityRepo);

            var siteMapRepo = new SiteMapRepository(_hostingEnvironment);
            siteMapRepo.AddRepository(blogRepo);
            services.AddSingleton<ISiteMapRepository>(siteMapRepo);

            services.AddTransient<RepoContextSeedData>();

            var sendGridConfiguration = new SendGridConfiguration
            {
                ApiKey = _configuration["SendGridApiKey"],
                From = _configuration["SendGridFromEmailAddress"]
            };
            services.AddSingleton(sendGridConfiguration);
            services.AddTransient<IEmail, SendGridEmail>();

            services.AddSingleton(new HttpClient());
            services.AddSingleton<IPodcastRespository, PodcastRespository>();

            var reCaptchaConfiguration = new ReCaptchaConfiguration
            {
                SecretKey = _configuration["ReCaptchaSecretKey"]
            };
            services.AddSingleton(reCaptchaConfiguration);
            services.AddTransient<ITokenVerification, ReCaptchaTokenVerification>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var mediaRoot = _configuration["MediaRoot"];
            if (!string.IsNullOrEmpty(mediaRoot))
            {
                app.UseStaticFiles(new StaticFileOptions()
                {
                    FileProvider = new PhysicalFileProvider(mediaRoot),
                    RequestPath = new PathString("/media")
                });
            } 
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Errors/Status/500");
                app.UseStatusCodePagesWithReExecute("/Errors/Status/{0}");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapControllerRoute(
                    name: "Podcasts Roadmap",
                    pattern: "Podcasts/Roadmap",
                    defaults: new { controller = "Podcasts", action = "Roadmap" }
                );

                endpoints.MapControllerRoute(
                    name: "Podcasts",
                    pattern: "Podcasts/{id}",
                    defaults: new { controller = "Podcasts", action = "Index" }
                );

                endpoints.MapControllerRoute(
                    name: "Blog",
                    pattern: "Blog/{url}",
                    defaults: new { controller = "Blog", action = "Index" }
                );

                endpoints.MapControllerRoute(
                    name: "Projects - Cordova",
                    pattern: "Projects/Cordova/{action}",
                    defaults: new { controller = "Cordova", action = "Index" }
                );

                endpoints.MapControllerRoute(
                    name: "Projects - Microservices",
                    pattern: "Projects/Microservices/{action}",
                    defaults: new { controller = "Microservice", action = "Index" }
                );

                endpoints.MapControllerRoute(
                    name: "Redirect",
                    pattern: "redirect",
                    defaults: new { controller = "Home", action = "Redirect" }
                );

                endpoints.MapControllerRoute(
                    name: "Newsletter Archive",
                    pattern: "NewsletterArchive",
                    defaults: new { controller = "Blog", action = "Index", filterBy = "Newsletter" }
                );

                endpoints.MapControllerRoute(
                    name: "CookiePolicy",
                    pattern: "cookiepolicy",
                    defaults: new { controller = "Home", action = "CookiePolicy" }
                );

                endpoints.MapControllerRoute(
                    name: "Default",
                    pattern: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" }
                );
            });
        }
    }
}
