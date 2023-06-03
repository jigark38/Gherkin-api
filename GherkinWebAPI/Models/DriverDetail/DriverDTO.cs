using Newtonsoft.Json;

namespace GherkinWebAPI.Models.DriverDetail
{
    public class DriverDTO
    {
        [JsonProperty("driverId")]
        public int DriverId { get; set; }
        [JsonProperty("employeeName")]
        public string EmployeeName { get; set; }
        [JsonProperty("employeeId")]
        public string EmployeeId { get; set; }
        [JsonProperty("empContactNo")]
        public string EmpContactNo { get; set; }
    }
}