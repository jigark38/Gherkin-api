using System;
using System.ComponentModel.DataAnnotations;

namespace GherkinWebAPI.Entities.MaterialInward
{
    public class MaterialInwardEntity
    {
        public int ID { get; set; }

        public string Inward_Type { get; set; }

        public DateTime Inward_Date_Time { get; set; }

        [Key]
        public string Inward_Gate_Pass_No { get; set; }

        public string Supplier_Transporter_Name { get; set; }

        public string Supplier_Transporter_Place { get; set; }

        public string Inv_DC_No { get; set; }

        public DateTime Inv_DC_Date { get; set; }

        public string Inv_Vehicle_No { get; set; }

        public string Driver_Name { get; set; }

        public string Employee_No { get; set; }

        public string Inward_Remarks { get; set; }

        public string Received_Material_Name { get; set; }

        public decimal? Received_Quantity { get; set; }

        public int Org_Office_No { get; set; }
    }
}