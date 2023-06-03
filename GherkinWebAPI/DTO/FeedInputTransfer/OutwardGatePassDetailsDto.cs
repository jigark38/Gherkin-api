using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO.FeedInputTransfer
{
    public class OutwardGatePassDetailsDto
    {
        public string oGPNO { get; set; }
        public string transactionNo { get; set; }
        public DateTime oGPDate { get; set; }
    }
}