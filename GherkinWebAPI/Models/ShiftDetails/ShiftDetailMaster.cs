using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models.ShiftDetail
{
    [Table("Shift_Details_Master")] 
    public class ShiftDetailMaster
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [JsonProperty("shiftNo")]
        [Column("Shift_No")]
        public long ShiftNo { get; set; }

        [Required]
        [JsonProperty("entryDate")]
        [Column("Entry_Date")]
        public DateTime EntryDate { get; set; }

        [Required]
        [JsonProperty("enteredEmpID")]
        [Column("Entered_Emp_ID")]
        public string EnteredEmpID { get; set; }

        [Required]
        [JsonProperty("shiftEffectiveDate")]
        [Column("Shift_Effective_Date")]
        public DateTime ShiftEffectiveDate { get; set; }
     
        [Required]
        [JsonProperty("shiftName")]
        [Column("Shift_Name")]
        public string ShiftName { get; set; }
        [Required]
        [JsonProperty("shiftTimeFrom")]
        [Column("Shift_Time_From")]
        public TimeSpan ShiftTimeFrom { get; set; }
        [Required]
        [JsonProperty("shiftTimeTo")]
        [Column("Shift_Time_To")]
        public TimeSpan ShiftTimeTo { get; set; }
        [Required]
        [JsonProperty("shiftDuration")]
        [Column("Shift_Duration")]
        public TimeSpan ShiftDuration { get; set; }

        [Required]
        [JsonProperty("shiftBreakTimeFrom")]
        [Column("Shift_Break_Time_From")]
        public TimeSpan ShiftBreakTimeFrom { get; set; }

        [Required]
        [JsonProperty("shiftBreakTimeTo")]
        [Column("Shift_Break_Time_To")]
        public TimeSpan ShiftBreakTimeTo { get; set; }

        [Required]
        [JsonProperty("shiftBreakDuration")]
        [Column("Shift_Break_Duration")]
        public TimeSpan ShiftBreakDuration { get; set; }

        [Required]
        [JsonProperty("shiftRotation")]
        [Column("Shift_Rotation")]
        public string ShiftRotation { get; set; }

        [Required]
        [JsonProperty("shiftRotationDays")]
        [Column("Shift_Rotation_Days")]
        public int ShiftRotationDays { get; set; }

    }
}