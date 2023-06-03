using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GherkinWebAPI.DTO;
using GherkinWebAPI.DTO.BatchProduction;
using GherkinWebAPI.DTO.Product_GradeDto;
using GherkinWebAPI.Entities;
using GherkinWebAPI.Models.BatchProduction;
using GherkinWebAPI.Models.GRNAndMaterialClassification;
using GherkinWebAPI.Models.GRPAndroid.ProdProcessBOM;

namespace GherkinWebAPI.Core.BatchProduction
{
    public interface IBatchProductionPreparationService
    {
        Task<List<OrganisationOfficeLocationDetailsDto>> GetOrgofficelocationDetails();
        Task<List<BatchGreenDetailsDto>> GetGreenReceivedByOrgOfficeNo(int orgOfficeNo);
        Task<List<BatchGreenDetailsDto>> GetMediaProcessDetails();
        Task<List<EmployeeDTO>> GetScheduledByDetails();
        Task<List<ProductGroupDto>> GetProductGroupDetails();
        Task<List<ProductVarietyDto>> GetProductNameDetails(string groupCode);
        Task<List<GradeDto>> GetGradeDetails(string varietyCode);
        Task<List<BatchScheduledOrderDto>> GetScheduleOrderDetails(int orgOfficeNo);
        Task<List<BatchScheduledOrderDto>> GetScheduleOrderDetailsByMediaProcess(string mediaprocessCode);

        Task<dynamic> GetLatestBatchNo();

        Task<BatchProductionDetails> SaveBatchProductionDetails(BatchProductionDetails batchProductionDetailsObject);

    }
}
