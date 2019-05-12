using BYO.Domain;
using BYO.Model;
using System;
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
            foreach (var input in inputs)
            {
                var output = salaryRate.CalculateSalary(input);
                yield return output.Result;
            }
        }
    }
}
