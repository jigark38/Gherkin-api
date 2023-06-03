using GherkinWebAPI.Core;
using GherkinWebAPI.Core.Login;
using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service.Login
{
    public class LoginService : ILoginService
    {
        public IRepositoryWrapper _repository { get; }

        public LoginService()
        {

        }
        public LoginService(IRepositoryWrapper repository)
        {
            this._repository = repository;
        }

        public async Task<Customer> GetUserByUserName(string userName)
        {
            return await _repository.LoginRepository.GetUserByUserName(userName);
        }

        public bool IsUserAuthenticated(Customer user)
        {
            return _repository.LoginRepository.IsUserAuthenticated(user);
        }

        public string GetRoleByUserName(string userName)
        {
            return _repository.LoginRepository.GetRoleByUserName(userName);
        }
    }
}