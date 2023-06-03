using GherkinWebAPI.Core;
using GherkinWebAPI.Core.MediaProcessDetail;
using GherkinWebAPI.DTO.MediaProcessDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service.MediaProcessDetail
{
    public class MediaProcessDetailsService: IMediaProcessDetailsService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public MediaProcessDetailsService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<MediaProcessDetailsDTO>> GetAllMediaProcessDetails()
        {
            return await _repositoryWrapper.MediaProcessDetailsRepository.GetAllMediaProcessDetails();
        }
    }
}