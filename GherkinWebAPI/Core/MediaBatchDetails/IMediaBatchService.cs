using GherkinWebAPI.Models.InputTransferDetails;
using GherkinWebAPI.Models.MediaBatchDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.MediaBatchDetails
{
    public interface IMediaBatchService
    {
        Task<List<EmployeeIdAndName>> GetAllEmployeeIdAndName();
        Task<List<MediaMaterialDetails>> GetMediaMaterialDetails(DateTime date, string MediaProcessCode, decimal totalQty);
        Task<List<MediaStockAndBatchDetail>> GetStockAndBatchDetailsFirst(DateTime date);
        Task<MediaBatchProductionAndMaterialDetails> SaveMediaBatchMaterialDetails(MediaBatchProductionAndMaterialDetails obj);
    }
}
