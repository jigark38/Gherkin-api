using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core
{
   public interface ICityOverseasService
    {
        Task<List<CityOverseas>> GetAllCityOverseasByStateIdAsync(string statecode);
        Task<CityOverseas> GetcityOverseasByIdAsync(string cityCode);
        Task<CityOverseas> AddCityIfntAsync(CityOverseas cities);
    }
}
