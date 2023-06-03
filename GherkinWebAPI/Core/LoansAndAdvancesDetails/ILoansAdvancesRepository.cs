using GherkinWebAPI.Models.LoansAndAdvancesDetails;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.LoansAndAdvancesDetails
{
    public interface ILoansAdvancesRepository
    {
        Task<LoansAdvancesDTO> CreateLoansAdvances(LoansAdvancesDTO loansAdvancesDTO);
        Task<string> GetLoansAdvanceNo();
        Task<List<LoansAdvancesDTO>> SearchLoansAdvances(int orgOfficeNo, string employeeId);
        Task<LoansAdvancesDTO> UpdateLoansAdvances(LoansAdvancesDTO loansAdvancesDTO);
    }
}
