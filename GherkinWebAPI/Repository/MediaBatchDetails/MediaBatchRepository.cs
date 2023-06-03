using GherkinWebAPI.Core.MediaBatchDetails;
using GherkinWebAPI.Models.InputTransferDetails;
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
    public class MediaBatchRepository : IMediaBatchRepository
    {
        private readonly RepositoryContext _context;
        public MediaBatchRepository(RepositoryContext context)
        {
            _context = context;
        }
        public async Task<List<EmployeeIdAndName>> GetAllEmployeeIdAndName()
        {
            try
            {
                var emp = await (from e in _context.Employees
                                 orderby e.employeeName
                                 select new EmployeeIdAndName
                                 {
                                     EmployeeId = e.employeeId,
                                     EmployeeName = e.employeeName
                                 }).ToListAsync();
                return emp;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<List<MediaMaterialDetails>> GetMediaMaterialDetails(DateTime date, string MediaProcessCode, decimal totalQty)
        {
            try
            {
                var mpc = await (from m in _context.productionStandardBOMs
                                 where m.MediaProcessCode == MediaProcessCode && m.EffectiveDate < date
                                 select new
                                 {
                                     m.BOMCode,
                                     m.EffectiveDate,
                                     m.StandardProductionQty,
                                     m.StandardUOM
                                 }).FirstOrDefaultAsync();
                var ppm = await (from p in _context.ProductionProcessMaterialDetails
                                 where p.BOMCode == mpc.BOMCode
                                 select new
                                 {
                                     p.RawMaterialGroupCode,
                                     p.RawMaterialDetailsCode,
                                     p.StandardQunatity,
                                     p.StandardUOM
                                 }).ToListAsync();
                List<MediaMaterialDetails> Listobj = new List<MediaMaterialDetails>();
                foreach (var i in ppm)
                {
                    MediaMaterialDetails obj = new MediaMaterialDetails();
                    obj.MaterialName = await (from r in _context.RawMaterialDetails
                                              where r.Raw_Material_Details_Code == i.RawMaterialDetailsCode
                                              select r.Raw_Material_Details_Name).FirstOrDefaultAsync();
                    obj.StandardQty = i.StandardQunatity + "/" + i.StandardUOM;
                    obj.RequiredQty = totalQty * mpc.StandardProductionQty;
                    Listobj.Add(obj);
                }
                return Listobj;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<List<MediaStockAndBatchDetail>> GetStockAndBatchDetailsFirst(DateTime date)
        {
            try
            {
                List<MediaStockAndBatchDetail> listObj = new List<MediaStockAndBatchDetail>();
                //listObj.firstFilleds = new List<firstFilled>();
                //listObj.secondFields = new List<secondFilled>();
                //firstFilled first = new firstFilled();
                listObj = await (from RSD in _context.RMStockDetails
                                 join RSLD in _context.RMStockLotDetails
                                 on RSD.Stock_No equals RSLD.Stock_No
                                 where RSD.Stock_Date < date
                                 select new MediaStockAndBatchDetail
                                 {
                                     orgOfficeNo = RSD.Org_office_No,
                                     rawMaterialDetailsCode_A = RSD.Raw_Material_Details_Code,
                                     rawMaterialGroupCode_A = RSD.Raw_Material_Group_Code,
                                     rawMaterialTotalAmount = RSD.Raw_Material_Total_Amount,
                                     rawMaterialTotalQty = RSD.Raw_Material_Total_QTY,
                                     rawMaterialUOM = RSD.Raw_Material_UOM,
                                     stockNo = RSD.Stock_No,
                                     stockDate = RSD.Stock_Date,
                                     rmStockTotalDetailQty = RSD.RM_Stock_Total_Detailed_Qty,
                                     rmStockLotGrnAmount = RSLD.RM_Stock_Lot_Grn_Amount,
                                     rmStockLOTGRNDate = RSLD.RM_Stock_LOT_GRN_Date,
                                     rmStockLOTGRNNo = RSLD.RM_Stock_LOT_GRN_No,
                                     rmStockLotGrnQty = RSLD.RM_Stock_Lot_Grn_Qty,
                                     rmStockLotGrnRate = RSLD.RM_Stock_Lot_Grn_Rate,
                                     flag = "A"

                                 }
                    ).ToListAsync();
                foreach (var item in listObj)
                {
                    var a = (from t in _context.MediaBatchMaterialDetails
                             where t.RMStockLOTGRNNo == item.rmStockLOTGRNNo
                             select t.RMMaterialTransferQty).ToList();
                    if (a.Count != 0)
                    {
                        foreach (var i in a)
                        {
                            item.rmMaterialTransferQty += i;
                        }

                    }
                    else
                    {
                        item.rmMaterialTransferQty = 0;
                    }
                    if (item.rmStockLotGrnQty < item.rmMaterialTransferQty)
                    {
                        listObj.Remove(item);
                    }


                }
                //if(listObj.firstFilleds.Count)
                //listObj.flag = "A";
                List<MediaStockAndBatchDetail> listObjB = new List<MediaStockAndBatchDetail>();
                listObjB = await (from RMG in _context.RMGRNDetails
                                  join RTC in _context.RMMaterialTotalCostDetails
                                  on RMG.RMGRNNo equals RTC.rmGrnNo
                                  where RMG.RMGRNDate < date
                                  select new MediaStockAndBatchDetail
                                  {
                                      rawMaterialDetailsCode_B = RTC.rawMaterialDetailsCode,
                                      rawmaterialGroupCode_B = RTC.rawMaterialGroupCode,
                                      rmBatchNo = RTC.rmBatchNo,
                                      rmGrnDate = RMG.RMGRNDate,
                                      rmGRNMaterialWiseTotalCost = RTC.rmGrnMaterialWiseTotalCost,
                                      rmGRNMaterialWiseTotalRate = RTC.rmGrnMaterialwiseTotalRate,
                                      rmGrnNo = RMG.RMGRNNo,
                                      rmGRNreceivedQty = RTC.rmGrnReceivedQty,
                                      flag = "B"

                                  }
                ).ToListAsync();
                foreach (var item in listObjB)
                {
                    var a = await (from t in _context.MediaBatchMaterialDetails
                                   where t.RMGRNNO == item.rmGrnNo
                                   select t.RMMaterialTransferQty).ToListAsync();
                    if (a.Count != 0)
                    {
                        foreach (var i in a)
                        {
                            item.rmMaterialTransferQty += i;
                        }

                    }
                    else
                    {
                        item.rmMaterialTransferQty = 0;
                    }
                    if (item.rmGRNreceivedQty < item.rmMaterialTransferQty)
                    {
                        listObjB.Remove(item);
                    }

                }
                //listObj.flag = "B";
                if (listObjB.Count > 0)
                {
                    foreach (MediaStockAndBatchDetail obj in listObjB)
                    {
                        listObj.Add(obj);
                    }
                }
                return listObj;

            }
            catch (Exception e)
            {

                return null;
            }
        }

        public async Task<MediaBatchProductionAndMaterialDetails> SaveMediaBatchMaterialDetails(MediaBatchProductionAndMaterialDetails obj)
        {
            try
            {
                foreach(var materials in obj.mediaMaterialDetails)
                {
                    _context.MediaBatchMaterialDetails.Add(materials);
                    await _context.SaveChangesAsync();
                }
                
                
                _context.MediaBatchProductionDetails.Add(obj.mediaProductionDetails);
                await _context.SaveChangesAsync();
                obj.status = "Success";
                return obj;
            }
            catch
            {
                obj.status = "Failed";
                return obj;
            }
        }
    }
}