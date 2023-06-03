using GherkinWebAPI.DTO.HarvestDetails;
using GherkinWebAPI.Models.HarvestDeatils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.HarvestDetails
{
    public interface IHarvestDetailsRepository
    {
        Task<List<FarmerDetailsDto>> GetFarmerDetails(string areaId, string psNumber);
        Task AddHarvestFarmersDetails(HarvestProcurementDetails harvestDetailsDto);
    }
}
