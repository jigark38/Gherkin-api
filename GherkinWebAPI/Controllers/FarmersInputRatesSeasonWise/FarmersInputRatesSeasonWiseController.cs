using GherkinWebAPI.Core;
using GherkinWebAPI.Models;
using GherkinWebAPI.Utilities;
using GherkinWebAPI.ValidateModel;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace GherkinWebAPI.Controllers
{
    [Route("api/V1/FarmersInputRatesSeasonWise")]
    public class FarmersInputRatesSeasonWiseController : ApiController
    {
        private readonly IFarmersInputRatesSeasonWiseService _service;
        public FarmersInputRatesSeasonWiseController(IFarmersInputRatesSeasonWiseService farmersInputRatesSeasonWiseService)
        {
            _service = farmersInputRatesSeasonWiseService;
        }
        [HttpGet]
        [Route("GetFarmerInputAreaDetails/{PS_Number}")]
        public async Task<IHttpActionResult> GetFarmerInputAreaDetails(string PS_Number)
        {
          try
            {
                var HarvestAreaDetails = await _service.GetFarmerInputAreaDetails(PS_Number);
                return Ok(HarvestAreaDetails);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in FarmersInputRatesSeasonWiseController/{nameof(FarmersInputRatesSeasonWiseController.GetFarmerInputAreaDetails)}");
                return InternalServerError();
            }

        }
        [HttpGet]
        [Route("GetMaterialDetails")]
        public async Task<IHttpActionResult> GetMaterialDetails()
        {
            try
            {
              var RawMaterialsDetails = await _service.GetMaterialDetails();
              return Ok(RawMaterialsDetails);
              
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in FarmersInputRatesSeasonWiseController/{nameof(FarmersInputRatesSeasonWiseController.GetMaterialDetails)}");
                return InternalServerError();
            }

        }

        [CheckModelForNull]
        [ValidateModelState]
        [HttpPost]
        [Route("CreateFarmersInputRatesSeasonWise")]
        public async Task<IHttpActionResult> CreateFarmersInputRatesSeasonWise([FromBody] FarmersInputRatesSeasonWise farmersInputRatesSeasonWise)
        {
            try
            {
                var result = await _service.CreateFarmersInputRatesSeasonWise(farmersInputRatesSeasonWise);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in FarmersInputRatesSeasonWiseController / {nameof(FarmersInputRatesSeasonWiseController.CreateFarmersInputRatesSeasonWise)}");
                return InternalServerError();
            }
        }

        [CheckModelForNull]
        [ValidateModelState]
        [Route("UpdateFarmersInputRatesSeasonWise")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateFarmersInputRatesSeasonWise([FromBody] FarmersInputRatesSeasonWise farmersInputRatesSeasonWise)
        {
            try
            {
                var result = await _service.UpdateFarmersInputRatesSeasonWise(farmersInputRatesSeasonWise);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in FarmersInputRatesSeasonWiseController / {nameof(FarmersInputRatesSeasonWiseController.UpdateFarmersInputRatesSeasonWise)}");
                return InternalServerError();
            }
        }
        [HttpGet]
        [Route("GetStatesbyCropSeason")]
        public async Task<IHttpActionResult> GetStatesbyCropSeason(string cropGroupCode, string cropNameCode, int countryCode, string PSNumber)
        {
            try
            {
                var states = await _service.GetStatesbyCropSeason(cropGroupCode, cropNameCode, countryCode, PSNumber);
                return Ok(states);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in FarmersInputRatesSeasonWiseController/{nameof(FarmersInputRatesSeasonWiseController.GetStatesbyCropSeason)}");
                return InternalServerError();
            }

        }
        [HttpGet]
        [Route("FindStatesbyCropSeason")]
        public async Task<IHttpActionResult> FindStatesbyCropSeason(string cropGroupCode, string cropNameCode, int countryCode, string PSNumber)
        {
            try
            {
                var states = await _service.FindStatesbyCropSeason(cropGroupCode, cropNameCode, countryCode, PSNumber);
                return Ok(states);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in FarmersInputRatesSeasonWiseController/{nameof(FarmersInputRatesSeasonWiseController.FindStatesbyCropSeason)}");
                return InternalServerError();
            }

        }

        [HttpGet]
        [Route("GetFarmerInputRateSeason")]
        public async Task<IHttpActionResult> GetFarmerInputRateSeason(string cropGroupCode, string cropNameCode, int countryCode, string PSNumber, int stateCode)
        {
            try
            {
                var states = await _service.GetFarmerInputRateSeason(cropGroupCode, cropNameCode, countryCode, PSNumber, stateCode);
                return Ok(states);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in FarmersInputRatesSeasonWiseController/{nameof(FarmersInputRatesSeasonWiseController.GetFarmerInputRateSeason)}");
                return InternalServerError();
            }

        }

        [HttpGet]
        [Route("GetEmployeesByDeptCode")]
        public async Task<IHttpActionResult> GetEmployeesByDeptCode(string deptCode)
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            try
            {
                var employees = await _service.GetEmployeesByDeptCode(deptCode);
                apiResponse.IsSucceed = true;
                apiResponse.Data = employees;
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
