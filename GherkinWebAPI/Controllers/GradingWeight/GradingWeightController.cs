using GherkinWebAPI.Core;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;
using GherkinWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GherkinWebAPI.Controllers.GradingWeight
{
    [RoutePrefix("api/V1/GradingWeight")]
    public class GradingWeightController : ApiController
    {
        private IGradingWeightService _gradingWeightService;
        public GradingWeightController(IGradingWeightService gradingWeightService)
        {
            _gradingWeightService = gradingWeightService;
        }

        [HttpGet]
        [Route("GetLocation")]
        public async Task<IHttpActionResult> GetOrganisationOfficesLocationsDetails()
        {
            try
            {
                var data = await _gradingWeightService.GetOrganisationOfficesLocationsDetails();
                return Ok(data);
            }
            catch (Exception ex)
            {
                ExceptonLog.LogError(ex, "GradingWeightController", "GetOrganisationOfficesLocationsDetails");
            }
            return NotFound();
        }

        [HttpGet]
        [Route("GetGridOne")]
        public async Task<IHttpActionResult> GetGridOne(int OrgofficeNo)
        {
            try
            {
                var data = await _gradingWeightService.GetGridOneData(OrgofficeNo);
                return Ok(data);
            }
            catch (Exception ex)
            {
                ExceptonLog.LogError(ex, "GradingWeightController", "GetGridOne");
            }
            return NotFound();
        }


        [HttpPost]
        [Route("SaveGreensGrading")]
        public async Task<IHttpActionResult> SaveGreensGrading(GreensGradingInwardDetailsDTO GreensGradingInwardDetail)
        {
            var response = await _gradingWeightService.SaveGreensGrading(GreensGradingInwardDetail);
            return Ok(response);
        }


        [HttpGet]
        [Route("GetGreensGradingByGrdNo")]
        public async Task<IHttpActionResult> GetGreensGradingByGrdNo(int greensGrdNo)
        {
            try
            {
                var data = await _gradingWeightService.GetGreensGradingByGrdNo(greensGrdNo);
                return Ok(data);
            }
            catch (Exception ex)
            {
                ExceptonLog.LogError(ex, "GradingWeightController", "GetGreensGradingByGrdNo");
            }
            return NotFound();
        }

        [HttpGet]
        [Route("ChangeStatus")]
        public async Task<IHttpActionResult> ChangeStatus(int greensGrdNo)
        {
            try
            {
                var data = await _gradingWeightService.ChangeStatus(greensGrdNo);
                return Ok(data);
            }
            catch (Exception ex)
            {
                ExceptonLog.LogError(ex, "GradingWeightController", "GetGreensGradingByGrdNo");
            }
            return NotFound();
        }
    }
}
