using GherkinWebAPI.Core;
using GherkinWebAPI.Core.TransportVehicleManagement;
using GherkinWebAPI.Models.TransportVehicleManagement;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Service.TransportVehicleManagement
{
	public class HiredVehicleDocumentService : IHiredVehicleDocumentService
	{
		private IRepositoryWrapper repositoryWrapper;

		public HiredVehicleDocumentService(IRepositoryWrapper _repositoryWrapper)
		{
			repositoryWrapper = _repositoryWrapper;
		}

		public async Task<HiredVehicleDocument> CreateHiredVehicleDocuments(HiredVehicleDocument hiredVehicleDocument)
		{
			return await repositoryWrapper.Hired_Vehicle_DocumentsRepository.CreateHiredVehicleDocuments(hiredVehicleDocument);
		}

		public async Task<HiredVehicleDocument> GetHiredVehicleDocumentByDocId(string docUploadNo)
		{
			return await repositoryWrapper.Hired_Vehicle_DocumentsRepository.GetHiredVehicleDocumentByDocId(docUploadNo);
		}

		public async Task<List<HiredVehicleDocument>> GetHiredVehicleDocumentsList(int hiredVehicleId)
		{
			return await repositoryWrapper.Hired_Vehicle_DocumentsRepository.GetHiredVehicleDocumentsList(hiredVehicleId);
		}
	}
}