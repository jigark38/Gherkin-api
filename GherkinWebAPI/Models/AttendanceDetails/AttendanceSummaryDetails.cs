using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.AttendanceDetails
{
    public class AttendanceSummaryDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [JsonProperty("attendConfirmationNo")]
        public int Attend_Confirmation_No { get; set; }


        [JsonProperty("employeeID")]
        public string Employee_ID { get; set; }

        [JsonProperty("monthYear")]
        public string Month_Year { get; set; }

        [JsonProperty("totalAttendedNoofDays")]
        public decimal Total_Attended_No_of_Days { get; set; }


        [JsonProperty("noofDaysConsiderThisMonth")]
        public decimal No_of_Days_Consider_This_Month { get; set; }


        [JsonProperty("noofDaysCarryForward")]
        public decimal No_of_Days_Carry_Forward { get; set; }


        
    }
}