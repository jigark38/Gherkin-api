using AutoMapper;
using GherkinWebAPI.Core;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace GherkinWebAPI.Controllers
{
    /// <summary>
    /// Defines the <see cref="PlantationHarvestController" />
    /// </summary>
    [Route("api/[controller]")]
    public class PlantationHarvestController : ApiController
    {
        /// <summary>
        /// Defines the _service
        /// </summary>
        private readonly IPlantationHarvestService _service;

        // private readonly ILogger<AccountManagementController> _logger;
        /// <summary>
        /// Defines the _repository
        /// </summary>
        private readonly IRepositoryWrapper _repository;

        /// <summary>
        /// Defines the controller
        /// </summary>
        public readonly string controller = nameof(PlantationHarvestController);

        /// <summary>
        /// Initializes a new instance of the <see cref="PlantationHarvestController"/> class.
        /// </summary>
        /// <param name="service">The service<see cref="IPlantationHarvestService"/></param>
        /// <param name="repository">The repository<see cref="IRepositoryWrapper"/></param>
        public PlantationHarvestController(IPlantationHarvestService service, IRepositoryWrapper repository)
        {
            this._service = service;
            //   _logger = logger;
            this._repository = repository;
        }

        [Route("GetPlantationSchedules"), HttpGet]
        public async Task<IHttpActionResult> GetPlantationSchedules()
        {
            try
            {
                var result = await _service.GetPlantationSchedules(string.Empty, string.Empty);
                if(result == null)
                {
                    return NotFound();
                }

                return Ok(Mapper.Map<List<PlantationScheduleDto>>(result));
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(PlantationHarvestController.GetPlantationSchedules)}");
                return InternalServerError(ex);
            }
        }


        /// <summary>
        /// The SchedulePlantation
        /// </summary>
        /// <param name="plantationScheduleDto">The plantationScheduleDto<see cref="PlantationScheduleDto"/></param>
        /// <returns>The <see cref="Task{IHttpActionResult}"/></returns>
        [HttpPost]
        [Route("SchedulePlantation")]
        public async Task<IHttpActionResult> SchedulePlantation([FromBody] PlantationScheduleDto plantationScheduleDto)
        {
            try
            {
                var result = await _service.SchedulePlanation(Mapper.Map<PlantationSchedule>(plantationScheduleDto));
                return Ok(Mapper.Map<PlantationScheduleDto>(result));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(PlantationHarvestController.SchedulePlantation)}");
                return InternalServerError(ex);
            }
        }

        [Route("UpdatePlantationSchedule"),HttpPost]
        public async Task<IHttpActionResult> UpdatePlantationSchedule([FromBody] PlantationScheduleDto plantationScheduleDto)
        {
            try
            {
                var result = await _service.UpdatePlantationSchedule(Mapper.Map<PlantationSchedule>(plantationScheduleDto));
                return Ok(Mapper.Map<PlantationScheduleDto>(result));
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(PlantationHarvestController.UpdatePlantationSchedule)}");
                return InternalServerError(ex);
            }
        }

        [Route("SearchPlantationSchedule"), HttpGet]
        public async Task<IHttpActionResult> SearchPlantationSchedule(string cropGroup, string cropName)
        {
            try
            {
                var result = await _service.GetPlantationSchedules(cropGroup, cropName);
                if (result == null)
                {
                    return NotFound();
                }

                return Ok(Mapper.Map<List<PlantationScheduleDto>>(result));
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(PlantationHarvestController.GetPlantationSchedules)}");
                return InternalServerError(ex);
            }
        }
    }
}