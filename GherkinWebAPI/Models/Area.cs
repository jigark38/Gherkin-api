using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GherkinWebAPI.Models
{
    public class Area
    {
        [JsonProperty("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Key]
        [JsonProperty("areaId")]
        public string Area_ID { get; set; }
        [JsonProperty("areaCode")]
        public int Area_Code { get; set; }
        [JsonProperty("areaName")]
        public string Area_Name { get; set; }
        [JsonProperty("areaEntryDate")]
        public DateTime Area_Entry_Date { get; set; }
        [JsonProperty("areaEnteredEmpId")]
        public string Area_Entered_Emp_ID { get; set; }
    }
}