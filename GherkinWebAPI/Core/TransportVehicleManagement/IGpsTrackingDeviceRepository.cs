using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GherkinWebAPI.Models.TransportVehicleManagement;

namespace GherkinWebAPI.Core.TransportVehicleManagement
{
    public interface IGpsTrackingDeviceRepository
    {
        Task<GPSTrackingDevices> CreateDevice(GPSTrackingDevices device);
        Task<GPSTrackingDevices> UpdateDevice(GPSTrackingDevices device);
        Task<GPSTrackingDevices> GetDeviceById(int deviceId);
        Task<GPSTrackingDevices> GetDeviceByVehicleId(int vehicleId);
        Task<bool> CheckDuplicateGPSTrackingDeviceNo(string gpsTrackingDeviceNo);
        Task<GPSTrackingDevices> GetDeviceByHiredVehicleId(int hiredVehicleId);
    }
}
