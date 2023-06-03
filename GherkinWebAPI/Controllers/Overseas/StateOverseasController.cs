using GherkinWebAPI.Core;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;
using GherkinWebAPI.Persistence;
using GherkinWebAPI.ValidateModel;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;

namespace GherkinWebAPI.Controllers.Overseas
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class StateOverseasController : ApiController
    {
        private readonly IStateOverseasService _service;
        private readonly ILogger<StateOverseasController> _logger;
        private readonly string controller = nameof(StateOverseasController);
        public StateOverseasController(IStateOverseasService service)
        {
            this._service = service;
        }

        [HttpGet, Route("GetAllStatesOverseasByCountry")]
        public async Task<IHttpActionResult> GetAllStatesOverseasByCountry(string countryCode)
        {
            try
            {
                var details = await _service.GetAllStatesOverseasByCountryIdAsync(countryCode);

                return Ok(details);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(StateOverseasController.GetAllStatesOverseasByCountry)}");
                return InternalServerError();
            }

        }

        [HttpGet, Route("GetStateOverseasById")]
        public async Task<IHttpActionResult> GetStateOverseasById(string stateCode)
        {

            try
            {
                var details = await _service.GetStateOverseasByIdAsync(stateCode);
                return Ok(details);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(StateOverseasController.GetStateOverseasById)}");
                return InternalServerError();
            }
        }


        [HttpPost, Route("AddStateByName")]
        public async Task<IHttpActionResult> AddStateIfntAsync(StateOverseas state)
        {
            try
            {

                StateOverseas obj = await _service.AddStateIfntAsync(state);
                return Ok(obj);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(StateOverseasController.GetStateOverseasById)}");
                return InternalServerError();
            }
        }
    }
}
