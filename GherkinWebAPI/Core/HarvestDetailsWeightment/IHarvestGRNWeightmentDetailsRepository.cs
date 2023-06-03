using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Core
{
    public interface IHarvestGRNWeightmentDetailsRepository
    {
        Task<IEnumerable<InwardDetailsDTO>> GetInwardDetails(int orgId);
        Task<IEnumerable<GreensReceptionDetailsDTO>> GetGreenReceptionDetails(int orgId);
        Task<HarvestGRNWeighmentDetailsDTO> AddHarvestGRNDetails(HarvestGRNWeighmentDetailsDTO materialDetails);
    }
}