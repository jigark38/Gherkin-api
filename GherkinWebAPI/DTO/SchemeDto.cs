using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO
{
    public class SchemeDto
    {
        /// <summary>
        /// Gets or sets the CropSchemeId
        /// </summary>
        public int CropSchemeId { get; set; }

        /// <summary>
        /// Gets or sets the Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the CropCode
        /// </summary>
        public string CropCode { get; set; }

        /// <summary>
        /// Gets or sets the From
        /// </summary>
        public int From { get; set; }

        /// <summary>
        /// Gets or sets the Sign
        /// </summary>
        public string Sign { get; set; }

        /// <summary>
        /// Gets or sets the Count
        /// </summary>
        public decimal Count { get; set; }
    }
}