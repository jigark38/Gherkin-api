using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core
{
   public interface IAreaMRInwardService
    {
        IEnumerable<AreaMRInwardDetails> GetAllAsync();
        Task<IEnumerable<ARMaterialOutwadDetailsDTO>> GetAMRbyAreaId(string areadId);
        Task<bool> CreateAreaDetails(AreaMRInwardPostDetails AreaMaterialReceivedDetailsModel);
        Task<AreaMRInwardDetails> UpdateAreaDetails(int id, AreaMRInwardDetails AreaMaterialReceivedDetailsModel);

        IEnumerable<ARMaterialGridUnderDTO> Getnote2(string areaId, string RMTransferNo);

        IEnumerable<NOTE1> Getnote1(string areaId, string RMTransferNo);

        long GetAreaMRNo();
    }
}
