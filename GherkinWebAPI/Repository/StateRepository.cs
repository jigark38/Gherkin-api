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
    public class StateRepository : RepositoryBase<State>, IStateRepository
    {
        private RepositoryContext _context;

        public StateRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            this._context = repositoryContext;
        }
        public async Task<List<State>> GetAllStatesByCountryIdAsync(int countryCode)
        {
            var states = await Task.Run(() => ((from state in _context.States
                                                where state.Country_Code == countryCode
                                                select new { state.State_Code, state.State_Name, state.Country_Code })
                                                   .AsEnumerable()
                                                   .Select(c => new State
                                                   {
                                                       State_Code = c.State_Code,
                                                       State_Name = c.State_Name,
                                                       Country_Code = c.Country_Code
                                                   }).ToList()));
            return states;
        }

        public async Task<State> GetStateByIdAsync(int stateCode)
        {
            var stateDetail = await Task.Run(() => (from state in _context.States
                                                    where state.State_Code == stateCode
                                                    select new { state.State_Code, state.State_Name, state.Country_Code })
                                                    .AsEnumerable()
                                                    .Select(s => new State
                                                    {
                                                        State_Code = s.State_Code,
                                                        State_Name = s.State_Name,
                                                        Country_Code = s.Country_Code
                                                    }).FirstOrDefault());
            return stateDetail;
        }

        public async Task<State> AddState(State state)
        {
            try
            {
                State obj = new State();

                var stateDetail = await (from c in _context.States
                                         where c.State_Name == state.State_Name
                                         select c
                                                           ).SingleOrDefaultAsync();
                if (stateDetail == null)
                {
                    obj.State_Name = state.State_Name.ToUpper();
                    obj.Country_Code = state.Country_Code;

                    _context.States.Add(obj);
                    var result = await _context.SaveChangesAsync();

                    obj = await (from c in _context.States
                                 where c.State_Name == state.State_Name
                                 select c
                                                               ).FirstOrDefaultAsync();
                    return obj;
                }
                else
                {
                    throw new CustomException("State already Exits");
                }

            }
            catch (Exception ex)
            {
                throw new CustomException("State already Exits");
            }
        }
    }
}