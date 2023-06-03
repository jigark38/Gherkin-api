using GherkinWebAPI.Core;
using GherkinWebAPI.Models;
using GherkinWebAPI.Persistence;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Linq;
using GherkinWebAPI.DTO;
using System.Data.SqlClient;
using AutoMapper;
using GherkinWebAPI.Request;
using GherkinWebAPI.CustomExceptions;
using System;
using GherkinWebAPI.Response.SupplierDetails;
using System.Data.Entity.Migrations;
using System.Text.RegularExpressions;

namespace GherkinWebAPI.Repository
{
    public class SupplierDetailsRepository : RepositoryBase<SupplierDetails>, ISupplierDetailsRepository
    {
        private RepositoryContext _context;

        public SupplierDetailsRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
            this._context = repositoryContext;
        }

        public async Task<SupplierDetailsResponse> AddSupplierDetails(SupplierDetailsRequest supplierDetailsreq)
        {
            SupplierDetailsResponse supplierDetailsResponse = new SupplierDetailsResponse();
            try
            {
                _context.SupplierDetails.Add(supplierDetailsreq.SupplierDetailsModel);
                await _context.SaveChangesAsync();
                //int id = supplierDetails.ID;
                var r = await _context.SupplierDetails.FirstAsync(x => x.ID == supplierDetailsreq.SupplierDetailsModel.ID);
                supplierDetailsResponse.SupplierDetailsDto = Mapper.Map<SupplierDetailsDto>(r);
                foreach (var item in supplierDetailsreq.SupplierDocumentDetails)
                {
                    byte[] docImage = null;
                    if (!string.IsNullOrEmpty(item.supplierDocumentDetails))
                    {
                        string ImgStr = Regex.Replace(item.supplierDocumentDetails, "^data:(image|pdf|application)\\/[a-zA-Z]*-?[a-zA-Z]+;base64,", string.Empty, RegexOptions.IgnoreCase);
                        docImage = Convert.FromBase64String(ImgStr);

                        var regexMatches = Regex.Matches(item.supplierDocumentDetails, "(^data:(image|pdf|application)\\/[a-zA-Z]*-?[a-zA-Z]+;base64,)", RegexOptions.IgnoreCase);
                        if (regexMatches != null)
                        {
                            var tt = regexMatches[0].Groups[0].Value;
                            item.supplierDocumentPreappendText = tt;
                        }
                    }
                    var supDocDetails = new SupplierDocumentDetails();
                    supDocDetails.supplierOrgID = r.supplierOrgID;
                    supDocDetails.supplierDocumentName = item.supplierDocumentName;
                    supDocDetails.supplierDocumentDetails = docImage;
                    supDocDetails.supplierDocumentPreappendText = item.supplierDocumentPreappendText;
                    _context.SupplierDocumentDetails.Add(supDocDetails);


                    //     var r1 = await _context.SupplierDocumentDetails.FirstAsync(x => x.supplierOrgID == r.supplierOrgID);
                    //     var itemDto = Mapper.Map<SupplierDocumentsDto>(r1);
                    //     supplierDetailsResponse.SupplierDocumentsDtos.Add(itemDto);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return supplierDetailsResponse;
        }

        public async Task<List<SupplierDetails>> GetAllSupplierOrgs()
        {
            return await FindAll().ToListAsync();
        }

        public async Task<SupplierDetailsResponse> UpdateSupplierDetails(SupplierDetailsRequest supplierDetailsReq)
        {
            SupplierDetailsResponse supDetRes = new SupplierDetailsResponse();
            try
            {
                if (supplierDetailsReq != null)
                {
                    var detail = await this._context.SupplierDetails.FirstOrDefaultAsync(x => x.supplierOrgID == supplierDetailsReq.SupplierDetailsModel.supplierOrgID);
                    detail.activity = supplierDetailsReq.SupplierDetailsModel.activity;
                    detail.address = supplierDetailsReq.SupplierDetailsModel.address;
                    detail.altCorrespondanceMailID = supplierDetailsReq.SupplierDetailsModel.altCorrespondanceMailID;
                    detail.approvedBy = supplierDetailsReq.SupplierDetailsModel.approvedBy;
                    detail.bankActNo = supplierDetailsReq.SupplierDetailsModel.bankActNo;
                    detail.bankBranch = supplierDetailsReq.SupplierDetailsModel.bankBranch;
                    detail.bankName = supplierDetailsReq.SupplierDetailsModel.contactPerson;
                    detail.contactPerson = supplierDetailsReq.SupplierDetailsModel.contactPerson;
                    detail.contactPersonDesignation = supplierDetailsReq.SupplierDetailsModel.contactPersonDesignation;
                    detail.contactPersonMailID = supplierDetailsReq.SupplierDetailsModel.contactPersonMailID;
                    detail.contactPersonNumber = supplierDetailsReq.SupplierDetailsModel.contactPersonNumber;
                    detail.correspondanceMailID = supplierDetailsReq.SupplierDetailsModel.correspondanceMailID;
                    detail.countryID = supplierDetailsReq.SupplierDetailsModel.countryID;
                    detail.creationDate = supplierDetailsReq.SupplierDetailsModel.creationDate;
                    detail.designation = supplierDetailsReq.SupplierDetailsModel.designation;
                    detail.gstNo = supplierDetailsReq.SupplierDetailsModel.gstNo;
                    detail.iFSCCode = supplierDetailsReq.SupplierDetailsModel.iFSCCode;
                    detail.legalStatus = supplierDetailsReq.SupplierDetailsModel.legalStatus;
                    detail.licenseNo = supplierDetailsReq.SupplierDetailsModel.licenseNo;
                    detail.mgmContactNumber = supplierDetailsReq.SupplierDetailsModel.mgmContactNumber;
                    detail.mgmName = supplierDetailsReq.SupplierDetailsModel.mgmName;
                    detail.officeNumber = supplierDetailsReq.SupplierDetailsModel.officeNumber;
                    detail.organisationName = supplierDetailsReq.SupplierDetailsModel.organisationName;
                    detail.pinCode = supplierDetailsReq.SupplierDetailsModel.pinCode;
                    detail.stateID = supplierDetailsReq.SupplierDetailsModel.stateID;
                    detail.placeCode = supplierDetailsReq.SupplierDetailsModel.placeCode;
                    detail.districtID = supplierDetailsReq.SupplierDetailsModel.districtID;
                    detail.userName = supplierDetailsReq.SupplierDetailsModel.userName;
                    detail.website = supplierDetailsReq.SupplierDetailsModel.website;
                    _context.SupplierDetails.AddOrUpdate(detail);
                    await _context.SaveChangesAsync();



                }
                var r = await _context.SupplierDetails.FirstAsync(x => x.supplierOrgID == supplierDetailsReq.SupplierDetailsModel.supplierOrgID);
                supDetRes.SupplierDetailsDto = Mapper.Map<SupplierDetailsDto>(r);

                if (supplierDetailsReq.SupplierDocumentDetails.Count > 0)
                {
                    foreach (var item in supplierDetailsReq.SupplierDocumentDetails)
                    {
                        if (item.supplierOrgDocNo == 0)
                        {
                            byte[] docImage = null;
                            if (!string.IsNullOrEmpty(item.supplierDocumentDetails))
                            {
                                string ImgStr = Regex.Replace(item.supplierDocumentDetails, "^data:(image|pdf|application)\\/[a-zA-Z]*-?[a-zA-Z]+;base64,", string.Empty, RegexOptions.IgnoreCase);
                                docImage = Convert.FromBase64String(ImgStr);

                                var regexMatches = Regex.Matches(item.supplierDocumentDetails, "(^data:(image|pdf|application)\\/[a-zA-Z]*-?[a-zA-Z]+;base64,)", RegexOptions.IgnoreCase);
                                if (regexMatches != null)
                                {
                                    var tt = regexMatches[0].Groups[0].Value;
                                    item.supplierDocumentPreappendText = tt;
                                }
                            }
                            var supDocDetails = new SupplierDocumentDetails();
                            supDocDetails.supplierOrgID = r.supplierOrgID;
                            supDocDetails.supplierDocumentName = item.supplierDocumentName;
                            supDocDetails.supplierDocumentDetails = docImage;
                            supDocDetails.supplierDocumentPreappendText = item.supplierDocumentPreappendText;
                            _context.SupplierDocumentDetails.Add(supDocDetails);
                            await _context.SaveChangesAsync();
                        }
                    }
                }

                return supDetRes;

                //var r = await this._context.SupplierDetails.FirstAsync(x => x.ID == detail.ID);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<SupplierDetailsResponse> GetSupplierDetailsByID(string SupplierOrgID)
        {
            SupplierDetailsResponse supDetRes = new SupplierDetailsResponse();

            try
            {
                // SupplierDetails sddetails = await _context.SupplierDetails.FirstAsync(x => x.supplierOrgID == SupplierOrgID);

                var data = await (from sd in _context.SupplierDetails
                                  join c in _context.Countries on sd.countryID equals c.Country_Code
                                  join s in _context.States on sd.stateID equals s.State_Code
                                  join d in _context.Districts on sd.districtID equals d.District_Code
                                  join p in _context.Places on sd.placeCode equals p.PlaceCode
                                  where sd.supplierOrgID == SupplierOrgID
                                  select new SupplierDetailsDto
                                  {
                                      ID = sd.ID,
                                      supplierOrgID = sd.supplierOrgID,
                                      creationDate = sd.creationDate,
                                      userName = sd.userName,
                                      organisationName = sd.organisationName,
                                      legalStatus = sd.legalStatus,
                                      address = sd.address,
                                      countryID = sd.countryID,
                                      stateID = sd.stateID,
                                      districtID = sd.districtID,
                                      placeCode = sd.placeCode,
                                      pinCode = sd.pinCode,
                                      mgmName = sd.mgmName,
                                      designation = sd.designation,
                                      mgmContactNumber = sd.mgmContactNumber,
                                      correspondanceMailID = sd.correspondanceMailID,
                                      altCorrespondanceMailID = sd.altCorrespondanceMailID,
                                      contactPerson = sd.contactPerson,
                                      contactPersonDesignation = sd.contactPersonDesignation,
                                      contactPersonNumber = sd.contactPersonNumber,
                                      contactPersonMailID = sd.contactPersonMailID,
                                      officeNumber = sd.officeNumber,
                                      activity = sd.activity,
                                      gstNo = sd.gstNo,
                                      website = sd.website,
                                      licenseNo = sd.licenseNo,
                                      bankName = sd.bankBranch,
                                      bankBranch = sd.bankBranch,
                                      bankActNo = sd.bankActNo,
                                      iFSCCode = sd.iFSCCode,
                                      approvedBy = sd.approvedBy,
                                      districtName = d.District_Name,
                                      countryName = c.Country_Name,
                                      stateName = s.State_Name,
                                      placeName = p.PlaceName
                                  }).ToListAsync();

                supDetRes.SupplierDetailsDto = data.FirstOrDefault();
                //Mapper.Map<SupplierDetailsDto>(data.FirstOrDefault());

                var r1 = await _context.SupplierDocumentDetails.Where(x => x.supplierOrgID == SupplierOrgID).ToListAsync();
                foreach (var item in r1)
                {
                    var itemDto = Mapper.Map<SupplierDocumentsDto>(item);
                    supDetRes.SupplierDocumentsDtos.Add(itemDto);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return supDetRes;


        }
    }
}