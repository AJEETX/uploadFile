using BYO.Service;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BYO.Domain
{
    public interface ISalaryRateHandlersSetup
    {
        SalaryRateHandlers SetupChain();
    }
    public class SalaryRateHandlersSetup : ISalaryRateHandlersSetup
    {
        private readonly IConfigService _configService;
        public SalaryRateHandlersSetup(IConfigService configService)
        {
            _configService = configService;
        }
        public SalaryRateHandlers SetupChain()
        {
            SalaryRateHandlers rateHandlers = null;
            try
            {
                rateHandlers = Chain();
            }
            catch
            {
                //Shout // Log //throw;
            }
            return rateHandlers;
        }
        SalaryRateHandlers Chain()
        {
            var salaryRates = _configService.GetSection<SalaryRateHandlers>(nameof(SalaryRateHandlers));
            for (int i = 0; i < salaryRates.SalaryRateHandlerList.Count() - 1; i++)

                salaryRates.SalaryRateHandlerList.ElementAt(i).SetNextHandler(salaryRates.SalaryRateHandlerList.ElementAt(i + 1));

            return salaryRates;
        }
    }
}
