using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using GherkinWebAPI.Core.GRNAndMaterialClassification;
using GherkinWebAPI.DTO.GRNAndMaterialClassification;
using GherkinWebAPI.Models.GRNAndMaterialClassification;
using GherkinWebAPI.Utilities;
using GherkinWebAPI.ValidateModel;

namespace GherkinWebAPI.Controllers.GRNAndMaterialClassification
{
    [Route("api/v1/[controller]")]
    public class HarvestGRNController : ApiController
    {
        private readonly IHarvestGRNService _service;
        private readonly IHarvestGRNFarmerService _farmerService;
        private readonly IHarvestGRNCrateService _crateService;
        public readonly string controller = nameof(HarvestGRNController);

        public HarvestGRNController(IHarvestGRNService service, IHarvestGRNFarmerService farmerService, IHarvestGRNCrateService crateService)
        {
            _service = service;
            _farmerService = farmerService;
            _crateService = crateService;
        }

        [HttpGet]
        [Route("GetAllGreensReceivedDetail/{areaId}")]
        public async Task<IHttpActionResult> GetGreensReceivedDetailAsync(string areaId)
        {
            try
            {
                var result = await _service.GetGreensReceivedDetailsAsync(areaId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(HarvestGRNController.GetGreensReceivedDetailAsync)}");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetHarvestGRNNo")]
        public async Task<IHttpActionResult> GetHarvestGRNNo()
        {
            try
            {
                var result = await _service.GetHarvestGRNNo();
                if (result > 0)
                {
                    return Ok(result);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(HarvestGRNController.GetHarvestGRNNo)}");
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("CreateHarvestGRNMaterialDetail")]
        [ValidateModelState]
        public async Task<IHttpActionResult> CreateHarvestGRNMaterialDetail([FromBody] HarvestGRNMaterialDetail materialDetail)
        {
            HarvestGRNMaterialDetail harvestGRNMaterialDetail = null;

            try
            {
                long NextHarvestGRNNo = await _service.GetNextHarvestGRNNo();
                materialDetail.HarvestGRNNo = NextHarvestGRNNo;
                harvestGRNMaterialDetail = await _service.CreateHarvestGRNDetail(materialDetail);
                harvestGRNMaterialDetail = await _farmerService.CreateHarvestGRNFarmer(materialDetail);
                harvestGRNMaterialDetail = await _crateService.CreateHarvestGRNCrate(materialDetail);

                if (harvestGRNMaterialDetail != null)
                {
                    return Ok(harvestGRNMaterialDetail);
                }
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(HarvestGRNController.CreateHarvestGRNMaterialDetail)}");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetAllGreensReceivedDetailNew/{areaId}/{supervisorId}")]
        public async Task<IHttpActionResult> GetGreensReceivedDetailsNew(string areaId, string supervisorId)
        {
            try
            {
                var result = await _service.GetGreensReceivedDetailsAsync(areaId, supervisorId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(HarvestGRNController.GetGreensReceivedDetailsNew)}");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("CompleteBuyer/{HarvestGRNNo}/{GreensProcurementNo}/{BuyerEmployerId}")]
        public async Task<IHttpActionResult> CompleteBuyer(long HarvestGRNNo, int GreensProcurementNo, string BuyerEmployerId)
        {
            try
            {
                var result = await _service.CompleteBuyer(HarvestGRNNo, GreensProcurementNo, BuyerEmployerId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(HarvestGRNController.CompleteBuyer)}");
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("AddBuyerQuantityCratewiseDetail")]
        [ValidateModelState]
        public async Task<IHttpActionResult> AddBuyerQuantityCratewiseDetail(HarvestBuyerWeighingDetailsDTO harvestBuyerWeighingDetailsDTO)
        {
            try
            {
                var result = await _service.AddBuyerQuantityCratewiseDetail(harvestBuyerWeighingDetailsDTO);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(HarvestGRNController.AddBuyerQuantityCratewiseDetail)}");
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("CompleteHarvestGrn")]
        [ValidateModelState]
        public async Task<IHttpActionResult> CompleteHarvestGrn(HarvestGRN harvestGRN)
        {
            try
            {
                var result = await _service.CompleteHarvestGrn(harvestGRN);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(HarvestGRNController.CompleteHarvestGrn)}");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetVehicles")]
        public async Task<IHttpActionResult> GetVehicles()
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            List<VehicleDetails> vehicles = new List<VehicleDetails>();
            try
            {
                vehicles = await _service.GetVehicles();
                apiResponse.IsSucceed = true;
                apiResponse.Data = vehicles;
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
        [Route("GetAllGreensReceivedDetailsList")]
        public async Task<IHttpActionResult> GetAllGreensReceivedDetailsList()
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            try
            {
                var result = await _service.GetAllGreensReceivedDetailsList();
                if (result.Count > 0)
                {
                    apiResponse.IsSucceed = true;
                    apiResponse.Data = result;
                    return Ok(apiResponse);
                }
                else
                {
                    apiResponse.IsSucceed = false;
                    return Ok(apiResponse);
                }

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
