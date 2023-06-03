using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{

    public class DocumentUpload
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty("id")]
        public int Id { get; set; }
        [Key]

        
        [MaxLength(10)]
        [JsonProperty("cbCode")]
        public string C_B_Code { get; set; }

        [Column("Document_Name")]
        [JsonProperty("documentName")]
        [Required]
        [MaxLength(100)]
        public string Document_Name { get; set; }

        [Column("Document_No")]
        [JsonProperty("documentNo")]
        public string Document_No { get; set; }

        [Column("Document_Details")]
        [JsonProperty("documentDetails")]
        public byte[] Document_Details { get; set; }

        [JsonProperty("ImagePreappendText")]
        public string ImagePreappendText { get; set; }


    }
}