using GherkinWebAPI.Core;
using GherkinWebAPI.Core.MediaBatchDetails;
using GherkinWebAPI.Models.InputTransferDetails;
using GherkinWebAPI.Models.MediaBatchDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service.MediaBatchDetails
{
    public class MediaBatchService: IMediaBatchService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public MediaBatchService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public async Task<List<EmployeeIdAndName>> GetAllEmployeeIdAndName()
        {
            return await _repositoryWrapper.MediaBatchRepository.GetAllEmployeeIdAndName();
        }
        public async Task<List<MediaMaterialDetails>> GetMediaMaterialDetails(DateTime date, string MediaProcessCode, decimal totalQty)
        {
            return await _repositoryWrapper.MediaBatchRepository.GetMediaMaterialDetails(date, MediaProcessCode, totalQty);
        }
        public async Task<List<MediaStockAndBatchDetail>> GetStockAndBatchDetailsFirst(DateTime date)
        {
            return await _repositoryWrapper.MediaBatchRepository.GetStockAndBatchDetailsFirst(date);
        }
        public async Task<MediaBatchProductionAndMaterialDetails> SaveMediaBatchMaterialDetails(MediaBatchProductionAndMaterialDetails obj)
        {
            return await _repositoryWrapper.MediaBatchRepository.SaveMediaBatchMaterialDetails(obj);
        }
    }
}