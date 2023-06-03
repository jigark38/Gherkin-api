using GherkinWebAPI.DTO.Reports.DepAndDes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Core.Reports.DepAndDes
{
    public interface IDepartMentAndDesignationRepository
    {
        Task<List<DepAndDesDto>> GetReportForDepartmentAndDesignation(string departmentCode = "");
    }
}