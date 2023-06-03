using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;
using GherkinWebAPI.Request;
using GherkinWebAPI.Response.SupplierDetails;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core
{
    public interface ISupplierDetailsService
    {
        //Task AddSupplierDetails(SupplierDetails supplierDetails);

        Task<SupplierDetailsResponse> AddSupplierDetails(SupplierDetailsRequest supplierDetailsreq);

        Task<SupplierDetailsResponse> GetSupplierDetailsByID(string Supplier_Org_ID);

        Task<List<SupplierDetails>> GetAllSupplierOrgs();


        Task<SupplierDetailsResponse> UpdateSupplierDetails(SupplierDetailsRequest supplierDetailsReq);
    }
}
