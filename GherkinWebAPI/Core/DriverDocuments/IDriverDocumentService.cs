using System.Threading.Tasks;

namespace GherkinWebAPI.Core.DriverDocuments
{
    public interface IDriverDocumentService
    {
        Task<bool> UploadDriverDocuments();
    }
}