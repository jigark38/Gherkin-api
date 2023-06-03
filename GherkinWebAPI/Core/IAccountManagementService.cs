using GherkinWebAPI.Models;
using GherkinWebAPI.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core
{
    public interface IAccountManagementService
    {
        Task<List<Account>> GetAllAccountByCustomerIdAsync(int id);
        Task<List<Customer>> GetAllCustomerAsync();

        Task<List<Menu>> GetMainMenus();

        Task<List<Menu>> GetModuleMenuByMenuId(string menuId);

        Task<List<Menu>> GetSubMenuByModuleMenuId(string moduleMenuId);

        Task<List<SubMenu>> GetUserPermissionByMenuAndModuleId(int userid, int organisation, int officeLocation, int menuId);

        Task<string> GetCustomerId();


        Task<List<UserMenu>> GetUserPermissionByUserId(string userid, int organisation, int officeLocation);

        Task<string> CreateUser(UserPermission customer);

        Task<UserDetails> GetUserName(string empId);

        Task<string> SavePermissions(PermissionDTO permission);

        Task<string> ResetPassword(ResetPasswordDTO resetPasswordDTO);

        Task<List<UsersDetails>> GetAllUsers();
    }
}
