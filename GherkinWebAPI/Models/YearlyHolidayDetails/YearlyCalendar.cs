using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GherkinWebAPI.Models
{
    public class YearlyCalendar
    {
       
        [JsonProperty("dateOfEntry")]
        public DateTime Entry_Date { get; set; }
        [JsonProperty("employeeId")]
        public string Entered_Emp_ID { get; set; }
        [Key]
        [JsonProperty("holidaysPassingNo")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Yearly_Holidays_Passing_No { get; set; }
        [JsonProperty("calenderYearFromDate")]
        public DateTime Yearly_Calender_Date_From { get; set; }
        [JsonProperty("calenderYearToDate")]
        public DateTime Yearly_Calender_Date_To { get; set; }
        [JsonProperty("yearlyStatutoryHolidayCount")]
        public int Yearly_Statutory_Holidays { get; set; }
        
    }
}