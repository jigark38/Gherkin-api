using System.Collections.Generic;
using System.Threading.Tasks;
using GherkinWebAPI.Core;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.Employee;

namespace GherkinWebAPI.Service
{
    public class FarmersInputRatesSeasonWiseService : IFarmersInputRatesSeasonWiseService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public FarmersInputRatesSeasonWiseService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Area>> GetFarmerInputAreaDetails(string PS_Number)
        {
            return await _repositoryWrapper.FarmersInputRatesSeasonWiseRepository.GetFarmerInputAreaDetails(PS_Number);
        }

        public async Task<List<RawMaterial>> GetMaterialDetails()
        {
            return await _repositoryWrapper.FarmersInputRatesSeasonWiseRepository.GetMaterialDetails();
        }

        public async Task<FarmersInputRatesSeasonWise> CreateFarmersInputRatesSeasonWise(FarmersInputRatesSeasonWise farmersInputRatesSeasonWise)
        {
            return await _repositoryWrapper.FarmersInputRatesSeasonWiseRepository.CreateFarmersInputRatesSeasonWise(farmersInputRatesSeasonWise);
        }

        public async Task<FarmersInputRatesSeasonWise> UpdateFarmersInputRatesSeasonWise(FarmersInputRatesSeasonWise farmersInputRatesSeasonWise)
        {
            return await _repositoryWrapper.FarmersInputRatesSeasonWiseRepository.UpdateFarmersInputRatesSeasonWise(farmersInputRatesSeasonWise);
        }


        public async Task<List<State>> GetStatesbyCropSeason(string cropGroupCode, string cropNameCode, int countryCode, string PSNumber)
        {
            return await _repositoryWrapper.FarmersInputRatesSeasonWiseRepository.GetStatesbyCropSeason(cropGroupCode, cropNameCode, countryCode, PSNumber);

        }
        public async Task<List<State>> FindStatesbyCropSeason(string cropGroupCode, string cropNameCode, int countryCode, string PSNumber)
        {
            return await _repositoryWrapper.FarmersInputRatesSeasonWiseRepository.FindStatesbyCropSeason(cropGroupCode, cropNameCode, countryCode, PSNumber);
        }
        public async Task<FarmersInputRatesSeasonWise> GetFarmerInputRateSeason(string cropGroupCode, string cropNameCode, int countryCode, string PSNumber, int stateCode)
        {
            return await _repositoryWrapper.FarmersInputRatesSeasonWiseRepository.GetFarmerInputRateSeason(cropGroupCode, cropNameCode, countryCode, PSNumber, stateCode);
        }

        public async Task<List<Employee>> GetEmployeesByDeptCode(string deptId)
        {
            return await _repositoryWrapper.FarmersInputRatesSeasonWiseRepository.GetEmployeesByDeptCode(deptId);
        }

    }
}