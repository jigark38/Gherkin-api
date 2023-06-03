using GherkinWebAPI.Core.DailyInputAndFeedingDetails;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.DailyInputAndFeedingDetails;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Repository.DailyInputAndFeedingDetails
{
    public class DailyInputRepository : IDailyInputRepository
    {
        private readonly RepositoryContext _context;
        public DailyInputRepository(RepositoryContext context)
        {
            _context = context;
        }
        public async Task<List<AreaName>> GetAllAreas()
        {
            try
            {
                return await (from f in _context.Areas
                              select new AreaName
                              {
                                  AreaId = f.Area_ID,
                                  Name = f.Area_Name
                              }).OrderBy(i => i.Name).ToListAsync();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<List<EmployeeName>> GetAreaWiseEmployeeDetails(string areaId)
        {
            try
            {
                List<EmployeeName> Listemp = new List<EmployeeName>();
                Listemp = await (from f in _context.FieldStaffDetails
                                 join e in _context.Employees on f.Employee_ID equals e.employeeId
                                 where f.Area_ID == areaId
                                 select new EmployeeName
                                 {
                                     employeeId = e.employeeId,
                                     employeeName = e.employeeName
                                 }).Distinct().ToListAsync();
                return Listemp;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<List<GroupCode>> GetAreaWiseCropGroup(string areaId)
        {
            try
            {
                return await (from f in _context.FarmersAgreementDetails
                              join g in _context.CropGroups on f.Crop_Group_Code equals g.CropGroupCode
                              where f.Area_ID == areaId
                              select new GroupCode
                              {
                                  CropGroupCode = g.CropGroupCode,
                                  CropGroupName = g.Name
                              }).OrderBy(i => i.CropGroupName).Distinct().ToListAsync();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<List<CropName>> GetCropNameCode(string cropGroup)
        {
            try
            {
            //    return await (from f in _context.FarmersAgreementDetails
            //                  join g in _context.Crops on f.Crop_Name_Code equals g.CropCode
            //                  where g.CropGroupCode == cropGroup
            //                  select new CropName
            //                  {
            //                      CropNameCode = g.CropCode,
            //                      Name = g.Name
            //                  }).OrderBy(i => i.Name).Distinct().ToListAsync();

                return await (from g in _context.Crops
                              where g.CropGroupCode == cropGroup
                              select new CropName
                              {
                                  CropNameCode = g.CropCode,
                                  Name = g.Name
                              }).OrderBy(i => i.Name).Distinct().ToListAsync();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<List<CropFromTo>> GetAreaWiseSeasonToFrom(string areaId)
        {
            try
            {
                var result = await (from f in _context.FarmersAgreementDetails
                              join g in _context.PlantationSchedules on f.PS_Number equals g.PsNumber
                              where f.Area_ID == areaId
                              select new CropFromTo
                              {
                                  PSNumber = g.PsNumber,
                                  SeasonFrom = g.FromDate,
                                  SeasonTo = g.ToDate,
                                  CropGroupCode = f.Crop_Group_Code,
                                  CropNameCode = f.Crop_Name_Code
                              }).GroupBy(l => l.PSNumber)
                                      .Select(m => m.FirstOrDefault())
                                      .ToListAsync();
                 
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<List<CountryName>> GetAreaWiseCountry(string areaId)
        {
            try
            {
                return await (from f in _context.HarvestAreaVillages
                              join g in _context.Countries on f.Country_Code equals g.Country_Code
                              where f.Area_ID == areaId
                              select new CountryName
                              {
                                  Country_Code = g.Country_Code,
                                  Country_Name = g.Country_Name
                              }).OrderBy(i => i.Country_Name).Distinct().ToListAsync();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<List<StateName>> GetAreaWiseState(string areaId)
        {
            try
            {
                return await (from f in _context.HarvestAreaVillages
                              join g in _context.States on f.State_Code equals g.State_Code
                              where f.Area_ID == areaId
                              select new StateName
                              {
                                  State_Code = g.State_Code,
                                  State_Name = g.State_Name
                              }).OrderBy(i => i.State_Name).Distinct().ToListAsync();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<List<DistrictName>> GetStateWiseDistrict(int statecode)
        {
            try
            {
                return await (from f in _context.HarvestAreaVillages
                              join g in _context.Districts on f.District_Code equals g.District_Code
                              where f.State_Code == statecode
                              select new DistrictName
                              {
                                  District_Code = g.District_Code,
                                  District_Name = g.District_Name
                              }).OrderBy(i => i.District_Name).Distinct().ToListAsync();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<List<MandalName>> GetDistrictWiseMandal(int districtCode)
        {
            try
            {
                return await (from f in _context.HarvestAreaVillages
                              join g in _context.Mandals on f.Mandal_Code equals g.Mandal_Code
                              where f.District_Code == districtCode
                              select new MandalName
                              {
                                  MandalCode = g.Mandal_Code,
                                  MandalsName = g.Mandal_Name
                              }).OrderBy(i => i.MandalsName).Distinct().ToListAsync();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<List<Models.DailyInputAndFeedingDetails.VillageName>> GetMandalWiseVillage(int mandalCode)
        {
            try
            {
                return await (from f in _context.HarvestAreaVillages
                              join g in _context.Villages on f.Village_Code equals g.Village_Code
                              where f.Mandal_Code == mandalCode
                              select new Models.DailyInputAndFeedingDetails.VillageName
                              {
                                  Village_Code = g.Village_Code,
                                  Village_Name = g.Village_Name
                              }).OrderBy(i => i.Village_Name).Distinct().ToListAsync();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<List<FarmerAgreementDetails>> GetAreaWiseFarmerAgreementDetails(string areaId, string ps_Number)
        {
            try
            {
                List<FarmerAgreementDetails> ListFarmerAgreement = new List<FarmerAgreementDetails>();
                ListFarmerAgreement = await (from f in _context.FarmersAgreementDetails
                                             where f.Area_ID == areaId && f.PS_Number == ps_Number
                                             select new FarmerAgreementDetails
                                             {
                                                 Farmers_Name = (from f1 in _context.Farmers
                                                                 where f1.Farmer_Code == f.Farmer_Code
                                                                 select f1.FarmerName).FirstOrDefault(),
                                                 Farmers_Agreement_Code = f.Farmers_Agreement_Code,
                                                 Farmer_Code = f.Farmer_Code,
                                                 Farmers_No_of_Acres_Area = f.Farmers_No_of_Acres_Area,
                                                 Agriculture_DRIP_NONDRIP = f.Agriculture_DRIP_NONDRIP,
                                                 AccountNumber = f.Farmers_Account_No,
                                                 VillageName = (from v in _context.Farmers
                                                                join v1 in _context.Villages
                                                                on v.Village_Code equals v1.Village_Code
                                                                where v.Farmer_Code == f.Farmer_Code
                                                                select v1.Village_Name).FirstOrDefault()
                                             }).Distinct().ToListAsync();
                return ListFarmerAgreement;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public async Task<List<FarmerAgreementDetails>> SearchFarmers(string keyword, string areaId, string ps_Number)
        {
            try
            {
                List<FarmerAgreementDetails> ListFarmerAgreement = new List<FarmerAgreementDetails>();
                ListFarmerAgreement = await (from f in _context.Farmers
                                             join fa in _context.FarmersAgreementDetails
                                             on f.Farmer_Code equals fa.Farmer_Code
                                             where (f.FarmerName.Contains(keyword) || fa.Farmers_Account_No.Contains(keyword)) && fa.Area_ID == areaId && fa.PS_Number == ps_Number
                                             select new FarmerAgreementDetails
                                             {
                                                 Farmers_Name = f.FarmerName,
                                                 Farmers_Agreement_Code = fa.Farmers_Agreement_Code,
                                                 Farmer_Code = fa.Farmer_Code,
                                                 Farmers_No_of_Acres_Area = fa.Farmers_No_of_Acres_Area,
                                                 Agriculture_DRIP_NONDRIP = fa.Agriculture_DRIP_NONDRIP,
                                                 AccountNumber = fa.Farmers_Account_No,
                                                 VillageName = (from v1 in _context.Villages
                                                                where v1.Village_Code == f.Village_Code
                                                                select v1.Village_Name).FirstOrDefault()
                                             }).Distinct().ToListAsync();
                return ListFarmerAgreement;
            }
            catch (Exception e)
            {
                return null;
            }

        }
    }
}