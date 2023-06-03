using GherkinWebAPI.Core;
using GherkinWebAPI.Core.TransportVehicleManagement;
using GherkinWebAPI.Models.TransportVehicleManagement;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Service.TransportVehicleManagement
{
	public class HiredVehicleDetailService : IHiredVehicleDetailService
	{
		private IRepositoryWrapper repositoryWrapper;

		public HiredVehicleDetailService(IRepositoryWrapper _repositoryWrapper)
		{
			repositoryWrapper = _repositoryWrapper;
		}

		public async Task<bool> CheckDuplicateHiredVehicleChasisNo(string chasisNumber)
		{
			return await repositoryWrapper.Hired_Vehicle_DetailsRepository.CheckDuplicateHiredVehicleChasisNo(chasisNumber);
		}

		public async Task<bool> CheckDuplicateHiredVehicleRegNo(string vehicleRegNo)
		{
			return await repositoryWrapper.Hired_Vehicle_DetailsRepository.CheckDuplicateHiredVehicleRegNo(vehicleRegNo);
		}

		public async Task<HiredVehicleDetail> CreateHiredVehicleDetails(HiredVehicleDetail hiredVehicleDetail)
		{
			return await repositoryWrapper.Hired_Vehicle_DetailsRepository.CreateHiredVehicleDetails(hiredVehicleDetail);
		}

		public async Task<List<HiredVehicleDetail>> GetHiredVehicleList(int hiredTransporterId)
		{
			return await repositoryWrapper.Hired_Vehicle_DetailsRepository.GetHiredVehicleList(hiredTransporterId);
		}

		public async Task<HiredVehicleDetail> UpdateHiredVehicleDetails(HiredVehicleDetail hiredVehicleDetail)
		{
			return await repositoryWrapper.Hired_Vehicle_DetailsRepository.UpdateHiredVehicleDetails(hiredVehicleDetail);
		}

	}
}