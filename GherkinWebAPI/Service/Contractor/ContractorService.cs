using GherkinWebAPI.Core;
using GherkinWebAPI.Core.Contractor;
using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using contractor = GherkinWebAPI.Models.Contractor;

namespace GherkinWebAPI.Service.Contractor
{
    public class ContractorService : IContratorService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public ContractorService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public async Task<List<contractor>> GetAllContractor()
        {
            return await _repositoryWrapper.ContractorRepository.GetAllContractor();
        }
        public async Task<contractor> GetEmployeeByContractorCode(string contractorCode)
        {
            return await _repositoryWrapper.ContractorRepository.GetEmployeeByContractorCode(contractorCode);
        }
        public async Task<contractor> CreateContractor(contractor ctor)
        {
            return await _repositoryWrapper.ContractorRepository.CreateContractor(ctor);
        }
    }
}