using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GherkinWebAPI.Core;
using GherkinWebAPI.Core.GreensTransportVehicleSchedules;
using GherkinWebAPI.Models.Employee;
using GherkinWebAPI.Models.GreensTransportVehicleSchedules;

namespace GherkinWebAPI.Service.GreensTransportVehicleSchedules
{
    public class GreensTransportVehicleScheduleService : IGreensTransportVehicleScheduleService
    {
        private IRepositoryWrapper repositoryWrapper;

        public GreensTransportVehicleScheduleService(IRepositoryWrapper _repositoryWrapper)
        {
            repositoryWrapper = _repositoryWrapper;
        }
        public async Task<GreensTransportMaterialDetail> AddGreensTransportMaterialDetail(GreensTransportMaterialDetail greensTransportMaterialDetail)
        {
            return await repositoryWrapper.GreensTransportVehicleScheduleRepository.AddGreensTransportMaterialDetail(greensTransportMaterialDetail);
        }

        public async Task<GreensTransportVehicleSchedule> AddGreensTransportVehicleSchedule(GreensTransportVehicleSchedule greensTransportVehicleSchedule)
        {
            return await repositoryWrapper.GreensTransportVehicleScheduleRepository.AddGreensTransportVehicleSchedule(greensTransportVehicleSchedule);
        }

        public async Task<ReturnableGatePassDetail> AddReturnableGatePassDetail(ReturnableGatePassDetail returnableGatePassDetail)
        {
            return await repositoryWrapper.GreensTransportVehicleScheduleRepository.AddReturnableGatePassDetail(returnableGatePassDetail);
        }

        public async Task<GreensTransportVechicleScheduleDetail> SearchGreensTransportVechicleScheduleDetail(string rgpNo)
        {
            return await repositoryWrapper.GreensTransportVehicleScheduleRepository.SearchGreensTransportVechicleScheduleDetail(rgpNo);
        }

        public async Task<string> GetRGPNo()
        {
            return await repositoryWrapper.GreensTransportVehicleScheduleRepository.GetRGPNo();
        }
        public async Task<List<ReturnableGatePassDetail>> GetRGPDetails()
        {
            return await repositoryWrapper.GreensTransportVehicleScheduleRepository.GetRGPDetails();
        }

        public async Task<GreensTransportVehicleSchedule> UpdateGreensTransportVehicleSchedule(GreensTransportVehicleSchedule greensTransportVehicleSchedule)
        {
            return await repositoryWrapper.GreensTransportVehicleScheduleRepository.UpdateGreensTransportVehicleSchedule(greensTransportVehicleSchedule);
        }

        public async Task<GreensTransportMaterialDetail> UpdateGreensTransportMaterialDetail(GreensTransportMaterialDetail greensTransportMaterialDetail)
        {
            return await repositoryWrapper.GreensTransportVehicleScheduleRepository.UpdateGreensTransportMaterialDetail(greensTransportMaterialDetail);
        }

        public async Task<ReturnableGatePassDetail> UpdateReturnableGatePassDetail(ReturnableGatePassDetail returnableGatePassDetail)
        {
            return await repositoryWrapper.GreensTransportVehicleScheduleRepository.UpdateReturnableGatePassDetail(returnableGatePassDetail);
        }

        public async Task<List<Employee>> GetGreensTransportVehicleBuyingSupervisor(string areaId, DateTime dateOfEntry)
        {
            return await repositoryWrapper.GreensTransportVehicleScheduleRepository.GetGreensTransportVehicleBuyingSupervisor(areaId, dateOfEntry);
        }
    }
}