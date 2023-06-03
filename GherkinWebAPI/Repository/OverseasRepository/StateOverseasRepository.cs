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
    public class StateOverseasRepository : RepositoryBase<State>, IStateOverseasRepository
    {
        private RepositoryContext _context;

        public StateOverseasRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            this._context = repositoryContext;
        }

        public async Task<List<StateOverseas>> GetAllStatesOverseasByCountryIdAsync(string countryCode)
        {
            var states = await Task.Run(() => ((from state in _context.StateOverseas
                                                join country in _context.countriesoverseas on state.W_Country_Id equals country.W_Country_Id
                                                where state.W_Country_Id == countryCode
                                                select new { state.W_State_id, state.W_State_Name, country.W_Country_Id })
                                                   .AsEnumerable()
                                                   .Select(c => new StateOverseas
                                                   {
                                                       W_State_id = c.W_State_id,
                                                       W_State_Name = c.W_State_Name,
                                                       W_Country_Id = c.W_Country_Id
                                                   }).ToList()));
            return states;
        }

        public async Task<StateOverseas> GetStateOverseasByIdAsync(string stateCode)
        {
            var stateDetail = await (from state in _context.StateOverseas
                                     where state.W_State_id == stateCode
                                     select state
                                                       ).SingleOrDefaultAsync();
            return stateDetail;
        }

        public async Task<StateOverseas> AddStateIfntAsync(StateOverseas state)
        {
            StateOverseas obj = null;
            var res = await this._context.StateOverseas.FirstOrDefaultAsync(x => x.W_State_Name == state.W_State_Name);
            if (res == null)
            {

                int? selectMaxDeptId = await _context.StateOverseas.MaxAsync(e => (int?)e.ID);
                if (selectMaxDeptId != null)
                    state.W_State_id = "S_" + Convert.ToString(selectMaxDeptId + 1);
                else
                    state.W_State_id = "S_" + "1";

                StateOverseas sta = new StateOverseas { W_State_id = state.W_State_id, W_State_Name = state.W_State_Name, W_Country_Id = state.W_Country_Id };
                _context.StateOverseas.Add(sta);
                var result = await _context.SaveChangesAsync();

                obj = await (from c in _context.StateOverseas
                             where c.W_State_Name == state.W_State_Name
                             select c
                                                           ).FirstOrDefaultAsync();
                return obj;
            }
            else
            {
                throw new CustomException("State Is already Exits");
            }
        }
    }
}