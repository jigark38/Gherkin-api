using GherkinWebAPI.Core;
using GherkinWebAPI.Core.GoodsReceiptNote;
using GherkinWebAPI.Models.GoodsReceiptNote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service.GoodsReceiptNote
{
    public class MaterialTotalCostService : IMaterialTotalCostService
    {
        private readonly IRepositoryWrapper _repository;
        public MaterialTotalCostService(IRepositoryWrapper repository)
        {
            _repository = repository;
        }
        public async Task<OrderDetail> CreateMaterialTotalCost(OrderDetail orderDetail)
        {
            return await _repository.MaterialTotalCostRepository.CreateMaterialTotalCost(orderDetail);
        }
        public async Task<List<OrderMaterialTotalCostDetail>> GetMaterialTotalCostByGRNCode(string GRNCode) {
            return await _repository.MaterialTotalCostRepository.GetMaterialTotalCostByGRNCode(GRNCode);
        }
        public async Task<OrderDetail> UpdateMaterialTotalCost(OrderDetail orderDetail)
        {
            return await _repository.MaterialTotalCostRepository.UpdateMaterialTotalCost(orderDetail);
        }
    }
}