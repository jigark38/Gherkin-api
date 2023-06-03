using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.Login
{
    public class LoginDto
    {
        public string UserName { get; set; }

        public string Password { get; set; }
        public bool isMobileApp { get; set; }

    }
}