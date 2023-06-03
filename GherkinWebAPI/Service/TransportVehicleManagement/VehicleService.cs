using System.Collections.Generic;
using System.Threading.Tasks;
using GherkinWebAPI.Core;
using GherkinWebAPI.Core.TransportVehicleManagement;
using GherkinWebAPI.Models.TransportVehicleManagement;

namespace GherkinWebAPI.Service.TransportVehicleManagement
{
    public class VehicleService : IVehicleService
    {
        private IRepositoryWrapper repositoryWrapper;

        public VehicleService(IRepositoryWrapper _repositoryWrapper)
        {
            repositoryWrapper = _repositoryWrapper;
        }
       
        public async Task<bool> CheckDuplicateRegistrationNumber(string registrationNumber)
        {
            return await repositoryWrapper.VehicleRepository.CheckDuplicateRegistrationNumber(registrationNumber);
        }

        public async Task<OwnVehiclesDetails> CreateVehicle(OwnVehiclesDetails vehicle)
        {
            return await repositoryWrapper.VehicleRepository.CreateVehicle(vehicle);
        }

        public async Task<List<OwnVehicleDocuments>> GetDocument(int vehicleId)
        {
            return await repositoryWrapper.VehicleRepository.GetDocument(vehicleId);
        }

        public async Task<OwnVehicleDocuments> GetDocumentByDocId(string docUploadNo)
        {
            return await repositoryWrapper.VehicleRepository.GetDocumentByDocId(docUploadNo);
        }

        public async Task<OwnVehiclesDetails> GetVehicleById(int vehicleId)
        {
            return await repositoryWrapper.VehicleRepository.GetVehicleById(vehicleId);
        }

        public async Task<List<OwnVehiclesDetails>> GetVehicleByRegistrationNumber(string registrationNumber)
        {
            return await repositoryWrapper.VehicleRepository.GetVehicleByRegistrationNumber(registrationNumber);
        }

        public async Task SaveDocument(OwnVehicleDocuments document)
        {
            await repositoryWrapper.VehicleRepository.SaveDocument(document);
        }

        public async Task<OwnVehiclesDetails> UpdateVehicle(OwnVehiclesDetails vehicle)
        {
            return await repositoryWrapper.VehicleRepository.UpdateVehicle(vehicle);
        }

        public async Task<bool> CheckDuplicateVehicleChasisNo(string chasisNumber)
        {
            return await repositoryWrapper.VehicleRepository.CheckDuplicateVehicleChasisNo(chasisNumber);
        }

        public async Task<List<OwnVehiclesDetails>> GetAllVehicles()
        {
            return await repositoryWrapper.VehicleRepository.GetAllVehicles();
        }
    }
}