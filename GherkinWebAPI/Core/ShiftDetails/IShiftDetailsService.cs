using GherkinWebAPI.DTO.ShiftDetails;
using GherkinWebAPI.Models.ShiftDetail;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.ShiftDetails
{
    public interface IShiftDetailsService
    {
        Task<List<ShiftDetailsDto>> GetShiftDetails();
        Task<ShiftDetailMaster> AddShiftDetails(ShiftDetailMaster shiftDetails);
        Task<ShiftStatusDetail> AddShiftStatus(ShiftStatusDetail shiftDetails);
        Task<ShiftStatusDetail> GetShiftStatus(long shiftNo);
        Task<ShiftStatusDetail> UpdateShiftStatus(ShiftStatusDetail shiftDetails);
    }
}
