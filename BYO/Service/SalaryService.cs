using BYO.Domain;
using BYO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BYO.Service
{
    public interface ISalaryService
    {
        List<OutputModel> GetSalaryDetails(List<InputModel> input);
    }
    public class SalaryService : ISalaryService
    {
        private readonly IConfigService _configService;
        public SalaryService(IConfigService configService)
        {
            _configService = configService;
        }
        public List<OutputModel> GetSalaryDetails(List<InputModel> inputs)
        {
            var salaryRates = _configService.GetSection<SalaryRates>(nameof(SalaryRates));
            for(int i=0;i< salaryRates.SalaryRate.Count-1; i++)
            {
                salaryRates.SalaryRate[i].SetNextHandler(salaryRates.SalaryRate[i + 1]);
            }
            var ops = new List<OutputModel>();
            foreach (var input in inputs)
            {
                ops.Add(salaryRates.SalaryRate[0].CalculateSalary(input));
            }
            return ops;
        }
    }
}
