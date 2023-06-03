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
	public class HiredVehicleDetailRepository : RepositoryBase<HiredVehicleDetailRepository>, IHiredVehicleDetailRepository
	{
		private RepositoryContext _context;
		public HiredVehicleDetailRepository(RepositoryContext repositoryContext) : base(repositoryContext)
		{
			_context = repositoryContext;
		}

		public async Task<HiredVehicleDetail> CreateHiredVehicleDetails(HiredVehicleDetail hiredVehicleDetail)
		{
			try
			{
				_context.Hired_Vehicle_Details.Add(hiredVehicleDetail);
				await _context.SaveChangesAsync();
				return hiredVehicleDetail;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<List<HiredVehicleDetail>> GetHiredVehicleList(int hiredTransporterId)
		{
			return await _context.Hired_Vehicle_Details.Where(x => x.HiredTransID == hiredTransporterId).ToListAsync();
		}

		public async Task<bool> CheckDuplicateHiredVehicleRegNo(string vehicleRegNo)
		{
			return await _context.Hired_Vehicle_Details.Where(x => x.VehicleRegNumber.ToString() == vehicleRegNo.ToString()).CountAsync() > 0;
		}

		public async Task<bool> CheckDuplicateHiredVehicleChasisNo(string chasisNumber)
		{
			return await _context.Hired_Vehicle_Details.Where(x => x.VehicleChassisNo.ToString() == chasisNumber.ToString()).CountAsync() > 0;
		}

		public async Task<HiredVehicleDetail> UpdateHiredVehicleDetails(HiredVehicleDetail hiredVehicleDetail)
		{
			try
			{
				var hiredVehicle = await _context.Hired_Vehicle_Details.Where(x => x.HiredVehicleID == hiredVehicleDetail.HiredVehicleID).FirstOrDefaultAsync();
				if (hiredVehicle != null && hiredVehicleDetail != null)
				{
					hiredVehicle.VehicleType = hiredVehicleDetail.VehicleType;
					hiredVehicle.VehicleMake = hiredVehicleDetail.VehicleMake;
					hiredVehicle.VehicleDOP = hiredVehicleDetail.VehicleDOP;
					hiredVehicle.VehicleRegNumber = hiredVehicleDetail.VehicleRegNumber;
					hiredVehicle.VehicleChassisNo = hiredVehicleDetail.VehicleChassisNo;
					hiredVehicle.VehicleNosTyres = hiredVehicleDetail.VehicleNosTyres;
					hiredVehicle.VehicleAvgMileage = hiredVehicleDetail.VehicleAvgMileage;
					hiredVehicle.VehicleRenewalDuration = hiredVehicleDetail.VehicleRenewalDuration;
					hiredVehicle.VehicleMaxCapacity = hiredVehicleDetail.VehicleMaxCapacity;

					_context.Hired_Vehicle_Details.AddOrUpdate(hiredVehicle);
					await _context.SaveChangesAsync();
					return hiredVehicleDetail;
				}
				else
				{
					return new HiredVehicleDetail();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}
	}
}