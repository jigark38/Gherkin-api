using GherkinWebAPI.DTO;
using GherkinWebAPI.DTO.RMStock;
using System.Collections.Generic;

namespace GherkinWebAPI.Response.RMStock
{
    public class RMStockFormInsertResponse
    {
        public RMStockFormInsertResponse()
        {
            RMStockDetailsDto = new RMStockDetailsDto();
            RMStockLotDetailsDto = new List<RMStockLotDetailsDto>();
        }

        public RMStockDetailsDto RMStockDetailsDto { get; set; }

        public List<RMStockLotDetailsDto> RMStockLotDetailsDto { get; set; }
    }
}