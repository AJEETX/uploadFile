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
        Task<List<OutputModel>> GetSalaryDetails(IFormFile file);
    }
    public class SalaryService : ISalaryService
    {
        private readonly IConfigService _configService;
        IFileReaderService _fileReaderService;
        public SalaryService(IConfigService configService,IFileReaderService fileReaderService)
        {
            _configService = configService;
            _fileReaderService = fileReaderService;
        }
        public async Task<List<OutputModel>> GetSalaryDetails(IFormFile file)
        {
            List<OutputModel> salaryData = null;
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
        async Task<List<OutputModel>> GetSalaryData(IFormFile file)
        {
            var inputs = await _fileReaderService.ReadInput(file);
            var salaryRates = _configService.GetSection<SalaryRateHandlers>(nameof(SalaryRateHandlers));
            for (int i = 0; i < salaryRates.SalaryRateHandlerList.Count - 1; i++)
            {
                salaryRates.SalaryRateHandlerList[i].SetNextHandler(salaryRates.SalaryRateHandlerList[i + 1]);
            }
            var ops = new List<OutputModel>();
            foreach (var input in inputs)
            {
                ops.Add(salaryRates.SalaryRateHandlerList[0].CalculateSalary(input));
            }
            return ops;
        }
    }
}
