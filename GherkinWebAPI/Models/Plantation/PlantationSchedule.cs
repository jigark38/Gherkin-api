using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models
{
    /// <summary>
    /// Defines the <see cref="PlantationSchedule" />
    /// </summary>
    [Table("Plantation_Sch_Details")]
    public class PlantationSchedule
    {
        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the PsNumber
        /// </summary>
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Key]
        [Column("PS_Number")]
        public string PsNumber { get; set; }

        /// <summary>
        /// Gets or sets the PsDate
        /// </summary>
        [Column("PS_Date")]
        public DateTime PsDate { get; set; }

        /// <summary>
        /// Gets or sets the CropNameCode
        /// </summary>
        [Column("Crop_Name_Code")]
        public string CropNameCode { get; set; }

        /// <summary>
        /// Gets or sets the CropGroupCode
        /// </summary>
        [Column("Crop_Group_Code")]
        public string CropGroupCode { get; set; }

        /// <summary>
        /// Gets or sets the FromDate
        /// </summary>
        [Column("Season_From_Date")]
        public DateTime FromDate { get; set; }

        /// <summary>
        /// Gets or sets the ToDate
        /// </summary>
        [Column("Season_To_Date")]
        public DateTime ToDate { get; set; }

        /// <summary>
        /// Gets or sets the PreparedBy
        /// </summary>
        [Column("PS_Prepared_By_Employee_ID")]
        public string PreparedBy { get; set; }

        /// <summary>
        /// Gets or sets the ApprovedBy
        /// </summary>
        [Column("PS_Approved_By_Employee_ID")]
        public string ApprovedBy { get; set; }
    }
}
