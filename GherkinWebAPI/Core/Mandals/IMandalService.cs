using GherkinWebAPI.Models.Mandals;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.Mandals
{
    public interface IMandalService
    {
        Task<List<MandalDetail>> GetAllMandalsAysnc();
        Task<List<MandalDetail>> GetMandalByDistrictCodeAsync(int districtCode);
        Task<MandalDetail> AddMandal(MandalDetail mandal);
    }
}
