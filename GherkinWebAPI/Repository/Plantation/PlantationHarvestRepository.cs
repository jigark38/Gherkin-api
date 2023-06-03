using GherkinWebAPI.Core;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace GherkinWebAPI.Repository
{
    /// <summary>
    /// Defines the <see cref="PlantationHarvestRepository" />
    /// </summary>
    public class PlantationHarvestRepository : RepositoryBase<HarvestArea>, IPlantationHarvestRepository
    {
        /// <summary>
        /// Defines the _context
        /// </summary>
        private RepositoryContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlantationHarvestRepository"/> class.
        /// </summary>
        /// <param name="repositoryContext">The repositoryContext<see cref="RepositoryContext"/></param>
        public PlantationHarvestRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
            this._context = repositoryContext;
        }

        /// <summary>
        /// The GetAllAreasByAreaIdAsync
        /// </summary>
        /// <param name="areaId">The areaId<see cref="string"/></param>
        /// <returns>The <see cref="Task{List{HarvestArea}}"/></returns>
        public async Task<List<HarvestArea>> GetAllAreasByAreaIdAsync(string areaId)
        {
            List<HarvestArea> res = new List<HarvestArea>();
            Object[] parameters = { areaId };
            return await Task.Run(() => ExecuteQuery("USP_GetAreaDetailsById", parameters).ToList());
        }

        /// <summary>
        /// The GetAllAreasByAreasAsync
        /// </summary>
        /// <returns>The <see cref="Task{List{HarvestArea}}"/></returns>
        public async Task<List<HarvestArea>> GetAllAreasByAreasAsync()
        {
            var allAreas = await (from area in _context.Areas
                                  join areaVillage in _context.HarvestAreaVillages on area.Area_ID equals areaVillage.Area_ID
                                  join country in _context.Countries on areaVillage.Country_Code equals country.Country_Code
                                  join state in _context.States on areaVillage.State_Code equals state.State_Code
                                  join district in _context.Districts on areaVillage.District_Code equals district.District_Code
                                  join village in _context.Villages on areaVillage.Village_Code equals village.Village_Code
                                  select new HarvestArea
                                  {
                                      AreaName = area.Area_Name,
                                      CountryName = country.Country_Name,
                                      StateName = state.State_Name,
                                      DistrictName = district.District_Name,
                                      VillageName = village.Village_Name
                                  }).Take(10).ToListAsync();

            return allAreas;
        }

        /// <summary>
        /// The AddArea
        /// </summary>
        /// <param name="area">The area<see cref="Area"/></param>
        public void AddArea(Area area)
        {
            _context.Areas.Add(area);
            _context.SaveChanges();
        }

        /// <summary>
        /// The AddAreaVillage
        /// </summary>
        /// <param name="harvestAreaVillage">The harvestAreaVillage<see cref="HarvestAreaVillage"/></param>
        public void AddAreaVillage(HarvestAreaVillage harvestAreaVillage)
        {
            _context.HarvestAreaVillages.Add(harvestAreaVillage);
            _context.SaveChanges();
        }

        /// <summary>
        /// The AddPlantationSchedule
        /// </summary>
        /// <param name="harvestAreaVillage">The harvestAreaVillage<see cref="PlantationSchedule"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task<PlantationSchedule> AddPlantationSchedule(PlantationSchedule plantationSchedule)
        {
            if (await _context.PlantationSchedules.AnyAsync())
            {
                var maxId = _context.PlantationSchedules.OrderByDescending(c => c.Id).Take(1).FirstOrDefault().Id;
                plantationSchedule.PsNumber = $"PSNO_{maxId + 1}";
            }
            else
            {
                plantationSchedule.PsNumber = $"PSNO_{1}";
            }

            var result = _context.PlantationSchedules.Add(plantationSchedule);
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<PlantationSchedule> UpdatePlantationSchedule(PlantationSchedule plantationSchedule)
        {
            var existingPlantation = await _context.PlantationSchedules.FirstOrDefaultAsync(p => p.PsNumber == plantationSchedule.PsNumber);
            if (existingPlantation == null)
            {
                throw new KeyNotFoundException("Plantation schedule not found with this ID");
            }

            existingPlantation.PreparedBy = plantationSchedule.PreparedBy;
            existingPlantation.ToDate = plantationSchedule.ToDate;
            existingPlantation.FromDate = plantationSchedule.FromDate;
            existingPlantation.CropGroupCode = plantationSchedule.CropGroupCode;
            existingPlantation.CropNameCode = plantationSchedule.CropNameCode;
            existingPlantation.ApprovedBy = plantationSchedule.ApprovedBy;
            existingPlantation.PsDate = plantationSchedule.PsDate;

            _context.Entry(existingPlantation).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return existingPlantation;
        }

        public async Task<List<PlantationSchedule>> GetPlantationSchedules(string cropGroup = null, string cropName = null)
        {
            var schedules = from sch in _context.PlantationSchedules
                            select sch;

            if (!string.IsNullOrEmpty(cropGroup))
            {
                schedules = schedules.Where(i => i.CropGroupCode == cropGroup);
            }

            if (!string.IsNullOrEmpty(cropName))
            {
                schedules = schedules.Where(i => i.CropNameCode == cropName);
            }

            return await schedules.ToListAsync();
        }
    }
}
