using GherkinWebAPI.Core;
using GherkinWebAPI.Core.DailyInputAndFeedingDetails;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.DailyInputAndFeedingDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service.DailyInputAndFeedingDetails
{
    public class DailyInputService: IDailyInputService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public DailyInputService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public async Task<List<AreaName>> GetAllAreas()
        {
            return await _repositoryWrapper.DailyInputRepository.GetAllAreas();
        }
        public async Task<List<EmployeeName>> GetAreaWiseEmployeeDetails(string areaId)
        {
            return await _repositoryWrapper.DailyInputRepository.GetAreaWiseEmployeeDetails(areaId);
        }
        public async Task<List<GroupCode>> GetAreaWiseCropGroup(string areaId)
        {
            return await _repositoryWrapper.DailyInputRepository.GetAreaWiseCropGroup(areaId);
        }
        public async Task<List<CropName>> GetCropNameCode(string cropGroup)
        {
            return await _repositoryWrapper.DailyInputRepository.GetCropNameCode(cropGroup);
        }
        public async Task<List<CropFromTo>> GetAreaWiseSeasonToFrom(string areaId)
        {
            return await _repositoryWrapper.DailyInputRepository.GetAreaWiseSeasonToFrom(areaId);
        }
        public async Task<List<CountryName>> GetAreaWiseCountry(string areaId)
        {
            return await _repositoryWrapper.DailyInputRepository.GetAreaWiseCountry(areaId);
        }
        public async Task<List<StateName>> GetAreaWiseState(string areaId)
        {
            return await _repositoryWrapper.DailyInputRepository.GetAreaWiseState(areaId);
        }
        public async Task<List<DistrictName>> GetStateWiseDistrict(int statecode)
        {
            return await _repositoryWrapper.DailyInputRepository.GetStateWiseDistrict(statecode);
        }
        public async Task<List<MandalName>> GetDistrictWiseMandal(int districtCode)
        {
            return await _repositoryWrapper.DailyInputRepository.GetDistrictWiseMandal(districtCode);
        }
        public async Task<List<Models.DailyInputAndFeedingDetails.VillageName>> GetMandalWiseVillage(int mandalCode)
        {
            return await _repositoryWrapper.DailyInputRepository.GetMandalWiseVillage(mandalCode);
        }
        public async Task<List<FarmerAgreementDetails>> GetAreaWiseFarmerAgreementDetails(string areaId, string ps_Number)
        {
            return await _repositoryWrapper.DailyInputRepository.GetAreaWiseFarmerAgreementDetails(areaId,ps_Number);
        }
        public async Task<List<FarmerAgreementDetails>> SearchFarmers(string keyword, string areaId, string ps_Number)
        {
            return await _repositoryWrapper.DailyInputRepository.SearchFarmers(keyword, areaId, ps_Number);
        }
    }
}