using GherkinWebAPI.Core;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service
{
    public class PlantationHarvestService : IPlantationHarvestService
    {
        public IPlantationHarvestRepository _repository { get; }

        public PlantationHarvestService(IPlantationHarvestRepository repository)
        {
            this._repository = repository;
        }

        public Task<List<HarvestArea>> GetAllAreasByAreaIdAsync(string areaId)
        {
            return _repository.GetAllAreasByAreaIdAsync(areaId);
        }

        public Task<List<HarvestArea>> GetAllAreasByAreasAsync()
        {
            return _repository.GetAllAreasByAreasAsync();
        }

        public void AddArea(Area area)
        {
            _repository.AddArea(area);
        }

        public void AddAreaVillage(HarvestAreaVillage harvestAreaVillage)
        {
            _repository.AddAreaVillage(harvestAreaVillage);
        }

        public async Task<PlantationSchedule> SchedulePlanation(PlantationSchedule plantationSchedule)
        {
           return await _repository.AddPlantationSchedule(plantationSchedule);
        }

        public async Task<PlantationSchedule> UpdatePlantationSchedule(PlantationSchedule plantationSchedule)
        {
            return await _repository.UpdatePlantationSchedule(plantationSchedule);
        }

        public async Task<List<PlantationSchedule>> GetPlantationSchedules(string cropGroup = null, string cropName = null)
        {
            return await _repository.GetPlantationSchedules(cropGroup, cropName);
        }
    }
}