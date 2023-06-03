using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Web.Http;
using GherkinWebAPI.Core;
using GherkinWebAPI.Models;
using System.Web.Http.Results;
using GherkinWebAPI.Filter;

namespace GherkinWebAPI.Controllers
{
    //[GherkinwebapiAuth]
    //[RoleAuthorize(Roles = "Admin")]
    public class AreaController :ApiController
    {
        private readonly IAreaService _service;

        public AreaController(IAreaService areaService)
        {
            _service = areaService;
        }

        [HttpGet]
        [Route("Area/GetAllArea")]
        public async Task<IHttpActionResult> GetAllArea()
        {
            List<Area> areas = new List<Area>();
            try
            {
                areas = await _service.GetAllArea();
                if (areas.Count > 0)
                    return Ok(areas);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);                
            }
            return Json(areas);
        }
        [HttpPost]
        [Route("Area/AddArea")]
        public async Task<IHttpActionResult> AddArea([FromBody]Area area)
        {
            try
            {
                await _service.CreateArea(area);
                return Ok();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in AreaController/{nameof(AreaController.GetAllArea)}");
                return InternalServerError();
                return InternalServerError();
            }
        }
    }
}