using GherkinWebAPI.Core.TransportVehicleManagement;
using GherkinWebAPI.Models.TransportVehicleManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GherkinWebAPI.Persistence;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace GherkinWebAPI.Repository.TransportVehicleManagement
{
	public class HiredTransporterDetailRepository : RepositoryBase<HiredTransporterDetailRepository>, IHiredTransporterDetailRepository
	{
		private RepositoryContext _context;
		public HiredTransporterDetailRepository(RepositoryContext repositoryContext) : base(repositoryContext)
		{
			_context = repositoryContext;
		}

		public async Task<HiredTransporterDetail> CreateHiredTransporterDetails(HiredTransporterDetail hiredTransporterDetail)
		{
			try
			{
				_context.Hired_Transporter_Details.Add(hiredTransporterDetail);
				await _context.SaveChangesAsync();
				return hiredTransporterDetail;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		public async Task<List<HiredTransporterDetail>> GetHiredTransporterList()
		{
			return await _context.Hired_Transporter_Details.ToListAsync();
		}

		public async Task<HiredTransporterDetail> UpdateHiredTransporterDetails(HiredTransporterDetail hiredTransporterDetail)
		{
			try
			{
				var transporter = await _context.Hired_Transporter_Details.Where(x => x.HiredTransID == hiredTransporterDetail.HiredTransID).FirstOrDefaultAsync();
				if (transporter != null && hiredTransporterDetail != null)
				{
					transporter.EmpCreatedID = hiredTransporterDetail.EmpCreatedID;
					transporter.DateOfEntry = hiredTransporterDetail.DateOfEntry;
					transporter.TransporterName = hiredTransporterDetail.TransporterName;
					transporter.TransporterManagementName = hiredTransporterDetail.TransporterManagementName;
					transporter.TransAddress = hiredTransporterDetail.TransAddress;
					transporter.TransContactNo = hiredTransporterDetail.TransContactNo;
					transporter.TransAltContactNo = hiredTransporterDetail.TransAltContactNo;
					transporter.TransMailId = hiredTransporterDetail.TransMailId;

					_context.Hired_Transporter_Details.AddOrUpdate(transporter);
					await _context.SaveChangesAsync();
					return hiredTransporterDetail;
				}
				else
				{
					return new HiredTransporterDetail();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			
		}
	}
}