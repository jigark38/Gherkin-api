using GherkinWebAPI.Core;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;
using GherkinWebAPI.Request;
using GherkinWebAPI.Response.SupplierDetails;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Service
{
    public class SupplierDetailsService : ISupplierDetailsService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public SupplierDetailsService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public Task<SupplierDetailsResponse> AddSupplierDetails(SupplierDetailsRequest supplierDetailsreq)
        {
            return _repositoryWrapper.SupplierDetails.AddSupplierDetails(supplierDetailsreq);
        }

        public Task<List<SupplierDetails>> GetAllSupplierOrgs()
        {
            return _repositoryWrapper.SupplierDetails.GetAllSupplierOrgs();
        }

        public Task<SupplierDetailsResponse> UpdateSupplierDetails(SupplierDetailsRequest supplierDetailsReq)
        {
            return _repositoryWrapper.SupplierDetails.UpdateSupplierDetails(supplierDetailsReq);
        }

        public Task<SupplierDetailsResponse> GetSupplierDetailsByID(string SupplierOrgID)
        {
            return _repositoryWrapper.SupplierDetails.GetSupplierDetailsByID(SupplierOrgID);
        }

    }
}