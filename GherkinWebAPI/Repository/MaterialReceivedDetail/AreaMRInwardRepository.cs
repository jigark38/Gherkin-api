using GherkinWebAPI.Core;
using GherkinWebAPI.Core.AreaMaterialReceivedDetails;
using GherkinWebAPI.Models;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Repository
{
    public class AreaMRInwardRepository : RepositoryBase<AreaMRInwardDetails>, IAreaMRInwardRepository
    {
        private RepositoryContext _context;
        public AreaMRInwardRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            this._context = repositoryContext;
        }

        public async Task<bool> CreateAreaDetails(AreaMRInwardPostDetails areaMRInwardDetails)
        {
            try
            {
                var data = new AreaMRInwardDetails
                {
                    Area_ID = areaMRInwardDetails.Area_ID,
                    Area_MRN_No_Visible = areaMRInwardDetails.Area_MRN_No_Visible,
                    Area_MR_Date = areaMRInwardDetails.Area_MR_Date,
                    Crop_Scheme_Code = areaMRInwardDetails.Crop_Scheme_Code,
                    MR_Expenses_Paid = areaMRInwardDetails.MR_Expenses_Paid,
                    MR_Remarks = areaMRInwardDetails.MR_Remarks,
                    OGP_NO = areaMRInwardDetails.OGP_NO,
                    PS_Number = areaMRInwardDetails.PS_Number,
                    RM_Transfer_No = areaMRInwardDetails.RM_Transfer_No
                };

                _context.AreaMRInwardDetails.Add(data);
                _context.SaveChanges();

                foreach (var item in areaMRInwardDetails.AreaMaterialReceivedDetails)
                {
                    item.Area_MR_No = data.Area_MR_No;
                    _context.AreaMaterialReceivedDetails.Add(item);
                    _context.SaveChanges();
                }

                return true;

            }catch(Exception ex){
                return false;
            }
         
        }

        public IEnumerable<AreaMRInwardDetails> GetAllAsync()
        {
            try
            {
                List<AreaMRInwardDetails> _listAreaDetails = _context.AreaMRInwardDetails.ToList();
                return _listAreaDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ARMaterialGridUnderDTO> Getnote2(string areaId, string RMTransferNo)
        {
            var query = (from aa in _context.FieldStaffDetails
                         join bb in _context.Employees on aa.Employee_ID equals bb.employeeId
                         where aa.StaffType.Equals("incharge", StringComparison.OrdinalIgnoreCase)
                                && aa.Area_ID == areaId
                         orderby aa.DateOfEntry descending
                         select new ARMaterialGridUnderDTO
                         {
                             EmployeeName = aa.LoginUserName,
                             EmployeeStatus = aa.StaffType,
                             EmployeeID = aa.Employee_ID,
                             FSEntryDate = aa.DateOfEntry,
                         });
            return query.ToList();
        }

        public async Task<IEnumerable<ARMaterialOutwadDetailsDTO>> GetMaterialOutwadDetailsByAreaId(string areaId)
        {
            try
            {
              var objA = await(from RMitd in _context.InputTransferDetails join
                             RMimtd in _context.RMInputMaterialTransferDetails on 
                             RMitd.TransferNumber equals RMimtd.rmTransferNo join
                             ogp in _context.OutwardGatePassDetails on RMimtd.ogpNo equals ogp.ogpNo join
                             rmgd in _context.RawMaterialGroupMaster on 
                             RMimtd.rawMaterialGroupCode equals rmgd.Raw_Material_Group_Code join
                             rmd in _context.RawMaterialDetails on 
                             RMimtd.rawMaterialDetailsCode equals rmd.Raw_Material_Details_Code join
                             mod in _context.MaterialOutwardDetails on
                             RMitd.TransferNumber equals mod.RmTransferNo
                            into gj from x in gj.DefaultIfEmpty()

                           join
                             amrid in _context.AreaMRInwardDetails on
                             RMitd.TransferNumber equals amrid.RM_Transfer_No
                             into hj from y in gj.DefaultIfEmpty()

                           where RMitd.AreaId == areaId // &&
                             //RMitd.TransferNumber == x.RmTransferNo &&
                             //!RMitd.TransferNumber.Contains(y.RmTransferNo)

                             select new ARMaterialOutwadDetailsDTO
                             {
                                 RMTransferNo = RMitd.TransferNumber,
                                 RMTransferDate = RMitd.TransferDate,
                                 RawMaterialGroupCode = rmgd.Raw_Material_Group_Code,
                                 RawMaterialDetailsCode = rmd.Raw_Material_Details_Code,
                                 RMMaterialTransferQty = RMimtd.rmMaterialTransferQty,
                                 AreaID = RMitd.AreaId,
                                 OGPDate = (x == null ? DateTime.Now : x.OgpDate),
                                 OGPNo = RMimtd.ogpNo, //(x == null ? String.Empty :
                                 OGPId = ogp.Id,
                                 MDDriverName = (x == null ? String.Empty : x.DriverName),
                                 MDDriverContactNo = (x == null ? String.Empty : x.DriverContactNumber),
                                 CropSchemeCode = RMitd.CropSchemeCode,
                                 RawMaterialDetailsName = rmd.Raw_Material_Details_Name,
                                 RawMaterialUOM = rmd.Raw_Material_UOM,
                                 RawMaterialGroup = rmgd.Raw_Material_Group,
                                 DespDate = (x == null ? DateTime.Now : x.DespatchDate)
                             }).ToListAsync();

               var areaMRInwardByArea = _context.AreaMRInwardDetails
                   .Where(x => x.Area_ID == areaId).ToList();

                if(areaMRInwardByArea.Count > 0)
                {
                    objA = objA.Where(x => !areaMRInwardByArea.Any(h => h.RM_Transfer_No == x.RMTransferNo)).ToList(); ;
                }

               var objB = await (from RMitd in _context.InputTransferDetails join
                             RMimtd in _context.RMInputMaterialTransferDetails on 
                             RMitd.TransferNumber equals RMimtd.rmTransferNo join
                             ogp in _context.OutwardGatePassDetails on RMimtd.ogpNo equals ogp.ogpNo join
                             rmgd in _context.RawMaterialGroupMaster on 
                             RMimtd.rawMaterialGroupCode equals rmgd.Raw_Material_Group_Code join
                             rmd in _context.RawMaterialDetails on 
                             RMimtd.rawMaterialDetailsCode equals rmd.Raw_Material_Details_Code join
                             mod in _context.MaterialOutwardDetails on
                             RMitd.TransferNumber equals mod.RmTransferNo
                              into gj from x in gj.DefaultIfEmpty()

                           where x.AreaId == areaId
                           select new ARMaterialOutwadDetailsDTO
                           {
                               RMTransferNo = RMitd.TransferNumber,
                               RMTransferDate = RMitd.TransferDate,
                               RawMaterialGroupCode = rmgd.Raw_Material_Group_Code,
                               RawMaterialDetailsCode = rmd.Raw_Material_Details_Code,
                               RMMaterialTransferQty = RMimtd.rmMaterialTransferQty,
                               AreaID = RMitd.AreaId,

                               OGPDate = (x == null ? DateTime.Now : x.OgpDate),
                               OGPNo =  RMimtd.ogpNo, //(x == null ? String.Empty :
                               OGPId = ogp.Id,
                               MDDriverName = (x == null ? String.Empty : x.DriverName),
                               MDDriverContactNo = (x == null ? String.Empty : x.DriverContactNumber),
                               CropSchemeCode = RMitd.CropSchemeCode,

                               RawMaterialDetailsName = rmd.Raw_Material_Details_Name,
                               RawMaterialUOM = rmd.Raw_Material_UOM,
                               RawMaterialGroup = rmgd.Raw_Material_Group,
                               DespDate = (x == null ? DateTime.Now : x.DespatchDate)
                           }).ToListAsync();

               return objA.Concat(objB).GroupBy(l => l.RMTransferNo)
                                                     .Select(m => m.FirstOrDefault())
                                                     .OrderByDescending(l => l.OGPId)
                                                     .ToList();

                //var rmgroup = (from aa in _context.RawMaterialGroupMaster
                //               join bb in _context.RawMaterialDetails on aa.Raw_Material_Group_Code equals bb.Raw_Material_Group_Code
                //               select new
                //               {
                //                   aa.Raw_Material_Group_Code,
                //                   bb.Raw_Material_Details_Code,
                //                   bb.Raw_Material_Details_Name,
                //                   bb.Raw_Material_UOM,
                //                   aa.Raw_Material_Group
                //               }).ToList();

                //var exceptionList = (from aa in _context.AreaMRInwardDetails
                //                     select aa.RM_Transfer_No).ToList();

                //var qry = (from a in _context.RMInputMaterialTransferDetails
                //           join b in _context.MaterialOutwardDetails on a.rmTransferNo equals b.RmTransferNo
                //           where (!exceptionList.Contains(b.RmTransferNo)) && b.AreaId == areaId
                //           select new ARMaterialOutwadDetailsDTO
                //           {
                //               RMTransferNo = a.rmTransferNo,
                //               RMTransferDate = a.rmTransferDate,
                //               RawMaterialGroupCode = a.rawMaterialGroupCode,
                //               RawMaterialDetailsCode = a.rawMaterialDetailsCode,
                //               RMMaterialTransferQty = a.rmMaterialTransferQty,
                //               AreaID = b.AreaId,
                //               OGPDate = b.OgpDate,
                //               OGPNo = b.OgpNumber,
                //               MDDriverName = b.DriverName,
                //               MDDriverContactNo = b.DriverContactNumber
                //           }).ToList();

                //return (from aa in rmgroup
                //        join qq in qry on aa.Raw_Material_Group_Code equals qq.RawMaterialGroupCode
                //        select new ARMaterialOutwadDetailsDTO
                //        {
                //            RMTransferNo = qq.RMTransferNo,
                //            RMTransferDate = qq.RMTransferDate,
                //            RawMaterialGroupCode = qq.RawMaterialGroupCode,
                //            RawMaterialDetailsCode = qq.RawMaterialDetailsCode,
                //            RMMaterialTransferQty = qq.RMMaterialTransferQty,
                //            AreaID = qq.AreaID,
                //            OGPDate = qq.OGPDate,
                //            OGPNo = qq.OGPNo,
                //            MDDriverName = qq.MDDriverName,
                //            MDDriverContactNo = qq.MDDriverContactNo,
                //            RawMaterialDetailsName = aa.Raw_Material_Details_Name,
                //            RawMaterialUOM = aa.Raw_Material_UOM,
                //            RawMaterialGroup = aa.Raw_Material_Group
                //        }).ToList();



                //if (qry.Any())
                //    return qry.Where(a => a.AreaID == areaId);
                //else
                //    return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<NOTE1> Getnote1(string areaId, string RMTransferNo)
        {
            var query = (from aa in _context.InputTransferDetails
                         join bb in _context.PlantationSchedules on aa.PsNumber equals bb.PsNumber
                         where aa.AreaId == areaId && aa.TransferNumber == RMTransferNo
                         select new NOTE1
                         {
                             PSNumber = aa.PsNumber,
                             RMTransferNo = aa.TransferNumber,
                             SeasonFromDate = bb.PsDate,
                             SeasonToDate = bb.ToDate
                         });
            return query.ToList();
        }

        public async Task<AreaMRInwardDetails> UpdateAreaDetails(int id, AreaMRInwardDetails areaMRInwardDetails)
        {
            try
            {
                AreaMRInwardDetails areameti = new AreaMRInwardDetails();
                areameti = await _context.AreaMRInwardDetails.FirstOrDefaultAsync(x => x.Area_MR_No == id);

                if (areameti != null)
                {
                    // update all required details
                    areameti.Area_MRN_No_Visible = areaMRInwardDetails.Area_MRN_No_Visible;
                    await _context.SaveChangesAsync();
                    return areameti;
                }
                else
                {
                    throw new Exception("Invalid Details");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long GetAreaMRNo()
        {
            long Area_MR_No = (from aa in _context.AreaMRInwardDetails
                               orderby aa.Area_MR_No descending
                               select aa.Area_MR_No).FirstOrDefault();
            if (Area_MR_No == 0) return 1;
            else
                return ++Area_MR_No;
        }
    }
}