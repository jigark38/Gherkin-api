using GherkinWebAPI.Core.Currencys;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.ConsigneeBuyersModel;
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
    public class CurrencyController : ApiController
    {

        private readonly ICurrencyService _service;
        private readonly ILogger<CurrencyController> _logger;
        private readonly string controller = nameof(CurrencyController);

        public CurrencyController(ICurrencyService service)
        {
            this._service = service;
        }

        [HttpGet, Route("GetAllCurrency")]
        public async Task<IHttpActionResult> GetAllCurrencyAsync()
        {
            try
            {
                var details = await _service.GetAllCurrencyAsync();
                return Ok(details);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(CurrencyController.GetAllCurrencyAsync)}");
                return InternalServerError();
            }

        }

        [HttpGet, Route("GetCurrencyById")]
        public async Task<IHttpActionResult> GetCurrencyByIdAsync(string currencyCode)
        {
            try
            {
                var details = await _service.GetCurrencyByIdAsync(currencyCode);
                return Ok(details);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(CurrencyController.GetCurrencyByIdAsync)}");
                return InternalServerError();
            }
        }

        [HttpPost, Route("AddCurrencyByName")]
        public async Task<IHttpActionResult> AddCurrencyIfntAsync(Currency currency)
        {
            try
            {
                Currency obj = await _service.AddCurrencyIfntAsync(currency);
                return Ok(obj);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(CurrencyController.AddCurrencyIfntAsync)}");
                return InternalServerError();
            }

        }


    }
}
