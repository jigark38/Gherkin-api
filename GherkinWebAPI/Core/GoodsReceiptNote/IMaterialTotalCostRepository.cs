using GherkinWebAPI.Models.GoodsReceiptNote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.GoodsReceiptNote
{
    public interface IMaterialTotalCostRepository
    {
        Task<OrderDetail> CreateMaterialTotalCost(OrderDetail orderDetail);
        Task<List<OrderMaterialTotalCostDetail>> GetMaterialTotalCostByGRNCode(string GRNCode);
        Task<OrderDetail> UpdateMaterialTotalCost(OrderDetail orderDetail);
    }
}
