using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace GherkinWebAPI.Models
{
    public class StatutoryHoliday
    {       
        [JsonProperty("holidaysPassingNo")]
        public int Yearly_Holidays_Passing_No { get; set; }
        [Key]
        [JsonProperty("statutoryHolidaysNo")]
        public int Statutory_Holiday_No { get; set; }
        [JsonProperty("holidaysDate")]
        public DateTime Holiday_Date { get; set; }
        [JsonProperty("weekDaysId")]
        public int Weekly_Weekdays_No { get; set; }
        [JsonProperty("holidayOccasion")]
        public string Holiday_Occasion { get; set; }
        [JsonProperty("employeeId")]
        public string Entered_Emp_ID { get; set; }
    }
}