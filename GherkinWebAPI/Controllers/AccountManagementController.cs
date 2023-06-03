
using GherkinWebAPI.Core;
using GherkinWebAPI.Models;
using GherkinWebAPI.Persistence;
using Microsoft.Extensions.Logging;
//using NLog.Internal;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;

namespace ESSWebAPI.Controllers
{
    [RoutePrefix("Account")]
    public class AccountManagementController : ApiController
    {
        private readonly IAccountManagementService _service;
        // private readonly ILogger<AccountManagementController> _logger;
        private readonly IRepositoryWrapper _repository;
        private IOrganisationService orgService;
        private readonly RepositoryContext _repositoryContext;

        public readonly string controller = nameof(AccountManagementController);

        public AccountManagementController(IOrganisationService orgService, IAccountManagementService service, IRepositoryWrapper repository, RepositoryContext repositoryContext)
        {
            this._service = service;
            //   _logger = logger;
            this._repository = repository;
            this._repositoryContext = repositoryContext;
            this.orgService = orgService;
        }

        [Route("Customer"), HttpGet]
        public async Task<List<Customer>> GetAllCustomerAsync()
        {
            List<Customer> lstCustomer = new List<Customer>();
            try
            {
                lstCustomer = await _service.GetAllCustomerAsync();

                return lstCustomer;
            }
            catch (Exception ex)
            {
                var error = string.Format("Exception in {0}/{1}", controller, nameof(AccountManagementController.GetAllCustomerAsync));
                //  _logger.LogError(ex, error);
                // return error;
            }

            return lstCustomer;
        }


        [Route("GetOrganisations"), HttpGet]
        public async Task<List<Organisation>> GetOrganisations()
        {
            List<Organisation> lstOrganisation = new List<Organisation>();
            Dictionary<string, string> dicCustomer = new Dictionary<string, string>();
            try
            {
                lstOrganisation = await orgService.GetOrganisations();
            }
            catch (Exception ex)
            {
                var error = string.Format("Exception in {0}/{1}", controller, nameof(AccountManagementController.GetAllCustomerAsync));
            }
            return lstOrganisation;
        }

        [Route("GetLocationsbyOrgid"), HttpGet]
        public async Task<List<OfficeLocation>> GetLocationsbyOrgid(int orgCode)
        {
            List<OfficeLocation> lstLocations = new List<OfficeLocation>();
            try
            {
                lstLocations = await orgService.GetLocationsbyOrgid(orgCode);
            }
            catch (Exception ex)
            {
                var error = string.Format("Exception in {0}/{1}", controller, nameof(AccountManagementController.GetAllCustomerAsync));
            }
            return lstLocations;
        }

        [Route("Account/{custid}"), HttpGet]
        public async Task<List<Account>> GetAllAccountByCustomerIdAsync(int custid)
        {
            List<Account> lstAccount = new List<Account>();
            try
            {
                lstAccount = await _service.GetAllAccountByCustomerIdAsync(custid);

                return lstAccount;
            }
            catch (Exception ex)
            {
                var error = string.Format("Exception in {0}/{1}", controller, nameof(AccountManagementController.GetAllAccountByCustomerIdAsync));

            }

            return lstAccount;
        }


        [Route("CreateUser"), HttpPost]
        public async Task<IHttpActionResult> CreateCustomer([FromBody]UserPermission customer)
        {
            try
            {
                if (customer == null)
                {
                    return BadRequest();
                }

                if (ModelState.IsValid)
                {
                    var Id = await _service.CreateUser(customer);
                    return Ok(Id);
                }
                else
                {
                    return BadRequest(ModelState.Values.First().Errors[0].ErrorMessage);
                }


            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        [Route("GetPermissions"), HttpGet]
        public async Task<IHttpActionResult> GetPermssionUserId(int userid, int organisation, int officeLocation)
        {
            var userPermissions = _repositoryContext.UserPermissions.AsNoTracking().Where(x => x.UserId == userid & x.Organisation == organisation & x.OfficeLocation == officeLocation).ToList<UserPermissions>();
            List<PermissionsDTO> permissiondata = new List<PermissionsDTO>();
            foreach (UserPermissions up in userPermissions)
            {
                var subMenuItems = _repositoryContext.SubMenu.ToList();
                if (subMenuItems.FirstOrDefault(z => z.Id == up.SubmenuId) == null)
                {
                    permissiondata.Add(new PermissionsDTO { MenuId = up.MenuId, ModuleId = 0, SubmenuId = up.SubmenuId });
                }
                else
                {
                    permissiondata.Add(new PermissionsDTO { MenuId = up.MenuId, ModuleId = subMenuItems.FirstOrDefault(z => z.Id == up.SubmenuId).ParentId, SubmenuId = up.SubmenuId });
                }
            }

            Dictionary<int, PermissionDataResult> parentdict = new Dictionary<int, PermissionDataResult>();
            Dictionary<int, PermissionDataResult> moduledict = new Dictionary<int, PermissionDataResult>();

            foreach (PermissionsDTO p in permissiondata)
            {
                if (moduledict.ContainsKey(p.ModuleId))
                {
                    if (p.ModuleId == 0)
                    {
                        moduledict[p.ModuleId].Children.Add(new PermissionDataResult { Id = p.SubmenuId, Name = string.Empty, Children = new List<PermissionDataResult>() });
                    }
                    var subMenuList = await _service.GetSubMenuByModuleMenuId(p.ModuleId.ToString());
                    string subMenuName = subMenuList.Where(x => x.Id == p.SubmenuId.ToString()).Select(x => x.ModuleName).FirstOrDefault();
                    moduledict[p.ModuleId].Children.Add(new PermissionDataResult { Id = p.SubmenuId, Name = subMenuName, Children = new List<PermissionDataResult>() });
                }
                else
                {
                    if (p.ModuleId == 0)
                    {
                        moduledict[p.ModuleId] = new PermissionDataResult { Id = p.ModuleId, Name = string.Empty, Children = new List<PermissionDataResult>() { new PermissionDataResult { Id = p.SubmenuId, Name = string.Empty, Children = new List<PermissionDataResult>() } } };
                    }
                    var moduleMenuList = await _service.GetModuleMenuByMenuId(p.MenuId.ToString());
                    string moduleMenuName = moduleMenuList.Where(x => x.Id == p.ModuleId.ToString()).Select(x => x.ModuleName).FirstOrDefault();
                    var subMenuList = await _service.GetSubMenuByModuleMenuId(p.ModuleId.ToString());
                    string subMenuName = subMenuList.Where(x => x.Id == p.SubmenuId.ToString()).Select(x => x.ModuleName).FirstOrDefault();
                    moduledict[p.ModuleId] = new PermissionDataResult { Id = p.ModuleId, Name = moduleMenuName, Children = new List<PermissionDataResult>() { new PermissionDataResult { Id = p.SubmenuId, Name = subMenuName, Children = new List<PermissionDataResult>() } } };
                }
            }

            foreach (PermissionsDTO p in permissiondata)
            {
                if (parentdict.ContainsKey(p.MenuId))
                {
                    parentdict[p.MenuId].Children.Add(moduledict[p.ModuleId]);
                }
                else
                {
                    parentdict[p.MenuId] = moduledict[p.ModuleId];
                }
            }


            List<PermissionDataResult> permissionData = new List<PermissionDataResult>();
            var mainMenuList = await _service.GetMainMenus();
            foreach (KeyValuePair<int, PermissionDataResult> data in parentdict)
            {
                string mainMenuName = mainMenuList.Where(x => x.Id == (data.Key).ToString()).Select(x => x.ModuleName).FirstOrDefault();
                permissionData.Add(new PermissionDataResult { Id = data.Key, Name = mainMenuName, Children = new List<PermissionDataResult> { data.Value } });
            }

            //Console.WriteLine(permissionData);



            return Ok(permissionData);
        }

        [HttpGet, Route("GetMainMenu")]
        public async Task<IHttpActionResult> GetMainMenu()
        {
            try
            {
                var mainMenus = await _service.GetMainMenus();
                if (mainMenus != null)
                {
                    return Ok(mainMenus);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in AccountManagementController/{nameof(AccountManagementController.GetMainMenu)}");
                return InternalServerError();
            }
        }


        [HttpGet, Route("GetModuleMenuByMenuId")]
        public async Task<IHttpActionResult> GetModuleMenuByMenuId(string menuId)
        {
            try
            {
                if (menuId != null)
                {
                    var moduleMenus = await _service.GetModuleMenuByMenuId(menuId);
                    if (moduleMenus != null)
                    {
                        return Ok(moduleMenus);
                    }
                    else
                        return NotFound();
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in AccountManagementController/{nameof(AccountManagementController.GetModuleMenuByMenuId)}");
                return InternalServerError(ex);
            }
        }

        [HttpGet, Route("GetSubMenuByModuleMenuId")]
        public async Task<IHttpActionResult> GetSubMenuByModuleMenuId(string moduleMenuId)
        {
            try
            {
                if (moduleMenuId != null)
                {
                    var subMenus = await _service.GetSubMenuByModuleMenuId(moduleMenuId);
                    if (subMenus != null)
                    {
                        return Ok(subMenus);
                    }
                    else
                        return NotFound();
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in AccountManagementController/{nameof(AccountManagementController.GetSubMenuByModuleMenuId)}");
                return InternalServerError(ex);
            }
        }


        [Route("GetAllCustomers"), HttpGet]
        public async Task<IHttpActionResult> GetAllCustomers()
        {
            List<Customer> lstCustomers = new List<Customer>();
            try
            {
                lstCustomers = await _service.GetAllCustomerAsync();


            }
            catch (Exception ex)
            {
                var error = string.Format("Exception in {0}/{1}", controller, nameof(AccountManagementController.GetAllCustomers));
                return InternalServerError(ex);

            }

            return Ok(lstCustomers);
        }


        [Route("SavePermissions"), HttpPost]
        public async Task<IHttpActionResult> SavePermissions([FromBody]PermissionDTO permissions)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.Values.First().Errors[0].ErrorMessage);
                }


            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(await _service.SavePermissions(permissions));
        }

        [Route("GetUserPermissionByMenuAndModuleId"), HttpGet]
        public async Task<IHttpActionResult> GetUserPermissionByMenuAndModuleId(int userid, int organisation, int officeLocation, int moduleId, int menuId)
        {
            List<SubMenuList> subMenuListData = new List<SubMenuList>();
            List<SubMenu> subMenuList = new List<SubMenu>();
            try
            {
                if (userid > 0 & organisation > 0 & officeLocation > 0 & moduleId > 0)
                {
                    var moduleMenu = await _service.GetModuleMenuByMenuId(menuId.ToString());
                    if (moduleMenu?.Any() ?? false)
                    {
                        var subMenu = _repositoryContext.SubMenu.AsNoTracking().Where(x => x.ParentId == moduleId).ToList();
                        subMenuList = await _service.GetUserPermissionByMenuAndModuleId(userid, organisation, officeLocation, menuId);
                        var subMenuData = subMenuList.Select(x => x.Id).ToList();
                        foreach (SubMenu sub in subMenu)
                        {
                            if (subMenuData.Contains(sub.Id))
                            {
                                subMenuListData.Add(new SubMenuList { Id = sub.Id, ModuleName = sub.ModuleName, IsSelected = true });
                            }
                            else
                            {
                                subMenuListData.Add(new SubMenuList { Id = sub.Id, ModuleName = sub.ModuleName, IsSelected = false });
                            }
                        }
                    }
                    else
                    {
                        var menuModuleName = _repositoryContext.MainMenu.AsNoTracking().Where(x => x.Id == moduleId).FirstOrDefault();
                        subMenuListData.Add(new SubMenuList { Id = menuId, ModuleName = menuModuleName.ModuleName, IsSelected = true });

                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(subMenuListData);
        }


        [Route("GetPermissionsV2"), HttpGet]
        public async Task<IHttpActionResult> GetPermssionByUserId(string userid, int organisation, int officeLocation)
        {
            List<UserMenu> permissions = new List<UserMenu>();
            try
            {
                if (!string.IsNullOrEmpty(userid) & organisation > 0 & officeLocation > 0)
                {
                    permissions = await _service.GetUserPermissionByUserId(userid, organisation, officeLocation);

                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(permissions);
        }

        [Route("GetUserPermissionByMenuAndModuleIdV2"), HttpGet]
        public async Task<IHttpActionResult> GetUserPermissionByMenuAndModuleIdV2(string userid, int organisation, int officeLocation, string moduleId, string menuId)
        {

            try
            {
                if (!string.IsNullOrEmpty(userid) & organisation > 0 & officeLocation > 0 & !string.IsNullOrEmpty(menuId) & !string.IsNullOrEmpty(moduleId))
                {

                    //var Id = _repositoryContext.Customers.AsNoTracking().Where(x => x.Employee_ID == userid).Select(x => x.UP_Serial_No).FirstOrDefault();

                    var permissions = _repositoryContext.Permissions.AsNoTracking().Where(x => x.Id == userid).Select(x => x.Permissions).FirstOrDefault();
                    if (moduleId != "0")
                    {
                        string MenuId = menuId.Split('_')[0];
                        string ModuleId = moduleId.Split('_')[1];
                        var menuList = _repositoryContext.MasterMenu.AsNoTracking().Where(x => x.MenuId == MenuId & x.ModuleId == ModuleId & x.SubMenuId != "0")
                            .Select(x => new SubMenuListV2 { Id = x.Id, ModuleName = x.ModuleName, IsSelected = false }).ToList();

                        foreach (SubMenuListV2 menu in menuList)
                        {
                            if (permissions != null && permissions.Split(',').Contains(menu.Id))
                            {
                                menu.IsSelected = true;
                            }
                        }
                        return Ok(menuList);
                    }
                    else
                    {
                        var menuList = _repositoryContext.MasterMenu.AsNoTracking().Where(x => x.Id == menuId)
                            .Select(x => new SubMenuListV2 { Id = x.Id, ModuleName = x.ModuleName, IsSelected = false }).ToList();

                        foreach (SubMenuListV2 menu in menuList)
                        {
                            if (permissions != null && permissions.Split(',').Contains(menu.Id))
                            {
                                menu.IsSelected = true;
                            }
                        }
                        return Ok(menuList);
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }


        [Route("GetUserNameFromEmployeeId"), HttpGet]
        public async Task<IHttpActionResult> GetUserNameFromEmployeeId(string empId)
        {
            try
            {
                if (!string.IsNullOrEmpty(empId))
                {
                    return Ok(await _service.GetUserName(empId));
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        [Route("ResetPassword"), HttpPost]
        public async Task<IHttpActionResult> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.Values.First().Errors[0].ErrorMessage);
                }
                string userid = await _service.ResetPassword(resetPasswordDTO);
                return Ok(userid);
            }
            catch (Exception ex)
            {
                var error = string.Format("Exception in {0}/{1}", controller, nameof(AccountManagementController.GetAllCustomers));
                return InternalServerError(ex);

            }
        }

        [Route("GetAllUsers"), HttpGet]
        public async Task<IHttpActionResult> GetAllUsers()
        {
            List<UsersDetails> users = new List<UsersDetails>();
            try
            {
                users = await _service.GetAllUsers();


            }
            catch (Exception ex)
            {
                var error = string.Format("Exception in {0}/{1}", controller, nameof(AccountManagementController.GetAllUsers));
                return InternalServerError(ex);

            }

            return Ok(users);
        }

        [Route("GetTestInfo"), HttpGet]
        public IHttpActionResult GetTestInfo()
        {
            string TestMessage = string.Empty;
            try
            {
                var list = System.Configuration.ConfigurationManager.ConnectionStrings;
                for (int i = 0; i < list.Count; i++)
                {
                    TestMessage += $" {list[i].Name} - {list[i].ConnectionString} ";
                }
            }
            catch (Exception ex)
            {
                var error = string.Format("Exception in {0}/{1}", controller, nameof(AccountManagementController.GetAllUsers));
                return InternalServerError(ex);
            }
            return Ok(TestMessage);
        }
    }
}