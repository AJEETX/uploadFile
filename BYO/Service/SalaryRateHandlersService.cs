using BYO.Domain;
using System.Linq;

namespace BYO.Service
{
    public interface ISalaryRateHandlersService
    {
        SalaryRateHandler SalaryRateHandler { get; }
    }
    public class SalaryRateHandlersService : ISalaryRateHandlersService
    {
        IConfigService _configService;
        static bool IsSalaryRatehandlerSet { get; set; } = false;
        static SalaryRateHandler salaryRateHandler = null;
        public SalaryRateHandlersService(IConfigService configService)
        {
            _configService = configService;
        }
        public SalaryRateHandler SalaryRateHandler
        {
            get{
                if (!IsSalaryRatehandlerSet){
                    try
                    {
                        var salaryRateHandlers = _configService.GetSection<SalaryRateHandlers>(nameof(SalaryRateHandlers));
                        for (int i = 0; i < salaryRateHandlers.SalaryRateHandlerList.Count() - 1; i++)

                            salaryRateHandlers.SalaryRateHandlerList.ElementAt(i).SetNextHandler(salaryRateHandlers.SalaryRateHandlerList.ElementAt(i + 1));

                        IsSalaryRatehandlerSet = true;

                        salaryRateHandler = salaryRateHandlers.SalaryRateHandlerList.First();
                    }
                    catch
                    {
                        //Shout // Log //throw;
                    }
                } return salaryRateHandler;
            }
        }
    }
}
