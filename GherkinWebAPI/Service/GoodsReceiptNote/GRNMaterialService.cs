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
    public class GRNMaterialService : IGRNMaterialService
    {
        private readonly IRepositoryWrapper _repository;

        public GRNMaterialService(IRepositoryWrapper repository)
        {
            _repository = repository;
        }
        public async Task<OrderDetail> CreateGRNMaterial(OrderDetail orderDetail)
        {
            return await _repository.GRNMaterialRepository.CreateGRNMaterial(orderDetail);
        }
        public async Task<List<OrderMaterialDetail>> GetGRNMaterialByGRNCode(string GRNCode)
        {
            return await _repository.GRNMaterialRepository.GetGRNMaterialByGRNCode(GRNCode);
        }
        public async Task<OrderDetail> UpdateGRNMaterial(OrderDetail orderDetail)
        {
            return await _repository.GRNMaterialRepository.UpdateGRNMaterial(orderDetail);
        }
    }
}