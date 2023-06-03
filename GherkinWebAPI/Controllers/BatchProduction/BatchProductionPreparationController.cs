using GherkinWebAPI.Core;
using GherkinWebAPI.Core.BatchProduction;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Models.BatchProduction;
using GherkinWebAPI.Models.GRNAndMaterialClassification;
using GherkinWebAPI.Persistence;
using GherkinWebAPI.Utilities;
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

namespace GherkinWebAPI.Controllers.BatchProduction
{
    [Route("api/V1/[Controller]")]
    public class BatchProductionPreparationController : ApiController
    {
        private readonly IBatchProductionPreparationService _service;
        private readonly string controller = nameof(BatchProductionPreparationController);
        private readonly RepositoryContext _repositoryContext;
        private readonly IRepositoryWrapper _repository;
        private readonly ILogger<BatchScheduleDetails> _logger;
        public BatchProductionPreparationController(IBatchProductionPreparationService service, IRepositoryWrapper repository, RepositoryContext repositoryContext)
        {
            this._service = service;
            //   _logger = logger;
            this._repository = repository;
            this._repositoryContext = repositoryContext;
        }

        [HttpGet]
        [Route("GetOrgofficelocationDetails")]
        public async Task<IHttpActionResult> GetOrgofficelocationDetails()
        {
            try
            {
                var res = await _service.GetOrgofficelocationDetails();
                return Ok(res);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(BatchProductionPreparationController.GetOrgofficelocationDetails)}");
                return InternalServerError();
            }

        }

        [HttpGet]
        [Route("GetGreenReceivedByOrgOfficeNo")]
        public async Task<IHttpActionResult> GetGreenReceivedByOrgOfficeNo(int OrgOfficeNo)
        {
            try
            {
                var res = await _service.GetGreenReceivedByOrgOfficeNo(OrgOfficeNo);
                return Ok(res);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(BatchProductionPreparationController.GetGreenReceivedByOrgOfficeNo)}");
                return InternalServerError();
            }

        }


        [HttpGet]
        [Route("GetMediaProcess")]
        public async Task<IHttpActionResult> GetMediaProcessDetails()
        {
            try
            {
                var res = await _service.GetMediaProcessDetails();
                return Ok(res);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(BatchProductionPreparationController.GetMediaProcessDetails)}");
                return InternalServerError();
            }

        }


        [HttpGet]
        [Route("GetScheduledBy")]
        public async Task<IHttpActionResult> GetScheduledByDetails()
        {
            try
            {
                var res = await _service.GetScheduledByDetails();
                return Ok(res);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(BatchProductionPreparationController.GetScheduledByDetails)}");
                return InternalServerError();
            }

        }

        [HttpGet]
        [Route("GetProductGroup")]
        public async Task<IHttpActionResult> GetProductGroupDetails()
        {
            try
            {
                var res = await _service.GetProductGroupDetails();
                return Ok(res);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(BatchProductionPreparationController.GetProductGroupDetails)}");
                return InternalServerError();
            }

        }

        [HttpGet]
        [Route("GetProductName")]
        public async Task<IHttpActionResult> GetProductNameDetails(string GroupCode)
        {
            try
            {
                var res = await _service.GetProductNameDetails(GroupCode);
                return Ok(res);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(BatchProductionPreparationController.GetProductNameDetails)}");
                return InternalServerError();
            }

        }

        [HttpGet]
        [Route("GetGrade")]
        public async Task<IHttpActionResult> GetGradeDetails(string VarietyCode)
        {
            try
            {
                var res = await _service.GetGradeDetails(VarietyCode);
                return Ok(res);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(BatchProductionPreparationController.GetGradeDetails)}");
                return InternalServerError();
            }

        }

        [HttpGet]
        [Route("GetScheduleOrderDetail")]
        public async Task<IHttpActionResult> GetScheduleOrderDetails(int OrgOfficeNo)
        {
            try
            {
                var res = await _service.GetScheduleOrderDetails(OrgOfficeNo);
                return Ok(res);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(BatchProductionPreparationController.GetScheduleOrderDetails)}");
                return InternalServerError();
            }

        }
        [HttpGet]
        [Route("GetScheduleOrderDetailByMediaProcess")]
        public async Task<IHttpActionResult> GetScheduleOrderDetailByMediaProcess(string mediaProcessCode)
        {
            try
            {
                var res = await _service.GetScheduleOrderDetailsByMediaProcess(mediaProcessCode);
                return Ok(res);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(BatchProductionPreparationController.GetScheduleOrderDetailByMediaProcess)}");
                return InternalServerError();
            }

        }
        [HttpGet]
        [Route("GetLatestBatchNo")]
        public async Task<IHttpActionResult> GetLatestBatchNo()
        {
            try
            {
                var res = await _service.GetLatestBatchNo();
                return Ok(res);

            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("SaveProductionBatchDetails")]
        public async Task<IHttpActionResult> SaveProductionBatchDetails([FromBody] BatchProductionDetails batchProductionDetailsObject)
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            try
            {
                var data = await _service.SaveBatchProductionDetails(batchProductionDetailsObject);
                if (data != null)
                {
                    apiResponse.IsSucceed = true;
                    apiResponse.Data = data;
                }
                else
                {
                    apiResponse.IsSucceed = false;
                    apiResponse.Data = null;
                }

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                apiResponse.IsSucceed = false;
                apiResponse.Exception = ex;
                return Ok(apiResponse);
            }
        }
    }
}
