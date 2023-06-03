using GherkinWebAPI.DTO.MediaProcessDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.MediaProcessDetail
{
    public interface IMediaProcessDetailsRepository
    {
        Task<List<MediaProcessDetailsDTO>> GetAllMediaProcessDetails();
    }
}
