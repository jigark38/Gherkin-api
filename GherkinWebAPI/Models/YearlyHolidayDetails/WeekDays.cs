using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace GherkinWebAPI.Models
{
    public class WeekDay
    {
        [Key]
        [JsonProperty("weekdaysId")]
        public int Weekly_Weekdays_No { get; set; }
        [JsonProperty("weekdaysName")]
        public string Weekly_Weekdays_Names { get; set; }
    }
}