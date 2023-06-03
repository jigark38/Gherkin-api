using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace GherkinWebAPI.Models
{
    public class ProductGroup
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty("id")]
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the FP_Creation_Date
        /// </summary>        
        [Column("FP_Creation_Date")]
        [JsonProperty("createdDate")]
        public Nullable<DateTime> CreatedDate { get; set; }
        /// <summary>
        /// Gets or sets the Code
        /// </summary>
        [Key]
        [Column("FP_Group_Code")]
        [JsonProperty("groupCode")]
        public string GroupCode { get; set; }

        /// <summary>
        /// Gets or sets the FP_Creation_Date
        /// </summary>
        [Required]
        [MaxLength(50)]
        [Column("FP_Group_Name")]
        [JsonProperty("grpName")]
        public string GrpName { get; set; }
    }
}