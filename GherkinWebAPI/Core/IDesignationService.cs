using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core
{
    public interface IDesignationService
    {
        Task<Designation> CreateDesignation(Designation designation);
        Task<List<Designation>> GetDesignationsByCondition(string subDepartment);
        Task<List<Designation>> GetAllDesignations();
        Task<Designation> UpdateDesignation(Designation designation);
    }
}
