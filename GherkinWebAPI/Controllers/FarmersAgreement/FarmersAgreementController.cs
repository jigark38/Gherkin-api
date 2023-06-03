using GherkinWebAPI.Core;
using GherkinWebAPI.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GherkinWebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Route("api/v1/[Contoller]")]
    public class FarmersAgreementController : ApiController
    {
        private readonly IFarmersAgreementService _service;
        private readonly IFarmersAgreementSizeService _sizeService;
        private readonly ILogger<HarvestAreaVillage> _logger;

        public readonly string controller = nameof(FarmersAgreementController);

        public FarmersAgreementController(IFarmersAgreementService service, IFarmersAgreementSizeService sService)
        {
            _service = service;
            _sizeService = sService;
        }

        [HttpGet]
        [Route("GetAgreementCode")]
        public async Task<IHttpActionResult> GetFarmersAgreementCode()
        {
            try
            {
                var farmersAgreementCode = await _service.GetFarmersAgreementCodeAsync();
                if (farmersAgreementCode != null)
                {
                    return Ok(farmersAgreementCode);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(FarmersAgreementController.GetFarmersAgreementCode)}");
                return InternalServerError();
            }

        }

        [HttpGet]
        [Route("SearchAgreement")]
        public async Task<IHttpActionResult> Search(string areaId, int cityCode, string farmersCode, string cropGroupCode, string cropNameCode, string psNumber)
        {
            try
            {
                var sizeDetail = await _service.SearchAgreement(areaId, cityCode, farmersCode, cropGroupCode, cropNameCode, psNumber);
                return sizeDetail != null ? Ok(sizeDetail) : (IHttpActionResult)NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(FarmersAgreementController.Search)}");
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("CreateAgreement")]
        public async Task<IHttpActionResult> CreateAgreement([System.Web.Mvc.Bind(Exclude = "FarmersAccountNo")]FarmersAgreement farmersAgreement)
        {
            try
            {
                if (farmersAgreement == null || !ModelState.IsValid)
                {
                    _logger.LogError("farmersAgreement object sent from client is not valid");
                    return BadRequest($"farmersAgreement object sent from client is not valid");
                }

                var farmersAgreementDetails = new FarmersAgreementDetail
                {
                    Farmers_Agreement_Code = farmersAgreement.FarmersAgreementCode,
                    Farmers_Agreement_Date = farmersAgreement.FarmersAgreementDate,
                    Area_ID = farmersAgreement.AreaID,
                    Area_Code = farmersAgreement.AreaCode,
                    Employee_ID = farmersAgreement.EmployeeID,
                    Village_Code = farmersAgreement.VillageCode,
                    Farmer_Code = farmersAgreement.FarmerCode,
                    Farmers_Account_No = farmersAgreement.FarmersAccountNo.ToString(),
                    Crop_Group_Code = farmersAgreement.CropGroupCode,
                    Crop_Name_Code = farmersAgreement.CropNameCode,
                    PS_Number = farmersAgreement.PSNumber,
                    Farmers_No_of_Acres_Area = farmersAgreement.FarmersNoOfAcersArea,
                    Agriculture_DRIP_NONDRIP = farmersAgreement.AgricultureDripNonDrip
                };

                var detail = await _service.CreateAgreement(farmersAgreementDetails);

                foreach (var fs in farmersAgreement.FarmersAgreementSizes)
                {
                    if (!string.IsNullOrEmpty(fs.CropRatesRemarks))
                    {
                        var farmerAgreementSizeDetails = new FarmersAgreementSizeDetail
                        {
                            Farmers_Agreement_Code = farmersAgreementDetails.Farmers_Agreement_Code,
                            Crop_Scheme_Code = fs.CropSchemeCode,
                            Crop_Count_mm = fs.CropCount,
                            Crop_Scheme_From = int.Parse(fs.CropSchemeFromSign.Substring(0, fs.CropSchemeFromSign.IndexOf(' '))),
                            Crop_Scheme_Sign = fs.CropSchemeFromSign.Substring(fs.CropSchemeFromSign.IndexOf(' ')),
                            Crop_Rate_As_per_Association = fs.CropRateAsPerAssociation,
                            Crop_Rate_Per_UOM = fs.CropRatePerUOM,
                            Crop_Rate_As_per_Our_Agreement = fs.CropRateAsPerOurAgreement,
                            Crop_Rates_Remarks = fs.CropRatesRemarks
                        };

                        var sizeDetail = await _sizeService.CreateAgreementSize(farmerAgreementSizeDetails);
                    }
                }


                return Ok($"FarmersAgreement with Code : {detail.Farmers_Agreement_Code} Created");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(FarmersAgreementController.CreateAgreement)}");
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("UpdateAgreement/{farmersAgreementCode}")]
        public async Task<IHttpActionResult> UpdateAgreement(string farmersAgreementCode, [FromBody] FarmersAgreement farmersAgreement)
        {
            try
            {
                if (farmersAgreement == null || !ModelState.IsValid)
                {
                    _logger.LogError("farmersAgreement object sent from client is not valid");
                    return BadRequest($"farmersAgreement object sent from client is not valid");
                }


                return Ok(await _service.UpdateAgreement(farmersAgreementCode, farmersAgreement));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(FarmersAgreementController.UpdateAgreement)}");
                return InternalServerError();
            }
        }

        [HttpDelete]
        [Route("DeleteAgreement")]
        public async Task<IHttpActionResult> DeleteAgreement(string farmersAgreementCode, string cropSchemeCode)
        {
            try
            {
                if (farmersAgreementCode == null || cropSchemeCode == null)
                {
                    return BadRequest();
                }

                await _sizeService.DeleteAgreementSize(farmersAgreementCode, cropSchemeCode);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(FarmersAgreementController.DeleteAgreement)}");
                return InternalServerError();
            }

        }
        [HttpGet]
        [Route("GetAreaIncharge")]
        public async Task<IHttpActionResult> GetAreaIncharge(string AreaId, DateTime date)
        {
            try
            {
                var farmersAreaIncharge = await _service.GetAreaInchargeDetailsByAreaId(AreaId, date);
                if (farmersAreaIncharge != null)
                {
                    return Ok(farmersAreaIncharge);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(FarmersAgreementController.GetAreaIncharge)}");
                return InternalServerError();
            }

        }

        [HttpGet]
        [Route("IsValidateFarmerAccount")]
        public async Task<IHttpActionResult> IsValidateFarmerAccount(string FarmersAccountNo, string FarmerCode, string PSNumber)
        {
            try
            {
                var isVlalid = await _service.ValidateFarmerAccount(FarmersAccountNo, FarmerCode, PSNumber);

                return Ok(isVlalid);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(FarmersAgreementController.IsValidateFarmerAccount)}");
                return InternalServerError();
            }

        }

        [HttpGet]
        [Route("FarmerAccountList")]
        public async Task<IHttpActionResult> FarmerAccountList(string CropGroupCode, string CropNameCode, string PSNumber)
        {
            try
            {
                var farmerAccountList = await _service.FarmerAccountList(CropGroupCode, CropNameCode, PSNumber);

                return Ok(farmerAccountList);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(FarmersAgreementController.FarmerAccountList)}");
                return InternalServerError();
            }

        }

        [HttpGet]
        [Route("FarmerDetailsByAccount")]
        public async Task<IHttpActionResult> FarmerDetailsByAccount(string FarmersAccountNo, string PSNumber)
        {
            try
            {
                var farmerList = await _service.FarmerDetailsByAccount(FarmersAccountNo, PSNumber);

                return Ok(farmerList);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(FarmersAgreementController.FarmerAccountList)}");
                return InternalServerError();
            }

        }

    }
}
