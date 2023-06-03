using GherkinWebAPI.Core.InputToFieldStaff;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Entities;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.InputToFieldStaff;
using GherkinWebAPI.Models.PackageOfPracticeModel;
using GherkinWebAPI.Models.PurchageMgmt;
using GherkinWebAPI.Persistence;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Repository.InputToFieldStaff
{
    public class InputToFieldStaffRepository : IInputToFieldStaffRepository
    {
        private readonly RepositoryContext _context;
        public InputToFieldStaffRepository(RepositoryContext repositoryContext)
        {
            _context = repositoryContext;
        }

        public async Task<string> GenerateMatIssueFSNo()
        {
           
            var item = await _context.InputsIssuedToFieldstaffDetails.AsNoTracking().ToListAsync();
            if (item.Count > 0)
            {
                string lastIdParam = "SELECT MAX(convert(INT, SUBSTRING(Material_Issued_FS_No, CHARINDEX('_',Material_Issued_FS_No)+1,LEN(Material_Issued_FS_No)))) FROM Inputs_Issued_To_Fieldstaff_Details";
                var results = _context.Database.SqlQuery<int>(lastIdParam).FirstOrDefault<int>();
                string increment = (int.Parse(results.ToString()) + 1).ToString();
                return string.Concat("MIFSNo_", increment);
            }
            else
            { // FIRST ENTRY
                return "MIFSNo_1";
            }
        }

        public async Task<string> GetOutwardGatePassNo()
        {

            int? MAXiD = await _context.OutwardGatePassDetails.MaxAsync(e => (int?)e.Id);
            if (MAXiD != null)
            {
                int newId = (int)MAXiD + 1;
                return Convert.ToString(newId);
            }
            else
            { // FIRST ENTRY
                return "1";

            }
        }

        public async Task<bool> Add(Inputs_Issued_To_Fieldstaff_Materials addToMatObj)
        {
            InputsIssuedToFieldstaffMaterials inputsIssuedToFStaffMat = new InputsIssuedToFieldstaffMaterials();

            try
            {
                InputsIssuedToFieldstaffDetails inputsIssued = new InputsIssuedToFieldstaffDetails();

                if (!_context.OutwardGatePassDetails.Any(e => e.ogpNo == addToMatObj.OGP_NO))
                {
                    OutwardGatePassDetail op = new OutwardGatePassDetail();

                    op.ogpNo = addToMatObj.OGP_NO;
                    op.transactionNo = addToMatObj.Material_Issued_FS_No;
                    op.ogpDate = addToMatObj.OGP_Date;
                    _context.OutwardGatePassDetails.Add(op);
                    await _context.SaveChangesAsync();
                }

                if (!_context.InputsIssuedToFieldstaffDetails.Any(e => e.MaterialIssuedFSNo == addToMatObj.Material_Issued_FS_No))
                {

                    inputsIssued.MaterialIssuedFSNo = addToMatObj.Material_Issued_FS_No;
                    inputsIssued.InputsIssuedFSDate = addToMatObj.Inputs_Issued_FS_Date;
                    inputsIssued.OrgofficeNo = addToMatObj.Org_office_No;
                    inputsIssued.AreaID = addToMatObj.Area_ID;
                    inputsIssued.EmployeeID = addToMatObj.Employee_ID;
                    inputsIssued.CropGroupCode = addToMatObj.Crop_Group_Code;
                    inputsIssued.CropNameCode = addToMatObj.Crop_Name_Code;
                    inputsIssued.PSNumber = addToMatObj.PsNumber;
                    inputsIssued.IssuedByEmployeeID = addToMatObj.Issued_By_Employee_ID;
                    inputsIssued.OGPNO = addToMatObj.OGP_NO;

                    _context.InputsIssuedToFieldstaffDetails.Add(inputsIssued);
                    await _context.SaveChangesAsync();
                }


                //Data coming from RMStockDetails
                if (addToMatObj.Stock_No != null && addToMatObj.RM_Batch_No == 0)
                {
                    inputsIssuedToFStaffMat.MaterialIssuedFSNo = addToMatObj.Material_Issued_FS_No;
                    inputsIssuedToFStaffMat.InputsIssuedFSDate = addToMatObj.Inputs_Issued_FS_Date;
                    inputsIssuedToFStaffMat.HBOMPracticeNo = addToMatObj.HBOM_Practice_No;
                    inputsIssuedToFStaffMat.RawMaterialGroupCode = addToMatObj.Raw_Material_Group_Code;
                    inputsIssuedToFStaffMat.RawMaterialDetailsCode = addToMatObj.Raw_Material_Details_Code;
                    inputsIssuedToFStaffMat.StockNo = addToMatObj.Stock_No;
                    inputsIssuedToFStaffMat.RMStockLOTGRNNo = addToMatObj.RM_Stock_LOT_GRN_No;
                    inputsIssuedToFStaffMat.RMStockLotGrnRate = addToMatObj.RM_Stock_Lot_Grn_Rate;
                    inputsIssuedToFStaffMat.FSMaterialIssuedQty = addToMatObj.FS_Material_Issued_Qty;
                    inputsIssuedToFStaffMat.RMMaterialTransferAmount = addToMatObj.RM_Material_Transfer_Amount;
                    inputsIssuedToFStaffMat.OGPNO = addToMatObj.OGP_NO;

                    inputsIssuedToFStaffMat.RMGRNNO = null;
                    inputsIssuedToFStaffMat.RMBatchNo = null;
                    inputsIssuedToFStaffMat.RMGRNMaterialwiseTotalRate = null;

                    _context.InputsIssuedToFieldstaffMaterials.Add(inputsIssuedToFStaffMat);
                    await _context.SaveChangesAsync();
                    return true;

                }
                else // Data coming from RMGRNDetails
                {
                    inputsIssuedToFStaffMat.MaterialIssuedFSNo = addToMatObj.Material_Issued_FS_No;
                    inputsIssuedToFStaffMat.InputsIssuedFSDate = addToMatObj.Inputs_Issued_FS_Date;
                    inputsIssuedToFStaffMat.HBOMPracticeNo = addToMatObj.HBOM_Practice_No;
                    inputsIssuedToFStaffMat.RawMaterialGroupCode = addToMatObj.Raw_Material_Group_Code;
                    inputsIssuedToFStaffMat.RawMaterialDetailsCode = addToMatObj.Raw_Material_Details_Code;

                    inputsIssuedToFStaffMat.StockNo = null;
                    inputsIssuedToFStaffMat.RMStockLOTGRNNo = null;
                    inputsIssuedToFStaffMat.RMStockLotGrnRate = null;

                    inputsIssuedToFStaffMat.FSMaterialIssuedQty = addToMatObj.FS_Material_Issued_Qty;
                    inputsIssuedToFStaffMat.RMMaterialTransferAmount = addToMatObj.RM_Material_Transfer_Amount;
                    inputsIssuedToFStaffMat.OGPNO = addToMatObj.OGP_NO;
                    inputsIssuedToFStaffMat.RMGRNNO = addToMatObj.RM_GRN_NO;
                    inputsIssuedToFStaffMat.RMBatchNo = addToMatObj.RM_Batch_No;
                    inputsIssuedToFStaffMat.RMGRNMaterialwiseTotalRate = addToMatObj.RM_GRN_Materialwise_Total_Rate;

                    _context.InputsIssuedToFieldstaffMaterials.Add(inputsIssuedToFStaffMat);
                    await _context.SaveChangesAsync();
                    return true;
                }

            }
            catch (Exception ex)
            {
                //  return ex.Message();
                return false;
            }


        }



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
        public async Task<List<Area>> GetAllArea()
        {
            return await _context.Areas.OrderBy(area => area.Area_Name).ToListAsync();
        }

        public async Task<List<EmpInfoByHarvestArea>> GetEmpInfoByAreaId(string areaId)
        {
            var details = await (from fsd in _context.FieldStaffDetails
                                 join
emp in _context.Employees on fsd.Employee_ID equals emp.employeeId
                                 where fsd.Area_ID.Equals(areaId) &&
                                 fsd.StaffType.ToLower().Equals("field staff")

                                 select new EmpInfoByHarvestArea
                                 {
                                     Area_ID = fsd.Area_ID,
                                     Employee_ID = emp.employeeId,
                                     Employee_Name = emp.employeeName,
                                     Employee_Status = fsd.StaffType

                                 }).OrderBy(c => c.Employee_Name).ToListAsync();

            return details;
        }

        public async Task<List<CropGroupDetailsByAreaId>> GetCropGroupDetailsByAreaId(string areaId)
        {
            var cropGroupInfo = await (from fad in _context.FarmersAgreementDetails
                                       join
                                          cg in _context.CropGroups on fad.Crop_Group_Code equals cg.CropGroupCode
                                       where fad.Area_ID.Equals(areaId)

                                       select new CropGroupDetailsByAreaId
                                       {
                                           Area_ID = fad.Area_ID,
                                           Crop_Group_Code = cg.CropGroupCode,
                                           Crop_Group_Name = cg.Name

                                       }).Distinct().OrderBy(c => c.Crop_Group_Name).ToListAsync();

            return cropGroupInfo;
        }

        public async Task<List<CropDetailsByGroupCode>> GetCropDetailsByCode(string cropGroupCode)
        {
            var cropDetails = await (from c in _context.Crops
                                     join cg in _context.CropGroups on
            c.CropGroupCode equals cg.CropGroupCode

                                     where c.CropGroupCode.Equals(cropGroupCode)

                                     select new CropDetailsByGroupCode
                                     {
                                         Crop_Group_Code = c.CropGroupCode,
                                         Crop_Group_Name = cg.Name,
                                         Crop_Name_Code = c.CropCode,
                                         Crop_Name = c.Name

                                     }).Distinct().OrderBy(c => c.Crop_Name).ToListAsync();

            return cropDetails;
        }

        public async Task<List<PlantationSchDetailsByAreaID>> GetPlantationSchByCropNameCode(string cropNameCode)
        {

            var schDetails = await (from fad in _context.FarmersAgreementDetails
                                    join ps in _context.PlantationSchedules on fad.PS_Number equals ps.PsNumber

                                    where fad.Area_ID.Equals(cropNameCode)

                                    select new PlantationSchDetailsByAreaID
                                    {
                                        Area_ID = fad.Area_ID,
                                        FromDate = ps.FromDate,
                                        ToDate = ps.ToDate,
                                        seasonFromToDate = "",
                                        PsNumber = ps.PsNumber

                                    }).Distinct().OrderByDescending(c => c.FromDate).ToListAsync();

            schDetails.ForEach(x => x.seasonFromToDate = x.FromDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture)
            + " / " + x.ToDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture));


            return schDetails;
        }

        public async Task<List<HBOMMatDetailsByCropNameCode>> GetHBOMMatDetailsByCropNameCode(string cropNameCode, string psNum)
        {
            var details = await (from ppd in _context.packagePracticeDivisions
                                 join ppm in _context.packagePracticeMaterials
                                      on ppd.PracticeNo equals ppm.PracticeNo
                                 join rmcm in _context.RawMaterialGroupMaster on
                                 ppm.RawmaterialGroupcode equals rmcm.Raw_Material_Group_Code

                                 join rmd in _context.RawMaterialDetails on
                                ppm.RawmaterialGroupcode equals rmd.Raw_Material_Group_Code

                                 where ppd.CropNameCode.Equals(cropNameCode) && ppd.PSNO == psNum
                                 && rmd.Raw_Material_Details_Code.Equals(ppm.Raw_Material_Details_Code)

                                 select new HBOMMatDetailsByCropNameCode
                                 {
                                     Id = rmd.ID,
                                     HBOM_Division_For = ppd.DivisionFor,
                                     Raw_Material_Group = rmcm.Raw_Material_Group,
                                     Raw_Material_Details_Name = rmd.Raw_Material_Details_Name,
                                     HBOM_Practice_Per_Acreage = ppd.PracticePerAcre,
                                     HS_Transaction_Code = ppd.TransCode,
                                     HS_Crop_Phase_Code = ppd.CropphaseCode,
                                     BOM_Practice_Effective_Date = ppd.PracticeEffectiveDate,
                                     HBOM_Practice_No = ppd.PracticeNo,
                                     Crop_Name_Code = ppd.CropNameCode,
                                     PS_Number = ppd.PSNO,
                                     Raw_Material_Group_Code = rmcm.Raw_Material_Group_Code,
                                     Raw_Material_Details_Code = ppm.Raw_Material_Details_Code,
                                     HBOM_Chemical_UOM = ppm.ChemicalUOM,
                                     HBOM_Chemical_Volume = ppm.Chemicalvolume,
                                     Material_Purchases = rmcm.Material_Purchases

                                 }).ToListAsync();

            var list = details.GroupBy(C => C.Raw_Material_Details_Code)
                .Select(G => G.First())
                .ToList();

            return list;
        }

        public async Task<List<RM_Stock_MatDetails_A>> GetRMStockDetails_A(string transferDate, string matGroupCode, string matDetailCode)
        {
            var res_Condition_A = await (from rmsd_ in _context.RMStockDetails 
                                         join rmsld in _context.RMStockLotDetails on
                                         rmsd_.Stock_No equals rmsld.Stock_No 
                                         join rmimtd_ in _context.RMInputMaterialTransferDetails 
                                         on rmsld.Stock_No equals rmimtd_.stockno  
                                         into yk from rmimtd
                                         in yk.DefaultIfEmpty()

                                         select new RM_Stock_MatDetails_A
                                         {
                                             Stock_Date = rmsd_.Stock_Date,
                                             Stock_No = rmsd_.Stock_No,
                                             Org_office_No = rmsd_.Org_office_No,
                                             Raw_Material_Group_Code =  rmsd_.Raw_Material_Group_Code,
                                             Raw_Material_Details_Code = rmsd_.Raw_Material_Details_Code,
                                             Raw_Material_UOM = rmsd_.Raw_Material_UOM,
                                             RM_Stock_Total_Detailed_Qty = rmsd_.RM_Stock_Total_Detailed_Qty,
                                             Raw_Material_Total_QTY = rmsd_.Raw_Material_Total_QTY,
                                             Raw_Material_Total_Amount = rmsd_.Raw_Material_Total_Amount,
                                             RM_Stock_LOT_GRN_Date = rmsld.RM_Stock_LOT_GRN_Date,
                                             RM_Stock_LOT_GRN_No = rmsld.RM_Stock_LOT_GRN_No,
                                             RM_Stock_Lot_Grn_Qty = rmsld.RM_Stock_Lot_Grn_Qty,
                                             RM_Stock_Lot_Grn_Rate = rmsld.RM_Stock_Lot_Grn_Rate,
                                             RM_Stock_Lot_Grn_Amount =  rmsld.RM_Stock_Lot_Grn_Amount,
                                             RM_Transfer_No = (rmimtd == null ? String.Empty : rmimtd.rmTransferNo),
                                             RM_Transfer_Date = (rmimtd == null ? DateTime.Now: rmimtd.rmTransferDate),
                                             RM_Material_Transfer_Qty = (rmimtd == null ? 0 : rmimtd.rmMaterialTransferQty)


                                         })
                       .ToListAsync();

            res_Condition_A = res_Condition_A.Where(x => x.Raw_Material_Group_Code == matGroupCode && 
                                                        x.Raw_Material_Details_Code == matDetailCode && 
                                                        x.Stock_Date <= DateTime.Parse(transferDate)).ToList();


            res_Condition_A= res_Condition_A.GroupBy(t => Convert.ToInt32(t.RM_Stock_LOT_GRN_No)).Select(t => new RM_Stock_MatDetails_A
           {
               Stock_Date = t.FirstOrDefault().Stock_Date,
               Stock_No = t.FirstOrDefault().Stock_No,
               Org_office_No = t.FirstOrDefault().Org_office_No,
               Raw_Material_Group_Code = t.FirstOrDefault().Raw_Material_Group_Code,
               Raw_Material_Details_Code = t.FirstOrDefault().Raw_Material_Details_Code,
               Raw_Material_UOM = t.FirstOrDefault().Raw_Material_UOM,
               RM_Stock_Total_Detailed_Qty = t.FirstOrDefault().RM_Stock_Total_Detailed_Qty,
               Raw_Material_Total_QTY = t.FirstOrDefault().Raw_Material_Total_QTY,
               Raw_Material_Total_Amount = t.FirstOrDefault().Raw_Material_Total_Amount,
               RM_Stock_LOT_GRN_Date = t.FirstOrDefault().RM_Stock_LOT_GRN_Date,
               RM_Stock_LOT_GRN_No = t.FirstOrDefault().RM_Stock_LOT_GRN_No,
               RM_Stock_Lot_Grn_Qty = t.FirstOrDefault().RM_Stock_Lot_Grn_Qty,
               RM_Stock_Lot_Grn_Rate = t.FirstOrDefault().RM_Stock_Lot_Grn_Rate,
               RM_Stock_Lot_Grn_Amount = t.FirstOrDefault().RM_Stock_Lot_Grn_Amount,
               RM_Transfer_No = t.FirstOrDefault().RM_Transfer_No,
               RM_Transfer_Date = t.FirstOrDefault().RM_Transfer_Date,
               RM_Material_Transfer_Qty = t.FirstOrDefault().RM_Material_Transfer_Qty,
               sum_RM_Material_Transfer_Qty = t.Sum(ta => ta.RM_Material_Transfer_Qty)
            }).ToList();

          var inputsToFieldStaffMaterial = await _context.InputsIssuedToFieldstaffMaterials.Where(x => x.RawMaterialDetailsCode == matDetailCode 
                                                                                && x.RawMaterialGroupCode == matGroupCode 
                                                                                && x.StockNo != null && x.RMBatchNo == null)
                                                                                .ToListAsync();


            var group_iFldStaffMat = inputsToFieldStaffMaterial.GroupBy(t => t.RMStockLOTGRNNo).Select(t => new
            {
                group_iFldStaffMat_InputsIssuedFSDate = t.FirstOrDefault().InputsIssuedFSDate,
                group_iFldStaffMat_Mat_DetailCode = t.FirstOrDefault().RawMaterialDetailsCode,
                group_iFldStaffMat_StockNo = t.FirstOrDefault().StockNo,
                group_iFldStaffMat_stockLot_GRN_NO = t.FirstOrDefault().RMStockLOTGRNNo,
                group_iFldStaffMat_Sum_FS_Material_Issued_Qty = t.Sum(ta => ta.FSMaterialIssuedQty)

            }).ToList();


            //group_iFldStaffMat_InputsIssuedFSDate compare with transfer date
            foreach (var ob2 in res_Condition_A)
            {
                if (group_iFldStaffMat.Any(x => x.group_iFldStaffMat_StockNo.Equals(ob2.Stock_No) && 
                x.group_iFldStaffMat_stockLot_GRN_NO.Equals(ob2.RM_Stock_LOT_GRN_No)))
                {
                    ob2.sum_stockNo_FS_Material_Issued_Qty = group_iFldStaffMat.Where(x => x.group_iFldStaffMat_StockNo.Equals(ob2.Stock_No) 
                    && x.group_iFldStaffMat_stockLot_GRN_NO.Equals(ob2.RM_Stock_LOT_GRN_No)).FirstOrDefault().group_iFldStaffMat_Sum_FS_Material_Issued_Qty;

                    ob2.Inputs_Issued_FS_Date = group_iFldStaffMat.Where(x => x.group_iFldStaffMat_StockNo.Equals(ob2.Stock_No)
                    && x.group_iFldStaffMat_stockLot_GRN_NO.Equals(ob2.RM_Stock_LOT_GRN_No)).FirstOrDefault().group_iFldStaffMat_InputsIssuedFSDate;
                }
            }

            // where sum(RM_Stock_Lot_Grn_Qty) > (sum(RM_Material_Transfer_Qty) + sum(FS_Material_Issued_Qty))
            res_Condition_A = res_Condition_A.Where(x => x.RM_Stock_Lot_Grn_Qty > (x.sum_RM_Material_Transfer_Qty + x.sum_stockNo_FS_Material_Issued_Qty)).ToList(); //UPDATED CONDITION

            var res_Condition_B = await (from rgd in _context.RMGRNDetails
                                         join rmmtcd in _context.RMMaterialTotalCostDetails on
                                            rgd.RMGRNNo equals rmmtcd.rmGrnNo 
                                         join rmimtd_ in _context.RMInputMaterialTransferDetails on
                                         rmmtcd.rmGrnNo equals rmimtd_.rmGrnNo into mk from
                                         rmimtd in mk.DefaultIfEmpty()
                                         select new RM_Stock_MatDetails_A
                                         {
                                             Raw_Material_Group_Code = rmmtcd.rawMaterialGroupCode,
                                             Raw_Material_Details_Code = rmmtcd.rawMaterialDetailsCode,

                                             RM_GRN_Date_B = rgd.RMGRNDate,
                                             RM_GRN_No_B = rgd.RMGRNNo,
                                             RM_Batch_No_B = rmmtcd.rmBatchNo,
                                             RM_GRN_Received_Qty_B = rmmtcd.rmGrnReceivedQty,
                                             RM_GRN_Materialwise_Total_Cost_B = rmmtcd.rmGrnMaterialWiseTotalCost,
                                             RM_GRN_Materialwise_Total_Rate_B = rmmtcd.rmGrnMaterialwiseTotalRate,
                                             RM_Transfer_No_B = (rmimtd == null ? String.Empty : rmimtd.rmTransferNo),
                                             RM_Transfer_Date_B = (rmimtd == null ? DateTime.Now : rmimtd.rmTransferDate),
                                             RM_Material_Transfer_Qty_B = (rmimtd == null ? 0 : rmimtd.rmMaterialTransferQty),

                                             RM_Material_Return_Qty = 0 // from Purchase_Return_Material_Details table

                                         }).ToListAsync();

            res_Condition_B = res_Condition_B.Where(x => x.Raw_Material_Group_Code == matGroupCode 
                                                     && x.Raw_Material_Details_Code == matDetailCode
                                                     && x.RM_GRN_Date_B <= DateTime.Parse(transferDate)
                                                     ).ToList(); //&& x.RM_Transfer_Date <= DateTime.Parse(transferDate)


            res_Condition_B = res_Condition_B.GroupBy(t => t.RM_Batch_No_B).Select(t => new RM_Stock_MatDetails_A
            {   Raw_Material_Group_Code = t.FirstOrDefault().Raw_Material_Group_Code,
                Raw_Material_Details_Code = t.FirstOrDefault().Raw_Material_Details_Code,

                RM_GRN_Date_B = t.FirstOrDefault().RM_GRN_Date_B,
                RM_GRN_No_B = t.FirstOrDefault().RM_GRN_No_B,
                RM_Batch_No_B = t.FirstOrDefault().RM_Batch_No_B,
                RM_GRN_Received_Qty_B = t.FirstOrDefault().RM_GRN_Received_Qty_B,
                RM_GRN_Materialwise_Total_Cost_B = t.FirstOrDefault().RM_GRN_Materialwise_Total_Cost_B,
                RM_GRN_Materialwise_Total_Rate_B = t.FirstOrDefault().RM_GRN_Materialwise_Total_Rate_B,
                RM_Transfer_No_B = t.FirstOrDefault().RM_Transfer_No_B,
                RM_Transfer_Date_B = t.FirstOrDefault().RM_Transfer_Date_B,
                RM_Material_Transfer_Qty_B = t.FirstOrDefault().RM_Material_Transfer_Qty_B,
                sum_RM_Material_Transfer_Qty_B = t.Sum(ta => ta.RM_Material_Transfer_Qty_B),
                RM_Material_Return_Qty = 0 // from Purchase_Return_Material_Details table

            }).ToList();



            var inputsToFieldStaffMaterial_B = await _context.InputsIssuedToFieldstaffMaterials.Where(x => x.RawMaterialDetailsCode == matDetailCode
                                                                      && x.RawMaterialGroupCode == matGroupCode
                                                                      && x.StockNo == null && x.RMBatchNo != null)
                                                                      .ToListAsync();


            var group_iFldStaffMat_B = inputsToFieldStaffMaterial_B.GroupBy(t => t.RMBatchNo).Select(t => new
            {
                group_iFldStaffMat_InputsIssuedFSDate = t.FirstOrDefault().InputsIssuedFSDate,
                group_iFldStaffMat_Mat_DetailCode = t.FirstOrDefault().RawMaterialDetailsCode,
                group_iFldStaffMat_BatchNo = t.FirstOrDefault().RMBatchNo,
                group_iFldStaffMat_GRN_NO = t.FirstOrDefault().RMGRNNO,
                group_iFldStaffMat_Sum_FS_Material_Issued_Qty = t.Sum(ta => ta.FSMaterialIssuedQty)

            }).ToList();


            foreach (var ob2 in res_Condition_B)
            {
                if (group_iFldStaffMat_B.Any(x => x.group_iFldStaffMat_BatchNo.Equals(ob2.RM_Batch_No_B) &&
                x.group_iFldStaffMat_GRN_NO.Equals(ob2.RM_GRN_No_B)))
                {
                    ob2.sum_BatchNo_FS_Material_Issued_Qty = group_iFldStaffMat_B.Where(x => x.group_iFldStaffMat_BatchNo.Equals(ob2.RM_Batch_No_B)
                    && x.group_iFldStaffMat_GRN_NO.Equals(ob2.RM_GRN_No_B)).FirstOrDefault().group_iFldStaffMat_Sum_FS_Material_Issued_Qty;

                    ob2.Inputs_Issued_FS_Date = group_iFldStaffMat_B.Where(x => x.group_iFldStaffMat_BatchNo.Equals(ob2.RM_Batch_No_B)
                    && x.group_iFldStaffMat_GRN_NO.Equals(ob2.RM_GRN_No_B)).FirstOrDefault().group_iFldStaffMat_InputsIssuedFSDate;
                }
            }

                var purchaseReturnsMaterialDetails = await (from prmd in _context.PurchageReturnMaterialDetails where
                                                            prmd.rawMaterialDetailsCode == matDetailCode &&
                                                            prmd.rawMaterialGroupCode == matGroupCode
                                                            group prmd by 1 into g 

                                                            select new { 
                                                                TransferDate = (g.Select(x => x.rmTransferDate != null ? x.rmTransferDate : DateTime.Now)),
                                                            RM_GRN_NO = (g.Select(x => x.rmGrnNo)),
                                                            RM_BATCH_NO = (g.Select(x => x.rmBatchNo)),
                                                            SUMMatReturnQty = g.Sum(x => x.rmMaterialReturnQty)
                                                            })
                                                            .ToListAsync();

            if (purchaseReturnsMaterialDetails.Count>0)
            {
                foreach (var ob3 in res_Condition_B)
                {
                    if (purchaseReturnsMaterialDetails.Any(x => x.RM_BATCH_NO.Equals(ob3.RM_Batch_No_B) &&
                    x.RM_GRN_NO.Equals(ob3.RM_GRN_No_B)))
                    {
                        ob3.RM_Material_Return_Qty = (decimal)purchaseReturnsMaterialDetails.Where(x => x.RM_BATCH_NO.Equals(ob3.RM_Batch_No_B)
                        && x.RM_GRN_NO.Equals(ob3.RM_GRN_No_B)).FirstOrDefault().SUMMatReturnQty;
                    }
                }                
            }
            //Purchase_Return_Material_Details
            res_Condition_B = res_Condition_B.Where(x => x.RM_GRN_Received_Qty_B > (x.sum_RM_Material_Transfer_Qty_B + x.sum_BatchNo_FS_Material_Issued_Qty + x.RM_Material_Return_Qty)).ToList();

            var unionResult = res_Condition_A.Concat(res_Condition_B).ToList();

            return unionResult;
        }

        public async Task<List<RawMaterialDetails>> GetAllRawMaterialDetails()
        {
            return await _context.RawMaterialDetails.ToListAsync();
        }

        public async Task<List<RawMaterialMaster>> GetAllRawMaterialGroups()
        {
            return await _context.RawMaterialGroupMaster.ToListAsync();
        }
    }
}