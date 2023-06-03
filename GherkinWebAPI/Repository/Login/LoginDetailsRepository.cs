using GherkinWebAPI.Core.Login;
using GherkinWebAPI.Models;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Repository.Login
{
    public class LoginDetailsRepository : RepositoryBase<Customer>, ILoginRepository
    {

        private RepositoryContext _context;
        public LoginDetailsRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            this._context = repositoryContext;
        }

        public string GetRoleByUserName(string userName)
        {
            return _context.Roles.FirstOrDefault(u => u.UserName == userName)?.Roles;
        }

        public async Task<Customer> GetUserByUserName(string userName)
        {
            var userDetail = await _context.Customers.Where(a => a.User_Name.Equals(userName)).FirstOrDefaultAsync();
            return userDetail;
        }

        public bool IsUserAuthenticated(Customer user)
        {
            var userDetail = _context.Customers.Where(a => a.User_Password.Equals(user.User_Password) && a.User_Name.Equals(user.User_Name)).FirstOrDefault();
            return userDetail == null ? false : true;
        }
    }
}