using GherkinWebAPI.Models;
using GherkinWebAPI.Models.DailyInputAndFeedingDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.DailyInputAndFeedingDetails
{
    public interface IDailyInputService
    {
        Task<List<AreaName>> GetAllAreas();
        Task<List<EmployeeName>> GetAreaWiseEmployeeDetails(string areaId);
        Task<List<GroupCode>> GetAreaWiseCropGroup(string areaId);
        Task<List<CropName>> GetCropNameCode(string cropGroup);
        Task<List<CropFromTo>> GetAreaWiseSeasonToFrom(string areaId);
        Task<List<CountryName>> GetAreaWiseCountry(string areaId);
        Task<List<StateName>> GetAreaWiseState(string areaId);
        Task<List<DistrictName>> GetStateWiseDistrict(int statecode);
        Task<List<MandalName>> GetDistrictWiseMandal(int districtCode);
        Task<List<Models.DailyInputAndFeedingDetails.VillageName>> GetMandalWiseVillage(int mandalCode);
        Task<List<FarmerAgreementDetails>> GetAreaWiseFarmerAgreementDetails(string areaId, string ps_Number);
        Task<List<FarmerAgreementDetails>> SearchFarmers(string keyword, string areaId, string ps_Number);
    }
}
