using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.InputToFieldStaff
{
    [Table("Inputs_Issued_To_Fieldstaff_Details")]
    public class InputsIssuedToFieldstaffDetails
    {
        #region Instance Properties
        [Key]
        [Column("Material_Issued_FS_No")]
        public string MaterialIssuedFSNo { get; set; }

        [Column("Inputs_Issued_FS_Date")]
        public DateTime InputsIssuedFSDate { get; set; }

        [Column("Org_office_No")]
        public int? OrgofficeNo { get; set; }

        [Column("Area_ID")]
        public string AreaID { get; set; }

        [Column("Employee_ID")]
        public string EmployeeID { get; set; }

        [Column("Crop_Group_Code")]
        public string CropGroupCode { get; set; }

        [Column("Crop_Name_Code")]
        public string CropNameCode { get; set; }

        [Column("PS_Number")]
        public string PSNumber { get; set; }

        [Column("Issued_By_Employee_ID")]
        public string IssuedByEmployeeID { get; set; }

        [Column("OGP_NO")]
        public string OGPNO { get; set; }

        #endregion Instance Properties
    }
}