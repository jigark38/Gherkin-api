using AutoMapper;
using GherkinWebAPI.Core.GreensAgentSupplierDetails;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.GreensAgentSupplierDetails;
using GherkinWebAPI.Persistence;
using GherkinWebAPI.Request.GreensAgentSupplierDetails;
using GherkinWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Repository.GreensAgentSupplierDetails
{
    public class GreensAgentSupplierDetailsRepository : RepositoryBase<SupplierInformationDetails>, IGreensAgentSupplierDetailsRepository
    {
        private RepositoryContext _context;
        public GreensAgentSupplierDetailsRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            _context = repositoryContext;
        }

        public async Task<ApiResponse<object>> SaveGreensAgentSupplierDetails(SupplierInformationDetailsRequest supplierInfoDetail)
        {

            ApiResponse<object> result = new ApiResponse<object>();
            try
            {
                supplierInfoDetail.placeCode = getPlaceCode(supplierInfoDetail.placeName, supplierInfoDetail.DistrictCode, supplierInfoDetail.StateCode, supplierInfoDetail.CountryCode);

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<AgentBankDetailsRequest, AgentBankDetails>();
                    cfg.CreateMap<AgentOrgDocumentsRequest, AgentOrgDocuments>();
                    cfg.CreateMap<SupplierInformationDetailsRequest, SupplierInformationDetails>().ForMember(dest => dest.AgentBankDetailsList, act => act.MapFrom(src => src.AgentBankDetailsList));
                    cfg.CreateMap<SupplierInformationDetailsRequest, SupplierInformationDetails>().ForMember(dest => dest.AgentOrgDocumentsList, act => act.MapFrom(src => src.AgentOrgDocumentsList));
                });

                var mapper = new Mapper(config);
                var suppInformationDetail = new SupplierInformationDetails();
                suppInformationDetail = mapper.DefaultContext.Mapper.Map<SupplierInformationDetails>(supplierInfoDetail);

                _context.SupplierInformationDetails.Add(suppInformationDetail);
                await _context.SaveChangesAsync();
                result.IsSucceed = true;
            }
            catch (Exception ex)
            {
                result.IsSucceed = false;
                result.Exception = ex;
                result.ErrorMessages = new List<string>();
                result.ErrorMessages.Add(ex.Message);
            }

            return result;
        }

        public async Task<ApiResponse<List<AgentOrgDetailsRequest>>> GetAgentOrganisationDetails()
        {
            ApiResponse<List<AgentOrgDetailsRequest>> result = new ApiResponse<List<AgentOrgDetailsRequest>>();
            try
            {
                var AgentOrganisationDetailsList = (from f in _context.SupplierInformationDetails
                                                    select new AgentOrgDetailsRequest
                                                    {
                                                        AgentOrgID = f.AgentOrgID,
                                                        AgentOrganisationName = f.AgentOrganisationName,
                                                    }).OrderBy(a => a.AgentOrganisationName).ToListAsync();
                result.IsSucceed = true;
                result.Data = await AgentOrganisationDetailsList;
            }
            catch (Exception ex)
            {
                result.IsSucceed = false;
                result.Exception = ex;
                result.ErrorMessages = new List<string>();
                result.ErrorMessages.Add(ex.Message);
            }
            return result;
        }

        public async Task<ApiResponse<object>> SaveBankAccountDetails(List<AgentBankDetailsRequest> agntBankDetailList)
        {
            ApiResponse<object> result = new ApiResponse<object>();
            try
            {                
                List<AgentBankDetails> bankDetail = Mapper.Map<List<AgentBankDetails>>(agntBankDetailList);
                var existingBankDetail =  _context.AgentBankDetails.AddRange(bankDetail);
                await _context.SaveChangesAsync();
                result.IsSucceed = true;
            }
            catch (Exception ex)
            {
                result.IsSucceed = false;
                result.Exception = ex;
                result.ErrorMessages = new List<string>();
                result.ErrorMessages.Add(ex.Message);
            }
            return result;
        }

        public async Task<ApiResponse<SupplierInformationDetailsRequest>> GetSupplierInformationDetail(int AgentOrgID)
        {
            ApiResponse<SupplierInformationDetailsRequest> result = new ApiResponse<SupplierInformationDetailsRequest>();
            try
            {
                var supplierInformationDetail = await (from si in _context.SupplierInformationDetails
                                                       join sdd in _context.AgentOrgDocuments on si.AgentOrgID equals sdd.AgentOrgID into agntOrg
                                                       from sdd in agntOrg.DefaultIfEmpty()
                                                       join sbd in _context.AgentBankDetails on si.AgentOrgID equals sbd.AgentOrgID into agntBank
                                                       from sbd in agntBank.DefaultIfEmpty()
                                                       where si.AgentOrgID == AgentOrgID
                                                       select new SupplierInformationDetailsRequest
                                                       {
                                                           AgentOrgID = si.AgentOrgID,
                                                           AgentCreationDate = si.AgentCreationDate,
                                                           EmpCreatedID = si.EmpCreatedID,
                                                           AgentOrganisationName = si.AgentOrganisationName,
                                                           AgentOrganisationLegalStatus = si.AgentOrganisationLegalStatus,
                                                           AgentOrganisationAddress = si.AgentOrganisationAddress,
                                                           CountryCode = si.CountryCode,
                                                           StateCode = si.StateCode,
                                                           DistrictCode = si.DistrictCode,
                                                           placeCode = si.placeCode,
                                                           placeName = _context.Places.Where(a=>a.PlaceCode == si.placeCode).FirstOrDefault().PlaceName,
                                                           AgentPINCode = si.AgentPINCode,
                                                           AgentManagementName = si.AgentManagementName,
                                                           AgentManagementDesignation = si.AgentManagementDesignation,
                                                           AgentManagementCN = si.AgentManagementCN,
                                                           AgentManagementMID = si.AgentManagementMID,
                                                           AgentOrganisationOfficeNumber = si.AgentOrganisationOfficeNumber,
                                                           AgentOrganisationActivity = si.AgentOrganisationActivity,
                                                           AgentOrganisationGSTN = si.AgentOrganisationGSTN,
                                                           AgentOrganisationWebsite = si.AgentOrganisationWebsite,
                                                           AgentBankDetailsList = (from f in si.AgentBankDetailsList
                                                                                   select new AgentBankDetailsRequest
                                                                                   {
                                                                                       AgentBankCode = f.AgentBankCode,
                                                                                       AgentOrgID = f.AgentOrgID,
                                                                                       AgentOrganisationBankName = f.AgentOrganisationBankName,
                                                                                       AgentOrganisationBankBranch = f.AgentOrganisationBankBranch,
                                                                                       AgentOrganisationBankAccountNo = f.AgentOrganisationBankAccountNo,
                                                                                       AgentOrganisationBankIFSC = f.AgentOrganisationBankIFSC,
                                                                                       PreferredBank = f.PreferredBank
                                                                                   }).ToList(),
                                                           AgentOrgDocumentsList = (from d in si.AgentOrgDocumentsList
                                                                                    select new AgentOrgDocumentsRequest
                                                                                    {
                                                                                        AgentOrgDocNo = d.AgentOrgDocNo,
                                                                                        AgentOrgID = d.AgentOrgID,
                                                                                        AgentDocumentName = d.AgentDocumentName,
                                                                                        AgentDocumentDetails = d.AgentDocumentDetails,
                                                                                    }).ToList()
                                                       }).FirstOrDefaultAsync();
                result.IsSucceed = true;
                result.Data = supplierInformationDetail;

            }
            catch (Exception ex)
            {

                result.IsSucceed = false;
                result.Exception = ex;
                result.ErrorMessages = new List<string>();
                result.ErrorMessages.Add(ex.Message);
            }
            return result;
        }

        private int getPlaceCode(string placeName, int districtCode, int stateCode, int countryCode)
        {
            string placName = placeName.Trim().ToLower();
            var place = _context.Places.Where(x => x.PlaceName == placeName && x.DistrictCode == districtCode).FirstOrDefault();
            if (place != null)
            {
                return place.PlaceCode;
            }
            else
            {
                var placeModel = new Place();
                placeModel.PlaceName = placeName;
                placeModel.CountryCode = countryCode;
                placeModel.StateCode = stateCode;
                placeModel.DistrictCode = districtCode;
                _context.Places.Add(placeModel);
                _context.SaveChanges();
                return placeModel.PlaceCode;
            }
        }

        public async Task<ApiResponse<AgentOrgDocuments>> GetDocumentByDocId(int docId)
        {
            ApiResponse<AgentOrgDocuments> result = new ApiResponse<AgentOrgDocuments>();
            try
            {
                result.Data = await _context.AgentOrgDocuments.Where(e => e.AgentOrgDocNo == docId).FirstOrDefaultAsync();
                result.IsSucceed = true;
            }
            catch (Exception ex) {
                result.IsSucceed = false;
                result.Exception = ex;
                result.ErrorMessages = new List<string>();
                result.ErrorMessages.Add(ex.Message);
            }
            return result;
        }

        public async Task<ApiResponse<object>> DeleteDocumentByDocId(int docId)
        {
            ApiResponse<object> result = new ApiResponse<object>();
            try
            {
                var data = await _context.AgentOrgDocuments.Where(e => e.AgentOrgDocNo == docId).FirstOrDefaultAsync();
                _context.AgentOrgDocuments.Remove(data);
                _context.SaveChanges();
                result.IsSucceed = true;
            }
            catch(Exception ex)
            {
                result.IsSucceed = false;
                result.Exception = ex;
                result.ErrorMessages = new List<string>();
                result.ErrorMessages.Add(ex.Message);
            }
            return result;
        }

        public async Task<ApiResponse<object>> ModifyGreensAgentSupplierDetails(SupplierInformationDetailsRequest suppInfoReq)
        {
            ApiResponse<object> result = new ApiResponse<object>();
            try
            {              
                var suppInfo = await _context.SupplierInformationDetails.Where(x => x.AgentOrgID == suppInfoReq.AgentOrgID).FirstOrDefaultAsync();
                                                           suppInfo.AgentOrgID = suppInfoReq.AgentOrgID;
                                                           suppInfo.AgentCreationDate = suppInfoReq.AgentCreationDate;
                                                           suppInfo.EmpCreatedID = suppInfoReq.EmpCreatedID;
                                                           suppInfo.AgentOrganisationName = suppInfoReq.AgentOrganisationName;
                                                           suppInfo.AgentOrganisationLegalStatus = suppInfoReq.AgentOrganisationLegalStatus;
                                                           suppInfo.AgentOrganisationAddress = suppInfoReq.AgentOrganisationAddress;
                                                           suppInfo.CountryCode = suppInfoReq.CountryCode;
                                                           suppInfo.StateCode = suppInfoReq.StateCode;
                                                           suppInfo.DistrictCode = suppInfoReq.DistrictCode;
                                                           suppInfo.placeCode = suppInfoReq.placeCode != 0? suppInfoReq.placeCode: 0;
                                                           suppInfo.AgentPINCode = suppInfoReq.AgentPINCode;
                                                           suppInfo.AgentManagementName = suppInfoReq.AgentManagementName;
                                                           suppInfo.AgentManagementDesignation = suppInfoReq.AgentManagementDesignation;
                                                           suppInfo.AgentManagementCN = suppInfoReq.AgentManagementCN;
                                                           suppInfo.AgentManagementMID = suppInfoReq.AgentManagementMID;
                                                           suppInfo.AgentOrganisationOfficeNumber = suppInfoReq.AgentOrganisationOfficeNumber;
                                                           suppInfo.AgentOrganisationActivity = suppInfoReq.AgentOrganisationActivity;
                                                           suppInfo.AgentOrganisationGSTN = suppInfoReq.AgentOrganisationGSTN;
                                                           suppInfo.AgentOrganisationWebsite = suppInfoReq.AgentOrganisationWebsite;
                _context.SaveChanges();
                var agntOrg = new List<AgentOrgDocuments>();
                foreach(var item in suppInfoReq.AgentOrgDocumentsList)
                {
                    if(item.AgentOrgDocNo == 0)
                    {
                        var agentOrgDoc = new AgentOrgDocuments();
                        agentOrgDoc.AgentOrgID = suppInfoReq.AgentOrgID;
                        agentOrgDoc.AgentDocumentDetails = item.AgentDocumentDetails;
                        agentOrgDoc.AgentDocumentName = item.AgentDocumentName;
                        agntOrg.Add(agentOrgDoc);
                    }
                }
                _context.AgentOrgDocuments.AddRange(agntOrg);
                _context.SaveChanges();
                result.IsSucceed = true;
            }
            catch(Exception ex)
            {
                result.IsSucceed = false;
                result.Exception = ex;
                result.ErrorMessages = new List<string>();
                result.ErrorMessages.Add(ex.Message);
            }
            return result;
        }

    }


}