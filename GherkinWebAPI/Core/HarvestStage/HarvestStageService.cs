using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Models.HarvestStage;
using GherkinWebAPI.Request.HarvestStage;
using GherkinWebAPI.Response;
using GherkinWebAPI.Response.HarvestStage;

namespace GherkinWebAPI.Core.HarvestStage
{
    public class HarvestStageService : IHarvestStageService
    {
        private readonly ICropService cropService;
        private readonly IEmployeeService employeeService;
        private readonly IHarvestStageRepository harvestStageRepository;


        public HarvestStageService(ICropService cropService, IEmployeeService employeeService, IHarvestStageRepository harvestStageRepository)
        {
            this.cropService = cropService;
            this.employeeService = employeeService;
            this.harvestStageRepository = harvestStageRepository;
        }

		public async Task<HarvestStageShowResponse> GetHarvestStageFormDataAsync()
        {
            HarvestStageShowResponse harvestStageShowResponse = new HarvestStageShowResponse();
            try
            {
                harvestStageShowResponse.CropGroups = Mapper.Map<List<CropGroupDto>>(await cropService.GetAllCropGroup());
                harvestStageShowResponse.Employees = await employeeService.GetAllEmployee();
                harvestStageShowResponse.Crops = Mapper.Map<List<CropDto>>(await cropService.GetAllCrops());
            }
            catch (Exception ex)
            {
                harvestStageShowResponse = null;
            }
            return harvestStageShowResponse;
        }

        public async Task<HarvestStageResponse> InsertHarvestStages(HarvestStageInsertRequest harvestStageInsertRequest)
        {
            HarvestStageResponse harvestStageResponse = await harvestStageRepository.InsertHarvestStages(harvestStageInsertRequest);
            return harvestStageResponse;
        }

        public async Task<List<EffectiveDateForHarvestDetails>> GetEffectiveDateList(string cropNameCode)
        {
            return await harvestStageRepository.GetEffectiveDateList(cropNameCode);
        }

		public async Task<HarvestStageInsertRequest> GetHarvestStageDetails(string hsTransactionCode)
		{
            return await harvestStageRepository.GetHarvestStageDetails(hsTransactionCode);
		}
	}
}