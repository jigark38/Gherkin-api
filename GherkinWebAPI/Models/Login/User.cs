using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.Login
{
    public class User
    {
        public int UserID { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Roles { get; set; }

        public bool Active { get; set; }
    }

    public class LoginDetail
    {
        public int UserID { get; set; }

        public int Status { get; set; }

        public DateTime LoginTime { get; set; }

        public string IP { get; set; }

        public int WrongLoginAttempt { get; set; }

        public DateTime LogoutTime { get; set; }
    }
}