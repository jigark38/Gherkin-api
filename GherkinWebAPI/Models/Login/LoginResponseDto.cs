using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.Login
{
    public class LoginResponseDto
    {
        public string Status { set; get; }
        public string Message { set; get; }
        public int ExpiryMinutes { set; get; }
        public bool IsAdmin { set; get; }
        public string UserId { get; set; }
        public string EmployeeId { get; set; }
        public List<LoginPermission> Permissions { set; get; }
        public int OfficeLocationCode { get; set; }
        public int OrgCode { get; set; }
    }

    public class LoginPermission 
    {
        public string id { set; get; }
        public string moduleName { set; get; }
        public string moduleShortCut { set; get; }
    }
}