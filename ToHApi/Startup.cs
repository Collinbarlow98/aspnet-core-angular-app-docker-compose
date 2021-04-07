using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using ToHApi.Models;
using Microsoft.AspNetCore.SpaServices.AngularCli;

namespace ToHApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                // adds the origins for cors as well as the methods and headers
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("https://tourofheroes3cloudbadge.z22.web.core.windows.net",
                                                          "http://localhost:4200")
                                             .WithMethods("GET", "PUT", "OPTIONS", "DELETE", "PUT")
                                             .WithHeaders("Content-Type");
                                  });
            });
            services.AddDbContext<TohContext>(opt =>
                                              opt.UseInMemoryDatabase("ToHHeroes"));
            services.AddControllers();
            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "angular-tour-of-heroes/dist";
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers()
                    .RequireCors(MyAllowSpecificOrigins); ;
            });

            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "angular-tour-of-heroes";

                if (env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer("http://tohapi.app:4200");
                }
            });
        }
    }
}
