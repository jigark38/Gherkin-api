using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GherkinWebAPI.Models.DriverDocument
{
    public class DriverDocument
    {
        [Key]
        [Column("Doc_Upload_No")]
        public string DocumentUploadNumber { get; set; }

        [Column("Driver_ID")]
        public int DriverID { get; set; }

        [Column("Document_Name")]
        public string DocumentName { get; set; }

        [Column("Document_Details")]
        public byte[] DocumentDetail { get; set; }
    }
}