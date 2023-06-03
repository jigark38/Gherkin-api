using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;

namespace GherkinWebAPI.Core
{
   public interface IAreaMaterialReceivedService
    {
        List<Area> GetHarvestAreaDetails();
        IEnumerable<AreaMaterialReceivedDetailsModel> AllAsync();
        Task<AreaMaterialReceivedDetailsModel> CreateAreaDetails(AreaMaterialReceivedDetailsModel AreaMaterialReceivedDetailsModel);
        Task<AreaMaterialReceivedDetailsModel> UpdateAreaDetails(int id, AreaMaterialReceivedDetailsModel AreaMaterialReceivedDetailsModel);
    }
}
