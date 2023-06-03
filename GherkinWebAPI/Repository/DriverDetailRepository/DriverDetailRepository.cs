using GherkinWebAPI.Models.DriverDetail;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;

namespace GherkinWebAPI.Repository.DriverDetailRepository
{
    public class DriverDetailRepository : RepositoryBase<DriverDetail>, IDriverDetailRepository
    {
        private RepositoryContext _context;
        public DriverDetailRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            _context = repositoryContext;
        }

        public async Task<List<DriverDetail>> GetDriverDetails()
        {
            return await _context.DriverDetails.ToListAsync();
        }

        public async Task<DriverDetail> GetDriverDetail(int driverId)
        {
            return await _context.DriverDetails.FirstOrDefaultAsync(driverDetails => driverDetails.DriverID == driverId);
        }

        public async Task<bool> AddDriverDetail(DriverDetail driverDetail)
        {
            _context.DriverDetails.Add(driverDetail);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> UpdateDriverDetail(DriverDetail driverDetail)
        {
            var entity = await _context.DriverDetails.FindAsync(driverDetail.DriverID);
            if (entity == null)
            {
                return false;
            }
            _context.DriverDetails.AddOrUpdate(driverDetail);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteDriverDetail(int driverid)
        {
            var entity = await _context.DriverDetails.FindAsync(driverid);
            if (entity == null)
            {
                return false;
            }
            _context.DriverDetails.Remove(entity);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<DriverDetail> GetDriverDetailByEmployeeId(int employeeid)
        {
            return await _context.DriverDetails.FirstOrDefaultAsync(driverDetails => driverDetails.EmployeeID == employeeid.ToString());
        }

        public async Task<List<int>> GetDriverIdsByEmployeeId(int employeeId)
        {
            return await _context.DriverDetails.Where(driverDetails => driverDetails.EmployeeID == employeeId.ToString()).Select(driver => driver.DriverID).ToListAsync();
        }

        public async Task<List<string>> GetAllEmployeesIdsSaveWithDriverDetails()
        {
            return await _context.DriverDetails.Select(driverDetail => driverDetail.EmployeeID).Distinct().ToListAsync();
        }

        public async Task<List<DriverDTO>> GetAllDriverNames()
        {
            var driverDetails = await (from d in _context.DriverDetails
                                       join e in _context.Employees on d.EmployeeID equals e.employeeId
                                       select new DriverDTO
                                       {
                                           DriverId = d.DriverID,
                                           EmployeeId = e.employeeId,
                                           EmployeeName = e.employeeName,
                                           EmpContactNo = e.employeeContactNo
                                       }).ToListAsync();

            return driverDetails;
        }
    }
}