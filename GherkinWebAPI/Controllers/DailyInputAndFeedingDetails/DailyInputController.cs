using GherkinWebAPI.Core.DailyInputAndFeedingDetails;
using GherkinWebAPI.DTO.DailyInputAndFeedingDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GherkinWebAPI.Controllers.DailyInputAndFeedingDetails
{
    [Route("api/V1/[Controller]")]
    public class DailyInputController : ApiController
    {
        private readonly IDailyInputService _service;
        private readonly string controller = nameof(DailyInputController);
        public DailyInputController(IDailyInputService dailyInputService)
        {
            _service = dailyInputService;
        }
        [HttpGet]
        [Route("GetAreaWiseEmployeeDetails/{areaId}")]
        public async Task<IHttpActionResult> GetAreaWiseEmployeeDetails(string areaId)
        {
            try
            {
                var res = await _service.GetAreaWiseEmployeeDetails(areaId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(DailyInputController.GetAreaWiseEmployeeDetails)}");
                return InternalServerError();
            }
        }
        [HttpGet]
        [Route("GetAreaWiseCropGroup/{areaId}")]
        public async Task<IHttpActionResult> GetAreaWiseCropGroup(string areaId)
        {
            try
            {
                var res = await _service.GetAreaWiseCropGroup(areaId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(DailyInputController.GetAreaWiseCropGroup)}");
                return InternalServerError();
            }
        }
        [HttpGet]
        [Route("GetCropName/{cropGroup}")]
        public async Task<IHttpActionResult> GetCropNameCode(string cropGroup)
        {
            try
            {
                var res = await _service.GetCropNameCode(cropGroup);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(DailyInputController.GetCropNameCode)}");
                return InternalServerError();
            }
        }
        [HttpGet]
        [Route("GetAreaWiseSeasonToFrom/{areaId}")]
        public async Task<IHttpActionResult> GetAreaWiseSeasonToFrom(string areaId)
        {
            try
            {
                var res = await _service.GetAreaWiseSeasonToFrom(areaId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(DailyInputController.GetAreaWiseSeasonToFrom)}");
                return InternalServerError();
            }
        }
        [HttpGet]
        [Route("GetAreaWiseCountry/{areaId}")]
        public async Task<IHttpActionResult> GetAreaWiseCountry(string areaId)
        {
            try
            {
                var res = await _service.GetAreaWiseCountry(areaId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(DailyInputController.GetAreaWiseCountry)}");
                return InternalServerError();
            }
        }
        [HttpGet]
        [Route("GetAreaWiseState/{areaId}")]
        public async Task<IHttpActionResult> GetAreaWiseState(string areaId)
        {
            try
            {
                var res = await _service.GetAreaWiseState(areaId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(DailyInputController.GetAreaWiseState)}");
                return InternalServerError();
            }
        }
        [HttpGet]
        [Route("GetStateWiseDistrict/{statecode}")]
        public async Task<IHttpActionResult> GetStateWiseDistrict(int statecode)
        {
            try
            {
                var res = await _service.GetStateWiseDistrict(statecode);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(DailyInputController.GetStateWiseDistrict)}");
                return InternalServerError();
            }
        }
        [HttpGet]
        [Route("GetDistrictWiseMandal/{districtCode}")]
        public async Task<IHttpActionResult> GetDistrictWiseMandal(int districtCode)
        {
            try
            {
                var res = await _service.GetDistrictWiseMandal(districtCode);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(DailyInputController.GetDistrictWiseMandal)}");
                return InternalServerError();
            }
        }
        [HttpGet]
        [Route("GetMandalWiseVillage/{mandalCode}")]
        public async Task<IHttpActionResult> GetMandalWiseVillage(int mandalCode)
        {
            try
            {
                var res = await _service.GetMandalWiseVillage(mandalCode);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(DailyInputController.GetMandalWiseVillage)}");
                return InternalServerError();
            }
        }
        [HttpPost]
        [Route("GetAreaWiseFarmerAgreementDetails")]
        public async Task<IHttpActionResult> GetAreaWiseFarmerAgreementDetails(AreaIdPSNumberDTO obj)
        {
            try
            {
                var res = await _service.GetAreaWiseFarmerAgreementDetails(obj.areaId,obj.psNumber);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(DailyInputController.GetAreaWiseFarmerAgreementDetails)}");
                return InternalServerError();
            }
        }
        [HttpPost]
        [Route("SearchFarmers")]
        public async Task<IHttpActionResult> SearchFarmers(string keyword, AreaIdPSNumberDTO obj)
        {
            try
            {
                var res = await _service.SearchFarmers(keyword,obj.areaId,obj.psNumber);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(DailyInputController.SearchFarmers)}");
                return InternalServerError();
            }
        }

    }
}
