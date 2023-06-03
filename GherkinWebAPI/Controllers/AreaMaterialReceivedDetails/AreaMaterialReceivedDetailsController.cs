using GherkinWebAPI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GherkinWebAPI.Controllers.AreaMaterialReceivedDetails
{
    [RoutePrefix("api/V1/AreaMaterialReceived")]
    public class AreaMaterialReceivedDetailsController : ApiController
    {
        private IAreaMaterialReceivedService _areaMaterialReceivedService;
        public AreaMaterialReceivedDetailsController(IAreaMaterialReceivedService areaMaterialReceivedService)
        {
            _areaMaterialReceivedService = areaMaterialReceivedService;
        }

        [Route("GetHarvestAreaDetails")]
        public IHttpActionResult GetHarvestAreaDetails()
        {
            var aa = _areaMaterialReceivedService.GetHarvestAreaDetails();
            return Ok(aa);
        }

        [Route("Get")]
        public IHttpActionResult GetDetails()
        {
            var aa = _areaMaterialReceivedService.AllAsync();
            return Ok(aa);
        }

    }
}
