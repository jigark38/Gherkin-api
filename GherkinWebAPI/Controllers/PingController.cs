using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GherkinWebAPI.Controllers
{
    public class PingController : ApiController
    {
        [HttpGet]
        public async Task<IHttpActionResult> Ping()
        {
            return Ok(true);
        }
    }
}
