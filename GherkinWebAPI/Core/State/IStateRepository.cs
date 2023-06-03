using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core
{
    public interface IStateRepository
    {
        Task<List<State>> GetAllStatesByCountryIdAsync(int countryCode);
        Task<State> GetStateByIdAsync(int stateCode);
        Task<State> AddState(State state);
    }
}