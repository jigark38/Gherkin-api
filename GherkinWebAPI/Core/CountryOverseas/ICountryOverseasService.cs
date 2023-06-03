using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core
{
    public interface ICountryOverseasService
    {
        Task<List<CountryOverseas>> GetAllCountriesOverseasAsync();
        Task<CountryOverseas> GetCountryOverseasByIdAsync(string countryCode);
        Task<CountryOverseas> AddCountryIfntAsync(CountryOverseas country);


    }
}
