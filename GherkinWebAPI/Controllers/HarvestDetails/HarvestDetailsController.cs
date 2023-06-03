using GherkinWebAPI.Core.HarvestDetails;
using GherkinWebAPI.DTO.HarvestDetails;
using GherkinWebAPI.Models.HarvestDeatils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace GherkinWebAPI.Controllers.HarvestDetails
{
    [Route("api/V1/[Controller]")]
    public class HarvestDetailsController : ApiController
    {
        readonly IHarvestDetailsService _harvestDetailsService;

        public HarvestDetailsController(IHarvestDetailsService harvestDetailsService)
        {
            _harvestDetailsService = harvestDetailsService;
        }

        [HttpGet]
        [Route("GetFarmerDetails")]
        public async Task<IHttpActionResult> GetFarmerDetails(string areaId, string psNumber)
        {
            try
            {
                List<FarmerDetailsDto> farmerDetails = await  _harvestDetailsService.GetFarmerDetails(areaId, psNumber);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }

            return Ok();
        }

        [Route("AddHarvestFarmersDetails"), HttpPost]
        public async Task<IHttpActionResult> AddHarvestFarmersDetails(HarvestProcurementDetails harvestDetailsDto)
        {
            try
            {
                 await _harvestDetailsService.AddHarvestFarmersDetails(harvestDetailsDto);
                return Ok();
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }
    }
}