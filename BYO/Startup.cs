using BYO.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace BYO
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IConfigService, ConfigService>()
                .AddScoped<ISalaryRateHandlersService, SalaryRateHandlersService>()
                .AddScoped<ISalaryService, SalaryService>()
                .AddScoped<ISalaryCalculatorService, SalaryCalculatorService>();
            services.AddSwaggerGen(config => {
                config.SwaggerDoc("v1", new Info { Title = "Azy API", Version = "V1" });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseMvc().UseSwagger().UseSwaggerUI(config => {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "Azy API");
            });
        }
    }
}
