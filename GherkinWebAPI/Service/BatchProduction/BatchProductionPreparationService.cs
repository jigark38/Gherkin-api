using GherkinWebAPI.Core;
using GherkinWebAPI.Core.BatchProduction;
using GherkinWebAPI.DTO;
using GherkinWebAPI.DTO.BankAccount;
using GherkinWebAPI.DTO.BatchProduction;
using GherkinWebAPI.DTO.Product_GradeDto;
using GherkinWebAPI.Entities;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.BatchProduction;
using GherkinWebAPI.Models.GRNAndMaterialClassification;
using GherkinWebAPI.Models.GRPAndroid.ProdProcessBOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service.BatchProduction
{
    public class BatchProductionPreparationService : IBatchProductionPreparationService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public IBatchProductionPreparationRepository _repository { get; }
        public BatchProductionPreparationService(IRepositoryWrapper repositoryWrapper, IBatchProductionPreparationRepository repository)
        {
            _repositoryWrapper = repositoryWrapper;
            _repository = repository;
        }
        public async Task<List<OrganisationOfficeLocationDetailsDto>> GetOrgofficelocationDetails()
        { 
            return await _repository.GetOrgofficelocationDetails();
        }

        public async Task<List<BatchGreenDetailsDto>> GetGreenReceivedByOrgOfficeNo(int orgOfficeNo)
        {
            return await _repository.GetGreenReceivedByOrgOfficeNo(orgOfficeNo);
        }
        public async Task<List<BatchGreenDetailsDto>> GetMediaProcessDetails()
        {
            return await _repository.GetMediaProcessDetails();
        }

        public async Task<List<EmployeeDTO>> GetScheduledByDetails()
        {
            return await _repository.GetScheduledByDetails();
        }

        public async Task<List<ProductGroupDto>> GetProductGroupDetails()
        {
            return await _repository.GetProductGroupDetails();
        }

        public async Task<List<ProductVarietyDto>> GetProductNameDetails(string groupCode)
        {
            return await _repository.GetProductNameDetails(groupCode);
        }

        public async Task<List<GradeDto>> GetGradeDetails(string varietyCode)
        {
            return await _repository.GetGradeDetails(varietyCode);
        }
        public async Task<List<BatchScheduledOrderDto>> GetScheduleOrderDetails(int orgOfficeNo)
        {
            return await _repository.GetScheduleOrderDetails(orgOfficeNo);
        }

        public async Task<List<BatchScheduledOrderDto>> GetScheduleOrderDetailsByMediaProcess(string mediaProcess)
        {
            return await _repository.GetScheduleOrderDetailsByMediaProcess(mediaProcess);
        }

        public async Task<dynamic> GetLatestBatchNo()
        {
            return await _repository.GetLatestBatchNo();
        }

        public async Task<BatchProductionDetails> SaveBatchProductionDetails(BatchProductionDetails batchProductionDetailsObject)
        {
            return await _repository.SaveBatchProductionDetails(batchProductionDetailsObject);
        }
    }
}