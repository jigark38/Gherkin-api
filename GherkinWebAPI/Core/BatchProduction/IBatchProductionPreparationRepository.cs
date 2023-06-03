using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GherkinWebAPI.DTO;
using GherkinWebAPI.DTO.BatchProduction;
using GherkinWebAPI.DTO.Product_GradeDto;
using GherkinWebAPI.Entities;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.BatchProduction;
using GherkinWebAPI.Models.GRNAndMaterialClassification;
using GherkinWebAPI.Models.GRPAndroid.ProdProcessBOM;

namespace GherkinWebAPI.Core.BatchProduction
{
   public interface IBatchProductionPreparationRepository
    {
        Task<List<OrganisationOfficeLocationDetailsDto>> GetOrgofficelocationDetails();
        Task<List<BatchGreenDetailsDto>> GetGreenReceivedByOrgOfficeNo(int orgOfficeNo);
        Task<List<BatchGreenDetailsDto>> GetMediaProcessDetails();
        Task<List<BatchScheduledOrderDto>> GetScheduleOrderDetails(int orgOfficeNo);
        Task<List<BatchScheduledOrderDto>> GetScheduleOrderDetailsByMediaProcess(string mediaprocessCode);
        Task<List<EmployeeDTO>> GetScheduledByDetails();
        Task<List<ProductGroupDto>> GetProductGroupDetails();
        Task<List<ProductVarietyDto>> GetProductNameDetails(string groupCode);
        Task<List<GradeDto>> GetGradeDetails(string varietyCode);

        Task<dynamic> GetLatestBatchNo();

        Task<BatchProductionDetails> SaveBatchProductionDetails(BatchProductionDetails batchProductionDetailsObject);
    }
}

