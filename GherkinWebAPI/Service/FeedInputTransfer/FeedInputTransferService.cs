using GherkinWebAPI.Core.FeedInputTransfer;
using GherkinWebAPI.DTO;
using GherkinWebAPI.DTO.FeedInputTransfer;
using GherkinWebAPI.Models.InputTransferDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service.FeedInputTransfer
{
    public class FeedInputTransferService : IFeedInputTransferService
    {
        private readonly IFeedInputTransferRepository _repository;

        public FeedInputTransferService(IFeedInputTransferRepository feedInputTransferRepository)
        {
            this._repository = feedInputTransferRepository;
        }

        public async Task<List<HBOMDetailsDto>> GetTranferDetails(string cropNameCode, string cropSchemeCode, string psNumber, string areaId)
        {
            return await _repository.GetTranferDetails(cropNameCode, cropSchemeCode, psNumber, areaId);
        }
        public async Task<decimal> GetFarmersNoOfAcres(string cropNameCode, string psNumber)
        {
            return await _repository.GetFarmersNoOfAcres(cropNameCode, psNumber);
        }
        public async Task<List<string>> GetHBOMPracticePerAcreage(string cropNameCode, string psNumber)
        {
            return await _repository.GetHBOMPracticePerAcreage(cropNameCode, psNumber);
        }
        public async Task<List<string>> GetHBOMPracticeNo(string cropNameCode, string psNumber)
        {
            return await _repository.GetHBOMPracticeNo(cropNameCode, psNumber);
        }

        public async Task<StockAndBatchDetail> GetAllStockAndBatchDetails(string groupCode, string detailsCode, DateTime transferDate)
        {
            return await _repository.GetAllStockAndBatchDetails(groupCode, detailsCode, transferDate);
        }

        public async Task<SaveStockAndBatchDetail> SaveStockAndBatchDetails(SaveStockAndBatchDetail saveStockAndBatchDetails)
        {
            return await _repository.SaveStockAndBatchDetail(saveStockAndBatchDetails);
        }

        public async Task<string> GenerateTransferNoPK()
        {
            return await _repository.GenerateTransferNoPK();
        }

        public async Task<string> GetOutwardGatePassNo()
        {
            return await _repository.GetOutwardGatePassNo();
        }

        public async Task<List<HBOMDetailsDto>> GetDetailsByCropAndPsNumber(string cropNameCode, string psNumber)
        {
            return await _repository.GetDetailsByCropAndPsNumber(cropNameCode, psNumber);
        }

        public async Task<List<OrganisationOfficeLocationDetailsDto>> GetOrgofficelocationDetails()
        {
            return await _repository.GetOrgofficelocationDetails();
        }

        public async Task<List<CropDetailsByGroupCode>> GetCropDetailsByCode(string cropGroupCode)
        {
            return await _repository.GetCropDetailsByCode(cropGroupCode);
        }
    }
}