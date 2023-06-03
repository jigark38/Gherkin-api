using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.AreaMaterialReceivedDetails
{
   public interface IAreaMRInwardRepository
    {
        IEnumerable<AreaMRInwardDetails> GetAllAsync();
        Task<bool> CreateAreaDetails(AreaMRInwardPostDetails AreaMaterialReceivedDetailsModel);
        Task<AreaMRInwardDetails> UpdateAreaDetails(int id, AreaMRInwardDetails AreaMaterialReceivedDetailsModel);
        Task<IEnumerable<ARMaterialOutwadDetailsDTO>> GetMaterialOutwadDetailsByAreaId(string areaId);

        IEnumerable<ARMaterialGridUnderDTO> Getnote2(string areaId,string RMTransferNo);

        IEnumerable<NOTE1> Getnote1(string areaId, string RMTransferNo);

        long GetAreaMRNo();
    }
}