using GherkinWebAPI.Models.DriverDetail;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Repository.DriverDetailRepository
{
    public interface IDriverDetailRepository
    {
        Task<List<Models.DriverDetail.DriverDetail>> GetDriverDetails();
        Task<DriverDetail> GetDriverDetail(int driverId);
        Task<bool> AddDriverDetail(DriverDetail driverDetail);
        Task<bool> UpdateDriverDetail(DriverDetail driverDetail);
        Task<bool> DeleteDriverDetail(int driverid);
        Task<DriverDetail> GetDriverDetailByEmployeeId(int employeeid);
        Task<List<int>> GetDriverIdsByEmployeeId(int employeeId);
        Task<List<string>> GetAllEmployeesIdsSaveWithDriverDetails();
        Task<List<DriverDTO>> GetAllDriverNames();
    }
}