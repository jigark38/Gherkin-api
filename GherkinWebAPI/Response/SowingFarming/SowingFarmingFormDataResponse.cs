using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GherkinWebAPI.Response.SowingFarming
{
    public class SowingFarmingFormDataResponse
    {
        public SowingFarmingFormDataResponse()
        {
            CropGroups = new List<CropGroupDto>();
            CropNames = new List<CropDto>();
            SowingSessions = new List<SowingSessionDto>();
        }

        [JsonProperty("cropGroups")]
        public List<CropGroupDto> CropGroups { get; set; }

        [JsonProperty("cropNames")]
        public List<CropDto> CropNames { get; set; }

        [JsonProperty("sowingSessions")]
        public List<SowingSessionDto> SowingSessions { get; set; }
    }
}