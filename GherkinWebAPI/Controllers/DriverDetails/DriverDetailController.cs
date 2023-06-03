using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using GherkinWebAPI.Core.DriverDetail;
using GherkinWebAPI.Models.DriverDetail;
using GherkinWebAPI.Models.DriverDocument;
using GherkinWebAPI.ValidateModel;

namespace GherkinWebAPI.Controllers.DriverDetails
{
    [Route("api/V1/[Controller]")]
    public class DriverDetailController : ApiController
    {
        private IDriverDetailService _driverDetailService;
        public readonly string controller = nameof(DriverDetailController);
        public DriverDetailController(IDriverDetailService driverDetailService)
        {
            _driverDetailService = driverDetailService;
        }

        [Route("GetDriverDetails")]
        public async Task<IHttpActionResult> GetDriverDetails()
        {
            try
            {
                return Ok(await _driverDetailService.GetDriverDetail());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(DriverDetailController.GetDriverDetails)}");
                return InternalServerError();
            }
        }

        [Route("GetDriverDetail/{driverid}")]
        public async Task<IHttpActionResult> GetDriverDetail(int driverid)
        {
            try
            {
                var driverDetail = await _driverDetailService.GetDriverDetail(driverid);
                if (driverDetail != null)
                    return Ok(driverDetail);
                return InternalServerError();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(DriverDetailController.GetDriverDetail)}");
                return InternalServerError();
            }
        }

        [Route("GetDriverDetailByEmployeeId/{employeeid}")]
        public async Task<IHttpActionResult> GetDriverDetailByEmployeeId(int employeeid)
        {
            try
            {
                var driverDetail = await _driverDetailService.GetDriverDocumentsByEmployeeId(employeeid);
                if (driverDetail != null)
                    return Ok(driverDetail);
                return InternalServerError();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(DriverDetailController.GetDriverDetail)}");
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("AddDriverDetail")]
        [ValidateModelState]
        public async Task<IHttpActionResult> AddDriverDetail([FromBody] DriverDetail driverDetail)
        {
            try
            {
                var result = await _driverDetailService.AddDriverDetail(driverDetail);
                if (result == null)
                {
                    return InternalServerError();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(DriverDetailController.AddDriverDetail)}");
                return InternalServerError();
            }
        }


        [HttpPut]
        [Route("UpdateDriverDetail")]
        [ValidateModelState]
        public async Task<IHttpActionResult> UpdateDriverDetail([FromBody] DriverDetail driverDetail)
        {
            try
            {
                var result = await _driverDetailService.UpdateDriverDetail(driverDetail);
                if (result == null)
                {
                    return BadRequest();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(DriverDetailController.UpdateDriverDetail)}");
                return InternalServerError();
            }
        }


        [HttpDelete]
        [Route("DeleteDriverDetail")]
        public async Task<IHttpActionResult> DeleteDriverDetail([FromUri] int driverid)
        {
            try
            {
                var result = await _driverDetailService.DeleteDriverDetail(driverid);
                if (result == false)
                {
                    return BadRequest();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(DriverDetailController.DeleteDriverDetail)}");
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("UploadDriverDocument")]
        [ValidateModelState]
        public async Task<IHttpActionResult> UploadDriverDocument(int driverId, string imageName)
        {
            var driverDocument = new DriverDocument();
            driverDocument.DocumentUploadNumber = "";
            driverDocument.DriverID = driverId;
            driverDocument.DocumentName = imageName;
            var file = HttpContext.Current.Request.Files[imageName];
            if (file != null)
            {
                BinaryReader br = new BinaryReader(file.InputStream);
                driverDocument.DocumentDetail = br.ReadBytes(file.ContentLength);
            }
            return Ok(await _driverDetailService.UploadDriverDocument(driverDocument));
        }


        [HttpGet]
        [Route("GetDriverDocumentByDocumentUploadNumber/{documentUploadNumber}")]
        public async Task<HttpResponseMessage> GetDriverDocumentByDocumentUploadNumber(int documentUploadNumber)
        {
            try
            {
                var driverDocument = await _driverDetailService.GetDriverDocumentByDocumentUploadNumber(documentUploadNumber);
                if (driverDocument != null)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, driverDocument.DocumentDetail);
                    MemoryStream ms = new MemoryStream(driverDocument.DocumentDetail);
                    response.Content = new StreamContent(ms);
                    response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
                    return response;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in Driver Detail Controller/ {nameof(DriverDetailController.GetDriverDocumentByDocumentUploadNumber)}");
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return new HttpResponseMessage();
        }



        [HttpGet]
        [Route("GetAllEmployeeNotRegisterWithDriverDetails/{designationcode}")]
        public async Task<IHttpActionResult> GetAllEmployeeNotRegisterWithDriverDetails(string designationcode)
        {
            try
            {
                var employees = await _driverDetailService.GetAllEmployeeNotRegisterWithDriverDetails(designationcode);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in EmployeeController / {nameof(EmployeeController.GetAllEmployeeByDesignationCode)}");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetAllEmployeeRegisterWithDriverDetails/{designationcode}")]
        public async Task<IHttpActionResult> GetAllEmployeeRegisterWithDriverDetails(string designationcode)
        {
            try
            {
                var employees = await _driverDetailService.GetAllEmployeeRegisterWithDriverDetails(designationcode);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in EmployeeController / {nameof(EmployeeController.GetAllEmployeeByDesignationCode)}");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetAllDriverNames")]
        public async Task<IHttpActionResult> GetAllDriverNames()
        {
            try
            {
                var driverDto = await _driverDetailService.GetAllDriverNames();
                return Ok(driverDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in DriverDetailController / {nameof(DriverDetailController.GetAllDriverNames)}");
                return InternalServerError();
            }
        }
    }
}