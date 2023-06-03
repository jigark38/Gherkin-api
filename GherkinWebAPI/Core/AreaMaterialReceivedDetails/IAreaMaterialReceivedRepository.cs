using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;

namespace GherkinWebAPI.Core
{
   public interface IAreaMaterialReceivedRepository
    {
        List<Area> GetHarvestAreaDetails();
       IEnumerable<AreaMaterialReceivedDetailsModel> GetAllAsync();
        Task<AreaMaterialReceivedDetailsModel> CreateAreaDetails(AreaMaterialReceivedDetailsModel AreaMaterialReceivedDetailsModel);
        Task<AreaMaterialReceivedDetailsModel> UpdateAreaDetails(int id, AreaMaterialReceivedDetailsModel AreaMaterialReceivedDetailsModel);
    }
}