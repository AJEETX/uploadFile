using BYO.Domain;
using BYO.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            Task <OutputModel> output= null;

            if (inputs == null || inputs.Count() == 0 || salaryRate == null) yield return null;

            foreach (var input in inputs)
            {
                try
                {
                    output = salaryRate.CalculateSalary(input);
                }
                catch
                { 
                    //  throw;
                }
                if (output.IsCompleted) yield return output.Result;
            }
        }
    }
}
