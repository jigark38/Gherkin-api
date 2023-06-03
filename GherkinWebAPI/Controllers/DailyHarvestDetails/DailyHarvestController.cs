using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using GherkinWebAPI.Core.DailyHarvestDetails;
using GherkinWebAPI.DTO.DailyHarvest;
using GherkinWebAPI.Models.DailyHarvestDetails;
using GherkinWebAPI.Utilities;
using GherkinWebAPI.ValidateModel;

namespace GherkinWebAPI.Controllers.TransportVehicleManagement
{
    [RoutePrefix("api/v1/DailyHarvest")]
    public class DailyHarvestController : ApiController
    {
        private readonly IDailyHarvestService _dailyHarvestService;
        public readonly string controller = nameof(DailyHarvestController);

        public DailyHarvestController(IDailyHarvestService service)
        {
            _dailyHarvestService = service;
        }

        [HttpGet]
        [Route("GetBuyerSchedules")]
        public async Task<IHttpActionResult> GetBuyerSchedules()
        {
            try
            {
                var buyerSchedules = await _dailyHarvestService.GetBuyerSchedules();
                return Ok(buyerSchedules);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller} / {nameof(DailyHarvestController.GetBuyerSchedules)}");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetGreensProcurementByDespNo/{DespNo}")]
        public async Task<IHttpActionResult> GetGreensProcurementByDespNo(int DespNo)
        {
            try
            {
                var GreensProcurementByDesp = await _dailyHarvestService.GetGreensProcurementByDespNo(DespNo);
                return Ok(GreensProcurementByDesp);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller} / {nameof(DailyHarvestController.GetGreensProcurementByDespNo)}");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetGreensFarmersDetails/{GreenProcurementNo}")]
        public async Task<IHttpActionResult> GetGreensFarmersDetails(int GreenProcurementNo)
        {
            try
            {
                var GreensFarmersDetails = await _dailyHarvestService.GetGreensFarmersDetails(GreenProcurementNo);
                return Ok(GreensFarmersDetails);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller} / {nameof(DailyHarvestController.GetGreensFarmersDetails)}");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetGreensQuantityCrateWiseDetails/{GreenProcurementNo}")]
        public async Task<IHttpActionResult> GetGreensQuantityCrateWiseDetails(int GreenProcurementNo)
        {
            try
            {
                var GreensQuantityCrateWiseDetail = await _dailyHarvestService.GetGreensQuantityCrateWiseDetails(GreenProcurementNo);
                return Ok(GreensQuantityCrateWiseDetail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller} / {nameof(DailyHarvestController.GetGreensQuantityCrateWiseDetails)}");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetGreensQuantityCountWiseDetails/{GreenProcurementNo}")]
        public async Task<IHttpActionResult> GetGreensQuantityCountWiseDetails(int GreenProcurementNo)
        {
            try
            {
                var GreensQuantityCountWiseDetail = await _dailyHarvestService.GetGreensQuantityCountWiseDetails(GreenProcurementNo);
                return Ok(GreensQuantityCountWiseDetail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller} / {nameof(DailyHarvestController.GetGreensQuantityCountWiseDetails)}");
                return InternalServerError();
            }
        }

        [CheckModelForNull]
        [ValidateModelState]
        [HttpPost, Route("AddGreensProcurement")]
        public async Task<IHttpActionResult> AddGreensProcurement(GreensProcurement greensProcurement)
        {
            try
            {
                var grnProcurement = await _dailyHarvestService.AddGreensProcurement(greensProcurement);
                return Ok(grnProcurement);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller} / {nameof(DailyHarvestController.AddGreensProcurement)}");
                return InternalServerError();
            }
        }

        /*[CheckModelForNull]
        [ValidateModelState]
        [HttpPost, Route("AddGreensFarmersDetail")]
        public async Task<IHttpActionResult> AddGreensFarmersDetail(GreensFarmersDetail greensFarmersDetail)
        {
            try
            {
                var greensFarmer = await _dailyHarvestService.AddGreensFarmersDetail(greensFarmersDetail);
                return Ok(greensFarmer);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller} / {nameof(DailyHarvestController.AddGreensFarmersDetail)}");
                return InternalServerError();
            }
        }*/

        [CheckModelForNull]
        [ValidateModelState]
        [HttpPost, Route("AddGreensQuantityCratewiseDetail")]
        public async Task<IHttpActionResult> AddGreensQuantityCratewiseDetail(GreenFarmerQuanityCrateWiseDTO greenFarmerQuanityCrateWiseDTO)
        {
            try
            {
                var grnCrateWiseDetail = await _dailyHarvestService.AddGreensQuantityCratewiseDetail(greenFarmerQuanityCrateWiseDTO);
                return Ok(grnCrateWiseDetail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller} / {nameof(DailyHarvestController.AddGreensQuantityCratewiseDetail)}");
                return InternalServerError();
            }
        }

        /*[CheckModelForNull]
        [ValidateModelState]
        [HttpPost, Route("AddGreensQuantityCountwiseDetail")]
        public async Task<IHttpActionResult> AddGreensQuantityCountwiseDetail(List<GreensQuantityCountwiseDetail> countwiseDetails)
        {
            try
            {
                var grnCountWiseDetail = await _dailyHarvestService.AddGreensQuantityCountwiseDetail(countwiseDetails);
                return Ok(grnCountWiseDetail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller} / {nameof(DailyHarvestController.AddGreensQuantityCountwiseDetail)}");
                return InternalServerError();
            }
        }
        [CheckModelForNull]
        [ValidateModelState]
        [HttpPost, Route("AddGreensFarmersDetailList")]
        public async Task<IHttpActionResult> AddGreensFarmersDetail(List<GreensFarmersDetail> greensFarmersDetail)
        {
            try
            {
                var greensFarmer = await _dailyHarvestService.AddGreensFarmersDetail(greensFarmersDetail);
                return Ok(greensFarmer);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller} / {nameof(DailyHarvestController.AddGreensFarmersDetail)}");
                return InternalServerError();
            }
        }

        [CheckModelForNull]
        [ValidateModelState]
        [HttpPost, Route("AddGreensQuantityCratewiseDetailList")]
        public async Task<IHttpActionResult> AddGreensQuantityCratewiseDetail(List<GreensQuantityCratewiseDetail> cratewiseDetail)
        {
            try
            {
                var grnCrateWiseDetail = await _dailyHarvestService.AddGreensQuantityCratewiseDetail(cratewiseDetail);
                return Ok(grnCrateWiseDetail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller} / {nameof(DailyHarvestController.AddGreensQuantityCratewiseDetail)}");
                return InternalServerError();
            }
        }*/

        [HttpGet]
        [Route("GetCompletedDailyGreensRecieving/{harvestDate}")]
        public async Task<IHttpActionResult> GetCompletedDailyGreensRecieving(DateTime harvestDate)
        {
            try
            {
                var buyerSchedules = await _dailyHarvestService.GetCompletedDailyGreensRecieving(harvestDate);
                return Ok(buyerSchedules);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller} / {nameof(DailyHarvestController.GetBuyerSchedules)}");
                return InternalServerError();
            }
        }
        [HttpGet]
        [Route("GetDailyGreensQuantityCrateWiseDetails/{GreenProcurementNo}")]
        public async Task<IHttpActionResult> GetDailyGreensQuantityCrateWiseDetails(int GreenProcurementNo)
        {
            try
            {
                var GreensQuantityCrateWiseDetail = await _dailyHarvestService.GetDailyGreensQuantityCrateWiseDetails(GreenProcurementNo);
                return Ok(GreensQuantityCrateWiseDetail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller} / {nameof(DailyHarvestController.GetDailyGreensQuantityCrateWiseDetails)}");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetBuyerSchedulesWithProcurementDetails")]
        public async Task<IHttpActionResult> GetBuyerSchedulesWithProcurementDetails()
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            try
            {
                var res = await _dailyHarvestService.GetBuyerSchedulesWithProcurementDetails();
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

        [CheckModelForNull]
        [ValidateModelState]
        [HttpPost, Route("BulkDailyGreensSync")]
        public async Task<IHttpActionResult> BulkDailyGreensSync(List<BulkGreensInsertDTO> bulkGreensInsertDTO)
        {
            try
            {
                var grnProcurement = await _dailyHarvestService.BulkGreensInsert(bulkGreensInsertDTO);
                return Ok(grnProcurement);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller} / {nameof(DailyHarvestController.BulkDailyGreensSync)}");
                return InternalServerError();
            }
        }
    }
}
