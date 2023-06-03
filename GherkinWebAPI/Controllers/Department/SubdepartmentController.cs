using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using Antlr.Runtime;
//using System.Web.Mvc;
using GherkinWebAPI.Core;
using GherkinWebAPI.Models;

namespace GherkinWebAPI.Controllers
{
    [Route("api/V1/[Controller]")]
    public class SubdepartmentController : ApiController
    {
        private readonly ISubDepartmentService _service;
        public SubdepartmentController(ISubDepartmentService designationService)
        {
            _service = designationService;
        }
        [HttpGet]
        [Route("GetSubdepartment/{department}")]
        public async Task<IHttpActionResult> GetSubdepartmentbyDepartment(string department)
        {
            List<SubDepartment> subDepartments = new List<SubDepartment>();
            try
            {
                subDepartments = await _service.GetSubDepartmentsByCondition(department);
                if (subDepartments.Count > 0)
                    return Ok(subDepartments);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in SubdepartmentController/{nameof(SubdepartmentController.GetSubdepartmentbyDepartment)}");
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("Createsubdepartment")]
        public async Task<IHttpActionResult> CreateSubdepartment([FromBody]SubDepartment subdepartment)
        {
            try
            {
                var subDepartment = await _service.CreateSubDepartment(subdepartment);
                return Ok(subDepartment);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in SubdepartmentController/{nameof(SubdepartmentController.CreateSubdepartment)}");
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("UpdateSubdepartment")]
        public async Task<IHttpActionResult> UpdateSubDepartment([FromBody]SubDepartment subdepartment)
        {
            try
            {
                var subDepartment = await _service.UpdateSubDepartment(subdepartment);
                return Ok(subDepartment);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}