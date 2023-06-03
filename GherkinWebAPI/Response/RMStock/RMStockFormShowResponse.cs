using GherkinWebAPI.DTO;
using System.Collections.Generic;

namespace GherkinWebAPI.Response.RMStock
{
    public class RMStockFormShowResponse
    {
        public List<RawMaterialMasterDto> RawMaterialMasters { get; set; }

        public List<RawMaterialDetailsDto> RawMaterialDetails { get; set; }

        public List<OrganisationOfficeLocationDetailsDto> OrganisationOfficeLocationDetails { get; set; }
    }
}