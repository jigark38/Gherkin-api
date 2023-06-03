using GherkinWebAPI.Core.TransportVehicleManagement;
using GherkinWebAPI.Models.TransportVehicleManagement;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using GherkinWebAPI.ValidateModel;
using System.Web;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;

namespace GherkinWebAPI.Controllers.TransportVehicleManagement
{
    [RoutePrefix("api/v1/Vehicle")]
    public class VehicleController : ApiController
    {
        private readonly IVehicleService _vehicleService;
        public readonly string controller = nameof(VehicleController);

        public VehicleController(IVehicleService service)
        {
            _vehicleService = service;
        }
        [CheckModelForNull]
        [ValidateModelState]
        [Route("CreateVehicle")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateVehicle(OwnVehiclesDetails vehicle)
        {
            try
            {
                if (vehicle == null)
                    return null;
                var vhcl = await _vehicleService.CreateVehicle(vehicle);
                return Ok(vhcl);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller} / {nameof(VehicleController.CreateVehicle)}");
                return InternalServerError();
            }

        }
        [CheckModelForNull]
        [ValidateModelState]
        [Route("UpdateVehicle")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateVehicle(OwnVehiclesDetails vehicle)
        {
            try
            {
                var vhcl = await _vehicleService.UpdateVehicle(vehicle);
                return Ok(vhcl);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller} / {nameof(VehicleController.UpdateVehicle)}");
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("SaveVehicleDocument")]
        public async Task<IHttpActionResult> SaveVehicleDocument(int vehicleId, string imageName)
        {
            try
            {
                List<OwnVehicleDocuments> docList = new List<OwnVehicleDocuments>();
                docList = await _vehicleService.GetDocument(vehicleId);
                OwnVehicleDocuments docDetails = new OwnVehicleDocuments();
                docDetails.OwnVehicleID = vehicleId;
                docDetails.DocumentName = imageName;
                docDetails.DocUploadNo = "Doc_VID_" + vehicleId + "_Sl_"+ (docList.Count+1);
                var file = HttpContext.Current.Request.Files[imageName];
                if (file != null)
                {
                    BinaryReader br = new BinaryReader(file.InputStream);
                    docDetails.DocumentDetails = br.ReadBytes(file.ContentLength);
                }
                await _vehicleService.SaveDocument(docDetails);
                return Ok(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller} / {nameof(VehicleController.SaveVehicleDocument)}");
                return InternalServerError();
            }
        }
        [HttpGet]
        [Route("GetVehicleByRegistrationNumber/{registrationNumber}")]
        public async Task<IHttpActionResult> GetVehicleByRegistrationNumber(string registrationNumber)
        {
            List<OwnVehiclesDetails> vehicles = new List<OwnVehiclesDetails>();
            try
            {
                vehicles = await _vehicleService.GetVehicleByRegistrationNumber(registrationNumber);
                return Ok(vehicles);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller} / {nameof(VehicleController.GetVehicleByRegistrationNumber)}");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetDocumentsByVehicleId/{vehicleId}")]
        public async Task<IHttpActionResult> GetDocumentsByVehicleId(int vehicleId)
        {
            try
            {
                List<OwnVehicleDocuments> docList = new List<OwnVehicleDocuments>();
                docList = await _vehicleService.GetDocument(vehicleId);
                return Ok(docList);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller} / {nameof(VehicleController.GetDocumentsByVehicleId)}");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetVehicleDocumentByDocId/{documentId}")]
        public async Task<HttpResponseMessage> GetVehicleDocumentByDocId(string documentId)
        {
            try
            {
                OwnVehicleDocuments vd = new OwnVehicleDocuments();
                vd = await _vehicleService.GetDocumentByDocId(documentId);
                if (vd != null)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, vd.DocumentDetails);
                    MemoryStream ms = new MemoryStream(vd.DocumentDetails);
                    response.Content = new StreamContent(ms);
                    response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
                    return response;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller} / {nameof(VehicleController.GetVehicleDocumentByDocId)}");
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return new HttpResponseMessage();
        }

        [HttpGet]
        [Route("CheckDuplicateRegistrationNumber/{registrationNumber}")]
        public async Task<IHttpActionResult> CheckDuplicateRegistrationNumber(string registrationNumber)
        {
            bool isDuplicate = false;
            try
            {
                isDuplicate = await _vehicleService.CheckDuplicateRegistrationNumber(registrationNumber);
                return Ok(isDuplicate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}  / {nameof(VehicleController.CheckDuplicateRegistrationNumber)}");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("CheckDuplicateVehicleChasisNo/{chasisNumber}")]
        public async Task<IHttpActionResult> CheckDuplicateVehicleChasisNo(string chasisNumber)
        {
            bool isDuplicate = false;
            try
            {
                isDuplicate = await _vehicleService.CheckDuplicateVehicleChasisNo(chasisNumber);
                return Ok(isDuplicate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}  / {nameof(VehicleController.CheckDuplicateVehicleChasisNo)}");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetAllVehicles")]
        public async Task<IHttpActionResult> GetAllVehicles()
        {
            List<OwnVehiclesDetails> vehicles = new List<OwnVehiclesDetails>();
            try
            {
                vehicles = await _vehicleService.GetAllVehicles();
                return Ok(vehicles);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller} / {nameof(VehicleController.GetAllVehicles)}");
                return InternalServerError();
            }
        }
    }
}
