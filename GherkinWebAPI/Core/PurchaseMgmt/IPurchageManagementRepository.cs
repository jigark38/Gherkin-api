using GherkinWebAPI.DTO.PurchageMgmt;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.GoodsReceiptNote;
using GherkinWebAPI.Models.PurchageMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.PurchaseMgmt
{
    public interface IPurchageManagementRepository
    {
        Task<CreatePOWithMaterialAndCondition> CreatePurchaseOrder(CreatePOWithMaterialAndCondition poDetail);
        Task<List<PurchaseOrderDetail>> GetAllPurchaseOrder();
        Task<List<IndentDetail>> GetAllIndentDetails();
        Task<int> GetNextPurchageOrderId();
        Task<List<State>> GetStatesBySuppOrgId(string suppOrgId);
        Task<List<Place>> GetPlacesBySuppOrgId(string suppOrgId);
        Task<List<Country>> GetCountryBySppOrgId(string suppOrgId);
        Task<List<SupplierDetails>> GetAllSuppliers();
        Task<RMPOMaterialDetail> CreateOrderMaterial(RMPOMaterialDetail rmPoDetail);
        Task<List<RMPOMaterialDetail>> GetAllRMPOMaterial();
        Task<TaxPercentageRate> GetTaxPercentByGSTType(string detailCode, string gstType);
        Task<List<PendingPurchaseOrder>> GetPendingPurchaseOrdersAsync();
        Task<SupplierDetails> GetSupplierOrgNameByRMGRNNo(string rmGrnNo);
        Task<Place> GetPlaceNameBySuppOrgId(string supOrgId);
        Task<RMGRNDetail> GetRMGrnDetailsByRMGrnNo(string v);
        Task<List<PurchageReceivedDetail>> GetPurchageRecievedDetails(string suppOrgId);
        Task<List<PurchaseReceivedMaterialDetail>> GetMaterialRecievedDetails(string rmGrnNo);
        Task<CreatePurchageReturn> CreatePurchaseReturn(CreatePurchageReturn pReturnDetail);
        Task<List<string>> GetOrderIdsBySuppOrgId(string SuppOrgId);
        Task<CreatePOWithMaterialAndCondition> GetPurchaseOrderByID(string RmPoNo);
        Task<CreatePOWithMaterialAndCondition> ModifyPurchaseOrder(CreatePOWithMaterialAndCondition poDetail);
        Task<List<string>> GetPurchaseReturnIdsBySuppOrgId(string SuppOrgId);
        Task<CreatePurchageReturn> ModifyPurchaseReturn(CreatePurchageReturn poDetail);
        Task<CreatePurchageReturn> FindPurchaseReturnById(string purchaseReturnID);
    }
}
