using GherkinWebAPI.Models.GoodsReceiptNote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.GoodsReceiptNote
{
    public interface IGRNMaterialRepository
    {
        Task<OrderDetail> CreateGRNMaterial(OrderDetail orderDetail);
        Task<List<OrderMaterialDetail>> GetGRNMaterialByGRNCode(string GRNCode);
        Task<OrderDetail> UpdateGRNMaterial(OrderDetail orderDetail);
    }
}
