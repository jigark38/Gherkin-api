using GherkinWebAPI.Core;
using GherkinWebAPI.Core.GoodsReceiptNote;
using GherkinWebAPI.Models.GoodsReceiptNote;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Service.GoodsReceiptNote
{
    public class GRNService : IGRNService
    {
        private readonly IRepositoryWrapper _repository;

        public GRNService(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        public async Task<OrderDetail> CreateGRNDetail(OrderDetail orderDetail)
        {
            return await _repository.GRNReposiory.CreateGRNDetail(orderDetail);
        }

        public async Task<int> GetGRNCode()
        {
            return await _repository.GRNReposiory.GetGRNCode();
        }

        public async Task<OrderDetail> GetGRNDetailByGRNCode(string GRNCode)
        {
            return await _repository.GRNReposiory.GetGRNDetailByGRNCode(GRNCode);
        }

        public async Task<List<string>> GetGRNCodeBySupOrgId(string SupOrgId)
        {
            return await _repository.GRNReposiory.GetGRNCodeBySupOrgId(SupOrgId);
        }
        public async Task<OrderDetail> UpdateGRNDetail(OrderDetail orderDetail)
        {
            return await _repository.GRNReposiory.UpdateGRNDetail(orderDetail);
        }

        public async Task<BatchMaterialDetails> UpdateBatchMaterialDetails(BatchMaterialDetails batchMaterialDetails)
        {
            return await _repository.GRNReposiory.UpdateBatchMaterialDetails(batchMaterialDetails);
        }

        public async Task<BatchMaterialDetails> GetBatchMaterialDetailsByBatchNo(string batchNo)
        {
            return await _repository.GRNReposiory.GetBatchMaterialDetailsByBatchNo(batchNo);
        }
    }
}