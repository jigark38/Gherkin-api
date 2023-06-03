using GherkinWebAPI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using GherkinWebAPI.Persistence;
using System.Data.Entity;
using GherkinWebAPI.Models;
using GherkinWebAPI.DTO.ProformaInvoiceDetail;

namespace GherkinWebAPI.Repository
{
    public class ProformaInvoiceDetailsRepository : RepositoryBase<ProductionDetailsData>, IProformaInvoiceDetailsRepository
    {

        private RepositoryContext _context;
        public ProformaInvoiceDetailsRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            _context = repositoryContext;
        }

        public async Task<ProformaInvoiceDetails> AddProfromaDetails(ProformaInvoice proformaInvoice)
        {
            using (DbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var proformaInvoiceDetails = new ProformaInvoiceDetails
                    {
                        Prof_Inv_No = proformaInvoice.Prof_Inv_No,
                        Prof_Inv_Date = proformaInvoice.Prof_Inv_Date,
                        Export_Domestic_sales = proformaInvoice.Export_Domestic_sales,
                        Exporters_Ref_No = proformaInvoice.Exporters_Ref_No,
                        IGST_SGST = proformaInvoice.IGST_SGST,
                        C_B_Code = proformaInvoice.C_B_Code,
                        W_Country_Id = proformaInvoice.W_Country_Id,
                        Buyer_C_B_Code = proformaInvoice.Buyer_C_B_Code,
                        Buyer_Order_No = proformaInvoice.Buyer_Order_No,
                        Buyer_Order_Date = proformaInvoice.Buyer_Order_Date,
                        Pre_Carriage_By = proformaInvoice.Pre_Carriage_By,
                        Place_of_Receipt_PC = proformaInvoice.Place_of_Receipt_PC,
                        Vessel_Flight_No = proformaInvoice.Vessel_Flight_No,
                        Port_of_Loading = proformaInvoice.Port_of_Loading,
                        Port_of_Discharge = proformaInvoice.Port_of_Discharge,
                        Final_Destination = proformaInvoice.Final_Destination,
                        Shipment_Date = proformaInvoice.Shipment_Date,
                        Currency = proformaInvoice.Currency,
                        Payment_Terms = proformaInvoice.Payment_Terms,
                        Terms_Conditions = proformaInvoice.Terms_Conditions,
                        Bank_Code = proformaInvoice.Bank_Code,
                        Total_Nos = proformaInvoice.Total_Nos,
                        Prof_Invoice_Amount = proformaInvoice.Prof_Invoice_Amount,
                        Ocean_Freight = proformaInvoice.Ocean_Freight,
                        Total_Prof_Invoice_Amount = proformaInvoice.Total_Prof_Invoice_Amount,
                        Total_Net_Wt_Kgs = proformaInvoice.Total_Net_Wt_Kgs,
                        Total_Gross_Wt_Kgs = proformaInvoice.Total_Gross_Wt_Kgs,
                        Employee_ID = proformaInvoice.Employee_ID,
                        Approved_Employee_Id = proformaInvoice.Approved_Employee_Id
                    };


                    _context.ProformaInvoiceDetails.Add(proformaInvoiceDetails);
                    await _context.SaveChangesAsync();

                    foreach (var fs in proformaInvoice.ProductionDetails)
                    {
                        var productionDetails = new ProductionDetails
                        {
                            Prof_Inv_No = proformaInvoice.Prof_Inv_No,
                            Prof_Inv_Prod_No = await GetProformaProductId(),
                            FP_Group_Code = fs.FP_Group_Code,
                            FP_Variety_Code = fs.FP_Variety_Code,
                            FP_Grade_Code = fs.FP_Grade_Code,
                            Preserved_In = fs.Preserved_In,
                            Palletised_Nonpalletised = fs.Palletised_Nonpalletised,
                            Process_Details = fs.Process_Details,
                            Pack_UOM = fs.Pack_UOM,
                            Qty_Drum = fs.Qty_Drum,
                            Drum_Weight = fs.Drum_Weight,
                            Order_Quantity = fs.Order_Quantity,
                            Qty_UOM = fs.Qty_UOM,
                            Order_Rate = fs.Order_Rate,
                            Rate_UOM = fs.Rate_UOM,
                            Product_Order_Amount = fs.Product_Order_Amount
                        };

                        _context.ProductionDetails.Add(productionDetails);
                        await _context.SaveChangesAsync();
                    }

                    transaction.Commit();
                    return proformaInvoiceDetails;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public async Task<ProductionDetails> AddProductionDetails(ProductionDetails productionDetails)
        {
            try
            {
                _context.ProductionDetails.Add(productionDetails);
                var result = await _context.SaveChangesAsync();

                if (result == 1)
                {
                    return productionDetails;
                }

                return new ProductionDetails();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> GetProformaInvoiceId()
        {
            string newId = string.Empty;
            ProformaInvoiceDetails invoiceDetails = await _context.ProformaInvoiceDetails.AsNoTracking().OrderByDescending(x => x.Id).FirstOrDefaultAsync();

            if (invoiceDetails == null)
            {
                newId = string.Concat("PI-1/", (DateTime.Now.Year).ToString().Substring(2, 2), "-", ((DateTime.Now.Year) + 1).ToString().Substring(2, 2));
            }
            else
            {
                string lastId = invoiceDetails.Prof_Inv_No;
                int increementNo = Convert.ToInt32((lastId.Split('/')[0]).Split('-')[1]) + 1;
                newId = string.Concat("PI-", increementNo.ToString(), "/", (DateTime.Now.Year).ToString().Substring(2, 2), "-", ((DateTime.Now.Year) + 1).ToString().Substring(2, 2));
            }
            return newId;
        }

        public async Task<string> GetProformaProductId()
        {
            string newId = string.Empty;
            ProductionDetails productionDetails = await _context.ProductionDetails.AsNoTracking().OrderByDescending(x => x.Id).FirstOrDefaultAsync();

            if (productionDetails == null)
            {
                newId = "PIPNO-1";
            }
            else
            {
                string lastId = productionDetails.Prof_Inv_Prod_No;
                int increementNo = Convert.ToInt32((lastId.Split('-')[1])) + 1;
                newId = string.Concat("PIPNO-", increementNo.ToString());
            }
            return newId;
        }

        public async Task<List<ProductDetailsDto>> GetProductionDetails()
        {
           var productDetailsDto = new List<ProductDetailsDto>();

           var data = await (from prodDtls in _context.ProductionDetails
                          join invoice in _context.ProformaInvoiceDetails on prodDtls.Prof_Inv_No equals invoice.Prof_Inv_No
                          join buyer in _context.Consignee_Buyers_Master on invoice.C_B_Code equals buyer.C_B_Code
                          join countryMst in _context.countriesoverseas on buyer.W_Country_Id equals countryMst.W_Country_Id
                          join product in _context.ProductDetails on prodDtls.FP_Variety_Code equals product.VarietyCode
                          join grade in _context.gradeDetails on prodDtls.FP_Grade_Code equals grade.GradeCode
                          select new ProductDetailsDto
                          {
                              Prof_Inv_No = prodDtls.Prof_Inv_No,
                              Prof_Inv_Prod_No = prodDtls.Prof_Inv_Prod_No,
                              Prof_Inv_Date = invoice.Prof_Inv_Date,
                              C_B_Code = invoice.C_B_Code,
                              C_B_Name = buyer.C_B_Name,
                              W_Country_Id = buyer.W_Country_Id,
                              W_Country_Name = countryMst.W_Country_Name,
                              FP_Group_Code = prodDtls.FP_Group_Code,
                              FP_Variety_Code = prodDtls.FP_Variety_Code,
                              FP_Variety_Name = product.VarietyName,
                              FP_Grade_Code = prodDtls.FP_Grade_Code,
                              FP_Grade = string.Concat(grade.GradeFrom, "/", grade.GradeTo),
                              Preserved_In = prodDtls.Preserved_In,
                              Packing_UOM = prodDtls.Pack_UOM,
                              Qty_Drum = prodDtls.Qty_Drum,
                          }).OrderBy(_ => _.Prof_Inv_Date).ThenBy(_ => _.Prof_Inv_No).ToListAsync();

            if (data.Any())
            {
                foreach(var item in data)
                {
                    var salesProductionSchedule = _context.SalesProductionSchedule.FirstOrDefault(_ => _.Prof_Inv_No == item.Prof_Inv_No || _.FP_Grade_Code == item.FP_Grade_Code);
                    if (salesProductionSchedule == null)
                    {
                        productDetailsDto.Add(item);
                    }
                }
            }

            return productDetailsDto;
        }

        public async Task<int> GetProductionScheduleId()
        {
            Int64? MaxPSId = await _context.ProductionScheduleDetails.MaxAsync(e => (Int64?)e.Production_Schedule_No);
            if (MaxPSId != null)
                return (int)MaxPSId + 1;
            else
                return 1;
        }

        public async Task<ProductionSchedule> AddProductionScheduleDetails(ProductionSchedule productionSchedule)
        {
            using (DbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if ((productionSchedule.PS_Through_Details).ToUpper() == "DIRECT PRODUCTION")
                    {
                        var productionScheduleDetails = new ProductionScheduleDetails();
                        productionScheduleDetails.Production_Schedule_No = productionSchedule.Production_Schedule_No;
                        productionScheduleDetails.Production_Schedule_Date = productionSchedule.Production_Schedule_Date;
                        productionScheduleDetails.Employee_ID = productionSchedule.Employee_ID;
                        productionScheduleDetails.PS_Through_Details = productionSchedule.PS_Through_Details;
                        productionScheduleDetails.PS_Require_Date_By = productionSchedule.PS_Require_Date_By;
                        productionScheduleDetails.Org_office_No = productionSchedule.Org_office_No;
                        _context.ProductionScheduleDetails.Add(productionScheduleDetails);
                        await _context.SaveChangesAsync();

                        foreach (DirectProductionSchedule dp in productionSchedule.DirectProductionSchedule)
                        {
                            var directProductionSchedule = new DirectProductionSchedule();
                            directProductionSchedule.Production_Schedule_No = dp.Production_Schedule_No;
                            directProductionSchedule.FP_Group_Code = dp.FP_Group_Code;
                            directProductionSchedule.FP_Variety_Code = dp.FP_Variety_Code;
                            directProductionSchedule.FP_Grade_Code = dp.FP_Grade_Code;
                            directProductionSchedule.PS_Quantity = dp.PS_Quantity;
                            directProductionSchedule.Qty_Drum = dp.Qty_Drum;
                            directProductionSchedule.PS_Qty_UOM = dp.PS_Qty_UOM;
                            directProductionSchedule.Pack_UOM = dp.Pack_UOM;
                            directProductionSchedule.Drum_Weight = dp.Drum_Weight;
                            directProductionSchedule.Media_Process_Code = dp.Media_Process_Code;
                            directProductionSchedule.Media_Process_Desc_Remarks = dp.Media_Process_Desc_Remarks;
                            _context.DirectProductionSchedule.Add(directProductionSchedule);
                        }
                        await _context.SaveChangesAsync();
                        transaction.Commit();
                    }
                    else
                    {
                        var productionScheduleDetails = new ProductionScheduleDetails();
                        productionScheduleDetails.Production_Schedule_No = productionSchedule.Production_Schedule_No;
                        productionScheduleDetails.Production_Schedule_Date = productionSchedule.Production_Schedule_Date;
                        productionScheduleDetails.Employee_ID = productionSchedule.Employee_ID;
                        productionScheduleDetails.PS_Through_Details = productionSchedule.PS_Through_Details;
                        productionScheduleDetails.PS_Require_Date_By = productionSchedule.PS_Require_Date_By;
                        productionScheduleDetails.Org_office_No = productionSchedule.Org_office_No;
                        _context.ProductionScheduleDetails.Add(productionScheduleDetails);
                        await _context.SaveChangesAsync();


                        foreach (SalesProductionSchedule sp in productionSchedule.SalesProductionSchedule)
                        {
                            var salesProductionSchedule = new SalesProductionSchedule();
                            salesProductionSchedule.Production_Schedule_No = sp.Production_Schedule_No;
                            salesProductionSchedule.Prof_Inv_No = sp.Prof_Inv_No;
                            salesProductionSchedule.FP_Group_Code = sp.FP_Group_Code;
                            salesProductionSchedule.FP_Variety_Code = sp.FP_Variety_Code;
                            salesProductionSchedule.FP_Grade_Code = sp.FP_Grade_Code;
                            salesProductionSchedule.PS_Quantity = sp.PS_Quantity;
                            salesProductionSchedule.Qty_Drum = sp.Qty_Drum;
                            salesProductionSchedule.Pack_UOM = sp.Pack_UOM;
                            salesProductionSchedule.Order_Quantity = sp.Order_Quantity;
                            salesProductionSchedule.PS_Product_Quantity = (int)sp.Qty_Drum * sp.PS_Quantity;
                            salesProductionSchedule.Media_Process_Code = sp.Media_Process_Code;
                            salesProductionSchedule.Media_Process_Desc_Remarks = sp.Media_Process_Desc_Remarks;
                            salesProductionSchedule.delivery_by = sp.delivery_by;
                            _context.SalesProductionSchedule.Add(salesProductionSchedule);
                            await _context.SaveChangesAsync();
                        }
                        transaction.Commit();
                    }

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
            return productionSchedule;
        }
    }
}