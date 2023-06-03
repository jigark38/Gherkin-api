using GherkinWebAPI.Core;
using GherkinWebAPI.Models;
using GherkinWebAPI.Persistence;
using GherkinWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace GherkinWebAPI.Repository
{
    public class FarmersAgreementRepository : RepositoryBase<FarmersAgreementDetail>, IFarmersAgreementRepository
    {
        private RepositoryContext _context;
        public FarmersAgreementRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            _context = repositoryContext;
        }
        public async Task<FarmersAgreementDetail> CreateAgreement(FarmersAgreementDetail farmersAgreement)
        {
            try
            {
                // farmersAgreement.Farmers_Account_No = farmersAgreement.Area_Code + "0001";

                FarmersAgreementDetail detail = new FarmersAgreementDetail
                {
                    Farmers_Agreement_Code = farmersAgreement.Farmers_Agreement_Code,
                    Farmers_Agreement_Date = farmersAgreement.Farmers_Agreement_Date,
                    Area_ID = farmersAgreement.Area_ID,
                    Area_Code = farmersAgreement.Area_Code,
                    Farmers_Account_No = farmersAgreement.Farmers_Account_No,
                    Employee_ID = farmersAgreement.Employee_ID,
                    Village_Code = farmersAgreement.Village_Code,
                    Farmer_Code = farmersAgreement.Farmer_Code,
                    Crop_Group_Code = farmersAgreement.Crop_Group_Code,
                    Crop_Name_Code = farmersAgreement.Crop_Name_Code,
                    PS_Number = farmersAgreement.PS_Number,
                    Farmers_No_of_Acres_Area = farmersAgreement.Farmers_No_of_Acres_Area,
                    Agriculture_DRIP_NONDRIP = farmersAgreement.Agriculture_DRIP_NONDRIP,
                    Boarder_Crop = farmersAgreement.Boarder_Crop,
                    Previous_Crop = farmersAgreement.Previous_Crop,
                    Mulching_Sheet = farmersAgreement.Mulching_Sheet,
                    FYM = farmersAgreement.FYM,
                };

                _context.FarmersAgreementDetails.Add(detail);
                var result = await _context.SaveChangesAsync();

                if (result == 1)
                {
                    return detail;
                }

                return new FarmersAgreementDetail();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<FarmersAgreement> SearchAgreement(string areaId, int cityCode, string farmersCode, string cropGroupCode, string cropNameCode, string psNumber)
        {
            var farmersAgreements = await (from fa in _context.FarmersAgreementDetails
                                           join fas in _context.FarmersAgreementSizeDetails on fa.Farmers_Agreement_Code equals fas.Farmers_Agreement_Code
                                           join farmer in _context.Farmers on fa.Farmer_Code equals farmer.Farmer_Code
                                           where fa.Area_ID == areaId && fa.Village_Code == cityCode
                                           && fa.Farmer_Code == farmersCode && fa.Crop_Group_Code == cropGroupCode && fa.Crop_Name_Code == cropNameCode
                                           && fa.PS_Number == psNumber
                                           select new FarmersAgreement
                                           {
                                               FarmersAgreementCode = fa.Farmers_Agreement_Code,
                                               FarmersAgreementDate = fa.Farmers_Agreement_Date,
                                               AreaID = fa.Area_ID,
                                               AreaCode = fa.Area_Code,
                                               EmployeeID = fa.Employee_ID,
                                               VillageCode = fa.Village_Code,
                                               FarmerCode = fa.Farmer_Code,
                                               FarmersAccountNo = fa.Farmers_Account_No,
                                               Address = farmer.Farmer_Address,
                                               CountryCode = farmer.Country_Code,
                                               DistrictCode = farmer.District_Code,
                                               StateCode = farmer.State_Code,
                                               CropGroupCode = fa.Crop_Group_Code,
                                               CropNameCode = fa.Crop_Name_Code,
                                               PSNumber = fa.PS_Number,
                                               FarmersNoOfAcersArea = fa.Farmers_No_of_Acres_Area,
                                               AgricultureDripNonDrip = fa.Agriculture_DRIP_NONDRIP,
                                               BoarderCrop = fa.Boarder_Crop,
                                               PreviousCrop = fa.Previous_Crop,
                                               MulchingSheet = fa.Mulching_Sheet,
                                               FYM = fa.FYM,
                                               FarmersAgreementSizes = (from f in fa.FarmersAgreementSizeDetails
                                                                        select new FarmersAgreementSize
                                                                        {
                                                                            CropSchemeCode = f.Crop_Scheme_Code,
                                                                            CropCount = f.Crop_Count_mm,
                                                                            CropSchemeFromSign = string.Concat(f.Crop_Scheme_From, f.Crop_Scheme_Sign),
                                                                            CropRateAsPerAssociation = f.Crop_Rate_As_per_Association,
                                                                            CropRatePerUOM = f.Crop_Rate_Per_UOM,
                                                                            CropRateAsPerOurAgreement = f.Crop_Rate_As_per_Our_Agreement,
                                                                            CropRatesRemarks = f.Crop_Rates_Remarks
                                                                        }).ToList()


                                           }).FirstOrDefaultAsync();

            return farmersAgreements;
        }

        public async Task<string> GetFarmersAgreementCodeAsync()
        {
            var agreements = await _context.FarmersAgreementDetails.AsNoTracking().ToListAsync();
            if (agreements?.Any() ?? false)
            {
                var selectMaxAgreementId = agreements.OrderByDescending(c => c.ID).Take(1).FirstOrDefault().ID;
                return "FA_" + Convert.ToString(selectMaxAgreementId + 1);
            }
            else
            {
                return "FA_" + "1";
            }
        }

        public async Task<ApiResponse<object>> UpdateAgreement(string farmersAgreementCode, FarmersAgreement farmersAgreement)
        {
            ApiResponse<object> result = new ApiResponse<object>();
            try
            {

                if (farmersAgreement != null)
                {
                    var farmersAgreementDetail = _context.FarmersAgreementDetails.Include(a => a.FarmersAgreementSizeDetails).Where(fc => fc.Farmers_Agreement_Code == farmersAgreementCode).FirstOrDefault();

                    if (farmersAgreementDetail.Crop_Name_Code != farmersAgreement.CropNameCode)
                    {
                        _context.FarmersAgreementSizeDetails.Where(b => b.Farmers_Agreement_Code == farmersAgreementDetail.Farmers_Agreement_Code).ToList().ForEach(item =>
                          {
                              var itemTobeDeleted = _context.FarmersAgreementSizeDetails.SingleOrDefault(a => a.ID == item.ID);
                              _context.FarmersAgreementSizeDetails.Remove(itemTobeDeleted);
                          });
                        await _context.SaveChangesAsync();

                    }
                    farmersAgreementDetail.Farmers_Agreement_Date = farmersAgreement.FarmersAgreementDate;
                    farmersAgreementDetail.Area_ID = farmersAgreement.AreaID;
                    farmersAgreementDetail.Area_Code = farmersAgreement.AreaCode;
                    farmersAgreementDetail.Employee_ID = farmersAgreement.EmployeeID;
                    farmersAgreementDetail.Village_Code = farmersAgreement.VillageCode;
                    farmersAgreementDetail.Farmer_Code = farmersAgreement.FarmerCode;
                    farmersAgreementDetail.Farmers_Account_No = farmersAgreement.FarmersAccountNo;
                    farmersAgreementDetail.Crop_Group_Code = farmersAgreement.CropGroupCode;
                    farmersAgreementDetail.Crop_Name_Code = farmersAgreement.CropNameCode;
                    farmersAgreementDetail.PS_Number = farmersAgreement.PSNumber;
                    farmersAgreementDetail.Farmers_No_of_Acres_Area = farmersAgreement.FarmersNoOfAcersArea;
                    farmersAgreementDetail.Agriculture_DRIP_NONDRIP = farmersAgreement.AgricultureDripNonDrip;
                    farmersAgreementDetail.Boarder_Crop = farmersAgreement.BoarderCrop;
                    farmersAgreementDetail.Previous_Crop = farmersAgreement.PreviousCrop;
                    farmersAgreementDetail.Mulching_Sheet = farmersAgreement.MulchingSheet;
                    farmersAgreementDetail.FYM = farmersAgreement.FYM;

                    if (farmersAgreement.FarmersAgreementSizes.Count > 0)
                    {
                        List<FarmersAgreementSizeDetail> farmersAgreementSizeDetailList = new List<FarmersAgreementSizeDetail>();
                        foreach (var newFarmerSize in farmersAgreement.FarmersAgreementSizes)
                        {

                            if (newFarmerSize.ID > 0)
                            {
                                var item = _context.FarmersAgreementSizeDetails.Where(a => a.ID == newFarmerSize.ID).FirstOrDefault();
                                item.Crop_Scheme_Code = newFarmerSize.CropSchemeCode;
                                item.Crop_Count_mm = newFarmerSize.CropCount;
                                item.Crop_Scheme_From = int.Parse(newFarmerSize.CropSchemeFromSign.Substring(0, newFarmerSize.CropSchemeFromSign.IndexOf(' ')));
                                item.Crop_Scheme_Sign = newFarmerSize.CropSchemeFromSign.Substring(newFarmerSize.CropSchemeFromSign.IndexOf(' '));
                                item.Crop_Rate_As_per_Association = newFarmerSize.CropRateAsPerAssociation;
                                item.Crop_Rate_Per_UOM = newFarmerSize.CropRatePerUOM;
                                item.Crop_Rate_As_per_Our_Agreement = newFarmerSize.CropRateAsPerOurAgreement;
                                item.Crop_Rates_Remarks = newFarmerSize.CropRatesRemarks;
                            }
                            else
                            {
                                FarmersAgreementSizeDetail farmersAgreementSizeDetailadd = new FarmersAgreementSizeDetail()
                                {
                                    Farmers_Agreement_Code = farmersAgreementDetail.Farmers_Agreement_Code,
                                    Crop_Scheme_Code = newFarmerSize.CropSchemeCode,
                                    Crop_Count_mm = newFarmerSize.CropCount,
                                    Crop_Scheme_From = int.Parse(newFarmerSize.CropSchemeFromSign.Substring(0, newFarmerSize.CropSchemeFromSign.IndexOf(' '))),
                                    Crop_Scheme_Sign = newFarmerSize.CropSchemeFromSign.Substring(newFarmerSize.CropSchemeFromSign.IndexOf(' ')),
                                    Crop_Rate_As_per_Association = newFarmerSize.CropRateAsPerAssociation,
                                    Crop_Rate_Per_UOM = newFarmerSize.CropRatePerUOM,
                                    Crop_Rate_As_per_Our_Agreement = newFarmerSize.CropRateAsPerOurAgreement,
                                    Crop_Rates_Remarks = newFarmerSize.CropRatesRemarks
                                };
                                farmersAgreementSizeDetailList.Add(farmersAgreementSizeDetailadd);
                            }
                        }
                        _context.FarmersAgreementSizeDetails.AddRange(farmersAgreementSizeDetailList);
                    }
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

        public async Task<List<FarmersAgreementDetail>> GetFarmersAgreementDetailsByAreaId(string areaId)
        {
            var agrementDetails = await this._context.FarmersAgreementDetails.Where(x => x.Area_ID == areaId).ToListAsync();
            return agrementDetails;
        }

        public async Task<FieldStaffDetails> GetAreaInchargeDetailsByAreaId(string areaId, DateTime date)
        {
            var inchrgeDetails = await this._context.FieldStaffDetails.Where(x => x.Area_ID == areaId && x.StaffType.ToUpper() == "INCHARGE" && x.EffectiveDate <= date).OrderByDescending(x => x.EffectiveDate).FirstOrDefaultAsync();
            return inchrgeDetails;
        }

        public async Task<bool> ValidateFarmerAccount(string FarmersAccountNo, string FarmerCode, string PSNumber)
        {
            var farmersAgreementslist = await _context.FarmersAgreementDetails.Where(fc => fc.Farmers_Account_No == FarmersAccountNo && fc.Farmer_Code == FarmerCode && fc.PS_Number == PSNumber).ToListAsync();

            if (farmersAgreementslist?.Any() ?? false)
            {
                return false;
            }
            return true;
        }

        public async Task<List<string>> FarmerAccountList(string CropGroupCode, string CropNameCode, string PSNumber)
        {
            var farmerAccountList = await _context.FarmersAgreementDetails.Where(fa => fa.Crop_Group_Code == CropGroupCode && fa.Crop_Name_Code == CropNameCode && fa.PS_Number == PSNumber).Select(a => a.Farmers_Account_No).Distinct().ToListAsync();
            return farmerAccountList;
        }

        public async Task<FarmersAgreement> FarmerDetailsByAccount(string FarmersAccountNo, string PSNumber)
        {
            var farmerAgg = await (from fa in _context.FarmersAgreementDetails
                                       //join fas in _context.FarmersAgreementSizeDetails on fa.Farmers_Agreement_Code equals fas.Farmers_Agreement_Code
                                   join farmer in _context.Farmers on fa.Farmer_Code equals farmer.Farmer_Code
                                   join village in _context.Villages on fa.Village_Code equals village.Village_Code
                                   join mandals in _context.Mandals on village.Mandal_Code equals mandals.Mandal_Code
                                   join district in _context.Districts on village.District_Code equals district.District_Code
                                   join state in _context.States on village.State_Code equals state.State_Code
                                   join country in _context.Countries on village.Country_Code equals country.Country_Code
                                   where fa.Farmers_Account_No == FarmersAccountNo && fa.PS_Number == PSNumber
                                   select new FarmersAgreement
                                   {
                                       FarmersAgreementCode = fa.Farmers_Agreement_Code,
                                       FarmersAgreementDate = fa.Farmers_Agreement_Date,
                                       AreaID = fa.Area_ID,
                                       AreaCode = fa.Area_Code,
                                       EmployeeID = fa.Employee_ID,
                                       VillageCode = fa.Village_Code,
                                       VillageName = village.Village_Name,
                                       MandalName = mandals.Mandal_Name,
                                       FarmerCode = fa.Farmer_Code,
                                       FarmerName = farmer.FarmerName,
                                       FarmersAccountNo = fa.Farmers_Account_No,
                                       Address = farmer.Farmer_Address,
                                       CountryCode = farmer.Country_Code,
                                       CountryName = country.Country_Name,
                                       DistrictCode = farmer.District_Code,
                                       DistrictName = district.District_Name,
                                       StateCode = farmer.State_Code,
                                       StateName = state.State_Name,
                                       CropGroupCode = fa.Crop_Group_Code,
                                       CropNameCode = fa.Crop_Name_Code,
                                       PSNumber = fa.PS_Number,
                                       FarmersNoOfAcersArea = fa.Farmers_No_of_Acres_Area,
                                       AgricultureDripNonDrip = fa.Agriculture_DRIP_NONDRIP,
                                       BoarderCrop = fa.Boarder_Crop,
                                       PreviousCrop = fa.Previous_Crop,
                                       MulchingSheet = fa.Mulching_Sheet,
                                       FYM = fa.FYM,
                                       FarmersAgreementSizes = (from f in fa.FarmersAgreementSizeDetails
                                                                where f.Farmers_Agreement_Code == fa.Farmers_Agreement_Code
                                                                select new FarmersAgreementSize
                                                                {
                                                                    ID = f.ID,
                                                                    CropSchemeCode = f.Crop_Scheme_Code,
                                                                    CropCount = f.Crop_Count_mm,
                                                                    CropSchemeFromSign = string.Concat(f.Crop_Scheme_From, f.Crop_Scheme_Sign),
                                                                    CropRateAsPerAssociation = f.Crop_Rate_As_per_Association,
                                                                    CropRatePerUOM = f.Crop_Rate_Per_UOM,
                                                                    CropRateAsPerOurAgreement = f.Crop_Rate_As_per_Our_Agreement,
                                                                    CropRatesRemarks = f.Crop_Rates_Remarks
                                                                }).ToList()


                                   }).FirstOrDefaultAsync();
            return farmerAgg;
        }
    }
}