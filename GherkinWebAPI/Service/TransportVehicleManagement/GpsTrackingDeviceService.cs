using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GherkinWebAPI.Core;
using GherkinWebAPI.Core.TransportVehicleManagement;
using GherkinWebAPI.Models.TransportVehicleManagement;

namespace GherkinWebAPI.Service.TransportVehicleManagement
{
    public class GpsTrackingDeviceService : IGpsTrackingDeviceService
    {
        private IRepositoryWrapper repositoryWrapper;

        public GpsTrackingDeviceService(IRepositoryWrapper _repositoryWrapper)
        {
            repositoryWrapper = _repositoryWrapper;
        }

        public async Task<GPSTrackingDevices> CreateDevice(GPSTrackingDevices device)
        {
            return await repositoryWrapper.GpsTrackingDeviceRepository.CreateDevice(device);
        }

        public async Task<GPSTrackingDevices> GetDeviceById(int deviceId)
        {
            return await repositoryWrapper.GpsTrackingDeviceRepository.GetDeviceById(deviceId);
        }

        public async Task<GPSTrackingDevices> GetDeviceByVehicleId(int vehicleId)
        {
            return await repositoryWrapper.GpsTrackingDeviceRepository.GetDeviceByVehicleId(vehicleId);
        }

        public async Task<GPSTrackingDevices> GetDeviceByHiredVehicleId(int hiredVehicleId)
        {
            return await repositoryWrapper.GpsTrackingDeviceRepository.GetDeviceByHiredVehicleId(hiredVehicleId);
        }

        public async Task<GPSTrackingDevices> UpdateDevice(GPSTrackingDevices device)
        {
            return await repositoryWrapper.GpsTrackingDeviceRepository.UpdateDevice(device);
        }
        public async Task<bool> CheckDuplicateGPSTrackingDeviceNo(string gpsTrackingDeviceNo)
        {
            return await repositoryWrapper.GpsTrackingDeviceRepository.CheckDuplicateGPSTrackingDeviceNo(gpsTrackingDeviceNo);
        }

    }
}