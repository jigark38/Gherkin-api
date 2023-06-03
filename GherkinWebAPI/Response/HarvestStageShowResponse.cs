using GherkinWebAPI.DTO;
using GherkinWebAPI.Models.Employee;
using System.Collections.Generic;

namespace GherkinWebAPI.Response
{
    public class HarvestStageShowResponse
    {
        public List<Employee> Employees { get; set; }

        public List<CropGroupDto> CropGroups { get; set; }

        public List<CropDto> Crops{ get; set; }

    }
}