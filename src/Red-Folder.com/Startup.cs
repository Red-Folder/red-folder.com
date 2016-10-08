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

namespace RedFolder
{
    public class Startup
    {

        private IConfigurationRoot _config;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            _config = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_config);
            services.AddDbContext<RepoContext>();

            services.AddMvc();

            services.AddLogging();

            services.AddScoped<IRepoRepository, RepoRepository>();
            services.AddScoped<IArticleRepository, ArticleRepository>();

            services.AddTransient<RepoContextSeedData>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, RepoContextSeedData seeder, ILoggerFactory loggerFactory)
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


            app.UseMvc(config =>
            {
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

            seeder.EnsureSeedData().Wait();
        }
    }
}
