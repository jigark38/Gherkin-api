using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using GherkinWebAPI.Core;
using GherkinWebAPI.Models;

namespace GherkinWebAPI.Controllers
{
    [Route("api/V1/[Controller]")]
    public class DepartmentController : ApiController
    {
        private readonly IDepartmentService _service;
        public DepartmentController(IDepartmentService departmentService)

        {
            _service = departmentService;
        }
        [HttpGet]
        [Route("GetDepartment")]
        public async Task<IHttpActionResult> GetAllDepartment()
        {
            List<Department> departments = new List<Department>();
            try
            {
                departments = await _service.GetDepartments();
                if (departments.Count > 0)
                {
                    return Ok(departments);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in DepartmentController/{nameof(DepartmentController.GetAllDepartment)}");
                return InternalServerError();
            }

        }

        [HttpPost]
        [Route("CreateDepartment")]
        public async Task<IHttpActionResult> CreateDepartment([System.Web.Mvc.Bind(Exclude = "Id, Department_Code")][FromBody] Department department)
        {
            try
            {
                var dept = await _service.CreateDepartment(department);
                return Ok(dept);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in DepartmentController/{nameof(DepartmentController.CreateDepartment)}");
                return InternalServerError();
            }

        }

        [HttpPost]
        [Route("UpdateDepartment")]
        public async Task<IHttpActionResult> UpdateDeparment([FromBody] Department department)
        {
            try
            {
                var dept = await _service.UpdateDeparment(department);
                return Ok(dept);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("GetDepartmentByOrganisation/{orgOfficeNo}")]
        public async Task<IHttpActionResult> GetDepartmentByOrganisation(int orgOfficeNo)
        {
            List<Department> departments = new List<Department>();
            try
            {
                departments = await _service.GetDepartmentsByOrganiation(orgOfficeNo);
                if (departments.Count > 0)
                {
                    return Ok(departments);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in DepartmentController/{nameof(DepartmentController.GetDepartmentByOrganisation)}");
                return InternalServerError();
            }

        }
    }
}
