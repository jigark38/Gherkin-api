using AutoMapper.QueryableExtensions;
using GherkinWebAPI.Core.Reports.GreensReceiptsSummary;
using GherkinWebAPI.DTO;
using GherkinWebAPI.DTO.Reports.GreensReceiptsSummary;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.Employee;
using GherkinWebAPI.Persistence;
using GherkinWebAPI.Utilities;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Repository.Reports.GreensReceiptsSummary
{
    public class GreensReceiptsSummaryRepository : IGreensReceiptsSummaryRepository
    {

        private RepositoryContext _context;
        public GreensReceiptsSummaryRepository(RepositoryContext repositoryContext)
        {
            _context = repositoryContext;
        }

        public async Task<List<CropGroup>> GetMaterialGroupDllData()
        {
            var data = await (from g in _context.CropGroups
                              orderby g.Name
                              select g).ToListAsync();
            return data;
        }

        public async Task<List<CropName>> GetMaterialNameFromGroupDllData(string cropGroupCode)
        {
            var data = await (from g in _context.Crops
                              where g.CropGroupCode == cropGroupCode
                              select new CropName
                              {
                                  CropNameCode = g.CropCode,
                                  Name = g.Name
                              }).OrderBy(i => i.Name).Distinct().ToListAsync();
            return data;
        }

        public async Task<object> GetReportData(GreensReceiptsSummaryReportDataDto data)
        {

            ApiResponse<object> response = new ApiResponse<object>();
            try
            {
                var CropSchemes = await _context.CropSchemes.Where(x => x.CropCode == data.CropNameCode).ToListAsync();

                List<string> columnsName = new List<string>();
                columnsName.Add("Sl.No");
                columnsName.Add("Date & Time");
                columnsName.Add("Area Name");
                columnsName.Add("Buyer Name");

                foreach (var code in CropSchemes)
                {
                    columnsName.Add(code.From.ToString() + ' ' + code.Sign + " / " + code.Count);
                }

                columnsName.Add("Total");

                var ReportData = await (from a in _context.GreensProcurements
                                        join e in _context.GreensTransportVehicleSchedules on a.GreensTransVehicleDespNo equals e.GreensTransVehicleDespNo
                                        join c in _context.GreensQuantityCratewiseDetails on a.GreensProcurementNo equals c.GreensProcurementNo
                                        where c.PSNumber == data.PsNumber && c.CropNameCode == data.CropNameCode
                                        && a.HarvestDate >= data.FromDate && a.HarvestDate <= data.ToDate
                                        orderby a.HarvestEndingTime
                                        select new FirstDataFromGreenTransportAndGreensProcurements
                                        {
                                            PsNumber = c.PSNumber,
                                            HarvestDate = a.HarvestDate,
                                            GreenProcurementNo = a.GreensProcurementNo,
                                            GreenTransVehicleDespNo = a.GreensTransVehicleDespNo,
                                            AreaId = a.AreaID,
                                            WeightMentmodule = a.WeighmentMode,
                                            HarvestEndingTime = a.HarvestEndingTime,
                                            EmployeeId = e.BuyerEmpId
                                        }).ToListAsync();

                ReportData = ReportData.DistinctBy(x => x.GreenProcurementNo).ToList();

                var AreaDetails = new List<Area>();
                if (ReportData?.Any() ?? false)
                {
                    var areaId = ReportData.Select(x => x.AreaId).ToList();
                    AreaDetails = await _context.Areas.Where(x => areaId.Contains(x.Area_ID)).ToListAsync();
                }
                var employeeName = new List<Employee>();
                if (ReportData?.Any() ?? false)
                {
                    var employeeIds = ReportData.Select(x => x.EmployeeId.ToString()).ToList();
                    employeeName = await _context.Employees.Where(x => employeeIds.Contains(x.employeeId)).ToListAsync();
                }
                List<List<string>> actualData = new List<List<string>>();
                Dictionary<string, List<List<string>>> dictionary = new Dictionary<string, List<List<string>>>();


                if (ReportData?.Any() ?? false)
                {
                    int count = 1;

                    List<int> procurementDetail = ReportData.Select(x => x.GreenProcurementNo).ToList();

                    var groupByData2 = await (from a in _context.GreensQuantityCountwiseDetails
                                              where procurementDetail.Contains(a.GreensProcurementNo)
                                              group a by new { a.CropSchemeCode, a.GreensProcurementNo } into g
                                              select new CropGroupData
                                              {
                                                  CropSchemeCode = g.Select(x => x.CropSchemeCode).FirstOrDefault(),
                                                  TotalFarmerHarvestQuantity = g.Sum(x => x.TotalFarmerHarvestQuantity),
                                                  ProcurementNo = g.Select(x => x.GreensProcurementNo).FirstOrDefault()
                                              }).ToListAsync();


                    foreach (var dd in ReportData)
                    {
                        List<string> res = new List<string>();
                        res.Add(count.ToString());
                        res.Add(dd.HarvestEndingTime?.ToString("dd-MM-yyyy hh:mm"));
                        res.Add(AreaDetails.Where(x => x.Area_ID == dd.AreaId)?.FirstOrDefault()?.Area_Name);
                        res.Add(employeeName.Where(x => x.employeeId.Equals(dd.EmployeeId.ToString()))?.FirstOrDefault()?.employeeName);

                        //var groupByData = await (from a in _context.GreensQuantityCountwiseDetails
                        //                         where a.GreensProcurementNo == dd.GreenProcurementNo
                        //                         group a by a.CropSchemeCode into g
                        //                         select new CropGroupData
                        //                         {
                        //                             CropSchemeCode = g.Select(x => x.CropSchemeCode).FirstOrDefault(),
                        //                             TotalFarmerHarvestQuantity = g.Sum(x => x.TotalFarmerHarvestQuantity)
                        //                         }).ToListAsync();

                        var groupByData = groupByData2.Where(x => x.ProcurementNo == dd.GreenProcurementNo).ToList();
                        decimal sum = 0;
                        foreach (var code in CropSchemes)
                        {
                            var value = groupByData.Where(x => x.CropSchemeCode.Equals(code.Code)).FirstOrDefault();
                            if (value == null)
                            {
                                res.Add("0");
                            }
                            else
                            {
                                sum = sum + (value.TotalFarmerHarvestQuantity);
                                res.Add(value.TotalFarmerHarvestQuantity.ToString());
                            }
                        }

                        res.Add(sum.ToString());

                        if (dd.HarvestEndingTime == null)
                        {
                            if (dictionary.ContainsKey("null"))
                            {
                                dictionary["null"].Add(res);
                            }
                            else
                            {
                                dictionary["null"] = new List<List<string>>();
                                dictionary["null"].Add(res);
                            }
                        }
                        else
                        {
                            if (dictionary.ContainsKey(dd.HarvestEndingTime?.ToString("dd-MM-yyyy")))
                            {
                                dictionary[dd.HarvestEndingTime?.ToString("dd-MM-yyyy")].Add(res);
                            }
                            else
                            {
                                dictionary[dd.HarvestEndingTime?.ToString("dd-MM-yyyy")] = new List<List<string>>();
                                dictionary[dd.HarvestEndingTime?.ToString("dd-MM-yyyy")].Add(res);

                            }
                        }
                        count++;
                        // actualData.Add(res);
                    }

                    foreach (var formatData in dictionary)
                    {
                        var columnCount = columnsName.Count();
                        var length = columnCount - 5;
                        int countVal = length;
                        int va = 4;
                        List<string> list = new List<string>();
                        list.Add(" ");
                        list.Add(" ");
                        list.Add("Day Total");
                        list.Add(" ");
                        decimal? totalSum = 0;
                        while (countVal != 0)
                        {
                            var val = formatData.Value.Select(x => x[va]).ToList();
                            decimal? sum = 0;
                            foreach (var total in val)
                            {
                                sum = sum + decimal.Parse(total);
                            }
                            totalSum = totalSum + sum;
                            list.Add(sum.ToString());
                            va++;
                            countVal--;
                        }
                        list.Add(totalSum.ToString());
                        var finalList = formatData.Value.Select(x => x).ToList();
                        finalList.Add(list);
                        actualData.AddRange(finalList);
                    }

                    List<string> empty = new List<string>();
                    for (int i = 0; i < columnsName.Count; i++)
                    {
                        empty.Add(" ");
                    }
                    actualData.Add(empty);

                    decimal? totalSum2 = 0;
                    List<string> list2 = new List<string>();
                    list2.Add(" ");
                    list2.Add(" ");
                    list2.Add("Grand Total");
                    list2.Add(" ");
                    var columnCount2 = columnsName.Count();
                    var length2 = columnCount2 - 4;
                    int countVal2 = length2;
                    int va2 = 4;
                    while (countVal2 != 0)
                    {
                        var val = actualData.Where(x => x[0] != " ").Select(x => x[va2]).ToList();
                        decimal? sum = 0;
                        foreach (var total in val)
                        {
                            sum = sum + decimal.Parse(total);
                        }
                        //totalSum2 = totalSum2 + sum;
                        list2.Add(sum.ToString());
                        va2++;
                        countVal2--;
                    }
                    //list2.Add(totalSum2.ToString());

                    actualData.Add(list2);
                    var org = await _context.Organisations.FirstOrDefaultAsync();
                    var resultDatass = new
                    {
                        ColumnsName = columnsName,
                        GridData = actualData,
                        org = org.Organisation_Name
                    };
                    response.Data = resultDatass;
                }
                else
                {
                    response.Data = null;
                }

                response.IsSucceed = true;
            }
            catch (Exception e)
            {
                response.Exception = e;
                response.IsSucceed = false;
            }

            return response;
        }

        public async Task<List<PlantationSchedule>> GetSeasonFromToDllData()
        {
            var data = await (from g in _context.PlantationSchedules
                              orderby g.FromDate, g.ToDate
                              select g).ToListAsync();
            return data;
        }


        public class CropGroupData
        {
            public string CropSchemeCode { get; set; }

            public decimal TotalFarmerHarvestQuantity { get; set; }

            public int ProcurementNo { get; set; }
        }
    }
}