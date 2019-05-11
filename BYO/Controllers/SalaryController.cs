using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BYO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryController : ControllerBase
    {
        /// Action to upload file
        /// </summary>
        /// <param name="file"></param>
        [HttpPost]
        [Route("upload")]
        public void PostFile(IFormFile file)
        {
            var stream = file.OpenReadStream();
            var name = Path.GetFileName(file.FileName);
            var JSON = System.IO.File.ReadAllText(name);
            //TODO: Save file
        }

    }
}
