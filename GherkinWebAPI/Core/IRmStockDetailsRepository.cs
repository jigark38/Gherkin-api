using GherkinWebAPI.DTO;
using GherkinWebAPI.DTO.RMStock;
using GherkinWebAPI.Request.RMStock;
using GherkinWebAPI.Response.RMStock;
using GherkinWebAPI.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core
{
    public interface IRmStockDetailsRepository
    {
        Task<ApiResponse<RMStockFormInsertResponse>> InsertRMStockDetails(RMStockInsertRequest rMStockInsertRequest);
        Task<RMStockBranchInsertResponse> InsertRMStockBranchDetails(RMStockBranchInsertRequest rMStockBranchInsertRequest);
        Task<List<RMStockBranchQuantityDetailDTO>> FindRMStockBranchDetails(string areaId);
        Task UpdateRMStockBranchDetails(List<RMStockBranchQuantityDetailDTO> rMBranchUpdateDetailsModel);
        Task<ApiResponse<RMStockDetailsDto>> FIndRMStockDetail(string Org_office_No, string Raw_Material_Group_Code, string Raw_Material_Details_Code);
        Task<ApiResponse<object>> UpdateRMStockDetail(RMStockDetailsDto newRMStockDetailsDtoItem);
    }
}
