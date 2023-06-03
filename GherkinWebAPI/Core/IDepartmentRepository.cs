using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GherkinWebAPI.Models;

namespace GherkinWebAPI.Core
{
    public interface IDepartmentRepository
    {
        Task<Department> CreateDepartment(Department department);       
        Task<List<Department>> GetDepartments();
        Task<List<Department>> GetDepartmentsByOrganiation(int orgOfficeNo);
        Task<Department> UpdateDeparment(Department department);
    }
}
