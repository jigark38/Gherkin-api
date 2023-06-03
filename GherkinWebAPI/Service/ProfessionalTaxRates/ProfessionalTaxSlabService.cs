using GherkinWebAPI.Core;
using GherkinWebAPI.Core.ProfessionalTaxRates;
using GherkinWebAPI.Models.ProfessionalTaxRates;
using System.Threading.Tasks;

namespace GherkinWebAPI.Service.ProfessionalTaxRates
{
    public class ProfessionalTaxSlabService : IProfessionalTaxSlabService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public ProfessionalTaxSlabService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public async Task<ProfessionalTaxSlabsDetail> CreateProfessionalSlab(ProfessionalTaxSlabsDetail professionalTaxSlab)
        {
            return await _repositoryWrapper.professionalTaxSlabRepository.CreateProfessionalSlab(professionalTaxSlab);
        }

        public async Task<ProfessionalTaxSlabsDetail> UpdateProfessionalSlab(long passingNo, ProfessionalTaxSlabsDetail professionalTaxSlab)
        {
            return await _repositoryWrapper.professionalTaxSlabRepository.UpdateProfessionalSlab(passingNo, professionalTaxSlab);
        }
    }
}