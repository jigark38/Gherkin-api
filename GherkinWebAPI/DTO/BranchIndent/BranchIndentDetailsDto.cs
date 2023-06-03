using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO.BranchIndent
{
    public class BranchIndentDetailsDto
    {
        public int ID { get; set; }

        public string RM_Indent_No { get; set; }

        public DateTime RM_Indent_Entry_Date { get; set; }

        public string RM_Indent_Emp_ID { get; set; }

        public string Area_ID { get; set; }

        public string Request_To { get; set; }
    }
}