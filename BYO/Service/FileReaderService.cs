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
        Task<List<InputModel>> ReadInput(IFormFile file);
    }
    public class FileReaderService : IFileReaderService
    {
        public async Task<List<InputModel>> ReadInput(IFormFile file)
        {
            List<InputModel> inputData = null;
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
        async Task<List<InputModel>> Read(IFormFile file)
        {
            var filePath = Path.GetTempFileName();

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var jsonInput = await System.IO.File.ReadAllTextAsync(filePath);

            var input = JsonConvert.DeserializeObject<List<InputModel>>(jsonInput);

            return input;
        }
    }
}
