using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models.TransportVehicleManagement
{
	public class HiredTransporterDetail
	{
        [Key]
        [Column("Hired_Trans_ID")]
        [JsonProperty("hiredTransID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int HiredTransID { get; set; }

        [Column("Emp_Created_ID")]
        [JsonProperty("empCreatedID")]
        [MaxLength(20)]
        [Required]
        public string EmpCreatedID { get; set; }

        [Column("Date_Of_Entry")]
        [JsonProperty("dateOfEntry")]
        [Required]
        public DateTime DateOfEntry { get; set; }

        [Column("Transporter_Name")]
        [JsonProperty("transporterName")]
        [MaxLength(100)]
        [Required]
        public string TransporterName { get; set; }

        [Column("Trans_Management_Name")]
        [JsonProperty("transporterManagementName")]
        [MaxLength(100)]
        [Required]
        public string TransporterManagementName { get; set; }

        [Column("Trans_Address")]
        [JsonProperty("transAddress")]
        [MaxLength(300)]
        [Required]
        public string TransAddress { get; set; }

        [Column("Trans_Contact_No")]
        [JsonProperty("transContactNo")]
        [Required]
        public Int64 TransContactNo { get; set; }

        [Column("Trans_Alt_Contact_No")]
        [JsonProperty("transAltContactNo")]
        public Int64 TransAltContactNo { get; set; }

        [Column("Trans_Mail_Id")]
        [JsonProperty("transMailId")]
        [MaxLength(30)]
        public string TransMailId { get; set; }
    }
}