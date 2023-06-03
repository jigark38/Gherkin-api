using GherkinWebAPI.Core;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.GRPAndroid.ProdProcessBOM;
using GherkinWebAPI.Request.FinishedSFOpeningStockDetails;
using GherkinWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service
{
    public class FinishedSFOpeningStockService : IFinishedSFOpeningStockService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public FinishedSFOpeningStockService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public async Task<ApiResponse<List<OrganisationOfficeUnitDto>>> GetOrganisationOfficeUnitList()
        {
            return await _repositoryWrapper.FinishedSFOpeningStockRepository.GetOrganisationOfficeUnitList();
        }

        public async Task<ApiResponse<List<Area>>> GetHarvestAreaList()
        {
            return await _repositoryWrapper.FinishedSFOpeningStockRepository.GetHarvestAreaList();
        }

        public async Task<ApiResponse<List<CountryOverseas>>> GetCountryOverSeasList()
        {
            return await _repositoryWrapper.FinishedSFOpeningStockRepository.GetCountryOverSeasList();
        }

        public async Task<ApiResponse<List<ConsigneeBuyersDetails>>> GetConsigneeBuyersList(string overseasCountryId)
        {
            return await _repositoryWrapper.FinishedSFOpeningStockRepository.GetConsigneeBuyersList(overseasCountryId);
        }

        public async Task<ApiResponse<List<ProformaInvoiceDto>>> GetProformaInvoiceList(string cBCode)
        {
            return await _repositoryWrapper.FinishedSFOpeningStockRepository.GetProformaInvoiceList(cBCode);
        }

        public async Task<ApiResponse<List<ProductGroup>>> GetFinishedProductGroupList()
        {
            return await _repositoryWrapper.FinishedSFOpeningStockRepository.GetFinishedProductGroupList();
        }

        public async Task<ApiResponse<List<ProductDetails>>> GetFinishedProductDetailsList(string GrpCode)
        {
            return await _repositoryWrapper.FinishedSFOpeningStockRepository.GetFinishedProductDetailsList(GrpCode);
        }

        public async Task<ApiResponse<List<ProductionProcessDetails>>> GetProductionProcessDetailsList(string VarietyCode)
        {
            return await _repositoryWrapper.FinishedSFOpeningStockRepository.GetProductionProcessDetailsList(VarietyCode);
        }

        public async Task<ApiResponse<List<MediaProcessDetails>>> GetMediaProcessDetailsList(string ProductionProcessCode)
        {
            return await _repositoryWrapper.FinishedSFOpeningStockRepository.GetMediaProcessDetailsList(ProductionProcessCode);
        }

        public async Task<ApiResponse<List<GradeDetails>>> GetFPGradesDetailsList(string VarietyCode)
        {
            return await _repositoryWrapper.FinishedSFOpeningStockRepository.GetFPGradesDetailsList(VarietyCode);
        }

        public async Task<ApiResponse<List<ContainerPackingDetails>>> GetContainerPackingDetailsList()
        {
            return await _repositoryWrapper.FinishedSFOpeningStockRepository.GetContainerPackingDetailsList();
        }

        public async Task<ApiResponse<List<GSCUomDetails>>> GetUOMDetailsList()
        {
            return await _repositoryWrapper.FinishedSFOpeningStockRepository.GetUOMDetailsList();
        }

        public async Task<ApiResponse<object>> SaveFinishedSFOpeningStock(FinishedSFStockProductDetailsRequest finishedStkProdDetail)
        {
            return await _repositoryWrapper.FinishedSFOpeningStockRepository.SaveFinishedSFOpeningStock(finishedStkProdDetail);
        }

        public async Task<ApiResponse<List<FinishedSFStockQuantityDetailsRequest>>> GetStockDetails(FinishedSFStockQuantityFindRequest finishedStkProdDetail)
        {
           return await _repositoryWrapper.FinishedSFOpeningStockRepository.GetStockDetails(finishedStkProdDetail);
        }

        public async Task<ApiResponse<object>> UpdateStockDetals(FinishedSFStockQuantityDetails finishedQtyDetail)
        {
            return await _repositoryWrapper.FinishedSFOpeningStockRepository.UpdateStockDetals(finishedQtyDetail);
        }

        public async Task<ApiResponse<object>> DeleteStockDetals(int FSFStockQuantityNo)
        {
            return await _repositoryWrapper.FinishedSFOpeningStockRepository.DeleteStockDetals(FSFStockQuantityNo);
        }
    }
}