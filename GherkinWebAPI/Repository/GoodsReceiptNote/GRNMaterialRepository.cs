using GherkinWebAPI.Core.GoodsReceiptNote;
using GherkinWebAPI.Models.GoodsReceiptNote;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;

namespace GherkinWebAPI.Repository.GoodsReceiptNote
{
    public class GRNMaterialRepository : RepositoryBase<RMGRNMaterialDetail>, IGRNMaterialRepository
    {
        private RepositoryContext _context;
        public GRNMaterialRepository(RepositoryContext context) : base(context)
        {
            _context = context;
        }
        public async Task<OrderDetail> CreateGRNMaterial(OrderDetail orderDetail)
        {
            try
            {
                var grnMaterial = await _context.RMGRNMaterialDetails.SingleOrDefaultAsync(g => g.RMGRNNO == string.Concat("GRN/", orderDetail.RMGRNNo));

                if (grnMaterial == null)
                {
                    foreach (var orderMaterial in orderDetail.OrderMaterialDetails)
                    {
                        var rmGRNMaterial = new RMGRNMaterialDetail
                        {
                            RMGRNNO = string.Concat("GRN/", orderMaterial.RMGRNNO),
                            RMPONO = string.Concat("RM_PO_NO_", orderMaterial.RMPONO),
                            RM_GRN_Batch_No = orderMaterial.RMGRNBatchNo,
                            RawMaterialGroupCode = orderMaterial.RawMaterialGroupCode,
                            RawMaterialDetailsCode = orderMaterial.RawMaterialDetailsCode,
                            RMGRNBillQty = orderMaterial.RMGRNBillQty,
                            RMPORate = orderMaterial.RMPORate,
                            RMGRNBillRate = orderMaterial.RMGRNBillRate,
                            RMGRNMaterialWiseCost = orderMaterial.RMGRNMaterialWiseCost,
                            RMGRNIGSTRate = orderMaterial.RMGRNIGSTRate,
                            RMGRNIGSTAmount = orderMaterial.RMGRNIGSTAmount,
                            RMGRNCGSTRate = orderMaterial.RMGRNCGSTRate,
                            RMGRNCGSTAmount = orderMaterial.RMGRNCGSTAmount,
                            RMGRNSGSTRate = orderMaterial.RMGRNSGSTRate,
                            RMGRNSGSTAmount = orderMaterial.RMGRNSGSTAmount,
                            RMGRNMaterialWiseTotalCost = orderMaterial.RMGRNMaterialWiseTotalCost
                        };

                        _context.RMGRNMaterialDetails.Add(rmGRNMaterial);
                    }

                    var result = await _context.SaveChangesAsync();

                    if (result == 1)
                    {
                        return orderDetail;
                    }
                    else
                        return new OrderDetail();
                }
                else
                    return new OrderDetail();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<OrderMaterialDetail>> GetGRNMaterialByGRNCode(string GRNCode)
        {
            try
            {

                var test = await (from po in _context.PurchaseOrderDetails
                                  join RD in _context.RMGRNMaterialDetails on po.rmPoNo equals RD.RMPONO
                                  join rmpom in _context.RMPOMaterialDetails on new { a = RD.RMPONO, b = RD.RawMaterialGroupCode, c = RD.RawMaterialDetailsCode } equals new { a = rmpom.rmPoNo, b = rmpom.rawMaterialGroupCode, c = rmpom.rowMaterialDetailsCode }
                                  into rmgrnm1
                                  from rmgrnm2 in rmgrnm1.DefaultIfEmpty()
                                  let result = (from rmm in _context.RMMaterialTotalCostDetails
                                                group rmm by new { rmm.rawMaterialDetailsCode, rmm.rmGrnNo } into rmmg
                                                select new
                                                {
                                                    rmgrno = rmmg.Key.rmGrnNo,
                                                    raw = rmmg.Key.rawMaterialDetailsCode,
                                                    sum = rmmg.Sum(g => g.rmGrnReceivedQty)
                                                }).ToList()
                                  join s in _context.SupplierDetails on po.supplierOrgId equals s.supplierOrgID
                                  join p in _context.Places on s.placeCode equals p.PlaceCode
                                  join rawm in _context.RawMaterialDetails on rmgrnm2.rowMaterialDetailsCode equals rawm.Raw_Material_Details_Code
                                  where RD.RMGRNNO == GRNCode
                                  select new OrderMaterialDetail
                                  {
                                      Id = RD.Id,
                                      RMGRNNO = RD.RMGRNNO,
                                      RMGRNBatchNo = RD.RM_GRN_Batch_No,
                                      RMPONO = RD.RMPONO,
                                      RawMaterialGroupCode = RD.RawMaterialGroupCode,
                                      RawMaterialDetailsCode = RD.RawMaterialDetailsCode,
                                      RMGRNBillQty = RD.RMGRNBillQty,
                                      RMPORate = RD.RMPORate,
                                      RMGRNBillRate = RD.RMGRNBillRate,
                                      RMGRNMaterialWiseCost = RD.RMGRNMaterialWiseCost,
                                      RMGRNIGSTRate = RD.RMGRNIGSTRate,
                                      RMGRNIGSTAmount = RD.RMGRNIGSTAmount,
                                      RMGRNCGSTRate = RD.RMGRNCGSTRate,
                                      RMGRNCGSTAmount = RD.RMGRNCGSTAmount,
                                      RMGRNSGSTRate = RD.RMGRNSGSTRate,
                                      RMGRNSGSTAmount = RD.RMGRNSGSTAmount,
                                      RMOrderQty = rmgrnm2.rmOrderQty,
                                      TillNowRecordQuantity = result.Where(y => y.rmgrno == RD.RMGRNNO && y.raw == rmgrnm2.rowMaterialDetailsCode).FirstOrDefault().sum,
                                      RMGRNMaterialWiseTotalCost = RD.RMGRNMaterialWiseTotalCost,
                                      SupplierOrganisationName = s.organisationName,
                                      RawMaterialDetaisName = rawm.Raw_Material_Details_Name,
                                      RMPOMaterialCGSTRate = rmgrnm2.rmPoMaterialCGSTRate,
                                      RMPOMaterialIGSTRate = rmgrnm2.rmPoMaterialIGSTRate,
                                      RMPOMaterialSGSRate = rmgrnm2.rmPoMaterialSGSTRate,
                                      PODate = po.rmPoDate
                                  }).Distinct().ToListAsync();




                var response = await (from RD in _context.RMGRNMaterialDetails
                                      join PO in _context.PurchaseOrderDetails on RD.RMPONO equals PO.rmPoNo
                                      join rmpom in _context.RMPOMaterialDetails on PO.rmPoNo equals rmpom.rmPoNo
                                      let result = (from rmm in _context.RMMaterialTotalCostDetails
                                                    group rmm by new { rmm.rawMaterialDetailsCode, rmm.rmGrnNo } into rmmg
                                                    select new
                                                    {
                                                        rmgrno = rmmg.Key.rmGrnNo,
                                                        raw = rmmg.Key.rawMaterialDetailsCode,
                                                        sum = rmmg.Sum(g => g.rmGrnReceivedQty)
                                                    }).ToList()
                                      where RD.RMGRNNO == GRNCode
                                      select new OrderMaterialDetail
                                      {
                                          Id = RD.Id,
                                          RMGRNNO = RD.RMGRNNO,
                                          RMPONO = RD.RMPONO,
                                          RawMaterialGroupCode = RD.RawMaterialGroupCode,
                                          RawMaterialDetailsCode = RD.RawMaterialDetailsCode,
                                          RMGRNBillQty = RD.RMGRNBillQty,
                                          RMPORate = RD.RMPORate,
                                          RMGRNBillRate = RD.RMGRNBillRate,
                                          RMGRNMaterialWiseCost = RD.RMGRNMaterialWiseCost,
                                          RMGRNIGSTRate = RD.RMGRNIGSTRate,
                                          RMGRNIGSTAmount = RD.RMGRNIGSTAmount,
                                          RMGRNCGSTRate = RD.RMGRNCGSTRate,
                                          RMGRNCGSTAmount = RD.RMGRNCGSTAmount,
                                          RMGRNSGSTRate = RD.RMGRNSGSTRate,
                                          RMGRNSGSTAmount = RD.RMGRNSGSTAmount,
                                          RMGRNMaterialWiseTotalCost = RD.RMGRNMaterialWiseTotalCost,

                                      }).ToListAsync();
                return test;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<OrderDetail> UpdateGRNMaterial(OrderDetail orderDetail)
        {
            try
            {
                foreach (var orderMaterial in orderDetail.OrderMaterialDetails)
                {
                    var rmGRNMaterial = new RMGRNMaterialDetail
                    {
                        Id = orderMaterial.Id,
                        RMGRNNO = orderMaterial.RMGRNNO,
                        RMPONO = orderMaterial.RMPONO,
                        RM_GRN_Batch_No = orderMaterial.RMGRNBatchNo,
                        RawMaterialGroupCode = orderMaterial.RawMaterialGroupCode,
                        RawMaterialDetailsCode = orderMaterial.RawMaterialDetailsCode,
                        RMGRNBillQty = orderMaterial.RMGRNBillQty,
                        RMPORate = orderMaterial.RMPORate,
                        RMGRNBillRate = orderMaterial.RMGRNBillRate,
                        RMGRNMaterialWiseCost = orderMaterial.RMGRNMaterialWiseCost,
                        RMGRNIGSTRate = orderMaterial.RMGRNIGSTRate,
                        RMGRNIGSTAmount = orderMaterial.RMGRNIGSTAmount,
                        RMGRNCGSTRate = orderMaterial.RMGRNCGSTRate,
                        RMGRNCGSTAmount = orderMaterial.RMGRNCGSTAmount,
                        RMGRNSGSTRate = orderMaterial.RMGRNSGSTRate,
                        RMGRNSGSTAmount = orderMaterial.RMGRNSGSTAmount,
                        RMGRNMaterialWiseTotalCost = orderMaterial.RMGRNMaterialWiseTotalCost
                    };

                    _context.RMGRNMaterialDetails.AddOrUpdate(rmGRNMaterial);
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

