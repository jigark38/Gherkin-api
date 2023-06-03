using GherkinWebAPI.Core.GRNAndMaterialClassification;
using GherkinWebAPI.Models.GRNAndMaterialClassification;
using GherkinWebAPI.Persistence;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System;
using GherkinWebAPI.DTO.GRNAndMaterialClassification;

namespace GherkinWebAPI.Repository.GRNAndMaterialClassification
{
    public class HarvestGRNRepository : RepositoryBase<HarvestGRN>, IHarvestGRNRepository
    {
        private RepositoryContext _context;

        public HarvestGRNRepository(RepositoryContext context) : base(context)
        {
            _context = context;
        }
        // get your EF context
        public async Task<long> GetNextHarvestGRNNo()
        {
            var nextHarvestGRNSequenceQuery = this._context.Database.SqlQuery<long>("SELECT NEXT VALUE FOR Harvest_GRN_Details_Sequence;");
            var nextVal = await nextHarvestGRNSequenceQuery.SingleAsync();

            return nextVal;
        }
        public async Task<HarvestGRNMaterialDetail> CreateHarvestGRNDetail(HarvestGRNMaterialDetail materialDetail)
        {
            var harvestGRN = await _context.HarvestGRNs.SingleOrDefaultAsync(h => h.HarvestGRNNo == materialDetail.HarvestGRNNo);

            if (harvestGRN == null)
            {
                var harvestGRNDetail = new HarvestGRN
                {
                    HarvestGRNNo = materialDetail.HarvestGRNNo,
                    AreaID = materialDetail.AreaId,
                    HarvestGRNDate = materialDetail.HarvestGRNDate,
                    GreensTransVehicleDespNo = materialDetail.GreensTransVehicleDespNo,
                    DriverName = materialDetail.DriverName,
                    //DriverContactNo = materialDetail.DriverContactNo,
                    VehicleStartingReading = materialDetail.VechicalStartingReading,
                    VehicleStartTime = materialDetail.VehicalStartTime,
                    VehicleFreight = materialDetail.VehicalFreight,
                    HarvestGRNTotalQuantity = materialDetail.HarvestGRNTotalQuantity,
                    HarvestGRNTotalDespCrates = materialDetail.HarvestGRNTotalDespCrates,
                    HarverstGRNRemarks = materialDetail.HarvestGRNRemarks,
                    OrgOfficeNo = materialDetail.OrgOfficeNo,
                    EmployeeId = materialDetail.EmployeeId,
                    VehicleNo = materialDetail.VechicalNo,
                    DriverContactNo = materialDetail.DriverContactNo
                };

                _context.HarvestGRNs.Add(harvestGRNDetail);
            }
            else
                return new HarvestGRNMaterialDetail();

            var result = await _context.SaveChangesAsync();

            if (result == 1)
            {
                return materialDetail;
            }
            else
                return new HarvestGRNMaterialDetail();
        }

        public async Task<List<GreensReceivedDetail>> GetGreensReceivedDetailsAsync(string areaId)
        {
            /*var result = (from a in _context.HarvestProcurementDetails
                          join b in _context.HarvestFarmersDetails on a.HarvestProcurementNo equals b.HarvestProcurementNo
                          join c in _context.Farmers on b.FarmerCode equals c.Farmer_Code
                          join d in _context.FarmersAgreementDetails on new { x = a.AreaId, y = a.PsNumber } equals new { x = d.Area_ID, y = d.PS_Number }
                          join e in _context.Villages on c.Village_Code equals e.Village_Code
                          join f in _context.CropSchemes on b.CropSchemeCode equals f.Code
                          where a.AreaId == areaId
                          && !_context.HarvestGRNFarmers.Any(h => h.HarvestFarmersEntryNo == b.HarvestFarmersEntryNo && h.CropSchemeCode == b.CropSchemeCode)
                          orderby f.Sign descending
                          select new GreensReceivedDetail
                          {
                              AreaId = a.AreaId,
                              HarvestDate = a.HarvestDate.ToString().Substring(0, 10),
                              HarvestProcurementNo = a.HarvestProcurementNo,
                              CropNameCode = a.CropNameCode,
                              PSNumber = a.PsNumber,
                              HarvestFarmersEntryNo = b.HarvestFarmersEntryNo,
                              FarmerCode = b.FarmerCode,
                              CropSchemeCode = b.CropSchemeCode,
                              FarmerwiseTotalCrates = b.FarmerwiseTotalCrates,
                              FarmerwiseTotalQuantity = b.FarmerwiseTotalQuantity,
                              FarmerName = c.FarmerName,
                              VillageCode = c.Village_Code,
                              FarmersAgreementCode = d.Farmers_Agreement_Code,
                              FarmersAccountNo = d.Farmers_Account_No,
                              VillageName = e.Village_Name,
                              CropSchemeFrom = f.From,
                              CropSchemeSign = f.Sign
                          }).ToListAsync();*/
            var result = (from a in _context.GreensProcurements
                          join b in _context.GreensFarmersDetails on a.GreensProcurementNo equals b.GreensProcurementNo
                          join c in _context.Farmers on b.FarmerCode equals c.Farmer_Code
                          join d in _context.FarmersAgreementDetails on new { p = b.FarmerCode, x = a.AreaID, y = b.PSNumber } equals new { p = d.Farmer_Code, x = d.Area_ID, y = d.PS_Number } into z
                          from d in z.Take(1)
                          join e in _context.Villages on c.Village_Code equals e.Village_Code
                          join f in _context.CropSchemes on b.CropSchemeCode equals f.Code
                          where a.AreaID == areaId && a.VehicleEndPoint == "Tr. to Pooled Truck"
                          && !_context.HarvestGRNFarmers.Any(h => h.GreensFarmersEntryNo == b.GreensFarmersEntryNo && h.CropSchemeCode == b.CropSchemeCode)
                          orderby a.HarvestDate descending
                          select new GreensReceivedDetail
                          {
                              AreaId = a.AreaID,
                              HarvestDate = a.HarvestDate,
                              HarvestProcurementNo = a.GreensProcurementNo,
                              CropNameCode = a.CropNameCode,
                              PSNumber = a.PSNumber,
                              HarvestFarmersEntryNo = b.GreensFarmersEntryNo,
                              FarmerCode = b.FarmerCode,
                              CropSchemeCode = b.CropSchemeCode,
                              FarmerwiseTotalCrates = b.CountwiseTotalCrates,
                              FarmerwiseTotalQuantity = b.CountwiseTotalQuantity,
                              FarmerName = c.FarmerName,
                              VillageCode = c.Village_Code,
                              FarmersAgreementCode = d.Farmers_Agreement_Code,
                              FarmersAccountNo = d.Farmers_Account_No,
                              VillageName = e.Village_Name,
                              CropSchemeFrom = f.From,
                              CropSchemeSign = f.Sign
                          }).ToListAsync();

            return await result;
        }

        public async Task<long> GetHarvestGRNNo()
        {
            var harvestGRNSequenceQuery = this._context.Database.SqlQuery<long>("SELECT current_value FROM sys.sequences WHERE name = 'Harvest_GRN_Details_Sequence';");
            var currentVal = await harvestGRNSequenceQuery.SingleAsync();
            return currentVal + 1;
            /*var harvestGRNNo = await _context.HarvestGRNs.AsNoTracking().ToListAsync();

            if (harvestGRNNo.Count > 0)
            {
                return harvestGRNNo.Count + 1;
            }
            else
            {
                return 1;
            }*/
        }

        public async Task<GreensRecievedDetailDTO> GetGreensReceivedDetailsAsync(string areaId, string supervisorId)
        {
            GreensRecievedDetailDTO greensRecievedDetailDTO = new GreensRecievedDetailDTO();
            HarvestGRN HarvestDetails = await _context.HarvestGRNs.FirstOrDefaultAsync(x => x.AreaID == areaId && x.EmployeeId == supervisorId && x.LoadingCompleted == 0);
            if (HarvestDetails == null)
            {
                long NextHarvestGRNNo = await GetNextHarvestGRNNo();
                HarvestGRN harvestGRN = new HarvestGRN()
                {
                    HarvestGRNNo = NextHarvestGRNNo,
                    HarvestGRNDate = new DateTime().Date,
                    AreaID = areaId,
                    EmployeeId = supervisorId,
                    GreensTransVehicleDespNo = null,
                    VehicleNo = "",
                    DriverName = "",
                    DriverContactNo = 0,
                    VehicleStartingReading = 0,
                    VehicleStartTime = DateTime.Now,
                    VehicleFreight = 0,
                    HarvestGRNTotalQuantity = 0,
                    HarvestGRNTotalDespCrates = 0,
                    HarverstGRNRemarks = "",
                    OrgOfficeNo = 1,
                    LoadingCompleted = 0
                };
                HarvestDetails = _context.HarvestGRNs.Add(harvestGRN);
                await _context.SaveChangesAsync();
            }
            greensRecievedDetailDTO.HarvestGRNNo = HarvestDetails.HarvestGRNNo;
            var result = await (from a in _context.GreensProcurements
                                join gt in _context.GreensTransportVehicleSchedules on a.GreensTransVehicleDespNo equals gt.GreensTransVehicleDespNo
                                join em in _context.Employees on gt.BuyerEmpId equals em.employeeId
                                join b in _context.GreensFarmersDetails on a.GreensProcurementNo equals b.GreensProcurementNo
                                join c in _context.Farmers on b.FarmerCode equals c.Farmer_Code
                                join d in _context.FarmersAgreementDetails on new { p = b.FarmerCode, x = a.AreaID, y = b.PSNumber } equals new { p = d.Farmer_Code, x = d.Area_ID, y = d.PS_Number } into z
                                from d in z.Take(1)
                                join e in _context.Villages on c.Village_Code equals e.Village_Code
                                join f in _context.CropSchemes on b.CropSchemeCode equals f.Code
                                join _ownVehicles in _context.OwnVehiclesDetails on gt.OwnVehicleID equals _ownVehicles.OwnVehicleID into ownDt
                                from _ownVehicles in ownDt.DefaultIfEmpty()
                                join _hiredVehicles in _context.Hired_Vehicle_Details on gt.HiredVehicleID equals _hiredVehicles.HiredVehicleID into hiredDt
                                from _hiredVehicles in hiredDt.DefaultIfEmpty()
                                where a.AreaID == areaId && a.VehicleEndPoint == "Tr. to Pooled Truck"
                                && (!_context.CWHarvestGRNWeightSummaryDetails.Any(h => h.GreensProcurementNo == a.GreensProcurementNo)
                                && !_context.CWHarvestGRNCountWeightDetails.Any(y => y.GreensProcurementNo == a.GreensProcurementNo && y.HarvestGRNNo != HarvestDetails.HarvestGRNNo))
                                orderby a.HarvestDate descending
                                select new GreensReceivedDetail
                                {
                                    AreaId = a.AreaID,
                                    HarvestDate = a.HarvestDate,
                                    HarvestProcurementNo = a.GreensProcurementNo,
                                    CropNameCode = b.CropNameCode,
                                    PSNumber = a.PSNumber,
                                    HarvestFarmersEntryNo = b.GreensFarmersEntryNo,
                                    FarmerCode = b.FarmerCode,
                                    CropSchemeCode = b.CropSchemeCode,
                                    FarmerwiseTotalCrates = b.CountwiseTotalCrates,
                                    FarmerwiseTotalQuantity = b.CountwiseTotalQuantity,
                                    FarmerName = c.FarmerName,
                                    VillageCode = c.Village_Code,
                                    FarmersAgreementCode = d.Farmers_Agreement_Code,
                                    FarmersAccountNo = d.Farmers_Account_No,
                                    VillageName = e.Village_Name,
                                    CropSchemeFrom = f.From,
                                    CropSchemeSign = f.Sign,
                                    CropGroupCode = b.CropGroupCode,
                                    BuyerEmployeeID = gt.BuyerEmpId,
                                    BuyerEmployeeName = em.employeeName,
                                    VehicleNo = gt.OwnVehicleID != null ? _ownVehicles.VehicleRegNumber : _hiredVehicles.VehicleRegNumber,
                                }).ToListAsync();
            greensRecievedDetailDTO.greensReceivedDetails = result;

            var weightdetails = await (from a in _context.CWHarvestGRNCountWeightDetails
                                       join b in _context.Employees on a.BuyerEmployeeID equals b.employeeId
                                       join c in _context.CropSchemes on a.CropSchemeCode equals c.Code
                                       where a.HarvestGRNNo == HarvestDetails.HarvestGRNNo
                                       select new HarvestGRNCountWeightDetailsDTO
                                       {
                                           CWGreensCratewiseEntryNo = a.CWGreensCratewiseEntryNo,
                                           HarvestGRNNo = a.HarvestGRNNo,
                                           CropGroupCode = a.CropGroupCode,
                                           CropNameCode = a.CropNameCode,
                                           CropSchemeCode = a.CropSchemeCode,
                                           NoofCrates = a.NoofCrates,
                                           EachCrateWt = a.EachCrateWt,
                                           CrateNoFrom = a.CrateNoFrom,
                                           CrateNoTo = a.CrateNoTo,
                                           CWGrossWeight = a.CWGrossWeight,
                                           CWTareweight = a.CWTareweight,
                                           CWCrateswiseNetWeight = a.CWCrateswiseNetWeight,
                                           GreensProcurementNo = a.GreensProcurementNo,
                                           BuyerEmployeeID = a.BuyerEmployeeID,
                                           cropSchemeFrom = c.From,
                                           cropSchemeSign = c.Sign,
                                           cropCountInfo = c.From + " " + c.Sign,
                                           BuyerEmployeeName = b.employeeName
                                       }).ToListAsync();
            greensRecievedDetailDTO.weightDetails = weightdetails;
            return greensRecievedDetailDTO;
        }

        public async Task<CWHarvestGRNWeightSummaryDetails> CompleteBuyer(long HarvestGRNNo, int GreensProcurementNo, string BuyerEmployerId)
        {
            try
            {
                List<CWHarvestGRNCountWeightDetails> greenProcurementFarmerWiseCountWeightData = await _context.CWHarvestGRNCountWeightDetails.Where(x =>
                            x.GreensProcurementNo == GreensProcurementNo &&
                            x.HarvestGRNNo == HarvestGRNNo &&
                            x.BuyerEmployeeID == BuyerEmployerId).ToListAsync();
                if (greenProcurementFarmerWiseCountWeightData != null)
                {
                    int countWiseTotalCrates = greenProcurementFarmerWiseCountWeightData.Sum(x => x.NoofCrates);
                    decimal countWiseTotalQuanity = greenProcurementFarmerWiseCountWeightData.Sum(x => x.CWCrateswiseNetWeight);
                    countWiseTotalQuanity = Math.Round(countWiseTotalQuanity, 3);

                    CWHarvestGRNWeightSummaryDetails cWHarvestGRNWeightSummaryDetails = new CWHarvestGRNWeightSummaryDetails()
                    {
                        HarvestGRNNo = HarvestGRNNo,
                        GreensProcurementNo = GreensProcurementNo,
                        BuyerEmployeeID = BuyerEmployerId,
                        CWCountwiseTotalQuantity = countWiseTotalQuanity,
                        NoofCrates = countWiseTotalCrates
                    };
                    var crateWiseDetail = _context.CWHarvestGRNWeightSummaryDetails.Add(cWHarvestGRNWeightSummaryDetails);
                    var result = await _context.SaveChangesAsync();
                    if (result == 1)
                    {
                        return crateWiseDetail;
                    }
                    else
                    {
                        throw new Exception("Error saving buyer information, please try again.");
                    }
                }
                else
                {
                    throw new Exception("Weighment not done for the provided buyer");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<CWHarvestGRNCountWeightDetails> AddBuyerQuantityCratewiseDetail(HarvestBuyerWeighingDetailsDTO harvestBuyerWeighingDetailsDTO)
        {
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        decimal calculatedTareweight = Math.Round((harvestBuyerWeighingDetailsDTO.NoofCrates * harvestBuyerWeighingDetailsDTO.EachCrateWt), 3);//Update to 3 digits
                        decimal calculatedCrateswiseNetWeight = harvestBuyerWeighingDetailsDTO.CWGrossWeight - calculatedTareweight;
                        CWHarvestGRNCountWeightDetails cratewiseDetail = null;
                        if (harvestBuyerWeighingDetailsDTO.CWGreensCratewiseEntryNo > 0)
                        {
                            cratewiseDetail = await _context.CWHarvestGRNCountWeightDetails.FirstOrDefaultAsync(x =>
                            x.HarvestGRNNo == harvestBuyerWeighingDetailsDTO.HarvestGRNNo &&
                            x.GreensProcurementNo == harvestBuyerWeighingDetailsDTO.GreensProcurementNo &&
                            x.CWGreensCratewiseEntryNo == harvestBuyerWeighingDetailsDTO.CWGreensCratewiseEntryNo);
                            if (cratewiseDetail != null)
                            {
                                cratewiseDetail.NoofCrates = harvestBuyerWeighingDetailsDTO.NoofCrates;
                                cratewiseDetail.EachCrateWt = harvestBuyerWeighingDetailsDTO.EachCrateWt;
                                cratewiseDetail.CrateNoFrom = harvestBuyerWeighingDetailsDTO.CrateNoFrom;
                                cratewiseDetail.CrateNoTo = harvestBuyerWeighingDetailsDTO.CrateNoTo;
                                cratewiseDetail.CWGrossWeight = harvestBuyerWeighingDetailsDTO.CWGrossWeight;
                                cratewiseDetail.CWTareweight = calculatedTareweight;
                                cratewiseDetail.CWCrateswiseNetWeight = calculatedCrateswiseNetWeight;
                            }
                            else
                            {
                                throw new Exception("Buyer Quantity CrateWise Record not found.");
                            }
                        }
                        else
                        {
                            cratewiseDetail = new CWHarvestGRNCountWeightDetails()
                            {
                                HarvestGRNNo = harvestBuyerWeighingDetailsDTO.HarvestGRNNo,
                                GreensProcurementNo = harvestBuyerWeighingDetailsDTO.GreensProcurementNo,
                                BuyerEmployeeID = harvestBuyerWeighingDetailsDTO.BuyerEmployeeID,
                                CropGroupCode = harvestBuyerWeighingDetailsDTO.CropGroupCode,
                                CropNameCode = harvestBuyerWeighingDetailsDTO.CropNameCode,
                                CropSchemeCode = harvestBuyerWeighingDetailsDTO.CropSchemeCode,
                                NoofCrates = harvestBuyerWeighingDetailsDTO.NoofCrates,
                                EachCrateWt = harvestBuyerWeighingDetailsDTO.EachCrateWt,
                                CrateNoFrom = harvestBuyerWeighingDetailsDTO.CrateNoFrom,
                                CrateNoTo = harvestBuyerWeighingDetailsDTO.CrateNoTo,
                                CWGrossWeight = harvestBuyerWeighingDetailsDTO.CWGrossWeight,
                                CWTareweight = calculatedTareweight,
                                CWCrateswiseNetWeight = calculatedCrateswiseNetWeight
                            };
                            _context.CWHarvestGRNCountWeightDetails.Add(cratewiseDetail);
                        }
                        await _context.SaveChangesAsync();

                        CWHarvestBuyerWeighingDetails greenFarmerRecord = await _context.CWHarvestBuyerWeighingDetails.FirstOrDefaultAsync(x =>
                        x.GreensProcurementNo == harvestBuyerWeighingDetailsDTO.GreensProcurementNo &&
                        x.HarvestGRNNo == harvestBuyerWeighingDetailsDTO.HarvestGRNNo &&
                        x.BuyerEmployeeID == harvestBuyerWeighingDetailsDTO.BuyerEmployeeID &&
                        x.CropGroupCode == harvestBuyerWeighingDetailsDTO.CropGroupCode &&
                        x.CropNameCode == harvestBuyerWeighingDetailsDTO.CropNameCode &&
                        x.CropSchemeCode == harvestBuyerWeighingDetailsDTO.CropSchemeCode);

                        if (greenFarmerRecord != null)
                        {
                            List<CWHarvestGRNCountWeightDetails> greenProcurementFarmerWiseCountWeightData = await _context.CWHarvestGRNCountWeightDetails.Where(x =>
                            x.GreensProcurementNo == harvestBuyerWeighingDetailsDTO.GreensProcurementNo &&
                            x.HarvestGRNNo == harvestBuyerWeighingDetailsDTO.HarvestGRNNo &&
                            x.BuyerEmployeeID == harvestBuyerWeighingDetailsDTO.BuyerEmployeeID &&
                            x.CropGroupCode == harvestBuyerWeighingDetailsDTO.CropGroupCode &&
                            x.CropNameCode == harvestBuyerWeighingDetailsDTO.CropNameCode &&
                            x.CropSchemeCode == harvestBuyerWeighingDetailsDTO.CropSchemeCode).ToListAsync();

                            int countWiseTotalCrates = greenProcurementFarmerWiseCountWeightData.Sum(x => x.NoofCrates);
                            decimal countWiseTotalQuanity = greenProcurementFarmerWiseCountWeightData.Sum(x => x.CWCrateswiseNetWeight);
                            countWiseTotalQuanity = Math.Round(countWiseTotalQuanity, 3);
                            greenFarmerRecord.NoofCrates = countWiseTotalCrates;
                            greenFarmerRecord.CWCountwiseTotalQuantity = countWiseTotalQuanity;
                        }
                        else
                        {
                            CWHarvestBuyerWeighingDetails greensFarmersDetail = new CWHarvestBuyerWeighingDetails()
                            {
                                GreensProcurementNo = harvestBuyerWeighingDetailsDTO.GreensProcurementNo,
                                HarvestGRNNo = harvestBuyerWeighingDetailsDTO.HarvestGRNNo,
                                BuyerEmployeeID = harvestBuyerWeighingDetailsDTO.BuyerEmployeeID,
                                CropGroupCode = harvestBuyerWeighingDetailsDTO.CropGroupCode,
                                CropNameCode = harvestBuyerWeighingDetailsDTO.CropNameCode,
                                CropSchemeCode = harvestBuyerWeighingDetailsDTO.CropSchemeCode,
                                NoofCrates = cratewiseDetail.NoofCrates,
                                CWCountwiseTotalQuantity = cratewiseDetail.CWCrateswiseNetWeight
                            };
                            _context.CWHarvestBuyerWeighingDetails.Add(greensFarmersDetail);
                        }
                        await _context.SaveChangesAsync();

                        if (harvestBuyerWeighingDetailsDTO.CompleteBuyer == 1)
                        {
                            List<CWHarvestGRNCountWeightDetails> greenProcurementFarmerWiseCountWeightData = await _context.CWHarvestGRNCountWeightDetails.Where(x =>
                            x.GreensProcurementNo == harvestBuyerWeighingDetailsDTO.GreensProcurementNo &&
                            x.HarvestGRNNo == harvestBuyerWeighingDetailsDTO.HarvestGRNNo &&
                            x.BuyerEmployeeID == harvestBuyerWeighingDetailsDTO.BuyerEmployeeID).ToListAsync();
                            if (greenProcurementFarmerWiseCountWeightData != null)
                            {
                                int countWiseTotalCrates = greenProcurementFarmerWiseCountWeightData.Sum(x => x.NoofCrates);
                                decimal countWiseTotalQuanity = greenProcurementFarmerWiseCountWeightData.Sum(x => x.CWCrateswiseNetWeight);
                                countWiseTotalQuanity = Math.Round(countWiseTotalQuanity, 3);

                                CWHarvestGRNWeightSummaryDetails cWHarvestGRNWeightSummaryDetails = new CWHarvestGRNWeightSummaryDetails()
                                {
                                    HarvestGRNNo = harvestBuyerWeighingDetailsDTO.HarvestGRNNo,
                                    GreensProcurementNo = harvestBuyerWeighingDetailsDTO.GreensProcurementNo,
                                    BuyerEmployeeID = harvestBuyerWeighingDetailsDTO.BuyerEmployeeID,
                                    CWCountwiseTotalQuantity = countWiseTotalQuanity,
                                    NoofCrates = countWiseTotalCrates
                                };
                                _context.CWHarvestGRNWeightSummaryDetails.Add(cWHarvestGRNWeightSummaryDetails);
                                await _context.SaveChangesAsync();
                            }
                            else
                            {
                                throw new Exception("Weighment not done for the provided buyer");
                            }
                        }

                        HarvestGRN harvestGRN = await _context.HarvestGRNs.FirstOrDefaultAsync(x => x.HarvestGRNNo == harvestBuyerWeighingDetailsDTO.HarvestGRNNo);
                        List<CWHarvestGRNCountWeightDetails> greenProcurementWiseCountWeightData = await _context.CWHarvestGRNCountWeightDetails.Where(
                                x => x.HarvestGRNNo == harvestBuyerWeighingDetailsDTO.HarvestGRNNo).ToListAsync();
                        int TotalCrates = greenProcurementWiseCountWeightData.Sum(x => x.NoofCrates);
                        decimal TotalQuantity = greenProcurementWiseCountWeightData.Sum(x => x.CWCrateswiseNetWeight);
                        TotalQuantity = Math.Round(TotalQuantity, 3);
                        harvestGRN.HarvestGRNTotalDespCrates = TotalCrates;
                        harvestGRN.HarvestGRNTotalQuantity = TotalQuantity;
                        await _context.SaveChangesAsync();
                        transaction.Commit();
                        return cratewiseDetail;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<HarvestGRN> CompleteHarvestGrn(HarvestGRN harvestGRN)
        {
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var harvestGRNDetail = await _context.HarvestGRNs.FirstOrDefaultAsync(x => x.HarvestGRNNo == harvestGRN.HarvestGRNNo);
                        harvestGRNDetail.HarvestGRNDate = harvestGRN.HarvestGRNDate;
                        harvestGRNDetail.GreensTransVehicleDespNo = harvestGRN.GreensTransVehicleDespNo;
                        harvestGRNDetail.DriverName = harvestGRN.DriverName;
                        //harvestGRNDetail.DriverContactNo = harvestGRN.DriverContactNo;
                        harvestGRNDetail.VehicleStartTime = harvestGRN.VehicleStartTime;
                        harvestGRNDetail.VehicleStartingReading = harvestGRN.VehicleStartingReading;
                        harvestGRNDetail.VehicleFreight = harvestGRN.VehicleFreight;
                        harvestGRNDetail.OrgOfficeNo = harvestGRN.OrgOfficeNo;
                        harvestGRNDetail.LoadingCompleted = 1;
                        harvestGRNDetail.HarverstGRNRemarks = harvestGRN.HarverstGRNRemarks;

                        List<CWHarvestGRNCountWeightDetails> greenProcurementWiseCountWeightData = await _context.CWHarvestGRNCountWeightDetails.Where(
                                        x => x.HarvestGRNNo == harvestGRN.HarvestGRNNo).ToListAsync();

                        int TotalCrates = greenProcurementWiseCountWeightData.Sum(x => x.NoofCrates);
                        decimal TotalQuantity = greenProcurementWiseCountWeightData.Sum(x => x.CWCrateswiseNetWeight);
                        TotalQuantity = Math.Round(TotalQuantity, 3);

                        harvestGRNDetail.HarvestGRNTotalDespCrates = TotalCrates;
                        harvestGRNDetail.HarvestGRNTotalQuantity = TotalQuantity;

                        var result = await _context.SaveChangesAsync();

                        List<CWHarvestGRNWeightSummaryDetails> cWHarvestGRNWeightSummaryDetails = await _context.CWHarvestGRNWeightSummaryDetails.Where(
                            x => x.HarvestGRNNo == harvestGRN.HarvestGRNNo).ToListAsync();

                        List<CWHarvestGRNWeightSummaryDetails> cWHarvestGRNWeightSummaryDetailsObjList = new List<CWHarvestGRNWeightSummaryDetails>();
                        foreach (CWHarvestGRNCountWeightDetails cWHarvestGRNCountWeightDetails in greenProcurementWiseCountWeightData)
                        {
                            if (!cWHarvestGRNWeightSummaryDetails.Any(x => x.GreensProcurementNo == cWHarvestGRNCountWeightDetails.GreensProcurementNo)
                                && !cWHarvestGRNWeightSummaryDetailsObjList.Any(x => x.GreensProcurementNo == cWHarvestGRNCountWeightDetails.GreensProcurementNo)
                                )
                            {
                                int countWiseTotalCrates = greenProcurementWiseCountWeightData.Where(x => x.GreensProcurementNo == cWHarvestGRNCountWeightDetails.GreensProcurementNo).Sum(x => x.NoofCrates);
                                decimal countWiseTotalQuanity = greenProcurementWiseCountWeightData.Where(x => x.GreensProcurementNo == cWHarvestGRNCountWeightDetails.GreensProcurementNo).Sum(x => x.CWCrateswiseNetWeight);
                                countWiseTotalQuanity = Math.Round(countWiseTotalQuanity, 3);

                                CWHarvestGRNWeightSummaryDetails cWHarvestGRNWeightSummaryObj = new CWHarvestGRNWeightSummaryDetails()
                                {
                                    HarvestGRNNo = cWHarvestGRNCountWeightDetails.HarvestGRNNo,
                                    GreensProcurementNo = cWHarvestGRNCountWeightDetails.GreensProcurementNo.GetValueOrDefault(),
                                    BuyerEmployeeID = cWHarvestGRNCountWeightDetails.BuyerEmployeeID,
                                    CWCountwiseTotalQuantity = countWiseTotalQuanity,
                                    NoofCrates = countWiseTotalCrates
                                };
                                cWHarvestGRNWeightSummaryDetailsObjList.Add(cWHarvestGRNWeightSummaryObj);
                            }
                        }
                        if (cWHarvestGRNWeightSummaryDetailsObjList.Count > 0)
                        {
                            _context.CWHarvestGRNWeightSummaryDetails.AddRange(cWHarvestGRNWeightSummaryDetailsObjList);
                            await _context.SaveChangesAsync();
                        }
                        transaction.Commit();
                        return harvestGRNDetail;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<List<VehicleDetails>> GetVehicles()
        {
            List<VehicleDetails> GPVehicleDespNos = await (from a in _context.GreensProcurements
                                                           select new VehicleDetails
                                                           {
                                                               greensTransVehicleDespNo = a.GreensTransVehicleDespNo
                                                           }
                                                              ).Distinct().ToListAsync();
            List<VehicleDetails> HarvestVehicleDespNos = await (from b in _context.HarvestGRNs where b.LoadingCompleted==1 select new VehicleDetails { greensTransVehicleDespNo = b.GreensTransVehicleDespNo }).Distinct().ToListAsync();

            List<VehicleDetails> VehicleDetails = await (from vd in _context.GreensTransportVehicleSchedules
                                                         join ovd in _context.OwnVehiclesDetails on vd.OwnVehicleID equals ovd.OwnVehicleID into temp1
                                                         from vd1 in temp1.DefaultIfEmpty()
                                                         join hvd in _context.Hired_Vehicle_Details on vd.HiredVehicleID equals hvd.HiredVehicleID into temp2
                                                         from vd2 in temp2.DefaultIfEmpty()
                                                         join dd in _context.DriverDetails on vd.DriverID equals dd.DriverID into temp3
                                                         from vd3 in temp3.DefaultIfEmpty()
                                                         join ed in _context.Employees on vd3.EmployeeID equals ed.employeeId
                                                         select new VehicleDetails
                                                         {
                                                             entryDate = vd.EntryDate,
                                                             areaID = vd.AreaId,
                                                             buyerEmpID = vd.BuyerEmpId,
                                                             greensTransVehicleDespNo = vd.GreensTransVehicleDespNo,
                                                             hiredVehicleID = vd.HiredVehicleID,
                                                             ownVehicleID = vd.OwnVehicleID,
                                                             timeOfDespatch = vd.TimeofDespatch,
                                                             vehicleReading = vd.VehicleReading,
                                                             vehicleRegNo = (vd.OwnVehicleID == null ? vd2.VehicleRegNumber : vd1.VehicleRegNumber),
                                                             driverName = (vd.DriverID == null ? vd.GCDriverName : ed.employeeName)
                                                         }).ToListAsync();

            return VehicleDetails.Where(e => !GPVehicleDespNos.Any(x => x.greensTransVehicleDespNo == e.greensTransVehicleDespNo) &&
            !HarvestVehicleDespNos.Any(y => y.greensTransVehicleDespNo == e.greensTransVehicleDespNo)).ToList();

        }

        public async Task<List<GreensRecievedDetailDTO>> GetAllGreensReceivedDetailsList()
        {
            List<GreensRecievedDetailDTO> allGreensRecievedDetails = new List<GreensRecievedDetailDTO>();

            var HarvestDetails = await _context.HarvestGRNs.Where(x => x.LoadingCompleted == 0).ToListAsync();

            if (HarvestDetails != null)
            {
                foreach (var obj in HarvestDetails)
                {
                    GreensRecievedDetailDTO greensRecievedDetailDTO = new GreensRecievedDetailDTO();

                    greensRecievedDetailDTO.HarvestGRNNo = obj.HarvestGRNNo;
                    var result = await (from a in _context.GreensProcurements
                                        join gt in _context.GreensTransportVehicleSchedules on a.GreensTransVehicleDespNo equals gt.GreensTransVehicleDespNo
                                        join em in _context.Employees on gt.BuyerEmpId equals em.employeeId
                                        join b in _context.GreensFarmersDetails on a.GreensProcurementNo equals b.GreensProcurementNo
                                        join c in _context.Farmers on b.FarmerCode equals c.Farmer_Code
                                        join d in _context.FarmersAgreementDetails on new { p = b.FarmerCode, x = a.AreaID, y = b.PSNumber } equals new { p = d.Farmer_Code, x = d.Area_ID, y = d.PS_Number } into z
                                        from d in z.Take(1)
                                        join e in _context.Villages on c.Village_Code equals e.Village_Code
                                        join f in _context.CropSchemes on b.CropSchemeCode equals f.Code
                                        join _ownVehicles in _context.OwnVehiclesDetails on gt.OwnVehicleID equals _ownVehicles.OwnVehicleID into ownDt
                                        from _ownVehicles in ownDt.DefaultIfEmpty()
                                        join _hiredVehicles in _context.Hired_Vehicle_Details on gt.HiredVehicleID equals _hiredVehicles.HiredVehicleID into hiredDt
                                        from _hiredVehicles in hiredDt.DefaultIfEmpty()
                                        where a.VehicleEndPoint == "Tr. to Pooled Truck"
                                        && (!_context.CWHarvestGRNWeightSummaryDetails.Any(h => h.GreensProcurementNo == a.GreensProcurementNo)
                                        && !_context.CWHarvestGRNCountWeightDetails.Any(y => y.GreensProcurementNo == a.GreensProcurementNo && y.HarvestGRNNo != obj.HarvestGRNNo))
                                        orderby a.HarvestDate descending
                                        select new GreensReceivedDetail
                                        {
                                            AreaId = a.AreaID,
                                            HarvestDate = a.HarvestDate,
                                            HarvestProcurementNo = a.GreensProcurementNo,
                                            CropNameCode = b.CropNameCode,
                                            PSNumber = a.PSNumber,
                                            HarvestFarmersEntryNo = b.GreensFarmersEntryNo,
                                            FarmerCode = b.FarmerCode,
                                            CropSchemeCode = b.CropSchemeCode,
                                            FarmerwiseTotalCrates = b.CountwiseTotalCrates,
                                            FarmerwiseTotalQuantity = b.CountwiseTotalQuantity,
                                            FarmerName = c.FarmerName,
                                            VillageCode = c.Village_Code,
                                            FarmersAgreementCode = d.Farmers_Agreement_Code,
                                            FarmersAccountNo = d.Farmers_Account_No,
                                            VillageName = e.Village_Name,
                                            CropSchemeFrom = f.From,
                                            CropSchemeSign = f.Sign,
                                            CropGroupCode = b.CropGroupCode,
                                            BuyerEmployeeID = gt.BuyerEmpId,
                                            BuyerEmployeeName = em.employeeName,
                                            VehicleNo = gt.OwnVehicleID != null ? _ownVehicles.VehicleRegNumber : _hiredVehicles.VehicleRegNumber,
                                        }).ToListAsync();
                    greensRecievedDetailDTO.greensReceivedDetails = result;

                    var weightdetails = await (from a in _context.CWHarvestGRNCountWeightDetails
                                               join b in _context.Employees on a.BuyerEmployeeID equals b.employeeId
                                               join c in _context.CropSchemes on a.CropSchemeCode equals c.Code
                                               where a.HarvestGRNNo == obj.HarvestGRNNo
                                               select new HarvestGRNCountWeightDetailsDTO
                                               {
                                                   CWGreensCratewiseEntryNo = a.CWGreensCratewiseEntryNo,
                                                   HarvestGRNNo = a.HarvestGRNNo,
                                                   CropGroupCode = a.CropGroupCode,
                                                   CropNameCode = a.CropNameCode,
                                                   CropSchemeCode = a.CropSchemeCode,
                                                   NoofCrates = a.NoofCrates,
                                                   EachCrateWt = a.EachCrateWt,
                                                   CrateNoFrom = a.CrateNoFrom,
                                                   CrateNoTo = a.CrateNoTo,
                                                   CWGrossWeight = a.CWGrossWeight,
                                                   CWTareweight = a.CWTareweight,
                                                   CWCrateswiseNetWeight = a.CWCrateswiseNetWeight,
                                                   GreensProcurementNo = a.GreensProcurementNo,
                                                   BuyerEmployeeID = a.BuyerEmployeeID,
                                                   cropSchemeFrom = c.From,
                                                   cropSchemeSign = c.Sign,
                                                   cropCountInfo = c.From + " " + c.Sign,
                                                   BuyerEmployeeName = b.employeeName
                                               }).ToListAsync();
                    greensRecievedDetailDTO.weightDetails = weightdetails;

                    allGreensRecievedDetails.Add(greensRecievedDetailDTO);
                }

            }
            return allGreensRecievedDetails;
        }
    }
}