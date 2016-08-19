using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using RedFolder.Services;
using Newtonsoft.Json.Serialization;
using RedFolder.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

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

            services.AddTransient<RepoContextSeedData>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, RepoContextSeedData seeder, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
                loggerFactory.AddConsole(_config.GetSection("Logging"));
                loggerFactory.AddDebug();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(config =>
            {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" }
                );
            });

            seeder.EnsureSeedData().Wait();
        }
    }
}
