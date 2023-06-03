using GherkinWebAPI.Models.TransportVehicleManagement;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.TransportVehicleManagement
{
	public interface IHiredVehicleDocumentService
	{
		Task<HiredVehicleDocument> CreateHiredVehicleDocuments(HiredVehicleDocument hiredVehicleDocument);
		Task<List<HiredVehicleDocument>> GetHiredVehicleDocumentsList(int hiredVehicleId);
		Task<HiredVehicleDocument> GetHiredVehicleDocumentByDocId(string docUploadNo);
	}
}