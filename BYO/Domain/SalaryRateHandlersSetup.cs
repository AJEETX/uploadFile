using BYO.Service;
using System.Linq;

namespace BYO.Domain
{
    public interface ISalaryRateHandlersSetup
    {
        SalaryRateHandler SalaryRateHandler { get; }
    }
    public class SalaryRateHandlersSetup : ISalaryRateHandlersSetup
    {
        IConfigService _configService;
        static SalaryRateHandler rateHandlers = null;
        public SalaryRateHandlersSetup(IConfigService configService)
        {
            _configService = configService;
        }
        public SalaryRateHandler SalaryRateHandler
        {
            get
            {
                if(rateHandlers==null)
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
