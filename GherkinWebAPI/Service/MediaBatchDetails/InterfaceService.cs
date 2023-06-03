using GherkinWebAPI.Core;
using GherkinWebAPI.Core.MediaBatchDetails;
using GherkinWebAPI.Models.MediaBatchDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service.MediaBatchDetails
{
    public class InterfaceService:IInterfaceService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public InterfaceService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public async Task<List<OrgOfficeNameList>> GetOrgOfficeNameLists()
        {
            return await _repositoryWrapper.InterfaceRepository.GetOrgOfficeNameLists();
        }
        public async Task<List<MediaProcessNameList>> GetMediaProcessNameList()
        {
            return await _repositoryWrapper.InterfaceRepository.GetMediaProcessNameList();
        }
        public async Task<List<PendingOrderScheduleGrid>> GetPendingOrderScheduleGrid(int orgOfficeNo)
        {
            string mediaProcessCode = "";
            return await _repositoryWrapper.InterfaceRepository.GetPendingOrderScheduleGrid(orgOfficeNo,mediaProcessCode);
        }
        public async Task<List<PendingOrderScheduleGrid>> GetFilteredPendingOrderScheduleGrid(int orgOfficeNo, string mediaProcessCode)
        {
            return await _repositoryWrapper.InterfaceRepository.GetPendingOrderScheduleGrid(orgOfficeNo, mediaProcessCode);
        }
        public async  Task<PendingOrder> SelectPendingOrderSchedule(SelectedPendingOrder Pendobj)
        {
            return await _repositoryWrapper.InterfaceRepository.SelectPendingOrderSchedule(Pendobj);
        }

    }
}