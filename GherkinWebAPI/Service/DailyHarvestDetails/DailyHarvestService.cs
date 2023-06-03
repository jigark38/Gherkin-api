using GherkinWebAPI.Core;
using GherkinWebAPI.Core.DailyHarvestDetails;
using GherkinWebAPI.DTO.DailyHarvest;
using GherkinWebAPI.DTO.HarvestDetails;
using GherkinWebAPI.Models.DailyHarvestDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GherkinWebAPI.Service.DailyHarvestDetails
{
    public class DailyHarvestService : IDailyHarvestService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public DailyHarvestService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<GreensFarmersDetail> AddGreensFarmersDetail(GreensFarmersDetail greensFarmersDetail)
        {
            if(greensFarmersDetail==null)
            {
                throw new Exception("Please provide object.");
            }
            if(greensFarmersDetail.GreensProcurementNo<=0)
            {
                throw new Exception("Green Procurement No is not provided.");
            }
            if(String.IsNullOrEmpty(greensFarmersDetail.FarmerCode))
            {
                throw new Exception("Farmer code is not provided.");
            }
            if (String.IsNullOrEmpty(greensFarmersDetail.CropSchemeCode))
            {
                throw new Exception("Crop scheme code not provided.");
            }
            if (String.IsNullOrEmpty(greensFarmersDetail.LastHarvestStatus))
            {
                throw new Exception("Last harvest status is not provided.");
            }
            return await _repositoryWrapper.DailyHarvestRepository.AddGreensFarmersDetail(greensFarmersDetail);
        }

        public async Task<GreensProcurement> AddGreensProcurement(GreensProcurement greensProcurement)
        {
            if(greensProcurement==null)
            {
                throw new Exception("Request object is not provided");
            }
            if(greensProcurement.GreensProcurementNo<=0)
            {
                greensProcurement.CropNameCode = "";
                greensProcurement.PSNumber = "";
            }
            return await _repositoryWrapper.DailyHarvestRepository.AddGreensProcurement(greensProcurement);
        }

        public async Task<List<GreensQuantityCountwiseDetail>> AddGreensQuantityCountwiseDetail(List<GreensQuantityCountwiseDetail> countwiseDetails)
        {
            if(countwiseDetails==null || countwiseDetails.Count<=0)
            {
                throw new Exception("Request object is not provided");
            }
            if(countwiseDetails.Where(x=>x.GreensProcurementNo<=0).Count()>0)
            {
                throw new Exception("Greens Procurement no is not provided");
            }
            if (countwiseDetails.Where(x => String.IsNullOrEmpty(x.CropSchemeCode)).Count() > 0)
            {
                throw new Exception("Crop scheme code is not provided");
            }
            return await _repositoryWrapper.DailyHarvestRepository.AddGreensQuantityCountwiseDetail(countwiseDetails);
        }

        public async Task<GreensQuantityCratewiseDetail> AddGreensQuantityCratewiseDetail(GreensQuantityCratewiseDetail cratewiseDetail)
        {
            if(cratewiseDetail==null)
            {
                throw new Exception("Request object is not provided");
            }
            if(cratewiseDetail.GreensProcurementNo<=0)
            {
                throw new Exception("Greens Procurement no is not provided");
            }
            if(String.IsNullOrEmpty(cratewiseDetail.FarmerCode))
            {
                throw new Exception("Farmer code is not provided");
            }
            if (String.IsNullOrEmpty(cratewiseDetail.CropGroupCode))
            {
                throw new Exception("Crop group code is not provided");
            }
            if (String.IsNullOrEmpty(cratewiseDetail.CropNameCode))
            {
                throw new Exception("Crop name code is not provided");
            }
            if (String.IsNullOrEmpty(cratewiseDetail.PSNumber))
            {
                throw new Exception("PSNumber is not provided");
            }
            if (String.IsNullOrEmpty(cratewiseDetail.CropSchemeCode))
            {
                throw new Exception("Crop scheme code is not provided");
            }
            return await _repositoryWrapper.DailyHarvestRepository.AddGreensQuantityCratewiseDetail(cratewiseDetail);
        }

        public async Task<List<BuyerSchedule>> GetBuyerSchedules()
        {
            return await _repositoryWrapper.DailyHarvestRepository.GetBuyerSchedules();
        }

        public async Task<List<GreensFarmersDetail>> AddGreensFarmersDetail(List<GreensFarmersDetail> greensFarmersDetail)
        {
            return await _repositoryWrapper.DailyHarvestRepository.AddGreensFarmersDetail(greensFarmersDetail);
        }

        public async Task<List<GreensQuantityCratewiseDetail>> AddGreensQuantityCratewiseDetail(List<GreensQuantityCratewiseDetail> cratewiseDetail)
        {
            return await _repositoryWrapper.DailyHarvestRepository.AddGreensQuantityCratewiseDetail(cratewiseDetail);
        }

        public async Task<GreensProcurement> GetGreensProcurementByDespNo(int DespNo)
        {
            return await _repositoryWrapper.DailyHarvestRepository.GetGreensProcurementByDespNo(DespNo);
        }

        public async Task<List<GreenFarmerDetailDto>> GetGreensFarmersDetails(int GreenProcurementNo)
        {
            return await _repositoryWrapper.DailyHarvestRepository.GetGreensFarmersDetails(GreenProcurementNo);
        }

        public async Task<List<GreensQuantityCratewiseDetail>> GetGreensQuantityCrateWiseDetails(int GreenProcurementNo)
        {
            return await _repositoryWrapper.DailyHarvestRepository.GetGreensQuantityCrateWiseDetails(GreenProcurementNo);
        }

        public async Task<List<GreensQuantityCountwiseDetail>> GetGreensQuantityCountWiseDetails(int GreenProcurementNo)
        {
            return await _repositoryWrapper.DailyHarvestRepository.GetGreensQuantityCountWiseDetails(GreenProcurementNo);
        }

        public async Task<GreensQuantityCratewiseDetail> AddGreensQuantityCratewiseDetail(GreenFarmerQuanityCrateWiseDTO greenFarmerQuanityCrateWiseDTO)
        {
            if (greenFarmerQuanityCrateWiseDTO == null)
            {
                throw new Exception("Request object is not provided");
            }
            if (greenFarmerQuanityCrateWiseDTO.GreensProcurementNo <= 0)
            {
                throw new Exception("Greens Procurement no is not provided");
            }
            if (String.IsNullOrEmpty(greenFarmerQuanityCrateWiseDTO.FarmerCode))
            {
                throw new Exception("Farmer code is not provided");
            }
            if (String.IsNullOrEmpty(greenFarmerQuanityCrateWiseDTO.CropSchemeCode))
            {
                throw new Exception("Crop scheme code is not provided");
            }
            if (String.IsNullOrEmpty(greenFarmerQuanityCrateWiseDTO.LastHarvestStatus))
            {
                throw new Exception("Last harvest status is not provided");
            }
            return await _repositoryWrapper.DailyHarvestRepository.AddGreensQuantityCratewiseDetail(greenFarmerQuanityCrateWiseDTO);
        }

        public async Task<List<BuyerSchedule>> GetCompletedDailyGreensRecieving(DateTime harvestDate)
        {
            return await _repositoryWrapper.DailyHarvestRepository.GetCompletedDailyGreensRecieving(harvestDate);
        }

        public async Task<List<GreensQuantityCratewiseDetailDTO>> GetDailyGreensQuantityCrateWiseDetails(int GreenProcurementNo)
        {
            return await _repositoryWrapper.DailyHarvestRepository.GetDailyGreensQuantityCrateWiseDetails(GreenProcurementNo);
        }

        public async Task<GreensRecievingAllDetails> GetBuyerSchedulesWithProcurementDetails()
        {
            return await _repositoryWrapper.DailyHarvestRepository.GetBuyerSchedulesWithProcurementDetails();
        }

        public async Task<bool> BulkGreensInsert(List<BulkGreensInsertDTO> bulkGreensInsertDTO)
        {
            try
            {
                foreach (var greensObj in bulkGreensInsertDTO)
                {
                    if (greensObj.greensProcurement == null)
                    {
                        throw new Exception("Request object is not provided");
                    }


                    var greenProcNumberResult = await _repositoryWrapper.DailyHarvestRepository.AddGreensProcurement(greensObj.greensProcurement);

                    foreach (var crateWiseDetailObj in greensObj.greenFarmerQuanityCrateWiseDTOs)
                    {
                        crateWiseDetailObj.GreensProcurementNo = greenProcNumberResult.GreensProcurementNo;

                        if (crateWiseDetailObj == null)
                        {
                            throw new Exception("Request object is not provided");
                        }
                        if (crateWiseDetailObj.GreensProcurementNo <= 0)
                        {
                            throw new Exception("Greens Procurement no is not provided");
                        }
                        if (String.IsNullOrEmpty(crateWiseDetailObj.FarmerCode))
                        {
                            throw new Exception("Farmer code is not provided");
                        }
                        if (String.IsNullOrEmpty(crateWiseDetailObj.CropSchemeCode))
                        {
                            throw new Exception("Crop scheme code is not provided");
                        }
                        if (String.IsNullOrEmpty(crateWiseDetailObj.LastHarvestStatus))
                        {
                            throw new Exception("Last harvest status is not provided");
                        }

                        await _repositoryWrapper.DailyHarvestRepository.AddGreensQuantityCratewiseDetail(crateWiseDetailObj);
                    }

                    greensObj.greensProcurementSave.GreensProcurementNo = greenProcNumberResult.GreensProcurementNo;

                    var greenProcNumberSave = await _repositoryWrapper.DailyHarvestRepository.AddGreensProcurement(greensObj.greensProcurementSave);

                }

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        
        }
    }
}