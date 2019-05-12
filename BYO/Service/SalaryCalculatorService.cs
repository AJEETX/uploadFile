using BYO.Domain;
using BYO.Model;
using System.Collections.Generic;

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
            foreach (var input in inputs)
            {
                var output = salaryRate.CalculateSalary(input);
                if(output.IsCompleted)
                yield return output.Result;
            }
        }
    }
}
