using GherkinWebAPI.Core.FarmerAccountDetailsFinalization;
using GherkinWebAPI.DTO.FarmersAccountSettlementDetail;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.FarmerAccountDetailsFinalization;
using GherkinWebAPI.Persistence;
using GherkinWebAPI.Response;
using GherkinWebAPI.Utilities;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Repository.FarmerAccountDetailsFinalization
{
    public class FarmerAccountDetailsFinalizationRepository : RepositoryBase<FarmersAccountSettlementDetail>, IFarmerAccountDetailsFinalizationRepository
    {
        private RepositoryContext _context;
        public FarmerAccountDetailsFinalizationRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            _context = repositoryContext;
        }

        public async Task<List<FarmerNameAccountVM>> SearchAgreement(FarmerAccountSettlementSearchDTO settlementSearchDTO)
        {
            List<FarmerNameAccountVM> farmerDetailsList = new List<FarmerNameAccountVM>();
            //settlementSearchDTO.SeasonFromTo = "PSNO_2";
            //settlementSearchDTO.AreaId = "CAC_14";
            //settlementSearchDTO.CropGroup = "CGC_1";
            //settlementSearchDTO.CropName = "CNC_1";
            //settlementSearchDTO.FieldStaffId = "55";

            try
            {
                if (settlementSearchDTO.FarmerAccountNo == null)
                {
                    var test = await _context.FarmersAgreementDetails.Where(x => x.Crop_Group_Code == settlementSearchDTO.CropGroup && x.Crop_Name_Code == settlementSearchDTO.CropName && x.PS_Number == settlementSearchDTO.SeasonFromTo && x.Area_ID == settlementSearchDTO.AreaId && x.Employee_ID == settlementSearchDTO.FieldStaffId).ToListAsync();

                    farmerDetailsList = (from s in test
                                         join fid in _context.Farmers on s.Farmer_Code equals fid.Farmer_Code
                                         where !_context.FarmersAccountSettlementDetails.Any(es => (es.Farmers_Account_No == s.Farmers_Account_No))
                                         select new FarmerNameAccountVM()
                                         {
                                             farmerName = fid.FarmerName,
                                             farmerAccountNo = s.Farmers_Account_No,
                                             farmerAgreementCode = s.Farmers_Agreement_Code,
                                             farmerCode = s.Farmer_Code,
                                             noOfAcres = s.Farmers_No_of_Acres_Area

                                         }).ToList();

                }
                else
                {
                    var test = await _context.FarmersAgreementDetails.Where(x => x.Crop_Group_Code == settlementSearchDTO.CropGroup && x.Crop_Name_Code == settlementSearchDTO.CropName && x.PS_Number == settlementSearchDTO.SeasonFromTo && x.Area_ID == settlementSearchDTO.AreaId && x.Employee_ID == settlementSearchDTO.FieldStaffId && x.Farmers_Account_No == settlementSearchDTO.FarmerAccountNo).ToListAsync();

                    farmerDetailsList = (from s in test
                                         join fid in _context.Farmers on s.Farmer_Code equals fid.Farmer_Code
                                         where !_context.FarmersAccountSettlementDetails.Any(es => (es.Farmers_Account_No == s.Farmers_Account_No))
                                         select new FarmerNameAccountVM()
                                         {
                                             farmerName = fid.FarmerName,
                                             farmerAccountNo = s.Farmers_Account_No,
                                             farmerAgreementCode = s.Farmers_Agreement_Code,
                                             farmerCode = s.Farmer_Code,
                                             noOfAcres = s.Farmers_No_of_Acres_Area
                                         }).ToList();
                }

                decimal cropRateAsperAssociation = (from dd in _context.CropRates
                                                    join cc in _context.CropAssociationRates on dd.Crop_Rate_No equals cc.Crop_Rate_No
                                                    where dd.CropGroupCode == settlementSearchDTO.CropGroup &&
                                                    dd.CropGroupName == settlementSearchDTO.CropName &&
                                                    dd.PSNumber == settlementSearchDTO.SeasonFromTo &&
                                                    dd.AreaId == settlementSearchDTO.AreaId
                                                    select cc).ToList().Sum(x => x.CropRateAsperAssociation);

                decimal farmerMaterialRate = (from dd in _context.FarmersInputsMaterialRates
                                              join cc in _context.FarmersInputsAreaDetails on dd.FIRatePassingNo equals cc.FIRatePassingNo
                                              where cc.AreaID == settlementSearchDTO.AreaId && cc.PSNumber == settlementSearchDTO.SeasonFromTo
                                              select dd.FarmerMaterialRate).FirstOrDefault();

                foreach (var item in farmerDetailsList)
                {
                    //greens Calculation
                    decimal greensSum = (from dd in _context.GreensFarmersDetails
                                         join cc in _context.FarmersAgreementDetails on dd.FarmerCode equals cc.Farmer_Code
                                         where cc.Farmer_Code == item.farmerCode
                                         select dd).ToList().Sum(x => x.CountwiseTotalQuantity);

                    if (!cropRateAsperAssociation.Equals(0))
                        item.greensReceived = greensSum * cropRateAsperAssociation;
                    else
                    {
                        decimal cropRateAsPerAgreement = (from dd in _context.FarmersAgreementSizeDetails
                                                          where dd.Farmers_Agreement_Code == item.farmerAgreementCode
                                                          select dd).ToList().Sum(x => x.Crop_Rate_As_per_Our_Agreement);
                        item.greensReceived = greensSum * cropRateAsPerAgreement;
                    }
                    //Inputs Issued
                    decimal farmerMaterialIssuedQtySum = (from dd in _context.FarmersInputConsumptionDetails
                                                          join cc in _context.FarmersMaterialIssueDetails on dd.MIFConsumptionNo equals cc.MIFConsumptionNo
                                                          where dd.PSNumber == settlementSearchDTO.SeasonFromTo && dd.FarmerCode == item.farmerCode
                                                          select cc).ToList().Sum(x => x.FarmersMaterialIssuedQty);



                    item.InputsIssued = farmerMaterialRate != 0 ? farmerMaterialIssuedQtySum * farmerMaterialRate : 0;
                    //advance Amount
                    decimal advancedCashIssued = (from dd in _context.AdvanceCashIssuedToFarmers
                                                  where dd.FarmersAccountNo == item.farmerAccountNo && dd.FarmerCode == item.farmerCode
                                                  select dd).ToList().Sum(x => x.AdvanceAmount);
                    item.advanceAmount = advancedCashIssued;

                    //Input Returned
                    decimal farmerMaterialReturnQtySum = (from dd in _context.FarmersInputsMaterialMaster
                                                          join cc in _context.FarmersInputsMaterialDetail on dd.FIMReturnNo equals cc.FIMReturnNo
                                                          where dd.PSNumber == settlementSearchDTO.SeasonFromTo && dd.FarmerCode == item.farmerCode
                                                          select cc).ToList().Sum(x => x.FarmersMaterialReturnQty);

                    item.inputReturn = farmerMaterialRate != 0 ? farmerMaterialReturnQtySum * farmerMaterialRate : 0;
                    //payable
                    item.payable = item.greensReceived - item.inputReturn;

                }


                return farmerDetailsList;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<List<GreensReceivingDetailsVM>> GetSettlementDetails(FarmerAccountSettlementSearchDTO settlementSearchDTO)
        {
            {
                List<GreensReceivingDetailsVM> greensList = new List<GreensReceivingDetailsVM>();
                settlementSearchDTO.SeasonFromTo = "PSNO_2";
                settlementSearchDTO.AreaId = "CAC_14";
                settlementSearchDTO.CropGroup = "CGC_1";
                settlementSearchDTO.CropName = "CNC_1";
                settlementSearchDTO.FieldStaffId = "5";

                try
                {
                    greensList = await (from dd in _context.GreensFarmersDetails
                                        join cc in _context.GreensProcurements on dd.GreensProcurementNo equals cc.GreensProcurementNo
                                        where dd.FarmerCode == settlementSearchDTO.FarmerCode && dd.PSNumber == settlementSearchDTO.SeasonFromTo
                                        select new GreensReceivingDetailsVM
                                        {
                                            harvestDate = cc.HarvestDate,
                                            cropSchemeCode = dd.CropSchemeCode,
                                            greensProcurementNo = dd.GreensProcurementNo
                                        }).ToListAsync();

                    decimal cropRateAsperAssociation = (from dd in _context.CropRates
                                                        join cc in _context.CropAssociationRates on dd.Crop_Rate_No equals cc.Crop_Rate_No
                                                        where dd.CropGroupCode == settlementSearchDTO.CropGroup &&
                                                        dd.CropGroupName == settlementSearchDTO.CropName &&
                                                        dd.PSNumber == settlementSearchDTO.SeasonFromTo &&
                                                        dd.AreaId == settlementSearchDTO.AreaId
                                                        select cc).ToList().Sum(x => x.CropRateAsperAssociation);

                    decimal cropRateAsPerAgreement = (from dd in _context.FarmersAgreementSizeDetails
                                                      where dd.Farmers_Agreement_Code == settlementSearchDTO.FarmersAgreementCode
                                                      select dd).ToList().Sum(x => x.Crop_Rate_As_per_Our_Agreement);

                    foreach (var item in greensList)
                    {
                        if (!cropRateAsperAssociation.Equals(0))
                            item.rate = cropRateAsperAssociation;
                        else
                            item.rate = cropRateAsPerAgreement;

                        item.count = (from dd in _context.CropSchemes
                                      where dd.CropCode == item.cropSchemeCode
                                      select new GreensCountVM
                                      {
                                          From = dd.From,
                                          Sign = dd.Sign,
                                          Count = dd.Count
                                      }).FirstOrDefault();

                    }

                    return greensList;
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }

        public async Task<ApiResponse<object>> CreateAgreement(FarmersAccountSettlementDetail farmersAccountSettlementDetail)
        {
            ApiResponse<object> result = new ApiResponse<object>();
            try
            {

                if (farmersAccountSettlementDetail != null)
                {
                    var newRecord = _context.FarmersAccountSettlementDetails.Add(farmersAccountSettlementDetail);
                    result.Data = await _context.SaveChangesAsync();
                    result.IsSucceed = true;
                }
            }
            catch (Exception ex)
            {
                result.IsSucceed = false;
                result.Exception = ex;
                result.ErrorMessages = new List<string>();
                result.ErrorMessages.Add(ex.Message);
            }

            return result;
        }

        public async Task<List<FarmerAdvancesPaidGRID>> GetFarmerAdvanceDetails(FarmerAccountSettlementSearchDTO farmerDetailsFilterModel)
        {
            try
            {
                var FarmerAdvancesPaidGRID = await (from f in _context.Farmers
                                                    join
                                                     acitd in _context.AdvanceCashIssuedToFarmers on
                                                     f.Farmer_Code equals acitd.FarmerCode
                                                     into temp
                                                    from x in temp.DefaultIfEmpty()
                                                    where f.Farmer_Code == farmerDetailsFilterModel.FarmerCode


                                                    select new FarmerAdvancesPaidGRID
                                                    {
                                                        FarmerName = f.FarmerName,
                                                        Farmer_Code = f.Farmer_Code,
                                                        NameOfAccountHolder = f.FarmerName,
                                                        Farmers_No_of_Acres_Area = f.NoOfAcres,
                                                        Farmers_Account_No = f.BankAccountNo,
                                                        FarmerAddress = f.Farmer_Address,
                                                        Village_Code = f.Village_Code,
                                                        VillageName = _context.Villages.Where(x => x.Village_Code == f.Village_Code).FirstOrDefault().Village_Name,
                                                        IFSC = f.BankIFSC,
                                                        Amount = x == null ? 0 : x.AdvanceAmount, //AdvanceAmount
                                                        CumulativeAmount = 0, //tbd

                                                        Date = x == null ? DateTime.Now : x.ACEntryDate

                                                    }).ToListAsync();



                return FarmerAdvancesPaidGRID;

            }


            catch (Exception ex)
            {
                throw;
                return new List<FarmerAdvancesPaidGRID>();
            }

        }

        public async Task<List<FarmerInputReturnGRID>> GetFarmerInputsReturnDetails(FarmerAccountSettlementSearchDTO settlementSearchDTO)
        {
            try
            {
                var inputReturnGrid = await (from f in _context.Farmers
                                             join
                                  fimm in _context.FarmersInputsMaterialMaster on
                                  f.Farmer_Code equals fimm.FarmerCode
                                             join
                                  fimd in _context.FarmersInputsMaterialDetail on
                                   fimm.FIMReturnNo equals fimd.FIMReturnNo
                                             where
                                        fimm.FarmerCode == settlementSearchDTO.FarmerCode &&
                                        fimm.PSNumber == settlementSearchDTO.PsNumber

                                             select new
                                             FarmerInputReturnGRID
                                             {
                                                 FIM_Return_No = fimm.FIMReturnNo,
                                                 Area_ID = fimm.AreaID,
                                                 FMIR_Date = fimm.FMIRDate,
                                                 Employee_ID = fimm.EmployeeID,
                                                 Crop_Group_Code = fimm.CropGroupCode,
                                                 Crop_Name_Code = fimm.CropNameCode,
                                                 PS_Number = fimm.PSNumber,
                                                 Farmer_Code = fimm.FarmerCode,
                                                 FIM_Return_Voucher_No = fimm.FIMReturnVoucherNo,
                                                 Stock_Return_Status = fimm.StockReturnStatus,
                                                 Org_office_No = fimm.FIMReturnNo,

                                                 FIM_Return_TR_No = fimd.FIMReturnTRNo,
                                                 Raw_Material_Group_Code = fimd.RawMaterialGroupCode,
                                                 Raw_Material_Details_Code = fimd.RawMaterialDetailsCode,
                                                 Farmers_Material_Return_Qty = fimd.FarmersMaterialReturnQty,
                                                 Raw_Material_Group_Name = _context.RawMaterialGroupMaster.Where(x => x.Raw_Material_Group_Code == fimd.RawMaterialGroupCode).FirstOrDefault().Raw_Material_Group,
                                                 Raw_Material_Details_Name = _context.RawMaterialDetails.Where(x => x.Raw_Material_Details_Code == fimd.RawMaterialDetailsCode).FirstOrDefault().Raw_Material_Details_Name
                                             }).ToListAsync();

                return inputReturnGrid;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<FarmerInputsIssuedGRID>> GetFarmerInputsIssuedDetails(FarmerAccountSettlementSearchDTO settlementSearchDTO)
        {
            try
            {
                var farmerInputsIssuedGRID = await (from ficd in _context.FarmersInputConsumptionDetails
                                                    join
fmid in _context.FarmersMaterialIssueDetails on
ficd.MIFConsumptionNo equals fmid.MIFConsumptionNo
                                                    where
                                                    ficd.PSNumber == settlementSearchDTO.PsNumber &&
                                                    ficd.FarmerCode == settlementSearchDTO.FarmerCode

                                                    select new FarmerInputsIssuedGRID
                                                    {
                                                        MIF_Date_of_Issue = ficd.MIFDateofIssue,
                                                        Employee_ID = ficd.EmployeeID,
                                                        MIF_Consumption_No = ficd.MIFConsumptionNo,
                                                        MIF_Consumption_Voucher_No = ficd.MIFConsumptionVoucherNo,
                                                        MIF_Material_Issue_TR_No = fmid.MIFMaterialIssueTRNo,
                                                        Raw_Material_Group_Code = fmid.RawMaterialGroupCode,
                                                        Raw_Material_Details_Code = fmid.RawMaterialDetailsCode,
                                                        Raw_Material_Group_Name = _context.RawMaterialGroupMaster.Where(x => x.Raw_Material_Group_Code == fmid.RawMaterialGroupCode).FirstOrDefault().Raw_Material_Group,
                                                        Raw_Material_Details_Name = _context.RawMaterialDetails.Where(x => x.Raw_Material_Details_Code == fmid.RawMaterialDetailsCode).FirstOrDefault().Raw_Material_Details_Name
                                                    }).ToListAsync();

                var Inputs_Area_and_Material_Rates = await (from fiad in _context.FarmersInputsAreaDetails
                                                            join
                                                             fimr in _context.FarmersInputsMaterialRates on
                                                             fiad.FIRatePassingNo equals fimr.FIRatePassingNo
                                                            where
                                                            fiad.AreaID == settlementSearchDTO.AreaId &&
                                                            fiad.PSNumber == settlementSearchDTO.PsNumber
                                                            select new
                                                            {
                                                                FI_Rate_Passing_No = fiad.FIRatePassingNo,
                                                                PS_Number = fiad.PSNumber,
                                                                Area_ID = fiad.AreaID,
                                                                Farmers_Rates_Area_ID = fiad.FarmersRatesAreaID,
                                                                Raw_Material_Group_Code = fimr.RawMaterialGroupCode,
                                                                Raw_Material_Details_Code = fimr.RawMaterialDetailsCode,
                                                                Material_Rate_ID = fimr.MaterialRateID,
                                                                Raw_Material_UOM = fimr.RawMaterialUOM,
                                                                Farmer_Material_Rate = fimr.FarmerMaterialRate
                                                            }).ToListAsync();



                foreach (var farmerissued in farmerInputsIssuedGRID)
                {
                    foreach (var areaMatRates in Inputs_Area_and_Material_Rates)
                    {
                        if (farmerissued.Raw_Material_Group_Code == areaMatRates.Raw_Material_Group_Code &&
                              farmerissued.Raw_Material_Details_Code == areaMatRates.Raw_Material_Details_Code)
                        {
                            farmerissued.Area_ID = areaMatRates.Area_ID;
                            farmerissued.FI_Rate_Passing_No = areaMatRates.FI_Rate_Passing_No;
                            farmerissued.Farmers_Rates_Area_ID = areaMatRates.Farmers_Rates_Area_ID;
                            farmerissued.Material_Rate_ID = areaMatRates.Material_Rate_ID;
                            farmerissued.Raw_Material_UOM = areaMatRates.Raw_Material_UOM;
                            farmerissued.Farmer_Material_Rate = areaMatRates.Farmer_Material_Rate;
                        }
                    }
                }

                return farmerInputsIssuedGRID;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<FarmerAgreementAndSettlementInfo>> GetFarmerAgreementAndSettlementInfo(FarmerAccountSettlementSearchDTO settlementSearchDTO)
        {
            //note i
            var farmerAgreementAndSettlementInfo = await _context.FarmersAgreementDetails.Where(x =>
                                                x.Crop_Group_Code == settlementSearchDTO.CropGroup &&
                                                x.Crop_Name_Code == settlementSearchDTO.CropName &&
                                                x.Area_ID == settlementSearchDTO.AreaId &&
                                                x.PS_Number == settlementSearchDTO.PsNumber).ToListAsync();

            var accountSettledFarmerDetails = await _context.FarmersAccountSettlementDetails.Where(x => x.Farmer_Code != null).ToListAsync();

            var farmerDetailsNotSettled = farmerAgreementAndSettlementInfo.Where(x => !accountSettledFarmerDetails.Any(y => y.Farmer_Code == x.Farmer_Code));

            var data = (from d in farmerDetailsNotSettled
                        join
fid in _context.Farmers on
d.Farmer_Code equals fid.Farmer_Code
                        select new FarmerAgreementAndSettlementInfo
                        {
                            FarmerName = fid.FarmerName,
                            Farmers_Account_No = d.Farmers_Account_No,
                            Farmer_Code = d.Farmer_Code,
                            Farmers_Agreement_Code = d.Farmers_Agreement_Code,
                            Farmers_Agreement_Date = d.Farmers_Agreement_Date,
                            Farmers_No_of_Acres_Area = d.Farmers_No_of_Acres_Area,
                            Village_Code = Convert.ToString(d.Village_Code)
                        }).ToList();

            return data;


        }

        public async Task<List<FarmerGreenReceivingGRID>> GetFarmerGreensReveivingDetails(FarmerAccountSettlementSearchDTO settlementSearchDTO)
        {
            var farmerAgreementAndSettlementInfoData = await GetFarmerAgreementAndSettlementInfo(settlementSearchDTO);

            var greenFarmerData = await (from gfd in _context.GreensFarmersDetails
                                         join
                                            gpd in _context.GreensProcurements on
                                            gfd.GreensProcurementNo equals gpd.GreensProcurementNo
                                         where
                                            gfd.FarmerCode == settlementSearchDTO.FarmerCode &&
                                            gfd.PSNumber == settlementSearchDTO.PsNumber
                                         select new FarmerGreenReceivingGRID
                                         {
                                             Farmer_Code = gfd.FarmerCode,
                                             Greens_Procurement_No = gfd.GreensProcurementNo,
                                             Crop_Scheme_Code = gfd.CropSchemeCode,
                                             Count_wise_Total_Crates = gfd.CountwiseTotalCrates,
                                             Count_wise_Total_Quantity = gfd.CountwiseTotalQuantity,
                                             Harvest_Date = gpd.HarvestDate,


                                             All_Areas = _context.CropRates.Where(x => x.CropGroupCode == gfd.CropGroupCode && x.CropGroupName == gfd.CropNameCode && x.PSNumber == gfd.PSNumber).FirstOrDefault().AllAreas,
                                             All_Villages = _context.CropRates.Where(x => x.CropGroupCode == gfd.CropGroupCode && x.CropGroupName == gfd.CropNameCode && x.PSNumber == gfd.PSNumber).FirstOrDefault().AllVillages,
                                             Village_Code = _context.CropRates.Where(x => x.CropGroupCode == gfd.CropGroupCode && x.CropGroupName == gfd.CropNameCode && x.PSNumber == gfd.PSNumber).FirstOrDefault().VillageCode,
                                             Crop_Rate_No = _context.CropRates.Where(x => x.CropGroupCode == gfd.CropGroupCode && x.CropGroupName == gfd.CropNameCode && x.PSNumber == gfd.PSNumber).FirstOrDefault().Crop_Rate_No
                                         }
                                         ).ToListAsync();


            //List<FarmerGreenReceivingGRID> data = new List<FarmerGreenReceivingGRID>();
            //foreach (var item in greenFarmerData)
            //{
            //    var check = data.Where(x => x.Greens_Procurement_No == item.Greens_Procurement_No && x.Crop_Scheme_Code == item.Crop_Scheme_Code).FirstOrDefault();
            //    if (check != null)
            //    {
            //        check.Count_wise_Total_Crates = check.Count_wise_Total_Crates + item.Count_wise_Total_Crates;

            //        check.Count_wise_Total_Quantity = check.Count_wise_Total_Quantity + item.Count_wise_Total_Quantity;
            //    }
            //    else
            //    {
            //        data.Add(item);
            //    }

            //}

            //if (data?.Any() ?? false)
            //{
            //    greenFarmerData = data;
            //}




            var cropAssociationRates = (from gfd in greenFarmerData
                                        join
                cear in _context.CropAssociationRates on
                 gfd.Crop_Rate_No equals cear.Crop_Rate_No
                                        where
                                        gfd.Farmer_Code == settlementSearchDTO.FarmerCode
                                        select new
                                        {
                                            Farmer_Code = gfd.Farmer_Code,
                                            Crop_Scheme_Code = cear.CropSchemeCode,
                                            Crop_Rate_As_per_Association = cear.CropRateAsperAssociation,
                                            Crop_Rate_Per_UOM = cear.CropRatePerUOM
                                        }).ToList();
            //   var distinctCropSchemeCodes = cropAssociationRates.GroupBy(x => new { x.Crop_Scheme_Code }).Select(x => x.FirstOrDefault()).OrderByDescending(x => x.f).ThenByDescending(x => x.CropSchemeSign);

            //  var distinctCropSchemeCodes = cropAssociationRates.GroupBy(x => new { x.Crop_Scheme_Code }).Select(x => x.FirstOrDefault()).OrderByDescending(x => x.Crop_Scheme_Code).ToList();
            
            
            var schemesPattern = (from car in greenFarmerData
                                  join
                                  csc in _context.CropSchemes on
                                  car.Crop_Scheme_Code equals csc.Code
                                  select new
                                  {
                                      CropSchemeId = csc.CropSchemeId,
                                      Crop_Name_Code = csc.CropCode,
                                      Crop_Scheme_Code = csc.Code,
                                      Crop_Scheme_From = csc.From,
                                      Crop_Scheme_Sign = csc.Sign,
                                      Crop_Count_mm = csc.Count

                                  }).DistinctBy(x=>x.Crop_Scheme_Code)
                                  .ToList();

            foreach (var gfd in greenFarmerData)
            {
                foreach (var scpattrn in schemesPattern)
                {

                    if (gfd.Crop_Scheme_Code == scpattrn.Crop_Scheme_Code)
                    {
                        gfd.CropSchemeId = scpattrn.CropSchemeId;
                        gfd.CropSchemePattern = scpattrn.Crop_Scheme_From + " " + scpattrn.Crop_Scheme_Sign + " / " + scpattrn.Crop_Count_mm;
                    }

                }
            }






            var farmersAgreementSizeDetails = (from fasd in farmerAgreementAndSettlementInfoData

                                               join
                                               fasizedetails in _context.FarmersAgreementSizeDetails on
                                               fasd.Farmers_Agreement_Code equals fasizedetails.Farmers_Agreement_Code

                                               select new
                                               {

                                                   Crop_Scheme_From = fasizedetails.Crop_Scheme_From,
                                                   Crop_Count_mm = fasizedetails.Crop_Count_mm,
                                                   Crop_Scheme_Sign = fasizedetails.Crop_Scheme_Sign,
                                                   Farmers_Agreement_Code = fasizedetails.Farmers_Agreement_Code,
                                                   Crop_Scheme_Code = fasizedetails.Crop_Scheme_Code,
                                                   Crop_Rate_As_per_Our_Agreement = fasizedetails.Crop_Rate_As_per_Our_Agreement,
                                                   Crop_Rate_As_per_Association = fasizedetails.Crop_Rate_As_per_Association
                                               }

                                                     ).ToList();

            foreach (var gfd in greenFarmerData)
            {
                foreach (var car in cropAssociationRates)
                {
                    foreach (var fasd in farmersAgreementSizeDetails)
                    {
                        if (car.Crop_Scheme_Code == fasd.Crop_Scheme_Code && car.Crop_Scheme_Code == gfd.Crop_Scheme_Code)
                        {
                            gfd.Farmer_Code = car.Farmer_Code;

                            gfd.Crop_Scheme_From = fasd.Crop_Scheme_From;
                            gfd.Crop_Count_mm = fasd.Crop_Count_mm;
                            gfd.Crop_Scheme_Sign = fasd.Crop_Scheme_Sign;
                            gfd.Farmers_Agreement_Code = fasd.Farmers_Agreement_Code;
                            // gfd.Crop_Scheme_Code = car.Crop_Scheme_Code;
                            gfd.Crop_Rate_As_per_Association = car.Crop_Rate_As_per_Association;
                            gfd.Crop_Rate_Per_UOM = car.Crop_Rate_Per_UOM;
                            gfd.Crop_Rate_As_per_Our_Agreement = fasd.Crop_Rate_As_per_Our_Agreement;

                        }
                    }
                }
            }





            return greenFarmerData.OrderBy(x => x.CropSchemeId).ToList();

        }

        public async Task<List<InputIssue>> GetFarmerInputIssues(FarmerAccountSettlementSearchDTO settlementSearchDTO)
        {
            var data = await (from inputIssue in _context.FarmersInputConsumptionDetails
                              join farmerMaterial in _context.FarmersMaterialIssueDetails
                              on inputIssue.MIFConsumptionNo equals farmerMaterial.MIFConsumptionNo
                              join Raw_Material_Details in _context.RawMaterialDetails
                              on farmerMaterial.RawMaterialDetailsCode equals Raw_Material_Details.Raw_Material_Details_Code
                              join Farmers_Inputs_Area_Details in _context.FarmersInputsAreaDetails
                              on inputIssue.PSNumber equals Farmers_Inputs_Area_Details.PSNumber
                              join Farmers_Inputs_Material_Rates in _context.FarmersInputsMaterialRates
                              on Farmers_Inputs_Area_Details.FIRatePassingNo equals Farmers_Inputs_Material_Rates.FIRatePassingNo
                              where inputIssue.PSNumber == settlementSearchDTO.PsNumber && inputIssue.FarmerCode == settlementSearchDTO.FarmerCode
                              && Farmers_Inputs_Area_Details.AreaID == settlementSearchDTO.AreaId
                              select new InputIssue
                              {
                                  MIF_Date_of_Issue = inputIssue.MIFDateofIssue,
                                  Raw_Material_Details_Name = Raw_Material_Details.Raw_Material_Details_Name,
                                  Farmers_Material_Issued_Qty = farmerMaterial.FarmersMaterialIssuedQty,
                                  Farmer_Material_Rate = Farmers_Inputs_Material_Rates.FarmerMaterialRate,
                                  Raw_Material_UOM = Farmers_Inputs_Material_Rates.RawMaterialUOM

                              }
                        ).ToListAsync();
            List<InputIssue> finalData = new List<InputIssue>();
            foreach (var compile in data)
            {
                var check = finalData.Where(x => x.Raw_Material_Details_Name == compile.Raw_Material_Details_Name).FirstOrDefault();
                if (check != null)
                {
                    check.Farmer_Material_Rate = check.Farmer_Material_Rate + compile.Farmer_Material_Rate;
                    check.Farmers_Material_Issued_Qty = check.Farmers_Material_Issued_Qty + compile.Farmers_Material_Issued_Qty;
                }
                else
                {
                    compile.Farmer_Material_Rate_Raw_Material_UOM = compile.Farmer_Material_Rate.ToString() + compile.Raw_Material_UOM;
                    finalData.Add(compile);
                }
            }

            for (int i = 0; i < finalData.Count; i++)
            {
                var fin = finalData[i];
                fin.Calculation = fin.Farmer_Material_Rate * fin.Farmers_Material_Issued_Qty;
                if (i > 0)
                {
                    fin.Cumulative = finalData[i - 1].Cumulative + fin.Calculation;
                }
                else
                {
                    fin.Cumulative = fin.Calculation;
                }
            }

            return finalData;
        }

        public async Task<List<InputReturn>> GetFarmerInputReturns(FarmerAccountSettlementSearchDTO settlementSearchDTO)
        {
            var data = await (from farmerInputsMaterialmaster in _context.FarmersInputsMaterialMaster
                              join Farmers_Inputs_Material_Details in _context.FarmersInputsMaterialDetail
                              on farmerInputsMaterialmaster.FIMReturnNo equals Farmers_Inputs_Material_Details.FIMReturnNo
                              join Raw_Material_Details in _context.RawMaterialDetails
                              on Farmers_Inputs_Material_Details.RawMaterialDetailsCode equals Raw_Material_Details.Raw_Material_Details_Code
                              join Farmers_Inputs_Area_Details in _context.FarmersInputsAreaDetails
                              on farmerInputsMaterialmaster.PSNumber equals Farmers_Inputs_Area_Details.PSNumber
                              join Farmers_Inputs_Material_Rates in _context.FarmersInputsMaterialRates
                              on Farmers_Inputs_Area_Details.FIRatePassingNo equals Farmers_Inputs_Material_Rates.FIRatePassingNo
                              where farmerInputsMaterialmaster.PSNumber == settlementSearchDTO.PsNumber
                              && farmerInputsMaterialmaster.FarmerCode == settlementSearchDTO.FarmerCode
                              && Farmers_Inputs_Area_Details.AreaID == settlementSearchDTO.AreaId
                              select new InputReturn
                              {
                                  FMIR_Date = farmerInputsMaterialmaster.FMIRDate,
                                  Raw_Material_Details_Name = Raw_Material_Details.Raw_Material_Details_Name,
                                  Farmers_Material_Return_Qty = Farmers_Inputs_Material_Details.FarmersMaterialReturnQty,
                                  Farmer_Material_Rate = Farmers_Inputs_Material_Rates.FarmerMaterialRate,
                                  Raw_Material_UOM = Farmers_Inputs_Material_Rates.RawMaterialUOM
                              }
                        ).ToListAsync();


            List<InputReturn> finalData = new List<InputReturn>();
            foreach (var compile in data)
            {
                var check = finalData.Where(x => x.Raw_Material_Details_Name == compile.Raw_Material_Details_Name).FirstOrDefault();
                if (check != null)
                {
                    check.Farmer_Material_Rate = check.Farmer_Material_Rate + compile.Farmer_Material_Rate;
                    check.Farmers_Material_Return_Qty = check.Farmers_Material_Return_Qty + compile.Farmers_Material_Return_Qty;
                }
                else
                {
                    compile.Farmer_Material_Rate_Raw_Material_UOM = compile.Farmer_Material_Rate.ToString() + compile.Raw_Material_UOM;
                    finalData.Add(compile);
                }
            }

            for (int i = 0; i < finalData.Count; i++)
            {
                var fin = finalData[i];
                fin.Calculation = fin.Farmer_Material_Rate * fin.Farmers_Material_Return_Qty;
                if (i > 0)
                {
                    fin.Cumulative = finalData[i - 1].Cumulative + fin.Calculation;
                }
                else
                {
                    fin.Cumulative = fin.Calculation;
                }
            }

            return data;
        }
    }
}