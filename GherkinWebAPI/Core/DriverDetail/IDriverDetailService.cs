using GherkinWebAPI.Models.DriverDetail;
using GherkinWebAPI.Models.DriverDocument;
using System.Collections.Generic;
using System.Threading.Tasks;
using DriverDetails = GherkinWebAPI.Models.DriverDetail.DriverDetail;
namespace GherkinWebAPI.Core.DriverDetail
{
    public interface IDriverDetailService
    {
        Task<IList<DriverDetails>> GetDriverDetail();
        Task<DriverDetails> GetDriverDetail(int driverId);
        Task<DriverDetails> AddDriverDetail(DriverDetails driverDetail);
        Task<DriverDetails> UpdateDriverDetail(DriverDetails driverDetail);
        Task<bool> DeleteDriverDetail(int driverid);
        Task<object> UploadDriverDocument(DriverDocument driverDocument);
        Task<DriverDetails> GetDriverDetailByEmployeeId(int employeeid);
        Task<object> GetDriverDocumentsByEmployeeId(int employeeid);
        Task<DriverDocument> GetDriverDocumentByDocumentUploadNumber(int documentUploadNumber);
        Task<object> GetAllEmployeeNotRegisterWithDriverDetails(string designationcode);
        Task<object> GetAllEmployeeRegisterWithDriverDetails(string designationcode);
        Task<List<DriverDTO>> GetAllDriverNames();
    }
}
