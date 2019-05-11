using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BYO.Model;
using BYO.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BYO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryController : ControllerBase
    {
        ISalaryService _salaryService;
        public SalaryController(ISalaryService  salaryService)
        {
            _salaryService = salaryService;
        }
        /// Action to upload file
        /// </summary>
        /// <param name="file"></param>
        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> PostFile(IFormFile file)
        {

            if (file.Length > 0)
            {
                var filePath = Path.GetTempFileName();

                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var jsonInput = await System.IO.File.ReadAllTextAsync(filePath);

                var input = JsonConvert.DeserializeObject<List<InputModel>>(jsonInput);
                var salaryDetail = _salaryService.GetSalaryDetails(input);
                return Ok(new { salaryDetail });
            }
            return BadRequest();
        }
    }
}
