using GherkinWebAPI.Core.ShiftDetails;
using GherkinWebAPI.CustomExceptions;
using GherkinWebAPI.DTO.ShiftDetails;
using GherkinWebAPI.Models.ShiftDetail;
using GherkinWebAPI.Persistence;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace GherkinWebAPI.Repository.ShiftDetails
{
    public class ShiftDetailsRepository : RepositoryBase<ShiftDetailsDto>, IShiftDetailsRepository
    {
        private RepositoryContext _context;

        public ShiftDetailsRepository(RepositoryContext repositoryContext)
               : base(repositoryContext)
        {
            this._context = repositoryContext;
        }

        public async Task<List<ShiftDetailsDto>> GetAllShiftDetailsAsync()
        {
            var shiftData = await (from shiftDetail in _context.ShiftDetailsMaster
                                   join shiftStatus in _context.ShiftStatusDetails on shiftDetail.ShiftNo equals shiftStatus.ShiftNo
                                  into shift
                                   from shfitStatuss in shift.DefaultIfEmpty()
                                   select new ShiftDetailsDto
                                   {
                                       ShiftNo = shiftDetail.ShiftNo,
                                       EntryDate = shiftDetail.EntryDate,
                                       EnteredEmpID = shiftDetail.EnteredEmpID,
                                       ShiftEffectiveDate = shiftDetail.ShiftEffectiveDate,
                                       ShiftName = shiftDetail.ShiftName,
                                       ShiftTimeFrom = shiftDetail.ShiftTimeFrom,
                                       ShiftTimeTo = shiftDetail.ShiftTimeTo,
                                       ShiftDuration = shiftDetail.ShiftDuration,
                                       ShiftBreakTimeFrom = shiftDetail.ShiftBreakTimeFrom,
                                       ShiftBreakTimeTo = shiftDetail.ShiftBreakTimeTo,
                                       ShiftBreakDuration = shiftDetail.ShiftBreakDuration,
                                       ShiftRotation = shiftDetail.ShiftRotation,
                                       ShiftRotationDays = shiftDetail.ShiftRotationDays,
                                       ShiftStatus = shfitStatuss.ShiftStatus,
                                       ShiftCancellationEffectiveFromDate = shfitStatuss.ShiftCancellationEffectiveFromDate
                                   }).OrderByDescending(x => x.ShiftEffectiveDate).ToListAsync();
            return shiftData;
        }

        public async Task<ShiftDetailMaster> AddShiftDetails(ShiftDetailMaster shiftDetails)
        {
            var result = _context.ShiftDetailsMaster.Add(shiftDetails);
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<ShiftStatusDetail> AddShiftStatus(ShiftStatusDetail shiftDetails)
        {
            var result = _context.ShiftStatusDetails.Add(shiftDetails);
            await _context.SaveChangesAsync();
            return result;
        }
        public async Task<ShiftStatusDetail> GetShiftStatus(long shiftNo)
        {
            var result = await (from shift in _context.ShiftStatusDetails
                                where shift.ShiftNo == shiftNo
                                select shift).SingleOrDefaultAsync();
            return result;
        }
        public async Task<ShiftStatusDetail> UpdateShiftStatus(ShiftStatusDetail shiftDetails)
        {
            var result = await _context.ShiftStatusDetails.FirstOrDefaultAsync(x => x.ShiftNo == shiftDetails.ShiftNo);
            if (result != null)
            {
                result.ShiftStatus = shiftDetails.ShiftStatus;
                result.ShiftCancellationEffectiveFromDate = shiftDetails.ShiftCancellationEffectiveFromDate;
                await _context.SaveChangesAsync();
                return result;
            }
            else
            {
                throw new CustomException("Management doesn't exist");
            }
        }

    }
}
