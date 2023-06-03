using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.Harvest_Area
{
    public interface IHarvestAreaService
    {
        Task<List<HarvestArea>> SearchArea(string areaId);
        Task<List<Area>> GetAreaNameAndCodeAsync();
        Task<Area> AddArea(Area area);
        Task<bool> IsAreaCodeAllowed(int areaCode);
        Task<bool> IsAreaNameAllowed(string areaName);
        Task<List<HarvestArea>> SearchAreaByStateCode(int stateId);
    }
}
