using GherkinWebAPI.Core;
using GherkinWebAPI.Models;
using GherkinWebAPI.ValidateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace GherkinWebAPI.Controllers
{
    [Route("api/V1/[Controller]")]
    public class ESICController : ApiController
    {
        private readonly IESICService _service;
        public ESICController(IESICService esicService)
        {
            _service = esicService;
        }
        [HttpGet]
        [Route("GetESICRates")]
        public async Task<IHttpActionResult> GetESICRates()
        {
            List<ESICRate> esicRates = new List<ESICRate>();
            try
            {
                esicRates = await _service.GetESICRates();
                return Ok(esicRates);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in ESICController / {nameof(ESICController.GetESICRates)}");
                return InternalServerError();
            }
        }

        [CheckModelForNull]
        [ValidateModelState]
        [Route("CreateESICRates")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateESICRates([FromBody] ESICRate esicRates)
        {
            try
            {
                if (esicRates == null)
                    return null;
                var esic = await _service.CreateESICRates(esicRates);
                return Ok(esic);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in ESICController / {nameof(ESICController.CreateESICRates)}");
                return InternalServerError();
            }

        }

        [CheckModelForNull]
        [ValidateModelState]
        [Route("UpdateESICRates")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateESICRates([FromBody] ESICRate esicRates)
        {
            try
            {
                var esic = await _service.UpdateESICRates(esicRates);
                return Ok(esic);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in EmployeeController / {nameof(ESICController.UpdateESICRates)}");
                return InternalServerError();
            }
        }
    }
}
