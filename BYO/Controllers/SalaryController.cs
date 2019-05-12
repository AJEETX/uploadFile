using System.Collections.Generic;
using System.Linq;
using BYO.Model;
using BYO.Service;
using Microsoft.AspNetCore.Mvc;

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

        /// Action to post json
        /// </summary>
        /// <param name="json"></param>
        [HttpPost]
        public IActionResult Post(IEnumerable<InputModel> json)
        {
            if ((json == null || json.Count()==0)) return BadRequest();
            {
                try
                {
                    var salaryDetail = _salaryService.GetSalaryDetails(json);
                    return Ok(new { salaryDetail });
                }
                catch
                {
                    //Shot // Lof // throw
                }
            }
            return BadRequest();
        }
    }
}
