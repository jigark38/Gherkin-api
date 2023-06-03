using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.MaterialOutward
{
    [Table("Material_Outwad_Details")]
    public class MaterialOutwardDetails
    {
        [Column("MOD_ID")]
        public int Id { get; set; }

        [Column("RM_Transfer_No")]
        public string RmTransferNo { get; set; }

        [Column("Employee_ID")]
        public int EmployeeId { get; set; }

        [Column("Area_ID")]
        public string AreaId { get; set; }

        [Column("OGP_Date")]
        public DateTime OgpDate { get; set; }

        [Column("OGP_No")]
        public string OgpNumber { get; set; }

        [Column("MD_Transporter_Name")]
        public string TransporterName { get; set; }

        [Column("MD_Vehicle_No")]
        public string VehicleNumber { get; set; }

        [Column("MD_Driver_Name")]
        public string DriverName { get; set; }

        [Column("MD_Driver_Contact_No")]
        public string DriverContactNumber { get; set; }

        [Column("MD_Field_Staff")]
        public string FieldStaff { get; set; }

        [Column("MD_Freight_Amount")]
        public int? FreightAmount { get; set; }

        [Column("MD_Advance_Amount")]
        public int? AdvanceAmount { get; set; }

        [Column("MD_Approx_Material_Amount")]
        public int? ApproxMaterialAmount { get; set; }

        [Column("Despatch_Date_Time")]
        public DateTime DespatchDate { get; set; }

        [Column("MD_Remarks")]
        public string Remarks { get; set; }

        [NotMapped]
        public string AreaName { get; set; }

        [NotMapped]
        public int? TotalMaterial { get; set; }

    }
}