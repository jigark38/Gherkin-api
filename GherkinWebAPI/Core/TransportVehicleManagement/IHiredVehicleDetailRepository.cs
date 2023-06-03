using GherkinWebAPI.Models.TransportVehicleManagement;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.TransportVehicleManagement
{
	public interface IHiredVehicleDetailRepository
	{
		Task<HiredVehicleDetail> CreateHiredVehicleDetails(HiredVehicleDetail hiredVehicleDetail);
		Task<HiredVehicleDetail> UpdateHiredVehicleDetails(HiredVehicleDetail hiredVehicleDetail);
		Task<List<HiredVehicleDetail>> GetHiredVehicleList(int hiredTransporterId);
		Task<bool> CheckDuplicateHiredVehicleRegNo(string vehicleRegNo);
		Task<bool> CheckDuplicateHiredVehicleChasisNo(string chasisNumber);
	}
}