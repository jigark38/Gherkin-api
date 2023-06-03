using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core
{
    public interface ISubDepartmentService
    { 
        Task<SubDepartment> CreateSubDepartment(SubDepartment subdepartment);
        Task<List<SubDepartment>> GetSubDepartments();
        Task<List<SubDepartment>> GetSubDepartmentsByCondition(string subDepartment);
        Task<SubDepartment> UpdateSubDepartment(SubDepartment subdepartment);
    }
}
