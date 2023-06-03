using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    /// <summary>
    /// Defines the <see cref="Crop" />
    /// </summary>
    [Table("Crop_Name_Details")]
    public class Crop
    {
        /// <summary>
        /// Gets or sets the CropId
        /// </summary>
        [Column("ID")]
        public int CropId { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        [Column("Crop_Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the CropCode
        /// </summary>
        [Column("Crop_Name_Code")]
        public string CropCode { get; set; }

        /// <summary>
        /// Gets or sets the CropGroupCode
        /// </summary>
        [Column("Crop_Group_Code")]
        public string CropGroupCode { get; set; }
    }
}