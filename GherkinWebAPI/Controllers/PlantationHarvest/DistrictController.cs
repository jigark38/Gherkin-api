using GherkinWebAPI.Core;
using GherkinWebAPI.Models;
using GherkinWebAPI.ValidateModel;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using DistrictDetail = GherkinWebAPI.Models.Districts.DistrictDetail;

namespace GherkinWebAPI.Controllers.PlantationHarvest
{
    [RoutePrefix("v1/district")]
    public class DistrictController : ApiController
    {
        private readonly IDistrictService _service;
        public readonly string controller = nameof(DistrictController);

        public DistrictController(IDistrictService service)
        {
            this._service = service;
        }

        [HttpGet, Route("GetAllDistrictsByStateCode")]
        public async Task<List<District>> GetAllDistrictsByStateCode(int stateCode)
        {
            return await _service.GetAllDistrictsByStateAsync(stateCode);
        }

        [HttpGet, Route("GetDistrictByCode")]
        public async Task<District> GetDistrictById(int districtCode)
        {
            return await _service.GetDistrictByIdAsync(districtCode);
        }

        [HttpPost]
        [Route("AddDistrict")]
        [ValidateModelState]
        public async Task<IHttpActionResult> AddDistrict([FromBody] DistrictDetail districtDetail)
        {
            try
            {
                var district = await _service.AddDistrict(districtDetail);

                if (district != null)
                {
                    return Ok(district);
                }
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(DistrictController.AddDistrict)}");
                return InternalServerError();
            }
        }
    }
}