using GherkinWebAPI.Core;
using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service
{
    public class CityOverseasService : ICityOverseasService
    {
        public ICityOverseasRepository _repository { get; }

        public CityOverseasService(ICityOverseasRepository repository)
        {
            this._repository = repository;
        }

        public Task<List<CityOverseas>> GetAllCityOverseasByStateIdAsync(string stateCode)
        {
            return _repository.GetAllCityOverseasByStateIdAsync(stateCode);
        }

        public Task<CityOverseas> GetcityOverseasByIdAsync(string cityCode)
        {
            return _repository.GetcityOverseasByIdAsync(cityCode);
        }

        public Task<CityOverseas> AddCityIfntAsync(CityOverseas cities)
        {
            return _repository.AddCityIfntAsync(cities);
        }
    }
}