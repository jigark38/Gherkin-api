namespace GherkinWebAPI.DTO
{
    using System;

    /// <summary>
    /// Defines the <see cref="CropGroupDto" />
    /// </summary>
    public class CropGroupDto
    {
        /// <summary>
        /// Gets or sets the CropGroupId
        /// </summary>
        public int CropGroupId { get; set; }

        /// <summary>
        /// Gets or sets the EntryDate
        /// </summary>
        public DateTime EntryDate { get; set; }

        /// <summary>
        /// Gets or sets the UserId
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the CropGroupCode
        /// </summary>
        public string CropGroupCode { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name { get; set; }
    }
}