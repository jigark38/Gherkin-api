using GherkinWebAPI.Core.GoodsReceiptNote;
using GherkinWebAPI.Models.PurchageMgmt;
using GherkinWebAPI.Models.GoodsReceiptNote;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity.Migrations;

namespace GherkinWebAPI.Repository.GoodsReceiptNote
{
    public class MaterialTotalCostRepository : RepositoryBase<RMMaterialTotalCostDetail>, IMaterialTotalCostRepository
    {
        private RepositoryContext _context;
        public MaterialTotalCostRepository(RepositoryContext context) : base(context)
        {
            _context = context;
        }
        public async Task<OrderDetail> CreateMaterialTotalCost(OrderDetail orderDetail)
        {
            try
            {
                foreach (var orderMaterial in orderDetail.OrderMaterialTotalCostDetails)
                {

                    var rmMaterialTotalCost = new RMMaterialTotalCostDetail
                    {
                        rmGrnNo = string.Concat("GRN/", orderMaterial.RMGRNNO),
                        rawMaterialGroupCode = orderMaterial.RawMaterialGroupCode,
                        rawMaterialDetailsCode = orderMaterial.RawMaterialDetailsCode,
                        rmBatchNo = orderMaterial.RMBatchNo,
                        rmGrnReceivedQty = orderMaterial.RMGRNReceivedQty,
                        rmGrnMaterialWiseTotalCost = orderMaterial.RMGRNMaterialWiseTotalCost,
                        rmCustomsShareAmount = orderMaterial.RMCustomsShareAmount,
                        rmPackingShareAmount = orderMaterial.RMPackingShareAmount,
                        rmFreightShareAmount = orderMaterial.RMFreightShareAmount,
                        rmInsuranceShareAmount = orderMaterial.RMInsuranceShareAmount,
                        rmGrnMaterialwiseTotalCost = orderMaterial.RMGRNMaterialwiseTotalCost,
                        rmGrnMaterialwiseTotalRate = orderMaterial.RMGRNMaterialwiseTotalRate
                    };

                    _context.RMMaterialTotalCostDetails.Add(rmMaterialTotalCost);
                }
                var result = await _context.SaveChangesAsync();

                if (result == 1)
                {
                    return orderDetail;
                }
                else
                    return new OrderDetail();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<OrderMaterialTotalCostDetail>> GetMaterialTotalCostByGRNCode(string GRNCode)
        {
            try
            {
                var response = await (from RD in _context.RMMaterialTotalCostDetails
                                      where RD.rmGrnNo == GRNCode
                                      select new OrderMaterialTotalCostDetail
                                      {
                                          Id = RD.Id,
                                          RMGRNNO = RD.rmGrnNo,
                                          RawMaterialGroupCode = RD.rawMaterialGroupCode,
                                          RawMaterialDetailsCode = RD.rawMaterialDetailsCode,
                                          RMBatchNo = RD.rmBatchNo,
                                          RMGRNReceivedQty = RD.rmGrnReceivedQty,
                                          RMGRNMaterialWiseTotalCost = RD.rmGrnMaterialWiseTotalCost,
                                          RMCustomsShareAmount = RD.rmCustomsShareAmount,
                                          RMPackingShareAmount = RD.rmPackingShareAmount,
                                          RMFreightShareAmount = RD.rmFreightShareAmount,
                                          RMInsuranceShareAmount = RD.rmInsuranceShareAmount,
                                          RMGRNMaterialwiseTotalCost = RD.rmGrnMaterialwiseTotalCost,
                                          RMGRNMaterialwiseTotalRate = RD.rmGrnMaterialwiseTotalRate
                                      }).ToListAsync();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<OrderDetail> UpdateMaterialTotalCost(OrderDetail orderDetail)
        {
            try
            {
                foreach (var orderMaterial in orderDetail.OrderMaterialTotalCostDetails)
                {

                    var rmMaterialTotalCost = new RMMaterialTotalCostDetail
                    {
                        Id = orderMaterial.Id,
                        rmGrnNo = orderMaterial.RMGRNNO,
                        rawMaterialGroupCode = orderMaterial.RawMaterialGroupCode,
                        rawMaterialDetailsCode = orderMaterial.RawMaterialDetailsCode,
                        rmBatchNo = orderMaterial.RMBatchNo,
                        rmGrnReceivedQty = orderMaterial.RMGRNReceivedQty,
                        rmGrnMaterialWiseTotalCost = orderMaterial.RMGRNMaterialWiseTotalCost,
                        rmCustomsShareAmount = orderMaterial.RMCustomsShareAmount,
                        rmPackingShareAmount = orderMaterial.RMPackingShareAmount,
                        rmFreightShareAmount = orderMaterial.RMFreightShareAmount,
                        rmInsuranceShareAmount = orderMaterial.RMInsuranceShareAmount,
                        rmGrnMaterialwiseTotalCost = orderMaterial.RMGRNMaterialwiseTotalCost,
                        rmGrnMaterialwiseTotalRate = orderMaterial.RMGRNMaterialwiseTotalRate
                    };

                    _context.RMMaterialTotalCostDetails.AddOrUpdate(rmMaterialTotalCost);
                }
                var result = await _context.SaveChangesAsync();

                if (result == 1)
                {
                    return orderDetail;
                }
                else
                    return new OrderDetail();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
