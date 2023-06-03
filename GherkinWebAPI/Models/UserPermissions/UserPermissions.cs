using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    public class UserPermissions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("menuId")]
        public int MenuId { get; set; }

        [JsonProperty("submenuId")]
        public int SubmenuId { get; set; }

        [JsonProperty("organisation")]
        public int Organisation { get; set; }

        [JsonProperty("locationId")]
        public int OfficeLocation { get; set; }

        //[ForeignKey("ParentId")]
        //public int ParentId { get; set; }
        [JsonIgnore]
        public SubMenu Submenu { get; set; }
    }

    public class PermissionDTO
    {
        [JsonProperty("userId")]
        public string UP_Serial_No { get; set; }

        [JsonProperty("locationId")]
        public int Org_Office_No { get; set; }

        [JsonProperty("selected")]
        public List<string> Selected { get; set; }

        [JsonProperty("unselected")]
        public List<string> Unselected { get; set; }

        [JsonProperty("dateofPermission")]
        public DateTime PermisisonDate { get; set; }
    }

    public class ResetPasswordDTO
    {
        [JsonProperty("userId")]
        public string UP_Serial_No { get; set; }

        [JsonProperty("userName")]
        public string User_Name { get; set; }

        [JsonProperty("password")]
        public string User_Password { get; set; }
    }


}