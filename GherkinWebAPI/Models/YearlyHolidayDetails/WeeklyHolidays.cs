using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models
{
    public class WeeklyHoliday
    {
        [Key]
        [JsonProperty("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [JsonProperty("holidaysPassingNo")]                
        public Int64 Yearly_Holidays_Passing_No { get; set; }
        [JsonProperty("weekDaysId")]
        public int Weekly_Weekdays_No { get; set; }
        [JsonProperty("employeeId")]
        public string Entered_Emp_ID { get; set; }
    }
}