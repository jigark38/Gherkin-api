using GherkinWebAPI.DTO.RMStock;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GherkinWebAPI.Response.RMStock
{
    public class RMStockBranchInsertResponse
    {
        public RMStockBranchInsertResponse()
        {
            RMStockBranchDto = new RMStockBranchDto();
            RMStockBranchQuantityDetailDTO = new List<RMStockBranchQuantityDetailDTO>();
        }
        
        [JsonProperty("responseMessage")]
        public string ResponseMessage { get; set; }

        [JsonProperty("rmStockBranchDetails")]
        public RMStockBranchDto RMStockBranchDto { get; set; }

        [JsonProperty("rmStockBranchQuantityDetails")]
        public List<RMStockBranchQuantityDetailDTO> RMStockBranchQuantityDetailDTO { get; set; }
    }
}