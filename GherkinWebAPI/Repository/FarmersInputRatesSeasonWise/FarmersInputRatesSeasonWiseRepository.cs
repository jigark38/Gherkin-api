using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using GherkinWebAPI.Core;
using System.Threading.Tasks;
using GherkinWebAPI.Persistence;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using GherkinWebAPI.Models.Employee;

namespace GherkinWebAPI.Repository
{
    public class FarmersInputRatesSeasonWiseRepository : RepositoryBase<FarmersInputsIssueRatesMaster>, IFarmersInputRatesSeasonWiseRepository
    {
        private RepositoryContext _context;
        public FarmersInputRatesSeasonWiseRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            _context = repositoryContext;
        }

        public async Task<List<Area>> GetFarmerInputAreaDetails(string PS_Number)
        {
            var farmArea = await _context.FarmersInputsAreaDetails.Where(e => e.PSNumber == PS_Number).ToListAsync();

            var Areas = farmArea.Select(e => e.AreaID).ToList();

            return await _context.Areas.Where(e => !Areas.Contains(e.Area_ID)).ToListAsync();

        }
        public async Task<List<RawMaterial>> GetMaterialDetails()
        {

            return await (from RD in _context.RawMaterialDetails
                          join RG in _context.RawMaterialGroupMaster on RD.Raw_Material_Group_Code equals RG.Raw_Material_Group_Code
                          orderby RD.Raw_Material_Group_Code, RD.Raw_Material_Details_Name
                          select new RawMaterial
                          {
                              rawMaterialGroupCode = RD.Raw_Material_Group_Code,
                              rawMaterialGroupName = RG.Raw_Material_Group,
                              rawMaterialDetailsName = RD.Raw_Material_Details_Name,
                              rawMaterialDetailsCode = RD.Raw_Material_Details_Code,
                              rawMaterialUOM = RD.Raw_Material_UOM
                          }).ToListAsync();
        }



        public async Task<FarmersInputRatesSeasonWise> CreateFarmersInputRatesSeasonWise(FarmersInputRatesSeasonWise farmersInputRatesSeasonWise)
        {
            try
            {
                var Areas = farmersInputRatesSeasonWise.FarmersInputsAreaDetails;
                var Materials = farmersInputRatesSeasonWise.FarmersInputsMaterialRates;

                FarmersInputsIssueRatesMaster farmersInputsIssueRatesMaster = new FarmersInputsIssueRatesMaster
                {
                    CountryCode = farmersInputRatesSeasonWise.countryCode,
                    CropGroupCode = farmersInputRatesSeasonWise.cropGroupCode,
                    CropNameCode = farmersInputRatesSeasonWise.cropNameCode,
                    CropRateApprovedByEmployeeID = farmersInputRatesSeasonWise.cropRateApprovedByEmployeeID,
                    CropRateEffectiveDate = farmersInputRatesSeasonWise.cropRateEffectiveDate,
                    FIRateEnteredByEmployeeID = farmersInputRatesSeasonWise.fiRateEnteredByEmployeeID,
                    FIRatesEntryDate = farmersInputRatesSeasonWise.fiRatesEntryDate,
                    PSNumber = farmersInputRatesSeasonWise.psNumber,
                    StateCode = farmersInputRatesSeasonWise.stateCode
                };

                _context.FarmersInputsIssueRatesMaster.Add(farmersInputsIssueRatesMaster);
                var result = await _context.SaveChangesAsync();

                foreach (var Area in Areas)
                {
                    FarmersInputsAreaDetail areaDetail = new FarmersInputsAreaDetail
                    {
                        AreaID = Area.AreaID,
                        FIRatePassingNo = farmersInputsIssueRatesMaster.FIRatePassingNo,
                        PSNumber = Area.PSNumber
                    };
                    _context.FarmersInputsAreaDetails.Add(areaDetail);
                }
                var result1 = await _context.SaveChangesAsync();
                foreach (var Material in Materials)
                {
                    FarmersInputsMaterialRate materialRates = new FarmersInputsMaterialRate
                    {
                        FarmerMaterialRate = Material.FarmerMaterialRate,
                        FIRatePassingNo = farmersInputsIssueRatesMaster.FIRatePassingNo,
                        RawMaterialDetailsCode = Material.RawMaterialDetailsCode,
                        RawMaterialGroupCode = Material.RawMaterialGroupCode,
                        RawMaterialUOM = Material.RawMaterialUOM
                    };
                    _context.FarmersInputsMaterialRates.Add(materialRates);
                }
                var result2 = await _context.SaveChangesAsync();



                if (result == 1)
                {
                    return farmersInputRatesSeasonWise;
                }
                return new FarmersInputRatesSeasonWise();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<FarmersInputRatesSeasonWise> UpdateFarmersInputRatesSeasonWise(FarmersInputRatesSeasonWise farmersInputRatesSeasonWise)
        {
            try
            {
                var Areas = farmersInputRatesSeasonWise.FarmersInputsAreaDetails;
                var Materials = farmersInputRatesSeasonWise.FarmersInputsMaterialRates;
                var firs = await _context.FarmersInputsIssueRatesMaster.Where(c => c.FIRatePassingNo == farmersInputRatesSeasonWise.fiRatePassingNo).FirstOrDefaultAsync();
                if (firs != null && farmersInputRatesSeasonWise != null)
                {
                    FarmersInputsIssueRatesMaster farmersInputsIssueRatesMaster = new FarmersInputsIssueRatesMaster
                    {
                        FIRatePassingNo = farmersInputRatesSeasonWise.fiRatePassingNo,
                        CountryCode = farmersInputRatesSeasonWise.countryCode,
                        CropGroupCode = farmersInputRatesSeasonWise.cropGroupCode,
                        CropNameCode = farmersInputRatesSeasonWise.cropNameCode,
                        CropRateApprovedByEmployeeID = farmersInputRatesSeasonWise.cropRateApprovedByEmployeeID,
                        CropRateEffectiveDate = farmersInputRatesSeasonWise.cropRateEffectiveDate,
                        FIRateEnteredByEmployeeID = farmersInputRatesSeasonWise.fiRateEnteredByEmployeeID,
                        FIRatesEntryDate = farmersInputRatesSeasonWise.fiRatesEntryDate,
                        PSNumber = farmersInputRatesSeasonWise.psNumber,
                        StateCode = farmersInputRatesSeasonWise.stateCode
                    };

                    _context.FarmersInputsIssueRatesMaster.AddOrUpdate(farmersInputsIssueRatesMaster);
                    var result = await _context.SaveChangesAsync();

                    var areaList = await _context.FarmersInputsAreaDetails.Where(c => c.FIRatePassingNo == farmersInputRatesSeasonWise.fiRatePassingNo).ToListAsync();
                    var materialList = await _context.FarmersInputsMaterialRates.Where(c => c.FIRatePassingNo == farmersInputRatesSeasonWise.fiRatePassingNo).ToListAsync();

                    var removeAreas = areaList.Where(e => !Areas.Any(x => x.AreaID == e.AreaID)).ToList();

                    foreach (var Area in Areas)
                    {
                        FarmersInputsAreaDetail areaDetail = new FarmersInputsAreaDetail
                        {
                            AreaID = Area.AreaID,
                            FIRatePassingNo = farmersInputsIssueRatesMaster.FIRatePassingNo,
                            PSNumber = Area.PSNumber
                        };
                        _context.FarmersInputsAreaDetails.AddOrUpdate(areaDetail);
                    }

                    foreach (var Area in removeAreas)
                    {
                        _context.FarmersInputsAreaDetails.Remove(Area);
                    }

                    var result1 = await _context.SaveChangesAsync();

                    foreach (var Material in Materials)
                    {
                        FarmersInputsMaterialRate materialRates = new FarmersInputsMaterialRate
                        {
                            MaterialRateID = Material.MaterialRateID,
                            FarmerMaterialRate = Material.FarmerMaterialRate,
                            FIRatePassingNo = farmersInputsIssueRatesMaster.FIRatePassingNo,
                            RawMaterialDetailsCode = Material.RawMaterialDetailsCode,
                            RawMaterialGroupCode = Material.RawMaterialGroupCode,
                            RawMaterialUOM = Material.RawMaterialUOM
                        };
                        _context.FarmersInputsMaterialRates.AddOrUpdate(materialRates);
                    }
                    var result2 = await _context.SaveChangesAsync();

                }
                return new FarmersInputRatesSeasonWise();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<State>> GetStatesbyCropSeason(string cropGroupCode, string cropNameCode, int countryCode, string PSNumber)
        {
            var AllStates = await Task.Run(() => ((from state in _context.States
                                                   where state.Country_Code == countryCode
                                                   select new { state.State_Code, state.State_Name, state.Country_Code })
                                       .AsEnumerable()
                                       .Select(c => new State
                                       {
                                           State_Code = c.State_Code,
                                           State_Name = c.State_Name,
                                           Country_Code = c.Country_Code
                                       }).ToList()));

            var existingStates = await Task.Run(() => ((from fiirs in _context.FarmersInputsIssueRatesMaster
                                                        where fiirs.PSNumber == PSNumber && fiirs.CropGroupCode == cropGroupCode && fiirs.CropNameCode == cropNameCode && fiirs.CountryCode == countryCode
                                                        select new { fiirs.StateCode })
                                       .AsEnumerable()
                                       .Select(c => new State
                                       {
                                           State_Code = c.StateCode
                                       }).ToList()));

            return AllStates.Where(e => !existingStates.Any(x => x.State_Code == e.State_Code)).ToList();

        }
        public async Task<List<State>> FindStatesbyCropSeason(string cropGroupCode, string cropNameCode, int countryCode, string PSNumber)
        {
            var AllStates = await Task.Run(() => ((from state in _context.States
                                                   where state.Country_Code == countryCode
                                                   select new { state.State_Code, state.State_Name, state.Country_Code })
                                       .AsEnumerable()
                                       .Select(c => new State
                                       {
                                           State_Code = c.State_Code,
                                           State_Name = c.State_Name,
                                           Country_Code = c.Country_Code
                                       }).ToList()));

            var existingStates = await Task.Run(() => ((from fiirs in _context.FarmersInputsIssueRatesMaster
                                                        where fiirs.PSNumber == PSNumber && fiirs.CropGroupCode == cropGroupCode && fiirs.CropNameCode == cropNameCode && fiirs.CountryCode == countryCode
                                                        select new { fiirs.StateCode })
                                       .AsEnumerable()
                                       .Select(c => new State
                                       {
                                           State_Code = c.StateCode
                                       }).ToList()));

            return AllStates.Where(e => existingStates.Any(x => x.State_Code == e.State_Code)).ToList();

        }

        public async Task<FarmersInputRatesSeasonWise> GetFarmerInputRateSeason(string cropGroupCode, string cropNameCode, int countryCode, string PSNumber, int stateCode)
        {
            FarmersInputRatesSeasonWise farmersInputRatesSeasonWise = new FarmersInputRatesSeasonWise();

            var farmersInputRates = await (from fiirs in _context.FarmersInputsIssueRatesMaster
                                           where fiirs.PSNumber == PSNumber && fiirs.CropGroupCode == cropGroupCode && fiirs.CropNameCode == cropNameCode && fiirs.CountryCode == countryCode && fiirs.StateCode == stateCode
                                           select new FarmersInputRatesSeasonWise
                                           {
                                               countryCode = fiirs.CountryCode,
                                               cropGroupCode = fiirs.CropGroupCode,
                                               cropNameCode = fiirs.CropNameCode,
                                               fiRatePassingNo = fiirs.FIRatePassingNo,
                                               stateCode = fiirs.StateCode,
                                               cropRateApprovedByEmployeeID = fiirs.CropRateApprovedByEmployeeID,
                                               cropRateEffectiveDate = fiirs.CropRateEffectiveDate,
                                               fiRateEnteredByEmployeeID = fiirs.FIRateEnteredByEmployeeID,
                                               fiRatesEntryDate = fiirs.FIRatesEntryDate,
                                               psNumber = fiirs.PSNumber
                                           }).FirstOrDefaultAsync();

            var farmersInputsAreaDetails = await _context.FarmersInputsAreaDetails.Where(c => c.FIRatePassingNo == farmersInputRates.fiRatePassingNo).ToListAsync();

            var farmersInputsMaterialRates = await _context.FarmersInputsMaterialRates.Where(c => c.FIRatePassingNo == farmersInputRates.fiRatePassingNo).ToListAsync();

            farmersInputRatesSeasonWise.countryCode = farmersInputRates.countryCode;
            farmersInputRatesSeasonWise.cropGroupCode = farmersInputRates.cropGroupCode;
            farmersInputRatesSeasonWise.cropNameCode = farmersInputRates.cropNameCode;
            farmersInputRatesSeasonWise.fiRatePassingNo = farmersInputRates.fiRatePassingNo;
            farmersInputRatesSeasonWise.stateCode = farmersInputRates.stateCode;
            farmersInputRatesSeasonWise.cropRateApprovedByEmployeeID = farmersInputRates.cropRateApprovedByEmployeeID;
            farmersInputRatesSeasonWise.cropRateEffectiveDate = farmersInputRates.cropRateEffectiveDate;
            farmersInputRatesSeasonWise.fiRateEnteredByEmployeeID = farmersInputRates.fiRateEnteredByEmployeeID;
            farmersInputRatesSeasonWise.fiRatesEntryDate = farmersInputRates.fiRatesEntryDate;
            farmersInputRatesSeasonWise.psNumber = farmersInputRates.psNumber;
            farmersInputRatesSeasonWise.FarmersInputsAreaDetails = farmersInputsAreaDetails;
            farmersInputRatesSeasonWise.FarmersInputsMaterialRates = farmersInputsMaterialRates;

            return farmersInputRatesSeasonWise;

        }

        public async Task<List<Employee>> GetEmployeesByDeptCode(string deptCode)
        {
            return await _context.Employees.Where(e => e.departmentCode == deptCode).OrderByDescending(e => e.empBiometricId).ToListAsync();
        }

    }
}