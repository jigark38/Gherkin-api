using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DistrictDetail = GherkinWebAPI.Models.Districts.DistrictDetail;

namespace GherkinWebAPI.Core
{
    public interface IDistrictRepository
    {
        Task<List<District>> GetAllDistrictsByStateIdAsync(int stateCode);
        Task<District> GetDistrictByIdAsync(int districtCode);
        Task<DistrictDetail> AddDistrict(DistrictDetail districtDetail);
    }
}