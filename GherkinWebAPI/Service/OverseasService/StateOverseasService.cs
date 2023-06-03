using GherkinWebAPI.Core;
using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service
{
    public class StateOverseasService : IStateOverseasService
    {
        public IStateOverseasRepository _repository { get; }

        public StateOverseasService(IStateOverseasRepository repository)
        {
            this._repository = repository;
        }

        public Task<List<StateOverseas>> GetAllStatesOverseasByCountryIdAsync(string countryCode)
        {
            return _repository.GetAllStatesOverseasByCountryIdAsync(countryCode);
        }

        public Task<StateOverseas> GetStateOverseasByIdAsync(string stateCode)
        {
            return _repository.GetStateOverseasByIdAsync(stateCode);
        }

        public Task<StateOverseas> AddStateIfntAsync(StateOverseas state)
        {
            return _repository.AddStateIfntAsync(state);
        }
    }
}