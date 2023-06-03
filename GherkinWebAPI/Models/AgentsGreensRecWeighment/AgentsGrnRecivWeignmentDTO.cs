using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.AgentsGreensRecWeighment
{
    public class AgentsGrnRecivWeignmentDTO
    {
        public GreensAgentReceivedDetails greensAgentReceivedDetails { get; set; }
        public List<GreensAgentActualWeightDetails> greensAgentActualWeightDetailsList { get; set; }
        public List<GreensAgentDespCountWeightDetails> greensAgentDespCountWeightDetailsList { get; set; }
        public List<GreensAgentGradesActualDetails> greensAgentGradesActualDetails { get; set; }
    }
}