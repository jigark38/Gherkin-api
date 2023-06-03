using System.Collections.Generic;
using System.Threading.Tasks;
using GherkinWebAPI.Models.TransportVehicleManagement;
namespace GherkinWebAPI.Core.TransportVehicleManagement
{
    public interface IVehicleService
    {
        Task<OwnVehiclesDetails> CreateVehicle(OwnVehiclesDetails vehicle);
        Task<OwnVehiclesDetails> UpdateVehicle(OwnVehiclesDetails vehicle);
        Task<bool> CheckDuplicateRegistrationNumber(string registrationNumber);
        Task<bool> CheckDuplicateVehicleChasisNo(string chasisNumber);
        Task SaveDocument(OwnVehicleDocuments document);
        Task<List<OwnVehicleDocuments>> GetDocument(int vehicleId);
        Task<OwnVehiclesDetails> GetVehicleById(int vehicleId);
        Task<List<OwnVehiclesDetails>> GetVehicleByRegistrationNumber(string registrationNumber);
        Task<OwnVehicleDocuments> GetDocumentByDocId(string docUploadNo);
        Task<List<OwnVehiclesDetails>> GetAllVehicles();
    }
}
