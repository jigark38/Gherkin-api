using GherkinWebAPI.Core;
using GherkinWebAPI.Core.ProfessionalTaxRates;
using GherkinWebAPI.Models.ProfessionalTaxRates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Service.ProfessionalTaxRates
{
    public class ProfessionalTaxMasterService : IProfessionalTaxMasterService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public ProfessionalTaxMasterService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public async Task<ProfessionalTaxMaster> CreateProfessionalTaxMaster(ProfessionalTaxMaster professionalTaxMaster)
        {
            return await _repositoryWrapper.professionalTaxMasterRepository.CreateProfessionalTaxMaster(professionalTaxMaster);
        }

        public async Task<List<ProfessionalTax>> GetProfessionalTaxRates()
        {
            return await _repositoryWrapper.professionalTaxMasterRepository.GetProfessionalTaxRates();
        }

        public async Task<ProfessionalTaxMaster> UpdateProfessionalTaxMaster(long passingNo, ProfessionalTaxMaster professionalTaxMaster)
        {
            return await _repositoryWrapper.professionalTaxMasterRepository.UpdateProfessionalTaxMaster(passingNo, professionalTaxMaster);
        }
    }
}