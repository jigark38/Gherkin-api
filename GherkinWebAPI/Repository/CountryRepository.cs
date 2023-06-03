using GherkinWebAPI.Core;
using GherkinWebAPI.CustomExceptions;
using GherkinWebAPI.Models;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Repository
{
    public class CountryRepository : RepositoryBase<Country>, ICountryRepository
    {
        private RepositoryContext _context;

        public CountryRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            this._context = repositoryContext;
        }
        public async Task<List<Country>> GetAllCountriesAsync()
        {
            var countries = await Task.Run(() => (from country in _context.Countries
                                                   select new { country.Country_Code, country.Country_Name })
                                                   .AsEnumerable()
                                                   .Select(c => new Country
                                                   {
                                                       Country_Code = c.Country_Code,
                                                       Country_Name = c.Country_Name
                                                   }).ToList());
            return countries;
        }

        public async Task<CountryDetail> GetCountryByIdAsync(int countryCode)
        {
            var countryDetails = (from c in _context.Countries
                                  join s in _context.States on c.Country_Code equals s.Country_Code into s1
                                  from s2 in s1.DefaultIfEmpty()
                                  join d in _context.Districts on s2.State_Code equals d.State_Code into d1
                                  from d2 in d1.DefaultIfEmpty()
                                  join m in _context.Mandals on d2.District_Code equals m.District_Code into m1
                                  from m2 in m1.DefaultIfEmpty()
                                  join v in _context.Villages on m2.Mandal_Code equals v.Mandal_Code into v1
                                  from v2 in v1.DefaultIfEmpty()
                                  where c.Country_Code == countryCode
                                  select new CountryDetail
                                  {
                                      CountryCode = c.Country_Code,
                                      CountryName = c.Country_Name,
                                      States = (from state in c.States
                                                select new StateDetail
                                                {
                                                    StateCode = state.State_Code,
                                                    StateName = state.State_Name,
                                                    Districts = (from dis in state.Districts
                                                                 select new DistrictDetail
                                                                 {
                                                                     DistrictCode = dis.District_Code,
                                                                     DistrictName = dis.District_Name,
                                                                     Mandals = (from mandal in dis.Mandals
                                                                                select new MandalDetail
                                                                                {
                                                                                    MandalCode = mandal.Mandal_Code,
                                                                                    MandalName = mandal.Mandal_Name,
                                                                                    Villages = (from village in mandal.Villages
                                                                                                select new VillageDetail
                                                                                                {
                                                                                                    VillageCode = village.Village_Code,
                                                                                                    VillageName = village.Village_Name
                                                                                                }).ToList()
                                                                                }).ToList()
                                                                 }).ToList()
                                                }).ToList()
                                  }).FirstOrDefaultAsync().ConfigureAwait(false);


            return await countryDetails;
        }

        public async Task<Country> AddCountry(string countryName)
        {
            
                var res = await this._context.Countries.AsNoTracking().FirstOrDefaultAsync(x => x.Country_Name == countryName.ToUpper());
                if (res == null)
                {
                    Country obj = new Country();
                    obj.Country_Name = countryName;

                    _context.Countries.Add(obj);
                    var result = await _context.SaveChangesAsync();

                    obj =  await (from c in _context.Countries
                                                where c.Country_Name == countryName
                                                select c).FirstOrDefaultAsync().ConfigureAwait(false);
                    return obj;
                }
                else
                {
                    throw new CustomException("Country already Exits");
                }
           
        }
    

    }
}