using GherkinWebAPI.Core.CullingDetails;
using GherkinWebAPI.DTO.CullingDetails;
using GherkinWebAPI.Persistence;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace GherkinWebAPI.Repository.CullingDetails
{
    /// <summary>
    /// Defines the <see cref="CullingDetailsRepository" />.
    /// </summary>
    public class CullingDetailsRepository : ICullingDetailsRepository
    {
        /// <summary>
        /// Defines the _context.
        /// </summary>
        private readonly RepositoryContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CullingDetailsRepository"/> class.
        /// </summary>
        /// <param name="context">The context<see cref="RepositoryContext"/>.</param>
        public CullingDetailsRepository(RepositoryContext context)
        {
            _context = context;
        }

        public async Task AddCullingDetails()
        {
            await Task.FromResult(2);
        }

        /// <summary>
        /// The GetGradedMaterialDetails.
        /// </summary>
        /// <param name="orgOfficeNo">The orgOfficeNo<see cref="int"/>.</param>
        /// <returns>The <see cref="Task{List{GradeMaterialDetails}}"/>.</returns>
        public async Task<List<GradeMaterialDetails>> GetGradedMaterialDetails(int orgOfficeNo)
        {
            var schemes = await _context.GreensCullingBarrelsWeightDetails.Select(i => i.CropSchemeCode).ToListAsync();
            var harvests = await _context.GreensCullingBarrelsWeightDetails.Select(i => i.HarvestGrnNo).ToListAsync();
            var cropCodes = await _context.GreensCullingBarrelsWeightDetails.Select(i => i.CropNameCode).ToListAsync();
            var batchProdNumber = await _context.GreensCullingBarrelsWeightDetails.Select(i => i.BatchProductionNo).ToListAsync();
            var gradeCodes = await _context.GreensCullingBarrelsWeightDetails.Select(i => i.FpGradeCode).ToListAsync();

            var query = from ggi in _context.GreensGradingInwardDetails
                        join gghg in _context.GreensGradedHarvestGRNDetails on ggi.Greens_Grade_No equals gghg.Greens_Grade_No
                        join ggq in _context.GreensGradingQuantityDetails on gghg.Greens_Grade_No equals ggq.Greens_Grade_No
                        join crop in _context.Crops on ggq.Crop_Name_Code equals crop.CropCode
                        join batch in _context.BatchScheduleGreensGRNDetails on gghg.Harvest_GRN_No equals batch.Harvest_GRN_No
                        join batchsc in _context.BatchScheduleDetails on batch.Batch_Production_No equals batchsc.Batch_Production_No
                        join batchscOrder in _context.BatchScheduleOrderProductions on batchsc.Batch_Production_No equals batchscOrder.Batch_Production_No
                        join gcbw in _context.GreensCullingBarrelsWeightDetails on batch.Harvest_GRN_No equals gcbw.HarvestGrnNo
                        join mpd in _context.MediaProcessDetails on batchsc.Media_Process_Code equals mpd.MediaProcessCode
                        join fgd in _context.gradeDetails on gcbw.FpGradeCode equals fgd.GradeCode
                        where ggi.Org_office_No != orgOfficeNo && !schemes.Contains(ggq.Crop_Scheme_Code) && !cropCodes.Contains(ggq.Crop_Name_Code)
                        && !harvests.Contains(gghg.Harvest_GRN_No) && !batchProdNumber.Contains(batchscOrder.Batch_Production_No) && !gradeCodes.Contains(batchscOrder.FP_Grade_Code)
                        select new GradeMaterialDetails()
                        {
                            BatchProductionDate = batchsc.Batch_Production_Date,
                            BatchProductionNo = batch.Batch_Production_No,
                            BSGradingQuantity = batch.BS_Grading_Quantity,
                            BsGreensConsumptionNo = batch.BS_Greens_Consumption_No,
                            CropName = crop.Name,
                            CropNameCode = crop.CropCode,
                            CropScheme = ggq.Crop_Scheme_Code,
                            CropSchemeCode = ggq.Crop_Scheme_Code,
                            GradedTotalQuantity = ggi.Graded_Total_Quantity,
                            GreensGradeQtyNo = batch.Greens_Grading_Qty_No,
                            GreensGradingQtyNo = ggq.Greens_Grading_Qty_No,
                            HarvestGrnNo = gghg.Harvest_GRN_No,
                            OrgOfficeNo = orgOfficeNo,
                            QuantityAfterGradingTotal = ggq.Quantity_After_Grading_Total,

                            //TODO
                            //TotalReceivedQty = ggi.Graded_Total_Quantity

                            MediaProcessCode = batchsc.Media_Process_Code,
                            BatchSizeApprox = batchsc.Batch_Size_Approx,
                            SalesOrderScheduleNo = batchscOrder.PS_Sales_Order_Schedule_No,
                            DirectOrderScheduleNo = batchscOrder.PS_Direct_Order_Schedule_No,
                            GradeCode = batchscOrder.FP_Grade_Code,
                            GroupCode = batchscOrder.FP_Group_Code,
                            PackUOM = batchscOrder.Pack_UOM,
                            ProductionQtyinUOM = batchscOrder.BS_Production_Qty_in_UOM,
                            ProductionQtyNos = batchscOrder.BS_Production_Qty_Nos,
                            VarietyCode = batchscOrder.FP_Variety_Code,
                            MediaProcessName = mpd.MediaProcessName,
                            GradeFrom = fgd.GradeFrom,
                            GradeTo = fgd.GradeTo
                        };

            var result = await query.ToListAsync();
            var batchProdnid = result.Select(i => i.BatchProductionNo).Distinct();

            foreach (var id in batchProdnid)
            {
                var sum = await _context.GreensCullingBarrelsWeightDetails.Where(i => i.BatchProductionNo == id).SumAsync(j => j.BarrelMaterialQuantity);
                var order = await _context.BatchScheduleOrderProductions.Where(i => i.Batch_Production_No == id).FirstOrDefaultAsync();
                if (order != null)
                {
                    var quantity = order.BS_Production_Qty_Nos;

                    if (sum >= quantity)
                    {
                        result.RemoveAll(i => i.BatchProductionNo == id);
                    }
                }
            }

            return result;
        }
    }
}