using BYO.Domain;
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
        ISalaryRateHandlersSetup _salaryRateHandlersSetup;
        ISalaryCalculatorService _salaryCalculatorService;
        public SalaryService(ISalaryRateHandlersSetup salaryRateHandlersSetup, ISalaryCalculatorService salaryCalculatorService)
        {
            _salaryRateHandlersSetup = salaryRateHandlersSetup;
            _salaryCalculatorService = salaryCalculatorService;
        }

        public IEnumerable<OutputModel> GetSalaryDetails(IEnumerable<InputModel> inputs)
        {
            IEnumerable<OutputModel> salaryData = null;
            if (inputs == null || inputs.Count()==0) return salaryData;
            try
            {
                var salaryRateHandler = _salaryRateHandlersSetup.SalaryRateHandler;

                if (salaryRateHandler == null) return salaryData;

                salaryData = _salaryCalculatorService.CalculateSalary(inputs, salaryRateHandler);
            }
            catch (System.Exception)
            {
                //shout // Log
            }
             return salaryData;
        }
    }
}
