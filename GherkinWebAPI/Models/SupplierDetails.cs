using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    [Table("Supplier_Information_Details")] 
    public class SupplierDetails
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("Supplier_Org_ID")]
        public string supplierOrgID { get; set; }

        [Column("Supplier_Creation_Date")]
        public DateTime creationDate { get; set; }

        [Column("Emp_Created_ID")]
        public string userName { get; set; }

        [Column("Supplier_Organisation_Name")]
        public string organisationName { get; set; }

        [Column("Supplier_Organisation_Legal_Status")]
        public string legalStatus { get; set; }

        [Column("Supplier_Organisation_Address")]
        public string address { get; set; }

        //[ForeignKey("Country_Details")]
        [Column("Country_Code")]
        public int countryID { get; set; }

        //[ForeignKey("State_Details")]
        [Column("State_Code")]
        public int stateID { get; set; }

        [Column("District_Code")]
        public int districtID { get; set; }
        
        [Column("Place_Code")]
        public int placeCode { get; set; }

        //[ForeignKey("Place_Details")]
        [Column("Supplier_PIN_Code")]
        public int pinCode { get; set; }

        [Column("Supplier_Management_Name")]
        public string mgmName { get; set; }

        [Column("Supplier_Management_Designation")]
        public string designation { get; set; }

        [Column("Supplier_Management_CN")]
        public long mgmContactNumber { get; set; }

        [Column("Supplier_Management_MID")]
        public string correspondanceMailID { get; set; }

        [Column("Supplier_Management_Alt_MID")]
        public string altCorrespondanceMailID { get; set; }

        [Column("Supplier_Org_Contact_Person")]
        public string contactPerson { get; set; }

        [Column("Supplier_Org_Contact_Person_Designation")]
        public string contactPersonDesignation { get; set; }

        [Column("Supplier_Org_Contact_Person_CN")]
        public long? contactPersonNumber { get; set; }

        [Column("Supplier_Org_Contact_Person_MID")]
        public string contactPersonMailID { get; set; }

        [Column("Supplier_Organisation_Office_Number")]
        public long? officeNumber { get; set; }

        [Column("Supplier_Organisation_Activity")]
        public string activity { get; set; }

        [Column("Supplier_Organisation_GSTN")]
        public string gstNo { get; set; }

        [Column("Supplier_Organisation_Website")]
        public string website { get; set; }

        [Column("Supplier_Organisation_License_No")]
        public string licenseNo { get; set; }

        [Column("Supplier_Organisation_Bank_Name")]
        public string bankName { get; set; }

        [Column("Supplier_Organisation_Bank_Branch")]
        public string bankBranch { get; set; }

        [Column("Supplier_Organisation_Bank_Account_No")]
        public long bankActNo { get; set; }

        [Column("Supplier_Organisation_Bank_IFSC")]
        public string iFSCCode { get; set; }

        [Column("Supplier_Approved_By_Emp_ID")]
        public string approvedBy { get; set; }

        //public string districtName { get; set; }
        //public string countryName { get; set; }
        //public string stateName { get; set; }
        //public string placeName { get; set; }

    }
}