namespace GherkinWebAPI.DTO
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="CropSchemeDto" />
    /// </summary>
    public class CropSchemeDto
    {
        /// <summary>
        /// Gets or sets the EntryDate
        /// </summary>
        public DateTime EntryDate { get; set; }

        /// <summary>
        /// Gets or sets the UserName
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the GroupCode
        /// </summary>
        public string GroupCode { get; set; }

        /// <summary>
        /// Gets or sets the CropName
        /// </summary>
        public string CropName { get; set; }

        /// <summary>
        /// Gets or sets the CropCode
        /// </summary>
        public string CropCode { get; set; }

        /// <summary>
        /// Gets or sets the Schemes
        /// </summary>
        public List<SchemeDto> Schemes { get; set; }
    }
}
