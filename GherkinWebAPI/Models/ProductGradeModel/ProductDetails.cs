using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace GherkinWebAPI.Models
{
    [Table("Finished_Product_Details")]
    public class ProductDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [JsonProperty("id")]
        public int ID { get; set; }
      
        /// <summary>
        /// Gets or sets the FP_Group_Code
        /// </summary>
        [Column("FP_Group_Code")]
        [JsonProperty("groupCode")]
        [Required]
        [MaxLength(10)]
        public string GroupCode { get; set; }
        /// <summary>
        /// Gets or sets the FP_Variety_Code
        /// </summary>
        [Key]
        [Column("FP_Variety_Code")]
        [JsonProperty("varietyCode")]

        public string VarietyCode { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("FP_Variety_Name")]
        [JsonProperty("varietyName")]
        public string VarietyName { get; set; }

        /// <summary>
        /// Gets or sets the FP_Scientific_Name
        /// </summary>        
        [MaxLength(100)]
        [Column("FP_Scientific_Name")]
        [JsonProperty("scientificName")]
        public string ScientificName { get; set; }
    }
}