using GherkinWebAPI.Models;
using GherkinWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core
{
    public interface IFarmersAgreementRepository
    {
        Task<string> GetFarmersAgreementCodeAsync();
        Task<FarmersAgreement> SearchAgreement(string areaId, int cityCode, string farmersCode, string cropGroupCode, string cropNameCode, string psNumber);
        Task<FarmersAgreementDetail> CreateAgreement(FarmersAgreementDetail farmersAgreement);
        Task<ApiResponse<object>> UpdateAgreement(string farmersAgreementCode, FarmersAgreement farmersAgreement);

        Task<List<FarmersAgreementDetail>> GetFarmersAgreementDetailsByAreaId(string areaId);

        Task<FieldStaffDetails> GetAreaInchargeDetailsByAreaId(string areaId, DateTime date);

        Task<bool> ValidateFarmerAccount(string FarmersAccountNo, string FarmerCode, string PSNumber);
        Task<List<string>> FarmerAccountList(string FarmersAccountNo, string FarmerCode, string PSNumber);
        Task<FarmersAgreement> FarmerDetailsByAccount(string FarmersAccountNo, string PSNumber);

    }
}
