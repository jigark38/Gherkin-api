using GherkinWebAPI.Core.MediaBatchDetails;
using GherkinWebAPI.Models.MediaBatchDetails;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Repository.MediaBatchDetails
{
    public class InterfaceRepository : IInterfaceRepository
    {
        private readonly RepositoryContext _context;
        public InterfaceRepository(RepositoryContext context)
        {
            _context = context;
        }
        public async Task<List<OrgOfficeNameList>> GetOrgOfficeNameLists()
        {
            try
            {
                var orgList = await (from o in _context.OrganisationOfficeLocationDetails
                                     orderby o.Org_Office_Name
                                     select new OrgOfficeNameList
                                     {
                                         Org_Code = o.Org_Code,
                                         Org_Office_Name = o.Org_Office_Name,
                                         Org_Office_No = o.Org_Office_No
                                     }).ToListAsync();
                return orgList;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<MediaProcessNameList>> GetMediaProcessNameList()
        {
            try
            {
                var medList = await (from o in _context.MediaProcessDetails
                                     orderby o.MediaProcessName
                                     select new MediaProcessNameList
                                     {
                                         MediaProcessCode = o.MediaProcessCode,
                                         MediaProcessName = o.MediaProcessName
                                     }).ToListAsync();
                return medList;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<PendingOrderScheduleGrid>> GetPendingOrderScheduleGrid(int orgOfficeNo, string mediaProcessCode)
        {
            try
            {
                List<BatchProdDetailModel> batchSch = new List<BatchProdDetailModel>();
                if (mediaProcessCode == "")
                {
                    batchSch = await (from b in _context.BatchScheduleDetails
                                      where b.Org_office_No == orgOfficeNo
                                      orderby b.Batch_Production_Date, b.Batch_Production_No
                                      select new BatchProdDetailModel
                                      {
                                          BatchProductionNo = b.Batch_Production_No,
                                          BatchProductionDate = b.Batch_Production_Date,
                                          MediaProcessCode = b.Media_Process_Code
                                      }).ToListAsync();
                }
                else
                {
                    batchSch = await (from b in _context.BatchScheduleDetails
                                      where b.Org_office_No == orgOfficeNo
                                      && b.Media_Process_Code == mediaProcessCode
                                      orderby b.Batch_Production_Date, b.Batch_Production_No
                                      select new BatchProdDetailModel
                                      {
                                          BatchProductionNo = b.Batch_Production_No,
                                          BatchProductionDate = b.Batch_Production_Date,
                                          MediaProcessCode = b.Media_Process_Code
                                      }).ToListAsync();
                }
                List<PendingOrderScheduleGrid> pendList = new List<PendingOrderScheduleGrid>();
                foreach (var item in batchSch)
                {


                    var BatSchOrd = await (from bs in _context.BatchScheduleOrderProductions
                                           where bs.Batch_Production_No == item.BatchProductionNo
                                           && !(from m in _context.MediaBatchMaterialDetails
                                                select m.BSOrderProductionNo).ToList().Contains(bs.BS_Order_Production_No)

                                           select new BatchSchOrdProdModel
                                           {
                                               BSOrderProductionNo = bs.BS_Order_Production_No,
                                               BSProductionQtyinUOM = bs.BS_Production_Qty_in_UOM,
                                               FPGradeCode = bs.FP_Grade_Code,
                                               FPGroupCode = bs.FP_Group_Code,
                                               FPVarietyCode = bs.FP_Variety_Code,
                                               PackUOM = bs.Pack_UOM,
                                               PSDirectOrderSchedule_No = bs.PS_Direct_Order_Schedule_No,
                                               PSSalesOrderScheduleNo = bs.PS_Sales_Order_Schedule_No
                                           }).FirstOrDefaultAsync();

                    PendingOrderScheduleGrid pendobj = new PendingOrderScheduleGrid();
                    pendobj.PreserveIn = await (from m in _context.MediaProcessDetails
                                                where m.MediaProcessCode == item.MediaProcessCode
                                                select m.MediaProcessName).FirstOrDefaultAsync();
                    pendobj.OrderDate = item.BatchProductionDate;
                    pendobj.OrderNo = item.BatchProductionNo;
                    pendobj.ProductName = await (from v in _context.ProductDetails
                                                 where v.VarietyCode == BatSchOrd.FPVarietyCode
                                                 select v.VarietyName).FirstOrDefaultAsync();
                    var grdObj = await (from g in _context.gradeDetails
                                        where g.GradeCode == BatSchOrd.FPGradeCode
                                        select new
                                        {
                                            g.GradeFrom,
                                            g.GradeTo
                                        }).FirstOrDefaultAsync();
                    pendobj.Grade = grdObj.GradeFrom.ToString() + "/" + grdObj.GradeTo.ToString();
                    pendobj.TotalQty = BatSchOrd.BSProductionQtyinUOM;
                    //var schno = (from s in _context.BatchScheduleOrderProductions
                    //             where s.Batch_Production_No == item.BatchProductionNo
                    //             select s.PS_Sales_Order_Schedule_No).FirstOrDefault();
                    //var schno = 0;
                    pendobj.BSOrderProdNo = BatSchOrd.BSOrderProductionNo;
                    if (BatSchOrd.PSSalesOrderScheduleNo != null)
                    {
                        //var psScheduleno = (from ps in _context.SalesProductionSchedule
                        //                    where ps.PS_Sales_Order_Schedule_No == schno
                        //                    select ps.Production_Schedule_No).FirstOrDefault();
                        var psScheduleno = 1;
                        pendobj.ReqDate = await (from pss in _context.ProductionScheduleDetails
                                                 where pss.Production_Schedule_No == psScheduleno
                                                 select pss.PS_Require_Date_By).FirstOrDefaultAsync();
                    }
                    else
                    {
                        var psScheduleno = await (from ps in _context.DirectProductionSchedule
                                                  where ps.PS_Direct_Order_Schedule_No == BatSchOrd.PSDirectOrderSchedule_No
                                                  select ps.Production_Schedule_No).FirstOrDefaultAsync();
                        pendobj.ReqDate = await (from pss in _context.ProductionScheduleDetails
                                                 where pss.Production_Schedule_No == psScheduleno
                                                 select pss.PS_Require_Date_By).FirstOrDefaultAsync();
                        pendobj.MediaProcessCode = item.MediaProcessCode;
                    }
                    pendList.Add(pendobj);
                }
                return pendList;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<PendingOrder> SelectPendingOrderSchedule(SelectedPendingOrder Pendobj)
        {
            try
            {
                PendingOrder obj = new PendingOrder();
                int MediaBatchPrepNo = await (from mbp in _context.MediaBatchProductionDetails
                                              select mbp.MediaBatchProductionNo)?.CountAsync();
                if (MediaBatchPrepNo == 0)
                {
                    obj.MediaBatchProductionNo = MediaBatchPrepNo + 1;
                }
                else
                {
                    long MediaMax = await (from mbp in _context.MediaBatchProductionDetails
                                           select mbp.MediaBatchProductionNo)?.MaxAsync();
                    obj.MediaBatchProductionNo = MediaMax + 1;
                }
                obj.MediaBatchProductionVisibleNo = "MB-" + obj.MediaBatchProductionNo.ToString() + DateTime.Now.ToString("yy");
                obj.MediaProcessCode = Pendobj.MediaProcessCode;
                obj.OrgOfficeNo = Pendobj.OrgOfficeNo;
                obj.PendingOrderObj = Pendobj.PendingOrderObj;


                return obj;
            }
            catch (Exception e)
            {
                return null;
            }
        }

    }
}