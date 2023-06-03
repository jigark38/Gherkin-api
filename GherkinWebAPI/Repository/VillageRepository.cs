using GherkinWebAPI.Core.Villages;
using GherkinWebAPI.Models;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using VillageDetail = GherkinWebAPI.Models.Villages.VillageDetail;

namespace GherkinWebAPI.Repository
{
    public class VillageRepository : RepositoryBase<Village>, IVillageRepository
    {
        private RepositoryContext _context;
        public VillageRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
            this._context = repositoryContext;
        }

        public async Task<VillageDetail> AddVillage(VillageDetail villageDetail)
        {
            try
            {
                var village = new Village
                {
                    Village_Name = villageDetail.VillageName,
                    Mandal_Code = villageDetail.MandalCode,
                    District_Code = villageDetail.DistrictCode,
                    State_Code = villageDetail.StateCode,
                    Country_Code = villageDetail.CountryCode
                };

                _context.Villages.Add(village);

                var result = await _context.SaveChangesAsync();

                if (result == 1)
                {
                    villageDetail.VillageCode = village.Village_Code;
                    return villageDetail;
                }
                else
                    return new VillageDetail();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<List<VillageDetail>> GetAllVillagesAsync()
        {
            var villages = (from v in _context.Villages
                            select new VillageDetail
                            {
                                VillageCode = v.Village_Code,
                                VillageName = v.Village_Name,
                                MandalCode = v.Mandal_Code,
                                DistrictCode = v.District_Code,
                                StateCode = v.State_Code,
                                CountryCode = v.Country_Code
                            }).ToListAsync();

            return await villages; ;
        }

        public async Task<VillageDetail> GetVillageByCodeAsync(int villageCode)
        {
            var villages = (from v in _context.Villages
                            where v.Village_Code == villageCode
                            select new VillageDetail
                            {
                                VillageCode = v.Village_Code,
                                VillageName = v.Village_Name,
                                MandalCode = v.Mandal_Code,
                                DistrictCode = v.District_Code,
                                StateCode = v.State_Code,
                                CountryCode = v.Country_Code
                            }).FirstOrDefaultAsync();

            return await villages;
        }

        public async Task<List<VillageDetail>> GetVillageByMandalCodeAsync(int mandalCode)
        {
            var villages = (from v in _context.Villages
                            where v.Mandal_Code == mandalCode
                            select new VillageDetail
                            {
                                VillageCode = v.Village_Code,
                                VillageName = v.Village_Name,
                                MandalCode = v.Mandal_Code,
                                DistrictCode = v.District_Code,
                                StateCode = v.State_Code,
                                CountryCode = v.Country_Code
                            }).ToListAsync();

            return await villages;
        }

        public async Task<bool> IsVillageExist(int countryCode, int stateCode, int districtCode, int mandalCode, string villageName)
        {
            return await _context.Villages.AnyAsync(x => x.Country_Code == countryCode && x.State_Code == stateCode && x.District_Code == districtCode && x.Mandal_Code == mandalCode && x.Village_Name.ToLower() == villageName.ToLower());
        }

        public async Task<VillageDetail> UpdateVillage(VillageDetail villageDetail)
        {
            var village = await _context.Villages.SingleOrDefaultAsync(v => v.Village_Code == villageDetail.VillageCode);

            village.Village_Name = villageDetail.VillageName;
            village.Country_Code = villageDetail.CountryCode;
            village.State_Code = villageDetail.StateCode;
            village.District_Code = villageDetail.DistrictCode;
            village.Mandal_Code = villageDetail.MandalCode;

            _context.Entry(village).State = EntityState.Modified;

            var result = await _context.SaveChangesAsync();

            if (result == 1)
            {
                return villageDetail;
            }
            else
            {
                return new VillageDetail();
            }
        }

        public async Task<List<Village>> GetVillageListByAreaAndEmployee(string areaId, string employeeId, string villageName)
        {
            //var villageCodeList = await _context.FarmersAgreementDetails.Where(x => x.Area_ID == areaId && x.Employee_ID == employeeId).Select(x => x.Village_Code).ToListAsync();
            var villageCodeList = await _context.FarmersAgreementDetails.Where(x => x.Area_ID == areaId).Select(x => x.Village_Code).ToListAsync();

            var villageList = new List<Village>();

            if (villageCodeList.Count > 0)
			{
                villageList = await _context.Villages.Where(x => villageCodeList.Contains(x.Village_Code) && x.Village_Name.ToLower().StartsWith(villageName.ToLower())).ToListAsync();
            }
            return villageList;
        }

        public async Task<VillageDetail> GetCountryInfoByVillageName(string villageName)
        {
            var villages = (from v in _context.Villages
                            where v.Village_Name == villageName
                            select new VillageDetail
                            {
                                VillageCode = v.Village_Code,
                                VillageName = v.Village_Name,
                                MandalCode = v.Mandal_Code,
                                DistrictCode = v.District_Code,
                                StateCode = v.State_Code,
                                CountryCode = v.Country_Code
                            }).FirstOrDefaultAsync();

            return await villages;
        }
    }
}
