using GherkinWebAPI.Core;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GherkinWebAPI.Service
{
    public class AccountManagementService: IAccountManagementService
    {
        public IAccountManagementRepository _repository { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public AccountManagementService(IAccountManagementRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<List<Account>> GetAllAccountByCustomerIdAsync(int id)
        {
            return _repository.GetAllAccountByCustomerIdAsync(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<List<Customer>> GetAllCustomerAsync()
        {
            return _repository.GetAllCustomerAsync();
        }

        public async Task<List<Menu>> GetMainMenus()
        {
            return await _repository.GetMainMenus();
        }

        public async Task<List<Menu>> GetModuleMenuByMenuId(string menuId)
        {
            return await _repository.GetModuleMenuByMenuId(menuId);
        }

        public async Task<List<Menu>> GetSubMenuByModuleMenuId(string moduleMenuId)
        {
            return await _repository.GetSubMenuByModuleMenuId(moduleMenuId);
        }

        public async Task<List<SubMenu>> GetUserPermissionByMenuAndModuleId(int userid, int organisation, int officeLocation, int menuId)
        {
            return await _repository.GetUserPermissionByMenuAndModuleId(userid, organisation, officeLocation, menuId);
        }

        public async Task<string> GetCustomerId()
        {
            return await _repository.GetCustomerId();
        }

        public async Task<List<UserMenu>> GetUserPermissionByUserId(string userid, int organisation, int officeLocation)
        {
            return await _repository.GetUserPermissionByUserId(userid,organisation,officeLocation);
        }

        public async Task<string> CreateUser(UserPermission customer)
        {
           return await _repository.CreateUser(customer);
        }

        public async Task<UserDetails> GetUserName(string empId)
        {
            return await _repository.GetUserName(empId);
        }

        public async Task<string> SavePermissions(PermissionDTO permission)
        {
            return await _repository.SavePermissions(permission);
        }

        public async Task<string> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            return await _repository.ResetPassword(resetPasswordDTO);
        }

        public async Task<List<UsersDetails>> GetAllUsers()
        {
            return await _repository.GetAllUsers();
        }
    }
}
