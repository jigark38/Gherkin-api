using GherkinWebAPI.Core.Accounts_Master;
using GherkinWebAPI.Models.Accounts_Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GherkinWebAPI.Controllers.AccountsMaster
{
    [Route("api/v1/[Controller]")]
    public class AccountsMasterController : ApiController
    {
        private readonly IAccountMasterService _service;
        public readonly string controller = nameof(AccountsMasterController);

        public AccountsMasterController(IAccountMasterService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("AddAccount")]
        public async Task<IHttpActionResult> AddAccount([FromBody] AccountMaster accountMaster)
        {
            try
            {
                var result = await _service.AddAccount(accountMaster);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(AccountsMasterController.AddAccount)}");
                return InternalServerError();
            }
        }
    }
}
