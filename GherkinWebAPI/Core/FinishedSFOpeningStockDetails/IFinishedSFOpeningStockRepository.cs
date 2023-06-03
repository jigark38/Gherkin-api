using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.GRPAndroid.ProdProcessBOM;
using GherkinWebAPI.Request.FinishedSFOpeningStockDetails;
using GherkinWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core
{
    public interface IFinishedSFOpeningStockRepository
    {
        Task<ApiResponse<List<OrganisationOfficeUnitDto>>> GetOrganisationOfficeUnitList();

        Task<ApiResponse<List<Area>>> GetHarvestAreaList();

        Task<ApiResponse<List<CountryOverseas>>> GetCountryOverSeasList();

        Task<ApiResponse<List<ConsigneeBuyersDetails>>> GetConsigneeBuyersList(string overseasCountryId);

        Task<ApiResponse<List<ProformaInvoiceDto>>> GetProformaInvoiceList(string cBCode);

        Task<ApiResponse<List<ProductGroup>>> GetFinishedProductGroupList();

        Task<ApiResponse<List<ProductDetails>>> GetFinishedProductDetailsList(string GrpCode);

        Task<ApiResponse<List<ProductionProcessDetails>>> GetProductionProcessDetailsList(string VarietyCode);

        Task<ApiResponse<List<MediaProcessDetails>>> GetMediaProcessDetailsList(string ProductionProcessCode);

        Task<ApiResponse<List<GradeDetails>>> GetFPGradesDetailsList(string VarietyCode);

        Task<ApiResponse<List<ContainerPackingDetails>>> GetContainerPackingDetailsList();

        Task<ApiResponse<List<GSCUomDetails>>> GetUOMDetailsList();

        Task<ApiResponse<object>> SaveFinishedSFOpeningStock(FinishedSFStockProductDetailsRequest finishedStkProdDetail);
        Task<ApiResponse<List<FinishedSFStockQuantityDetailsRequest>>> GetStockDetails(FinishedSFStockQuantityFindRequest finishedStkProdDetail);

        Task<ApiResponse<object>> UpdateStockDetals(FinishedSFStockQuantityDetails finishedQtyDetail);
        Task<ApiResponse<object>> DeleteStockDetals(int FSFStockQuantityNo);
    }
}
