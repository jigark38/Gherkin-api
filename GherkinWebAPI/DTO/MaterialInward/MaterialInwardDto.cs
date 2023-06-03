using Newtonsoft.Json;
using System;

namespace GherkinWebAPI.DTO.MaterialInward
{
    public class MaterialInwardDto
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("inwardGatePassNo")]
        public string Inward_Gate_Pass_No { get; set; }

        [JsonProperty("inwardType")]
        public string Inward_Type { get; set; }

        [JsonProperty("inwardDateTime")]
        public DateTime Inward_Date_Time { get; set; }

        [JsonProperty("supplierTransporterName")]
        public string Supplier_Transporter_Name { get; set; }

        [JsonProperty("supplierTransportPlace")]
        public string Supplier_Transporter_Place { get; set; }

        [JsonProperty("invDcNo")]
        public string Inv_DC_No { get; set; }

        [JsonProperty("invDcDate")]
        public DateTime Inv_DC_Date { get; set; }

        [JsonProperty("invVehicleNo")]
        public string Inv_Vehicle_No { get; set; }

        [JsonProperty("driverName")]
        public string Driver_Name { get; set; }

        [JsonProperty("employeeNo")]
        public string Employee_No { get; set; }

        [JsonProperty("inwardRemarks")]
        public string Inward_Remarks { get; set; }

        [JsonProperty("receivedMaterialName")]
        public string Received_Material_Name { get; set; }

        [JsonProperty("receivedQuantity")]
        public decimal? Received_Quantity { get; set; }

        [JsonProperty("orgOfficeNo")]
        public int Org_Office_No { get; set; }

        [JsonProperty("isOngoing")]
        public bool? IsOnGoing { get; set; }
    }
}