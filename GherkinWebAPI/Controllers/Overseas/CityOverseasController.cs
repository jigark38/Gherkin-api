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
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;

namespace GherkinWebAPI.Controllers.Overseas
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CityOverseasController : ApiController
    {
        private readonly ICityOverseasService _service;
        private readonly ILogger<CityOverseasController> _logger;
        private readonly string controller = nameof(CityOverseasController);



        public CityOverseasController(ICityOverseasService service)
        {
            this._service = service;
        }

        [HttpGet, Route("GetAllCityOverseasByState")]
        public async Task<IHttpActionResult> GetAllCityOverseasByState(string stateCode)
        {
            try
            {

                var details = await _service.GetAllCityOverseasByStateIdAsync(stateCode);
                return Ok(details);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(CityOverseasController.GetAllCityOverseasByState)}");
                return InternalServerError();
            }
        }

        [HttpGet, Route("GetCityOverseasById")]
        public async Task<IHttpActionResult> GetCityOverseasById(string cityCode)
        {
            try
            {
                var cities = await _service.GetcityOverseasByIdAsync(cityCode);
                return Ok(cities);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(CityOverseasController.GetCityOverseasById)}");
                return InternalServerError();
            }
        }


        [HttpPost, Route("GetCityOverseasByname")]
        public async Task<IHttpActionResult> GetCityOverseasBynameAsync(CityOverseas cityOverseas)
        {
            try
            {

                var obj = await _service.AddCityIfntAsync(cityOverseas);
                return Ok(obj);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(CityOverseasController.GetCityOverseasBynameAsync)}");
                return InternalServerError();
            }
        }

    }
}
