using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Softdesign.Api.Infrastructure.Interfaces;
using Softdesign.Api.Infrastructure.Repositories;
using Softdesign.Api.Infrastructure.Services;

namespace Softdesign.Api
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
            RegisterInitialConfigs(services);

            RegisterServices(services);

            RegisterRepositories(services);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Softdesign.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Softdesign.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void RegisterInitialConfigs(IServiceCollection services)
        {
            services.AddHttpClient();
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddSingleton<ILogger, Logger<object>>();

            services.AddScoped<IApplicationRepository, ApplicationRepository>();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IApplicationService, ApplicationService>();
        }
    }
}
