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
    public class CityOverseasRepository : RepositoryBase<CityOverseas>, ICityOverseasRepository
    {
        private RepositoryContext _context;

        public CityOverseasRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            this._context = repositoryContext;
        }

        public async Task<List<CityOverseas>> GetAllCityOverseasByStateIdAsync(string stateCode)
        {
            //Need to check in local first before we change
            var states = await Task.Run(() => ((from city in _context.CityOverseas
                                                join state in _context.StateOverseas on city.W_State_id equals state.W_State_id
                                                join country in _context.countriesoverseas on city.W_Country_Id equals country.W_Country_Id
                                                where city.W_State_id == stateCode
                                                select new { city.W_City_Id, city.W_City_Name, state.W_State_id, country.W_Country_Id })
                                                .AsEnumerable()
                                                .Select(c => new CityOverseas
                                                {
                                                    W_City_Id = c.W_City_Id,
                                                    W_City_Name = c.W_City_Name,
                                                    W_State_id = c.W_State_id,
                                                    W_Country_Id = c.W_Country_Id
                                                }).ToList()));

            return states;
        }

        public async Task<CityOverseas> GetcityOverseasByIdAsync(string cityCode)
        {
            var cityDetail = await (from city in _context.CityOverseas
                                    where city.W_City_Id == cityCode
                                    select city
                                                       ).SingleOrDefaultAsync();
            return cityDetail;
        }

        public async Task<CityOverseas> AddCityIfntAsync(CityOverseas cities)
        {
            CityOverseas obj = null;
            var res = await this._context.CityOverseas.FirstOrDefaultAsync(x => x.W_City_Name == cities.W_City_Name);
            if (res == null)
            {

                int? selectMaxDeptId = await _context.CityOverseas.MaxAsync(e => (int?)e.Id);
                if (selectMaxDeptId != null)
                    cities.W_City_Id = "C_" + Convert.ToString(selectMaxDeptId + 1);
                else
                    cities.W_City_Id = "C_" + "1";

                CityOverseas ct = new CityOverseas { W_City_Id = cities.W_City_Id, W_City_Name = cities.W_City_Name, W_State_id = cities.W_State_id, W_Country_Id = cities.W_Country_Id };
                _context.CityOverseas.Add(ct);
                var result = await _context.SaveChangesAsync();

                obj = await (from c in _context.CityOverseas
                             where c.W_City_Name == cities.W_City_Name
                             select c
                                                           ).FirstOrDefaultAsync();
                return obj;
            }
            else
            {
                throw new CustomException("City Is already Exits");
            }
        }
    }
}