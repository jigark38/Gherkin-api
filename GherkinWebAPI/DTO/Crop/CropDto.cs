namespace GherkinWebAPI.DTO
{
    /// <summary>
    /// Defines the <see cref="CropDto" />
    /// </summary>
    public class CropDto
    {
        /// <summary>
        /// Gets or sets the CropId
        /// </summary>
        public int CropId { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the CropCode
        /// </summary>
        public string CropCode { get; set; }

        /// <summary>
        /// Gets or sets the CropGroupCode
        /// </summary>
        public string CropGroupCode { get; set; }
    }
}