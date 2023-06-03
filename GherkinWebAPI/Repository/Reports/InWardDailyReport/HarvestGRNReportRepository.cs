using GherkinWebAPI.Core.Reports.InWardDailyReport;
using GherkinWebAPI.DTO;
using GherkinWebAPI.DTO.Reports.InWardDailyReport;
using GherkinWebAPI.Persistence;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Repository.Reports.InWardDailyReport
{
    public class HarvestGRNReportRepository : IHarvestGRNReportRepository
    {

        private RepositoryContext _context;
        public HarvestGRNReportRepository(RepositoryContext repositoryContext)
        {
            _context = repositoryContext;
        }


        public async Task<object> GetReportData(InwardDetailRequest request)
        {
            List<InWardDetailsReportData> firstData = new List<InWardDetailsReportData>();
            var areaId = request.AreaId;
            var date = request.SelectedDate;
            var ownAgentAll = "Own";

            List<string> columnsName = new List<string>();
            columnsName.Add("Sl.No");
            columnsName.Add("Date");
            columnsName.Add("No");
            //columnsName.Add("GRN/PROCUREMENT");
            //columnsName.Add("Crop Name");
            columnsName.Add("Received From");
            columnsName.Add("Area Name");
            columnsName.Add("Vehicle No");

            if (request.OwnAgent == 1)
            {
                List<GreensReceptionDetailsDTO> receptionDetailsDTOs = new List<GreensReceptionDetailsDTO>();
                List<GreensReceptionDetailsDTO> _greenReceptionDetails = new List<GreensReceptionDetailsDTO>();
                var _greenReceptionHarvestDetails = await (from _harvestGRN in _context.HarvestGRNInwardDetails
                                                           join _harvestGrnInwardDetails in _context.HarvestGRNInwardMaterialDetails on _harvestGRN.Unit_HM_Inward_No equals _harvestGrnInwardDetails.Unit_HM_Inward_No
                                                           //join _greensProcurements in _context.GreensProcurements on _harvestGRN.Greens_Procurement_No equals _greensProcurements.GreensProcurementNo
                                                           //join _vehicleSchedule in _context.GreensTransportVehicleSchedules on _greensProcurements.GreensTransVehicleDespNo equals _vehicleSchedule.GreensTransVehicleDespNo
                                                           //join _ownVehicles in _context.OwnVehiclesDetails on _vehicleSchedule.OwnVehicleID equals _ownVehicles.OwnVehicleID into ownDt
                                                           //from _ownVehicles in ownDt.DefaultIfEmpty()
                                                           //join _hiredVehicles in _context.Hired_Vehicle_Details on _vehicleSchedule.HiredVehicleID equals _hiredVehicles.HiredVehicleID into hiredDt
                                                           //from _hiredVehicles in hiredDt.DefaultIfEmpty()
                                                           join _materialgatePass in _context.MaterialInwardEntity on _harvestGRN.Inward_Gate_Pass_No equals _materialgatePass.Inward_Gate_Pass_No
                                                           join _cropName in _context.Crops on _harvestGrnInwardDetails.Crop_Name_Code equals _cropName.CropCode //into crop
                                                                                                                                                                 //from _cropName in crop.DefaultIfEmpty()
                                                           join _cropScheme in _context.CropSchemes on _harvestGrnInwardDetails.Crop_Scheme_Code equals _cropScheme.Code into cropScheme
                                                           from _cropScheme in cropScheme.DefaultIfEmpty()
                                                           join _area in _context.Areas on _harvestGRN.Area_ID equals _area.Area_ID into area
                                                           from _area in area.DefaultIfEmpty()
                                                           where _harvestGRN.Area_ID == areaId

                                                           select new GreensReceptionDetailsDTO
                                                           {
                                                               OrgID = _harvestGRN.Org_office_No,
                                                               HarvestGRNNo = _harvestGRN.Harvest_GRN_No,
                                                               GreenProcurementNo = null,
                                                               AreaId = _harvestGRN.Area_ID,
                                                               InwardDateTime = _materialgatePass.Inward_Date_Time,
                                                               Area = _area.Area_Name,
                                                               CropGroupCode = _harvestGrnInwardDetails.Crop_Group_Code,
                                                               CropCode = _cropName.CropCode,
                                                               HarvestProcurementNumber = _harvestGRN.Greens_Procurement_No,
                                                               // VehicleNo = _vehicleSchedule.OwnVehicleID != null ? _ownVehicles.VehicleRegNumber : _hiredVehicles.VehicleRegNumber,
                                                               CropName = _cropName.Name,
                                                               CropSchemeCode = _harvestGrnInwardDetails.Crop_Scheme_Code,
                                                               CropSchemeFrom = _cropScheme.From,
                                                               CropSchemeSign = _cropScheme.Sign,
                                                               CropCountmm = _cropScheme.Count,
                                                               NoofCrates = _harvestGrnInwardDetails.No_of_Crates,
                                                               Grade_wise_Total_Quantity = _harvestGrnInwardDetails.Grade_wise_Total_Quantity,
                                                               HarvestGRNTotalQuantity = _harvestGRN.Harvest_GRN_Total_Quantity
                                                           }).ToListAsync();


                _greenReceptionDetails.AddRange(_greenReceptionHarvestDetails);
                var _greenReceptionProcurementDetails = await (from _greensProcurements in _context.GreensProcurements
                                                               join _greensQuantityCountWise in _context.GreensQuantityCountwiseDetails on _greensProcurements.GreensProcurementNo equals _greensQuantityCountWise.GreensProcurementNo
                                                               join _cropName in _context.Crops on _greensQuantityCountWise.CropNameCode equals _cropName.CropCode into crop
                                                               from _cropName in crop.DefaultIfEmpty()
                                                               join _cropScheme in _context.CropSchemes on _greensQuantityCountWise.CropSchemeCode equals _cropScheme.Code into cropScheme
                                                               from _cropScheme in cropScheme.DefaultIfEmpty()
                                                               join _area in _context.Areas on _greensProcurements.AreaID equals _area.Area_ID into area
                                                               from _area in area.DefaultIfEmpty()
                                                               join _greenTransportVehicleSchedule in _context.GreensTransportVehicleSchedules on _greensProcurements.GreensTransVehicleDespNo equals _greenTransportVehicleSchedule.GreensTransVehicleDespNo into vehicles

                                                               from _greenTransportVehicleSchedule in vehicles.DefaultIfEmpty()
                                                               join _employee in _context.Employees on _greenTransportVehicleSchedule.BuyerEmpId equals _employee.employeeId
                                                               join _ownVehicles in _context.OwnVehiclesDetails on _greenTransportVehicleSchedule.OwnVehicleID equals _ownVehicles.OwnVehicleID into ownDt
                                                               from _ownVehicles in ownDt.DefaultIfEmpty()
                                                               join _hiredVehicles in _context.Hired_Vehicle_Details on _greenTransportVehicleSchedule.HiredVehicleID equals _hiredVehicles.HiredVehicleID into hiredDt
                                                               from _hiredVehicles in hiredDt.DefaultIfEmpty()
                                                               where _greensProcurements.HarvestDate == date && _greensProcurements.AreaID == areaId

                                                               select new GreensReceptionDetailsDTO
                                                               {
                                                                   OrgID = _greensProcurements.OrgofficeNo,
                                                                   HarvestGRNDate = _greensProcurements.HarvestDate,
                                                                   HarvestGRNNo = 0,
                                                                   GreenProcurementNo = _greensProcurements.GreensProcurementNo,
                                                                   AreaId = _greensProcurements.AreaID,
                                                                   Area = _area.Area_Name,
                                                                   VehicleNo = _greenTransportVehicleSchedule.OwnVehicleID != null ? _ownVehicles.VehicleRegNumber : _hiredVehicles.VehicleRegNumber,
                                                                   CropGroupCode = _greensQuantityCountWise.CropGroupCode,
                                                                   CropCode = _cropName.CropCode,
                                                                   CropName = _cropName.Name,
                                                                   BuyerEmployeeId = _greenTransportVehicleSchedule.BuyerEmpId,
                                                                   EmployeeName = _employee.employeeName,
                                                                   CropSchemeCode = _greensQuantityCountWise.CropSchemeCode,
                                                                   CropSchemeFrom = _cropScheme.From,
                                                                   CropSchemeSign = _cropScheme.Sign,
                                                                   NoofCrates = _greensQuantityCountWise.TotalNoOfCrates,
                                                                   Grade_wise_Total_Quantity = _greensQuantityCountWise.TotalFarmerHarvestQuantity,
                                                                   HarvestGRNTotalDespCrates = _greensProcurements.TripTotalCrates,
                                                                   VehicleStartingReading = _greenTransportVehicleSchedule.VehicleReading,
                                                                   VeichleGreenProcurementStartTime = _greenTransportVehicleSchedule.TimeofDespatch,
                                                                   HarvestGRNTotalQuantity = _greensProcurements.TripTotalQuantity
                                                               }).ToListAsync();
                _greenReceptionDetails.AddRange(_greenReceptionProcurementDetails);
                var distinctCropSchemeCodes = _greenReceptionDetails.GroupBy(x => new { x.CropSchemeCode }).Select(x => x.FirstOrDefault()).OrderByDescending(x => x.CropSchemeFrom).ThenByDescending(x => x.CropSchemeSign);

                foreach (var item in _greenReceptionDetails)
                {
                    if (item.HarvestGRNNo != 0)
                    {
                        if (receptionDetailsDTOs.Any(x => x.HarvestGRNNo == item.HarvestGRNNo))
                        {
                            var receptionDetail = receptionDetailsDTOs.Where(x => x.HarvestGRNNo == item.HarvestGRNNo).FirstOrDefault();
                            var grades = receptionDetail.Grades;
                            var grade = grades.Where(x => x.CropSchemeCode == item.CropSchemeCode).FirstOrDefault();
                            grade.NoofCrates += item.NoofCrates;
                            grade.FarmerWiseTotalQuantity += item.Grade_wise_Total_Quantity;
                            grade.CropGradeData = grade.FarmerWiseTotalQuantity + " / " + grade.NoofCrates;
                        }
                        else
                        {
                            List<GradeDTO> _grades = new List<GradeDTO>();
                            foreach (var schemeInfo in distinctCropSchemeCodes)
                            {
                                _grades.Add(new GradeDTO()
                                {
                                    NoofCrates = 0,
                                    FarmerWiseTotalQuantity = 0,
                                    CropGradeData = null,
                                    CropSchemeCode = schemeInfo.CropSchemeCode,
                                    CropSchemeFromSign = schemeInfo.CropSchemeFrom.ToString() + (schemeInfo.CropSchemeSign.ToString()) + " / " + (schemeInfo.CropCountmm.ToString())
                                });
                            }
                            item.Grades = _grades;
                            GreensReceptionDetailsDTO greensReceptionDetailsDTO = item;
                            var grade = item.Grades.Where(x => x.CropSchemeCode == item.CropSchemeCode).FirstOrDefault();
                            grade.NoofCrates += item.NoofCrates;
                            grade.FarmerWiseTotalQuantity += item.Grade_wise_Total_Quantity;
                            grade.CropGradeData = grade.FarmerWiseTotalQuantity + " / " + grade.NoofCrates;
                            receptionDetailsDTOs.Add(greensReceptionDetailsDTO);
                        }
                    }
                    else if (item.GreenProcurementNo != null)
                    {
                        if (receptionDetailsDTOs.Any(x => x.GreenProcurementNo == item.GreenProcurementNo))
                        {
                            var receptionDetail = receptionDetailsDTOs.Where(x => x.GreenProcurementNo == item.GreenProcurementNo).FirstOrDefault();
                            var grades = receptionDetail.Grades;
                            var grade = grades.Where(x => x.CropSchemeCode == item.CropSchemeCode).FirstOrDefault();
                            grade.NoofCrates += item.NoofCrates;
                            grade.FarmerWiseTotalQuantity += item.Grade_wise_Total_Quantity;
                            grade.CropGradeData = grade.FarmerWiseTotalQuantity + " / " + grade.NoofCrates;
                        }
                        else
                        {
                            List<GradeDTO> _grades = new List<GradeDTO>();
                            foreach (var schemeInfo in distinctCropSchemeCodes)
                            {
                                _grades.Add(new GradeDTO()
                                {
                                    NoofCrates = 0,
                                    FarmerWiseTotalQuantity = 0,
                                    CropGradeData = null,
                                    CropSchemeCode = schemeInfo.CropSchemeCode,
                                    CropSchemeFromSign = schemeInfo.CropSchemeFrom + schemeInfo.CropSchemeSign + " / " + schemeInfo.CropCountmm
                                });
                            }
                            item.Grades = _grades;
                            GreensReceptionDetailsDTO greensReceptionDetailsDTO = item;
                            var grade = item.Grades.Where(x => x.CropSchemeCode == item.CropSchemeCode).FirstOrDefault();
                            grade.NoofCrates += item.NoofCrates;
                            grade.FarmerWiseTotalQuantity += item.Grade_wise_Total_Quantity;
                            grade.CropGradeData = grade.FarmerWiseTotalQuantity + " / " + grade.NoofCrates;
                            receptionDetailsDTOs.Add(greensReceptionDetailsDTO);
                        }
                    }
                }
                var result = new Object();

                if (receptionDetailsDTOs?.Any() ?? false)
                {
                    var finalData = receptionDetailsDTOs.OrderBy(o => o.HarvestGRNDate).ThenBy(o2 => o2.HarvestGRNNo).ToList();

                    var grade1 = finalData[0].Grades;
                    foreach (var col in grade1)
                    {
                        columnsName.Add(col.CropSchemeFromSign);
                    }
                    columnsName.Add("Total");
                    columnsName.Add("Crates/Bags");
                    List<List<string>> data = new List<List<string>>();
                    int count = 1;

                    decimal? countOfCrates = 0;
                    decimal? countOfTotalQuantity = 0;
                    foreach (var reportData in receptionDetailsDTOs)
                    {
                        List<string> prepareData = new List<string>();
                        prepareData.Add(count.ToString());
                        decimal? noOfCrates = reportData.HarvestGRNTotalQuantity;
                        decimal? totalQuantity = reportData.HarvestGRNTotalQuantity;
                        string vehicleNumber = reportData.VehicleNo;
                        // prepareData.Add(reportData.InwardDateTime == null ? "NA" : reportData.InwardDateTime.ToString("dd-MM-yyyy"));
                        if (reportData.HarvestGRNNo != null && reportData.HarvestGRNNo > 0)
                        {
                            prepareData.Add(reportData.InwardDateTime.ToString("dd-MM-yyyy"));
                            prepareData.Add(reportData.HarvestGRNNo.ToString());
                            // prepareData.Add("GRN");
                            var grnData = await _context.HarvestGRNInwardMaterialDetails.Where(x => x.Harvest_GRN_No == reportData.HarvestGRNNo).ToListAsync();
                            noOfCrates = grnData.Sum(x => x.No_of_Crates);
                            totalQuantity = grnData.Sum(x => x.Grade_wise_Total_Quantity);
                            countOfCrates = countOfCrates + noOfCrates;
                            countOfTotalQuantity = countOfTotalQuantity + totalQuantity;
                            if (reportData.HarvestProcurementNumber != null && reportData.HarvestProcurementNumber > 0)
                            {
                                var fetchVehicleNumber = (from aa in _context.GreensProcurements
                                                          join bb in _context.GreensTransportVehicleSchedules on aa.GreensTransVehicleDespNo equals bb.GreensTransVehicleDespNo
                                                          join _ownVehicles in _context.OwnVehiclesDetails on bb.OwnVehicleID equals _ownVehicles.OwnVehicleID into ownDt
                                                          from _ownVehicles in ownDt.DefaultIfEmpty()
                                                          join _hiredVehicles in _context.Hired_Vehicle_Details on bb.HiredVehicleID equals _hiredVehicles.HiredVehicleID into hiredDt
                                                          from _hiredVehicles in hiredDt.DefaultIfEmpty()
                                                          where aa.GreensProcurementNo == reportData.HarvestProcurementNumber
                                                          select new GreensReceptionDetailsDTO
                                                          {
                                                              VehicleNo = bb.OwnVehicleID != null ? _ownVehicles.VehicleRegNumber : _hiredVehicles.VehicleRegNumber,
                                                          }).SingleOrDefault();

                                vehicleNumber = fetchVehicleNumber.VehicleNo;
                            }


                        }
                        else
                        {
                            prepareData.Add(date.ToString("dd-MM-yyyy"));
                            prepareData.Add(reportData.GreenProcurementNo.ToString());
                            //prepareData.Add("Procurement");
                            var procurementDetails = await _context.GreensQuantityCountwiseDetails.Where(x => x.GreensProcurementNo == reportData.GreenProcurementNo).ToListAsync();
                            noOfCrates = procurementDetails.Sum(x => x.TotalNoOfCrates);
                            totalQuantity = procurementDetails.Sum(x => x.TotalFarmerHarvestQuantity);
                            countOfCrates = countOfCrates + noOfCrates;
                            countOfTotalQuantity = countOfTotalQuantity + totalQuantity;

                        }
                        // prepareData.Add(reportData.CropName);
                        prepareData.Add(reportData.EmployeeName ?? "Centre Point Buying");
                        prepareData.Add(reportData.Area);
                        prepareData.Add(vehicleNumber ?? "");
                        foreach (var gradesValues in reportData.Grades)
                        {
                            prepareData.Add(gradesValues.CropGradeData);
                        }
                        prepareData.Add(totalQuantity.ToString() ?? "0");
                        prepareData.Add(noOfCrates.ToString() ?? "0");
                        data.Add(prepareData);
                        count++;
                    }
                    var resultFinal = new
                    {
                        ColumnsName = columnsName,
                        GridData = data,
                        SumOfCrates = countOfCrates,
                        SumOfTotalQuantity = countOfTotalQuantity,
                        TotalRows = count - 1,
                        SelectedDate = date.ToString("dd-MM-yyyy"),
                        AreaName = receptionDetailsDTOs[0].Area,
                        SelectedLabel = "OWN",
                        OrganisationName = _context.Organisations.FirstOrDefault().Organisation_Name
                    };
                    return resultFinal;
                }
            }



            if (request.OwnAgent == 2)
            {
                // var organisation = await _context.OrganisationOfficeLocationDetails.SingleOrDefaultAsync();
                var data = await (from _Greens_Agent_Received_Details in _context.GreensAgentReceivedDetails
                                      //join _Greens_Agent_Desp_Count_Weight_Details in _context.GreensAgentDespCountWeightDetails
                                      //on _Greens_Agent_Received_Details.GreensAgentGRNNo equals _Greens_Agent_Desp_Count_Weight_Details.GreensAgentGRNNo
                                  join _Material_Inward_Gate_Pass in _context.MaterialInwardEntity
                                  on _Greens_Agent_Received_Details.InwardGatePassNo equals _Material_Inward_Gate_Pass.Inward_Gate_Pass_No
                                  join _Greens_Agent_Grades_Actual_Details in _context.GreensAgentGradesActualDetails
                                  on _Greens_Agent_Received_Details.GreensAgentGRNNo equals _Greens_Agent_Grades_Actual_Details.GreensAgentGRNNo
                                  join _Supplier_Information_Details in _context.SupplierInformationDetails
                                  on _Greens_Agent_Received_Details.AgentOrgID equals _Supplier_Information_Details.AgentOrgID
                                  join _place in _context.Places on _Supplier_Information_Details.placeCode equals _place.PlaceCode
                                  join _cropName in _context.Crops on _Greens_Agent_Grades_Actual_Details.CropNameCode equals _cropName.CropCode into crop
                                  from _cropName in crop.DefaultIfEmpty()
                                  join _cropScheme in _context.CropSchemes on _Greens_Agent_Grades_Actual_Details.CropSchemeCode equals _cropScheme.Code into cropScheme
                                  from _cropScheme in cropScheme.DefaultIfEmpty()
                                  where _Greens_Agent_Received_Details.GreensAgentGRNDateTime == date && _Greens_Agent_Received_Details.OrgOfficeNo == 1
                                  && _Material_Inward_Gate_Pass.Inward_Type == "Greens Agent Supplier"
                                  orderby _Greens_Agent_Received_Details.GreensAgentGRNDateTime descending
                                  select new GreensReceptionDetailsDTO
                                  {
                                      HarvestGRNDate = _Greens_Agent_Received_Details.GreensAgentGRNDateTime,
                                      GreenProcurementNo = _Greens_Agent_Received_Details.GreensAgentGRNNo,
                                      AgentOrganisationName = _Supplier_Information_Details.AgentOrganisationName,
                                      PlaceName = _place.PlaceName,
                                      VehicleNo = _Material_Inward_Gate_Pass.Inv_Vehicle_No,
                                      CropGroupCode = _Greens_Agent_Received_Details.CropGroupCode,
                                      Area = _place.PlaceName,
                                      CropCode = _cropName.CropCode,
                                      CropName = _cropName.Name,
                                      CropCountmm = _cropScheme.Count,
                                      CropSchemeCode = _Greens_Agent_Grades_Actual_Details.CropSchemeCode,
                                      CropSchemeFrom = _cropScheme.From,
                                      CropSchemeSign = _cropScheme.Sign,
                                      Count_Total_Weight = _Greens_Agent_Grades_Actual_Details.CountTotalWeight,
                                      NoofCrates = _Greens_Agent_Grades_Actual_Details.CountTotalCrates
                                  }).ToListAsync();

                if (data?.Any() ?? false)
                {
                    var distinctCropSchemeCodes2 = data.GroupBy(x => new { x.CropSchemeCode }).Select(x => x.FirstOrDefault()).OrderByDescending(x => x.CropSchemeFrom).ThenByDescending(x => x.CropSchemeSign);


                    List<GreensReceptionDetailsDTO> response = new List<GreensReceptionDetailsDTO>();
                    var finalList = data;
                    foreach (var item in finalList)
                    {

                        if (response.Any(x => x.GreenProcurementNo == item.GreenProcurementNo))
                        {
                            var receptionDetail = response.Where(x => x.GreenProcurementNo == item.GreenProcurementNo).FirstOrDefault();
                            var grades = receptionDetail.Grades;
                            var grade = grades.Where(x => x.CropSchemeCode == item.CropSchemeCode).FirstOrDefault();
                            grade.NoofCrates += item.NoofCrates;
                            grade.FarmerWiseTotalQuantity += item.Count_Total_Weight;
                            grade.CropGradeData = (grade.FarmerWiseTotalQuantity.ToString()) + " / " + (grade.NoofCrates.ToString());
                        }
                        else
                        {
                            List<GradeDTO> _grades = new List<GradeDTO>();
                            foreach (var uniqueData in distinctCropSchemeCodes2)
                            {
                                _grades.Add(new GradeDTO()
                                {
                                    NoofCrates = 0,
                                    FarmerWiseTotalQuantity = 0,
                                    CropGradeData = null,
                                    CropSchemeCode = uniqueData.CropSchemeCode,
                                    CropSchemeFromSign = (uniqueData.CropSchemeFrom.ToString()) + (uniqueData.CropSchemeSign.ToString()) + " / " + (uniqueData.CropCountmm.ToString())
                                }); ;
                            }

                            item.Grades = _grades;
                            GreensReceptionDetailsDTO greensReceptionDetailsDTO = item;
                            var grade = item.Grades.Where(x => x.CropSchemeCode == item.CropSchemeCode).FirstOrDefault();
                            grade.NoofCrates += item.NoofCrates;
                            grade.FarmerWiseTotalQuantity += item.Count_Total_Weight;
                            grade.CropGradeData = (grade.FarmerWiseTotalQuantity.ToString()) + " / " + (grade.NoofCrates.ToString());
                            response.Add(greensReceptionDetailsDTO);
                        }

                    }

                    var grade1 = finalList[0].Grades;
                    foreach (var col in grade1)
                    {
                        columnsName.Add(col.CropSchemeFromSign);
                    }
                    columnsName.Add("Total");
                    columnsName.Add("Crates/Bags");
                    List<List<string>> data2 = new List<List<string>>();
                    int count = 1;

                    decimal? countOfCrates = 0;
                    decimal? countOfTotalQuantity = 0;
                    foreach (var reportData in response)
                    {
                        int totalCrates = 0;
                        decimal totalCount = 0;
                        List<string> prepareData = new List<string>();
                        prepareData.Add(count.ToString());
                        prepareData.Add(date.ToString("dd-MM-yyyy"));
                        prepareData.Add(reportData.GreenProcurementNo.ToString());
                        prepareData.Add(reportData.AgentOrganisationName);
                        prepareData.Add(reportData.Area);
                        prepareData.Add(reportData.VehicleNo);

                        foreach (var gradesValues in reportData.Grades)
                        {
                            totalCrates = totalCrates + (gradesValues.NoofCrates ?? 0);
                            totalCount = totalCount + gradesValues.FarmerWiseTotalQuantity;
                            prepareData.Add(gradesValues.CropGradeData);
                        }
                        countOfCrates = countOfCrates + totalCrates;
                        countOfTotalQuantity = countOfTotalQuantity + totalCount;
                        prepareData.Add(totalCount.ToString());
                        prepareData.Add(totalCrates.ToString());
                        data2.Add(prepareData);
                        count++;
                    }
                    var resultFinal = new
                    {
                        ColumnsName = columnsName,
                        GridData = data2,
                        SumOfCrates = countOfCrates,
                        SumOfTotalQuantity = countOfTotalQuantity,
                        TotalRows = count - 1,
                        SelectedDate = date.ToString("dd-MM-yyyy"),
                        AreaName = response[0].Area,
                        SelectedLabel = "AGENT",
                        OrganisationName = _context.Organisations.FirstOrDefault().Organisation_Name
                    };
                    return resultFinal;
                }

            }

            return null;
        }
    }
}