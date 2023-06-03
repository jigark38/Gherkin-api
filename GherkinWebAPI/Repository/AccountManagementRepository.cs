using GherkinWebAPI.Core;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.User;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using GherkinWebAPI.Utilities;

namespace GherkinWebAPI.Repository
{
    public class AccountManagementRepository : RepositoryBase<Permission>, IAccountManagementRepository
    {

        private RepositoryContext _context;
        public AccountManagementRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            _context = repositoryContext;
        }

        public async Task<string> CreateUser(UserPermission userPermission)
        {
            var users = await _context.Customers.AsNoTracking().Select(x => x.Employee_ID).ToListAsync();
            var usernames = await _context.Customers.AsNoTracking().Select(x => x.User_Name).ToListAsync();
            if (users.Contains(userPermission.Employee_ID))
            {
                throw new Exception("Employee ID already Exists");
            }
            else if (usernames.Contains(userPermission.User_Name))
            {
                throw new Exception("Username already Exists");
            }
            string serialId = await GetCustomerId();
            Customer customer = new Customer();
            customer.UP_Serial_No = serialId;
            customer.UP_Date = DateTime.Now.Date.ToString("dd-MM-yyyy");
            customer.UP_Employee_ID = userPermission.UP_Employee_ID;
            customer.Employee_ID = userPermission.Employee_ID;
            customer.Org_Code = userPermission.Org_Code;
            customer.Org_Office_No = userPermission.Org_Office_No;
            customer.User_Name = userPermission.User_Name;
            var hash = new CredentialHash(userPermission.User_Password);
            customer.User_Password = hash.ToArray();
            _context.Customers.Add(customer);
            var result = await _context.SaveChangesAsync();
            if (result == 1)
            {
                Permission permission = new Permission();
                permission.Id = serialId;
                permission.Permissions = userPermission.Permissions;
                _context.Permissions.Add(permission);
                await _context.SaveChangesAsync();
            }
            return serialId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<Account>> GetAllAccountByCustomerIdAsync(int id)
        {
            return  Account.GetAllAccounts().Where(e => e.customerId == id).OrderBy(b => b.accountId).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Customer>> GetAllCustomerAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<List<UsersDetails>> GetAllUsers()
        {
            List<UsersDetails> usersDetails = new List<UsersDetails>();
            var users = await _context.Customers.AsNoTracking().Select(x => x).ToListAsync();
            foreach (var emp in users)
            {
                var employees = await _context.Employees.AsNoTracking().Where(x => x.employeeId == emp.Employee_ID).Select(x => x).FirstOrDefaultAsync();
                if (employees != null)
                {
                    Department department = await _context.Departments.AsNoTracking().Where(x => x.departmentCode == employees.departmentCode).FirstOrDefaultAsync();
                    SubDepartment subDepartment = await _context.SubDepartments.AsNoTracking().Where(x => x.departmentCode == employees.departmentCode).FirstOrDefaultAsync();
                    Designation desg = await _context.Designations.AsNoTracking().Where(x => x.designationCode == employees.designationCode).FirstOrDefaultAsync();
                    usersDetails.Add(new UsersDetails
                    {
                        EmployeeID = emp.Employee_ID,
                        User_Name = emp.User_Name,
                        UP_Serial_No = emp.UP_Serial_No,
                        UP_Date = emp.UP_Date,
                        Org_Code = emp.Org_Code,
                        Org_Office_No = emp.Org_Office_No,
                        Department_Name = department.departMentName,
                        DepartmentId = department.departmentCode,
                        Sub_Department_Name = subDepartment.subDepartmentName,
                        DesignationId = desg.designationCode,
                        SubDepartmentId = subDepartment.subDepartmentCode,
                        Employee_Name = employees.employeeName
                    });
                }

            }
            return usersDetails;
        }

        public async Task<string> GetCustomerId()
        {
            string nextId = string.Empty;
            var customer = await _context.Customers.AsNoTracking().OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            if (customer != null)
            {
                nextId = string.Concat("UPSNo_", (Convert.ToInt16((customer.UP_Serial_No).Split('_')[1]) + 1).ToString());
            }
            else
            {
                nextId = "UPSNo_1";
            }
            return nextId;
        }

        public async Task<List<Menu>> GetMainMenus()
        {
            var dataList = await _context.MasterMenu.AsNoTracking().ToListAsync();
            return dataList.Where(x => x.ModuleId == "0" & x.SubMenuId == "0").Select(x => new Menu { Id = x.Id, ModuleName = x.ModuleName, ModuleShortCut = x.ModuleShortCut }).ToList();
        }

        public async Task<List<Menu>> GetModuleMenuByMenuId(string menuId)
        {

            var dataList = await _context.MasterMenu.AsNoTracking().ToListAsync();
            return dataList.Where(x => x.MenuId == menuId.Split('_')[0] & x.ModuleId != "0" & x.SubMenuId == "0").Select(x => new Menu { Id = x.Id, ModuleName = x.ModuleName, ModuleShortCut = x.ModuleShortCut }).ToList();
            //if (moduleMenu.Count() > 0)
            //{
            //    return moduleMenu;
            //}
            //else
            //{
            //    return _context.MasterMenu.AsNoTracking().Where(x => x.Id == menuId).Select(x => new Menu { Id = x.Id, ModuleName = x.ModuleName, ModuleShortCut = x.ModuleShortCut }).ToList();
            //}
        }

        public async Task<List<Menu>> GetSubMenuByModuleMenuId(string moduleMenuId)
        {
            var dataList = await _context.MasterMenu.AsNoTracking().ToListAsync();
            return dataList.Where(x => x.MenuId == moduleMenuId.Split('_')[0] && x.ModuleId == moduleMenuId.Split('_')[1] & x.SubMenuId != "0").Select(x => new Menu { Id = x.Id, ModuleName = x.ModuleName, ModuleShortCut = x.ModuleShortCut }).ToList();
            //if(subMenu.Count() > 0)
            //{
            //    return subMenu;
            //}
            //else
            //{
            //    return  _context.MasterMenu.AsNoTracking().Where(x => x.Id == moduleMenuId).Select(x => new Menu { Id = x.Id, ModuleName = x.ModuleName, ModuleShortCut = x.ModuleShortCut }).ToList();
            //}
        }

        public async Task<UserDetails> GetUserName(string empId)
        {
            return await _context.Customers.Where(x => x.Employee_ID == empId).Select(x => new UserDetails { UP_Serial_No = x.UP_Serial_No, User_Name = x.User_Name }).FirstOrDefaultAsync();
        }

        public async Task<List<SubMenu>> GetUserPermissionByMenuAndModuleId(int userid, int organisation, int officeLocation, int menuId)
        {
            List<SubMenu> subMenus = new List<SubMenu>();
            var userPermissions = await _context.UserPermissions.AsNoTracking().Where(x => x.UserId == userid & x.Organisation == organisation & x.OfficeLocation == officeLocation & x.MenuId == menuId).ToListAsync();

            foreach (UserPermissions up in userPermissions)
            {
                var subMenu = await _context.SubMenu.Where(x => x.Id == up.SubmenuId).FirstOrDefaultAsync();
                subMenus.Add(subMenu);

            }

            return subMenus;
        }

        public async Task<List<UserMenu>> GetUserPermissionByUserId(string userid, int organisation, int officeLocation)
        {
            List<UserMenu> permissionDataResults = new List<UserMenu>();
            //var Id = await _context.Customers.AsNoTracking().Where(x => x.Employee_ID == userid.ToString() & x.Org_Code == organisation & x.Org_Office_No == officeLocation).Select(x => x.UP_Serial_No).FirstOrDefaultAsync();
            var permissions = await _context.Permissions.AsNoTracking().Where(x => x.Id == userid.ToString()).Select(x => x.Permissions).FirstOrDefaultAsync();
            if (!string.IsNullOrEmpty(permissions))
            {
                Dictionary<string, List<Menu>> dict = new Dictionary<string, List<Menu>>();
                string[] Ids = permissions.Split(',');
                foreach (string id in Ids)
                {
                    string menuId = id.Split('_')[0]; string moduleId = id.Split('_')[1];
                    var parentMenu = await _context.MasterMenu.AsNoTracking().Where(x => x.MenuId == menuId & x.ModuleId == moduleId & x.SubMenuId == "0").Select(x => x.Id).FirstOrDefaultAsync();
                    if (dict.ContainsKey(parentMenu))
                    {
                        var menu = await _context.MasterMenu.AsNoTracking().Where(x => x.Id == id).Select(x => new UserMenu { Id = x.Id, ModuleName = x.ModuleName, ModuleShortCut = x.ModuleShortCut }).FirstOrDefaultAsync();
                        dict[parentMenu].Add(new Menu { Id = menu.Id, ModuleName = menu.ModuleName, ModuleShortCut = menu.ModuleShortCut });
                    }
                    else
                    {
                        dict[parentMenu] = await _context.MasterMenu.AsNoTracking().Where(x => x.Id == id).Select(x => new Menu { Id = x.Id, ModuleName = x.ModuleName, ModuleShortCut = x.ModuleShortCut }).ToListAsync();
                    }

                }

                foreach (KeyValuePair<string, List<Menu>> data in dict)
                {
                    var mainMenuName = await _context.MasterMenu.AsNoTracking().Where(x => x.Id == data.Key).Select(x => new UserMenu { Id = x.Id, ModuleName = x.ModuleName, ModuleShortCut = x.ModuleShortCut }).FirstOrDefaultAsync();
                    permissionDataResults.Add(new UserMenu { Id = data.Key, ModuleName = mainMenuName.ModuleName, ModuleShortCut = mainMenuName.ModuleShortCut, Children = data.Value });
                }

            }
            return permissionDataResults;
        }

        public async Task<string> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            var user = await _context.Customers.Where(x => x.UP_Serial_No == resetPasswordDTO.UP_Serial_No).Select(x => x).FirstOrDefaultAsync();
            if (user != null)
            {
                var hash = new CredentialHash(resetPasswordDTO.User_Password);
                user.User_Password = hash.ToArray();
                _context.Customers.AddOrUpdate(user);
                await _context.SaveChangesAsync();
                return String.Format("Password reset successfully for userid {0}", resetPasswordDTO.User_Name);
            }
            else
            {
                return "UserId not found";
            }
        }

        public async Task<string> SavePermissions(PermissionDTO permission)
        {
            string permissionId = await _context.Customers.AsNoTracking().Where(x => x.UP_Serial_No == permission.UP_Serial_No & x.Org_Office_No == permission.Org_Office_No).Select(x => x.UP_Serial_No).FirstOrDefaultAsync();
            var existingPermissions = await _context.Permissions.AsNoTracking().Where(x => x.Id == permission.UP_Serial_No).Select(x => x).FirstOrDefaultAsync();

            if (existingPermissions.Permissions == null)
            {
                existingPermissions.Permissions = (permission.Selected).Aggregate((i, j) => i + "," + j);
                _context.Permissions.AddOrUpdate(existingPermissions);
                await _context.SaveChangesAsync();
            }
            else
            {
                if (permission.Unselected.Count() > 0)
                {
                    string unselected = string.Empty;
                    foreach (string p in existingPermissions.Permissions.Split(','))
                    {
                        if (!(permission.Unselected).Contains(p))
                        {
                            unselected = unselected + "," + p;
                        }
                    }
                    unselected = unselected.TrimStart(',');
                    if (permission.Selected.Count() > 0)
                    {
                        existingPermissions.Permissions = string.Concat(unselected, ",", (permission.Selected).Aggregate((i, j) => i + "," + j));
                        _context.Permissions.AddOrUpdate(existingPermissions);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        existingPermissions.Permissions = unselected;
                        _context.Permissions.AddOrUpdate(existingPermissions);
                        await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    existingPermissions.Permissions = string.Concat(existingPermissions.Permissions, ",", (permission.Selected).Aggregate((i, j) => i + "," + j));
                    _context.Permissions.AddOrUpdate(existingPermissions);
                    await _context.SaveChangesAsync();
                }
            }
            return permission.UP_Serial_No;
        }
    }
}
