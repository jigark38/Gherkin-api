using GherkinWebAPI.DTO.DailyHarvest;
using GherkinWebAPI.DTO.HarvestDetails;
using GherkinWebAPI.Models.DailyHarvestDetails;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.DailyHarvestDetails
{
    public interface IDailyHarvestService
    {
        Task<List<BuyerSchedule>> GetBuyerSchedules();
        Task<GreensProcurement> AddGreensProcurement(GreensProcurement greensProcurement);
        Task<GreensFarmersDetail> AddGreensFarmersDetail(GreensFarmersDetail greensFarmersDetail);
        Task<GreensQuantityCratewiseDetail> AddGreensQuantityCratewiseDetail(GreensQuantityCratewiseDetail cratewiseDetail);
        Task<GreensQuantityCratewiseDetail> AddGreensQuantityCratewiseDetail(GreenFarmerQuanityCrateWiseDTO greenFarmerQuanityCrateWiseDTO);
        Task<List<GreensQuantityCountwiseDetail>> AddGreensQuantityCountwiseDetail(List<GreensQuantityCountwiseDetail> countwiseDetails);
        Task<List<GreensFarmersDetail>> AddGreensFarmersDetail(List<GreensFarmersDetail> greensFarmersDetail);
        Task<List<GreensQuantityCratewiseDetail>> AddGreensQuantityCratewiseDetail(List<GreensQuantityCratewiseDetail> cratewiseDetail);
        Task<GreensProcurement> GetGreensProcurementByDespNo(int DespNo);
        Task<List<GreenFarmerDetailDto>> GetGreensFarmersDetails(int GreenProcurementNo);
        Task<List<GreensQuantityCratewiseDetail>> GetGreensQuantityCrateWiseDetails(int GreenProcurementNo);
        Task<List<GreensQuantityCountwiseDetail>> GetGreensQuantityCountWiseDetails(int GreenProcurementNo);
        Task<List<BuyerSchedule>> GetCompletedDailyGreensRecieving(DateTime harvestDate);
        Task<List<GreensQuantityCratewiseDetailDTO>> GetDailyGreensQuantityCrateWiseDetails(int GreenProcurementNo);
        Task<GreensRecievingAllDetails> GetBuyerSchedulesWithProcurementDetails();
        Task<bool> BulkGreensInsert(List<BulkGreensInsertDTO> bulkGreensInsertDTO);
    }
}
