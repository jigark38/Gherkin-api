using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;
using System.Collections.Generic;

namespace GherkinWebAPI.Response.RMStock
{
    public class RMBranchShowFormResponse
    {
        public List<Area> Area { get; set; }

        public List<RawMaterialMasterDto> RawMaterialMasters { get; set; }

        public List<RawMaterialDetailsDto> RawMaterialDetails { get; set; }

    }
}