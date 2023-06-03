using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GherkinWebAPI.DTO.MaterialInward;
using GherkinWebAPI.Models.GoodsReceiptNote;
using GherkinWebAPI.Models.MaterialOutward;
using GherkinWebAPI.Request.MaterialInward;

namespace GherkinWebAPI.Core.MaterialInward
{
    public interface IMaterialService
    {
        Task<MaterialInwardDto> InsertMaterialInward(InsertMaterialInwardRequest insertMaterialInwardRequest);
        Task<List<MaterialInwardDto>> FindMaterialInward(DateTime dateFrom, DateTime dateTo, string inwardType);
        Task<List<InwardDetail>> GetMaterialInwardDetailsAsync();
        Task<MaterialInwardDto> UpdateMaterialInward(int id, InsertMaterialInwardRequest materialInwardDto);
        Task<MaterialOutwardDetails> UpMaterialOutwardDetails(MaterialOutwardDetails materialOutwardDetails);
        Task<List<MaterialOutwardDetails>> GetAllMaterialOutwardDetails();
    }
}
