using GherkinWebAPI.Core.TransportVehicleManagement;
using GherkinWebAPI.Models.TransportVehicleManagement;
using GherkinWebAPI.Persistence;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace GherkinWebAPI.Repository.TransportVehicleManagement
{
	public class HiredVehicleDocumentRepository : RepositoryBase<HiredVehicleDocumentRepository>, IHiredVehicleDocumentRepository
	{
		private RepositoryContext _context;
		public HiredVehicleDocumentRepository(RepositoryContext repositoryContext) : base(repositoryContext)
		{
			_context = repositoryContext;
		}

		public async Task<HiredVehicleDocument> CreateHiredVehicleDocuments(HiredVehicleDocument hiredVehicleDocument)
		{
			_context.Hired_Vehicle_Documents.Add(hiredVehicleDocument);
			await _context.SaveChangesAsync();
			return hiredVehicleDocument;
		}

		public async Task<HiredVehicleDocument> GetHiredVehicleDocumentByDocId(string docUploadNo)
		{
			return await _context.Hired_Vehicle_Documents.Where(x => x.DocUploadNo == docUploadNo).FirstOrDefaultAsync();
		}

		public async Task<List<HiredVehicleDocument>> GetHiredVehicleDocumentsList(int hiredVehicleId)
		{
			return await _context.Hired_Vehicle_Documents.Where(x => x.HiredVehicleID == hiredVehicleId).ToListAsync();
		}
	}
}