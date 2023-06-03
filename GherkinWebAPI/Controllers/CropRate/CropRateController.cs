using GherkinWebAPI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Http;
using Newtonsoft.Json;
using GherkinWebAPI.Models;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Utilities;

namespace GherkinWebAPI.Controllers
{
    [Route("api/V1/[Controller]")]
    public class CropRateController : ApiController
    {
        private readonly ICropService _service;
        private readonly string controller = nameof(CropRateController);
        public CropRateController(ICropService cropService)
        {
            _service = cropService;
        }

        [HttpGet]
        [Route("GetCropRate")]
        public async Task<IHttpActionResult> GetCropRate()
        {
            try
            {
                var res = await _service.GetAllCropRates();
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(CropRateController.GetCropRate)}");
                return InternalServerError();
            }
        }
        [HttpGet]
        [Route("GetCropRateByCode")]
        public async Task<IHttpActionResult> GetCropRateByCode(string cropSchemeCode, decimal cropCountMM)
        {
            try
            {
                var res = await _service.GetCropRateByCode(cropSchemeCode, cropCountMM);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(CropRateController.GetCropRateByCode)}");
                return InternalServerError();
            }
        }
        [HttpGet]
        [Route("GetAllGroupName")]
        public async Task<IHttpActionResult> GetAllGroupName()
        {
            try
            {
                var res = await _service.GetAllGroupName();
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(CropRateController.GetAllGroupName)}");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetCropNameCode/{groupCode}")]
        public async Task<IHttpActionResult> GetCropNameCode(string groupCode)
        {
            List<CropName> designations = new List<CropName>();
            try
            {
                designations = await _service.GetCropNameCode(groupCode);
                return Ok(designations);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in DesignationController/{nameof(CropRateController.GetCropNameCode)}");
                return InternalServerError();
            }

        }
        [HttpGet]
        [Route("GetAllAreas")]
        public async Task<IHttpActionResult> GetAllAreas()
        {
            try
            {
                var res = await _service.GetAllAreas();
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(CropRateController.GetAllAreas)}");
                return InternalServerError();
            }
        }
        [HttpGet]
        [Route("GetVillageCode/{areaId}")]
        public async Task<IHttpActionResult> GetVillageCode(string areaId)
        {
            try
            {
                var res = await _service.GetVillageCode(areaId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(CropRateController.GetVillageCode)}");
                return InternalServerError();
            }
        }
        [HttpGet]
        [Route("GetSeasonFromTo/{CNameCode}")]
        public async Task<IHttpActionResult> GetSeasonFromTo(string CNameCode)
        {
            try
            {
                var res = await _service.GetSeasonFromTo(CNameCode);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(CropRateController.GetSeasonFromTo)}");
                return InternalServerError();
            }
        }
        [HttpGet]
        [Route("GetCropCountMM/{PS_number}")]
        public async Task<IHttpActionResult> GetCropCountMM(string PS_number)
        {
            try
            {
                var res = await _service.GetCropCountMM(PS_number);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(CropRateController.GetCropCountMM)}");
                return InternalServerError();
            }
        }
        [HttpGet]
        [Route("GetFruitSizeCount/{CropCountMM}")]
        public async Task<IHttpActionResult> GetFruitSizeCount(string CropCountMM)
        {
            try
            {
                decimal cropCont = Convert.ToDecimal(CropCountMM);
                var res = await _service.GetFruitSizeCount(cropCont);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(CropRateController.GetFruitSizeCount)}");
                return InternalServerError();
            }
        }
        [HttpGet]
        [Route("GetCropRateUOM/{cropRate}")]
        public async Task<IHttpActionResult> GetCropRateUOM(string cropRate)
        {
            try
            {

                var res = await _service.GetCropRateUOM(cropRate);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(CropRateController.GetFruitSizeCount)}");
                return InternalServerError();
            }
        }
        [Route("AddCropRate"), HttpPost]
        public async Task<IHttpActionResult> AddCropRate([FromBody] List<CropRateDTO> cropRate)
        {
            try
            {
                //await 
                var res = await _service.AddCropRate(cropRate);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(CropRateController.AddCropRate)}");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("FindCropRateAccordingToSeason/{psNumber}")]
        public async Task<IHttpActionResult> FindCropRateAccordingToSeason(string psNumber)
        {
            try
            {
               
                var res = await _service.FindCropRateAccordingToSeason(psNumber);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(CropRateController.FindCropRateAccordingToSeason)}");
                return InternalServerError();
            }
        }

        [Route("ModifySelectedCropRate"), HttpPost]
        public async Task<IHttpActionResult> ModifySelectedCropRate([FromBody] CropRateDTO cropRate)
        {
            try
            {

                var res = await _service.ModifySelectedCropRate(cropRate);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(CropRateController.ModifySelectedCropRate)}");
                return InternalServerError();
            }
        }
        [HttpGet]
        [Route("DeleteSelectedCropRate/{cropRateNo}")]
        public async Task<IHttpActionResult> DeleteSelectedCropRate(string cropRateNo)
        {
            try
            {

                var res = await _service.DeleteSelectedCropRate(cropRateNo);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(CropRateController.DeleteSelectedCropRate)}");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetAllVillageCode")]
        public async Task<IHttpActionResult> GetAllVillageCode()
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            try
            {
                var res = await _service.GetAllVillageCode();
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

        [HttpGet]
        [Route("GetAllSeasonFromTo")]
        public async Task<IHttpActionResult> GetAllSeasonFromTo()
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            try
            {
                var res = await _service.GetAllSeasonFromTo();
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
