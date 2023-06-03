using GherkinWebAPI.DTO;
using GherkinWebAPI.DTO.FeedInputTransfer;
using GherkinWebAPI.Models.InputTransferDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.FeedInputTransfer
{
    public interface IFeedInputTransferRepository
    {
        Task<List<HBOMDetailsDto>> GetTranferDetails(string cropNameCode, string cropSchemeCode, string psNumber, string areaId);
        Task<decimal> GetFarmersNoOfAcres(string cropNameCode, string psNumber);
        Task<List<string>> GetHBOMPracticePerAcreage(string cropNameCode, string psNumber);
        Task<List<string>> GetHBOMPracticeNo(string cropNameCode, string psNumber);
        Task<StockAndBatchDetail> GetAllStockAndBatchDetails(string groupCode, string detailsCode, DateTime transferDate);
        Task<SaveStockAndBatchDetail> SaveStockAndBatchDetail(SaveStockAndBatchDetail saveStockAndBatchDetails);
        Task<string> GenerateTransferNoPK();
        Task<string> GetOutwardGatePassNo();
        Task<List<HBOMDetailsDto>> GetDetailsByCropAndPsNumber(string cropNameCode, string psNumber);
        Task<List<OrganisationOfficeLocationDetailsDto>> GetOrgofficelocationDetails();
        Task<List<CropDetailsByGroupCode>> GetCropDetailsByCode(string cropGroupCode);
    }
}
