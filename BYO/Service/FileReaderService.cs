using BYO.Model;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BYO.Service
{
    public interface IFileReaderService
    {
        Task<IEnumerable<InputModel>> ReadInput(IFormFile file);
    }
    public class FileReaderService : IFileReaderService
    {
        public async Task<IEnumerable<InputModel>> ReadInput(IFormFile file)
        {
            IEnumerable<InputModel> inputData = null;
            if (file == null) return inputData;
            try
            {
                inputData =await Read(file);
            }
            catch (Exception)
            {
                //Shout//Log //throw
            }

            return inputData;
        }
        async Task<IEnumerable<InputModel>> Read(IFormFile file)
        {
            var filePath = Path.GetTempFileName();

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var jsonInput = await File.ReadAllTextAsync(filePath);

            return JsonConvert.DeserializeObject<IEnumerable<InputModel>>(jsonInput);
        }
    }
}
