using System.Collections.Generic;
using System.Threading.Tasks;
using GherkinWebAPI.Models.GreensTransportVehicleSchedules;
using System;
using GherkinWebAPI.Models.Employee;

namespace GherkinWebAPI.Core.GreensTransportVehicleSchedules
{
    public interface IGreensTransportVehicleScheduleRepository
    {
        Task<string> GetRGPNo();
        Task<List<ReturnableGatePassDetail>> GetRGPDetails();
        Task<GreensTransportVechicleScheduleDetail> SearchGreensTransportVechicleScheduleDetail(string rgpNo);
        Task<GreensTransportVehicleSchedule> AddGreensTransportVehicleSchedule(GreensTransportVehicleSchedule greensTransportVehicleSchedule);
        Task<GreensTransportMaterialDetail> AddGreensTransportMaterialDetail(GreensTransportMaterialDetail greensTransportMaterialDetail);
        Task<ReturnableGatePassDetail> AddReturnableGatePassDetail(ReturnableGatePassDetail returnableGatePassDetail);
        Task<GreensTransportVehicleSchedule> UpdateGreensTransportVehicleSchedule(GreensTransportVehicleSchedule greensTransportVehicleSchedule);
        Task<GreensTransportMaterialDetail> UpdateGreensTransportMaterialDetail(GreensTransportMaterialDetail greensTransportMaterialDetail);
        Task<ReturnableGatePassDetail> UpdateReturnableGatePassDetail(ReturnableGatePassDetail returnableGatePassDetail);
        Task<List<Employee>> GetGreensTransportVehicleBuyingSupervisor(string areaId, DateTime dateOfEntry);
    }
}
