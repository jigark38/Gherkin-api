using GherkinWebAPI.Core.ManualAttendence;
using GherkinWebAPI.Models.ManualAttendence;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Repository.ManualAttendence
{
    public class ManualAttendenceMasterRepository : RepositoryBase<ManualAttendenceMaster>, IManualAttendenceMasterRepository
    {
        private readonly RepositoryContext _context;
        public ManualAttendenceMasterRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            _context = repositoryContext;
        }
    }
}