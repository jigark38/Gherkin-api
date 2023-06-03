using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    public class ARMaterialOutwadDetailsDTO
    {
        public string RMTransferNo { get; set; }
        public DateTime RMTransferDate { get; set; }
        public string RawMaterialGroupCode { get; set; }
        public string RawMaterialDetailsCode { get; set; }
        public decimal RMMaterialTransferQty { get; set; }

        public string AreaID { get; set; }
        public DateTime OGPDate { get; set; }
        public string OGPNo { get; set; }
        public int OGPId { get; set; }
        public string MDDriverName { get; set; }
        public string MDDriverContactNo { get; set; }

        public string RawMaterialDetailsName { get; set; }

        public string RawMaterialUOM { get; set; }

        public string RawMaterialGroup { get; set; }
        public string CropSchemeCode { get; set; }
        public DateTime DespDate { get; set; }
    }

    public class ARMaterialGridUnderDTO
    {

        public String EmployeeStatus { get; set; }

        public String EmployeeID { get; set; }

        public DateTime FSEntryDate { get; set; }

        public string EmployeeName { get; set; }

        public string RMTransferNo { get; set; }
    }

    public class NOTE1
    {
        public String RMTransferNo { get; set; }
        public String PSNumber { get; set; }
        public DateTime SeasonFromDate { get; set; }
        public DateTime SeasonToDate { get; set; }
    }
}