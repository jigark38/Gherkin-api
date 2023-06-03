using GherkinWebAPI.Core.TransportVehicleManagement;
using GherkinWebAPI.Models.TransportVehicleManagement;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using GherkinWebAPI.ValidateModel;

namespace GherkinWebAPI.Controllers.TransportVehicleManagement
{
    [RoutePrefix("api/v1/GpsTrackingDevice")]
    public class GpsTrackingDeviceController : ApiController
    {
        private readonly IGpsTrackingDeviceService _gpsTrackingDeviceServiceservice;
        public readonly string controller = nameof(GpsTrackingDeviceController);

        public GpsTrackingDeviceController(IGpsTrackingDeviceService service)
        {
            _gpsTrackingDeviceServiceservice = service;
        }
        [CheckModelForNull]
        [ValidateModelState]
        [Route("CreateGpsTrackingDevice")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateGpsTrackingDevice(GPSTrackingDevices device)
        {
            try
            {
                if (device == null)
                    return null;
                var dvc = await _gpsTrackingDeviceServiceservice.CreateDevice(device);
                return Ok(dvc);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller} / {nameof(GpsTrackingDeviceController.CreateGpsTrackingDevice)}");
                return InternalServerError();
            }

        }
        [CheckModelForNull]
        [ValidateModelState]
        [Route("UpdateGpsTrackingDevice")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateGpsTrackingDevice([FromBody] GPSTrackingDevices device)
        {
            try
            {
                var dvc = await _gpsTrackingDeviceServiceservice.UpdateDevice(device);
                return Ok(dvc);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller} / {nameof(GpsTrackingDeviceController.UpdateGpsTrackingDevice)}");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetGPSDeviceByVehicleId/{vehicleId}")]
        public async Task<IHttpActionResult> GetGPSDeviceByVehicleId(int vehicleId)
        {
            try
            {
                var device = await _gpsTrackingDeviceServiceservice.GetDeviceByVehicleId(vehicleId);
                return Ok(device);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller} / {nameof(GpsTrackingDeviceController.GetGPSDeviceByVehicleId)}");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetGPSDeviceByHiredVehicleId/{hiredVehicleId}")]
        public async Task<IHttpActionResult> GetGPSDeviceByHiredVehicleId(int hiredVehicleId)
        {
            try
            {
                var device = await _gpsTrackingDeviceServiceservice.GetDeviceByHiredVehicleId(hiredVehicleId);
                return Ok(device);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller} / {nameof(GpsTrackingDeviceController.GetGPSDeviceByVehicleId)}");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("CheckDuplicateGPSTrackingDeviceNo/{deviceNumber}")]
        public async Task<IHttpActionResult> CheckDuplicateGPSTrackingDeviceNo(string deviceNumber)
        {
            bool isDuplicate = false;
            try
            {
                isDuplicate = await _gpsTrackingDeviceServiceservice.CheckDuplicateGPSTrackingDeviceNo(deviceNumber);
                return Ok(isDuplicate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}  / {nameof(GpsTrackingDeviceController.CheckDuplicateGPSTrackingDeviceNo)}");
                return InternalServerError();
            }
        }
    }
}
