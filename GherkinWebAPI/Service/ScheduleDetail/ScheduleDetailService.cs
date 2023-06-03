using GherkinWebAPI.Core;
using GherkinWebAPI.Core.ScheduleDetail;
using GherkinWebAPI.DTO.ScheduleDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service.ScheduleDetail
{
    public class ScheduleDetailService : IScheduleDetailService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public ScheduleDetailService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<ScheduleDetailDTO>> GetPendingOrderScheduleDetails()
        {
            return await _repositoryWrapper.ScheduleDetailRepository.GetPendingOrderScheduleDetails();
        }
       
    }
}