using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core
{
    /// <summary>
    /// Defines the <see cref="IPlantationHarvestRepository" />
    /// </summary>
    public interface IPlantationHarvestRepository
    {
        /// <summary>
        /// The GetAllAreasByAreaIdAsync
        /// </summary>
        /// <param name="areaId">The areaId<see cref="string"/></param>
        /// <returns>The <see cref="Task{List{HarvestArea}}"/></returns>
        Task<List<HarvestArea>> GetAllAreasByAreaIdAsync(string areaId);

        /// <summary>
        /// The GetAllAreasByAreasAsync
        /// </summary>
        /// <returns>The <see cref="Task{List{HarvestArea}}"/></returns>
        Task<List<HarvestArea>> GetAllAreasByAreasAsync();

        /// <summary>
        /// The AddArea
        /// </summary>
        /// <param name="area">The area<see cref="Area"/></param>
        void AddArea(Area area);

        /// <summary>
        /// The AddAreaVillage
        /// </summary>
        /// <param name="harvestAreaVillage">The harvestAreaVillage<see cref="HarvestAreaVillage"/></param>
        void AddAreaVillage(HarvestAreaVillage harvestAreaVillage);

        Task<PlantationSchedule> UpdatePlantationSchedule(PlantationSchedule plantationSchedule);

        /// <summary>
        /// The AddPlantationSchedule
        /// </summary>
        /// <param name="harvestAreaVillage">The harvestAreaVillage<see cref="PlantationSchedule"/></param>
        /// <returns>The <see cref="Task"/></returns>
        Task<PlantationSchedule> AddPlantationSchedule(PlantationSchedule harvestAreaVillage);
        Task<List<PlantationSchedule>> GetPlantationSchedules(string cropGroup = null, string cropName = null);
    }
}
