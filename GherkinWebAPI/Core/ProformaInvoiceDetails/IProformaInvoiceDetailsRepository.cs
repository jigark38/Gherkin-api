using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GherkinWebAPI.DTO.ProformaInvoiceDetail;
using GherkinWebAPI.Models;


namespace GherkinWebAPI.Core
{
    public interface IProformaInvoiceDetailsRepository { 
        Task<string> GetProformaInvoiceId();

        Task<ProformaInvoiceDetails> AddProfromaDetails(ProformaInvoice proformaInvoice);
        Task<ProductionDetails> AddProductionDetails(ProductionDetails productionDetails);

        Task<string> GetProformaProductId();

        Task<List<ProductDetailsDto>> GetProductionDetails();

        Task<int> GetProductionScheduleId();

        Task<ProductionSchedule> AddProductionScheduleDetails(ProductionSchedule productionSchedule);

    }
}

