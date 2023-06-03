using GherkinWebAPI.Core.Reports.MonthlyAttendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GherkinWebAPI.Controllers.Reports.MonthlyAttendanceReport
{
    [RoutePrefix("MonthlyAttendance")]
    public class MonthlyAttendanceReport2Controller : ApiController
    {
        private readonly IMonthlyAttendanceRepository _repository;
        public MonthlyAttendanceReport2Controller(IMonthlyAttendanceRepository repository)
        {
            _repository = repository;
        }
        [Route("GetMonthlyAttendanceDetails")]
        [HttpGet]
        public async Task<IHttpActionResult> GetMonthlyAttendanceDetails(int orgOfficeNo, string status, string division, string deptCode, string subDeptCode,
            string month, string year, string filter)
        {
            return Ok(await _repository.GetMonthlyAttendanceDetails(orgOfficeNo, status, division, deptCode, subDeptCode, month, year, filter));
        }
    }
}