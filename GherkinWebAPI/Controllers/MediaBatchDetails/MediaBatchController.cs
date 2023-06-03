using GherkinWebAPI.Core.MediaBatchDetails;
using GherkinWebAPI.Models.MediaBatchDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GherkinWebAPI.Controllers.MediaBatchDetails
{
    
    [Route("api/V1/[Controller]")]
    public class MediaBatchController : ApiController
    {
        private readonly IMediaBatchService _service;
        private readonly string controller = nameof(MediaBatchController);
        public MediaBatchController(IMediaBatchService mediaBatchService)
        {
            _service = mediaBatchService;
        }
        [HttpGet]
        [Route("GetAllEmployeeIdAndName")]
        public async Task<IHttpActionResult> GetAllEmployeeIdAndName()
        {
            try
            {
                var res = await _service.GetAllEmployeeIdAndName();
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(MediaBatchController.GetAllEmployeeIdAndName)}");
                return InternalServerError();
            }
        }
        [HttpGet]
        [Route("GetStockAndBatchDetailsFirst")]
        public async Task<IHttpActionResult> GetStockAndBatchDetailsFirst(DateTime date)
        {
            try
            {
                var res = await _service.GetStockAndBatchDetailsFirst(date);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(MediaBatchController.GetStockAndBatchDetailsFirst)}");
                return InternalServerError();
            }
        }
        [HttpPost]
        [Route("GetMediaMaterialDetails")]
        public async Task<IHttpActionResult> GetMediaMaterialDetails([FromBody] GetMediaMaterialDetailsParams obj)
        {
            try
            {
                var res = await _service.GetMediaMaterialDetails(obj.Date,obj.MediaProcessCode,obj.TotalQty);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(MediaBatchController.GetMediaMaterialDetails)}");
                return InternalServerError();
            }
        }
        [HttpPost]
        [Route("SaveMediaBatchMaterialDetails")]
        public async Task<IHttpActionResult> SaveMediaBatchMaterialDetails([FromBody] MediaBatchProductionAndMaterialDetails obj)
        {
            try
            {
                var res = await _service.SaveMediaBatchMaterialDetails(obj);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(MediaBatchController.SaveMediaBatchMaterialDetails)}");
                return InternalServerError();
            }
        }
    }
}
