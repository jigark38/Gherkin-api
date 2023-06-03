
using GherkinWebAPI.DTO.GRNAndMaterialClassification;
using GherkinWebAPI.Models.GRNAndMaterialClassification;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.GRNAndMaterialClassification
{
    public interface IHarvestGRNRepository
    {
        Task<List<GreensReceivedDetail>> GetGreensReceivedDetailsAsync(string areaId);
        Task<HarvestGRNMaterialDetail> CreateHarvestGRNDetail(HarvestGRNMaterialDetail materialDetail);
        Task<long> GetHarvestGRNNo();
        Task<long> GetNextHarvestGRNNo();
        Task<GreensRecievedDetailDTO> GetGreensReceivedDetailsAsync(string areaId, string supervisorId);
        Task<CWHarvestGRNWeightSummaryDetails> CompleteBuyer(long HarvestGRNNo, int GreensProcurementNo, string BuyerEmployerId);
        Task<CWHarvestGRNCountWeightDetails> AddBuyerQuantityCratewiseDetail(HarvestBuyerWeighingDetailsDTO harvestBuyerWeighingDetailsDTO);
        Task<HarvestGRN> CompleteHarvestGrn(HarvestGRN harvestGRN);
        Task<List<VehicleDetails>> GetVehicles();
        Task<List<GreensRecievedDetailDTO>> GetAllGreensReceivedDetailsList();
    }
}
