using GherkinWebAPI.Models;
using GherkinWebAPI.Models.Employee;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core
{
    public interface IFarmersInputRatesSeasonWiseRepository
    {
        Task<List<Area>> GetFarmerInputAreaDetails(string PS_Number);
        Task<List<RawMaterial>> GetMaterialDetails();
        Task<FarmersInputRatesSeasonWise> CreateFarmersInputRatesSeasonWise(FarmersInputRatesSeasonWise farmersInputRatesSeasonWise);
        Task<FarmersInputRatesSeasonWise> UpdateFarmersInputRatesSeasonWise(FarmersInputRatesSeasonWise farmersInputRatesSeasonWise);
        Task<List<State>> GetStatesbyCropSeason(string cropGroupCode, string cropNameCode, int countryCode, string PSNumber);
        Task<List<State>> FindStatesbyCropSeason(string cropGroupCode, string cropNameCode, int countryCode, string PSNumber);
        Task<FarmersInputRatesSeasonWise> GetFarmerInputRateSeason(string cropGroupCode, string cropNameCode, int countryCode, string PSNumber, int stateCode);
        Task<List<Employee>> GetEmployeesByDeptCode(string deptId);

    }
}
