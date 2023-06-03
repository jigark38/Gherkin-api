using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO.Reports.DepAndDes
{
    public class DepAndDesDto
    {
        public string SkillsCode { get; set; }

        public string SkillsName { get; set; }

        public string DepartmentCode { get; set; }

        public string DepartmentName { get; set; }

        public string SubDepartmentCode { get; set; }

        public string SubDepartmentName { get; set; }

        public string DesignationCode { get; set; }

        public string DesignationName { get; set; }
    }
}