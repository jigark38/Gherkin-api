using AutoMapper;
using GherkinWebAPI.Core;
using GherkinWebAPI.Models;
using GherkinWebAPI.Persistence;
using GherkinWebAPI.Request;
using GherkinWebAPI.Response;
using GherkinWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;


namespace GherkinWebAPI.Repository
{
    public class AdvCashIssuedToFarmrRepository : RepositoryBase<AdvanceCashIssuedToFarmersModel>, IAdvCashIssuedToFarmrRepository
    {
        private RepositoryContext _context;
        public AdvCashIssuedToFarmrRepository(RepositoryContext repositoryContext)
           : base(repositoryContext)
        {
            _context = repositoryContext;
        }

        public async Task<ApiResponse<List<FieldStaffDetailsResponse>>> GetFieldSupervisorList(string areaId, DateTime aggrementDate)
        {
            ApiResponse<List<FieldStaffDetailsResponse>> result = new ApiResponse<List<FieldStaffDetailsResponse>>();
            try
            {
                var data = (from a in _context.FieldStaffDetails
                            where a.Area_ID == areaId && a.StaffType == "incharge" && a.EffectiveDate <= aggrementDate
                            select new FieldStaffDetailsResponse
                            {
                                FieldStaffID = a.FieldStaffID,
                                EffectiveDate = a.EffectiveDate,
                                StaffType = a.StaffType,
                                Employee_ID = a.Employee_ID,
                                EmployeeName = _context.Employees.Where(d => d.employeeId == a.Employee_ID).FirstOrDefault().employeeName
                            }).ToListAsync();
                result.Data = new List<FieldStaffDetailsResponse>();
                result.Data = await data;
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

        public async Task<ApiResponse<List<FieldStaffDetailsResponse>>> GetFieldStaffList(string areaId, DateTime aggrementDate)
        {
            ApiResponse<List<FieldStaffDetailsResponse>> result = new ApiResponse<List<FieldStaffDetailsResponse>>();
            try
            {
                var data = (from a in _context.FieldStaffDetails
                            where a.Area_ID == areaId && a.StaffType == "Field Staff" && a.EffectiveDate <= aggrementDate
                            select new FieldStaffDetailsResponse
                            {
                                FieldStaffID = a.FieldStaffID,
                                EffectiveDate = a.EffectiveDate,
                                StaffType = a.StaffType,
                                Employee_ID = a.Employee_ID,
                                EmployeeName = _context.Employees.Where(d => d.employeeId == a.Employee_ID).FirstOrDefault().employeeName
                            }).ToListAsync();
                result.Data = new List<FieldStaffDetailsResponse>();
                result.Data = await data;
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

        public async Task<ApiResponse<List<AdvCasIssuedToFarnrResponse>>> GetFarmerDetails(FarmerDetailsFilterModel farmerDetailsFilterModel)
        {
            ApiResponse<List<AdvCasIssuedToFarnrResponse>> result = new ApiResponse<List<AdvCasIssuedToFarnrResponse>>();
            try
            {
                //var farmerAggrementDetails = _context.FarmersAgreementDetails.Where(a => a.Area_ID == farmerDetailsFilterModel.AreaId
                //&& a.Crop_Group_Code == farmerDetailsFilterModel.CropGroupCode
                //&& a.Crop_Name_Code == farmerDetailsFilterModel.CropNameCode
                //&& a.PS_Number == farmerDetailsFilterModel.PSNumber
                //&& (a.Employee_ID == farmerDetailsFilterModel.SupervisorFarmerCode || a.Employee_ID == farmerDetailsFilterModel.FieldStaffFarmerCode));

                var farmersAgreements = await (from fa in _context.FarmersAgreementDetails
                                               join farmer in _context.Farmers on fa.Farmer_Code equals farmer.Farmer_Code into xj
                                               from x in xj.DefaultIfEmpty()
                                               where fa.Area_ID == farmerDetailsFilterModel.AreaId
                                                && fa.Crop_Group_Code == farmerDetailsFilterModel.CropGroupCode
                                                && fa.Crop_Name_Code == farmerDetailsFilterModel.CropNameCode
                                                && fa.PS_Number == farmerDetailsFilterModel.PSNumber
                                                && (fa.Employee_ID == farmerDetailsFilterModel.SupervisorFarmerCode || fa.Employee_ID == farmerDetailsFilterModel.FieldStaffFarmerCode)
                                               select new AdvCasIssuedToFarnrResponse
                                               {
                                                   AreaID = fa.Area_ID,
                                                   VillageCode = fa.Village_Code,
                                                   VillageName = _context.Villages.Where(x => x.Village_Code == fa.Village_Code).FirstOrDefault().Village_Name,
                                                   FarmerCode = fa.Farmer_Code,
                                                   FarmersAccountNo = fa.Farmers_Account_No,
                                                   FarmerAddress = x.Farmer_Address,
                                                   FarmerName = x.FarmerName,
                                                   mandalCode = x.Mandal_Code,

                                               }).ToListAsync();

                var advCashfarmer = await (from adCI in _context.AdvanceCashIssuedToFarmers
                                           join farmer in _context.Farmers on adCI.FarmerCode equals farmer.Farmer_Code into xj
                                           from x in xj.DefaultIfEmpty()
                                           where adCI.FarmerCode == x.Farmer_Code 
                                           && adCI.AreaID == farmerDetailsFilterModel.AreaId
                                           && (adCI.FieldSupervisorEmployeeID == farmerDetailsFilterModel.SupervisorFarmerCode || adCI.FieldStaffEmployeeID == farmerDetailsFilterModel.FieldStaffFarmerCode)
                                           select new AdvCasIssuedToFarnrResponse
                                           {
                                               ACEntryDate = adCI.ACEntryDate,
                                               ACEnteredEmployeeID = adCI.ACEnteredEmployeeID,
                                               ACIssuedNo = adCI.ACIssuedNo,
                                               ACIssuedDate = adCI.ACIssuedDate,
                                               AreaID = adCI.AreaID,
                                               FieldSupervisorEmployeeID = adCI.FieldSupervisorEmployeeID,
                                               FieldStaffEmployeeID = adCI.FieldStaffEmployeeID,
                                               FarmersAccountNo = adCI.FarmersAccountNo,
                                               FarmerCode = adCI.FarmerCode,
                                               AdvanceAmount = adCI.AdvanceAmount,
                                               VillageCode = x.Village_Code,
                                               VillageName = _context.Villages.Where(v => v.Village_Code == x.Village_Code).FirstOrDefault().Village_Name,
                                               FarmerName = x.FarmerName,
                                               FarmerAddress = x.Farmer_Address,
                                               mandalCode = x.Mandal_Code,
                                           }).ToListAsync();


                var finalCasIssuedToFarnrList = new List<AdvCasIssuedToFarnrResponse>();
                finalCasIssuedToFarnrList.AddRange(advCashfarmer);
                foreach (var item in farmersAgreements)
                {
                    if(!finalCasIssuedToFarnrList.Exists(x=>x.FarmerCode == item.FarmerCode)) {
                        var advCasIssuedToFarnrResponse = new AdvCasIssuedToFarnrResponse()
                        {
                            ACEntryDate = item.ACEntryDate,
                            ACEnteredEmployeeID = item.ACEnteredEmployeeID,
                            ACIssuedNo = item.ACIssuedNo,
                            ACIssuedDate = item.ACIssuedDate,
                            AreaID = item.AreaID,
                            FieldSupervisorEmployeeID = item.FieldSupervisorEmployeeID,
                            FieldStaffEmployeeID = item.FieldStaffEmployeeID,
                            FarmersAccountNo = item.FarmersAccountNo,
                            FarmerCode = item.FarmerCode,
                            AdvanceAmount = item.AdvanceAmount,
                            VillageCode = item.VillageCode,
                            VillageName = item.VillageName,
                            FarmerName = item.FarmerName,
                            FarmerAddress = item.FarmerAddress,
                            mandalCode = item.mandalCode
                        };
                        finalCasIssuedToFarnrList.Add(item);
                    }
                    
                }

                result.IsSucceed = true;
                result.Data = finalCasIssuedToFarnrList;
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

        public async Task<ApiResponse<object>> AddAdvanceCashToFarmer(List<AdvanceCashIssuedToFarmersModel> advanceCashIssuedToFarmersList)
        {
            ApiResponse<object> result = new ApiResponse<object>();
            try
            {
                foreach(var item in advanceCashIssuedToFarmersList)
                {
                    if(item.ACIssuedNo <= 0)
                    {
                        _context.AdvanceCashIssuedToFarmers.Add(item);
                    }
                    else
                    {
                        var advCash = _context.AdvanceCashIssuedToFarmers.Where(x => x.ACIssuedNo == item.ACIssuedNo).FirstOrDefault();
                        advCash.AdvanceAmount = item.AdvanceAmount;
                    }
                }
                //_context.AdvanceCashIssuedToFarmers.AddRange(advanceCashIssuedToFarmersList);
                await _context.SaveChangesAsync();

                result.IsSucceed = true;
            }
            catch(Exception ex)
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