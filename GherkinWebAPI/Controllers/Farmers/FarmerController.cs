using GherkinWebAPI.Core;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.Farmers;
using GherkinWebAPI.Utilities;
using GherkinWebAPI.ValidateModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace GherkinWebAPI.Controllers
{
    [Route("api/V1/[Controller]")]
    public class FarmerController : ApiController
    {
        private readonly IFarmerService _service;
        public FarmerController(IFarmerService service)
        {
            _service = service;
        }

        [HttpGet, Route("GetAllFarmers")]
        public async Task<HttpResponseMessage> GetAllFarmers()
        {

            List<Farmer> farmers = new List<Farmer>();
            try
            {
                farmers = await _service.GetAllFarmers();

                if (farmers.Count > 0)
                    return Request.CreateResponse(HttpStatusCode.OK, farmers);
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
        [HttpGet, Route("GetFarmerByCode")]
        public async Task<HttpResponseMessage> GetFarmerByCode(string code)
        {

            FarmerDetailsDTO farmer = new FarmerDetailsDTO();
            try
            {
                farmer = await _service.GetFarmersByCode(code);
                if (farmer != null)
                    return Request.CreateResponse(HttpStatusCode.OK, farmer);
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        [Route("GetFarmersByVillageCode")]
        public async Task<IHttpActionResult> GetFarmersByVillageCode(int villageCode)
        {
            List<FarmersDetail> farmers = new List<FarmersDetail>();
            try
            {
                farmers = await _service.GetFarmersByVillageCode(villageCode);
                return Ok(farmers);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in DepartmentController/{nameof(FarmerController.GetFarmersByVillageCode)}");
                return InternalServerError();
            }
        }
        [CheckModelForNull]
        [ValidateModelState]
        [HttpPost, Route("AddFarmer")]
        public async Task<HttpResponseMessage> AddFarmer([System.Web.Mvc.Bind(Exclude = "Id, Farmer_Code")][FromBody] FarmerDetailsDTO farmerDetail)
        {
            try
            {
                var fardetail = await _service.AddFarmer(farmerDetail);
                return Request.CreateResponse(HttpStatusCode.OK, fardetail);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [CheckModelForNull]
        [ValidateModelState]
        [HttpPost, Route("AddFarmerBankAccountDetails")]
        public async Task<HttpResponseMessage> AddFarmerBankAccountDetails([System.Web.Mvc.Bind(Exclude = "FarmerBankDetailsDTO")][FromBody] List<FarmerBankDetailsDTO> bankDetailList)
        {
            try
            {
                await _service.AddBankAccountDetails(bankDetailList);
                return Request.CreateResponse(HttpStatusCode.OK, bankDetailList);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [CheckModelForNull]
        [ValidateModelState]
        [HttpPut, Route("UpdateFarmer")]
        public async Task<HttpResponseMessage> UpdateFarmer([FromBody] FarmerDetailsDTO farmer)
        {
            try
            {
                await _service.UpdateFarmer(farmer.Farmer_Code, farmer);
                return Request.CreateResponse(HttpStatusCode.OK, farmer);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPost, Route("UploadDocuments")]
        public async Task<HttpResponseMessage> UploadDocuments([FromBody] IEnumerable<FarmerDocumentDTO> uploadDocuments)
        {
            if (uploadDocuments.Count() > 0)
            {
                try
                {
                    List<FarmerDocument> farmerDocuments = new List<FarmerDocument>();
                    foreach (var farmer in uploadDocuments)
                    {
                        FarmerDocument _farmerDocument = new FarmerDocument();
                        _farmerDocument.Farmer_Code = farmer.Farmer_Code;
                        _farmerDocument.DocumentName = farmer.DocumentName;
                        var file = HttpContext.Current.Request.Files[farmer.DocumentName];
                        var fileType = file.ContentType;

                        if (file != null)
                        {
                            BinaryReader br = new BinaryReader(file.InputStream);
                            _farmerDocument.Document = br.ReadBytes(file.ContentLength);

                            farmerDocuments.Add(_farmerDocument);
                        }

                    }

                    await _service.SaveFarmerDocuments(farmerDocuments);
                    return Request.CreateResponse(HttpStatusCode.OK, farmerDocuments);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);

            }
        }
        [CheckModelForNull]
        [ValidateModelState]
        [HttpPost, Route("SaveFarmerDocument")]
        public async Task<IHttpActionResult> SaveFarmerDocument([FromBody] string farmerCode, string fileName)
        {
            try
            {
                FarmerDocument docDetails = new FarmerDocument();
                docDetails.Farmer_Code = farmerCode;
                docDetails.DocumentName = fileName;
                var file = HttpContext.Current.Request.Files[fileName];
                if (file != null)
                {
                    BinaryReader br = new BinaryReader(file.InputStream);
                    docDetails.Document = br.ReadBytes(file.ContentLength);
                    await _service.SaveFarmerDocument(docDetails);
                    return Ok(true);
                }
                else
                    return NotFound();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in EmployeeController / {nameof(EmployeeController.SaveDocument)}");
                return InternalServerError();
            }
        }

        
        [HttpGet, Route("GetFarmerDocumentsbyFarmerCode")]
        public async Task<HttpResponseMessage> GetFarmerDocuments(string code)
        {
            List<FarmerDocument> farmerDocuments = new List<FarmerDocument>();
            try
            {
                farmerDocuments = await _service.GetFarmerDocumentsbyFarmer(code);
                if (farmerDocuments != null || farmerDocuments.Count>0)
                    return Request.CreateResponse(HttpStatusCode.OK, farmerDocuments);
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }


        }
        [HttpGet, Route("GetFarmerDocumentsByID")]
        public async Task<HttpResponseMessage> GetFarmerDocumentsByID(int Id)
        {
            FarmerDocument farmerDocument = new FarmerDocument();
            try
            {
                farmerDocument = await _service.GetFarmerDocumentbyID(Id);
                if (farmerDocument != null)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new ByteArrayContent(farmerDocument.Document);
                    response.Content.Headers.ContentLength = farmerDocument.Document.LongLength;
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    response.Content.Headers.ContentDisposition.FileName = farmerDocument.DocumentName;
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(farmerDocument.DocumentName));
                    return response;
                    // return Request.CreateResponse(HttpStatusCode.OK, farmerDocument);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }


        }
        [HttpPost, Route("DeleteFarmerDocumentsByID")]
        public async Task<HttpResponseMessage> DeleteFarmerDocumentsByID(int Id)
        {
            try
            {
                await _service.DeleteFarmerDocumentbyID(Id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }


        }

        [Route("farmerbystatecode")]
        public async Task<HttpResponseMessage> GetFarmerByStateCode(int stateCode)
        {
            try
            {
                var data = await _service.GetFarmerByStateCode(stateCode);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }


        }

        [HttpGet, Route("GetFarmerListByAreaEmployeePSNumberAndFarmerName/{farmerName}/{areaId}/{employeeId}/{psNumber}")]
        public async Task<IHttpActionResult> GetFarmerListByAreaEmployeePSNumberAndFarmerName(string farmerName, string areaId, string employeeId, string psNumber)
        {
            try
            {
                var farmerList = await _service.GetFarmerListByAreaEmployeePSNumberAndFarmerName(farmerName, areaId, employeeId, psNumber);
                return Ok(farmerList);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in GetFarmerListByAreaEmployeePSNumberAndFarmerName / {nameof(FarmerController.GetFarmerListByAreaEmployeePSNumberAndFarmerName)}");
                return InternalServerError();
            }
        }

        [HttpGet, Route("GetFarmerListByAreaEmployeePSNumberAndFarmerAltContactPerson/{farmerAltContactPerson}/{areaId}/{employeeId}/{psNumber}")]
        public async Task<IHttpActionResult> GetFarmerListByAreaEmployeePSNumberAndFarmerAltContactPerson(string farmerAltContactPerson, string areaId, string employeeId, string psNumber)
        {
            try
            {
                var farmerList = await _service.GetFarmerListByAreaEmployeePSNumberAndFarmerAltContactPerson(farmerAltContactPerson, areaId, employeeId, psNumber);
                return Ok(farmerList);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in GetFarmerListByAreaEmployeePSNumberAndFarmerAltContactPerson / {nameof(FarmerController.GetFarmerListByAreaEmployeePSNumberAndFarmerAltContactPerson)}");
                return InternalServerError();
            }
        }

        [HttpGet, Route("GetFarmerListByAreaAndVillageCodeAndPSNumber/{areaId}/{psNumber}/{villageCode}")]
        public async Task<IHttpActionResult> GetFarmerListByAreaAndVillageCodeAndPSNumber(string areaId, string psNumber,int villageCode)
        {
            try
            {
                var farmerList = await _service.GetFarmerListByAreaAndVillageCodeAndPSNumber(areaId, psNumber, villageCode);
                return Ok(farmerList);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in GetFarmerListByAreaAndVillageCodeAndPSNumberAndFarmerName / {nameof(FarmerController.GetFarmerListByAreaAndVillageCodeAndPSNumber)}");
                return InternalServerError();
            }
        }

        [HttpGet, Route("GetFarmerByAreaAndPSNumberAndAccountNo/{areaId}/{psNumber}/{accountNo}")]
        public async Task<IHttpActionResult> GetFarmerByAreaAndPSNumberAndAccountNo(string areaId, string psNumber,string accountNo)
        {
            try
            {
                var farmerList = await _service.GetFarmerByAreaAndPSNumberAndAccountNo(areaId, psNumber, accountNo);
                return Ok(farmerList);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in GetFarmerByAreaAndPSNumberAndAccountNo / {nameof(FarmerController.GetFarmerByAreaAndPSNumberAndAccountNo)}");
                return InternalServerError();
            }
        }

        [HttpGet, Route("GetAllFarmersWithAgreementDetail")]
        public async Task<IHttpActionResult> GetAllFarmersWithAgreementDetail()
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            try
            {
                var res = await _service.GetAllFarmersWithAgreementDetail();
                if (res != null)
                {
                    apiResponse.IsSucceed = true;
                    apiResponse.Data = res;
                }
                else
                {
                    apiResponse.IsSucceed = true;
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

