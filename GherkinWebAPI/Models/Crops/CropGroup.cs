using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    [Table("Crop_Group_Details")]
    public class CropGroup
    {
        [Column("ID")]
        public int CropGroupId { get; set; }

        [Column("CS_Entry_Date")]
        public DateTime EntryDate { get; set; }

        [Column("CS_Entered_Emp_ID")]
        public string UserId { get; set; }

        [Column("Crop_Group_Code")]
        public string CropGroupCode { get; set; }


        [Column("Crop_Group_Name")]
        public string Name { get; set; }
    }
}