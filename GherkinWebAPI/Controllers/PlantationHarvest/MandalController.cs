using GherkinWebAPI.Core;
using GherkinWebAPI.Core.Mandals;
using GherkinWebAPI.Models.Mandals;
using GherkinWebAPI.ValidateModel;
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
    [Route("api/v1/[Contoller]")]
    public class MandalController : ApiController
    {
        private readonly IMandalService _service;
        public readonly string controller = nameof(MandalController);

        public MandalController(IMandalService service)
        {
            this._service = service;
        }

        [HttpGet]
        [Route("GetAllMandals")]
        public async Task<IHttpActionResult> GetAllMandalsAsync()
        {
            try
            {
                var mandals = await _service.GetAllMandalsAysnc();
                if (mandals.Count > 0)
                {
                    return Ok(mandals);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in MandalController/{nameof(MandalController.GetAllMandalsAsync)}");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetMandalByCode")]
        public async Task<IHttpActionResult> GetMandalByDistrictCodeAsync(int districtCode)
        {
            try
            {
                var mandal = await _service.GetMandalByDistrictCodeAsync(districtCode);
                if (mandal != null)
                {
                    return Ok(mandal);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in MandalController/{nameof(MandalController.GetMandalByDistrictCodeAsync)}");
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("AddMandal")]
        [ValidateModelState]
        public async Task<IHttpActionResult> Addmandal([FromBody] MandalDetail mandalDetail)
        {
            try
            {
                var result = await _service.AddMandal(mandalDetail);
                if (mandalDetail != null)
                {
                    return Ok(result);
                }
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in MandalController/{nameof(MandalController.Addmandal)}");
                return InternalServerError();
            }
        }
    }
}