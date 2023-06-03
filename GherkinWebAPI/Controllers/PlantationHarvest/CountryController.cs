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
    [RoutePrefix("v1/Country")]
    public class CountryController : ApiController
    {
        private readonly ICountryService _service;
        private readonly ILogger<CountryController> _logger;
        public readonly string controller = nameof(CountryController);

        public CountryController(ICountryService service)
        {
            this._service = service;
        }

        [HttpGet, Route("GetAllCountries")]
        public async Task<IHttpActionResult> GetAllCountries()
        {
            try
            {
                var countries = await _service.GetAllCountriesAsync();
                if (countries.Count > 0)
                {
                    return Ok(countries);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in CountryController/{nameof(CountryController.GetAllCountries)}");
                return InternalServerError();
            }

        }

        [HttpGet, Route("GetCountryByCode")]
        public async Task<IHttpActionResult> GetCountryById(int countryCode)
        {
            try
            {
                var countryDetail = await _service.GetCountryByIdAsync(countryCode);
                if (countryDetail != null)
                {
                    return Ok(countryDetail);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in CountryController/{nameof(CountryController.GetCountryById)}");
                return InternalServerError();
            }
        }

        [HttpPost, Route("AddCountry")]
        public async Task<IHttpActionResult> AddCountry(string countryName)
        {
            try
            {
                var countryDetail = await _service.AddCountry(countryName);
                if (countryDetail.Country_Code > 0)
                {
                    return Ok(countryDetail);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in CountryController/{nameof(CountryController.AddCountry)}");
                return InternalServerError();
            }
           
        }
    }
}
