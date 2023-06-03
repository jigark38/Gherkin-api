using GherkinWebAPI.Core.Accounts_Master;
using GherkinWebAPI.Models.Accounts_Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GherkinWebAPI.Controllers.AccountsMaster
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Route("api/v1/[Controller]")]
    public class AccountsGroupController : ApiController
    {
        private readonly IAccountsGroupService _groupService;
        public readonly string controller = nameof(FarmersAgreementController);

        public AccountsGroupController(IAccountsGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet]
        [Route("GetAllAccountsGroup")]
        public IHttpActionResult GetAllAccountsGroup()
        {
            try
            {
                var allGroups = _groupService.GetAllGroupsAsync();
                if (allGroups.Count() > 0)
                {
                    return Ok(allGroups);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in AccountsGroupController/{nameof(AccountsGroupController.GetAllAccountsGroup)}");
                return InternalServerError();
            }

        }

       
        [HttpGet, Route("GetAllGroupsHead")]
        public IHttpActionResult GetAllGroupsHead(string AccountHead)
        {
            try
            {
                var allGroups = _groupService.GetAllGroupsForHeadAsync(AccountHead);
                if (allGroups.Count() > 0)
                {
                    return Ok(allGroups);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in AccountsGroupController/{nameof(AccountsGroupController.GetAllAccountsGroup)}");
                return InternalServerError();
            }

        }

        [HttpGet]
        [Route("FindAccountGroup")]
        public async Task<IHttpActionResult> FindGroup(string headCode, int orgCode, string accountOrGroupCode, string mainGroupCode, string parentGroupCode, bool isAccount)
        {
            try
            {
                var accountGroupDetail = await _groupService.FindAccountsGroup(headCode, orgCode, accountOrGroupCode, mainGroupCode, parentGroupCode, isAccount);
                if (accountGroupDetail != null)
                {
                    return Ok(accountGroupDetail);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in AccountsGroupController/{nameof(AccountsGroupController.FindGroup)}");
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("AddAccountsGroup")]
        public async Task<IHttpActionResult> AddAccountsGroup([FromBody] AccountMaster accountMaster)
        {
            try
            {
                var result = await _groupService.AddAccountsGroup(accountMaster);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(AccountsGroupController.AddAccountsGroup)}");
                return InternalServerError();
            }
        }

    }
}
