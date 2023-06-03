using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace GherkinWebAPI.DTO
{
    public class WeeklyHolidaysDTO
    {
        [Key]
        [JsonProperty("id")]
        public int ID { get; set; }
        [JsonProperty("holidaysPassingNo")]                
        public Int64 Yearly_Holidays_Passing_No { get; set; }
        [JsonProperty("weekDaysId")]
        public int? Weekly_Weekdays_No { get; set; }
        [JsonProperty("employeeId")]
        public string Entered_Emp_ID { get; set; }
    }
}