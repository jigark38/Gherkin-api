﻿using GherkinWebAPI.DTO.ScheduleDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.ScheduleDetail
{
    public interface IScheduleDetailRepository
    {
        Task<List<ScheduleDetailDTO>> GetPendingOrderScheduleDetails();
    }
}
