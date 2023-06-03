using GherkinWebAPI.Core.Reports.DepAndDes;
using GherkinWebAPI.DTO.Reports.DepAndDes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GherkinWebAPI.Controllers.Reports.DepAndDes
{
    [RoutePrefix("DepartmentAndDesignation")]
    public class DepartmentAndDesignationReportController : ApiController
    {
        private readonly IDepartMentAndDesignationRepository _repository;

        public DepartmentAndDesignationReportController(IDepartMentAndDesignationRepository repository)
        {
            _repository = repository;
        }

        [Route("Get")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDepartMentDesignationReport(string departmentCode = "")
        {
            List<DepAndDesDto> depAndDesDto = new List<DepAndDesDto>();
            try
            {
                depAndDesDto = await _repository.GetReportForDepartmentAndDesignation(departmentCode);
                if (depAndDesDto?.Any() ?? false)
                {
                    return Ok(depAndDesDto);
                }
                else
                {
                    return Ok(new List<DepAndDesDto>());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in DepartmentAndDesignationReportController / {nameof(DepartmentAndDesignationReportController.GetDepartMentDesignationReport)}");
                return InternalServerError();
            }
        }
    }
}
