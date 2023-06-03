using GherkinWebAPI.Core.MaterialInward;
using GherkinWebAPI.DTO.MaterialInward;
using GherkinWebAPI.Models.GoodsReceiptNote;
using GherkinWebAPI.Models.MaterialOutward;
using GherkinWebAPI.Request.MaterialInward;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service.MaterialInward
{
    public class MaterialInwardService : IMaterialService
    {
        private readonly IMaterialRepository _materialRepository;
        public MaterialInwardService(IMaterialRepository _materialRepository)
        {
            this._materialRepository = _materialRepository;
        }

        public async Task<MaterialOutwardDetails> UpMaterialOutwardDetails(MaterialOutwardDetails materialOutwardDetails)
        {
            return await _materialRepository.UpMaterialOutwardDetails(materialOutwardDetails);
        }

        public async Task<List<MaterialInwardDto>> FindMaterialInward(DateTime dateFrom, DateTime dateTo, string inwardType)
        {
            return await _materialRepository.FindMaterialInward(dateFrom, dateTo,inwardType);
        }
        
        public  async Task<List<MaterialOutwardDetails>> GetAllMaterialOutwardDetails()
        {
            return await _materialRepository.GetAllMaterialOutwardDetails();
        }

        public async Task<List<InwardDetail>> GetMaterialInwardDetailsAsync()
        {
            return await _materialRepository.GetMaterialInwardDetailsAsync();
        }

        public async Task<MaterialInwardDto> InsertMaterialInward(InsertMaterialInwardRequest insertMaterialInwardRequest)
        {
            return await _materialRepository.InsertMaterialInward(insertMaterialInwardRequest);
        }

        public async Task<MaterialInwardDto> UpdateMaterialInward(int id, InsertMaterialInwardRequest materialInwardDto)
        {
            return await _materialRepository.UpdateMaterialInward(id, materialInwardDto);
        }
    }
}