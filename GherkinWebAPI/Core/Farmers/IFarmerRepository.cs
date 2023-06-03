using System.Collections.Generic;
using System.Threading.Tasks;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.Farmers;
using GherkinWebAPI.Models.FarmersInputReturns;

namespace GherkinWebAPI.Core
{
    public interface IFarmerRepository
    {
        Task<List<Farmer>> GetAllFarmers();
        Task<FarmerDetailsDTO> GetFarmersByCode(string code);
        Task<List<FarmersDetail>> GetFarmersByVillageCode(int villageCode);
        Task<Farmer> AddFarmer(FarmerDetailsDTO farmerDetail);
        Task AddBankAccountDetails(List<FarmerBankDetailsDTO> bankDetailList);
        void UpdateFarmer(string id, FarmerDetailsDTO farmer);
        Task SaveFarmerDocument(FarmerDocument document);
        Task<List<FarmerDocument>> GetFarmerDocumentsbyFarmer(string farmerCode);
        Task<FarmerDocument> GetFarmerDocumentbyID(int Id);
        Task DeleteFarmerDocumentbyID(int id);

        Task<List<Farmer>> GetFarmerByStateCode(int stateCode);
        Task<List<FarmerAndVillage>> GetFarmerListByAreaEmployeePSNumberAndFarmerName(string farmerName, string areaId, string employeeId, string psNumber);
        Task<List<FarmerAndVillage>> GetFarmerListByAreaEmployeePSNumberAndFarmerAltContactPerson(string farmerAltContactPerson, string areaId, string employeeId, string psNumber);
        Task<List<FarmerAndVillage>> GetFarmerListByAreaAndVillageCodeAndPSNumber(string areaId, string psNumber, int villageCode);
        Task<FarmerAndVillage> GetFarmerByAreaAndPSNumberAndAccountNo(string areaId, string psNumber, string accountNo);
        Task<List<FarmerAndVillage>> GetAllFarmersWithAgreementDetail();
    }
}
