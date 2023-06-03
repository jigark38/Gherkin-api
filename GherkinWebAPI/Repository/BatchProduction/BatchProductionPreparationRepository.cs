using GherkinWebAPI.Core.BatchProduction;
using GherkinWebAPI.DTO;
using GherkinWebAPI.DTO.BatchProduction;
using GherkinWebAPI.DTO.Product_GradeDto;
using GherkinWebAPI.Entities;
using GherkinWebAPI.Models.BatchProduction;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace GherkinWebAPI.Repository.BatchProduction
{
    public class BatchProductionPreparationRepository : RepositoryBase<BatchScheduleDetails>, IBatchProductionPreparationRepository
    {
        private RepositoryContext _context;

        public BatchProductionPreparationRepository(RepositoryContext repositoryContext)
               : base(repositoryContext)
        {
            this._context = repositoryContext;
        }
        /// <summary>
        /// The GetOrgofficelocationDetails
        /// </summary>
        /// <returns>The <see cref="Task{List{OrganisationOfficeLocationDetails}}"/></returns>
        public async Task<List<OrganisationOfficeLocationDetailsDto>> GetOrgofficelocationDetails()
        {
            List<OrganisationOfficeLocationDetailsDto> list = new List<OrganisationOfficeLocationDetailsDto>();
            list = await (from orgdetails in _context.OrganisationOfficeLocationDetails
                          select new OrganisationOfficeLocationDetailsDto
                          {
                              Org_Code = orgdetails.Org_Code,
                              Org_Office_No = orgdetails.Org_Office_No,
                              Org_Office_Name = orgdetails.Org_Office_Name
                          }).OrderBy(c => c.Org_Office_Name).ToListAsync();
            return list;
        }

        public async Task<List<BatchGreenDetailsDto>> GetGreenReceivedByOrgOfficeNo(int orgOfficeNo)
        {
            List<BatchGreenDetailsDto> list = await (from hGrN in _context.HarvestGRNs
                                                     join gghg in _context.GreensGradedHarvestGRNDetails on hGrN.HarvestGRNNo
                                                            equals gghg.Harvest_GRN_No into GrnDetails
                                                     from i in GrnDetails.DefaultIfEmpty()
                                                     join ggq in _context.GreensGradingQuantityDetails on i.Greens_Grade_No
                                                            equals ggq.Greens_Grade_No into GradingDetails
                                                     from j in GradingDetails.DefaultIfEmpty()
                                                     join crop in _context.Crops on j.Crop_Name_Code equals crop.CropCode
                                                     join csd in _context.CropSchemes on j.Crop_Scheme_Code equals csd.Code into CropDetails
                                                     from k in CropDetails.DefaultIfEmpty()
                                                     
                                                     where hGrN.OrgOfficeNo == orgOfficeNo
                                                     && !_context.BatchScheduleGreensGRNDetails
                                                                   .Any(f => f.Harvest_GRN_No != hGrN.HarvestGRNNo
                                                                   && f.Crop_Name_Code != k.CropCode && f.Crop_Scheme_Code != k.Code)
                                                     orderby hGrN.HarvestGRNDate descending, hGrN.HarvestGRNNo descending
                                                     select new BatchGreenDetailsDto()
                                                     {
                                                         OrgOfficeNo = hGrN.OrgOfficeNo,
                                                         Harvest_GRN_No = hGrN.HarvestGRNNo,
                                                         Harvest_GRN_Date = hGrN.HarvestGRNDate,
                                                         Greens_Grade_No = (int?)i.Greens_Grade_No,
                                                         Greens_Grading_Qty_No = (int?)j.Greens_Grading_Qty_No,
                                                         Crop_Name = crop.Name,
                                                         Crop_Name_Code= j.Crop_Name_Code,
                                                         Crop_Scheme_Code = j.Crop_Scheme_Code,
                                                         Grading_No_of_Crates = (int?)j.Grading_No_of_Crates,
                                                         Quantity_After_Grading_Total = j == null ? null : (decimal?)j.Quantity_After_Grading_Total,
                                                         Grades = k.From + ' ' + k.Sign,
                                                     }).ToListAsync();

            return list;
        }

        public async Task<List<BatchGreenDetailsDto>> GetMediaProcessDetails()
        {

            List<BatchGreenDetailsDto> list = new List<BatchGreenDetailsDto>();
            list = await (from mprocess in _context.MediaProcessDetails
                          select new BatchGreenDetailsDto
                          {
                              MediaProcessCode = mprocess.MediaProcessCode,
                              MediaProcessName = mprocess.MediaProcessName,

                          }).ToListAsync();

            return list;

        }


        public async Task<List<EmployeeDTO>> GetScheduledByDetails()
        {

            List<EmployeeDTO> list = new List<EmployeeDTO>();
            list = await (from emp in _context.Employees
                          select new EmployeeDTO
                          {
                              employeeId = emp.employeeId,
                              employeeName = emp.employeeName,

                          }).ToListAsync();

            return list;

        }

        public async Task<List<ProductGroupDto>> GetProductGroupDetails()
        {

            List<ProductGroupDto> list = new List<ProductGroupDto>();
            list = await (from grp in _context.ProductGroups
                          select new ProductGroupDto
                          {
                              GroupCode = grp.GroupCode,
                              GrpName = grp.GrpName,

                          }).ToListAsync();

            return list;

        }

        public async Task<List<ProductVarietyDto>> GetProductNameDetails(string groupCode)
        {

            List<ProductVarietyDto> list = new List<ProductVarietyDto>();
            list = await (from prod in _context.ProductDetails
                          where prod.GroupCode == groupCode
                          select new ProductVarietyDto
                          {
                              VarietyCode = prod.VarietyCode,
                              VarietyName = prod.VarietyName,

                          }).ToListAsync();

            return list;

        }

        public async Task<List<GradeDto>> GetGradeDetails(string varietyCode)
        {

            List<GradeDto> list = new List<GradeDto>();
            list = await (from grades in _context.gradeDetails
                          where grades.VarietyCode == varietyCode
                          select new GradeDto
                          {
                              GradeCode = grades.GradeCode,
                              GradeFrom = grades.GradeFrom,
                              GradeTo = grades.GradeTo

                          }).ToListAsync();

            return list;
        }

        public async Task<List<BatchScheduledOrderDto>> GetScheduleOrderDetails(int orgOfficeNo)
        {
            List<BatchScheduledOrderDto> list = new List<BatchScheduledOrderDto>();
            var List2 = _context.SalesProductionSchedule.Where(item => !_context.BatchScheduleOrderProductions.Any(item2 => item2.PS_Sales_Order_Schedule_No == item.Production_Schedule_No));
            var List3 = _context.DirectProductionSchedule.Where(item => !_context.BatchScheduleOrderProductions.Any(item2 => item2.PS_Direct_Order_Schedule_No == item.PS_Direct_Order_Schedule_No && item2.FP_Grade_Code == item.FP_Grade_Code));

            if (List2 != null)
            {
                // DirectProductionSchedule
                list = await (from psqty in List2
                              join ps in _context.ProductionScheduleDetails on psqty.Production_Schedule_No equals ps.Production_Schedule_No
                              where ps.Org_office_No == orgOfficeNo
                              orderby ps.Production_Schedule_Date, ps.Production_Schedule_No
                              select new BatchScheduledOrderDto
                              {
                                  Production_Schedule_Date = ps.Production_Schedule_Date,
                                  Production_Schedule_No = ps.Production_Schedule_No,
                                  C_B_Name = (from PInvno in _context.ProformaInvoiceDetails.Where(c => c.Prof_Inv_No == psqty.Prof_Inv_No)
                                              join cus in _context.Consignee_Buyers_Master on PInvno.C_B_Code equals cus.C_B_Code
                                              where cus.C_B_Code == PInvno.C_B_Code
                                              select cus.C_B_Name).FirstOrDefault(),
                                  VarietyName = (from vname in _context.ProductDetails
                                                 where psqty.FP_Variety_Code == vname.VarietyCode
                                                 select vname.VarietyName).FirstOrDefault(),
                                  GradeFrom = (from gardes in _context.gradeDetails
                                               where psqty.FP_Grade_Code == gardes.GradeCode
                                               select gardes.GradeFrom).FirstOrDefault(),
                                  Qty_Drum = psqty.Qty_Drum, // Qty_Drum / Pack_UOM Concate
                                  Pack_UOM = psqty.Pack_UOM, // Qty_Drum / Pack_UOM Concate
                                  PS_Quantity = psqty.PS_Quantity,
                                  PS_Product_Quantity = psqty.PS_Product_Quantity,// Display with /KGS concate
                                  MediaProcessName = (from mprocess in _context.MediaProcessDetails
                                                      where psqty.Media_Process_Code == mprocess.MediaProcessCode
                                                      select mprocess.MediaProcessName).FirstOrDefault(),
                                  Media_Process_Desc_Remarks = psqty.Media_Process_Desc_Remarks

                              }).ToListAsync();
            }
            if (list.Count <= 0)
            {
                list = await (from psqty in List3
                              join ps in _context.ProductionScheduleDetails on psqty.Production_Schedule_No equals ps.Production_Schedule_No
                              where ps.Org_office_No == orgOfficeNo
                              orderby ps.Production_Schedule_Date, ps.Production_Schedule_No
                              select new BatchScheduledOrderDto
                              {
                                  Production_Schedule_Date = ps.Production_Schedule_Date,
                                  Production_Schedule_No = ps.Production_Schedule_No,
                                  C_B_Name = (from PInvno in _context.ProformaInvoiceDetails
                                              join cus in _context.Consignee_Buyers_Master on PInvno.C_B_Code equals cus.C_B_Code
                                              where cus.C_B_Code == PInvno.C_B_Code
                                              select cus.C_B_Name).FirstOrDefault(),
                                  VarietyName = (from vname in _context.ProductDetails
                                                 where psqty.FP_Variety_Code == vname.VarietyCode
                                                 select vname.VarietyName).FirstOrDefault(),
                                  GradeFrom = (from gardes in _context.gradeDetails
                                               where psqty.FP_Grade_Code == gardes.GradeCode
                                               select gardes.GradeFrom).FirstOrDefault(),
                                  Qty_Drum = psqty.Qty_Drum, // Qty_Drum / Pack_UOM Concate
                                  Pack_UOM = psqty.Pack_UOM, // Qty_Drum / Pack_UOM Concate
                                  PS_Quantity = psqty.PS_Quantity, // Display with /KGS concate
                                  PS_Qty_UOM = psqty.PS_Qty_UOM,
                                  MediaProcessName = (from mprocess in _context.MediaProcessDetails
                                                      where psqty.Media_Process_Code == mprocess.MediaProcessCode
                                                      select mprocess.MediaProcessName).FirstOrDefault(),
                                  Media_Process_Desc_Remarks = psqty.Media_Process_Desc_Remarks

                              }).ToListAsync();
            }

            return list;

        }
        public async Task<List<BatchScheduledOrderDto>> GetScheduleOrderDetailsByMediaProcess(string mediaprocessCode)
        {
            List<BatchScheduledOrderDto> list = new List<BatchScheduledOrderDto>();
            var List2 = _context.SalesProductionSchedule.Where(item => !_context.BatchScheduleOrderProductions.Any(item2 => item2.PS_Sales_Order_Schedule_No == item.Production_Schedule_No));
            var List3 = _context.DirectProductionSchedule.Where(item => !_context.BatchScheduleOrderProductions.Any(item2 => item2.PS_Direct_Order_Schedule_No == item.PS_Direct_Order_Schedule_No && item2.FP_Grade_Code == item.FP_Grade_Code));

            if (List2 != null)
            {
                // DirectProductionSchedule
                list = await (from psqty in List2
                              join ps in _context.ProductionScheduleDetails on psqty.Production_Schedule_No equals ps.Production_Schedule_No
                              where psqty.Media_Process_Code == mediaprocessCode
                              orderby ps.Production_Schedule_Date, ps.Production_Schedule_No
                              select new BatchScheduledOrderDto
                              {
                                  OrgOfficeNo = ps.Org_office_No,
                                  Production_Schedule_Date = ps.Production_Schedule_Date,
                                  Production_Schedule_No = ps.Production_Schedule_No,
                                  C_B_Name = (from PInvno in _context.ProformaInvoiceDetails.Where(c => c.Prof_Inv_No == psqty.Prof_Inv_No)
                                              join cus in _context.Consignee_Buyers_Master on PInvno.C_B_Code equals cus.C_B_Code
                                              where cus.C_B_Code == PInvno.C_B_Code
                                              select cus.C_B_Name).FirstOrDefault(),
                                  VarietyName = (from vname in _context.ProductDetails
                                                 where psqty.FP_Variety_Code == vname.VarietyCode
                                                 select vname.VarietyName).FirstOrDefault(),
                                  GradeFrom = (from gardes in _context.gradeDetails
                                               where psqty.FP_Grade_Code == gardes.GradeCode
                                               select gardes.GradeFrom).FirstOrDefault(),
                                  Qty_Drum = psqty.Qty_Drum, // Qty_Drum / Pack_UOM Concate
                                  Pack_UOM = psqty.Pack_UOM, // Qty_Drum / Pack_UOM Concate
                                  PS_Quantity = psqty.PS_Quantity,
                                  PS_Product_Quantity = psqty.PS_Product_Quantity,// Display with /KGS concate
                                  MediaProcessName = (from mprocess in _context.MediaProcessDetails
                                                      where psqty.Media_Process_Code == mprocess.MediaProcessCode
                                                      select mprocess.MediaProcessName).FirstOrDefault(),
                                  Media_Process_Desc_Remarks = psqty.Media_Process_Desc_Remarks

                              }).ToListAsync();
            }
            if (list.Count <= 0)
            {
                list = await (from psqty in List3
                              join ps in _context.ProductionScheduleDetails on psqty.Production_Schedule_No equals ps.Production_Schedule_No
                              where psqty.Media_Process_Code == mediaprocessCode
                              orderby ps.Production_Schedule_Date, ps.Production_Schedule_No
                              select new BatchScheduledOrderDto
                              {
                                  OrgOfficeNo = ps.Org_office_No,
                                  Production_Schedule_Date = ps.Production_Schedule_Date,
                                  Production_Schedule_No = ps.Production_Schedule_No,
                                  C_B_Name = (from PInvno in _context.ProformaInvoiceDetails
                                              join cus in _context.Consignee_Buyers_Master on PInvno.C_B_Code equals cus.C_B_Code
                                              where cus.C_B_Code == PInvno.C_B_Code
                                              select cus.C_B_Name).FirstOrDefault(),
                                  VarietyName = (from vname in _context.ProductDetails
                                                 where psqty.FP_Variety_Code == vname.VarietyCode
                                                 select vname.VarietyName).FirstOrDefault(),
                                  GradeFrom = (from gardes in _context.gradeDetails
                                               where psqty.FP_Grade_Code == gardes.GradeCode
                                               select gardes.GradeFrom).FirstOrDefault(),
                                  Qty_Drum = psqty.Qty_Drum, // Qty_Drum / Pack_UOM Concate
                                  Pack_UOM = psqty.Pack_UOM, // Qty_Drum / Pack_UOM Concate
                                  PS_Quantity = psqty.PS_Quantity, // Display with /KGS concate
                                  PS_Qty_UOM = psqty.PS_Qty_UOM,
                                  MediaProcessName = (from mprocess in _context.MediaProcessDetails
                                                      where psqty.Media_Process_Code == mprocess.MediaProcessCode
                                                      select mprocess.MediaProcessName).FirstOrDefault(),
                                  Media_Process_Desc_Remarks = psqty.Media_Process_Desc_Remarks

                              }).ToListAsync();
            }

            return list;

        }

        public async Task<dynamic> GetLatestBatchNo()
        {
            dynamic batchNo;
            batchNo = await ((from batch in _context.BatchScheduleDetails select batch.Batch_Production_No).FirstOrDefaultAsync());
            return batchNo;
        }

        public async Task<BatchProductionDetails> SaveBatchProductionDetails(BatchProductionDetails batchProductionDetailsObject)
        {
            try
            {
                long number = 0;

                using (var trans = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _context.BatchScheduleDetails.Add(batchProductionDetailsObject.batchScheduleDetails);
                        await _context.SaveChangesAsync();

                        if (_context.BatchScheduleDetails.Any())
                        {
                            number = _context.BatchScheduleDetails.OrderByDescending(i => i.Batch_Production_No).FirstOrDefault().Batch_Production_No;
                        }
                        batchProductionDetailsObject.batchScheduleDummyProduction.Batch_Production_No = number;

                        _context.BatchScheduleDummyProductions.Add(batchProductionDetailsObject.batchScheduleDummyProduction);
                        await _context.SaveChangesAsync();

                        foreach (var item in batchProductionDetailsObject.batchScheduleGreensGRNDetails)
                        {
                            item.Batch_Production_No = number;
                            _context.BatchScheduleGreensGRNDetails.Add(item);
                           
                        }
                        await _context.SaveChangesAsync();

                        Guid obj = Guid.NewGuid();
                        BatchScheduleDrumsBarcodeDetails batchScheduleDrumsBarcodeDetails = new BatchScheduleDrumsBarcodeDetails();
                        batchScheduleDrumsBarcodeDetails.Prod_Barrel_No = obj;
                        batchScheduleDrumsBarcodeDetails.Batch_Production_No = number;
                        batchScheduleDrumsBarcodeDetails.Media_Process_Code = batchProductionDetailsObject.batchScheduleDetails.Media_Process_Code;
                        batchScheduleDrumsBarcodeDetails.BS_Order_Production_No = null;

                        _context.BatchScheduleDrumsBarcodeDetails.Add(batchScheduleDrumsBarcodeDetails);
                        await _context.SaveChangesAsync();

                        trans.Commit();
                        batchProductionDetailsObject.status = "Success";
                        return batchProductionDetailsObject;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw ex;
                    }
                }
            }
            catch
            {
                batchProductionDetailsObject.status = "Failed";
                return batchProductionDetailsObject;
            }
        }
    }
}