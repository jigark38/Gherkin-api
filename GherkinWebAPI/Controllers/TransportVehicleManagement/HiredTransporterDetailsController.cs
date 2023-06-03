using GherkinWebAPI.Core.TransportVehicleManagement;
using GherkinWebAPI.Models.TransportVehicleManagement;
using GherkinWebAPI.ValidateModel;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace GherkinWebAPI.Controllers.TransportVehicleManagement
{
    [RoutePrefix("api/v1/HiredTransporterDetails")]
    public class HiredTransporterDetailsController : ApiController
    {
        private readonly IHiredTransporterDetailService _transporterService;
        private readonly IHiredVehicleDetailService _vehicleService;
        private readonly IHiredVehicleDocumentService _documentsService;
        public readonly string controller = nameof(HiredTransporterDetailsController);

        public HiredTransporterDetailsController(IHiredTransporterDetailService transporterService, IHiredVehicleDetailService vehicleService,
            IHiredVehicleDocumentService documentsService)
        {
            _transporterService = transporterService;
            _vehicleService = vehicleService;
            _documentsService = documentsService;
        }


        #region Hired Transporter

        [CheckModelForNull]
        [ValidateModelState]
        [Route("CreateHiredTransporterDetails")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateHiredTransporterDetails([FromBody] HiredTransporterDetail hired_Transporter_Details)
        {
            try
            {
                if (hired_Transporter_Details == null)
                    return null;
                hired_Transporter_Details = await _transporterService.CreateHiredTransporterDetails(hired_Transporter_Details);
                return Ok(hired_Transporter_Details);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in HiredTransporterDetailsController / {nameof(HiredTransporterDetailsController.CreateHiredTransporterDetails)}");
                return InternalServerError();
            }
        }

        [CheckModelForNull]
        [ValidateModelState]
        [Route("UpdateHiredTransporterDetails")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateHiredTransporterDetails([FromBody] HiredTransporterDetail hired_Transporter_Details)
        {
            try
            {
                hired_Transporter_Details = await _transporterService.UpdateHiredTransporterDetails(hired_Transporter_Details);
                return Ok(hired_Transporter_Details);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in HiredTransporterDetailsController / {nameof(HiredTransporterDetailsController.UpdateHiredTransporterDetails)}");
                return InternalServerError();
            }
        }

        [Route("GetHiredTransporterList")]
        [HttpGet]
        public async Task<IHttpActionResult> GetHiredTransporterList()
        {
            try
            {
                var transporterList = await _transporterService.GetHiredTransporterList();
                return Ok(transporterList);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in GetHiredTransporterListByAgencyName / {nameof(HiredTransporterDetailsController.GetHiredTransporterList)}");
                return InternalServerError();
            }
        }

        #endregion


        #region Hired Vehicle

        [CheckModelForNull]
        [ValidateModelState]
        [Route("CreateHiredVehicleDetails")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateHiredVehicleDetails([FromBody] HiredVehicleDetail hired_Vehicle_Details)
        {
            try
            {
                if (hired_Vehicle_Details == null)
                    return null;
                hired_Vehicle_Details = await _vehicleService.CreateHiredVehicleDetails(hired_Vehicle_Details);
                return Ok(hired_Vehicle_Details);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in HiredTransporterDetailsController / {nameof(HiredTransporterDetailsController.CreateHiredVehicleDetails)}");
                return InternalServerError();
            }
        }

        [CheckModelForNull]
        [ValidateModelState]
        [Route("UpdateHiredVehicleDetails")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateHiredVehicleDetails([FromBody] HiredVehicleDetail hired_Vehicle_Details)
        {
            try
            {
                hired_Vehicle_Details = await _vehicleService.UpdateHiredVehicleDetails(hired_Vehicle_Details);
                return Ok(hired_Vehicle_Details);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in HiredTransporterDetailsController / {nameof(HiredTransporterDetailsController.UpdateHiredVehicleDetails)}");
                return InternalServerError();
            }
        }

        [Route("GetHiredVehicleList/{hiredTransporterId}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetHiredVehicleList(int hiredTransporterId)
        {
            try
            {
                var vehicleList = await _vehicleService.GetHiredVehicleList(hiredTransporterId);
                return Ok(vehicleList);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in GetHiredTransporterListByAgencyName / {nameof(HiredTransporterDetailsController.GetHiredVehicleList)}");
                return InternalServerError();
            }
        }

        [Route("CheckDuplicateHiredVehicleRegNo/{vehicleRegNo}")]
        [HttpGet]
        public async Task<IHttpActionResult> CheckDuplicateHiredVehicleRegNo(string vehicleRegNo)
        {
            bool isDuplicate = false;
            try
            {
                isDuplicate = await _vehicleService.CheckDuplicateHiredVehicleRegNo(vehicleRegNo);
                return Ok(isDuplicate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}  / {nameof(HiredTransporterDetailsController.CheckDuplicateHiredVehicleRegNo)}");
                return InternalServerError();
            }
        }

        [Route("CheckDuplicateHiredVehicleChassisNo/{chassisNo}")]
        [HttpGet]
        public async Task<IHttpActionResult> CheckDuplicateHiredVehicleChassisNo(string chassisNo)
        {
            bool isDuplicate = false;
            try
            {
                isDuplicate = await _vehicleService.CheckDuplicateHiredVehicleChasisNo(chassisNo);
                return Ok(isDuplicate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}  / {nameof(HiredTransporterDetailsController.CheckDuplicateHiredVehicleChassisNo)}");
                return InternalServerError();
            }
        }

        #endregion


        #region Vehicle Documents

        [Route("CreateHiredVehicleDocuments")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateHiredVehicleDocuments()
        {
            try
            {
                HiredVehicleDocument hired_Vehicle_Documents = new HiredVehicleDocument();
                hired_Vehicle_Documents.HiredVehicleID = Convert.ToInt32(HttpContext.Current.Request.Form["hiredVehicleID"]);
                hired_Vehicle_Documents.DocUploadNo = HttpContext.Current.Request.Form["docUploadNo"];
                hired_Vehicle_Documents.DocumentName = HttpContext.Current.Request.Form["documentName"];

                var file = HttpContext.Current.Request.Files["documentDetails"];
                if (file != null)
                {
                    BinaryReader br = new BinaryReader(file.InputStream);
                    hired_Vehicle_Documents.DocumentDetails = br.ReadBytes(file.ContentLength);
                }

                if (hired_Vehicle_Documents == null)
                    return null;
                hired_Vehicle_Documents = await _documentsService.CreateHiredVehicleDocuments(hired_Vehicle_Documents);
                return Ok(hired_Vehicle_Documents);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in HiredTransporterDetailsController / {nameof(HiredTransporterDetailsController.CreateHiredVehicleDocuments)}");
                return InternalServerError();
            }
        }

        [Route("GetHiredVehicleDocumentsList/{hiredVehicleId}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetHiredVehicleDocumentsList(int hiredVehicleId)
        {
            try
            {
                var documentsList = await _documentsService.GetHiredVehicleDocumentsList(hiredVehicleId);
                return Ok(documentsList);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in GetHiredTransporterListByAgencyName / {nameof(HiredTransporterDetailsController.GetHiredVehicleDocumentsList)}");
                return InternalServerError();
            }
        }


        [HttpGet]
        [Route("GetHiredVehicleDocumentByDocId/{documentId}")]
        public async Task<HttpResponseMessage> GetHiredVehicleDocumentByDocId(string documentId)
        {
            try
            {
                HiredVehicleDocument hiredVehicleDocument = new HiredVehicleDocument();
                hiredVehicleDocument = await _documentsService.GetHiredVehicleDocumentByDocId(documentId);
                if (hiredVehicleDocument != null)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, hiredVehicleDocument.DocumentDetails);
                    MemoryStream ms = new MemoryStream(hiredVehicleDocument.DocumentDetails);
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

        #endregion

    }
}
