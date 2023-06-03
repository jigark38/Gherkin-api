using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using GherkinWebAPI.Core;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.Employee;
using GherkinWebAPI.ValidateModel;

namespace GherkinWebAPI.Controllers
{
    [Route("api/V1/[Controller]")]
    public class EmployeeController : ApiController
    {
        private IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [Route("GetEmployee")]
        public async Task<HttpResponseMessage> GetEmployeeByCondition(string designation)
        {
            List<EmployeeDTO> employees = new List<EmployeeDTO>();
            try
            {
                employees = await _employeeService.GetEmployeesByCondition(designation);
                return Request.CreateResponse(HttpStatusCode.OK, employees);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [Route("GetAllEmployee")]
        public async Task<IHttpActionResult> GetAllEmployee()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                employees = await _employeeService.GetAllEmployee();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in EmployeeController / {nameof(EmployeeController.GetAllEmployee)}");
                return InternalServerError();
            }
        }

        [Route("GetEmployee/{empId}")]
        public async Task<IHttpActionResult> GetEmployee(string empId)
        {
            Employee employee = new Employee();
            try
            {
                employee = await _employeeService.GetEmployeeById(empId);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in EmployeeController / {nameof(EmployeeController.GetEmployee)}");
                return InternalServerError();
            }

        }

        [CheckModelForNull]
        [ValidateModelState]
        [Route("CreateEmployee")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateEmployee([FromBody] Employee employee)
        {
            try
            {
                if (employee == null)
                    return null;
                var emp = await _employeeService.CreateEmployee(employee);
                return Ok(emp);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in EmployeeController / {nameof(EmployeeController.CreateEmployee)}");
                return InternalServerError();
            }

        }
        [CheckModelForNull]
        [ValidateModelState]
        [Route("UpdateEmployee")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateEmployee([FromBody] Employee employee)
        {
            try
            {
                var emp = await _employeeService.UpdateEmployee(employee);
                return Ok(emp);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in EmployeeController / {nameof(EmployeeController.UpdateEmployee)}");
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("SaveDocument")]
        public async Task<IHttpActionResult> SaveDocument(string employeeId, string imageName)
        {
            try
            {
                EmployeeDocument docDetails = new EmployeeDocument(); ;
                docDetails.employeeId = employeeId;
                docDetails.employeeDocName = imageName;
                var file = HttpContext.Current.Request.Files[imageName];
                if (file != null)
                {
                    BinaryReader br = new BinaryReader(file.InputStream);
                    docDetails.employeeDocDetails = br.ReadBytes(file.ContentLength);
                }

                await _employeeService.SaveDocument(docDetails);
                return Ok(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in EmployeeController / {nameof(EmployeeController.SaveDocument)}");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetAllDocumentByEmpId/{EmployeeId}")]
        public async Task<IHttpActionResult> GetDocumentByEmpId(string EmployeeId)
        {
            List<EmployeeDTO> employees = new List<EmployeeDTO>();
            try
            {
                List<EmployeeDocument> ed = new List<EmployeeDocument>();
                List<HttpResponseMessage> msg = new List<HttpResponseMessage>();
                ed = await _employeeService.GetDocument(EmployeeId);
                if (ed.Count > 0)
                    return Ok(ed);
                else
                    return NotFound();
                //if (ed.Count > 0)
                //{
                //    foreach (EmployeeDocument doc in ed)
                //    {
                //        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, doc.Employee_Document_Details);
                //        MemoryStream ms = new MemoryStream(doc.Employee_Document_Details);
                //        response.Content = new StreamContent(ms);
                //        response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
                //        msg.Add(response);
                //    }
                //    return msg;
                //}
                //else
                //{
                //    return new List<HttpResponseMessage>();
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in EmployeeController / {nameof(EmployeeController.GetDocumentByEmpId)}");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetDocumentByDocId/{docId}")]
        public async Task<HttpResponseMessage> GetDocumentByDocId(int docId)
        {
            List<EmployeeDTO> employees = new List<EmployeeDTO>();
            try
            {
                EmployeeDocument ed = new EmployeeDocument();
                ed = await _employeeService.GetDocumentByDocId(docId);
                if (ed != null)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, ed.employeeDocDetails);
                    MemoryStream ms = new MemoryStream(ed.employeeDocDetails);
                    response.Content = new StreamContent(ms);
                    response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
                    return response;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in EmployeeController / {nameof(EmployeeController.GetDocumentByDocId)}");
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return new HttpResponseMessage();
        }

        [HttpGet]
        [Route("GetEmployeeByBioMetricId/{bioId}")]
        public async Task<IHttpActionResult> GetEmployeeByBioMetricId(int bioId)
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                employees = await _employeeService.GetEmployeeByBioMetricId(bioId);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in EmployeeController / {nameof(EmployeeController.GetEmployeeByBioMetricId)}");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetEmployeeByEmployeeName/{empName}")]
        public async Task<IHttpActionResult> GetEmployeeByEmployeeName(string empName)
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                employees = await _employeeService.GetEmployeeByEmployeeName(empName);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in EmployeeController / {nameof(EmployeeController.GetEmployeeByEmployeeName)}");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("CheckDuplicateBiometricId/{bioId}/{unitId}")]
        public async Task<IHttpActionResult> CheckDuplicateBiometricId(int bioId, int unitId)
        {
            bool isDuplicate = false;
            try
            {
                isDuplicate = await _employeeService.CheckDuplicateBiometricId(bioId, unitId);
                return Ok(isDuplicate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in EmployeeController / {nameof(EmployeeController.CheckDuplicateBiometricId)}");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetEmployeePayment/{EmpId}")]
        public async Task<IHttpActionResult> GetEmployeePayment(string EmpId)
        {
            try
            {
                var empPayment = await _employeeService.GetEmployeePayment(EmpId);
                return Ok(empPayment);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in EmployeeController / {nameof(EmployeeController.GetEmployeePayment)}");
                return InternalServerError();
            }
        }
        [CheckModelForNull]
        [ValidateModelState]
        [HttpPost]
        [Route("CreatePayment")]
        public async Task<IHttpActionResult> CreatePayment([FromBody] EmployeePayment empPayment)
        {
            try
            {
                var empPayments = await _employeeService.CreatePayment(empPayment);
                return Ok(empPayments);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in EmployeeController / {nameof(EmployeeController.CreatePayment)}");
                return InternalServerError();
            }
        }

        [CheckModelForNull]
        [ValidateModelState]
        [HttpPut]
        [Route("UpadtePayment")]
        public async Task<IHttpActionResult> UpadtePayment([FromBody] EmployeePayment empPayment)
        {
            try
            {
                var empPayments = await _employeeService.UpdatePayment(empPayment);
                return Ok(empPayments);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in EmployeeController / {nameof(EmployeeController.UpadtePayment)}");
                return InternalServerError();
            } 
        }

        [HttpGet]
        [Route("GetAllEmployeeByDeptCode")]
        public async Task<IHttpActionResult> GetAllEmployeeByDeptCode(int orgOfficeNo, string deptCode)
        {
            try
            {
                var employees = await _employeeService.GetAllEmployeeByDeptCode(orgOfficeNo, deptCode);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in EmployeeController / {nameof(EmployeeController.GetAllEmployeeByDeptCode)}");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetAllEmployeeByDesignationCode/{designationcode}")]
        public async Task<IHttpActionResult> GetAllEmployeeByDesignationCode(string designationcode)
        {
            try
            {
                var employees = await _employeeService.GetAllEmployeeByDesignationCode(designationcode);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in EmployeeController / {nameof(EmployeeController.GetAllEmployeeByDesignationCode)}");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetEmployeeByDesignationsManager")]
        public async Task<IHttpActionResult> GetEmployeeByDesignationsManager()
        {
            try
            {
                var employees = await _employeeService.GetEmployeeByDesignationsManager();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in EmployeeController / {nameof(EmployeeController.GetEmployeeByDesignationsManager)}");
                return InternalServerError();
            }

        }
    }
}
