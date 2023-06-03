using GherkinWebAPI.Core.GreensTransportVehicleSchedules;
using GherkinWebAPI.Models.GreensTransportVehicleSchedules;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using GherkinWebAPI.ValidateModel;

namespace GherkinWebAPI.Controllers
{
    [Route("api/v1/[Contoller]")]
    public class GreensTransportVehicleScheduleController : ApiController
    {
        private readonly IGreensTransportVehicleScheduleService _service;
        private readonly ILogger<GreensTransportVehicleSchedule> _logger;
        public readonly string controller = nameof(GreensTransportVehicleScheduleController);

        public GreensTransportVehicleScheduleController(IGreensTransportVehicleScheduleService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("GetRGPNo")]
        public async Task<IHttpActionResult> GetRGPNo()
        {
            try
            {
                var gtvDetails = await _service.GetRGPNo();
                return Ok(gtvDetails);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(GreensTransportVehicleScheduleController.GetRGPNo)}");
                return InternalServerError();
            }

        }

        [HttpGet]
        [Route("GetGreensTransportVehicleDetail/{rgpNo}")]
        public async Task<IHttpActionResult> GetGreensTransportVehicleDetail(string rgpNo)
        {
            try
            {
                var gtvDetails = await _service.SearchGreensTransportVechicleScheduleDetail(rgpNo);
                return Ok(gtvDetails);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(GreensTransportVehicleScheduleController.GetGreensTransportVehicleDetail)}");
                return InternalServerError();
            }

        }

        [HttpGet]
        [Route("GetRGPDetail")]
        public async Task<IHttpActionResult> GetRGPDetail()
        {
            try
            {
                var rgpDetail = await _service.GetRGPDetails();
                return Ok(rgpDetail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(GreensTransportVehicleScheduleController.GetRGPDetail)}");
                return InternalServerError();
            }
        }

        [HttpPost]
        [ValidateModelState]
        [CheckModelForNull]
        [Route("AddGreensTransportVehicleSchedule")]
        public async Task<IHttpActionResult> AddGreensTransportVehicleSchedule(GreensTransportVechicleScheduleDetail greensTransportVechicleScheduleDetail)
        {
            try
            {
                var returnableGatePassDetail = new ReturnableGatePassDetail
                {
                    RGPNo = greensTransportVechicleScheduleDetail.RGPNo,
                    RGPDate = greensTransportVechicleScheduleDetail.ReturnableGatePassDate
                };

                var rgpDetail = await _service.AddReturnableGatePassDetail(returnableGatePassDetail);

                var greensTransportVehicleSchedule = new GreensTransportVehicleSchedule
                {
                    GreensTransVehicleDespNo = greensTransportVechicleScheduleDetail.GreensTransVehicleDespNo,
                    EntryDate = greensTransportVechicleScheduleDetail.DateofEntry,
                    EnteredEmpID = greensTransportVechicleScheduleDetail.LoginUserName,
                    OrgofficeNo = greensTransportVechicleScheduleDetail.OrgOfficeNo,
                    AreaId = greensTransportVechicleScheduleDetail.AreaId,
                    BuyerEmpId = greensTransportVechicleScheduleDetail.BuyerEmpId,
                    RGPNo = greensTransportVechicleScheduleDetail.RGPNo,
                    GCTransporterName = greensTransportVechicleScheduleDetail.TransporterName,
                    HiredTransID = greensTransportVechicleScheduleDetail.HiredTransID,
                    OwnVehicleID = greensTransportVechicleScheduleDetail.OwnVehicleID,
                    HiredVehicleID = greensTransportVechicleScheduleDetail.HiredVehicleID,
                    DriverID = greensTransportVechicleScheduleDetail.DriverID,
                    GCDriverName = greensTransportVechicleScheduleDetail.DriverName,
                    GCDriverContactNo = greensTransportVechicleScheduleDetail.DriverContactNo,
                    VehicleReading = greensTransportVechicleScheduleDetail.StartKMSReading,
                    TimeofDespatch = greensTransportVechicleScheduleDetail.TimeofDespatch,
                    RGPRemarks = greensTransportVechicleScheduleDetail.Remarks
                };

                var gtvDetail = await _service.AddGreensTransportVehicleSchedule(greensTransportVehicleSchedule);

                foreach (var gtm in greensTransportVechicleScheduleDetail.GreenTransportMaterials)
                {
                    var greensTransportMaterialDetail = new GreensTransportMaterialDetail
                    {
                        ID = gtm.ID,
                        GreensTransVehicleDespNo = gtvDetail.GreensTransVehicleDespNo,
                        RawMaterialGroupCode = gtm.MaterailGroupCode,
                        RawMaterialDetailsCode = gtm.MaterialNameCode,
                        DescDetails = gtm.DescDetails,
                        DespQty = gtm.TotalNo
                    };

                    var gtmDetail = await _service.AddGreensTransportMaterialDetail(greensTransportMaterialDetail);
                }

                return Ok($"GreensTransportVehicleSchedule with Code : {gtvDetail.GreensTransVehicleDespNo} Created");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(GreensTransportVehicleScheduleController.AddGreensTransportVehicleSchedule)}");
                return InternalServerError();
            }
        }

        [HttpPost]
        [ValidateModelState]
        [CheckModelForNull]
        [Route("UpdateGreensTransportVehicleSchedule")]
        public async Task<IHttpActionResult> UpdateGreensTransportVehicleSchedule(GreensTransportVechicleScheduleDetail greensTransportVechicleScheduleDetail)
        {
            try
            {
                var returnableGatePassDetail = new ReturnableGatePassDetail
                {
                    RGPNo = greensTransportVechicleScheduleDetail.RGPNo,
                    RGPDate = greensTransportVechicleScheduleDetail.ReturnableGatePassDate
                };

                var rgpDetail = await _service.UpdateReturnableGatePassDetail(returnableGatePassDetail);

                var greensTransportVehicleSchedule = new GreensTransportVehicleSchedule
                {
                    GreensTransVehicleDespNo = greensTransportVechicleScheduleDetail.GreensTransVehicleDespNo,
                    EntryDate = greensTransportVechicleScheduleDetail.DateofEntry,
                    EnteredEmpID = greensTransportVechicleScheduleDetail.LoginUserName,
                    OrgofficeNo = greensTransportVechicleScheduleDetail.OrgOfficeNo,
                    AreaId = greensTransportVechicleScheduleDetail.AreaId,
                    BuyerEmpId = greensTransportVechicleScheduleDetail.BuyerEmpId,
                    RGPNo = greensTransportVechicleScheduleDetail.RGPNo,
                    GCTransporterName = greensTransportVechicleScheduleDetail.TransporterName,
                    HiredTransID = greensTransportVechicleScheduleDetail.HiredTransID,
                    OwnVehicleID = greensTransportVechicleScheduleDetail.OwnVehicleID,
                    DriverID = greensTransportVechicleScheduleDetail.DriverID,
                    HiredVehicleID = greensTransportVechicleScheduleDetail.HiredVehicleID,
                    GCDriverName = greensTransportVechicleScheduleDetail.DriverName,
                    GCDriverContactNo = greensTransportVechicleScheduleDetail.DriverContactNo,
                    VehicleReading = greensTransportVechicleScheduleDetail.StartKMSReading,
                    TimeofDespatch = greensTransportVechicleScheduleDetail.TimeofDespatch,
                    RGPRemarks = greensTransportVechicleScheduleDetail.Remarks
                };

                var gtvDetail = await _service.UpdateGreensTransportVehicleSchedule(greensTransportVehicleSchedule);

                foreach (var gtm in greensTransportVechicleScheduleDetail.GreenTransportMaterials)
                {
                    var greensTransportMaterialDetail = new GreensTransportMaterialDetail
                    {
                        ID = gtm.ID,
                        GreensTransVehicleDespNo = gtvDetail.GreensTransVehicleDespNo,
                        RawMaterialGroupCode = gtm.MaterailGroupCode,
                        RawMaterialDetailsCode = gtm.MaterialNameCode,
                        DescDetails = gtm.DescDetails,
                        DespQty = gtm.TotalNo
                    };
                    if (greensTransportMaterialDetail.ID > 0)
                    {
                        var gtmDetail = await _service.UpdateGreensTransportMaterialDetail(greensTransportMaterialDetail);
                    }
                    else 
                    {
                        var gtmDetail = await _service.AddGreensTransportMaterialDetail(greensTransportMaterialDetail);
                    }
                }

                return Ok($"GreensTransportVehicleSchedule with Code : {gtvDetail.GreensTransVehicleDespNo} Modified");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(GreensTransportVehicleScheduleController.UpdateGreensTransportVehicleSchedule)}");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetGreensTransportVehicleBuyingSupervisor/{areaId}/{dateOfEntry}")]
        public async Task<IHttpActionResult> GetGreensTransportVehicleBuyingSupervisor(string areaId, DateTime dateOfEntry)
        {
            try
            {
                var buyerSupervisor = await _service.GetGreensTransportVehicleBuyingSupervisor(areaId,dateOfEntry);
                return Ok(buyerSupervisor);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(GreensTransportVehicleScheduleController.GetGreensTransportVehicleBuyingSupervisor)}");
                return InternalServerError();
            }

        }
    }
}
