using GherkinWebAPI.Core.MediaProcessDetail;
using GherkinWebAPI.DTO.MediaProcessDetail;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Repository.MediaProcessDetail
{
    public class MediaProcessDetailsRepository : IMediaProcessDetailsRepository
    {
        private readonly RepositoryContext _context;
        public MediaProcessDetailsRepository(RepositoryContext context)
        {
            _context = context;
        }

        public async Task<List<MediaProcessDetailsDTO>> GetAllMediaProcessDetails()
        {

            var emp = from e in _context.MediaProcessDetails
                      orderby e.MediaProcessName
                      select new MediaProcessDetailsDTO
                      {
                          MediaProcessCode = e.MediaProcessCode,
                          MediaProcessName = e.MediaProcessName
                      };
            return await emp.ToListAsync();

        }

       
    }
}