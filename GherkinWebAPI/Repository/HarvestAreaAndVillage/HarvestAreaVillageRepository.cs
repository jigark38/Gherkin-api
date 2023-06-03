using GherkinWebAPI.Core.Harvest_Area_Village;
using GherkinWebAPI.Models;
using GherkinWebAPI.Persistence;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using GherkinWebAPI.Models.Harvest_Area;

namespace GherkinWebAPI.Repository.HarvestAreaAndVillage
{
    public class HarvestAreaVillageRepository : RepositoryBase<HarvestAreaVillage>, IHarvestAreaVillageRepository
    {
        private RepositoryContext _context;

        public HarvestAreaVillageRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
            this._context = repositoryContext;
        }

        public async Task<HarvestAreaVillageDetail> AddAreaVillage(HarvestAreaVillageDetail harvestAreaVillage)
        {

            var data = new HarvestAreaVillage
            {
                Area_ID = harvestAreaVillage.AreaId,
                Country_Code = harvestAreaVillage.CountryCode,
                State_Code = harvestAreaVillage.StateCode,
                District_Code = harvestAreaVillage.DistrictCode,
                Mandal_Code = harvestAreaVillage.MandalCode,
                Village_Code = harvestAreaVillage.VillageCode
            };

            _context.HarvestAreaVillages.Add(data);

            var result = await _context.SaveChangesAsync();

            if (result == 1)
            {
                harvestAreaVillage.ID = data.ID;
                return harvestAreaVillage;
            }
            else
            {
                return new HarvestAreaVillageDetail();
            }
        }

        public async Task<List<HarvestArea>> GetAvailableVillagesAsync(string areaId, string areaName, int countryCode, int stateCode, int districtCode, int mandalCode)
        {
            var villageCodes = await (from hav in _context.HarvestAreaVillages
                                      where hav.Area_ID == areaId && hav.Country_Code == countryCode
                                      && hav.State_Code == stateCode && hav.District_Code == districtCode
                                      && hav.Mandal_Code == mandalCode
                                      select hav.Village_Code).ToListAsync();

            var villages = (from c in _context.Countries
                            join s in _context.States on c.Country_Code equals s.Country_Code
                            join d in _context.Districts on s.State_Code equals d.State_Code
                            join m in _context.Mandals on d.District_Code equals m.District_Code
                            join v in _context.Villages on m.Mandal_Code equals v.Mandal_Code
                            where v.Country_Code == countryCode && v.State_Code == stateCode
                                  && v.District_Code == districtCode && v.Mandal_Code == mandalCode
                                  && !villageCodes.Contains(v.Village_Code) 
                            select new HarvestArea
                            {
                                AreaId = areaId,
                                AreaName = areaName,
                                CountryCode = c.Country_Code,
                                CountryName = c.Country_Name,
                                StateCode = s.State_Code,
                                StateName = s.State_Name,
                                DistrictCode = d.District_Code,
                                DistrictName = d.District_Name,
                                MandalCode = m.Mandal_Code,
                                MandalName = m.Mandal_Name,
                                VillageCode = v.Village_Code,
                                VillageName = v.Village_Name
                            }).OrderByDescending(a=>a.VillageCode).ToListAsync();

            return await villages;
        }

        public async Task<bool> IsAreaVillageAllowed(string areaId, int countryCode, int stateCode, int districtCode, int mandalCode, int villageCode)
        {
            return await _context.HarvestAreaVillages.AnyAsync(h => h.Area_ID.ToLower() == areaId.ToLower() && h.Country_Code == countryCode
           && h.State_Code == stateCode && h.Mandal_Code == mandalCode && h.Village_Code == villageCode);
        }

        public async Task<bool> IsVillageCodeAllowed(int villageCode)
        {
            return await _context.HarvestAreaVillages.AnyAsync(ac => ac.Village_Code == villageCode);
        }
        public async Task<HarvestAreaVillageDetail> UpdateHarvestAreaVillage(HarvestAreaVillageDetail harvestAreaVillage)
        {
            var areaVillage = await _context.HarvestAreaVillages.SingleOrDefaultAsync(a => a.ID == harvestAreaVillage.ID);

            areaVillage.Country_Code = harvestAreaVillage.CountryCode;
            areaVillage.State_Code = harvestAreaVillage.StateCode;
            areaVillage.District_Code = harvestAreaVillage.DistrictCode;
            areaVillage.Mandal_Code = harvestAreaVillage.MandalCode;
            areaVillage.Village_Code = harvestAreaVillage.VillageCode;

            _context.Entry(areaVillage).State = EntityState.Modified;

            var result = await _context.SaveChangesAsync();

            if (result == 1)
            {
                return harvestAreaVillage;
            }
            else
                return new HarvestAreaVillageDetail();
        }
    }

}

