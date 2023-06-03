using GherkinWebAPI.Models.DriverDocument;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.DriverDocuments
{
    public interface IDriverDocumentRepository
    {
        Task<bool> UploadDriverDocument(DriverDocument driverDocument);
        Task<object> GetDriverDocumentsByDriverIds(List<int> driverIds);
        Task<DriverDocument> GetDriverDocumentByDocumentUploadNumber(int driverDocumentId);
    }
}
