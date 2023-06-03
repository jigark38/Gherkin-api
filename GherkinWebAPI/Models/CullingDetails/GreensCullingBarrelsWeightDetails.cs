using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models.CullingDetails
{
    /// <summary>
    /// Defines the <see cref="GreensCullingBarrelsWeightDetails" />.
    /// </summary>
    [Table("Greens_Culling_Barrels_Weight_Details")]
    public class GreensCullingBarrelsWeightDetails
    {
        /// <summary>
        /// Gets or sets the ProductionBarrelNo.
        /// </summary>
        [Key]
        [Column("Production_Barrel_No")]
        public int ProductionBarrelNo { get; set; }

        /// <summary>
        /// Gets or sets the GreensCullingNo.
        /// </summary>
        [Column("Greens_Culling_No")]
        public int GreensCullingNo { get; set; }

        /// <summary>
        /// Gets or sets the CullingWeightNo.
        /// </summary>
        [Column("Culling_Weight_No")]
        public int CullingWeightNo { get; set; }

        /// <summary>
        /// Gets or sets the ProductionBarrelNoVisible.
        /// </summary>
        [Column("Production_Barrel_No_Visible")]
        public string ProductionBarrelNoVisible { get; set; }

        /// <summary>
        /// Gets or sets the HarvestGrnNo.
        /// </summary>
        [Column("Harvest_GRN_No")]
        public long HarvestGrnNo { get; set; }

        /// <summary>
        /// Gets or sets the CropNameCode.
        /// </summary>
        [Column("Crop_Name_Code")]
        public string CropNameCode { get; set; }

        /// <summary>
        /// Gets or sets the CropSchemeCode.
        /// </summary>
        [Column("Crop_Scheme_Code")]
        public string CropSchemeCode { get; set; }

        /// <summary>
        /// Gets or sets the BatchProductionNo.
        /// </summary>
        [Column("Batch_Production_No")]
        public long BatchProductionNo { get; set; }

        /// <summary>
        /// Gets or sets the FpVarietyCode.
        /// </summary>
        [Column("FP_Variety_Code")]
        public string FpVarietyCode { get; set; }

        /// <summary>
        /// Gets or sets the FpGradeCode.
        /// </summary>
        [Column("FP_Grade_Code")]
        public string FpGradeCode { get; set; }

        /// <summary>
        /// Gets or sets the BarrelSizeFor.
        /// </summary>
        [Column("Barrel_Size_for")]
        public int BarrelSizeFor { get; set; }

        /// <summary>
        /// Gets or sets the BarrelMaterialQuantity.
        /// </summary>
        [Column("Barrel_Material_Quantity")]
        public int BarrelMaterialQuantity { get; set; }

        /// <summary>
        /// Gets or sets the BarrelUom.
        /// </summary>
        [Column("Barrel_UOM")]
        public string BarrelUom { get; set; }
    }
}
