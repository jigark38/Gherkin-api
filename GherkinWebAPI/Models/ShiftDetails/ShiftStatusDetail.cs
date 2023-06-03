using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.ShiftDetail
{
    [Table("Shift_Status_Details")] 
    public partial class ShiftStatusDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [JsonProperty("iD")]
        [Column("ID")]
        public int ID { get; set; }

        [Required]
        [JsonProperty("shiftNo")]
        [Column("Shift_No")]
        public long ShiftNo { get; set; }

        [Required]
        [JsonProperty("shiftStatus")]
        [Column("Shift_Status")]
        public string ShiftStatus { get; set; }

        [Required]
        [JsonProperty("shiftCancellationEffectiveFromDate")]
        [Column("Shift_Cancellation_Effective_From_Date")]
        public System.DateTime ShiftCancellationEffectiveFromDate { get; set; }
    }
}