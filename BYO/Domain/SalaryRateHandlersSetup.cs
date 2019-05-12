using BYO.Service;
using System.Linq;

namespace BYO.Domain
{
    public interface ISalaryRateHandlersSetup
    {
        SalaryRateHandler SetupChain();
    }
    public class SalaryRateHandlersSetup : ISalaryRateHandlersSetup
    {
        private readonly IConfigService _configService;
        public SalaryRateHandlersSetup(IConfigService configService)
        {
            _configService = configService;
        }
        public SalaryRateHandler SetupChain()
        {
            SalaryRateHandler rateHandlers = null;
            try
            {
                rateHandlers = Setup();
            }
            catch
            {
                //Shout // Log //throw;
            }
            return rateHandlers;
        }
        SalaryRateHandler Setup()
        {
            var salaryRates = _configService.GetSection<SalaryRateHandlers>(nameof(SalaryRateHandlers));
            for (int i = 0; i < salaryRates.SalaryRateHandlerList.Count() - 1; i++)

                salaryRates.SalaryRateHandlerList.ElementAt(i).SetNextHandler(salaryRates.SalaryRateHandlerList.ElementAt(i + 1));

            return salaryRates.SalaryRateHandlerList.First();
        }
    }
}
