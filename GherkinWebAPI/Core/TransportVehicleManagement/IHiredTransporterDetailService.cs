using GherkinWebAPI.Models.TransportVehicleManagement;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.TransportVehicleManagement
{
	public interface IHiredTransporterDetailService
	{
		Task<HiredTransporterDetail> CreateHiredTransporterDetails(HiredTransporterDetail hiredTransporterDetail);
		Task<HiredTransporterDetail> UpdateHiredTransporterDetails(HiredTransporterDetail hiredTransporterDetail);
		Task<List<HiredTransporterDetail>> GetHiredTransporterList();
	}
}