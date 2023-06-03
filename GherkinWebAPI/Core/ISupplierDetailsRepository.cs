using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;
using GherkinWebAPI.Request;
using GherkinWebAPI.Response.SupplierDetails;

namespace GherkinWebAPI.Core
{
    public interface ISupplierDetailsRepository : IRepositoryBase<SupplierDetails>
    {
        Task<SupplierDetailsResponse> AddSupplierDetails(SupplierDetailsRequest supplierDetailsreq);
        
        Task<SupplierDetailsResponse> GetSupplierDetailsByID(string Supplier_Org_ID);

        Task<List<SupplierDetails>> GetAllSupplierOrgs();

        Task<SupplierDetailsResponse> UpdateSupplierDetails(SupplierDetailsRequest supplierDetailsReq);
    }
}
