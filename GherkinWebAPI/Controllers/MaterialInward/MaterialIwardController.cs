using GherkinWebAPI.Core.MaterialInward;
using GherkinWebAPI.DTO.MaterialInward;
using GherkinWebAPI.Request.MaterialInward;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GherkinWebAPI.Controllers.MaterialInward
{
    public class MaterialIwardController : ApiController
    {
        private readonly IMaterialService _service;

        public MaterialIwardController(IMaterialService _service)
        {
            this._service = _service;
        }

        [HttpPost]
        [Route("materialinward/add")]
        public async Task<HttpResponseMessage> InsertMaterialInward(InsertMaterialInwardRequest insertMaterialInwardRequest)
        {
            MaterialInwardDto data = new MaterialInwardDto();
            try
            {
                if (insertMaterialInwardRequest == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
                data = await _service.InsertMaterialInward(insertMaterialInwardRequest);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

        }

        [HttpPost]
        [Route("materialinward/find")]
        public async Task<HttpResponseMessage> FindMaterialInward(DateTime dateFrom, DateTime dateTo, string inwardType)
        {
            List<MaterialInwardDto> data = new List<MaterialInwardDto>();
            try
            {
                if (dateFrom == null || dateTo == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
                data = await _service.FindMaterialInward(dateFrom, dateTo, inwardType);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

        }

        [HttpPost]
        [Route("materialinward/update/{id}")]
        public async Task<HttpResponseMessage> UpdateMaterialInward(int id, InsertMaterialInwardRequest materialInwardDto)
        {
            try
            {
                if (materialInwardDto == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
                var data = await _service.UpdateMaterialInward(id, materialInwardDto);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

        }

        [HttpGet]
        [Route("materialinward/getAll")]
        public async Task<IHttpActionResult> GetMaterialInwardDetails()
        {
            try
            {
                var inwardDetails = await _service.GetMaterialInwardDetailsAsync();
                if (inwardDetails.Count > 0)
                {
                    return Ok(inwardDetails);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in MaterialInwardController/{nameof(MaterialIwardController.GetMaterialInwardDetails)}");
                return InternalServerError();
            }

        }
    }
}
