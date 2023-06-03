using GherkinWebAPI.Core;
using GherkinWebAPI.Core.Villages;
using GherkinWebAPI.Models;
using GherkinWebAPI.Utilities;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Routing;
using VillageDetail = GherkinWebAPI.Models.Villages.VillageDetail;

namespace GherkinWebAPI.Controllers.PlantationHarvest
{
    //[Route("api/v1/[Contoller]")]
    public class VillageController : ApiController
    {
        private readonly IVillageService _service;
        private readonly ILogger<Village> _logger;
        public readonly string controller = nameof(FarmersAgreementController);
        private readonly ICountryService _countryService;
        public VillageController(IRepositoryWrapper repository, IVillageService service, ICountryService country)
        {
            _service = service;
            _countryService = country;
        }

        [HttpPost]
        [Route("AddVillage")]
        public async Task<IHttpActionResult> AddVillage([FromBody] VillageDetail villageDetail)
        {
            try
            {
                var result = await _service.AddVillage(villageDetail);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                    return BadRequest();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(VillageController.AddVillage)}");
                return InternalServerError();

            }
        }

        [HttpGet]
        [Route("GetAllVillages")]
        public async Task<IHttpActionResult> GetVillagesAsync()
        {
            try
            {
                var villages = await _service.GetAllVillagesAsync();
                if (villages.Count > 0)
                {
                    return Ok(villages);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(VillageController.GetVillagesAsync)}");
                return InternalServerError();
            }

        }

        [HttpGet]
        [Route("GetAllVillageByCode/{villageCode}")]
        public async Task<IHttpActionResult> GetVillageByCodeAsync(int villageCode)
        {
            try
            {
                var village = await _service.GetVillageByCodeAsync(villageCode);
                if (village != null)
                {
                    return Ok(village);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(VillageController.GetVillageByCodeAsync)}");
                return InternalServerError();
            }

        }

        [HttpGet]
        [Route("GetVillagesByMandalCode/{mandalCode}")]
        public async Task<IHttpActionResult> GetVillagesByMandalCodeAsync(int mandalCode)
        {
            try
            {
                var villages = await _service.GetVillageByMandalCodeAsync(mandalCode);
                if (villages.Count > 0)
                {
                    return Ok(villages);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(VillageController.GetVillagesByMandalCodeAsync)}");
                return InternalServerError();
            }

        }

        [HttpGet]
        [Route("GetVillageListByAreaAndEmployee/{areaId}/{employeeId}/{villageName}")]
        public async Task<IHttpActionResult> GetVillageListByAreaAndEmployee(string areaId, string employeeId, string villageName)
        {
            try
            {
                var villages = await _service.GetVillageListByAreaAndEmployee(areaId, employeeId, villageName);
                return Ok(villages);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(VillageController.GetVillageListByAreaAndEmployee)}");
                return InternalServerError();
            }

        }

        [HttpGet]
        [Route("GetCountryInfoByVillageName")]
        public async Task<IHttpActionResult> GetCountryInfoByVillageName(string villageName)
        {
             ApiResponse<object> apiResponse = new ApiResponse<object>();
            try
            {
                var res = await _service.GetAllVillagesAsync();
                var countryCode = res.Find(x => x.VillageName.ToLower() == villageName.ToLower()).CountryCode;
                if (countryCode != null)
                {
                    var data = await _countryService.GetCountryByIdAsync(countryCode);
                    apiResponse.IsSucceed = true;
                    apiResponse.Data = data;
                }
                else
                {
                    apiResponse.IsSucceed = true;
                    apiResponse.Data = null;
                }
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                apiResponse.IsSucceed = false;
                apiResponse.Exception = ex;
                return Ok(apiResponse);
            }

        }

        [HttpGet]
        [Route("GetVillageDetailsByName")]
        public async Task<IHttpActionResult> GetVillageDetailsByName(string villageName)
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            try
            {
                var res = await _service.GetCountryInfoByVillageName(villageName);
                if (res != null)
                {
                    apiResponse.IsSucceed = true;
                    apiResponse.Data = res;
                }
                else
                {
                    apiResponse.IsSucceed = true;
                    apiResponse.Data = null;
                }
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                apiResponse.IsSucceed = false;
                apiResponse.Exception = ex;
                return Ok(apiResponse);
            }

        }
    }
}
