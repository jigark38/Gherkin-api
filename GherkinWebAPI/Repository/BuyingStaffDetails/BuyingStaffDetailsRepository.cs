using AutoMapper;
using GherkinWebAPI.Core.BuyingStaffDetails;
using GherkinWebAPI.Models;
using GherkinWebAPI.Persistence;
using GherkinWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Repository.BuyingStaffDetails
{
    public class BuyingStaffDetailsRepository : RepositoryBase<HarvestAreaBuyingStaffDetails>, IBuyingStaffDetailsRepository
    {
        private RepositoryContext _context;
        public BuyingStaffDetailsRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            _context = repositoryContext;
        }

        public async Task<ApiResponse<object>> AddBuyingStaffDetails(List<HarvestAreaBuyingStaffDetails> harvestAreaBuyingStaffDetailsList)
        {
            ApiResponse<object> result = new ApiResponse<object>();

            try
            {
                var empid = harvestAreaBuyingStaffDetailsList[0].EmployeeID;

                var staffDetail = await _context.HarvestAreaBuyingStaffDetails.Where(a => a.EmployeeID == empid).ToArrayAsync();
                foreach (var item in harvestAreaBuyingStaffDetailsList)
                {
                    if (staffDetail.Any(e => e.AreaID == item.AreaID))
                    {
                        result.ErrorMessages = new List<string>();
                        result.ErrorMessages.Add("Some areas are already assigined to this employee");
                    }
                    else
                    {
                        _context.HarvestAreaBuyingStaffDetails.Add(item);
                    }
                }
                await _context.SaveChangesAsync();
                result.IsSucceed = true;
            }
            catch (Exception ex)
            {
                result.IsSucceed = false;
                result.Exception = ex;
                result.ErrorMessages = new List<string>();
                result.ErrorMessages.Add(ex.Message);
            }
            return result;
        }

        public async Task<ApiResponse<object>> UpdateBuyingStaffDetails(List<HarvestAreaBuyingStaffDetails> harvestAreaBuyingStaffDetailsList)
        {
            ApiResponse<object> result = new ApiResponse<object>();

            try
            {
                var empid = harvestAreaBuyingStaffDetailsList[0].EmployeeID;

                var staffDetail = await _context.HarvestAreaBuyingStaffDetails.Where(a => a.EmployeeID == empid).ToArrayAsync();
                foreach (var item in harvestAreaBuyingStaffDetailsList)
                {
                    if (staffDetail.Any(e => e.AreaID == item.AreaID))
                    {
                        result.ErrorMessages = new List<string>();
                        result.ErrorMessages.Add("Some areas are already assigined to this employee");
                    }
                    
                    else if(item.Id != 0)
                    {
                        var data = await _context.HarvestAreaBuyingStaffDetails.Where(x => x.Id == item.Id).FirstOrDefaultAsync();
                        data.BSEffectiveDate = item.BSEffectiveDate;
                        data.AreaID = item.AreaID;
                    }
                    
                }
                var newData = harvestAreaBuyingStaffDetailsList.Where(x => x.Id == 0);
                _context.HarvestAreaBuyingStaffDetails.AddRange(newData);

                await _context.SaveChangesAsync();
                result.IsSucceed = true;
            }
            catch (Exception ex)
            {
                result.IsSucceed = false;
                result.Exception = ex;
                result.ErrorMessages = new List<string>();
                result.ErrorMessages.Add(ex.Message);
            }
            return result;
        }

        public async Task<ApiResponse<List<HarvestAreaBuyingStaffDetails>>> getBuyingStaffDetailsByEmployee(string employeId)
        {
            ApiResponse<List<HarvestAreaBuyingStaffDetails>> result = new ApiResponse<List<HarvestAreaBuyingStaffDetails>>();
            try
            {
                var data = await _context.HarvestAreaBuyingStaffDetails.Where(a => a.EmployeeID == employeId).ToArrayAsync();
                List<HarvestAreaBuyingStaffDetails> HarvestAreaBuyingStaffDetailsList = new List<HarvestAreaBuyingStaffDetails>();

                if (data.Length > 0)
                {
                    foreach (var item in data)
                    {
                        HarvestAreaBuyingStaffDetails harvestAreaBuyingStaffDetails = new HarvestAreaBuyingStaffDetails()
                        {
                            Id = item.Id,
                            BSEntryDate = item.BSEntryDate,
                            BSEnteredEmpID = item.BSEnteredEmpID,
                            Employee_Status = item.Employee_Status,
                            EmployeeID = item.EmployeeID,
                            BSEffectiveDate = item.BSEffectiveDate,
                            AreaID = item.AreaID,
                        };
                        HarvestAreaBuyingStaffDetailsList.Add(harvestAreaBuyingStaffDetails);
                    }
                }
                result.Data = HarvestAreaBuyingStaffDetailsList;
                result.IsSucceed = true;
            }
            catch (Exception ex)
            {
                result.IsSucceed = false;
                result.Exception = ex;
                result.ErrorMessages = new List<string>();
                result.ErrorMessages.Add(ex.Message);
            }


            return result;
        }

        public async Task<ApiResponse<List<HarvestAreaBuyingStaffDetails>>> DeleteBuyingStaffDetailsByEmployee(string employeId, string areaId)
        {
            ApiResponse<List<HarvestAreaBuyingStaffDetails>> result = new ApiResponse<List<HarvestAreaBuyingStaffDetails>>();
            try
            {
                var data = await _context.HarvestAreaBuyingStaffDetails.Where(a => a.EmployeeID == employeId && a.AreaID == areaId).FirstOrDefaultAsync();
                if (data != null)
                {
                    _context.HarvestAreaBuyingStaffDetails.Remove(data);
                    await _context.SaveChangesAsync();
                    result.IsSucceed = true;
                }
            }
            catch (Exception ex)
            {
                result.IsSucceed = false;
                result.Exception = ex;
                result.ErrorMessages = new List<string>();
                result.ErrorMessages.Add(ex.Message);
            }


            return result;
        }
    }
}