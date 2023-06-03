using GherkinWebAPI.Core.Reports.DialyGreensReceiving;
using GherkinWebAPI.DTO.HarvestDetails;
using GherkinWebAPI.DTO.Reports.DialyGreensReceiving;
using GherkinWebAPI.Entities;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.DailyHarvestDetails;
using GherkinWebAPI.Models.TransportVehicleManagement;
using GherkinWebAPI.Persistence;
using GherkinWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Repository.Reports.DialyGreensReceiving
{
    public class DialyGreensReceivingRepository : IDialyGreensReceivingRepository
    {
        private readonly RepositoryContext _context;

        public DialyGreensReceivingRepository(RepositoryContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<List<DialyGreensReceivingForBuyersDto>>> GetBuyers(DateTime date, string areaCode)
        {
            ApiResponse<List<DialyGreensReceivingForBuyersDto>> result = new ApiResponse<List<DialyGreensReceivingForBuyersDto>>();
            try
            {
                result.Data = await (from g in _context.GreensTransportVehicleSchedules
                                     join gp in _context.GreensProcurements
                                     on g.GreensTransVehicleDespNo equals gp.GreensTransVehicleDespNo
                                     join gf in _context.GreensFarmersDetails on gp.GreensProcurementNo equals gf.GreensProcurementNo
                                     join e in _context.Employees on g.BuyerEmpId equals e.employeeId
                                     join ph in _context.PlantationSchedules on gf.PSNumber equals ph.PsNumber
                                     where gp.AreaID == areaCode && gp.HarvestDate == date
                                     select new DialyGreensReceivingForBuyersDto
                                     {
                                         EmployeeName = e.employeeName,
                                         EntryDate = g.EntryDate,
                                         AreaId = g.AreaId,
                                         BuyerEmployeeId = g.BuyerEmpId,
                                         GreensTransVehDesNo = g.GreensTransVehicleDespNo,
                                         TimeOdDispatch = g.TimeofDespatch,
                                         OwnVehicleId = g.OwnVehicleID,
                                         HiredVehicleId = g.HiredVehicleID,
                                         GreensProcurementNo = gp.GreensProcurementNo,
                                         CropsNameCode = gf.CropNameCode,
                                         OrgOfficeNo = g.OrgofficeNo,
                                         HarvestEndingTime = gp.HarvestEndingTime,
                                         HarvestTimeDuration = gp.HarvestTimeDuration,
                                         PsNumber = gf.PSNumber,
                                         BuyingAsst1EmpId = gp.BuyingAsst1EmployeeID.ToString(),
                                         BuyingAsst2EmpId = gp.BuyingAsst2EmployeeID.ToString(),
                                         SeasonFrom = ph.FromDate,
                                         SeasonTo = ph.ToDate,
                                         WeightMode = gp.WeighmentMode,
                                         DriverId = g.DriverID,
                                         DriverName = g.GCDriverName
                                     }).ToListAsync();

                result.IsSucceed = true;
            }
            catch (Exception e)
            {
                result.Exception = e;
                result.IsSucceed = false;
            }

            return result;
        }

        public async Task<ApiResponse<object>> GetDailyBuyerWiseReport(DialyGreensReceivingForBuyersDto data)
        {
            ApiResponse<object> result = new ApiResponse<object>();
            try
            {
                // var greensFarmerDetails = await _context.GreensFarmersDetails.Where(x => x.GreensProcurementNo == data.GreensProcurementNo)?.OrderBy(x => x.GreensFarmersEntryNo)?.ToListAsync();

                var greensFarmersDetails = await (from a in _context.GreensFarmersDetails
                                                  join e in _context.CropSchemes on a.CropSchemeCode equals e.Code
                                                  join b in _context.Farmers on a.FarmerCode equals b.Farmer_Code
                                                  join d in _context.FarmersAgreementDetails on new { p = a.FarmerCode, x = data.AreaId} equals new { p = d.Farmer_Code, x = d.Area_ID } into z
                                                  from d in z.Take(1)
                                                  where a.GreensProcurementNo == data.GreensProcurementNo
                                                  select new GreenFarmerDetailDto
                                                  {
                                                      GreensFarmersEntryNo = a.GreensFarmersEntryNo,
                                                      GreensProcurementNo = a.GreensProcurementNo,
                                                      FarmerCode = a.FarmerCode,
                                                      CropSchemeCode = a.CropSchemeCode,
                                                      CountwiseTotalCrates = a.CountwiseTotalCrates,
                                                      CountwiseTotalQuantity = a.CountwiseTotalQuantity,
                                                      LastHarvestStatus = a.LastHarvestStatus,
                                                      FarmerAccountNumber = d.Farmers_Account_No,
                                                      FarmerName = b.FarmerName,
                                                      CropSchemeInfo = e.From + " " + e.Sign + " / " + e.Count
                                                  }).ToListAsync();
                //var AllFarmersAgreementDetails = await _context.FarmersAgreementDetails.ToListAsync();
                ////Add areaid and psnumber for account number
                //greensFarmersDetails.ForEach(x =>
                //{
                //    x.FarmerAccountNumber = AllFarmersAgreementDetails.Where(y => y.Farmer_Code == x.FarmerCode && y.Area_ID == data.AreaId && y.PS_Number == data.PsNumber)?.FirstOrDefault()?.Farmers_Account_No;
                //});

                var CropSchemes = await _context.CropSchemes.Where(x => x.CropCode == data.CropsNameCode).ToListAsync();

                if (greensFarmersDetails?.Any() ?? false)
                {
                    Crop cropNameDetail = null;
                    OwnVehiclesDetails vehicleInfo = null;
                    HiredVehicleDetail vehicleInfo2 = null;
                    if (!string.IsNullOrEmpty(data.CropsNameCode))
                    {
                        cropNameDetail = await _context.Crops.Where(x => x.CropCode == data.CropsNameCode)?.FirstOrDefaultAsync();
                    }
                    if (data.OwnVehicleId != null && data.OwnVehicleId > 0)
                    {
                        vehicleInfo = await _context.OwnVehiclesDetails.Where(x => x.OwnVehicleID == data.OwnVehicleId)?.FirstOrDefaultAsync();
                    }
                    else
                    {
                        vehicleInfo2 = await _context.Hired_Vehicle_Details.Where(x => x.HiredVehicleID == data.HiredVehicleId)?.FirstOrDefaultAsync();
                    }
                    OrganisationOfficeLocationDetails organisationDetail = null;
                    if (data.OrgOfficeNo > 0)
                    {
                        organisationDetail = await _context.OrganisationOfficeLocationDetails.Where(x => x.Org_Office_No == data.OrgOfficeNo)?.FirstOrDefaultAsync();
                    }
                    Organisation org = await _context.Organisations.Where(x => x.Org_Code == organisationDetail.Org_Code).FirstOrDefaultAsync();
                    PlantationSchedule plantation = null;
                    if (!string.IsNullOrEmpty(data.PsNumber))
                    {
                        plantation = await _context.PlantationSchedules.Where(x => x.PsNumber == data.PsNumber)?.FirstOrDefaultAsync();
                    }
                    var farmerCodes = greensFarmersDetails.Select(x => x.FarmerCode).ToList();
                    var farmer = await _context.Farmers.Where(x => farmerCodes.Contains(x.Farmer_Code))?.ToListAsync();
                    List<Village> farmerVillage = null;

                    if (farmer?.Any() ?? false)
                    {
                        var villagesCode = farmer.Select(x => x.Village_Code).ToList();
                        farmerVillage = await _context.Villages.Where(x => villagesCode.Contains(x.Village_Code)).ToListAsync();
                    }
                    List<string> columnsName = new List<string>();
                    columnsName.Add("Sl.No");
                    columnsName.Add("Farmer Code");
                    columnsName.Add("A/c No");
                    columnsName.Add("Farmer Name");
                    columnsName.Add("Village");

                    foreach (var code in CropSchemes)
                    {
                        columnsName.Add(code.From.ToString() + ' ' + code.Sign + " / " + code.Count);
                    }

                    List<List<string>> gridData = new List<List<string>>();
                    int count = 1;
                    foreach (var details in greensFarmersDetails)
                    {
                        var colData = new List<string>();

                        colData.Add(count.ToString());
                        colData.Add(details.FarmerCode);
                        colData.Add(details.FarmerAccountNumber);
                        colData.Add(details.FarmerName);
                        var farmerData = farmer.Where(x => x.Farmer_Code == details.FarmerCode).FirstOrDefault();
                        colData.Add(farmerVillage.Where(x => x.Village_Code == farmerData.Village_Code)?.FirstOrDefault().Village_Name);
                        decimal sum = 0;
                        foreach (var code in CropSchemes)
                        {
                            var Value = getDataForCrop(details.FarmerCode, code.Code, greensFarmersDetails);
                            colData.Add(Value.ToString());
                            sum = sum + Value;
                        }
                        colData.Add(sum.ToString());
                        bool update = false;
                        for (int i = 0; i < gridData.Count; i++)
                        {
                            var gData = gridData[i];
                            if (gData[1] == farmerData.Farmer_Code)
                            {
                                colData[0] = gData[0];
                                update = true;
                                gridData[i] = colData;
                            }

                        }

                        if (gridData.Count == 0 || !update)
                        {
                            gridData.Add(colData);
                            count++;
                        }
                    }

                    columnsName.Add("Total");

                    List<List<string>> gridDataWithoutFarmerCode = new List<List<string>>();
                    columnsName.RemoveAt(1);
                    foreach (var dataForRemove in gridData)
                    {
                        dataForRemove.RemoveAt(1);
                        gridDataWithoutFarmerCode.Add(dataForRemove);
                    }




                    string helperNameFirst = "";
                    if (data.BuyingAsst1EmpId != null && data.BuyingAsst1EmpId.IntTryParseNotNull() > 0)
                    {
                        var employee1 = await _context.Employees.Where(x => x.employeeId == data.BuyingAsst1EmpId)?.FirstOrDefaultAsync();
                        helperNameFirst = employee1?.employeeName;
                    }
                    string helperNameSecond = "";
                    string driverName = "";
                    if (data.BuyingAsst2EmpId != null && data.BuyingAsst2EmpId.IntTryParseNotNull() > 0)
                    {
                        var employee2 = await _context.Employees.Where(x => x.employeeId == data.BuyingAsst2EmpId)?.FirstOrDefaultAsync();
                        helperNameSecond = employee2?.employeeName;
                    }

                    if (string.IsNullOrEmpty(data.DriverName))
                    {
                        var employee = await _context.DriverDetails.Where(x => x.DriverID == data.DriverId)?.FirstOrDefaultAsync();
                        if (employee != null)
                        {
                            var driver = await _context.Employees.Where(x => x.employeeId == employee.EmployeeID)?.FirstOrDefaultAsync();
                            driverName = driver.employeeName;
                        }
                    }
                    else
                    {
                        driverName = data.DriverName;
                    }
                    var resultData = new
                    {
                        ColumnsName = columnsName,
                        GridData = gridDataWithoutFarmerCode,
                        GreensFarmerDetailList = greensFarmersDetails,
                        CropNameDetail = cropNameDetail,
                        VehicleInfo = vehicleInfo,
                        VehicleInfo2 = vehicleInfo2,
                        DriverName = driverName,
                        Organisation = org,
                        OrganisationDetail = organisationDetail,
                        Plantation = plantation,
                        PlantationDate = plantation.FromDate.ToString("dd-MM-yyyy") + " / " + plantation.ToDate.ToString("dd-MM-yyyy"),
                        //Farmer = farmer,
                        //FarmerVillage = farmerVillage,
                        HelperNameFirst = helperNameFirst,
                        HelperNameSecond = helperNameSecond
                    };
                    result.Data = resultData;
                }

                result.IsSucceed = true;
            }
            catch (Exception e)
            {
                result.Exception = e;
                result.IsSucceed = false;
            }
            return result;
        }

        private decimal getDataForCrop(string farmerCode, string cropCode, List<GreenFarmerDetailDto> list)
        {
            decimal sum = 0;
            foreach (var detail in list)
            {
                if (detail.FarmerCode == farmerCode && detail.CropSchemeCode == cropCode)
                {
                    decimal num = detail.CountwiseTotalQuantity;
                    sum = sum + num;
                }
            }
            return sum;
        }
    }
}
