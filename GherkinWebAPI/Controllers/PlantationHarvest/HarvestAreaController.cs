using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using GherkinWebAPI.Core;
using GherkinWebAPI.Core.Harvest_Area;
using GherkinWebAPI.Models;
using GherkinWebAPI.ValidateModel;
using Microsoft.Extensions.Logging;

namespace GherkinWebAPI.Controllers.PlantationHarvest
{
    [System.Web.Http.RoutePrefix("v1/harvestArea")]
    public class HarvestAreaController : ApiController
    {
        private readonly IHarvestAreaService _service;
        private readonly ILogger<HarvestAreaController> _logger;

        //public readonly string controller = nameof(PlantationHarvestController);
        public HarvestAreaController(IHarvestAreaService service, IRepositoryWrapper repository)
        {
            this._service = service;
            //this._logger = logger;
        }

        [HttpGet]
        [Route("SearchArea")]
        public async Task<IHttpActionResult> Search(string areaId)
        {
            try
            {
                var areaDetails = await _service.SearchArea(areaId);
                if (areaDetails.Count > 0)
                {
                    return Ok(areaDetails);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside AddHarvestArea action: {ex.Message}");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("areaNameAndCode")]
        public async Task<List<Area>> GetAreaNameAndCode()
        {
            return await _service.GetAreaNameAndCodeAsync();
        }

        [HttpPost]
        [Route("addArea")]
        [ValidateModelState]
        public async Task<IHttpActionResult> AddHarvestArea([System.Web.Mvc.Bind(Exclude = "ID, Area_ID")]Area area)
        {
            try
            {
                var result = await _service.AddArea(area);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside AddHarvestArea action: {ex.Message}");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("IsAreaCodeAllowed")]
        public async Task<IHttpActionResult> IsAreaCodeAllowed(int areaCode)
        {
            try
            {
                var result = await _service.IsAreaCodeAllowed(areaCode);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside IsAreaCodeAllowed action: {ex.Message}");
                return InternalServerError();
            }

        }

        [HttpGet]
        [Route("IsAreaNameAllowed")]
        public async Task<IHttpActionResult> IsAreaNameAllowed(string areaName)
        {
            try
            {
                var result = await _service.IsAreaNameAllowed(areaName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside IsAreaNameAllowed action: {ex.Message}");
                return InternalServerError();
            }

        }

        [HttpGet]
        [Route("SearchAreaByStateCode")]
        public async Task<IHttpActionResult> SearchAreaByStateCode(int stateId)
        {
            try
            {
                var areaDetails = await _service.SearchAreaByStateCode(stateId);
                if (areaDetails.Count > 0)
                {
                    return Ok(areaDetails);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside AddHarvestArea action: {ex.Message}");
                return InternalServerError();
            }
        }
    }
}
