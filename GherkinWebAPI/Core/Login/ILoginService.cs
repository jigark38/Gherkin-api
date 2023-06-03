using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.Login
{
    public interface ILoginService
    {
        bool IsUserAuthenticated(Customer user);
        Task<Customer> GetUserByUserName(string userName);
        string GetRoleByUserName(string userName);
    }
}
