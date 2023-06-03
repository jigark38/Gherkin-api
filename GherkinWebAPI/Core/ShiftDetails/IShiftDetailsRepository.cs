using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GherkinWebAPI.DTO;
using GherkinWebAPI.DTO.ShiftDetails;
using GherkinWebAPI.Models.ShiftDetail;

namespace GherkinWebAPI.Core.ShiftDetails
{
   public interface IShiftDetailsRepository
    {
        Task<List<ShiftDetailsDto>> GetAllShiftDetailsAsync();
        Task<ShiftDetailMaster> AddShiftDetails(ShiftDetailMaster shiftDetails);
        Task<ShiftStatusDetail> AddShiftStatus(ShiftStatusDetail shiftDetails);
        Task<ShiftStatusDetail> GetShiftStatus(long shiftNo);
        Task<ShiftStatusDetail> UpdateShiftStatus(ShiftStatusDetail shiftDetails);        
    }
}
