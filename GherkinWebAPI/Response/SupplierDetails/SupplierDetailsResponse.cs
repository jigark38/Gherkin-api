using GherkinWebAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Response.SupplierDetails
{
    public class SupplierDetailsResponse
    {
        public SupplierDetailsResponse()
        {
            SupplierDetailsDto = new SupplierDetailsDto();
            SupplierDocumentsDtos = new List<SupplierDocumentsDto>();
        }
        public SupplierDetailsDto SupplierDetailsDto { get; set; }
        public List<SupplierDocumentsDto> SupplierDocumentsDtos { get; set; }
    }
}