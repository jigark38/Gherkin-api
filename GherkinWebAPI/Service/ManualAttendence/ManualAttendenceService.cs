using GherkinWebAPI.Core;
using GherkinWebAPI.Core.ManualAttendence;
using GherkinWebAPI.Models.ManualAttendence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service.ManualAttendence
{
    public class ManualAttendenceService : IManualAttendenceService
    {
        private readonly IRepositoryWrapper _repository;
        public ManualAttendenceService(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        public async Task<ManualAttendenceData> GetManualAttendenceDetailsList(string areadId, DateTime fromDate, DateTime toDate, int pageIndex, int pageSize)
        {

            var result = await _repository.ManualAttendenceDetailsRepository.GetManualAttendenceDetailsList(areadId, fromDate, toDate, pageIndex, pageSize);
            var distinctEMpList = result.ToList().OrderByDescending(a=>a.EmployeeName).Select(a => a.EmployeeID).Distinct();
            var paginatedEmp = distinctEMpList.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            var manudata = new ManualAttendenceData();
            manudata.AttendanceData = result.ToList().Where(a => paginatedEmp.Any(b => a.EmployeeID == b)).ToList();
            manudata.TotalCount = distinctEMpList.ToList().Count;
            return manudata;
        }

        public async Task<bool> SaveManualAttendence(List<ManualAttendenceDto> manualAttendenceDtos)
        {
            return await _repository.ManualAttendenceDetailsRepository.SaveManualAttendence(manualAttendenceDtos);

        }

    }
}