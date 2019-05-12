﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BYO.Model;
using BYO.Service;
using Microsoft.AspNetCore.Http;
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
        /// Action to upload file
        /// </summary>
        /// <param name="file"></param>
        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> PostFile(IFormFile file)
        {
            if (file.Length > 0)
            {
                try
                {
                    var salaryDetail = await _salaryService.GetSalaryDetails(file);
                    return Ok(new { salaryDetail });
                }
                catch (Exception)
                {
                    //Shot // Lof // throw
                }

            }
            return BadRequest();
        }
        /// Action to post json
        /// </summary>
        /// <param name="fijsonle"></param>
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
                catch (Exception)
                {
                    //Shot // Lof // throw
                }

            }
            return BadRequest();
        }
    }
}
