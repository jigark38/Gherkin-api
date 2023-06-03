using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    public class GSCUomDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty("gscUomCode")]
        [Required]
        public int GSC_UOM_Code { get; set; }

        [JsonProperty("gscUomName")]
        [Required]
        [MaxLength(100)]
        public string GSC_UOM_Name { get; set; }
    }
}