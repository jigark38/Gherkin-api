using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.ConsigneeBuyersModel
{
    public class Currency
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty("id")]
        public int ID { get; set; }
        [Key]
       
        [JsonProperty("currencyCode")]
        [Column("Currency_Code")]
        public string Currency_Code { get; set; }
        [Required]
        [MaxLength(50)]
        [Column("Currency_Name")]
        [JsonProperty("currencyName")]
        public string Currency_Name { get; set; }

    }
}