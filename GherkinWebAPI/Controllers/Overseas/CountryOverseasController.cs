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
    public class CountryOverseasController : ApiController
    {
        private readonly ICountryOverseasService _service;
        private readonly ILogger<CountryOverseasController> _logger;
        private readonly string controller = nameof(CityOverseasController);

        public CountryOverseasController(ICountryOverseasService service)
        {
            this._service = service;
        }

        [HttpGet, Route("GetAllCountriesOverseas")]
        public async Task<IHttpActionResult> GetAllCountriesOverseasAsync()
        {
            try
            {
                var details = await _service.GetAllCountriesOverseasAsync();

                return Ok(details);


            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(CountryOverseasController.GetAllCountriesOverseasAsync)}");
                return InternalServerError();
            }
        }

        [HttpGet, Route("GetCountryOverseasById")]
        public async Task<IHttpActionResult> GetCountryOverseasByIdAsync(string countryCode)
        {
            var details = await _service.GetCountryOverseasByIdAsync(countryCode);

            return Ok(details);

        }

        [HttpPost, Route("AddCountryByName")]
        public async Task<IHttpActionResult> AddCountryIfntAsync(CountryOverseas countryName)
        {
            try
            {

                CountryOverseas obj = await _service.AddCountryIfntAsync(countryName);

                return Ok(obj);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(CountryOverseasController.AddCountryIfntAsync)}");
                return InternalServerError();
            }
        }

    }

}
