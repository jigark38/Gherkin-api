using GherkinWebAPI.Core;
using GherkinWebAPI.Core.Reports.MonthlyAttendance;
using GherkinWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service.MonthlyAttendance
{
    public class MonthlyAttendanceService : IMonthlyAttendanceService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public MonthlyAttendanceService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public async Task<ApiResponse<object>> GetMonthlyAttendanceDetails(int orgOfficeNo, string status, string division, string deptCode, string subDeptCode, string month, string year,string filter)
        {
            return await _repositoryWrapper.MonthlyAttendanceRepository.GetMonthlyAttendanceDetails(orgOfficeNo, status, division, deptCode, subDeptCode, month, year,filter);
        }

    }
}