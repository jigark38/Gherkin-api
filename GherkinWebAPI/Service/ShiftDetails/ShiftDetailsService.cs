using GherkinWebAPI.Core;
using GherkinWebAPI.Core.ShiftDetails;
using GherkinWebAPI.DTO.ShiftDetails;
using GherkinWebAPI.Models.ShiftDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service.ShiftDetails
{
    public class ShiftDetailsService : IShiftDetailsService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public IShiftDetailsRepository _shiftDetailsRepository { get; }
        
        public ShiftDetailsService(IShiftDetailsRepository shiftDetailsRepository)
        {
            this._shiftDetailsRepository = shiftDetailsRepository;
        }

        public async Task<List<ShiftDetailsDto>> GetShiftDetails()
        {
            return await _shiftDetailsRepository.GetAllShiftDetailsAsync();
        }
        public async Task<ShiftDetailMaster> AddShiftDetails(ShiftDetailMaster shiftDetails)
        {
            return await _shiftDetailsRepository.AddShiftDetails(shiftDetails);
        }
        public async Task<ShiftStatusDetail> AddShiftStatus(ShiftStatusDetail shiftStatus)
        {
            return await _shiftDetailsRepository.AddShiftStatus(shiftStatus);
        }
        public async Task<ShiftStatusDetail> GetShiftStatus(long shiftNo)
        {
            return await _shiftDetailsRepository.GetShiftStatus(shiftNo);
        }
        public async Task<ShiftStatusDetail> UpdateShiftStatus(ShiftStatusDetail shiftStatus)
        {
            return await _shiftDetailsRepository.UpdateShiftStatus(shiftStatus);
        }
        

    }
}