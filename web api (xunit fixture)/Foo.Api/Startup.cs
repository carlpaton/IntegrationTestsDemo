using Foo.Api.Application.Infrastructure.Common;
using Foo.Api.Application.Infrastructure.Services.Nuget;
using Foo.Api.Application.Infrastructure.Services.OssIndex;
using Foo.Api.Domain.Interfaces;
using Foo.Api.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Foo.Api
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
            services.Configure<MySqlOptions>(Configuration.GetSection(MySqlOptions.MySql));
            services.Configure<NugetServiceOptions>(Configuration.GetSection(NugetServiceOptions.NugetService));
            services.Configure<OssIndexOptions>(Configuration.GetSection(OssIndexOptions.OssIndex));

            services.AddScoped<IPackageRepository, PackageRepository>();
            services.AddScoped<IPackageVersionRepository, PackageVersionRepository>();
            services.AddScoped<IVulnerabilityRepository, VulnerabilityRepository>();

            services.AddHttpClient<INugetServiceClient, NugetServiceClient>();
            services.AddHttpClient<IOssIndexClient, OssIndexClient>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
