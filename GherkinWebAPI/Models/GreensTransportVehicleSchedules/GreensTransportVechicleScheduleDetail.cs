using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GherkinWebAPI.Models.GreensTransportVehicleSchedules
{
    public class GreensTransportVechicleScheduleDetail
    {
        [JsonProperty("greensTransVehicleDespNo")]
        public int GreensTransVehicleDespNo { get; set; }
        [JsonProperty("dateofEntry")]
        public DateTime DateofEntry { get; set; }
        [JsonProperty("loginUserName")]
        public string LoginUserName { get; set; }
        [JsonProperty("orgOfficeNo")]
        public int OrgOfficeNo { get; set; }
        [JsonProperty("orgOfficeName")]
        public string OrgOfficeName { get; set; }
        [JsonProperty("areaId")]
        public string AreaId { get; set; }
        [JsonProperty("areaName")]
        public string AreaName { get; set; }
        [JsonProperty("buyerEmpId")]
        public string BuyerEmpId { get; set; }
        [JsonProperty("buyerEmpName")]
        public string BuyerEmpName { get; set; }
        [JsonProperty("returnableGatePassDate")]
        public DateTime ReturnableGatePassDate { get; set; }
        [JsonProperty("rgpNo")]
        public string RGPNo { get; set; }
        [JsonProperty("transporterName")]
        public string TransporterName { get; set; }
        [JsonProperty("vehicleNo")]
        public string VehicleNo { get; set; }
        [JsonProperty("hiredTransID")]
        public int? HiredTransID { get; set; }
        [JsonProperty("ownVehicleID")]
        public int? OwnVehicleID { get; set; }
        [JsonProperty("hiredVehicleID")]
        public int? HiredVehicleID { get; set; }
        [JsonProperty("driverID")]
        public int? DriverID { get; set; }
        [JsonProperty("driverName")]
        public string DriverName { get; set; }
        [JsonProperty("driverContactNo")]
        public string DriverContactNo { get; set; }
        [JsonProperty("startKMSReading")]
        public string StartKMSReading { get; set; }
        [JsonProperty("timeOfDespatch")]
        public TimeSpan TimeofDespatch { get; set; }
        [JsonProperty("remarks")]
        public string Remarks { get; set; }
        public List<GreenTransportMaterial> GreenTransportMaterials { get; set; }
    }

    public class GreenTransportMaterial
    {
        [JsonProperty("id")]
        public int ID { get; set; }
        [JsonProperty("materailGroup")]
        public string MaterailGroup { get; set; }
        [JsonProperty("materailGroupCode")]
        public string MaterailGroupCode { get; set; }
        [JsonProperty("materialName")]
        public string MaterialName { get; set; }
        [JsonProperty("materialNameCode")]
        public string MaterialNameCode { get; set; }
        [JsonProperty("descDetails")]
        public string DescDetails { get; set; }
        [JsonProperty("totalNo")]
        public int TotalNo { get; set; }
    }
}