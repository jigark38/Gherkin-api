using System;

namespace GherkinWebAPI.DTO
{
    /// <summary>
    /// Defines the <see cref="PlantationScheduleDto" />
    /// </summary>
    public class PlantationScheduleDto
    {

        /// <summary>
        /// Gets or sets the PsNumber
        /// </summary>
        public string PsNumber { get; set; }

        /// <summary>
        /// Gets or sets the PsDate
        /// </summary>
        public DateTime PsDate { get; set; }

        /// <summary>
        /// Gets or sets the CropNameCode
        /// </summary>
        public string CropNameCode { get; set; }

        /// <summary>
        /// Gets or sets the CropGroupCode
        /// </summary>
        public string CropGroupCode { get; set; }

        /// <summary>
        /// Gets or sets the FromDate
        /// </summary>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// Gets or sets the ToDate
        /// </summary>
        public DateTime ToDate { get; set; }

        /// <summary>
        /// Gets or sets the PreparedBy
        /// </summary>
        public string PreparedBy { get; set; }

        /// <summary>
        /// Gets or sets the ApprovedBy
        /// </summary>
        public string ApprovedBy { get; set; }
    }
}
