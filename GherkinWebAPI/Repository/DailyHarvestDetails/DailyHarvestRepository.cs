using GherkinWebAPI.Core.DailyHarvestDetails;
using GherkinWebAPI.DTO.DailyHarvest;
using GherkinWebAPI.DTO.HarvestDetails;
using GherkinWebAPI.Models.DailyHarvestDetails;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace GherkinWebAPI.Repository.DailyHarvestDetails
{
    public class DailyHarvestRepository : RepositoryBase<GreensProcurement>, IDailyHarvestRepository
    {
        private readonly RepositoryContext _repositoryContext;
        public DailyHarvestRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public async Task<GreensFarmersDetail> AddGreensFarmersDetail(GreensFarmersDetail greensFarmersDetail)
        {
            try
            {
                if (greensFarmersDetail.GreensFarmersEntryNo <= 0)
                {
                    var checkRecordExists = await _repositoryContext.GreensFarmersDetails.FirstOrDefaultAsync(x => x.GreensProcurementNo == greensFarmersDetail.GreensProcurementNo && x.FarmerCode == greensFarmersDetail.FarmerCode && x.CropSchemeCode == greensFarmersDetail.CropSchemeCode);
                    if (checkRecordExists != null)
                    {
                        greensFarmersDetail.GreensFarmersEntryNo = checkRecordExists.GreensFarmersEntryNo;
                    }
                }
                var greensFarmersDetails = await _repositoryContext.GreensFarmersDetails.SingleOrDefaultAsync(x => x.GreensFarmersEntryNo == greensFarmersDetail.GreensFarmersEntryNo);
                if (greensFarmersDetails != null)
                {
                    greensFarmersDetails.CountwiseTotalCrates = greensFarmersDetail.CountwiseTotalCrates;
                    greensFarmersDetails.CountwiseTotalQuantity = greensFarmersDetail.CountwiseTotalQuantity;
                    var result1 = await _repositoryContext.SaveChangesAsync();

                    if (result1 == 1)
                    {
                        return greensFarmersDetails;
                    }
                }
                var farmerDetail = _repositoryContext.GreensFarmersDetails.Add(greensFarmersDetail);

                var result = await _repositoryContext.SaveChangesAsync();

                if (result == 1)
                {
                    greensFarmersDetail.GreensFarmersEntryNo = farmerDetail.GreensFarmersEntryNo;
                    return greensFarmersDetail;
                }

                return new GreensFarmersDetail();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<GreensProcurement> AddGreensProcurement(GreensProcurement greensProcurement)
        {
            try
            {
                if (greensProcurement.GreensTransVehicleDespNo > 0)
                {
                    GreensProcurement checkRecordExists = await _repositoryContext.GreensProcurements.FirstOrDefaultAsync(x => x.GreensTransVehicleDespNo == greensProcurement.GreensTransVehicleDespNo);
                    if (checkRecordExists != null)
                    {
                        greensProcurement.GreensProcurementNo = checkRecordExists.GreensProcurementNo;
                    }
                }
                var greensProcurementDetail = await _repositoryContext.GreensProcurements.SingleOrDefaultAsync(g => g.GreensProcurementNo == greensProcurement.GreensProcurementNo);
                using (var transaction = _repositoryContext.Database.BeginTransaction())
                {
                    try
                    {
                        if (greensProcurementDetail != null && greensProcurement.HarvestEndingTime != null)
                        {
                            decimal totalQuantity = 0;
                            int totalCrates = 0;
                            List<GreensFarmersDetail> greensFarmersDetails = await _repositoryContext.GreensFarmersDetails.Where(g => g.GreensProcurementNo == greensProcurement.GreensProcurementNo).ToListAsync();
                            List<GreensQuantityCountwiseDetail> greensQuantityCountwiseDetails = new List<GreensQuantityCountwiseDetail>();
                            if (greensFarmersDetails != null && greensFarmersDetails.Count > 0)
                            {
                                foreach (var greensFarmer in greensFarmersDetails)
                                {
                                    if (greensQuantityCountwiseDetails.Any(x => x.GreensProcurementNo == greensFarmer.GreensProcurementNo && x.CropGroupCode == greensFarmer.CropGroupCode && x.CropNameCode == greensFarmer.CropNameCode && x.PSNumber == greensFarmer.PSNumber && x.CropSchemeCode == greensFarmer.CropSchemeCode))
                                    {
                                        GreensQuantityCountwiseDetail greensQuantityCountwiseDetail = greensQuantityCountwiseDetails.SingleOrDefault(x => x.GreensProcurementNo == greensFarmer.GreensProcurementNo && x.CropGroupCode == greensFarmer.CropGroupCode && x.CropNameCode == greensFarmer.CropNameCode && x.PSNumber == greensFarmer.PSNumber && x.CropSchemeCode == greensFarmer.CropSchemeCode);
                                        greensQuantityCountwiseDetail.TotalNoOfCrates += greensFarmer.CountwiseTotalCrates;
                                        greensQuantityCountwiseDetail.TotalFarmerHarvestQuantity += greensFarmer.CountwiseTotalQuantity;
                                    }
                                    else
                                    {
                                        GreensQuantityCountwiseDetail greensQuantityCountwiseDetail = new GreensQuantityCountwiseDetail()
                                        {
                                            GreensProcurementNo = greensFarmer.GreensProcurementNo,
                                            CropGroupCode = greensFarmer.CropGroupCode,
                                            CropNameCode = greensFarmer.CropNameCode,
                                            PSNumber = greensFarmer.PSNumber,
                                            CropSchemeCode = greensFarmer.CropSchemeCode,
                                            TotalNoOfCrates = greensFarmer.CountwiseTotalCrates,
                                            TotalFarmerHarvestQuantity = greensFarmer.CountwiseTotalQuantity
                                        };
                                        greensQuantityCountwiseDetails.Add(greensQuantityCountwiseDetail);
                                    }
                                    totalCrates += greensFarmer.CountwiseTotalCrates;
                                    totalQuantity += greensFarmer.CountwiseTotalQuantity;
                                }
                                _repositoryContext.GreensQuantityCountwiseDetails.AddRange(greensQuantityCountwiseDetails);
                                await _repositoryContext.SaveChangesAsync();
                            }

                            greensProcurementDetail.HarvestEndingTime = greensProcurement.HarvestEndingTime;
                            greensProcurementDetail.HarvestEndingKMS = greensProcurement.HarvestEndingKMS;
                            greensProcurementDetail.HarvestTimeDuration = greensProcurement.HarvestTimeDuration;
                            greensProcurementDetail.HaverstKMSTotalReading = greensProcurement.HaverstKMSTotalReading;
                            greensProcurementDetail.HarvestOtherCharges = greensProcurement.HarvestOtherCharges;
                            greensProcurementDetail.TripTotalQuantity = totalQuantity;
                            greensProcurementDetail.TripTotalCrates = totalCrates;
                            greensProcurementDetail.VehicleEndPoint = greensProcurement.VehicleEndPoint;
                            greensProcurementDetail.OrgofficeNo = greensProcurement.OrgofficeNo;
                            greensProcurementDetail.LocationAreaID = greensProcurement.LocationAreaID;
                            greensProcurementDetail.ReceivingCompleted = greensProcurement.ReceivingCompleted;
                            await _repositoryContext.SaveChangesAsync();
                        }
                        else
                        {
                            _repositoryContext.GreensProcurements.Add(greensProcurement);
                            await _repositoryContext.SaveChangesAsync();
                        }
                        transaction.Commit();
                        return greensProcurement;
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

        public async Task<List<GreensQuantityCountwiseDetail>> AddGreensQuantityCountwiseDetail(List<GreensQuantityCountwiseDetail> countwiseDetails)
        {
            try
            {
                int result = 0;

                foreach (var count in countwiseDetails)
                {
                    var checkRecordExists = await _repositoryContext.GreensQuantityCountwiseDetails.FirstOrDefaultAsync(x => x.GreensProcurementNo == count.GreensProcurementNo && x.CropSchemeCode == count.CropSchemeCode);
                    if (checkRecordExists != null)
                    {
                        count.GreensQuantityEntryNo = checkRecordExists.GreensQuantityEntryNo;
                    }
                    else
                    {
                        var countWiseDetail = _repositoryContext.GreensQuantityCountwiseDetails.Add(count);
                    }
                    result = await _repositoryContext.SaveChangesAsync();
                }

                if (result > 0)
                {
                    return countwiseDetails;
                }

                return new List<GreensQuantityCountwiseDetail>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<GreensQuantityCratewiseDetail> AddGreensQuantityCratewiseDetail(GreensQuantityCratewiseDetail cratewiseDetail)
        {
            try
            {
                var crateWiseDetail = _repositoryContext.GreensQuantityCratewiseDetails.Add(cratewiseDetail);

                var result = await _repositoryContext.SaveChangesAsync();

                if (result == 1)
                {
                    cratewiseDetail.GreensCratewiseEntryNo = crateWiseDetail.GreensCratewiseEntryNo;
                    return cratewiseDetail;
                }

                return new GreensQuantityCratewiseDetail();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<GreensQuantityCratewiseDetail> AddGreensQuantityCratewiseDetail(GreenFarmerQuanityCrateWiseDTO greenFarmerQuanityCrateWiseDTO)
        {
            try
            {
                using (var transaction = _repositoryContext.Database.BeginTransaction())
                {
                    try
                    {
                        decimal calculatedTareweight = Math.Round((greenFarmerQuanityCrateWiseDTO.NoofCrates * greenFarmerQuanityCrateWiseDTO.EachCrateWt), 3);//Update to 3 digits
                        decimal calculatedCrateswiseNetWeight = greenFarmerQuanityCrateWiseDTO.GrossWeight - calculatedTareweight;
                        GreensQuantityCratewiseDetail cratewiseDetail = null;
                        if (greenFarmerQuanityCrateWiseDTO.GreensCratewiseEntryNo > 0)
                        {
                            cratewiseDetail = await _repositoryContext.GreensQuantityCratewiseDetails.FirstOrDefaultAsync(x => x.GreensProcurementNo == greenFarmerQuanityCrateWiseDTO.GreensProcurementNo && x.GreensCratewiseEntryNo == greenFarmerQuanityCrateWiseDTO.GreensCratewiseEntryNo);
                            if (cratewiseDetail != null)
                            {
                                cratewiseDetail.NoofCrates = greenFarmerQuanityCrateWiseDTO.NoofCrates;
                                cratewiseDetail.EachCrateWt = greenFarmerQuanityCrateWiseDTO.EachCrateWt;
                                cratewiseDetail.CrateNoFrom = greenFarmerQuanityCrateWiseDTO.CrateNoFrom;
                                cratewiseDetail.CrateNoTo = greenFarmerQuanityCrateWiseDTO.CrateNoTo;
                                cratewiseDetail.GrossWeight = greenFarmerQuanityCrateWiseDTO.GrossWeight;
                                cratewiseDetail.Tareweight = calculatedTareweight;
                                cratewiseDetail.CrateswiseNetWeight = calculatedCrateswiseNetWeight;
                            }
                            else
                            {
                                throw new Exception("Greens Quantity CrateWise Record not found.");
                            }
                        }
                        else
                        {
                            cratewiseDetail = new GreensQuantityCratewiseDetail()
                            {
                                GreensProcurementNo = greenFarmerQuanityCrateWiseDTO.GreensProcurementNo,
                                FarmerCode = greenFarmerQuanityCrateWiseDTO.FarmerCode,
                                CropGroupCode = greenFarmerQuanityCrateWiseDTO.CropGroupCode,
                                CropNameCode = greenFarmerQuanityCrateWiseDTO.CropNameCode,
                                PSNumber = greenFarmerQuanityCrateWiseDTO.PSNumber,
                                CropSchemeCode = greenFarmerQuanityCrateWiseDTO.CropSchemeCode,
                                NoofCrates = greenFarmerQuanityCrateWiseDTO.NoofCrates,
                                EachCrateWt = greenFarmerQuanityCrateWiseDTO.EachCrateWt,
                                CrateNoFrom = greenFarmerQuanityCrateWiseDTO.CrateNoFrom,
                                CrateNoTo = greenFarmerQuanityCrateWiseDTO.CrateNoTo,
                                GrossWeight = greenFarmerQuanityCrateWiseDTO.GrossWeight,
                                Tareweight = calculatedTareweight,
                                CrateswiseNetWeight = calculatedCrateswiseNetWeight
                            };
                            _repositoryContext.GreensQuantityCratewiseDetails.Add(cratewiseDetail);
                        }
                        await _repositoryContext.SaveChangesAsync();

                        GreensFarmersDetail greenFarmerRecord = await _repositoryContext.GreensFarmersDetails.FirstOrDefaultAsync(x => x.GreensProcurementNo == greenFarmerQuanityCrateWiseDTO.GreensProcurementNo && x.FarmerCode == greenFarmerQuanityCrateWiseDTO.FarmerCode && x.CropGroupCode == greenFarmerQuanityCrateWiseDTO.CropGroupCode && x.CropNameCode == greenFarmerQuanityCrateWiseDTO.CropNameCode && x.PSNumber == greenFarmerQuanityCrateWiseDTO.PSNumber && x.CropSchemeCode == greenFarmerQuanityCrateWiseDTO.CropSchemeCode);
                        if (greenFarmerRecord != null)
                        {
                            List<GreensQuantityCratewiseDetail> greenProcurementFarmerWiseCountWeightData = await _repositoryContext.GreensQuantityCratewiseDetails.Where(x => x.GreensProcurementNo == greenFarmerQuanityCrateWiseDTO.GreensProcurementNo && x.FarmerCode == greenFarmerQuanityCrateWiseDTO.FarmerCode && x.CropGroupCode == greenFarmerQuanityCrateWiseDTO.CropGroupCode && x.CropNameCode == greenFarmerQuanityCrateWiseDTO.CropNameCode && x.PSNumber == greenFarmerQuanityCrateWiseDTO.PSNumber && x.CropSchemeCode == greenFarmerQuanityCrateWiseDTO.CropSchemeCode).ToListAsync();
                            int countWiseTotalCrates = greenProcurementFarmerWiseCountWeightData.Sum(x => x.NoofCrates);
                            decimal countWiseTotalQuanity = greenProcurementFarmerWiseCountWeightData.Sum(x => x.CrateswiseNetWeight);
                            countWiseTotalQuanity = Math.Round(countWiseTotalQuanity, 3);
                            greenFarmerRecord.CountwiseTotalCrates = countWiseTotalCrates;
                            greenFarmerRecord.CountwiseTotalQuantity = countWiseTotalQuanity;
                        }
                        else
                        {
                            GreensFarmersDetail greensFarmersDetail = new GreensFarmersDetail()
                            {
                                GreensProcurementNo = cratewiseDetail.GreensProcurementNo,
                                FarmerCode = cratewiseDetail.FarmerCode,
                                CropGroupCode = cratewiseDetail.CropGroupCode,
                                CropNameCode = cratewiseDetail.CropNameCode,
                                PSNumber = cratewiseDetail.PSNumber,
                                CropSchemeCode = cratewiseDetail.CropSchemeCode,
                                CountwiseTotalCrates = cratewiseDetail.NoofCrates,
                                CountwiseTotalQuantity = cratewiseDetail.CrateswiseNetWeight,
                                LastHarvestStatus = greenFarmerQuanityCrateWiseDTO.LastHarvestStatus
                            };
                            _repositoryContext.GreensFarmersDetails.Add(greensFarmersDetail);
                        }
                        await _repositoryContext.SaveChangesAsync();


                        GreensQuantityCountwiseDetail greensQuantityCountwiseDetail = await _repositoryContext.GreensQuantityCountwiseDetails.FirstOrDefaultAsync(
                            x => x.GreensProcurementNo == greenFarmerQuanityCrateWiseDTO.GreensProcurementNo && x.CropGroupCode == greenFarmerQuanityCrateWiseDTO.CropGroupCode && x.CropNameCode == greenFarmerQuanityCrateWiseDTO.CropNameCode && x.PSNumber == greenFarmerQuanityCrateWiseDTO.PSNumber && x.CropSchemeCode == greenFarmerQuanityCrateWiseDTO.CropSchemeCode);
                        if (greensQuantityCountwiseDetail != null)
                        {
                            List<GreensQuantityCratewiseDetail> greenProcurementAndSchemeWiseCountWeightData = await _repositoryContext.GreensQuantityCratewiseDetails.Where(
                                x => x.GreensProcurementNo == greenFarmerQuanityCrateWiseDTO.GreensProcurementNo && x.CropGroupCode == greenFarmerQuanityCrateWiseDTO.CropGroupCode && x.CropNameCode == greenFarmerQuanityCrateWiseDTO.CropNameCode && x.PSNumber == greenFarmerQuanityCrateWiseDTO.PSNumber && x.CropSchemeCode == greenFarmerQuanityCrateWiseDTO.CropSchemeCode).ToListAsync();
                            int TotalNoOfCrates = greenProcurementAndSchemeWiseCountWeightData.Sum(x => x.NoofCrates);
                            decimal TotalFarmerHarvestQuantity = greenProcurementAndSchemeWiseCountWeightData.Sum(x => x.CrateswiseNetWeight);
                            TotalFarmerHarvestQuantity = Math.Round(TotalFarmerHarvestQuantity, 3);
                            greensQuantityCountwiseDetail.TotalNoOfCrates = TotalNoOfCrates;
                            greensQuantityCountwiseDetail.TotalFarmerHarvestQuantity = TotalFarmerHarvestQuantity;
                        }

                        GreensProcurement greensProcurement = await _repositoryContext.GreensProcurements.FirstOrDefaultAsync(x => x.GreensProcurementNo == greenFarmerQuanityCrateWiseDTO.GreensProcurementNo);
                        List<GreensQuantityCratewiseDetail> greenProcurementWiseCountWeightData = await _repositoryContext.GreensQuantityCratewiseDetails.Where(
                                x => x.GreensProcurementNo == greenFarmerQuanityCrateWiseDTO.GreensProcurementNo ).ToListAsync();
                        int TotalCrates = greenProcurementWiseCountWeightData.Sum(x => x.NoofCrates);
                        decimal TotalQuantity = greenProcurementWiseCountWeightData.Sum(x => x.CrateswiseNetWeight);
                        TotalQuantity = Math.Round(TotalQuantity, 3);
                        greensProcurement.TripTotalCrates = TotalCrates;
                        greensProcurement.TripTotalQuantity = TotalQuantity;

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
        public async Task<List<BuyerSchedule>> GetBuyerSchedules()
        {
            var buyerSchedules = await (from a in _repositoryContext.GreensTransportVehicleSchedules
                                        join e in _repositoryContext.Employees on a.BuyerEmpId equals e.employeeId
                                        join b in _repositoryContext.Areas on a.AreaId equals b.Area_ID
                                        join v in _repositoryContext.GreensProcurements on a.GreensTransVehicleDespNo equals v.GreensTransVehicleDespNo into outer
                                        from o in outer.DefaultIfEmpty()
                                        where o.ReceivingCompleted == null || o.ReceivingCompleted == 0
                                        //where g.ReceivingCompleted==0
                                        //where !_repositoryContext.GreensProcurements.Any(g => g.GreensTransVehicleDespNo == a.GreensTransVehicleDespNo)
                                        select new BuyerSchedule
                                        {
                                            BuyerName = e.employeeName,
                                            BuyerId = a.BuyerEmpId,
                                            Area = b.Area_Name,
                                            AreaId = b.Area_ID,
                                            DespDate = a.EntryDate,
                                            DespNo = a.GreensTransVehicleDespNo,
                                            GreensProcurementNo = o.GreensProcurementNo
                                        }).ToListAsync();
            return buyerSchedules;
        }

        public async Task<List<GreensFarmersDetail>> AddGreensFarmersDetail(List<GreensFarmersDetail> greensFarmersDetail)
        {
            try
            {
                int result = 0;

                foreach (var count in greensFarmersDetail)
                {
                    var countWiseDetail = _repositoryContext.GreensFarmersDetails.Add(count);
                    result = await _repositoryContext.SaveChangesAsync();
                }

                if (result > 0)
                {
                    return greensFarmersDetail;
                }

                return new List<GreensFarmersDetail>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<GreensQuantityCratewiseDetail>> AddGreensQuantityCratewiseDetail(List<GreensQuantityCratewiseDetail> cratewiseDetail)
        {
            try
            {
                int result = 0;

                foreach (var count in cratewiseDetail)
                {
                    var countWiseDetail = _repositoryContext.GreensQuantityCratewiseDetails.Add(count);
                    result = await _repositoryContext.SaveChangesAsync();
                }

                if (result > 0)
                {
                    return cratewiseDetail;
                }

                return new List<GreensQuantityCratewiseDetail>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<GreensProcurement> GetGreensProcurementByDespNo(int DespNo)
        {
            return await _repositoryContext.GreensProcurements.Where(e => e.GreensTransVehicleDespNo == DespNo).FirstOrDefaultAsync();
        }

        public async Task<List<GreenFarmerDetailDto>> GetGreensFarmersDetails(int GreenProcurementNo)
        {
            //return await _repositoryContext.GreensFarmersDetails.Where(e => e.GreensProcurementNo== GreenProcurementNo).ToListAsync();

            var GreensFarmersDetails = await (from v in _repositoryContext.GreensProcurements
                                              join a in _repositoryContext.GreensFarmersDetails on v.GreensProcurementNo equals a.GreensProcurementNo
                                              join e in _repositoryContext.CropSchemes on a.CropSchemeCode equals e.Code
                                              join b in _repositoryContext.Farmers on a.FarmerCode equals b.Farmer_Code
                                              join d in _repositoryContext.FarmersAgreementDetails on new { p = a.FarmerCode, x = v.AreaID, y = a.PSNumber } equals new { p = d.Farmer_Code, x = d.Area_ID, y = d.PS_Number } into z
                                              from d in z.Take(1)
                                              where a.GreensProcurementNo == GreenProcurementNo
                                              select new GreenFarmerDetailDto
                                              {
                                                  GreensFarmersEntryNo = a.GreensFarmersEntryNo,
                                                  GreensProcurementNo = a.GreensProcurementNo,
                                                  FarmerCode = a.FarmerCode,
                                                  CropSchemeCode = a.CropSchemeCode,
                                                  CountwiseTotalCrates = a.CountwiseTotalCrates,
                                                  CountwiseTotalQuantity = a.CountwiseTotalQuantity,
                                                  LastHarvestStatus = a.LastHarvestStatus,
                                                  FarmerName = b.FarmerName,
                                                  CropSchemeInfo = e.From + " " + e.Sign + " / " + e.Count,
                                                  CropGroupCode = a.CropGroupCode,
                                                  CropNameCode = a.CropNameCode,
                                                  PSNumber = a.PSNumber,
                                                  FarmerAccountNumber = d.Farmers_Account_No
                                              }).ToListAsync();
            return GreensFarmersDetails;
        }

        public async Task<List<GreensQuantityCratewiseDetail>> GetGreensQuantityCrateWiseDetails(int GreenProcurementNo)
        {
            return await _repositoryContext.GreensQuantityCratewiseDetails.Where(e => e.GreensProcurementNo == GreenProcurementNo).ToListAsync();
        }

        public async Task<List<GreensQuantityCratewiseDetailDTO>> GetDailyGreensQuantityCrateWiseDetails(int GreenProcurementNo)
        {
            var GreensQuantityCratewiseDetails = await (from v in _repositoryContext.GreensProcurements
                                                        join a in _repositoryContext.GreensQuantityCratewiseDetails on v.GreensProcurementNo equals a.GreensProcurementNo
                                                        join e in _repositoryContext.CropSchemes on a.CropSchemeCode equals e.Code
                                                        join b in _repositoryContext.Farmers on a.FarmerCode equals b.Farmer_Code
                                                        join y in _repositoryContext.Villages on b.Village_Code equals y.Village_Code
                                                        join d in _repositoryContext.FarmersAgreementDetails on new { p = a.FarmerCode, x = v.AreaID, y = a.PSNumber } equals new { p = d.Farmer_Code, x = d.Area_ID, y = d.PS_Number } into z
                                                        from d in z.Take(1)
                                                        where a.GreensProcurementNo == GreenProcurementNo
                                                        select new GreensQuantityCratewiseDetailDTO
                                                        {
                                                            GreensCratewiseEntryNo = a.GreensCratewiseEntryNo,
                                                            GreensProcurementNo = a.GreensProcurementNo,
                                                            FarmerCode = a.FarmerCode,
                                                            CropSchemeCode = a.CropSchemeCode,
                                                            NoofCrates = a.NoofCrates,
                                                            EachCrateWt = a.EachCrateWt,
                                                            CrateNoFrom = a.CrateNoFrom,
                                                            CrateNoTo = a.CrateNoTo,
                                                            GrossWeight = a.GrossWeight,
                                                            Tareweight = a.Tareweight,
                                                            CrateswiseNetWeight = a.CrateswiseNetWeight,
                                                            FarmerName = b.FarmerName,
                                                            CropSchemeInfo = e.From + " " + e.Sign + " / " + e.Count,
                                                            FarmerAccountNumber = d.Farmers_Account_No,
                                                            VillageName = y.Village_Name,
                                                            VillageCode = d.Village_Code,

                                                        }).ToListAsync();
            return GreensQuantityCratewiseDetails;
        }

        public async Task<List<GreensQuantityCountwiseDetail>> GetGreensQuantityCountWiseDetails(int GreenProcurementNo)
        {
            return await _repositoryContext.GreensQuantityCountwiseDetails.Where(e => e.GreensProcurementNo == GreenProcurementNo).ToListAsync();
        }

        public async Task<List<BuyerSchedule>> GetCompletedDailyGreensRecieving(DateTime harvestDate)
        {
            var buyerSchedules = await (from v in _repositoryContext.GreensProcurements
                                        join a in _repositoryContext.GreensTransportVehicleSchedules on v.GreensTransVehicleDespNo equals a.GreensTransVehicleDespNo
                                        join e in _repositoryContext.Employees on a.BuyerEmpId equals e.employeeId
                                        join b in _repositoryContext.Areas on a.AreaId equals b.Area_ID
                                        where v.ReceivingCompleted == 1 && DbFunctions.TruncateTime(v.HarvestDate) == harvestDate
                                        select new BuyerSchedule
                                        {
                                            BuyerName = e.employeeName,
                                            BuyerId = a.BuyerEmpId,
                                            Area = b.Area_Name,
                                            AreaId = b.Area_ID,
                                            DespDate = a.EntryDate,
                                            DespNo = a.GreensTransVehicleDespNo,
                                            GreensProcurementNo = v.GreensProcurementNo
                                        }).ToListAsync();
            return buyerSchedules;
        }

        public async Task<List<GreenFarmerDetailDto>> GetAllGreensFarmersDetails()
        {
            var GreensFarmersDetails = await (from v in _repositoryContext.GreensProcurements
                                              join a in _repositoryContext.GreensFarmersDetails on v.GreensProcurementNo equals a.GreensProcurementNo
                                              join e in _repositoryContext.CropSchemes on a.CropSchemeCode equals e.Code
                                              join b in _repositoryContext.Farmers on a.FarmerCode equals b.Farmer_Code
                                              join d in _repositoryContext.FarmersAgreementDetails on new { p = a.FarmerCode, x = v.AreaID, y = a.PSNumber } equals new { p = d.Farmer_Code, x = d.Area_ID, y = d.PS_Number } into z
                                              from d in z.Take(1)
                                              where v.ReceivingCompleted !=1
                                              select new GreenFarmerDetailDto
                                              {
                                                  GreensFarmersEntryNo = a.GreensFarmersEntryNo,
                                                  GreensProcurementNo = a.GreensProcurementNo,
                                                  FarmerCode = a.FarmerCode,
                                                  CropSchemeCode = a.CropSchemeCode,
                                                  CountwiseTotalCrates = a.CountwiseTotalCrates,
                                                  CountwiseTotalQuantity = a.CountwiseTotalQuantity,
                                                  LastHarvestStatus = a.LastHarvestStatus,
                                                  FarmerName = b.FarmerName,
                                                  CropSchemeInfo = e.From + " " + e.Sign + " / " + e.Count,
                                                  CropGroupCode = a.CropGroupCode,
                                                  CropNameCode = a.CropNameCode,
                                                  PSNumber = a.PSNumber,
                                                  FarmerAccountNumber = d.Farmers_Account_No
                                              }).ToListAsync();
            return GreensFarmersDetails;
        }

        public async Task<List<GreensQuantityCratewiseDetailDTO>> GetAllDailyGreensQuantityCrateWiseDetails()
        {
            var GreensQuantityCratewiseDetails = await (from v in _repositoryContext.GreensProcurements
                                                        join a in _repositoryContext.GreensQuantityCratewiseDetails on v.GreensProcurementNo equals a.GreensProcurementNo
                                                        join e in _repositoryContext.CropSchemes on a.CropSchemeCode equals e.Code
                                                        join b in _repositoryContext.Farmers on a.FarmerCode equals b.Farmer_Code
                                                        join y in _repositoryContext.Villages on b.Village_Code equals y.Village_Code
                                                        join d in _repositoryContext.FarmersAgreementDetails on new { p = a.FarmerCode, x = v.AreaID, y = a.PSNumber } equals new { p = d.Farmer_Code, x = d.Area_ID, y = d.PS_Number } into z
                                                        from d in z.Take(1)
                                                        where v.ReceivingCompleted != 1
                                                        select new GreensQuantityCratewiseDetailDTO
                                                        {
                                                            GreensCratewiseEntryNo = a.GreensCratewiseEntryNo,
                                                            GreensProcurementNo = a.GreensProcurementNo,
                                                            FarmerCode = a.FarmerCode,
                                                            CropSchemeCode = a.CropSchemeCode,
                                                            NoofCrates = a.NoofCrates,
                                                            EachCrateWt = a.EachCrateWt,
                                                            CrateNoFrom = a.CrateNoFrom,
                                                            CrateNoTo = a.CrateNoTo,
                                                            GrossWeight = a.GrossWeight,
                                                            Tareweight = a.Tareweight,
                                                            CrateswiseNetWeight = a.CrateswiseNetWeight,
                                                            FarmerName = b.FarmerName,
                                                            CropSchemeInfo = e.From + " " + e.Sign + " / " + e.Count,
                                                            FarmerAccountNumber = d.Farmers_Account_No,
                                                            VillageName = y.Village_Name,
                                                            VillageCode = d.Village_Code,

                                                        }).ToListAsync();
            return GreensQuantityCratewiseDetails;
        }

        public async Task<List<GreensProcurement>> GetAllGreensProcurement()
        {
            return await _repositoryContext.GreensProcurements.Where(e => e.ReceivingCompleted != 1).ToListAsync();
        }
        public async Task<GreensRecievingAllDetails> GetBuyerSchedulesWithProcurementDetails()
        {
            GreensRecievingAllDetails greensRecievingAllDetails = new GreensRecievingAllDetails();
            greensRecievingAllDetails.buyerSchedules= await GetBuyerSchedules();
            greensRecievingAllDetails.greensProcurements = await GetAllGreensProcurement();
            greensRecievingAllDetails.greenFarmerDetails = await GetAllGreensFarmersDetails();
            greensRecievingAllDetails.greensQuantityCratewiseDetails =await GetAllDailyGreensQuantityCrateWiseDetails();
            return greensRecievingAllDetails;
        }

    }
}