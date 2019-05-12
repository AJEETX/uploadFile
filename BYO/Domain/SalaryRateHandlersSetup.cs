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
        static bool IsSalaryRatehandlerSet { get; set; } = false;
        SalaryRateHandlers  rateHandlers => _configService.GetSection<SalaryRateHandlers>(nameof(SalaryRateHandlers));
        static SalaryRateHandler rateHandler = null;
        public SalaryRateHandlersSetup(IConfigService configService)
        {
            _configService = configService;
        }
        public SalaryRateHandler SalaryRateHandler
        {
            get
            {
                if (!IsSalaryRatehandlerSet)
                {
                    try
                    {
                        rateHandler = Setup();
                    }
                    catch
                    {
                        //Shout // Log //throw;
                    }
                }
                   
                return rateHandler;
            }

        }
        SalaryRateHandler Setup()
        {
            SalaryRateHandlers tmpSalaryRatehandlers = rateHandlers;

            for (int i = 0; i < tmpSalaryRatehandlers.SalaryRateHandlerList.Count() - 1; i++)

                tmpSalaryRatehandlers.SalaryRateHandlerList.ElementAt(i).SetNextHandler(tmpSalaryRatehandlers.SalaryRateHandlerList.ElementAt(i+1));

            IsSalaryRatehandlerSet = true;

            return tmpSalaryRatehandlers.SalaryRateHandlerList.First();
        }
    }
}
