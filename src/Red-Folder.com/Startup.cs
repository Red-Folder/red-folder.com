using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using RedFolder.Services;
using Newtonsoft.Json.Serialization;
using RedFolder.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;
using Red_Folder.com.Services;
using RedFolder.Models;
using System.Net.Http;

namespace RedFolder
{
    public class Startup
    {

        private IConfiguration _config;
        private IHostingEnvironment _env;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            _config = configuration;
            _env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHostingEnvironment>(_env);

            services.AddApplicationInsightsTelemetry(_config);

            services.AddSingleton(_config);
            services.AddDbContext<RepoContext>();

            services.AddMvc()
                    .AddXmlSerializerFormatters();

            services.AddLogging();

            services.AddScoped<IRepoRepository, RepoRepository>();
            services.AddScoped<IArticleRepository, ArticleRepository>();

            var blogRepo = new BlogRepository(_config["BlogUrl"]);
            services.AddSingleton<IBlogRepository>(blogRepo);
            services.AddSingleton<IRedirectRepository>(new RedirectRepository(new System.Collections.Generic.List<IRedirectRepository> { blogRepo }));

            var activityRepo = new ActivityRepository(_config["ActivityUrl"], _config["ActivityCode"]);
            services.AddSingleton<IActivityRepository>(activityRepo);

            var siteMapRepo = new SiteMapRepository(_env);
            siteMapRepo.AddRepository(blogRepo);
            services.AddSingleton<ISiteMapRepository>(siteMapRepo);

            services.AddTransient<RepoContextSeedData>();

            var sendGridConfiguration = new SendGridConfiguration
            {
                ApiKey = _config["SendGridApiKey"],
                From = _config["SendGridFromEmailAddress"]
            };
            services.AddSingleton(sendGridConfiguration);
            services.AddTransient<IEmail, SendGridEmail>();

            services.AddSingleton(new HttpClient());
            services.AddTransient<IPodcastRespository, PodcastRespository>();

            var reCaptchaConfiguration = new ReCaptchaConfiguration
            {
                SecretKey = _config["ReCaptchaSecretKey"]
            };
            services.AddSingleton(reCaptchaConfiguration);
            services.AddTransient<ITokenVerification, ReCaptchaTokenVerification>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseStaticFiles(new StaticFileOptions()
                {
                    FileProvider = new PhysicalFileProvider(@"C:\inetpub\mediaroot"),
                    RequestPath = new PathString("/media")
                });

                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
                loggerFactory.AddConsole(_config.GetSection("Logging"));
                loggerFactory.AddDebug();
            }
            else
            {
                app.UseStaticFiles(new StaticFileOptions()
                {
                    FileProvider = new PhysicalFileProvider(@"D:\home\site\mediaroot"),
                    RequestPath = new PathString("/media")
                });

                app.UseExceptionHandler("/Errors/Status/500");
                app.UseStatusCodePagesWithReExecute("/Errors/Status/{0}");
            }

            // Add CSP
            //app.Use(async (context, next) =>
            //{
            //    var key = "Content-Security-Policy-Report-Only";

            //    // Don't add if already exists - otherwise you'll get 500's
            //    if (!context.Response.Headers.ContainsKey(key))
            //    {
            //        context.Response.Headers.Add(
            //            key,
            //            "default-src 'none'; " +
            //            "form-action 'none'; " +
            //            "frame-ancestors 'none'; " +
            //            "report-uri https://redfolder.report-uri.com/r/d/csp/wizard");
            //    }

            //    await next();
            //});

            app.UseMvc(config =>
            {
                config.MapRoute(
                    name: "Podcasts",
                    template: "Podcasts/{id}",
                    defaults: new { controller = "Podcasts", action = "Index" }
                );

                config.MapRoute(
                    name: "Blog",
                    template: "Blog/{url}",
                    defaults: new { controller = "Blog", action = "Index" }
                );

                config.MapRoute(
                    name: "Projects - Cordova",
                    template: "Projects/Cordova/{action}",
                    defaults: new { controller = "Cordova", action = "Index" }
                );

                config.MapRoute(
                    name: "Projects - Microservices",
                    template: "Projects/Microservices/{action}",
                    defaults: new { controller = "Microservice", action = "Index" }
                );

                config.MapRoute(
                    name: "Redirect",
                    template: "redirect",
                    defaults: new { controller = "Home", action = "Redirect" }
                );

                config.MapRoute(
                    name: "Newsletter Archive",
                    template: "NewsletterArchive",
                    defaults: new { controller = "Blog", action = "Index", filterBy = "Newsletter" }
                );

                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" }
                );

                //config.MapRoute(
                //    name: "CatchAll",
                //    template: "{url}",
                //    defaults: new { controller = "Error", action = "NotFound" }
                //);

            });

            //app.Run(context =>
            //{
            //    context.Response.StatusCode = 404;
            //    return Task.FromResult(0);
            //});

        }
    }
}
