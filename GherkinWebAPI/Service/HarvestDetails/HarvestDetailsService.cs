using GherkinWebAPI.Core.HarvestDetails;
using GherkinWebAPI.DTO.HarvestDetails;
using GherkinWebAPI.Models.HarvestDeatils;
using GherkinWebAPI.Persistence;
using GherkinWebAPI.Repository.HarvestDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service.HarvestDetails
{
    public class HarvestDetailsService : IHarvestDetailsService
    {
        IHarvestDetailsRepository _harvestDetailsRepository;
        public HarvestDetailsService(IHarvestDetailsRepository harvestDetailsRepository)
        {
            _harvestDetailsRepository = harvestDetailsRepository;
        }

        public async Task AddHarvestFarmersDetails(HarvestProcurementDetails harvestDetailsDto)
        {
            await _harvestDetailsRepository.AddHarvestFarmersDetails(harvestDetailsDto);
        }

        public async Task<List<FarmerDetailsDto>> GetFarmerDetails(string areaId, string psNumber)
        {
            return await _harvestDetailsRepository.GetFarmerDetails(areaId, psNumber);
        }
    }
}