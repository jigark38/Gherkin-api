using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO.Reports.DialyGreensReceiving
{
    public class DialyGreensReceivingForBuyersDto
    {
        public string EmployeeName { get; set; }

        public DateTime EntryDate { get; set; }

        public string AreaId { get; set; }

        public string BuyerEmployeeId { get; set; }

        public int? GreensTransVehDesNo { get; set; }

        public int? GreensProcurementNo { get; set; }

        public string CropsNameCode { get; set; }

        public string PsNumber { get; set; }

        public string BuyingAsst1EmpId { get; set; }

        public string BuyingAsst2EmpId { get; set; }

        public string WeightMode { get; set; }

        public DateTime SelectedDate { get; set; }

        public TimeSpan? TimeOdDispatch { get; set; }

        public int? OwnVehicleId { get; set; }

        public int OrgOfficeNo { get; set; }

        public int? HiredVehicleId { get; set; }

        public DateTime? HarvestEndingTime { get; set; }

        public decimal? HarvestTimeDuration { get; set; }

        public DateTime SeasonFrom { get; set; }


        public DateTime SeasonTo { get; set; }

        public int? DriverId { get; set; }

        public string DriverName { get; set; }

    }
}