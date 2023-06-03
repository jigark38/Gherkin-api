using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GherkinWebAPI.Models
{
    public class UserPermission
    {

        [JsonProperty("authorisedBy")]
        public string UP_Employee_ID { get; set; }

        [JsonProperty("organisationId")]
        public int Org_Code { get; set; }

        [JsonProperty("locationId")]
        public int Org_Office_No { get; set; }

        [JsonProperty("employeeId")]
        public string Employee_ID { get; set; }

        [JsonProperty("userName")]
        public string User_Name { get; set; }

        [JsonProperty("password")]
        public string User_Password { get; set; }

        [JsonProperty("permissions")]
        public string Permissions { get; set; }

        [JsonProperty("dateofPermission")]
        public DateTime UP_Date { get; set; }

        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[JsonIgnore]
        //[JsonProperty("userID")]
        //public int UserID { get; set; }

        //[JsonProperty("userName")]
        //public string UserName { get; set; }

        //[JsonProperty("roles")]
        //public string Roles { get; set; }

        //[JsonProperty("password")]
        //public string Password { get; set; }

        //[JsonProperty("active")]
        //public bool Active { get; set; }

        //[JsonProperty("createdDate")]
        //public DateTime CreatedDate { get; set; }
    }


    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [JsonIgnore]
        public int Id { get; set; }

        [JsonIgnore]
        public string UP_Serial_No { get; set; }

        [JsonProperty("authorisedBy")]
        public string UP_Employee_ID { get; set; }

        [JsonProperty("organisationId")]
        public int Org_Code { get; set; }

        [JsonProperty("officeLocationId")]
        public int Org_Office_No { get; set; }

        [JsonProperty("employeeId")]
        public string Employee_ID { get; set; }

        [JsonProperty("userName")]
        public string User_Name { get; set; }

        [JsonProperty("userPassword")]
        public byte[] User_Password { get; set; }

        [JsonIgnore]
        public string UP_Date { get; set; }
    }

    public class Permission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        public string Permissions { get; set; }
    }



    public class UserDetails
    {
        [JsonProperty("id")]
        public string UP_Serial_No { get; set; }

        [JsonProperty("userName")]
        public string User_Name { get; set; }
    }

    public class UsersDetails
    {
        [JsonProperty("userId")]
        public string UP_Serial_No { get; set; }

        [JsonProperty("userName")]
        public string User_Name { get; set; }

        [JsonProperty("employeeId")]
        public string EmployeeID { get; set; }

        [JsonProperty("organisationId")]
        public int Org_Code { get; set; }

        [JsonProperty("officeLocationId")]
        public int Org_Office_No { get; set; }


        [JsonProperty("departmentId")]
        public string DepartmentId { get; set; }
        [JsonProperty("departmentName")]
        public string Department_Name { get; set; }

        [JsonProperty("subDepartmentId")]
        public string SubDepartmentId { get; set; }

        [JsonProperty("subdepartmentName")]
        public string Sub_Department_Name { get; set; }


        [JsonProperty("designation")]
        public string Designation { get; set; }
        [JsonProperty("designationId")]
        public string DesignationId { get; set; }

        [JsonProperty("employeeName")]
        public string Employee_Name { get; set; }

        [JsonProperty("dateOfPermission")]
        public string UP_Date { get; set; }
    }
}
