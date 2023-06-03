using System;
using System.Collections.Generic;
using System.Linq;
using GherkinWebAPI.Models.TransportVehicleManagement;
using GherkinWebAPI.Core.TransportVehicleManagement;
using GherkinWebAPI.Persistence;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace GherkinWebAPI.Repository.TransportVehicleManagement
{
    public class VehicleRepository : RepositoryBase<OwnVehiclesDetails>, IVehicleRepository
    {
        private RepositoryContext _context;
        public VehicleRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            _context = repositoryContext;
        }
        public async Task<bool> CheckDuplicateRegistrationNumber(string registrationNumber)
        {
            var vehicles = await _context.OwnVehiclesDetails.Where(e => e.VehicleRegNumber != null & e.VehicleRegNumber.ToLower() == registrationNumber.ToLower()).ToListAsync();

            if (vehicles.Count > 0)
                return true;
            else
                return false;
        }

        public async Task<OwnVehiclesDetails> CreateVehicle(OwnVehiclesDetails vehicle)
        {
            try
            {
                _context.OwnVehiclesDetails.Add(vehicle);
                await _context.SaveChangesAsync();
                return vehicle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<OwnVehicleDocuments>> GetDocument(int vehicleId)
        {
            try
            {
                return await _context.OwnVehicleDocuments.Where(e => e.OwnVehicleID == vehicleId).ToListAsync();
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<OwnVehicleDocuments> GetDocumentByDocId(string docUploadNo)
        {
            try
            {
                return await _context.OwnVehicleDocuments.Where(e => e.DocUploadNo == docUploadNo).FirstOrDefaultAsync();
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<OwnVehiclesDetails> GetVehicleById(int vehicleId)
        {
            try
            {
                return await _context.OwnVehiclesDetails.Where(e => e.OwnVehicleID == vehicleId).FirstOrDefaultAsync();
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<List<OwnVehiclesDetails>> GetVehicleByRegistrationNumber(string registrationNumber)
        {
            try
            {
                return await _context.OwnVehiclesDetails.Where(e => e.VehicleRegNumber.StartsWith(registrationNumber)).ToListAsync();
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task SaveDocument(OwnVehicleDocuments document)
        {
            _context.OwnVehicleDocuments.Add(document);
            await _context.SaveChangesAsync();
        }

        public async Task<OwnVehiclesDetails> UpdateVehicle(OwnVehiclesDetails vehicle)
        {
            try
            {
                var veh = await _context.OwnVehiclesDetails.Where(c => c.OwnVehicleID == vehicle.OwnVehicleID).FirstOrDefaultAsync();
                if (veh != null && vehicle != null)
                {
                    veh.EmpCreatedID = vehicle.EmpCreatedID;
                    veh.VehicleEntryDate = vehicle.VehicleEntryDate;
                    veh.VehicleType = vehicle.VehicleType;
                    veh.VehicleMake = vehicle.VehicleMake;
                    veh.VehicleDOP = vehicle.VehicleDOP;
                    veh.VehicleRegNumber = vehicle.VehicleRegNumber;
                    veh.VehicleChassisNo = vehicle.VehicleChassisNo;
                    veh.VehicleNosTyres = vehicle.VehicleNosTyres;
                    veh.VehicleAvgMileage = vehicle.VehicleAvgMileage;
                    veh.VehicleRenewalDuration = vehicle.VehicleRenewalDuration;
                    veh.VehicleMaxCapacity = vehicle.VehicleMaxCapacity;

                    _context.OwnVehiclesDetails.AddOrUpdate(vehicle);
                    await _context.SaveChangesAsync();
                    return vehicle;
                }

                return new OwnVehiclesDetails();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> CheckDuplicateVehicleChasisNo(string chasisNumber)
        {
            var vehicles = await _context.OwnVehiclesDetails.Where(e => e.VehicleChassisNo != null & e.VehicleChassisNo.ToLower() == chasisNumber.ToLower()).ToListAsync();

            if (vehicles.Count > 0)
                return true;
            else
                return false;
        }

        public async Task<List<OwnVehiclesDetails>> GetAllVehicles()
        {
            try
            {
                return await _context.OwnVehiclesDetails.ToListAsync();
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
