using BYO.Domain;
using BYO.Model;
using System.Collections.Generic;
using System.Linq;

namespace BYO.Service
{
    public interface ISalaryCalculatorService
    {
        IEnumerable<OutputModel> CalculateSalary(IEnumerable<InputModel> inputs, SalaryRateHandler salaryRate);
    }
    public class SalaryCalculatorService : ISalaryCalculatorService
    {
        public IEnumerable<OutputModel> CalculateSalary(IEnumerable<InputModel> inputs, SalaryRateHandler salaryRate)
        {
            OutputModel output= null;

            if (inputs == null || inputs.Count() == 0 || salaryRate == null) yield return null;

            foreach (var input in inputs)
            {
                try
                {
                    output =salaryRate.CalculateSalary(input);
                }
                catch
                { 
                    //  throw;
                }
                 yield return output;
            }
        }
    }
}
