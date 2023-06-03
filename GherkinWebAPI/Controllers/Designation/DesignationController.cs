using GherkinWebAPI.Core;
using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace GherkinWebAPI.Controllers
{
    [Route("api/V1/[Controller]")]
    public class DesignationController : ApiController
    {
        private readonly IDesignationService _service;
        public DesignationController(IDesignationService designationService)
        {
            _service = designationService;
        }
        [HttpGet]
        [Route("GetDesignations/{subDepartment}")]
        public async Task<IHttpActionResult> GetDesignationBySubdepartment(string subDepartment)
        {
            List<Designation> designations = new List<Designation>();
            try
            {
                designations = await _service.GetDesignationsByCondition(subDepartment);
                if (designations.Count > 0)
                    return Ok(designations);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in DesignationController/{nameof(DesignationController.GetDesignationBySubdepartment)}");
                return InternalServerError();
            }

        }

        [HttpGet]
        [Route("GetAllDesignations")]
        public async Task<IHttpActionResult> GetAllDesignations()
        {
            List<Designation> designations = new List<Designation>();
            try
            {
                designations = await _service.GetAllDesignations();
                if (designations.Count > 0)
                    return Ok(designations);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in DesignationController/{nameof(DesignationController.GetAllDesignations)}");
                return InternalServerError();
            }

        }

        [HttpPost]
        [Route("CreateDesignation")]
        public async Task<IHttpActionResult> CreateDesignation([FromBody] Designation designation)
        {
            try
            {
                var degn = await _service.CreateDesignation(designation);
                return Ok(degn);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in DesignationController/{nameof(DesignationController.CreateDesignation)}");
                return InternalServerError();
            }

        }

        [HttpPost]
        [Route("UpdateDesignation")]
        public async Task<IHttpActionResult> UpdateDesignation([FromBody] Designation designation)
        {
            try
            {
                var degn = await _service.UpdateDesignation(designation);
                return Ok(degn);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
