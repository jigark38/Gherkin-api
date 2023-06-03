using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    public class PermissionsDTO
    {
        public int MenuId { get; set; }
        public int ModuleId { get; set; }

        public int SubmenuId { get; set; }
    }

    public class PermissionData
    {
        public List<int> ModuleId { get; set; }

        public List<int> SubmenuId { get; set; }
    }

    public class PermissionDataResult
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public List<PermissionDataResult> Children {get;set;}
    }


}