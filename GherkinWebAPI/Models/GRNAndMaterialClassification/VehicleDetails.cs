using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.GRNAndMaterialClassification
{
    public class VehicleDetails
    {
        public DateTime entryDate { get; set; }
        public string areaID { get; set; }
        public string buyerEmpID { get; set; }
        public int? greensTransVehicleDespNo { get; set; }
        public int? ownVehicleID { get; set; }
        public int? hiredVehicleID { get; set; }
        public string vehicleReading { get; set; }
        public TimeSpan timeOfDespatch { get; set; }
        public string driverName { get; set; }
        public string vehicleRegNo { get; set; }
    }
}