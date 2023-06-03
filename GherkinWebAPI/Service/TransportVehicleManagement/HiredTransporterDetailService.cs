using GherkinWebAPI.Core;
using GherkinWebAPI.Core.TransportVehicleManagement;
using GherkinWebAPI.Models.TransportVehicleManagement;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Service.TransportVehicleManagement
{
	public class HiredTransporterDetailService : IHiredTransporterDetailService
	{
		private IRepositoryWrapper repositoryWrapper;

		public HiredTransporterDetailService(IRepositoryWrapper _repositoryWrapper)
		{
			repositoryWrapper = _repositoryWrapper;
		}

		public async Task<HiredTransporterDetail> CreateHiredTransporterDetails(HiredTransporterDetail hiredTransporterDetail)
		{
			return await repositoryWrapper.Hired_Transporter_DetailsRepository.CreateHiredTransporterDetails(hiredTransporterDetail);
		}

		public async Task<List<HiredTransporterDetail>> GetHiredTransporterList()
		{
			return await repositoryWrapper.Hired_Transporter_DetailsRepository.GetHiredTransporterList();
		}

		public async Task<HiredTransporterDetail> UpdateHiredTransporterDetails(HiredTransporterDetail hiredTransporterDetail)
		{
			return await repositoryWrapper.Hired_Transporter_DetailsRepository.UpdateHiredTransporterDetails(hiredTransporterDetail);
		}

	}
}