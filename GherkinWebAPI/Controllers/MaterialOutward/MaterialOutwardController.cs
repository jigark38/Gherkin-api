using GherkinWebAPI.Core.MaterialInward;
using GherkinWebAPI.DTO.MaterialInward;
using GherkinWebAPI.Models.MaterialOutward;
using GherkinWebAPI.Request.MaterialInward;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GherkinWebAPI.Controllers.MaterialOutward
{
    [Route("api/V1/[Controller]")]
    public class MaterialOutwardController : ApiController
    {
        private readonly IMaterialService _service;

        public MaterialOutwardController(IMaterialService _service)
        {
            this._service = _service;
        }

        [HttpPost]
        [Route("UpMaterialOutwardDetails")]
        public async Task<IHttpActionResult> UpMaterialOutwardDetails([FromBody]MaterialOutwardDetails materialOutwardDetails)
        {
            try
            {
                var result = await _service.UpMaterialOutwardDetails(materialOutwardDetails);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("GetAllMaterialOutwardDetails")]
        public async Task<IHttpActionResult> GetAllMaterialOutwardDetails()
        {
            try
            {
                var result = await _service.GetAllMaterialOutwardDetails();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //[HttpGet]
        //[Route("GetMaterialOutwardDetails")]
        //public async Task<IHttpActionResult> GetAllMaterialOutwardDetails()
        //{
        //    try
        //    {
        //        var result = await _service.GetAllMaterialOutwardDetails();
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}
    }
}
