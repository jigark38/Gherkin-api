using GherkinWebAPI.Core.PurchaseMgmt;
using GherkinWebAPI.DTO.PurchageMgmt;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.GoodsReceiptNote;
using GherkinWebAPI.Models.PurchageMgmt;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Linq.SqlClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Repository.PurchaseMgmt
{
    public class PurchaseManagementRepository : IPurchageManagementRepository
    {
        private RepositoryContext _context;
        public PurchaseManagementRepository(RepositoryContext repositoryContext)
        {
            _context = repositoryContext;
        }

        public async Task<CreatePOWithMaterialAndCondition> CreatePurchaseOrder(CreatePOWithMaterialAndCondition poDetail)
        {
            CreatePOWithMaterialAndCondition pOWithMaterialAndCondition = new CreatePOWithMaterialAndCondition();
            pOWithMaterialAndCondition.rmPoMaterialConditions = new List<RMPOMaterialCondition>();
            pOWithMaterialAndCondition.rmPoMaterialDetails = new List<RMPOMaterialDetail>();
            pOWithMaterialAndCondition.rMPOIndentDetails = new List<RMPOIndentDetail>();

            using (var tran = _context.Database.BeginTransaction())
            {
                try
                {
                    if (poDetail.purchageOrderDetail != null)
                    {
                        int? MaxPOId = await _context.PurchaseOrderDetails.MaxAsync(e => (int?)e.Id);
                        if (MaxPOId != null)
                        {
                            int newId = (int)MaxPOId + 1;
                            poDetail.purchageOrderDetail.rmPoNo = "RM_PO_NO_" + newId;
                        }
                        else
                            poDetail.purchageOrderDetail.rmPoNo = "RM_PO_NO_" + 1;

                        pOWithMaterialAndCondition.purchageOrderDetail = poDetail.purchageOrderDetail;
                        _context.PurchaseOrderDetails.Add(pOWithMaterialAndCondition.purchageOrderDetail);
                        await _context.SaveChangesAsync();

                        if (poDetail.rmPoMaterialDetails.Count > 0)
                        {
                            foreach (var pomd in poDetail.rmPoMaterialDetails)
                            {
                                pomd.rmPoNo = poDetail.purchageOrderDetail.rmPoNo;
                                pOWithMaterialAndCondition.rmPoMaterialDetails.Add(pomd);
                            }
                            _context.RMPOMaterialDetails.AddRange(pOWithMaterialAndCondition.rmPoMaterialDetails);
                            await _context.SaveChangesAsync();
                        }

                        if (poDetail.rmPoMaterialConditions.Count > 0)
                        {
                            foreach (var pocm in poDetail.rmPoMaterialConditions)
                            {
                                int? MaxPOCmId = await _context.RMPOMaterialConditions.MaxAsync(e => (int?)e.Id);
                                if (MaxPOCmId != null)
                                {
                                    int newId = (int)MaxPOCmId + 1;
                                    pocm.pOMcNo = "POMC_" + newId;
                                }
                                else
                                    pocm.pOMcNo = "POMC_" + 1;

                                pocm.rmPoNo = poDetail.purchageOrderDetail.rmPoNo;
                                pOWithMaterialAndCondition.rmPoMaterialConditions.Add(pocm);
                            }
                            _context.RMPOMaterialConditions.AddRange(pOWithMaterialAndCondition.rmPoMaterialConditions);
                            await _context.SaveChangesAsync();
                        }
                        if (poDetail.rMPOIndentDetails.Count > 0)
                        {
                            foreach (var poid in poDetail.rMPOIndentDetails)
                            {
                                poid.rmPoNo = poDetail.purchageOrderDetail.rmPoNo;
                                pOWithMaterialAndCondition.rMPOIndentDetails.Add(poid);
                            }
                            _context.RMPOIndentDetails.AddRange(pOWithMaterialAndCondition.rMPOIndentDetails);
                            await _context.SaveChangesAsync();
                        }


                    }

                    tran.Commit();
                    return pOWithMaterialAndCondition;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }


        public async Task<List<PurchaseOrderDetail>> GetAllPurchaseOrder()
        {
            return await _context.PurchaseOrderDetails.ToListAsync();
        }
        public async Task<List<IndentDetail>> GetAllIndentDetails()
        {
            //var connString = GetConnectionString();
            List<IndentDetail> lstIndentDetails = new List<IndentDetail>();
            IndentDetail idetails = null; ;
            try
            {

                var data = await this._context.Database.SqlQuery<IndentDetailResponse>(
                "USP_GetALLIndentDetails"
                ).ToListAsync();

                foreach (var indentDetail in data)
                {
                    idetails = new IndentDetail();
                    idetails.indentNo = Convert.ToString(indentDetail.RM_INDENT_NO);
                    idetails.indentDate = Convert.ToDateTime(indentDetail.RM_Indent_Entry_Date);
                    idetails.materialGroupCode = Convert.ToString(indentDetail.Raw_Material_Group_Code);
                    idetails.rawMaterialGroupName = Convert.ToString(indentDetail.Raw_Material_Group);
                    idetails.rawMaterialDetailsCode = Convert.ToString(indentDetail.Raw_Material_Details_Code);
                    idetails.rawMaterialDetailsName = Convert.ToString(indentDetail.Raw_Material_Details_Name);
                    idetails.orderQty = Convert.ToInt32(0);
                    idetails.branchAreaId = Convert.ToString(indentDetail.AREA_ID);
                    idetails.branchArea = Convert.ToString(indentDetail.Area_Name);
                    idetails.indentQty = Convert.ToInt32(indentDetail.RM_Indent_Req_Qty);
                    idetails.indentUom = Convert.ToString(indentDetail.Raw_Material_UOM);

                    lstIndentDetails.Add(idetails);
                }


                return lstIndentDetails.OrderBy(e => e.indentDate).ThenBy(e => e.rawMaterialGroupName).ToList();
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<int> GetNextPurchageOrderId()
        {
            int? MaxPOId = await _context.PurchaseOrderDetails.MaxAsync(e => (int?)e.Id);
            if (MaxPOId != null)
                return (int)MaxPOId;
            else
                return 0;
        }

        public async Task<List<State>> GetStatesBySuppOrgId(string suppOrgId)
        {
            var suppDetails = _context.SupplierDetails.Where(e => e.supplierOrgID == suppOrgId).ToList();
            if (suppDetails.Count > 0)
            {
                int stateId = Convert.ToInt32(suppDetails[0].stateID);
                var states = await Task.Run(() => ((from state in _context.States
                                                    where state.State_Code == stateId
                                                    select new { state.State_Code, state.State_Name })
                                                     .AsEnumerable()
                                                     .Select(c => new State
                                                     {
                                                         State_Code = c.State_Code,
                                                         State_Name = c.State_Name
                                                     }).ToList()));

                return states;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Place>> GetPlacesBySuppOrgId(string suppOrgId)
        {
            var placeCode = _context.SupplierDetails.Where(e => e.supplierOrgID == suppOrgId).FirstOrDefault().placeCode;
            if (placeCode > 0)
            {
                return await _context.Places.Where(e => e.PlaceCode == placeCode).ToListAsync();
            }
            else
                return null;
        }

        public async Task<List<Country>> GetCountryBySppOrgId(string suppOrgId)
        {
            var supDetails = await _context.SupplierDetails.Where(e => e.supplierOrgID == suppOrgId).ToListAsync();
            if (supDetails.Count > 0)
            {
                int countryId = Convert.ToInt32(supDetails[0].countryID);
                var contry = await Task.Run(() => ((from country in _context.Countries
                                                    where country.Country_Code == countryId
                                                    select new { country.Country_Code, country.Country_Name })
                                                  .AsEnumerable()
                                                  .Select(c => new Country
                                                  {
                                                      Country_Code = c.Country_Code,
                                                      Country_Name = c.Country_Name
                                                  }).ToList()));

                return contry;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<SupplierDetails>> GetAllSuppliers()
        {
            return await _context.SupplierDetails.OrderBy(e => e.organisationName).ToListAsync();
        }

        public async Task<RMPOMaterialDetail> CreateOrderMaterial(RMPOMaterialDetail rmPoDetail)
        {
            try
            {
                _context.RMPOMaterialDetails.Add(rmPoDetail);
                await _context.SaveChangesAsync();
                return rmPoDetail;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<RMPOMaterialDetail>> GetAllRMPOMaterial()
        {
            return await _context.RMPOMaterialDetails.ToListAsync();
        }

        public async Task<TaxPercentageRate> GetTaxPercentByGSTType(string detailCode, string gstType)
        {
            TaxPercentageRate tpr = new TaxPercentageRate();
            try
            {
                if (!string.IsNullOrEmpty(detailCode))
                {
                    var rowMaterialDetail = await _context.RawMaterialDetails.FirstOrDefaultAsync(e => e.Raw_Material_Details_Code == detailCode.Trim());
                    if (rowMaterialDetail != null)
                    {
                        if (gstType.ToUpper().Trim() == "IGST")
                        {
                            tpr.igstRate = rowMaterialDetail.Raw_Material_IGST_Rate;
                            return tpr;
                        }
                        else if (gstType.ToUpper().Trim() == "SGST")
                        {
                            tpr.cgstRate = rowMaterialDetail.Raw_Material_CGST_Rate;
                            tpr.sgstRate = rowMaterialDetail.Raw_Material_SGST_Rate;

                            return tpr;
                        }
                        else
                        {
                            return new TaxPercentageRate();
                        }
                    }
                }
                return new TaxPercentageRate();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<PendingPurchaseOrder>> GetPendingPurchaseOrdersAsync()
        {
            try
            {
                var result1 = await (from PO in _context.PurchaseOrderDetails
                                     join rmgd in _context.RMGRNMaterialDetails on PO.rmPoNo equals rmgd.RMPONO
                                     select new { PO.rmPoNo, rmgd.RawMaterialDetailsCode, rmgd.RawMaterialGroupCode, rmgd.RMGRNBillQty } into x
                                     group x by new { x.RawMaterialDetailsCode, x.rmPoNo, x.RawMaterialGroupCode } into rmmg
                                     select new
                                     {
                                         rmPoNo = rmmg.Key.rmPoNo.Length > 9 ? rmmg.Key.rmPoNo.Substring(9) : rmmg.Key.rmPoNo,
                                         RawMaterialGroupCode = rmmg.Key.RawMaterialGroupCode,
                                         RawMaterialDetailsCode = rmmg.Key.RawMaterialDetailsCode,
                                         sum = rmmg.Sum(g => g.RMGRNBillQty)
                                     }).ToListAsync();

                var pendingPurchaseOrders1 = await
                            (from rmpom in _context.RMPOMaterialDetails
                             join po in _context.PurchaseOrderDetails on rmpom.rmPoNo equals po.rmPoNo
                             join s in _context.SupplierDetails on po.supplierOrgId equals s.supplierOrgID
                             join p in _context.Places on s.placeCode equals p.PlaceCode
                             join rawm in _context.RawMaterialDetails on rmpom.rowMaterialDetailsCode equals rawm.Raw_Material_Details_Code
                             select new PendingPurchaseOrder
                             {
                                 RMPODate = po.rmPoDate,
                                 RMPONo = rmpom.rmPoNo.Length > 9 ? rmpom.rmPoNo.Substring(9) : rmpom.rmPoNo,
                                 SupplierOrgId = po.supplierOrgId,
                                 SupplierOrganisationName = s.organisationName,
                                 RawMaterialDetaisName = rawm.Raw_Material_Details_Name,
                                 RawMaterialDetailsCode = rmpom.rowMaterialDetailsCode,
                                 RawMaterialGroupCode = rmpom.rawMaterialGroupCode,
                                 DomesticImport = po.domesticImport,
                                 GSTType = po.gstType,
                                 PlaceName = p.PlaceName,
                                 RMOrderQty = rmpom.rmOrderQty,
                                 TillNowRecordQuantity = 0,
                                 PendingQuatity = rmpom.rmOrderQty,
                                 RMPORate = rmpom.rmPoRate,
                                 RMPOMaterialWiseCost = rmpom.rmPoMaterialWiseCost,
                                 RMPOMaterialCGSTRate = rmpom.rmPoMaterialCGSTRate,
                                 RMPOMaterialIGSTRate = rmpom.rmPoMaterialIGSTRate,
                                 RMPOMaterialSGSRate = rmpom.rmPoMaterialSGSTRate
                             }).Distinct().ToListAsync();

                List<PendingPurchaseOrder> pendingPurchaseOrders = new List<PendingPurchaseOrder>();

                foreach (var item in pendingPurchaseOrders1)
                {
                    if (result1.Where(y => y.rmPoNo == item.RMPONo && y.RawMaterialDetailsCode == item.RawMaterialDetailsCode && y.RawMaterialGroupCode == item.RawMaterialGroupCode).FirstOrDefault() != null)
                    {
                        item.TillNowRecordQuantity = result1.Where(y => y.rmPoNo == item.RMPONo && y.RawMaterialDetailsCode == item.RawMaterialDetailsCode && y.RawMaterialGroupCode == item.RawMaterialGroupCode).FirstOrDefault().sum;
                        item.PendingQuatity = item.RMOrderQty - item.TillNowRecordQuantity;
                    }
                }

                pendingPurchaseOrders = pendingPurchaseOrders1.Where(e => e.PendingQuatity > 0).ToList();

                return pendingPurchaseOrders;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<SupplierDetails> GetSupplierOrgNameByRMGRNNo(string rmGrnNo)
        {
            var RMGRNDetail = await _context.RMGRNDetails.FirstOrDefaultAsync(e => e.RMGRNNo == rmGrnNo);
            if (RMGRNDetail != null && !string.IsNullOrEmpty(RMGRNDetail.SupplierOrgID))
            {
                return await _context.SupplierDetails.FirstOrDefaultAsync(e => e.supplierOrgID == RMGRNDetail.SupplierOrgID);
            }
            return null;
        }

        public async Task<Place> GetPlaceNameBySuppOrgId(string supOrgId)
        {
            var places = await _context.SupplierDetails.Where(e => e.supplierOrgID == supOrgId).ToListAsync();
            if (places.Count > 0)
            {
                int placeCode = places[0].placeCode;
                return await _context.Places.Where(e => e.PlaceCode == placeCode).FirstOrDefaultAsync();
            }
            else
            {
                return null;
            }
        }

        public async Task<RMGRNDetail> GetRMGrnDetailsByRMGrnNo(string rmGrnNo)
        {
            return await _context.RMGRNDetails.FirstOrDefaultAsync(e => e.RMGRNNo == rmGrnNo);
        }

        public async Task<List<PurchageReceivedDetail>> GetPurchageRecievedDetails(string suppOrgId)
        {
            //var connString = GetConnectionString();
            List<PurchageReceivedDetail> lstIndentDetails = new List<PurchageReceivedDetail>();
            PurchageReceivedDetail idetails = null; ;
            try
            {
                var data = await this._context.Database.SqlQuery<PurchageReceivedDetailResponse>(
                        "USP_GetPOReceivedDetailsSuppOrgId @Supplier_Org_ID",
                       new SqlParameter("Supplier_Org_ID", suppOrgId)
                   ).ToListAsync();
                foreach (var purchageReceivedDetail in data)
                {
                    idetails = new PurchageReceivedDetail();
                    idetails.grnDate = Convert.ToDateTime(purchageReceivedDetail.RM_GRN_Date);
                    idetails.gstType = Convert.ToString(purchageReceivedDetail.GST_Type);
                    idetails.rmGrnNo = Convert.ToString(purchageReceivedDetail.RM_GRN_NO);
                    idetails.invoiceDate = Convert.ToDateTime(purchageReceivedDetail.Bill_DC_Date);
                    idetails.invoiceNo = Convert.ToString(purchageReceivedDetail.Bill_DC_No);
                    idetails.invoiceType = Convert.ToString(purchageReceivedDetail.Invoice_DC_Type);
                    idetails.rawMaterialDetailsCode = Convert.ToString(purchageReceivedDetail.Raw_Material_Details_Code);
                    idetails.rawMaterialGroupCode = Convert.ToString(purchageReceivedDetail.Raw_Material_Group_Code);
                    idetails.rmGrnReceivedQty = Convert.ToInt32(purchageReceivedDetail.RM_GRN_Received_Qty);
                    idetails.rmGrnMaterialTransferQty = Convert.ToInt32(purchageReceivedDetail.RM_Material_Transfer_Qty);
                    idetails.rmGrnMaterialWiseTotalCost = Convert.ToDecimal(purchageReceivedDetail.RM_GRN_Material_Wise_Total_Cost);

                    lstIndentDetails.Add(idetails);
                }


                return lstIndentDetails.OrderBy(e => e.grnDate).ThenBy(e => e.rmGrnNo).ToList();
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<List<PurchaseReceivedMaterialDetail>> GetMaterialRecievedDetails(string rmGrnNo)
        {
            try
            {
                var recivedMaterialDetails = (from rmtcd in _context.RMMaterialTotalCostDetails
                                              join rgmd in _context.RMGRNMaterialDetails on rmtcd.rmGrnNo equals rgmd.RMGRNNO
                                              join rmgm in _context.RawMaterialGroupMaster on rmtcd.rawMaterialGroupCode equals rmgm.Raw_Material_Group_Code
                                              join rmd in _context.RawMaterialDetails on rmtcd.rawMaterialDetailsCode equals rmd.Raw_Material_Details_Code
                                              where rgmd.RMGRNNO == rmGrnNo
                                              orderby rmgm.Raw_Material_Group, rmd.Raw_Material_Group_Code

                                              select new PurchaseReceivedMaterialDetail()
                                              {
                                                  rmGrnNo = rgmd.RMGRNNO,
                                                  rmBatchNo = rmtcd.rmBatchNo,
                                                  rawMaterialGroupCode = rmtcd.rawMaterialGroupCode,
                                                  rawMaterialDetailsCode = rmtcd.rawMaterialDetailsCode,
                                                  rmGrnReceivedQty = rmtcd.rmGrnReceivedQty,
                                                  rmGrnMaterialwiseTotalCost = rmtcd.rmGrnMaterialwiseTotalCost,
                                                  rmCustomsShareAmount = rmtcd.rmCustomsShareAmount,
                                                  rmPackingShareAmount = rmtcd.rmPackingShareAmount,
                                                  rmFreightShareAmount = rmtcd.rmFreightShareAmount,
                                                  rmInsuranceShareAmount = rmtcd.rmInsuranceShareAmount,
                                                  rawMaterialGroup = rmgm.Raw_Material_Group,
                                                  rawMaterialDetailsName = rmd.Raw_Material_Details_Name,
                                                  rmGrnMaterialTransferQty = 0,
                                                  rmGrnMaterialBalanceQty = 0,
                                                  rmGrnMaterialReturnQty = 0,
                                                  rmGrnReturnMaterialCost = 0,
                                                  rmGrnRateApply = 0,
                                                  rmGrnBillQty = rgmd.RMGRNBillQty,
                                                  rmGrnBillRate = rgmd.RMGRNBillRate,
                                                  rmGrnIGSTRate = rgmd.RMGRNIGSTRate,
                                                  rmGrnIGSTAmount = rgmd.RMGRNIGSTAmount,
                                                  rmGrnCGSTRate = rgmd.RMGRNCGSTRate,
                                                  rmGrnCGSTAmount = rgmd.RMGRNCGSTAmount,
                                                  rmGrnSGSTRate = rgmd.RMGRNSGSTRate,
                                                  rmGrnSGSTAmount = rgmd.RMGRNSGSTAmount

                                              }).ToListAsync();


                return await recivedMaterialDetails;
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<CreatePurchageReturn> CreatePurchaseReturn(CreatePurchageReturn pReturnDetail)
        {
            CreatePurchageReturn pReturn = new CreatePurchageReturn();
            // pReturn.rmGrnDetail = new List<RMGRNDetail>();
            pReturn.outwardGatePassDetail = new List<OutwardGatePassDetail>();
            pReturn.purchageReturnMaterialDetail = new List<PurchageReturnMaterialDetail>();

            using (var tran = _context.Database.BeginTransaction())
            {
                try
                {
                    if (pReturnDetail.purchageReturnDetail != null)
                    {
                        int? MaxPOId = await _context.PurchageReturnDetails.MaxAsync(e => (int?)e.Id);
                        if (MaxPOId != null)
                        {
                            int newId = (int)MaxPOId + 1;
                            pReturnDetail.purchageReturnDetail.purchageReturnNo = "PR_" + newId;
                        }
                        else
                            pReturnDetail.purchageReturnDetail.purchageReturnNo = "PR_" + 1;

                        pReturn.purchageReturnDetail = pReturnDetail.purchageReturnDetail;
                        _context.PurchageReturnDetails.Add(pReturnDetail.purchageReturnDetail);
                        await _context.SaveChangesAsync();

                        // Save Outward Gatepass Detail
                        if (pReturnDetail.outwardGatePassDetail.Count > 0)
                        {
                            foreach (var ogpd in pReturnDetail.outwardGatePassDetail)
                            {
                                int? MaxOGPId = await _context.OutwardGatePassDetails.MaxAsync(e => (int?)e.Id);
                                if (MaxOGPId != null)
                                {
                                    int newId = (int)MaxOGPId + 1;
                                    ogpd.ogpNo = "OGP_" + newId;
                                }
                                else
                                    ogpd.ogpNo = "OGP_" + 1;

                                ogpd.transactionNo = pReturn.purchageReturnDetail.purchageReturnNo;
                                pReturn.outwardGatePassDetail.Add(ogpd);
                            }

                            _context.OutwardGatePassDetails.AddRange(pReturn.outwardGatePassDetail);
                            await _context.SaveChangesAsync();
                        }
                        if (pReturnDetail.purchageReturnMaterialDetail.Count > 0)
                        {
                            foreach (var prmd in pReturnDetail.purchageReturnMaterialDetail)
                            {
                                prmd.purchageReturnNo = pReturn.purchageReturnDetail.purchageReturnNo;
                                pReturn.purchageReturnMaterialDetail.Add(prmd);
                            }
                            _context.PurchageReturnMaterialDetails.AddRange(pReturn.purchageReturnMaterialDetail);
                            await _context.SaveChangesAsync();
                        }
                    }

                    tran.Commit();
                    return pReturn;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }

        public async Task<List<string>> GetOrderIdsBySuppOrgId(string SuppOrgId)
        {
            return await _context.PurchaseOrderDetails.Where(e => e.supplierOrgId == SuppOrgId).Select(x => x.rmPoNo).ToListAsync();
        }
        public async Task<CreatePOWithMaterialAndCondition> GetPurchaseOrderByID(string RmPoNo)
        {
            CreatePOWithMaterialAndCondition pOWithMaterialAndCondition = new CreatePOWithMaterialAndCondition();


            pOWithMaterialAndCondition.purchageOrderDetail = await _context.PurchaseOrderDetails.FirstOrDefaultAsync(e => e.rmPoNo == RmPoNo);

            pOWithMaterialAndCondition.rmPoMaterialDetails = await _context.RMPOMaterialDetails.Where(e => e.rmPoNo == pOWithMaterialAndCondition.purchageOrderDetail.rmPoNo).ToListAsync();
            pOWithMaterialAndCondition.rmPoMaterialConditions = await _context.RMPOMaterialConditions.Where(e => e.rmPoNo == pOWithMaterialAndCondition.purchageOrderDetail.rmPoNo).ToListAsync();
            pOWithMaterialAndCondition.rMPOIndentDetails = await _context.RMPOIndentDetails.Where(e => e.rmPoNo == pOWithMaterialAndCondition.purchageOrderDetail.rmPoNo).ToListAsync();

            pOWithMaterialAndCondition.indentMaterialNames = new List<IndentMaterialName>();
            foreach (var indentDetail in pOWithMaterialAndCondition.rMPOIndentDetails)
            {
                List<IndentMaterialName> indentMaterialNames = await (from ID in _context.branchIndentMaterialDetails
                                                                      join RG in _context.RawMaterialGroupMaster on ID.Raw_Material_Group_Code equals RG.Raw_Material_Group_Code
                                                                      join RD in _context.RawMaterialDetails on ID.Raw_Material_Details_Code equals RD.Raw_Material_Details_Code
                                                                      where ID.RM_Indent_No == indentDetail.rmIndentNo
                                                                      select new IndentMaterialName
                                                                      {
                                                                          indentNo = RD.Raw_Material_Group_Code,
                                                                          rawMaterialGroupName = RG.Raw_Material_Group,
                                                                          rawMaterialDetailsName = RD.Raw_Material_Details_Name,
                                                                          rawMaterialDetailsCode = RD.Raw_Material_Details_Code,
                                                                          materialGroupCode = ID.Raw_Material_Group_Code
                                                                      }).ToListAsync();

                pOWithMaterialAndCondition.indentMaterialNames.AddRange(indentMaterialNames);
            }
            return pOWithMaterialAndCondition;
        }

        public async Task<CreatePOWithMaterialAndCondition> ModifyPurchaseOrder(CreatePOWithMaterialAndCondition poDetail)
        {

            CreatePOWithMaterialAndCondition pOWithMaterialAndCondition = new CreatePOWithMaterialAndCondition();

            using (var tran = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.PurchaseOrderDetails.AddOrUpdate(poDetail.purchageOrderDetail);

                    foreach (var pomd in poDetail.rmPoMaterialDetails)
                    {
                        _context.RMPOMaterialDetails.AddOrUpdate(pomd);
                    }

                    foreach (var pocm in poDetail.rmPoMaterialConditions)
                    {
                        _context.RMPOMaterialConditions.AddOrUpdate(pocm);
                    }
                    await _context.SaveChangesAsync();


                    tran.Commit();
                    return pOWithMaterialAndCondition;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }

            }
        }
        public async Task<List<string>> GetPurchaseReturnIdsBySuppOrgId(string SuppOrgId)
        {
            return await _context.PurchageReturnDetails.Where(e => e.supplierOrgId == SuppOrgId).Select(x => x.purchageReturnNo).ToListAsync();
        }
        public async Task<CreatePurchageReturn> ModifyPurchaseReturn(CreatePurchageReturn pReturnDetail)
        {
            CreatePurchageReturn pReturn = new CreatePurchageReturn();
            // pReturn.rmGrnDetail = new List<RMGRNDetail>();
            pReturn.outwardGatePassDetail = new List<OutwardGatePassDetail>();
            pReturn.purchageReturnMaterialDetail = new List<PurchageReturnMaterialDetail>();

            using (var tran = _context.Database.BeginTransaction())
            {
                try
                {
                    if (pReturnDetail.purchageReturnDetail != null)
                    {
                        pReturn.purchageReturnDetail = pReturnDetail.purchageReturnDetail;
                        _context.PurchageReturnDetails.AddOrUpdate(pReturnDetail.purchageReturnDetail);
                        //await _context.SaveChangesAsync();

                        if (pReturnDetail.purchageReturnMaterialDetail.Count > 0)
                        {
                            foreach (var prmd in pReturnDetail.purchageReturnMaterialDetail)
                            {
                                _context.PurchageReturnMaterialDetails.AddOrUpdate(prmd);
                            }
                        }
                        await _context.SaveChangesAsync();
                    }

                    tran.Commit();
                    return pReturnDetail;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }

        public async Task<CreatePurchageReturn> FindPurchaseReturnById(string purchaseReturnID)
        {
            CreatePurchageReturn createPurchageReturn = new CreatePurchageReturn();
            createPurchageReturn.purchageReturnDetail = await _context.PurchageReturnDetails.FirstOrDefaultAsync(e => e.purchageReturnNo == purchaseReturnID);
            createPurchageReturn.outwardGatePassDetail = await _context.OutwardGatePassDetails.Where(e => e.transactionNo == purchaseReturnID).ToListAsync();
            createPurchageReturn.purchageReturnMaterialDetail = await _context.PurchageReturnMaterialDetails.Where(e => e.purchageReturnNo == purchaseReturnID).ToListAsync();

            return createPurchageReturn;
        }
    }
}