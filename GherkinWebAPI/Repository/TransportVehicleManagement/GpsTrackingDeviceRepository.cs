using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GherkinWebAPI.Models.TransportVehicleManagement;
using GherkinWebAPI.Core.TransportVehicleManagement;
using GherkinWebAPI.Persistence;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using System.Data.Entity;

namespace GherkinWebAPI.Repository.TransportVehicleManagement
{
    public class GpsTrackingDeviceRepository : RepositoryBase<GPSTrackingDevices>, IGpsTrackingDeviceRepository
    {
        private RepositoryContext _context;
        public GpsTrackingDeviceRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            _context = repositoryContext;
        }

        public async Task<GPSTrackingDevices> CreateDevice(GPSTrackingDevices device)
        {
            try
            {
                _context.GPSTrackingDevices.Add(device);
                await _context.SaveChangesAsync();
                return device;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<GPSTrackingDevices> GetDeviceById(int deviceId)
        {
            try
            {
                return await _context.GPSTrackingDevices.Where(e => e.GPSTrackingDeviceID == deviceId).FirstOrDefaultAsync();
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<GPSTrackingDevices> GetDeviceByVehicleId(int vehicleId)
        {
            try
            {
                return await _context.GPSTrackingDevices.Where(e => e.OwnVehicleID == vehicleId).FirstOrDefaultAsync();
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<GPSTrackingDevices> GetDeviceByHiredVehicleId(int hiredVehicleId)
        {
            try
            {
                return await _context.GPSTrackingDevices.Where(e => e.HiredVehicleID == hiredVehicleId).FirstOrDefaultAsync();
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<GPSTrackingDevices> UpdateDevice(GPSTrackingDevices device)
        {
            try
            {
                var dev = await _context.GPSTrackingDevices.Where(c => c.GPSTrackingDeviceID == device.GPSTrackingDeviceID).FirstOrDefaultAsync();
                if (dev != null && dev != null)
                {
                    dev.GPSDeviceNo = device.GPSDeviceNo;
                    dev.OwnVehicleID = device.OwnVehicleID;
                    _context.GPSTrackingDevices.AddOrUpdate(device);
                    await _context.SaveChangesAsync();
                    return dev;
                }

                return new GPSTrackingDevices();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> CheckDuplicateGPSTrackingDeviceNo(string gpsTrackingDeviceNo)
        {

            var devices = await _context.GPSTrackingDevices.Where(e => e.GPSDeviceNo !=null && e.GPSDeviceNo.ToLower() == gpsTrackingDeviceNo.ToLower()).ToListAsync();

            if (devices.Count > 0)
                return true;
            else
                return false;
        }
    }
}