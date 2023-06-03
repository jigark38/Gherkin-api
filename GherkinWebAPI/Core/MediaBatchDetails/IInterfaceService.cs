﻿using GherkinWebAPI.Models.MediaBatchDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.MediaBatchDetails
{
    public interface IInterfaceService
    {
        Task<List<OrgOfficeNameList>> GetOrgOfficeNameLists();

        Task<List<MediaProcessNameList>> GetMediaProcessNameList();

        Task<List<PendingOrderScheduleGrid>> GetPendingOrderScheduleGrid(int orgOfficeNo);
        Task<List<PendingOrderScheduleGrid>> GetFilteredPendingOrderScheduleGrid(int orgOfficeNo, string mediaProcessCode);
        Task<PendingOrder> SelectPendingOrderSchedule(SelectedPendingOrder Pendobj);
    }
}
