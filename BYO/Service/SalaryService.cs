using BYO.Domain;
using BYO.Model;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BYO.Service
{
    public interface ISalaryService
    {
        Task<IEnumerable<OutputModel>> GetSalaryDetails(IFormFile file);
        IEnumerable<OutputModel> GetSalaryDetails(IEnumerable<InputModel> inputs);
    }
    public class SalaryService : ISalaryService
    {
        private readonly ISalaryRateHandlersSetup _salaryRateHandlersSetup;
        IFileReaderService _fileReaderService;
        public SalaryService(ISalaryRateHandlersSetup salaryRateHandlersSetup, IFileReaderService fileReaderService)
        {
            _salaryRateHandlersSetup = salaryRateHandlersSetup;
            _fileReaderService = fileReaderService;
        }
        public async Task<IEnumerable<OutputModel>> GetSalaryDetails(IFormFile file)
        {
            IEnumerable<OutputModel> salaryData = null;
            if (file == null || file.Length == 0) return salaryData;
            try
            {
                var inputs = await _fileReaderService.ReadInput(file);
                var salaryRates = _salaryRateHandlersSetup.SetupChain();
                salaryData = CalculateSalary(inputs, salaryRates);
            }
            catch (Exception)
            {
                //Shot // Log // Throw
            }
            return salaryData;
        }

        public IEnumerable<OutputModel> GetSalaryDetails(IEnumerable<InputModel> inputs)
        {
            IEnumerable<OutputModel> salaryData = null;
            if (inputs == null) return salaryData;
           
            var salaryRates = _salaryRateHandlersSetup.SetupChain();
            var salary = CalculateSalary(inputs, salaryRates);
             return salary;
        }
         IEnumerable<OutputModel> CalculateSalary(IEnumerable<InputModel> inputs, SalaryRateHandler salaryRate)
        {
            foreach (var input in inputs)
            {
                var output = salaryRate.CalculateSalary(input);
                yield return output.Result;
            }
        }
    }
}
