using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.PurchageMgmt
{
    public class IndentDetail
    {
        public DateTime indentDate { get; set; }
        public string indentNo { get; set; }
        public string materialGroupCode { get; set; }
        public string rawMaterialGroupName { get; set; }
        public string rawMaterialDetailsCode { get; set; }
        public string rawMaterialDetailsName { get; set; }
        public string branchAreaId { get; set; }
        public string branchArea { get; set; }
        public string indentUom { get; set; }
        public decimal indentQty { get; set; } = 0;
        public decimal availableQty { get; set; } = 0;
        public decimal orderQty { get; set; } = 0;
    }
    public class IndentDetailResponse
    {
        public DateTime RM_Indent_Entry_Date { get; set; }
        public string RM_INDENT_NO { get; set; }
        public string Raw_Material_Group_Code { get; set; }
        public string Raw_Material_Group { get; set; }
        public string Raw_Material_Details_Code { get; set; }
        public string Raw_Material_Details_Name { get; set; }
        public string AREA_ID { get; set; }
        public string Area_Name { get; set; }
        public string Raw_Material_UOM { get; set; }
        public decimal RM_Indent_Req_Qty { get; set; } = 0;
        public decimal availableQty { get; set; } = 0;
        public decimal orderQty { get; set; } = 0;
    }
}