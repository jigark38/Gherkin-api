using GherkinWebAPI.Core;
using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service
{
    public class CountryOverseasService : ICountryOverseasService
    {
        public ICountyOverseasRepository _repository { get; }

        public CountryOverseasService(ICountyOverseasRepository repository)
        {
            this._repository = repository;
        }
        public Task<List<CountryOverseas>> GetAllCountriesOverseasAsync()
        {
            return _repository.GetAllCountriesOverseasAsync();
        }

        public Task<CountryOverseas> GetCountryOverseasByIdAsync(string countryCode)
        {
            return _repository.GetCountryOverseasByIdAsync(countryCode);
        }

        public Task<CountryOverseas> AddCountryIfntAsync(CountryOverseas country)
        {
            return _repository.AddCountryIfntAsync(country);
        }


    }
}