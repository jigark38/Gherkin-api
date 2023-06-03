using GherkinWebAPI.Core;
using GherkinWebAPI.Core.LoansAndAdvancesDetails;
using GherkinWebAPI.Models.LoansAndAdvancesDetails;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Service.LoansAndAdvancesDetails
{
    public class LoansAdvancesService : ILoansAdvancesService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public LoansAdvancesService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public async Task<LoansAdvancesDTO> CreateLoansAdvances(LoansAdvancesDTO loansAdvancesDTO)
        {
            return await _repositoryWrapper.LoansAdvancesRepository.CreateLoansAdvances(loansAdvancesDTO);
        }

        public async Task<string> GetLoansAdvanceNo()
        {
            return await _repositoryWrapper.LoansAdvancesRepository.GetLoansAdvanceNo();
        }

        public async Task<List<LoansAdvancesDTO>> SearchLoansAdvances(int orgOfficeNo, string employeeId)
        {
            return await _repositoryWrapper.LoansAdvancesRepository.SearchLoansAdvances(orgOfficeNo, employeeId);
        }

        public async Task<LoansAdvancesDTO> UpdateLoansAdvances(LoansAdvancesDTO loansAdvancesDTO)
        {
            return await _repositoryWrapper.LoansAdvancesRepository.UpdateLoansAdvances(loansAdvancesDTO);
        }
    }
}