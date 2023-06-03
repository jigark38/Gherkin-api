using System.Collections.Generic;
using System.Threading.Tasks;
using GherkinWebAPI.Models;

namespace GherkinWebAPI.Core
{
    public interface IDepartmentService
    {
        Task<Department> CreateDepartment(Department department);
        Task<List<Department>> GetDepartments();
        Task<List<Department>> GetDepartmentsByOrganiation(int orgOfficeNo);
        Task<Department> UpdateDeparment(Department department);
    }
}