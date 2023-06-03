using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GherkinWebAPI.DTO
{
    public class YearlyCalendarDTO
    {
        public YearlyCalendarDTO()
        {
            Weekly_Holidays = new List<WeeklyHolidaysDTO>();
            Statutory_Holidays = new List<StatutoryHolidaysDTO>();
        }
        [JsonProperty("dateOfEntry")]
        public DateTime Entry_Date { get; set; }
        [JsonProperty("employeeId")]
        public string Entered_Emp_ID { get; set; }
        [JsonProperty("employeeName")]
        public string Entered_Emp_Name { get; set; }
        [JsonProperty("holidaysPassingNo")]
        public Int64 Yearly_Holidays_Passing_No { get; set; }
        [JsonProperty("calenderYearFromDate")]
        public DateTime Yearly_Calender_Date_From { get; set; }
        [JsonProperty("calenderYearToDate")]
        public DateTime Yearly_Calender_Date_To { get; set; }
        [JsonProperty("yearlyStatutoryHolidayCount")]
        public int Yearly_Statutory_Holidays { get; set; }
        [JsonProperty("weeklyHolidaysList")]
        public List<WeeklyHolidaysDTO> Weekly_Holidays { get; set; }
        [JsonProperty("statutoryHolidaysList")]
        public List<StatutoryHolidaysDTO> Statutory_Holidays { get; set; }
        //[JsonProperty("statutoryHolidaysList")]
        //public List<StatutoryHolidaysDTO> Statutory_Holidays { get; set; }
    }
}