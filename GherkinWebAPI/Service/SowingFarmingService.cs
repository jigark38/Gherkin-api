using System.Threading.Tasks;
using GherkinWebAPI.Core;
using GherkinWebAPI.Core.SowingFarming;
using GherkinWebAPI.Response.SowingFarming;
using System.Linq;
using System;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Request.SowingFarming;
using GherkinWebAPI.Models;

namespace GherkinWebAPI.Service
{
    public class SowingFarmingService : ISowingFarmingService
    {
        private readonly IFarmersAgreementRepository farmersAgreementRepository;
        private readonly ICropRepository cropRepository;
        private readonly IPlantationHarvestRepository plantationHarvestRepository;
        private readonly ISowingFarmingRepository sowingFarmingRepository;

        public SowingFarmingService(IFarmersAgreementRepository farmersAgreementRepository, ICropRepository cropRepository,
                                    IPlantationHarvestRepository plantationHarvestRepository, ISowingFarmingRepository sowingFarmingRepository)
        {
            this.farmersAgreementRepository = farmersAgreementRepository;
            this.cropRepository = cropRepository;
            this.plantationHarvestRepository = plantationHarvestRepository;
            this.sowingFarmingRepository = sowingFarmingRepository;
        }

        public async Task<SowingFarmingFormDataResponse> GetSowingFarmingDataForFormByAreaId(string areaId)
        {
            SowingFarmingFormDataResponse sowingFarmingFormDataResponse = new SowingFarmingFormDataResponse();
            var farmerdetails = await this.farmersAgreementRepository.GetFarmersAgreementDetailsByAreaId(areaId);

            var uniqueCropGroupCode = farmerdetails.Select(x => x.Crop_Group_Code).Distinct();
            var allCropGroup = await this.cropRepository.GetAllCropGroup();
            foreach (var item in uniqueCropGroupCode)
            {
                var matched = allCropGroup.First(x => x.CropGroupCode == item);
                sowingFarmingFormDataResponse.CropGroups.Add(new CropGroupDto { Name = matched.Name, CropGroupCode = matched.CropGroupCode, CropGroupId = matched.CropGroupId });
            }

            var uniqueCropNameCode = farmerdetails.Select(x => x.Crop_Name_Code).Distinct();
            var allCropName = await this.cropRepository.GetAllCrops();

            foreach (var item in uniqueCropNameCode)
            {
                var matched = allCropName.First(x => x.CropCode == item);
                sowingFarmingFormDataResponse.CropNames.Add(new CropDto { Name = matched.Name, CropCode = matched.CropCode, CropId = matched.CropId, CropGroupCode = matched.CropGroupCode });
            }

            var uniquePsNo = farmerdetails.Select(x => x.PS_Number).Distinct();
            var allplantationSchhedule = await this.plantationHarvestRepository.GetPlantationSchedules();

            foreach (var item in uniquePsNo)
            {
                var matching = allplantationSchhedule.First(x => x.PsNumber == item);
                var matchingFarmers = farmerdetails.Where(x => x.PS_Number == matching.PsNumber);
                foreach (var m in matchingFarmers)
                {
                    sowingFarmingFormDataResponse.SowingSessions.Add(new SowingSessionDto
                    {
                        PSNumber = matching.PsNumber,
                        Farmers_Code = m.Farmer_Code,
                        SessionFrom = matching.FromDate,
                        SessionTo = matching.ToDate,
                        CropNameCode = m.Crop_Name_Code,
                        Agriculture_DRIP_NONDRIP = m.Agriculture_DRIP_NONDRIP,
                        Farmers_Agreement_Code = m.Farmers_Agreement_Code,
                        Farmers_No_of_Acres_Area = m.Farmers_No_of_Acres_Area
                    });
                }
            }
            return sowingFarmingFormDataResponse;
        }

        public async Task<SowingFarmingDataForFormRequiredForGrid> GetSowingFarmingDataForFormRequiredForGrid(DateTime sowingDate, string cropNameCode, string psNumber)
        {
            return await this.sowingFarmingRepository.GetSowingFarmingDataForFormRequiredForGrid(sowingDate, cropNameCode, psNumber);
        }

        public async Task<SowingFarmingInsert> InsertSowingFamring(SowingFarmingInsert sowingFarmingInsert)
        {
            return await this.sowingFarmingRepository.InserSowingFarmingDetails(sowingFarmingInsert);
        }
    }
}