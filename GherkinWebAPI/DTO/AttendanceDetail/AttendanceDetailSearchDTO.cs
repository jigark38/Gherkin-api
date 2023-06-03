using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO.AttendanceDetail
{
    public class AttendanceDetailSearchDTO
    {
        public DateTime date { get; set; }
        public int orgOfficeNo { get; set; }
        public string category { get; set; }
        public string deptCode { get; set; }
        public string subDeptCode { get; set; }
        public string gender { get; set; }
        public string division { get; set; }
        public int biometricNo { get; set; }
        public string filter { get; set; }
    }
}