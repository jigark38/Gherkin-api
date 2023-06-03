using GherkinWebAPI.Core;
using GherkinWebAPI.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GherkinWebAPI.Controllers.PlantationHarvest
{
    [RoutePrefix("v1/state")]
    public class StateController : ApiController
    {
        private readonly IStateService _service;
        private readonly ILogger<StateController> _logger;

        public StateController(IStateService service)
        {
            this._service = service;
        }

        [HttpGet, Route("GetAllStatesByCountyCode")]
        public async Task<List<State>> GetAllStatesByCountry(int countryCode)
        {
            return await _service.GetAllStatesByCountryAsync(countryCode);
        }

        [HttpGet, Route("GetStateByCode/{stateCode}")]
        public async Task<State> GetStateById(int stateCode)
        {
            return await _service.GetStateByIdAsync(stateCode);
        }

        [HttpPost, Route("AddState")]
        public async Task<IHttpActionResult> AddState(State state)
        {
            try
            {
                var stateDetail = await _service.AddState(state);
                if (stateDetail != null)
                {
                    return Ok(stateDetail);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in StateController/{nameof(StateController.AddState)}");
                return InternalServerError();
            }

        }
    }
}