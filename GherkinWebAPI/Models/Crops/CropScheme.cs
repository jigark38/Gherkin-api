namespace GherkinWebAPI.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="CropScheme" />
    /// </summary>
    [Table("Crop_Schemes_Details")]
   public class CropScheme
   {
        /// <summary>
        /// Gets or sets the CropSchemeId
        /// </summary>
        [Column("ID")]
       public int CropSchemeId { get; set; }

        /// <summary>
        /// Gets or sets the Code
        /// </summary>
       [Column("Crop_Scheme_Code")]
       public string Code { get; set; }

        /// <summary>
        /// Gets or sets the CropCode
        /// </summary>
        [Column("Crop_Name_Code")]
        public string CropCode { get; set; }

        /// <summary>
        /// Gets or sets the From
        /// </summary>
        [Column("Crop_Scheme_From")]
        public int From { get; set; }

        /// <summary>
        /// Gets or sets the Sign
        /// </summary>
        [Column("Crop_Scheme_Sign")]     
        public string Sign { get; set; }

        /// <summary>
        /// Gets or sets the Count
        /// </summary>
        [Column("Crop_Count_mm")]
        public decimal Count { get; set; }
    }
}