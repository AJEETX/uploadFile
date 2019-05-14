using Swashbuckle.AspNetCore.Examples;
using BYO.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using BYO.Model;
using System.Collections.Generic;

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
    public class PayslipRequestExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var sampleEmployees = new List<InputModel>();
            sampleEmployees.Add(new InputModel { FirstName = "David", LastName = "Rudd", AnnualSalary = 60050, SuperRate = 9, PaymentStartDate = "01 March - 31 March" });
            sampleEmployees.Add(new InputModel { FirstName = "Ryan", LastName = "Chen", AnnualSalary = 120000, SuperRate = 10, PaymentStartDate = "01 March - 31 March" });

            return sampleEmployees;
        }
    }
}
