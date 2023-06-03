using GherkinWebAPI.Core;
using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service
{
    public class StateService : IStateService
    {
        public IStateRepository _repository { get; }

        public StateService(IStateRepository repository)
        {
            this._repository = repository;
        }

        public Task<List<State>> GetAllStatesByCountryAsync(int countryCode)
        {
            return _repository.GetAllStatesByCountryIdAsync(countryCode);
        }

        public Task<State> GetStateByIdAsync(int stateCode)
        {
            return _repository.GetStateByIdAsync(stateCode);
        }

        public Task<State> AddState(State state)
        {
            return _repository.AddState(state);
        }
    }
}