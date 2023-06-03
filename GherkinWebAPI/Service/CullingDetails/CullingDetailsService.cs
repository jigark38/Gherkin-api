using GherkinWebAPI.Core.CullingDetails;
using GherkinWebAPI.DTO.CullingDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service.CullingDetails
{
    public class CullingDetailsService : ICullingDetailsService
    {
        private readonly ICullingDetailsRepository _cullingDetailsRepository;
        public CullingDetailsService(ICullingDetailsRepository cullingDetailsRepository)
        {
            _cullingDetailsRepository = cullingDetailsRepository;
        }

        public async Task AddCullingDetails()
        {
            await _cullingDetailsRepository.AddCullingDetails();
        }

        public async Task<List<GradeMaterialDetails>> GetGradedMaterialDetails(int orgOfficeNo)
        {
            return await _cullingDetailsRepository.GetGradedMaterialDetails(orgOfficeNo);
        }
    }
}