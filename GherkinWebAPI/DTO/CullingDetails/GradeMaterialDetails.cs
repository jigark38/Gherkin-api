using System;

namespace GherkinWebAPI.DTO.CullingDetails
{
    /// <summary>
    /// Defines the <see cref="GradeMaterialDetails" />.
    /// </summary>
    public class GradeMaterialDetails
    {
        /// <summary>
        /// Gets or sets the OrgOfficeNo.
        /// </summary>
        public int OrgOfficeNo { get; set; }

        /// <summary>
        /// Gets or sets the HarvestGrnNo.
        /// </summary>
        public long HarvestGrnNo { get; set; }

        /// <summary>
        /// Gets or sets the AreaId.
        /// </summary>
        public string AreaId { get; set; }

        /// <summary>
        /// Gets or sets the AreaName.
        /// </summary>
        public string AreaName { get; set; }

        /// <summary>
        /// Gets or sets the GradedTotalQuantity.
        /// </summary>
        public decimal GradedTotalQuantity { get; set; }

        /// <summary>
        /// Gets or sets the TotalReceivedQty.
        /// </summary>
        public decimal TotalReceivedQty { get; set; }

        /// <summary>
        /// Gets or sets the GreensGradingQtyNo.
        /// </summary>
        public int GreensGradingQtyNo { get; set; }

        /// <summary>
        /// Gets or sets the CropNameCode.
        /// </summary>
        public string CropNameCode { get; set; }

        /// <summary>
        /// Gets or sets the CropName.
        /// </summary>
        public string CropName { get; set; }

        /// <summary>
        /// Gets or sets the CropSchemeCode.
        /// </summary>
        public string CropSchemeCode { get; set; }

        /// <summary>
        /// Gets or sets the CropScheme.
        /// </summary>
        public string CropScheme { get; set; }

        /// <summary>
        /// Gets or sets the QuantityAfterGradingTotal.
        /// </summary>
        public decimal QuantityAfterGradingTotal { get; set; }

        /// <summary>
        /// Gets or sets the BatchProductionNo.
        /// </summary>
        public long BatchProductionNo { get; set; }

        /// <summary>
        /// Gets or sets the BsGreensConsumptionNo.
        /// </summary>
        public long BsGreensConsumptionNo { get; set; }

        /// <summary>
        /// Gets or sets the GreensGradeQtyNo.
        /// </summary>
        public int GreensGradeQtyNo { get; set; }

        /// <summary>
        /// Gets or sets the BSGradingQuantity.
        /// </summary>
        public decimal BSGradingQuantity { get; set; }

        /// <summary>
        /// Gets or sets the BatchProductionDate.
        /// </summary>
        public DateTime BatchProductionDate { get; set; }

        /// <summary>
        /// Gets or sets the MediaProcessCode.
        /// </summary>
        public string MediaProcessCode { get; set; }

        /// <summary>
        /// Gets or sets the BatchSizeApprox.
        /// </summary>
        public decimal BatchSizeApprox { get; set; }

        /// <summary>
        /// Gets or sets the SalesOrderScheduleNo.
        /// </summary>
        public long SalesOrderScheduleNo { get; set; }

        /// <summary>
        /// Gets or sets the DirectOrderScheduleNo.
        /// </summary>
        public long DirectOrderScheduleNo { get; set; }

        /// <summary>
        /// Gets or sets the GroupCode.
        /// </summary>
        public string GroupCode { get; set; }

        /// <summary>
        /// Gets or sets the VarietyCode.
        /// </summary>
        public string VarietyCode { get; set; }

        /// <summary>
        /// Gets or sets the GradeCode.
        /// </summary>
        public string GradeCode { get; set; }

        /// <summary>
        /// Gets or sets the PackUOM.
        /// </summary>
        public string PackUOM { get; set; }

        /// <summary>
        /// Gets or sets the ProductionQtyNos.
        /// </summary>
        public int ProductionQtyNos { get; set; }

        /// <summary>
        /// Gets or sets the ProductionQtyinUOM.
        /// </summary>
        public long ProductionQtyinUOM { get; set; }

        /// <summary>
        /// Gets or sets the MediaProcessName.
        /// </summary>
        public string MediaProcessName { get; set; }

        /// <summary>
        /// Gets or sets the GradeFrom.
        /// </summary>
        public int GradeFrom { get; set; }

        /// <summary>
        /// Gets or sets the GradeTo.
        /// </summary>
        public int GradeTo { get; set; }
    }
}
