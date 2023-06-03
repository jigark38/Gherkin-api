using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using GherkinWebAPI.Core.Harvest_Area_Village;
using GherkinWebAPI.Models.Harvest_Area;
using GherkinWebAPI.ValidateModel;
using Microsoft.Extensions.Logging;
using VillageDetail = GherkinWebAPI.Models.Villages.VillageDetail;
using GherkinWebAPI.Core.Villages;
using System.Text;

namespace GherkinWebAPI.Controllers.PlantationHarvest
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("v1/harvestAreaVillage")]
    public class HarvestAreaVillageController : ApiController
    {
        private readonly IHarvestAreaVillageService _service;
        private readonly ILogger<HarvestAreaVillageDetail> _logger;
        private readonly IVillageService _villageService;

        public HarvestAreaVillageController(IHarvestAreaVillageService service, IVillageService farmerService)
        {
            _service = service;
            _villageService = farmerService;
        }

        [HttpPost]
        [Route("addAreaVillage")]
        [ValidateModelState]
        public async Task<IHttpActionResult> AddHarvestAreaVillage([FromBody] List<HarvestAreaVillageDetail> harvestAreaVillages)
        {
            try
            {
                StringBuilder errors = new StringBuilder();
                var result = new List<HarvestAreaVillageDetail>();

                foreach (var v in harvestAreaVillages)
                {
                    var isVillageExist = await _villageService.IsVillageExist(v.CountryCode, v.StateCode, v.DistrictCode, v.MandalCode, v.VillageName);

                    VillageDetail village = null;

                    if (!isVillageExist)
                    {
                        village = new VillageDetail
                        {
                            VillageCode = v.VillageCode,
                            VillageName = v.VillageName,
                            CountryCode = v.CountryCode,
                            StateCode = v.StateCode,
                            DistrictCode = v.DistrictCode,
                            MandalCode = v.MandalCode,
                        };

                        var addedVillage = await _villageService.AddVillage(village);
                        v.VillageCode = village.VillageCode;
                    }

                    var isVillageAreaVillageExist = await _service.IsAreaVillageAllowed(v.AreaId, v.CountryCode, v.StateCode, v.DistrictCode, v.MandalCode, village == null ? v.VillageCode : village.VillageCode);
                    if (!isVillageAreaVillageExist)
                    {
                        var addedHarvestVillage = await _service.AddAreaVillage(v);
                        result.Add(addedHarvestVillage);
                    }
                    else
                        errors.Append($"Village {v.VillageName} already exist in this Mandal.");

                }

                if (errors.Length > 0)
                {
                    return BadRequest(errors.ToString());
                }

                if (result.Count > 0)
                {
                    return Ok(result);
                }
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside AddHarvestAreaVillage action: {ex.Message}");
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("updateHarvestAreaVillage")]
        [ValidateModelState]
        public async Task<IHttpActionResult> UpdateHarvestAreaVillage([FromBody] HarvestAreaVillageDetail harvestAreaVillage)
        {
            StringBuilder errors = new StringBuilder();
            var result = new HarvestAreaVillageDetail();

            try
            {
                var isVillageExist = await _villageService.IsVillageExist(harvestAreaVillage.CountryCode, harvestAreaVillage.StateCode, harvestAreaVillage.DistrictCode, harvestAreaVillage.MandalCode, harvestAreaVillage.VillageName);

                if (!isVillageExist)
                {
                    var village = new VillageDetail
                    {
                        VillageCode = harvestAreaVillage.VillageCode,
                        VillageName = harvestAreaVillage.VillageName,
                        CountryCode = harvestAreaVillage.CountryCode,
                        StateCode = harvestAreaVillage.StateCode,
                        DistrictCode = harvestAreaVillage.DistrictCode,
                        MandalCode = harvestAreaVillage.MandalCode
                    };

                    var updatedVillage = await _villageService.UpdateVillage(village);
                }

                var isAreaVillageExist = await _service.IsAreaVillageAllowed(harvestAreaVillage.AreaId, harvestAreaVillage.CountryCode, harvestAreaVillage.StateCode, harvestAreaVillage.DistrictCode, harvestAreaVillage.MandalCode, harvestAreaVillage.VillageCode);
                if (!isAreaVillageExist)
                {
                    result = await _service.UpdateHarvestAreaVillage(harvestAreaVillage);
                }
                else if (isVillageExist && isAreaVillageExist)
                {
                    errors.Append($"Colud not update harvestAreaVillage : {harvestAreaVillage.VillageName} due to duplicate record");
                }

                if (errors.Length > 0)
                {
                    return BadRequest(errors.ToString());
                }
                else
                    return Ok(result.AreaId == null ? harvestAreaVillage : result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateHavestAreaVillage action: {ex.Message}");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("IsVillageCodeAllowed")]
        public async Task<IHttpActionResult> IsAreaNameAllowed(int villageCode)
        {
            try
            {
                var result = await _service.IsVillageCodeAllowed(villageCode);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside IsVillageCodeAllowed action: {ex.Message}");
                return InternalServerError();
            }

        }

        [HttpGet]
        [Route("GetAvailableVillages")]
        public async Task<IHttpActionResult> GetAvailableVillages(string areaId, string areaName, int countryCode, int stateCode, int districtCode, int mandalCode)
        {
            try
            {
                var villages = await _service.GetAvailableVillagesAsync(areaId, areaName, countryCode, stateCode, districtCode, mandalCode);
                return Ok(villages);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside IsVillageCodeAllowed action: {ex.Message}");
                return InternalServerError();
            }

        }
    }
}
