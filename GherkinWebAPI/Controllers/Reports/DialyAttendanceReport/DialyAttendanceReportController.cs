using GherkinWebAPI.Core.Reports.DailyAttendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GherkinWebAPI.Controllers.Reports.DialyAttendanceReport
{
    [RoutePrefix("DialyAttendanceReport")]
    public class DialyAttendanceReportController : ApiController
    {
        private readonly IDialyAttendanceRepository _repository;
        public DialyAttendanceReportController(IDialyAttendanceRepository repository)
        {
            _repository = repository;
        }

        [Route("GetAttendanceDetails")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAttendanceDetails(string date, int orgOfficeNo, string status, string deptCode, string subDeptCode,
            string shift, string division, string filter, string category, string gender, int biometricId)
        {
            return Ok(await _repository.GetAttendanceDetails(date, orgOfficeNo, status, deptCode, subDeptCode, shift, division, filter, category, gender, biometricId));
        }
        [Route("GetAttendanceDetailsForView")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAttendanceDetailsForView(string date, int orgOfficeNo, string status, string deptCode, string subDeptCode,
            string shift, string division, string filter, string category, string gender, int biometricId)
        {
            return Ok(await _repository.GetAttendanceDetailsForView(date, orgOfficeNo, status, deptCode, subDeptCode, shift, division, filter, category, gender, biometricId));
        }
    }
}
