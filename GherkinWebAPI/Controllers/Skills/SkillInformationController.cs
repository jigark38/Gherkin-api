using GherkinWebAPI.Core;
using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GherkinWebAPI.Controllers
{
    [Route("api/V1/[Controller]")]
    public class SkillInformationController : ApiController
    {
        private readonly ISkillInformationService _service;
        public SkillInformationController(ISkillInformationService skillInfoService)
        {
            _service = skillInfoService;
        }
        [HttpGet]
        [Route("GetAllSkillsInformation")]
        public async Task<IHttpActionResult> GetAllSkillsInformation()
        {
            List<SkillInformation> skillInfos = new List<SkillInformation>();
            try
            {
                skillInfos = await _service.GetAllSkillsInformation();
                return Ok(skillInfos);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in SkillInformationController/{nameof(SkillInformationController.GetAllSkillsInformation)}");
                return InternalServerError();
            }

        }


        [HttpPost]
        [Route("CreateSkillInformation")]
        public async Task<IHttpActionResult> CreateSkillInformation([FromBody] SkillInformation skillIndo)
        {
            try
            {
                var degn = await _service.CreateSkillInformation(skillIndo);
                return Ok(degn);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in SkillInformationController/{nameof(SkillInformationController.CreateSkillInformation)}");
                return InternalServerError();
            }

        }

        [HttpPost]
        [Route("UpdateSkillInformation")]
        public async Task<IHttpActionResult> UpdateSkillInformation([FromBody] SkillInformation skillIndo)
        {
            try
            {
                var degn = await _service.UpdateSkillInformation(skillIndo);
                return Ok(degn);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}

