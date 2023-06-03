using GherkinWebAPI.Core;
using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service
{
    public class CountryService : ICountryService
    {
        public ICountryRepository _repository { get; }

        public CountryService(ICountryRepository repository)
        {
            this._repository = repository;
        }
        public async Task<List<Country>> GetAllCountriesAsync()
        {
            return await _repository.GetAllCountriesAsync();
        }

        public async Task<CountryDetail> GetCountryByIdAsync(int countryCode)
        {
            return await _repository.GetCountryByIdAsync(countryCode);
        }

        public async Task<Country> AddCountry(string countryName)
        {
            return await _repository.AddCountry(countryName);
        }
    }
}