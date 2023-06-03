using Newtonsoft.Json;
using System;

namespace GherkinWebAPI.DTO.ShiftDetails
{
    public class ShiftDetailsDto
    {
        [JsonProperty("shiftNo")]
        public long ShiftNo { get; set; }

        [JsonProperty("entryDate")]
        public DateTime EntryDate { get; set; }

        [JsonProperty("empID")]
        public string EnteredEmpID { get; set; }

        [JsonProperty("shiftEffectiveDate")]
        public DateTime ShiftEffectiveDate { get; set; }        

        [JsonProperty("shiftName")]
        public string ShiftName { get; set; }

        [JsonProperty("shiftTimeFrom")]
        public TimeSpan ShiftTimeFrom { get; set; }

        [JsonProperty("shiftTimeTo")]
        public TimeSpan ShiftTimeTo { get; set; }

        [JsonProperty("shiftDuration")]
        public TimeSpan ShiftDuration { get; set; }

        [JsonProperty("shiftBreakTimeFrom")]
        public TimeSpan ShiftBreakTimeFrom { get; set; }

        [JsonProperty("shiftBreakTimeTo")]
        public TimeSpan ShiftBreakTimeTo { get; set; }

        [JsonProperty("shiftBreakDuration")]
        public TimeSpan ShiftBreakDuration { get; set; }

        [JsonProperty("ShiftRotation")]
        public string ShiftRotation { get; set; }

        [JsonProperty("shiftRotationDays")]
        public int ShiftRotationDays { get; set; }

        [JsonProperty("shiftStatus")]
        public string ShiftStatus { get; set; }

        [JsonProperty("shiftCancellationEffectiveFromDate")]
        public DateTime? ShiftCancellationEffectiveFromDate { get; set; }
    }
}