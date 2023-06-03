using GherkinWebAPI.Core.ScheduleDetail;
using GherkinWebAPI.DTO.ScheduleDetail;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Repository.ScheduleDetail
{
    public class ScheduleDetailRepository : IScheduleDetailRepository
    {
        private readonly RepositoryContext _context;
        public ScheduleDetailRepository(RepositoryContext context)
        {
            _context = context;
        }

        public async Task<List<ScheduleDetailDTO>> GetPendingOrderScheduleDetails()
        {
            var pendingOrderDetails = from bthDetails in _context.BatchScheduleDetails
                                      join bthOrder in _context.BatchScheduleOrderProductions on bthDetails.Batch_Production_No equals bthOrder.Batch_Production_No
                                      join mediaDetails in _context.MediaProcessDetails on bthDetails.Media_Process_Code equals mediaDetails.MediaProcessCode
                                      join FPDetails in _context.ProductDetails on bthOrder.FP_Variety_Code equals FPDetails.VarietyCode
                                      join FPGrdDetails in _context.gradeDetails on bthOrder.FP_Grade_Code equals FPGrdDetails.GradeCode
                                      join bthOrderProd in _context.BatchScheduleOrderProductions on bthDetails.Batch_Production_No equals bthOrderProd.Batch_Production_No
                                      join psthrSales in _context.SalesProductionSchedule on bthOrderProd.PS_Sales_Order_Schedule_No equals psthrSales.PS_Sales_Order_Schedule_No
                                      join prdSchDetails in _context.ProductionScheduleDetails on psthrSales.Production_Schedule_No equals prdSchDetails.Production_Schedule_No
                                      where !(from s in _context.MediaBatchMaterialDetails select s.BSOrderProductionNo).Contains(bthOrder.BS_Order_Production_No)
                                      select new ScheduleDetailDTO()
                                      {

                                          MediaProcessName = mediaDetails.MediaProcessName,
                                          BatchProductionDate = bthDetails.Batch_Production_Date,
                                          BatchProductionNo = bthDetails.Batch_Production_No,
                                          BS_Order_Production_No = bthOrder.BS_Order_Production_No,
                                          PS_Sales_Order_Schedule_No = bthOrder.PS_Sales_Order_Schedule_No,
                                          PS_Direct_Order_Schedule_No = bthOrder.PS_Direct_Order_Schedule_No,
                                          FP_Group_Code = bthOrder.FP_Group_Code,
                                          FP_Variety_Code = bthOrder.FP_Variety_Code,
                                          FP_Grade_Code = bthOrder.FP_Grade_Code,
                                          Pack_UOM = bthOrder.Pack_UOM,
                                          BS_Production_Qty_in_UOM = bthOrder.BS_Production_Qty_in_UOM,
                                          FP_Variety_Name = FPDetails.VarietyName,
                                          GradeFromTo = FPGrdDetails.GradeFrom + "-" + FPGrdDetails.GradeTo,
                                          Production_Schedule_No = psthrSales.Production_Schedule_No,
                                          PS_Require_Date_By = prdSchDetails.PS_Require_Date_By
                                      };
            return await pendingOrderDetails.ToListAsync();
        }
    }
}