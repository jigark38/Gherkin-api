using GherkinWebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.Login
{
    public interface ILoginRepository
    {
        bool IsUserAuthenticated(Customer user);
        Task<Customer> GetUserByUserName(string userName);
        string GetRoleByUserName(string userName);
    }
}
