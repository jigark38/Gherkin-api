using System.Collections.Generic;
using System.Threading.Tasks;
using GherkinWebAPI.Core;
using GherkinWebAPI.Core.Harvest_Area;
using GherkinWebAPI.DTO;
using GherkinWebAPI.DTO.RMStock;
using GherkinWebAPI.Request.RMStock;
using GherkinWebAPI.Response.RMStock;
using GherkinWebAPI.Utilities;

namespace GherkinWebAPI.Service
{
    public class RMStockService : IRMStockService
    {
        private IRawMaterialRepository _rawMaterialRepository { get; }
        private IOrganisationOfficeLocationDetialsRepository _organisationOfficeLocationDetialsRepository { get; }

        private IHarvestAreaRepository harvestAreaRepository { get; }

        private IRmStockDetailsRepository _rmStockDetailsRepository { get; }

        public RMStockService(IHarvestAreaRepository harvestAreaRepository, IRawMaterialRepository rawMaterialRepository, IOrganisationOfficeLocationDetialsRepository organisationOfficeLocationDetialsRepository, IRmStockDetailsRepository rmStockDetailsRepository)
        {
            this.harvestAreaRepository = harvestAreaRepository;
            this._rawMaterialRepository = rawMaterialRepository;
            this._organisationOfficeLocationDetialsRepository = organisationOfficeLocationDetialsRepository;
            this._rmStockDetailsRepository = rmStockDetailsRepository;
        }

        public async Task<RMStockFormShowResponse> GetRMStockFormData()
        {
            RMStockFormShowResponse rMStockFormShowResponse = new RMStockFormShowResponse();
            rMStockFormShowResponse.RawMaterialMasters = await _rawMaterialRepository.GetRawMaterialMaster();
            rMStockFormShowResponse.RawMaterialDetails = await _rawMaterialRepository.GetRawmaterialDetails();
            rMStockFormShowResponse.OrganisationOfficeLocationDetails = await _organisationOfficeLocationDetialsRepository.GetOrganisationOfficeLocationDetials();
            return rMStockFormShowResponse;
        }

        public async Task<ApiResponse<RMStockFormInsertResponse>> InsertRMStockDetails(RMStockInsertRequest rMStockInsertRequest)
        {
            return await _rmStockDetailsRepository.InsertRMStockDetails(rMStockInsertRequest);
        }

        public async Task<RMBranchShowFormResponse> GetRMBranchDataForForm()
        {
            RMBranchShowFormResponse rMBranchShowFormResponse = new RMBranchShowFormResponse();
            rMBranchShowFormResponse.RawMaterialMasters = await _rawMaterialRepository.GetRawMaterialMaster();
            rMBranchShowFormResponse.RawMaterialDetails = await _rawMaterialRepository.GetRawmaterialDetails();
            rMBranchShowFormResponse.Area = await harvestAreaRepository.GetAreaNameAndCodeAsync();
            return rMBranchShowFormResponse;
        }

        public async Task<RMStockBranchInsertResponse> InsertRMStockBranchDetails(RMStockBranchInsertRequest rMStockBranchInsertRequest)
        {
            RMStockBranchInsertResponse rMStockBranchInsertResponse = await _rmStockDetailsRepository.InsertRMStockBranchDetails(rMStockBranchInsertRequest);
            return rMStockBranchInsertResponse;
        }

        public async Task<List<RMStockBranchQuantityDetailDTO>> FindRMStockBranchDetails(string areaId)
        {
            List<RMStockBranchQuantityDetailDTO> result = await _rmStockDetailsRepository.FindRMStockBranchDetails(areaId);
            return result;
        }

        public async Task UpdateRMStockBranchDetails(List<RMStockBranchQuantityDetailDTO> rMBranchUpdateDetailsModel)
        {
            await _rmStockDetailsRepository.UpdateRMStockBranchDetails(rMBranchUpdateDetailsModel);
        }

        public async Task<ApiResponse<RMStockDetailsDto>> FIndRMStockDetail(string Org_office_No, string Raw_Material_Group_Code, string Raw_Material_Details_Code)
        {
           return await _rmStockDetailsRepository.FIndRMStockDetail(Org_office_No, Raw_Material_Group_Code, Raw_Material_Details_Code);
        }
        public async Task<ApiResponse<object>> UpdateRMStockDetail(RMStockDetailsDto newRMStockDetailsDtoItem)
        {
            return await _rmStockDetailsRepository.UpdateRMStockDetail(newRMStockDetailsDtoItem);
        }
    }
}