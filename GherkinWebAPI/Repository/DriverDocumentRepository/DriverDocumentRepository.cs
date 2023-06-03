using GherkinWebAPI.Core.DriverDocuments;
using GherkinWebAPI.Models.DriverDocument;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;

namespace GherkinWebAPI.Repository.DriverDocumentRepository
{
    public class DriverDocumentRepository : RepositoryBase<DriverDocument>, IDriverDocumentRepository
    {
        private RepositoryContext _context;
        public DriverDocumentRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            _context = repositoryContext;
        }

        public async Task<object> GetDriverDocumentsByDriverIds(List<int> driverIds)
        {
            return await _context.DriverDocuments.Where(driverDocument => driverIds.Contains(driverDocument.DriverID)).Select(driverDocument => new
            {
                driverid = driverDocument.DriverID,
                documentUploadNumber = driverDocument.DocumentUploadNumber,
                driverDocumentName = driverDocument.DocumentName
            }).ToListAsync();
        }

        public async Task<bool> UploadDriverDocument(DriverDocument driverDocument)
        {
            try
            {
                driverDocument.DocumentUploadNumber = await GenerateDocUploadNumber(driverDocument.DriverID);
                _context.DriverDocuments.Add(driverDocument);
                return (await _context.SaveChangesAsync() > 0);
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        public async Task<DriverDocument> GetDriverDocumentByDocumentUploadNumber(int driverDocumentId)
        {
            return await _context.DriverDocuments.FirstOrDefaultAsync(driverDetail => driverDetail.DocumentUploadNumber == driverDocumentId.ToString());
        }

        private async Task<string> GenerateDocUploadNumber(int driverId)
        {
            var autoIncreamentCount = await _context.DriverDocuments.AsNoTracking().CountAsync();
            // Need to increase Doc_Upload_No column length from 15 to 30 then uncomment this code
            //  return "Doc_DID_" + Convert.ToString(driverId) + "_SI_" + Convert.ToString(autoIncreamentCount + 1);
            return "Doc_" + Convert.ToString(driverId) + "_" + Convert.ToString(autoIncreamentCount + 1);
        }


    }
}