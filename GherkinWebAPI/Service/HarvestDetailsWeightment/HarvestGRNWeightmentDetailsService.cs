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
    
    public class HarvestGRNWeightmentDetailsService : IHarvestGRNWeightmentDetailsService
    {
        public IRepositoryWrapper _repository { get; }
               
        public HarvestGRNWeightmentDetailsService(IRepositoryWrapper repository)
        {
            this._repository = repository;
        }
        public async Task<IEnumerable<InwardDetailsDTO>> GetInwardDetails(int orgId)
        {
            return await  _repository.HarvestGRNWeightmentDetails.GetInwardDetails(orgId);
        }
        public async Task<IEnumerable<GreensReceptionDetailsDTO>> GetGreenReceptionDetails(int orgId)
        {
            return await _repository.HarvestGRNWeightmentDetails.GetGreenReceptionDetails(orgId);
        }

        public async Task<HarvestGRNWeighmentDetailsDTO> AddHarvestGRNDetails(HarvestGRNWeighmentDetailsDTO materialDetails)
        {
            return await _repository.HarvestGRNWeightmentDetails.AddHarvestGRNDetails(materialDetails);
        }

        //public async Task<HarvestGRNInwardDetails> AddHarvestGRNInwardDetails(HarvestGRNInwardDetails details)
        //{
        //    return await _repository.HarvestGRNWeightmentDetails.AddHarvestGRNInwardDetails(details);
        //}
        //public async Task<HarvestGRNIMWeightDetails> AddHarvestGRNIMWeightDetails(HarvestGRNIMWeightDetails details)
        //{
        //    return await _repository.HarvestGRNWeightmentDetails.AddHarvestGRNIMWeightDetails(details);
        //}
    }
}