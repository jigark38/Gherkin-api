using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Request
{
    public class SupplierDetailsRequest
    {
        public SupplierDetails SupplierDetailsModel { get; set; }

        public List<SupplierDetailsDTO> SupplierDocumentDetails { get; set; }
        
    }
}