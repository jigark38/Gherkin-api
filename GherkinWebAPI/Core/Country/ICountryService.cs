using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core
{
    public interface ICountryService
    {
        Task<List<Country>> GetAllCountriesAsync();
        Task<CountryDetail> GetCountryByIdAsync(int countryCode);

        Task<Country> AddCountry(string countryName);
    }
}
