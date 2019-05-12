using BYO.Domain;
using BYO.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BYO.Service
{
    public interface ISalaryService
    {
        Task<IEnumerable<OutputModel>> GetSalaryDetails(IFormFile file);
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
                salaryData = await GetSalaryData(file);
            }
            catch (Exception)
            {
                //Shot // Log // Throw
            }
            return salaryData;
        }
        async Task<IEnumerable<OutputModel>> GetSalaryData(IFormFile file)
        {
            return CalculateSalary(await _fileReaderService.ReadInput(file), _salaryRateHandlersSetup.SetupChain());
        }
        IEnumerable<OutputModel> CalculateSalary(IEnumerable<InputModel> inputs, SalaryRateHandlers salaryRates)
        {
            foreach (var input in inputs)
                yield return (salaryRates.SalaryRateHandlerList.First().CalculateSalary(input));
        }
    }
}
