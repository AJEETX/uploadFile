using BYO.Domain;
using System.Linq;

namespace BYO.Service
{
    public interface ISalaryRateHandlersService
    {
        SalaryRateHandler FirstSalaryRateHandler { get; }
    }
    public class SalaryRateHandlersService : ISalaryRateHandlersService
    {
        IConfigService _configService;
        static SalaryRateHandler salaryRateHandler = null;
        public SalaryRateHandlersService(IConfigService configService)
        {
            _configService = configService;
        }
        public SalaryRateHandler FirstSalaryRateHandler
        {
            get{
                try
                {
                    if(salaryRateHandler == null) GetFirstHandler();
                }
                catch
                {
                    //Shout // Log //throw;
                }
                 return salaryRateHandler;
            }
        }
        void GetFirstHandler()
        {
            var salaryRateHandlers = _configService.GetSection<SalaryRateHandlers>(nameof(SalaryRateHandlers));
            for (int i = 0; i < salaryRateHandlers.SalaryRateHandlerList.Count() - 1; i++)

                salaryRateHandlers.SalaryRateHandlerList.ElementAt(i).SetNextHandler(salaryRateHandlers.SalaryRateHandlerList.ElementAt(i + 1));

            salaryRateHandler= salaryRateHandlers.SalaryRateHandlerList.First();
        }
    }
}
