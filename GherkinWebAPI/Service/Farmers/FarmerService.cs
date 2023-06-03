using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GherkinWebAPI.Core;
using GherkinWebAPI.Models;
using System.Threading.Tasks;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Models.Farmers;
using GherkinWebAPI.Models.FarmersInputReturns;

namespace GherkinWebAPI.Service
{
    public class FarmerService : IFarmerService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public FarmerService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public async Task<Farmer> AddFarmer(FarmerDetailsDTO farmer)
        {
            return await _repositoryWrapper.FarmerRepository.AddFarmer(farmer);
        }

        public async Task AddBankAccountDetails(List<FarmerBankDetailsDTO> bankDetailList)
        {
            await _repositoryWrapper.FarmerRepository.AddBankAccountDetails(bankDetailList);
        }

        public async Task UpdateFarmer(string id, FarmerDetailsDTO farmer)
        {
            _repositoryWrapper.FarmerRepository.UpdateFarmer(id, farmer);
            await _repositoryWrapper.SaveAsync();
        }

        public async Task<List<Farmer>> GetAllFarmers()
        {
            return await _repositoryWrapper.FarmerRepository.GetAllFarmers();
        }

        public async Task<FarmerDetailsDTO> GetFarmersByCode(string code)
        {
            return await _repositoryWrapper.FarmerRepository.GetFarmersByCode(code);
        }

        public async Task SaveFarmerDocuments(IEnumerable<FarmerDocument> documents)
        {
            if (documents.Count() > 0)
            {
                foreach (var document in documents)
                {
                    await _repositoryWrapper.FarmerRepository.SaveFarmerDocument(document);
                }
            }

        }
        public async Task SaveFarmerDocument(FarmerDocument document)
        {
            if (document != null)
            {
                await _repositoryWrapper.FarmerRepository.SaveFarmerDocument(document);
            }

        }
        public async Task<List<FarmerDocument>> GetFarmerDocumentsbyFarmer(string farmerCode)
        {
            return await _repositoryWrapper.FarmerRepository.GetFarmerDocumentsbyFarmer(farmerCode);
        }
        public async Task<FarmerDocument> GetFarmerDocumentbyID(int ID)
        {
            return await _repositoryWrapper.FarmerRepository.GetFarmerDocumentbyID(ID);
        }

        public async Task<List<FarmersDetail>> GetFarmersByVillageCode(int villageCode)
        {
            return await _repositoryWrapper.FarmerRepository.GetFarmersByVillageCode(villageCode);
        }
        public async Task DeleteFarmerDocumentbyID(int id)
        {
            await _repositoryWrapper.FarmerRepository.DeleteFarmerDocumentbyID(id);
        }

        public async Task<List<Farmer>> GetFarmerByStateCode(int stateCode)
        {
            return await _repositoryWrapper.FarmerRepository.GetFarmerByStateCode(stateCode);
        }

        public async Task<List<FarmerAndVillage>> GetFarmerListByAreaEmployeePSNumberAndFarmerName(string farmerName, string areaId, string employeeId, string psNumber)
		{
            return await _repositoryWrapper.FarmerRepository.GetFarmerListByAreaEmployeePSNumberAndFarmerName(farmerName, areaId, employeeId, psNumber);
        }

        public async Task<List<FarmerAndVillage>> GetFarmerListByAreaEmployeePSNumberAndFarmerAltContactPerson(string farmerAltContactPerson, string areaId, string employeeId, string psNumber)
        {
            return await _repositoryWrapper.FarmerRepository.GetFarmerListByAreaEmployeePSNumberAndFarmerAltContactPerson(farmerAltContactPerson, areaId, employeeId, psNumber);
        }

        public async Task<List<FarmerAndVillage>> GetFarmerListByAreaAndVillageCodeAndPSNumber(string areaId, string psNumber, int villageCode)
        {
            return await _repositoryWrapper.FarmerRepository.GetFarmerListByAreaAndVillageCodeAndPSNumber(areaId, psNumber,villageCode);
        }

        public async Task<FarmerAndVillage> GetFarmerByAreaAndPSNumberAndAccountNo(string areaId, string psNumber, string accountNo)
        {
            return await _repositoryWrapper.FarmerRepository.GetFarmerByAreaAndPSNumberAndAccountNo(areaId, psNumber, accountNo);
        }

        public async Task<List<FarmerAndVillage>> GetAllFarmersWithAgreementDetail()
        {
            return await _repositoryWrapper.FarmerRepository.GetAllFarmersWithAgreementDetail();
        }
    }
}