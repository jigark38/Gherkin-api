using GherkinWebAPI.Core;
using GherkinWebAPI.Core.GRNAndMaterialClassification;
using GherkinWebAPI.DTO.GRNAndMaterialClassification;
using GherkinWebAPI.Models.GRNAndMaterialClassification;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Service.GRNAndMaterialClassification
{
    public class HarvestGRNService : IHarvestGRNService
    {
        private readonly IRepositoryWrapper _repository;

        public HarvestGRNService(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        public async Task<CWHarvestGRNCountWeightDetails> AddBuyerQuantityCratewiseDetail(HarvestBuyerWeighingDetailsDTO harvestBuyerWeighingDetailsDTO)
        {
            return await _repository.HarvestGRNRepository.AddBuyerQuantityCratewiseDetail(harvestBuyerWeighingDetailsDTO);
        }

        public async Task<CWHarvestGRNWeightSummaryDetails> CompleteBuyer(long HarvestGRNNo, int GreensProcurementNo, string BuyerEmployerId)
        {
            return await _repository.HarvestGRNRepository.CompleteBuyer(HarvestGRNNo, GreensProcurementNo, BuyerEmployerId);
        }

        public async Task<HarvestGRN> CompleteHarvestGrn(HarvestGRN harvestGRN)
        {
            return await _repository.HarvestGRNRepository.CompleteHarvestGrn(harvestGRN);
        }

        public async Task<HarvestGRNMaterialDetail> CreateHarvestGRNDetail(HarvestGRNMaterialDetail materialDetail)
        {
            return await _repository.HarvestGRNRepository.CreateHarvestGRNDetail(materialDetail);
        }

        public async Task<List<GreensRecievedDetailDTO>> GetAllGreensReceivedDetailsList()
        {
             return await _repository.HarvestGRNRepository.GetAllGreensReceivedDetailsList();
        }

        public async Task<List<GreensReceivedDetail>> GetGreensReceivedDetailsAsync(string areaId)
        {
            return await _repository.HarvestGRNRepository.GetGreensReceivedDetailsAsync(areaId);
        }

        public async Task<GreensRecievedDetailDTO> GetGreensReceivedDetailsAsync(string areaId, string supervisorId)
        {
            return await _repository.HarvestGRNRepository.GetGreensReceivedDetailsAsync(areaId, supervisorId);
        }

        public async Task<long> GetHarvestGRNNo()
        {
            return await _repository.HarvestGRNRepository.GetHarvestGRNNo();
        }

        public async Task<long> GetNextHarvestGRNNo()
        {
            return await _repository.HarvestGRNRepository.GetNextHarvestGRNNo();
        }

        public async Task<List<VehicleDetails>> GetVehicles()
        {
            return await _repository.HarvestGRNRepository.GetVehicles();
        }
        
    }
}