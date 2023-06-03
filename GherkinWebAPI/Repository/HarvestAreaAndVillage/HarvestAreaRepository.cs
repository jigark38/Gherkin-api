using GherkinWebAPI.Core;
using GherkinWebAPI.Core.Harvest_Area;
using GherkinWebAPI.Models;
using GherkinWebAPI.Persistence;
using GherkinWebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Repository.HarvestAreaAndVillage
{
    public class HarvestAreaRepository : RepositoryBase<Area>, IHarvestAreaRepository
    {
        private RepositoryContext _context;

        public HarvestAreaRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
            this._context = repositoryContext;
        }

        public async Task<List<HarvestArea>> SearchArea(string areaId)
        {
            var allAreas = (from area in _context.Areas
                            join areaVillage in _context.HarvestAreaVillages on area.Area_ID equals areaVillage.Area_ID
                            join country in _context.Countries on areaVillage.Country_Code equals country.Country_Code
                            join state in _context.States on areaVillage.State_Code equals state.State_Code
                            join district in _context.Districts on areaVillage.District_Code equals district.District_Code
                            join mandal in _context.Mandals on areaVillage.Mandal_Code equals mandal.Mandal_Code
                            join village in _context.Villages on areaVillage.Village_Code equals village.Village_Code
                            where area.Area_ID == areaId
                            select new HarvestArea
                            {
                                ID = areaVillage.ID,
                                AreaId = area.Area_ID,
                                AreaName = area.Area_Name,
                                CountryCode = country.Country_Code,
                                CountryName = country.Country_Name,
                                StateCode = state.State_Code,
                                StateName = state.State_Name,
                                DistrictCode = district.District_Code,
                                DistrictName = district.District_Name,
                                MandalCode = mandal.Mandal_Code,
                                MandalName = mandal.Mandal_Name,
                                VillageCode = village.Village_Code,
                                VillageName = village.Village_Name
                            }).OrderByDescending(a => a.ID).ToListAsync();

            return await allAreas;
        }

        public async Task<Area> AddArea(Area area)
        {
            var areas = await _context.Areas.AsNoTracking().ToListAsync();
            if (areas.Count > 0)
            {
                var selectMaxAreaId = areas.OrderByDescending(c => c.ID).Take(1).FirstOrDefault().ID;
                area.Area_ID = "CAC_" + Convert.ToString(selectMaxAreaId + 1);
            }
            else
            {
                area.Area_ID = "CAC_" + "1";
            }
            _context.Areas.Add(area);

            var result = await _context.SaveChangesAsync();

            if (result == 1)
            {
                return area;
            }
            else
                return new Area();
        }

        public async Task<List<Area>> GetAreaNameAndCodeAsync()
        {
            //TODO: I need to check before we change to async
            var areaCodeAndNames = await Task.Run(() => (from area in _context.Areas
                                                         select new { area.Area_ID, area.Area_Code, area.Area_Name }).AsEnumerable()
                                                        .Select(a => new Area { Area_ID = a.Area_ID, Area_Code = a.Area_Code, Area_Name = a.Area_Name }).ToList());
            return areaCodeAndNames.OrderBy(x => x.Area_Name)?.ToList();
        }

        public async Task<bool> IsAreaCodeAllowed(int areaCode)
        {
            return await _context.Areas.AnyAsync(ac => ac.Area_Code == areaCode);
        }

        public async Task<bool> IsAreaNameAllowed(string areaName)
        {
            return await _context.Areas.AnyAsync(ac => ac.Area_Name.ToLower() == areaName.ToLower());
        }

        public async Task<List<HarvestArea>> SearchAreaByStateCode(int stateId)
        {
            var allAreas = (from area in _context.Areas
                            join areaVillage in _context.HarvestAreaVillages on area.Area_ID equals areaVillage.Area_ID
                            join country in _context.Countries on areaVillage.Country_Code equals country.Country_Code
                            join state in _context.States on areaVillage.State_Code equals state.State_Code
                            join district in _context.Districts on areaVillage.District_Code equals district.District_Code
                            join mandal in _context.Mandals on areaVillage.Mandal_Code equals mandal.Mandal_Code
                            join village in _context.Villages on areaVillage.Village_Code equals village.Village_Code
                            where state.State_Code == stateId
                            select new HarvestArea
                            {
                                ID = areaVillage.ID,
                                AreaId = area.Area_ID,
                                AreaName = area.Area_Name,
                                CountryCode = country.Country_Code,
                                CountryName = country.Country_Name,
                                StateCode = state.State_Code,
                                StateName = state.State_Name,
                                DistrictCode = district.District_Code,
                                DistrictName = district.District_Name,
                                MandalCode = mandal.Mandal_Code,
                                MandalName = mandal.Mandal_Name,
                                VillageCode = village.Village_Code,
                                VillageName = village.Village_Name
                            }).OrderByDescending(a => a.ID)
                            .GroupBy(f => f.AreaId)
                            .Select(m => m.FirstOrDefault())
                            .ToListAsync();

            return await allAreas;
        }
    }
}