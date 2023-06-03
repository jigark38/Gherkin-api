using GherkinWebAPI.Core;
using GherkinWebAPI.Core.DriverDetail;
using GherkinWebAPI.Models.DriverDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DriverDetails = GherkinWebAPI.Models.DriverDetail.DriverDetail;
namespace GherkinWebAPI.Service.DriverDetail
{
    public class DriverDetailService : IDriverDetailService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public DriverDetailService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<IList<DriverDetails>> GetDriverDetail()
        {
            return await _repositoryWrapper.DriverDetailRepository.GetDriverDetails();
        }

        public async Task<DriverDetails> GetDriverDetail(int driverId)
        {
            return await _repositoryWrapper.DriverDetailRepository.GetDriverDetail(driverId);
        }

        public async Task<DriverDetails> AddDriverDetail(DriverDetails driverDetail)
        {
            var isdriverDetailSaved = await _repositoryWrapper.DriverDetailRepository.AddDriverDetail(driverDetail);
            if (isdriverDetailSaved)
            {
                return driverDetail;
            }
            return null;
        }

        public async Task<DriverDetails> UpdateDriverDetail(DriverDetails driverDetail)
        {
            var isdriverDetailUpdated = await _repositoryWrapper.DriverDetailRepository.UpdateDriverDetail(driverDetail);
            if (isdriverDetailUpdated)
            {
                return driverDetail;
            }
            return null;
        }

        public async Task<bool> DeleteDriverDetail(int driverid)
        {
            return await _repositoryWrapper.DriverDetailRepository.DeleteDriverDetail(driverid);
        }

        public async Task<object> UploadDriverDocument(Models.DriverDocument.DriverDocument driverDocument)
        {
            var isDocumentSaved = await _repositoryWrapper.DriverDocumentRepository.UploadDriverDocument(driverDocument);
            if (isDocumentSaved)
            {
                return new
                {
                    documentName = driverDocument.DocumentName,
                    isSaved = isDocumentSaved
                };
            }
            return new
            {
                documentName = driverDocument.DocumentName,
                isSaved = isDocumentSaved
            };
        }

        public async Task<DriverDetails> GetDriverDetailByEmployeeId(int employeeid)
        {
            return await _repositoryWrapper.DriverDetailRepository.GetDriverDetailByEmployeeId(employeeid);
        }

        public async Task<object> GetDriverDocumentsByEmployeeId(int employeeid)
        {
            var driverDetail = await GetDriverDetailByEmployeeId(employeeid);
            var driverIds = await _repositoryWrapper.DriverDetailRepository.GetDriverIdsByEmployeeId(employeeid);
            var allDriverDocuments = (await _repositoryWrapper.DriverDocumentRepository.GetDriverDocumentsByDriverIds(driverIds));
            return new
            {
                empCreatedID = driverDetail.EmpCreatedID,
                driverEntryDate = driverDetail.DriverEntryDate,
                employeeID = driverDetail.EmpCreatedID,
                drivingYearsExp = driverDetail.DrivingYearsExp,
                driverLicenseType = driverDetail.DriverLicenseType,
                driverLicenseNumber = driverDetail.DriverLicenseNumber,
                driverExpiryDate = driverDetail.DriverExpiryDate,
                drivingLicenseIssueAuthority = driverDetail.DrivingLicenseIssueAuthority,
                documentList = allDriverDocuments
            };
        }

        public async Task<Models.DriverDocument.DriverDocument> GetDriverDocumentByDocumentUploadNumber(int documentUploadNumber)
        {
            return await _repositoryWrapper.DriverDocumentRepository.GetDriverDocumentByDocumentUploadNumber(documentUploadNumber);
        }

        public async Task<object> GetAllEmployeeNotRegisterWithDriverDetails(string designationcode)
        {
            var allEmployeesIdsSaveWithDriverDetails = await _repositoryWrapper.DriverDetailRepository.GetAllEmployeesIdsSaveWithDriverDetails();
            if (allEmployeesIdsSaveWithDriverDetails == null)
            {
                allEmployeesIdsSaveWithDriverDetails = new List<string>();
            }
            var allEmployeeWithDesignationCode = (await _repositoryWrapper.EmployeeRepository.GetAllEmployeeByDesignationCode(designationcode)).ToList();

            return allEmployeeWithDesignationCode.Where(employee => !allEmployeesIdsSaveWithDriverDetails.Contains(employee.employeeId)).Select(emp => new
            {
                empId = emp.employeeId,
                empName = emp.employeeName,
                contactNo = emp.employeeContactNo
            });
        }

        public async Task<object> GetAllEmployeeRegisterWithDriverDetails(string designationcode)
        {
            var allEmployeesIdsSaveWithDriverDetails = await _repositoryWrapper.DriverDetailRepository.GetAllEmployeesIdsSaveWithDriverDetails();
            if (allEmployeesIdsSaveWithDriverDetails == null)
            {
                allEmployeesIdsSaveWithDriverDetails = new List<string>();
            }
            var allEmployeeWithDesignationCode = (await _repositoryWrapper.EmployeeRepository.GetAllEmployeeByDesignationCode(designationcode)).ToList();

            return allEmployeeWithDesignationCode.Where(employee => allEmployeesIdsSaveWithDriverDetails.Contains(employee.employeeId)).Select(emp => new
            {
                empId = emp.employeeId,
                empName = emp.employeeName,
                contactNo = emp.employeeContactNo
            });
        }

        public async Task<List<DriverDTO>> GetAllDriverNames()
        {
            return await _repositoryWrapper.DriverDetailRepository.GetAllDriverNames();
        }
    }
}