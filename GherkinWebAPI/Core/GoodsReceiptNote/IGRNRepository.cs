using GherkinWebAPI.Models.GoodsReceiptNote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.GoodsReceiptNote
{
    public interface IGRNRepository
    {
        Task<int> GetGRNCode();
        Task<OrderDetail> CreateGRNDetail(OrderDetail orderDetail);
        Task<OrderDetail> GetGRNDetailByGRNCode(string GRNCode);
        Task<List<string>> GetGRNCodeBySupOrgId(string SupOrgId);
        Task<OrderDetail> UpdateGRNDetail(OrderDetail orderDetail);
        Task<BatchMaterialDetails> UpdateBatchMaterialDetails(BatchMaterialDetails batchMaterialDetails);
        Task<BatchMaterialDetails> GetBatchMaterialDetailsByBatchNo(string batchNo);
        
    }
}
