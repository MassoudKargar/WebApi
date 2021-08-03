
using Api.Common;
using Api.Data.Contracts;
using Api.Data.Repositories;
using Api.Services;
using Api.WebFramework.Configuration;
using Api.WebFramework.Middlewares;

using ElmahCore.Mvc;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Api.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            SiteSetting = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
        }

        private SiteSettings SiteSetting { get; }
        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<SiteSettings>(Configuration.GetSection(nameof(SiteSettings)));

            services.AddDbContext(Configuration);

            services.AddJwtAuthentication(SiteSetting.JwtSettings);

            services.AddCustomApiVersioning();

            services.AddElmahCore(Configuration, SiteSetting);

            services.AddMinimalMvc();



            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api.WebApi", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCustomExceptionHandler();
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                //app.UseElmahExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api.WebApi v1"));
            }
            else
            {
                app.UseExceptionHandler();
                //app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseElmah();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
