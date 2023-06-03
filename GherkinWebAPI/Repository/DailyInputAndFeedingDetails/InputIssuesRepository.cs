using GherkinWebAPI.Core.DailyInputAndFeedingDetails;
using GherkinWebAPI.DTO.FeedInputTransfer;
using GherkinWebAPI.Models.DailyInputAndFeedingDetails;
using GherkinWebAPI.Models.PackageOfPracticeModel;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Repository.DailyInputAndFeedingDetails
{
    public class InputIssuesRepository : IInputIssuesRepository
    {
        private readonly RepositoryContext _context;
        public InputIssuesRepository(RepositoryContext context)
        {
            _context = context;
        }

        public async Task<InputIssuesModel> MaterialInputConsumed(DailyInputModel dailyInputModel)
        {
            try
            {
                InputIssuesModel InpModel = new InputIssuesModel();
                InpModel.InputIssuesGrid = new List<InputIssuesGridModel>();
                var HBOMModel = await (from h in _context.packagePracticeDivisions
                                       where h.CropNameCode == dailyInputModel.CropName && h.PSNO == dailyInputModel.PSnumber
                                       select new HBOMPackagePracticeDivision
                                       {
                                           HBOMDivisionFor = h.DivisionFor,
                                           HBOMPracticeNo = h.PracticeNo,
                                           HBOMPracticePerAcre = h.PracticePerAcre,
                                           ListPackage = (from p in _context.packagePracticeMaterials
                                                          where p.PracticeNo == h.PracticeNo
                                                          select p).FirstOrDefault()
                                       }).ToListAsync();
                var FarmerInputConsumption = await (from f in _context.FarmersInputConsumptionDetails
                                                    where f.AreaID == dailyInputModel.AreaId
                                                    && f.PSNumber == dailyInputModel.PSnumber
                                                    && f.CropNameCode == dailyInputModel.CropName
                                                    && f.MIFDateofIssue <= dailyInputModel.DateOfIssue
                                                    select new FarmerInputConsumptionModel
                                                    {
                                                        FarmerCode = f.FarmerCode,
                                                        MIFConsumptionNo = f.MIFConsumptionNo,
                                                        MIFDateofIssue = f.MIFDateofIssue,
                                                        ListFarmer = (from lf in _context.FarmersMaterialIssueDetails
                                                                      where lf.MIFConsumptionNo == f.MIFConsumptionNo
                                                                      select lf).ToList()
                                                    }).FirstOrDefaultAsync();

                var farmerInputConsumptionsGroup = await _context.FarmersMaterialIssueDetails
                                                        .GroupBy(f => f.RawMaterialDetailsCode)
                                                        .Select(group => new { RawMaterialDetailsCode = group.Key, FarmersMaterialIssuedQty = group.Sum(X => X.FarmersMaterialIssuedQty) })
                                                        .ToListAsync();

                var rawmaterialGroupAndDetails = await (from rmd in _context.RawMaterialDetails join
                                                           rmgm in _context.RawMaterialGroupMaster
                                                          on
                                                          rmd.Raw_Material_Group_Code equals rmgm.Raw_Material_Group_Code 
                                                           into gj
                                                           from x in gj.DefaultIfEmpty()
                                                           select new 
                                                           {
                                                               RawMaterialDetailsCode = rmd.Raw_Material_Details_Code,
                                                               RawMaterialGroupCode = rmd.Raw_Material_Group_Code,
                                                               RawMaterialDetails = rmd.Raw_Material_Details_Name,
                                                               RawMaterialGroup = x.Raw_Material_Group,
                                                               FarmersMaterialIssuedQty = 0,
                                                               MIFConsumptionNo = 0,
                                                               MIFMaterialIssueTRNo = 0,
                                                           }

                                                      ).ToListAsync();

                if (FarmerInputConsumption != null)
                {
                    FarmerInputConsumption.ListFarmer = FarmerInputConsumption.ListFarmer.GroupBy(x => x.RawMaterialDetailsCode).Select(x => x.First()).ToList();

                    foreach (var it in FarmerInputConsumption.ListFarmer)
                    {
                        foreach(var item in farmerInputConsumptionsGroup)
                        {
                            if(it.RawMaterialDetailsCode == item.RawMaterialDetailsCode)
                            {
                                it.FarmersMaterialIssuedQty = item.FarmersMaterialIssuedQty;
                            }
                        }
                    }

                   
                }


                foreach (var item in HBOMModel)
                {
                    InputIssuesGridModel obj = new InputIssuesGridModel();
                    obj.POPDivision = item.HBOMDivisionFor;
                    var obj1 = await (from r in _context.RawMaterialGroupMaster
                                      where r.Raw_Material_Group_Code == item.ListPackage.RawmaterialGroupcode
                                      select r.Raw_Material_Group).FirstOrDefaultAsync();
                    var obj2 = await (from r in _context.RawMaterialDetails
                                      where r.Raw_Material_Details_Code == item.ListPackage.Raw_Material_Details_Code
                                      select r.Raw_Material_Details_Name).FirstOrDefaultAsync();
                    obj.GroupMaterialName = obj1.ToString() + "/" + obj2.ToString();
                    obj.TradeName = item.ListPackage.TradeName;
                    obj.POPStdPerUOM = item.ListPackage.Chemicalvolume + "/" + item.ListPackage.ChemicalUOM;
                    if (item.HBOMPracticePerAcre == "HALF ACRE")
                    {
                        obj.QtyReqAcre = 2 * item.ListPackage.Chemicalvolume * dailyInputModel.FarmerAgreementDetail.Farmers_No_of_Acres_Area;
                    }
                    else if (item.HBOMPracticePerAcre == "ONE ACRE")
                    {
                        obj.QtyReqAcre = 1 * item.ListPackage.Chemicalvolume * dailyInputModel.FarmerAgreementDetail.Farmers_No_of_Acres_Area;
                    }
                    if (FarmerInputConsumption != null)
                    {
                        foreach (var item1 in farmerInputConsumptionsGroup)
                        {
                            decimal transfer = 0;
                            if (item1.RawMaterialDetailsCode == item.ListPackage.Raw_Material_Details_Code)
                            {
                                transfer = item1.FarmersMaterialIssuedQty;
                            }


                            obj.TransferedTillDate = obj.TransferedTillDate + transfer;
                        }
                    }
                    else
                    {
                        obj.TransferedTillDate = 0;
                    }

                    obj.ToBeIssueQuantity = obj.QtyReqAcre - obj.TransferedTillDate;
                    obj.RawMaterialDetailCode = item.ListPackage.Raw_Material_Details_Code;
                    obj.RawMaterialGroupCode = item.ListPackage.RawmaterialGroupcode;
                    InpModel.InputIssuesGrid.Add(obj);
                }
                InpModel.AreaCode = await (from a in _context.Areas
                                           where a.Area_ID == dailyInputModel.AreaId
                                           select a.Area_Code).FirstOrDefaultAsync();
                int FarmerCount = await (from fic in _context.FarmersInputConsumptionDetails
                                         select fic.MIFConsumptionNo).CountAsync();
                if (FarmerCount == 0)
                {
                    InpModel.MifConsumptionNumber = FarmerCount + 1;
                }
                else
                {
                    int FarmerMax = await (from fic in _context.FarmersInputConsumptionDetails
                                           select fic.MIFConsumptionNo).MaxAsync();
                    InpModel.MifConsumptionNumber = FarmerMax + 1;
                }

                var dto = InpModel.InputIssuesGrid.ToList();
                 

                //fedd input api result

                var feedgridData = await GetDetailsByCropAndPsNumber(dailyInputModel.CropName, dailyInputModel.PSnumber);

                if(feedgridData!= null)
                {
                    var hbomDetails = feedgridData.FirstOrDefault().HBOMDetails;

                    var combinedModel = (from rmgd in hbomDetails
                                         join d in dto
                                              on
                                              rmgd.rawMaterialDetailsCode equals d.RawMaterialDetailCode
                                         into gj
                                         from x in gj.DefaultIfEmpty()
                                         select new InputIssuesGridModel
                                         {
                                             RawMaterialDetailCode = rmgd.rawMaterialDetailsCode,
                                             RawMaterialGroupCode = rmgd.rawMaterialGroupCode,
                                             GroupMaterialName = rmgd.rawMaterialGroup + '/' + rmgd.RawMaterialDetailsName,
                                             POPDivision =   rmgd.hbOMDivisonFor,
                                             POPStdPerUOM = x == null ? "" : x.POPStdPerUOM,
                                             QtyReqAcre = x == null ? 0 : x.QtyReqAcre,
                                             ToBeIssueQuantity = x == null ? 0 : x.ToBeIssueQuantity,
                                             TradeName = x == null ? "" : x.TradeName,
                                             TransferedTillDate = x == null ? 0 : x.TransferedTillDate,
                                             TransferQty = x == null ? 0 : x.TransferQty
                                         }).ToList();

                    foreach(var ob1 in combinedModel)
                    {
                        foreach(var ob2 in hbomDetails)
                        {
                            if(ob1.RawMaterialDetailCode == ob2.rawMaterialDetailsCode)
                            {
                                ob1.POPStdPerUOM = ob2.hbomChemicalVolume + ob2.hbomChemicalUOM;
                            }
                        }
                    }

                    InpModel.InputIssuesGrid = combinedModel;
                }



                if (InpModel.InputIssuesGrid.Count() > 0)
                {
                //    var firstGroupCode = InpModel.InputIssuesGrid.FirstOrDefault().RawMaterialGroupCode;
                //    combinedModel = all.Where(x => x.RawMaterialGroupCode == firstGroupCode)
                //                    .ToList();

                    //InpModel.InputIssuesGrid = combinedModel;
                    InpModel.InputIssuesGrid =  InpModel.InputIssuesGrid.GroupBy(x => x.RawMaterialDetailCode).Select(x => x.First()).ToList();

                }

              

                return InpModel;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<FarmerInputConAndMatIssueModel> SaveFarmerInputDatails(FarmerInputConAndMatIssueModel ListModel)
        {
            try
            {
                FarmersInputConsumptionDetails FarObj = new FarmersInputConsumptionDetails();
                FarObj.MIFConsumptionVoucherNo = ListModel.FarmersInputConsumptionDetail.MIFConsumptionVoucherNo;
                FarObj.MIFDateofIssue = ListModel.FarmersInputConsumptionDetail.MIFDateofIssue;
                FarObj.AreaID = ListModel.FarmersInputConsumptionDetail.AreaID;
                FarObj.CropNameCode = ListModel.FarmersInputConsumptionDetail.CropNameCode;
                FarObj.EmployeeID = ListModel.FarmersInputConsumptionDetail.EmployeeID;
                FarObj.FarmerCode = ListModel.FarmersInputConsumptionDetail.FarmerCode;
                FarObj.MIFEnteredEmpID = ListModel.FarmersInputConsumptionDetail.MIFEnteredEmpID;
                FarObj.PSNumber = ListModel.FarmersInputConsumptionDetail.PSNumber;
                _context.FarmersInputConsumptionDetails.Add(FarObj);
                await _context.SaveChangesAsync();
                foreach (FarmersMaterialIssueDetails item in ListModel.ListFarmersMaterialIssueDetail)
                {

                    item.MIFConsumptionNo = await (from FIC in _context.FarmersInputConsumptionDetails
                                                   select FIC.MIFConsumptionNo).MaxAsync();
                    _context.FarmersMaterialIssueDetails.Add(item);
                    await _context.SaveChangesAsync();
                }
                ListModel.Status = "Inserted Successfully";
                return ListModel;
            }
            catch (Exception e)
            {
                ListModel.Status = "Insertion Failed";
                return ListModel;
            }
        }


        public async Task<List<HBOMDetailsDto>> GetDetailsByCropAndPsNumber(string cropNameCode, string psNumber)
        {
            List<HBOMDetailsDto> lstHbom = new List<HBOMDetailsDto>();
            HBOMDetailsDto hbom = new HBOMDetailsDto();
            hbom.HBOMDetails = new List<HBOMDetails>();

            hbom.HBOMDetails = await (from ppd in _context.packagePracticeDivisions
                                      join
                    ppm in _context.packagePracticeMaterials on ppd.PracticeNo equals ppm.PracticeNo
                                      join
                    rmd in _context.RawMaterialDetails on ppm.Raw_Material_Details_Code equals rmd.Raw_Material_Details_Code
                                      join
                    rmgd in _context.RawMaterialGroupMaster on ppm.RawmaterialGroupcode equals rmgd.Raw_Material_Group_Code
                  
                                      where ppd.CropNameCode.Equals(cropNameCode) && ppd.PSNO.Equals(psNumber)

                                      select new HBOMDetails()
                                      {
                                          hbOMDivisonFor = ppd.DivisionFor,
                                          hbomPracticePerAcrage = ppd.PracticePerAcre,
                                          hSTransactionCode = ppd.TransCode,
                                          hSCropPhaseCode = ppd.CropphaseCode,
                                          hbomPracticeEffectiveDate = ppd.PracticeEffectiveDate,
                                          hbomPracticeNo = ppd.PracticeNo,
                                          rawMaterialGroupCode = ppm.RawmaterialGroupcode,
                                          rawMaterialDetailsCode = ppm.Raw_Material_Details_Code,
                                          hbomChemicalUOM = ppm.ChemicalUOM,
                                          hbomChemicalVolume = ppm.Chemicalvolume,
                                          rawMaterialGroup = rmgd.Raw_Material_Group,
                                          RawMaterialDetailsName = rmd.Raw_Material_Details_Name 

                                      }).ToListAsync();

            if (hbom.HBOMDetails.Count > 0)
            {
                var groupedData = (from dtails in hbom.HBOMDetails
                                   group dtails by
                                   dtails.rawMaterialDetailsCode into hbomGroupsData
                                   select new
                                   {
                                       rawMaterialDetailsCode = hbomGroupsData.Key,
                                       SumOfhbomChemicalVolume = hbomGroupsData.Sum(x => x.hbomChemicalVolume)
                                   }).ToList();

                hbom.HBOMDetails = hbom.HBOMDetails.GroupBy(l => l.rawMaterialDetailsCode)
                                   .Select(m => m.FirstOrDefault())
                                   .ToList();


                foreach (var d in hbom.HBOMDetails)
                {
                    foreach (var m in groupedData)
                    {
                        if (d.rawMaterialDetailsCode == m.rawMaterialDetailsCode)
                        {
                            d.hbomChemicalVolume = m.SumOfhbomChemicalVolume;
                        }
                    }
                }

            }

            var sumOfMatTransferQty = await (from rmit in _context.InputTransferDetails
                                             join rimtd in _context.RMInputMaterialTransferDetails on
                                               rmit.TransferNumber equals rimtd.rmTransferNo into gj
                                             from x in gj.DefaultIfEmpty()
                                             where rmit.CropNameCode == cropNameCode
                                             group x by x.rawMaterialDetailsCode into k
                                             select new
                                             {
                                                 rawMaterialDetailsCode = k.Key,
                                                 SumOfMatTransferQty = k.Sum(x => x.rmMaterialTransferQty)
                                             }).ToListAsync();

            foreach (var d in hbom.HBOMDetails)
            {
                foreach (var m in sumOfMatTransferQty)
                {
                    if (d.rawMaterialDetailsCode == m.rawMaterialDetailsCode)
                    {
                        d.transferredAmount = m.SumOfMatTransferQty;
                    }
                }
            }

            //hbom.transferredTillDate = hbom.HBOMDetails.Sum(e => e.sumRmMaterialTransferQty);

            lstHbom.Add(hbom);
            return lstHbom;
        }
    }
}