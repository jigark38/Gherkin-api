using GherkinWebAPI.Core;
using GherkinWebAPI.CustomExceptions;
using GherkinWebAPI.Models;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Repository
{
    public class CountryOverseasRepository : RepositoryBase<CountryOverseas>, ICountyOverseasRepository
    {
        private RepositoryContext _context;

        public CountryOverseasRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            this._context = repositoryContext;
        }

        public async Task<List<CountryOverseas>> GetAllCountriesOverseasAsync()
        {
            //Need to check in local first before we change
            var countryoverseas = await Task.Run(() => ((from CountryOverseas in _context.countriesoverseas
                                                         select new { CountryOverseas.W_Country_Id, CountryOverseas.W_Country_Name })
                                                   .AsEnumerable()
                                                   .Select(c => new CountryOverseas
                                                   {
                                                       W_Country_Id = c.W_Country_Id,
                                                       W_Country_Name = c.W_Country_Name
                                                   }).ToList()));
            return countryoverseas;
        }

        public async Task<CountryOverseas> GetCountryOverseasByIdAsync(string countryCode)
        {
            var countryoverseasDetails = await (from CountryOverseas in _context.countriesoverseas
                                                where CountryOverseas.W_Country_Id == countryCode
                                                select CountryOverseas
                                                       ).AsNoTracking().FirstOrDefaultAsync();
            return countryoverseasDetails;
        }



        public async Task<CountryOverseas> AddCountryIfntAsync(CountryOverseas country)
        {
            CountryOverseas obj = null;
            var res = await this._context.countriesoverseas.FirstOrDefaultAsync(x => x.W_Country_Name == country.W_Country_Name);
            if (res == null)
            {

                int? selectMaxDeptId = await _context.countriesoverseas.MaxAsync(e => (int?)e.Id);
                if (selectMaxDeptId != null)
                    country.W_Country_Id = "IND_" + Convert.ToString(selectMaxDeptId + 1);
                else
                    country.W_Country_Id = "IND_" + "1";

                CountryOverseas cons = new CountryOverseas { W_Country_Id = country.W_Country_Id, W_Country_Name = country.W_Country_Name };
                _context.countriesoverseas.Add(cons);
                var result = await _context.SaveChangesAsync();

                obj = await (from c in _context.countriesoverseas
                             where c.W_Country_Name == country.W_Country_Name
                             select c
                                                           ).FirstOrDefaultAsync();
                return obj;
            }
            else
            {
                throw new CustomException("Country Is already Exits");
            }

        }
    }
}
