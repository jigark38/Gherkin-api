using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace GherkinWebAPI.Models
{

    [Table("FP_Grades_Details")]
    public class GradeDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [JsonProperty("id")]
        public int ID { get; set; }

        [Required]
        [MaxLength(10)]
        [JsonProperty("varietyCode")]
        [Column("FP_Variety_Code")]
        public string VarietyCode { get; set; }
        [Key]
        [Required]
        [MaxLength(10)]
        [Column("FP_Grade_Code")]
        [JsonProperty("gradeCode")]
        public string GradeCode { get; set; }

        [Required]      
        [Column("FP_Grade_From")]
        [JsonProperty("gradeFrom")]
        public int GradeFrom { get; set; }
        [Required]      
        [Column("FP_Grade_To")]
        [JsonProperty("gradeTo")]
        public int GradeTo { get; set; }

    }
}