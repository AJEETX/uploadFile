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
                        var handlers = _configService.GetSection<SalaryRateHandlers>(nameof(SalaryRateHandlers));
                        rateHandler = Setup(handlers); 
                    }
                    catch
                    {
                        //Shout // Log //throw;
                    }
                }
                return rateHandler;
            }

        }
        SalaryRateHandler Setup(SalaryRateHandlers rateHandlers)
        {
            for (int i = 0; i < rateHandlers.SalaryRateHandlerList.Count() - 1; i++)

                rateHandlers.SalaryRateHandlerList.ElementAt(i).SetNextHandler(rateHandlers.SalaryRateHandlerList.ElementAt(i+1));

            IsSalaryRatehandlerSet = true;

            return rateHandlers.SalaryRateHandlerList.First();
        }
    }
}
