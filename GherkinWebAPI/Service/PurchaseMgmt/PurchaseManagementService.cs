using GherkinWebAPI.Core.PurchaseMgmt;
using GherkinWebAPI.DTO.PurchageMgmt;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.GoodsReceiptNote;
using GherkinWebAPI.Models.PurchageMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service.PurchaseMgmt
{
    public class PurchaseManagementService : IPurchageManagementService
    {
        private readonly IPurchageManagementRepository _repository;
        public PurchaseManagementService(IPurchageManagementRepository repository)
        {
            _repository = repository;
        }

        public async Task<CreatePOWithMaterialAndCondition> CreatePurchaseOrder(CreatePOWithMaterialAndCondition poDetail)
        {
            return await _repository.CreatePurchaseOrder(poDetail);
        }

        public async Task<List<PurchaseOrderDetail>> GetAllPurchaseOrder()
        {
            return await _repository.GetAllPurchaseOrder();
        }

        public async Task<List<IndentDetail>> GetAllIndentDetails()
        {
            return await _repository.GetAllIndentDetails();
        }

        public async Task<int> GetNextPurchageOrderId()
        {
            return await _repository.GetNextPurchageOrderId();
        }

        public async Task<List<Place>> GetPlacesBySuppOrgId(string suppOrgId)
        {
            return await _repository.GetPlacesBySuppOrgId(suppOrgId);
        }

        public async Task<List<State>> GetStatesBySuppOrgId(string suppOrgId)
        {
            return await _repository.GetStatesBySuppOrgId(suppOrgId);
        }

        public async Task<List<Country>> GetCountryBySppOrgId(string suppOrgId)
        {
            return await _repository.GetCountryBySppOrgId(suppOrgId);
        }

        public async Task<List<SupplierDetails>> GetAllSuppliers()
        {
            return await _repository.GetAllSuppliers();
        }

        public async Task<RMPOMaterialDetail> CreateOrderMaterial(RMPOMaterialDetail rmPoDetail)
        {
            return await _repository.CreateOrderMaterial(rmPoDetail);
        }

        public async Task<List<RMPOMaterialDetail>> GetAllRMPOMaterial()
        {
            return await _repository.GetAllRMPOMaterial();
        }

        public async Task<TaxPercentageRate> GetTaxPercentByGSTType(string detailCode, string gstType)
        {
            return await _repository.GetTaxPercentByGSTType(detailCode, gstType);
        }

        public async Task<List<PendingPurchaseOrder>> GetPendingPurchaseOrdersAsync()
        {
            return await _repository.GetPendingPurchaseOrdersAsync();
        }

        public async Task<SupplierDetails> GetSupplierOrgNameByRMGRNNo(string rmGrnNo)
        {
            return await _repository.GetSupplierOrgNameByRMGRNNo(rmGrnNo);
        }

        public async Task<Place> GetPlaceNameBySuppOrgId(string supOrgId)
        {
            return await _repository.GetPlaceNameBySuppOrgId(supOrgId);
        }

        public async Task<RMGRNDetail> GetRMGrnDetailsByRMGrnNo(string rmGrnNo)
        {
            return await _repository.GetRMGrnDetailsByRMGrnNo(rmGrnNo.Trim());
        }

        public async Task<List<PurchageReceivedDetail>> GetPurchageRecievedDetails(string suppOrgId)
        {
            return await _repository.GetPurchageRecievedDetails(suppOrgId);
        }

        public async Task<List<PurchaseReceivedMaterialDetail>> GetMaterialRecievedDetails(string rmGrnNo)
        {
            return await _repository.GetMaterialRecievedDetails(rmGrnNo);
        }

        public async Task<CreatePurchageReturn> CreatePurchaseReturn(CreatePurchageReturn pReturnDetail)
        {
            return await _repository.CreatePurchaseReturn(pReturnDetail);
        }

        public async Task<List<string>> GetOrderIdsBySuppOrgId(string SuppOrgId)
        {
            return await _repository.GetOrderIdsBySuppOrgId(SuppOrgId);
        }
        public async Task<CreatePOWithMaterialAndCondition> GetPurchaseOrderByID(string RmPoNo)
        {
            return await _repository.GetPurchaseOrderByID(RmPoNo);
        }

        public async Task<CreatePOWithMaterialAndCondition> ModifyPurchaseOrder(CreatePOWithMaterialAndCondition poDetail)
        {
            return await _repository.ModifyPurchaseOrder(poDetail);
        }
        public async Task<List<string>> GetPurchaseReturnIdsBySuppOrgId(string SuppOrgId)
        {
            return await _repository.GetPurchaseReturnIdsBySuppOrgId(SuppOrgId);
        }
        public async Task<CreatePurchageReturn> ModifyPurchaseReturn(CreatePurchageReturn poDetail)
        {
            return await _repository.ModifyPurchaseReturn(poDetail);
        }
        public async Task<CreatePurchageReturn> FindPurchaseReturnById(string purchaseReturnID)
        {
            return await _repository.FindPurchaseReturnById(purchaseReturnID);
        }
      }
}