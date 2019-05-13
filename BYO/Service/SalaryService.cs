using BYO.Model;
using System.Collections.Generic;
using System.Linq;

namespace BYO.Service
{
    public interface ISalaryService
    {
        IEnumerable<OutputModel> GetSalaryDetails(IEnumerable<InputModel> inputs);
    }
    public class SalaryService : ISalaryService
    {
        ISalaryRateHandlersService _salaryRateHandlersService;
        ISalaryCalculatorService _salaryCalculatorService;
        public SalaryService(ISalaryRateHandlersService salaryRateHandlersService, ISalaryCalculatorService salaryCalculatorService)
        {
            _salaryRateHandlersService = salaryRateHandlersService;
            _salaryCalculatorService = salaryCalculatorService;
        }

        public IEnumerable<OutputModel> GetSalaryDetails(IEnumerable<InputModel> inputs)
        {
            IEnumerable<OutputModel> salaryData = null;
            if (inputs == null || inputs.Count()==0) return salaryData;
            try
            {
                var salaryRateHandler = _salaryRateHandlersService.SalaryRateHandler;

                if (salaryRateHandler == null) return salaryData;

                salaryData = _salaryCalculatorService.CalculateSalary(inputs, salaryRateHandler);
            }
            catch
            {
                //shout // Log
            } return salaryData;
        }
    }
}
