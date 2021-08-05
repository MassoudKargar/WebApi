
using Api.Common;
using Api.WebFramework.Configuration;
using Api.WebFramework.CustomMapping;
using Api.WebFramework.Middlewares;
using Api.WebFramework.Swagger;

using Autofac;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            services.InitializeAutoMapper();

            services.AddDbContext(Configuration);

            services.AddCustomIdentity(SiteSetting.IdentitySettings);

            services.AddMinimalMvc();

            services.AddElmahCore(Configuration, SiteSetting);

            services.AddJwtAuthentication(SiteSetting.JwtSettings);

            services.AddCustomApiVersioning();

            services.AddSwagger();
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //Register Services to Autofac ContainerBuilder
            builder.AddServices();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.IntializeDatabase();

            app.UseCustomExceptionHandler();

            app.UseHsts(env);

            app.UseHttpsRedirection();

            app.UseSwaggerAndUI();

            app.UseElmahCore(SiteSetting);

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
