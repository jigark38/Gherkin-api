using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO.FeedInputTransfer
{
    public class RMInputTransferDetailsDto
    {
        public DateTime rMTransferDate { get; set; }
        public string areaID { get; set; }
        public string cropGroupCode { get; set; }
        public string cropNameCode { get; set; }
        public string cropSchemeCode { get; set; }
        public string pSNumber { get; set; }
        public string inputTransferRemarks { get; set; }
        public string rMTransferNo { get; set; }

         
    }
}