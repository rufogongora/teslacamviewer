using System.IO.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using teslacamviewer.data.Context;
using teslacamviewer.data.Repositories;
using teslacamviewer.web.Helpers;
using teslacamviewer.web.Services;

namespace teslacamviewer.web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IFavoritesRepository, FavoritesRepository>();
            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddScoped<IFileSystem, FileSystem>();

            //db repos
            services.AddDbContext<TeslaContext>(options => 
                options.UseSqlite(Configuration["connectionString"], 
                x => x.MigrationsAssembly("teslacamviewer.data")));
            
            services.AddScoped<ITeslaClipsRepository, TeslaClipsRepository>();
            services.AddScoped<ITeslaFolderRepository, TeslaFolderRepository>();
            services.AddScoped<ITeslaConfigurationRepository, TeslaConfigurationRepository>();
            services.AddScoped<ITeslaDataRepository, TeslaDataRepository>();

            //custom services
            services.AddScoped<ITeslaPhysicalFolderRepository, TeslaPhysicalFolderRepository>();
            services.AddScoped<ITeslaFolderScannerService, TeslaFolderScannerService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, TeslaContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseMiddleware<JwtMiddleware>();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
            context.Database.Migrate();
        }
    }
}
