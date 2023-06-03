using System.Collections.Generic;
using System.Threading.Tasks;
using GherkinWebAPI.Models.Mandals;

namespace GherkinWebAPI.Core.Mandals
{
    public interface IMandalRepository
    {
        Task<List<MandalDetail>> GetAllMandalsAysnc();
        Task<List<MandalDetail>> GetMandalByDistrictCodeAsync(int districtCode);
        Task<MandalDetail> AddMandal(MandalDetail mandal);
    }
}
