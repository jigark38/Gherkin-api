using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO.MediaProcessDetail
{
    public class MediaProcessDetailsDTO
    {
       
        [JsonProperty("mediaProcessCode")]
       
        public string MediaProcessCode { get; set; }

        

        [JsonProperty("mediaProcessName")]
       
        public string MediaProcessName { get; set; }

        
    }
}