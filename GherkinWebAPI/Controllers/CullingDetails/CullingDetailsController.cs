using GherkinWebAPI.Core.CullingDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace GherkinWebAPI.Controllers.CullingDetails
{
    [Route("api/V1/[Controller]")]
    public class CullingDetailsController : ApiController
    {
        private readonly ICullingDetailsService _cullingDetailsService;
        public CullingDetailsController(ICullingDetailsService cullingDetailsService)
        {
            _cullingDetailsService = cullingDetailsService;
        }


        [Route("GetGradedMaterialDetails"), HttpGet]
        public async Task<IHttpActionResult> GetGradedMaterialDetails(int OrgOfficeNo)
        {
            try
            {
                var res = await _cullingDetailsService.GetGradedMaterialDetails(OrgOfficeNo);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }


        [Route("AddCullingDetails"), HttpPost]
        public async Task<IHttpActionResult> AddCullingDetails()
        {
            try
            {
                await _cullingDetailsService.AddCullingDetails();
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}