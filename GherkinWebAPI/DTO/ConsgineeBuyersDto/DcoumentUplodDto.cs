using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.DTO
{
    public class DcoumentUplodDto
    {
        /// <summary>
        /// Gets or sets the CBCode
        /// </summary>
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("cbCode")]
        public string C_B_Code { get; set; }


        [JsonProperty("documentNo")]
        public string Document_No { get; set; }

        /// <summary>
        /// Gets or sets the Dcoumnet Name
        /// </summary>
        [JsonProperty("documentName")]
        public string Document_Name { get; set; }

        [JsonProperty("documentDetails")]
        public string DocDetails { get; set; }


        [JsonIgnore]
        [JsonProperty("DocDetailstest")]
        public byte[] Document_Details { get; set; }

        [JsonProperty("ImagePreappendText")]
        public string ImagePreappendText { get; set; }



    }
}