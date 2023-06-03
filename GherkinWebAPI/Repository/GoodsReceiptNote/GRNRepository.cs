using GherkinWebAPI.Core.GoodsReceiptNote;
using GherkinWebAPI.Models.GoodsReceiptNote;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Repository.GoodsReceiptNote
{
    public class GRNRepository : RepositoryBase<RMGRNDetail>, IGRNRepository
    {
        private RepositoryContext _context;

        public GRNRepository(RepositoryContext context) : base(context)
        {
            _context = context;
        }

        public async Task<OrderDetail> CreateGRNDetail(OrderDetail orderDetail)
        {
            try
            {
                var grnDetail = await _context.RMGRNDetails.SingleOrDefaultAsync(g => g.RMGRNNo == orderDetail.RMGRNNo);

                if (grnDetail == null)
                {
                    var rmGRNDetail = new RMGRNDetail
                    {
                        RMGRNDate = orderDetail.RMGRNDate,
                        RMGRNNo = string.Concat("GRN/", orderDetail.RMGRNNo),
                        InwardGatePassNo = string.Concat("IGP_", orderDetail.InwardGatePassNo),
                        DomesticImport = orderDetail.DomesticImport,
                        SupplierOrgID = orderDetail.SupplierOrgID,
                        Currency = orderDetail.Currency,
                        TotalMaterialCost = orderDetail.TotalMaterialCost,
                        GSTType = orderDetail.GSTType,
                        TotalTaxAmount = orderDetail.TotalTaxAmount,
                        TotalMaterialCostAndTaxAmount = orderDetail.TotalMaterialCostAndTaxAmount,
                        CustomsAmount = orderDetail.CustomsAmount,
                        PackingAmount = orderDetail.PackingAmount,
                        PackingTaxRatePercentage = orderDetail.PackingTaxRatePercentage,
                        PackingTaxAmount = orderDetail.PackingTaxAmount,
                        FreightAmount = orderDetail.FreightAmount,
                        FreightTaxRatePercentage = orderDetail.FreightTaxRatePercentage,
                        FreightTaxAmount = orderDetail.FreightTaxAmount,
                        InsuranceAmount = orderDetail.InsuranceAmount,
                        InsuranceTaxRatePercentage = orderDetail.InsuranceTaxRatePercentage,
                        InsuranceTaxAmount = orderDetail.InsuranceTaxAmount,
                        TotalBillAmount = orderDetail.TotalBillAmount,
                        AdvancePayment = orderDetail.AdvancePayment,
                        BalanceAmount = orderDetail.BalanceAmount,
                        CreditDays = orderDetail.CreditDays,
                        InvoiceDCType = orderDetail.InvoiceDCType,
                        BillDCDate = orderDetail.BillDCDate,
                        BillDCNo = orderDetail.BillDCNo,
                        PackingHSNCode = orderDetail.PackingHSNCode,
                        FreightHSNCode = orderDetail.FreightHSNCode,
                        InsuranceHSNCode = orderDetail.InsuranceHSNCode,
                        DiscountPercentage = orderDetail.DiscountPercentage,
                        DiscountAmount = orderDetail.DiscountAmount
                    };

                    _context.RMGRNDetails.Add(rmGRNDetail);

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

        public async Task<int> GetGRNCode()
        {
            try
            {
                var grns = await _context.RMGRNDetails.AsNoTracking().ToListAsync();
                if (grns.Count > 0)
                {
                    return grns.Count + 1;
                }
                else
                {
                    return 1;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<OrderDetail> GetGRNDetailByGRNCode(string GRNCode)
        {
            try
            {
                var response = await (from GRN in _context.RMGRNDetails
                                      where GRN.RMGRNNo == GRNCode
                                      select new OrderDetail
                                      {
                                          RMGRNDate = GRN.RMGRNDate,
                                          RMGRNNo = GRN.RMGRNNo,
                                          InwardGatePassNo = GRN.InwardGatePassNo,
                                          DomesticImport = GRN.DomesticImport,
                                          SupplierOrgID = GRN.SupplierOrgID,
                                          Currency = GRN.Currency,
                                          TotalMaterialCost = GRN.TotalMaterialCost,
                                          GSTType = GRN.GSTType,
                                          TotalTaxAmount = GRN.TotalTaxAmount,
                                          TotalMaterialCostAndTaxAmount = GRN.TotalMaterialCostAndTaxAmount,
                                          CustomsAmount = GRN.CustomsAmount,
                                          PackingAmount = GRN.PackingAmount,
                                          PackingTaxRatePercentage = GRN.PackingTaxRatePercentage,
                                          PackingTaxAmount = GRN.PackingTaxAmount,
                                          FreightAmount = GRN.FreightAmount,
                                          FreightTaxRatePercentage = GRN.FreightTaxRatePercentage,
                                          FreightTaxAmount = GRN.FreightTaxAmount,
                                          InsuranceAmount = GRN.InsuranceAmount,
                                          InsuranceTaxRatePercentage = GRN.InsuranceTaxRatePercentage,
                                          InsuranceTaxAmount = GRN.InsuranceTaxAmount,
                                          TotalBillAmount = GRN.TotalBillAmount,
                                          AdvancePayment = GRN.AdvancePayment,
                                          BalanceAmount = GRN.BalanceAmount,
                                          CreditDays = GRN.CreditDays,
                                          InvoiceDCType = GRN.InvoiceDCType,
                                          BillDCDate = GRN.BillDCDate,
                                          BillDCNo = GRN.BillDCNo,
                                          PackingHSNCode = GRN.PackingHSNCode,
                                          FreightHSNCode = GRN.FreightHSNCode,
                                          InsuranceHSNCode = GRN.InsuranceHSNCode,
                                          DiscountPercentage = GRN.DiscountPercentage,
                                          DiscountAmount = GRN.DiscountAmount
                                      }).SingleOrDefaultAsync();
                return response;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<string>> GetGRNCodeBySupOrgId(string SupOrgId)
        {
            try
            {
                return await _context.RMGRNDetails.Where(e => e.SupplierOrgID == SupOrgId).Select(x => x.RMGRNNo).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<OrderDetail> UpdateGRNDetail(OrderDetail orderDetail)
        {
            try
            {
                var grnDetail = await _context.RMGRNDetails.SingleOrDefaultAsync(g => g.RMGRNNo == orderDetail.RMGRNNo);

                if (grnDetail != null)
                {
                    var rmGRNDetail = new RMGRNDetail
                    {
                        RMGRNDate = orderDetail.RMGRNDate,
                        RMGRNNo = orderDetail.RMGRNNo,
                        InwardGatePassNo = orderDetail.InwardGatePassNo,
                        DomesticImport = orderDetail.DomesticImport,
                        SupplierOrgID = orderDetail.SupplierOrgID,
                        Currency = orderDetail.Currency,
                        TotalMaterialCost = orderDetail.TotalMaterialCost,
                        GSTType = orderDetail.GSTType,
                        TotalTaxAmount = orderDetail.TotalTaxAmount,
                        TotalMaterialCostAndTaxAmount = orderDetail.TotalMaterialCostAndTaxAmount,
                        CustomsAmount = orderDetail.CustomsAmount,
                        PackingAmount = orderDetail.PackingAmount,
                        PackingTaxRatePercentage = orderDetail.PackingTaxRatePercentage,
                        PackingTaxAmount = orderDetail.PackingTaxAmount,
                        FreightAmount = orderDetail.FreightAmount,
                        FreightTaxRatePercentage = orderDetail.FreightTaxRatePercentage,
                        FreightTaxAmount = orderDetail.FreightTaxAmount,
                        InsuranceAmount = orderDetail.InsuranceAmount,
                        InsuranceTaxRatePercentage = orderDetail.InsuranceTaxRatePercentage,
                        InsuranceTaxAmount = orderDetail.InsuranceTaxAmount,
                        TotalBillAmount = orderDetail.TotalBillAmount,
                        AdvancePayment = orderDetail.AdvancePayment,
                        BalanceAmount = orderDetail.BalanceAmount,
                        CreditDays = orderDetail.CreditDays,
                        InvoiceDCType = orderDetail.InvoiceDCType,
                        BillDCDate = orderDetail.BillDCDate,
                        BillDCNo = orderDetail.BillDCNo,
                        PackingHSNCode = orderDetail.PackingHSNCode,
                        FreightHSNCode = orderDetail.FreightHSNCode,
                        InsuranceHSNCode = orderDetail.InsuranceHSNCode,
                        DiscountPercentage = orderDetail.DiscountPercentage,
                        DiscountAmount = orderDetail.DiscountAmount
                    };

                    _context.RMGRNDetails.AddOrUpdate(rmGRNDetail);

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

        public async Task<BatchMaterialDetails> UpdateBatchMaterialDetails(BatchMaterialDetails batchMaterialDetails)
        {
            try
            {
                if (batchMaterialDetails != null)
                {
                  var existing = _context.BatchMaterialDetails.SingleOrDefault(a => a.Batch_No_Lot_No == batchMaterialDetails.Batch_No_Lot_No);
                    if (existing != null)
                    {
                        existing.Bag_Pack_Size = batchMaterialDetails.Bag_Pack_Size;
                        existing.Bag_Pack_UOM = batchMaterialDetails.Bag_Pack_UOM;
                        existing.Batch_No_Lot_No = batchMaterialDetails.Batch_No_Lot_No;
                        existing.Bill_Rate = batchMaterialDetails.Bill_Rate;
                        existing.Discount_Amount = batchMaterialDetails.Discount_Amount;
                        existing.Expiry_Date = batchMaterialDetails.Expiry_Date;
                        existing.Free_Bag_Pack_Size = batchMaterialDetails.Free_Bag_Pack_Size;
                        existing.Free_Bag_Pack_UOM = batchMaterialDetails.Free_Bag_Pack_UOM;
                        existing.Free_Batch_No = batchMaterialDetails.Free_Batch_No;
                        existing.Free_Expiry_Date = batchMaterialDetails.Free_Expiry_Date;
                        existing.Free_Mfg_Date = batchMaterialDetails.Free_Mfg_Date;
                        existing.Free_No_Bags_Packs = batchMaterialDetails.Free_No_Bags_Packs;
                        existing.Free_Total_Quantity = batchMaterialDetails.Free_Total_Quantity;
                        existing.Gross_Total_Material_Amount = batchMaterialDetails.Gross_Total_Material_Amount;
                        existing.Gross_Total_Quantity = batchMaterialDetails.Gross_Total_Quantity;
                        existing.Mfg_Date = batchMaterialDetails.Mfg_Date;
                        existing.Net_Material_Amount = batchMaterialDetails.Net_Material_Amount;
                        existing.Net_Material_Rate = batchMaterialDetails.Net_Material_Rate;
                        existing.No_Bags_Packs = batchMaterialDetails.No_Bags_Packs;
                        existing.Received_Quantity = batchMaterialDetails.Received_Quantity;
                       // existing.RM_GRN_Batch_No = batchMaterialDetails.RM_GRN_Batch_No;
                        existing.Total_Amount = batchMaterialDetails.Total_Amount;
                        existing.Total_Qty_UOM = batchMaterialDetails.Total_Qty_UOM;
                        existing.Total_Quantity = batchMaterialDetails.Total_Quantity;
                        var res = await _context.SaveChangesAsync();
                        if (res > 0)
                        {
                            return existing;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        _context.BatchMaterialDetails.Add(batchMaterialDetails);
                        var res = await _context.SaveChangesAsync();
                         if(res > 0)
                        {
                            return batchMaterialDetails;
                        }else
                        {
                            return null;
                        }
                    }

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return null;
        }

        public async Task<BatchMaterialDetails> GetBatchMaterialDetailsByBatchNo(string batchNo)
        {
            try
            {
                if (!string.IsNullOrEmpty(batchNo))
                {
                    int batchNoVal = Convert.ToInt32(batchNo);
                 var res = await  _context.BatchMaterialDetails.SingleOrDefaultAsync(a => a.RM_GRN_Batch_No == batchNoVal);
                    return res;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}