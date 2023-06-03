using GherkinWebAPI.Models.ManualAttendence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Core.ManualAttendence
{
    public interface IManualAttendenceDetailsRepository
    {
        Task<List<ManualAttendenceDto>> GetManualAttendenceDetailsList(string areadId, DateTime fromDate, DateTime toDate, int pageIndex, int pageSize);
        Task<bool> SaveManualAttendence(List<ManualAttendenceDto> manualAttendenceDtos);
    }
}