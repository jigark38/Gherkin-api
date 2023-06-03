using GherkinWebAPI.Models.LoansAndAdvancesDetails;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.LoansAndAdvancesDetails
{
    public interface ILoansAdvancesService
    {
        Task<LoansAdvancesDTO> CreateLoansAdvances(LoansAdvancesDTO loansAdvancesDTO);
        Task<string> GetLoansAdvanceNo();
        Task<List<LoansAdvancesDTO>> SearchLoansAdvances(int orgOfficeNo, string employeeId);
        Task<LoansAdvancesDTO> UpdateLoansAdvances(LoansAdvancesDTO loansAdvancesDTO);
    }
}
