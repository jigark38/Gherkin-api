using GherkinWebAPI.Models.PurchageMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO.PurchageMgmt
{
    public class CreatePOWithMaterialAndCondition
    {
        public PurchaseOrderDetail purchageOrderDetail { get; set; }
        public List<RMPOMaterialCondition> rmPoMaterialConditions { get; set; }
        public List<RMPOMaterialDetail> rmPoMaterialDetails { get; set; }
        public List<RMPOIndentDetail> rMPOIndentDetails { get; set; }

        public List<IndentMaterialName> indentMaterialNames { get; set; }
        
    }

    public class IndentMaterialName
    {
        public string indentNo { get; set; }
        public string materialGroupCode { get; set; }
        public string rawMaterialGroupName { get; set; }
        public string rawMaterialDetailsCode { get; set; }
        public string rawMaterialDetailsName { get; set; }

    }
}