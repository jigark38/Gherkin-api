using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core
{
   public interface IStateOverseasService
    {
        Task<List<StateOverseas>> GetAllStatesOverseasByCountryIdAsync(string countryCode);
        Task<StateOverseas> GetStateOverseasByIdAsync(string stateCode);
        Task<StateOverseas> AddStateIfntAsync(StateOverseas state);
    }
}
