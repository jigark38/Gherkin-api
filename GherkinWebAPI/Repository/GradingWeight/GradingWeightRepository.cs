using GherkinWebAPI.Core;
using GherkinWebAPI.Entities;
using GherkinWebAPI.Models;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Data.Entity;
using GherkinWebAPI.Utilities;
using GherkinWebAPI.DTO;
using AutoMapper;

namespace GherkinWebAPI.Repository
{
    public class GradingWeightRepository : RepositoryBase<GreensGradingQuantityDetails>, IGradingWeightRepository
    {
        private RepositoryContext _context;
        public GradingWeightRepository(RepositoryContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ApiResponse<List<GridOneResponse>>> GetGridOneData(int OrgofficeNo)
        {
            ApiResponse<List<GridOneResponse>> result = new ApiResponse<List<GridOneResponse>>();
            try
            {

                var subqry = await (from aa in _context.GreensGradedHarvestGRNDetails
                                    select aa.Harvest_GRN_No).ToListAsync();

                var ongoing = await (from aa in _context.GreensGradedHarvestGRNDetails
                                     select aa.STATUS).ToListAsync();


                List<GridOneResponse> gridOneResponsesGRN = new List<GridOneResponse>();

                gridOneResponsesGRN = (from a in _context.HarvestGRNInwardDetails
                                    where a.Org_office_No == OrgofficeNo

                                    join b in _context.HarvestGRNs on a.Harvest_GRN_No equals b.HarvestGRNNo
                                    join c in _context.HarvestGRNInwardMaterialDetails on a.Harvest_GRN_No equals c.Harvest_GRN_No into d
                                    from dc in d.Select(x => x.Crop_Name_Code).Distinct()
                                    join harvestGrnDetail in _context.GreensGradedHarvestGRNDetails on a.Harvest_GRN_No equals harvestGrnDetail.Harvest_GRN_No into z
                                    from s in z.DefaultIfEmpty() where s.STATUS != false
                                    join dd in _context.Areas on a.Area_ID equals dd.Area_ID
                                    join ee in _context.Crops on dc equals ee.CropCode
                                    where a.Harvest_GRN_No != 0
                                    //orderby (DateTime.Now - b.HarvestGRNDate)
                                    orderby b.HarvestGRNDate
                                    orderby a.Harvest_GRN_No
                                    select new GridOneResponse
                                    {
                                        HarvestGRNNo = a.Harvest_GRN_No,
                                        AreaID = a.Area_ID,
                                        TotalReceivedCrates = a.Total_Received_Crates,
                                        TotalReceivedQty = a.Total_Received_Qty,
                                        HarvestGRNDate = b.HarvestGRNDate,
                                        CropNameCode = dc,
                                        AreaName = dd.Area_Name,
                                        CropName = ee.Name,
                                        IsOnGoing = s.STATUS,
                                        GreensGradeNo = s.Greens_Grade_No,
                                        GreensProcurementNo = s.Greens_Procurement_No
                                    }).ToList();

                List<GridOneResponse> gridOneResponsesProcurement = new List<GridOneResponse>();

                gridOneResponsesProcurement = (from a in _context.HarvestGRNInwardDetails
                                       where a.Org_office_No == OrgofficeNo

                                       join b in _context.GreensProcurements on a.Greens_Procurement_No equals b.GreensProcurementNo
                                       join c in _context.HarvestGRNInwardMaterialDetails on a.Greens_Procurement_No equals c.Greens_Procurement_No into d
                                       from dc in d.Select(x => x.Crop_Name_Code).Distinct()
                                       join harvestGrnDetail in _context.GreensGradedHarvestGRNDetails on a.Greens_Procurement_No equals harvestGrnDetail.Greens_Procurement_No into z
                                       from s in z.DefaultIfEmpty()
                                       where s.STATUS != false
                                       join dd in _context.Areas on a.Area_ID equals dd.Area_ID
                                       join ee in _context.Crops on dc equals ee.CropCode
                                       where a.Greens_Procurement_No != 0
                                       //orderby (DateTime.Now - b.HarvestGRNDate)
                                       orderby b.HarvestDate
                                       orderby a.Harvest_GRN_No
                                       select new GridOneResponse
                                       {
                                           HarvestGRNNo = a.Harvest_GRN_No,
                                           AreaID = a.Area_ID,
                                           TotalReceivedCrates = a.Total_Received_Crates,
                                           TotalReceivedQty = a.Total_Received_Qty,
                                           HarvestGRNDate = b.HarvestDate,
                                           CropNameCode = dc,
                                           AreaName = dd.Area_Name,
                                           CropName = ee.Name,
                                           IsOnGoing = s.STATUS,
                                           GreensGradeNo = s.Greens_Grade_No,
                                           GreensProcurementNo = a.Greens_Procurement_No
                                       }).ToList();


                result.IsSucceed = true;
                result.Data = gridOneResponsesGRN.Union(gridOneResponsesProcurement).ToList();
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


        public async Task<ApiResponse<List<OrganisationOfficeLocationDetailsResponse>>> GetOrganisationOfficesLocationsDetails()
        {
            ApiResponse<List<OrganisationOfficeLocationDetailsResponse>> result = new ApiResponse<List<OrganisationOfficeLocationDetailsResponse>>();
            try
            {
                List<OrganisationOfficeLocationDetailsResponse> _listDetails = await (from aa in _context.OrganisationOfficeLocationDetails
                                                                                      orderby aa.Org_Office_Name
                                                                                      select new OrganisationOfficeLocationDetailsResponse
                                                                                      {
                                                                                          OrgCode = aa.Org_Code,
                                                                                          OrgOfficeName = aa.Org_Office_Name,
                                                                                          OrgOfficeNo = aa.Org_Office_No
                                                                                      }
                                                                       ).ToListAsync();
                result.Data = _listDetails;
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

        public async Task<ApiResponse<GreensGradingInwardDetailsDTO>> SaveGreensGrading(GreensGradingInwardDetailsDTO GreensGradingInwardDetail)
        {
            ApiResponse<GreensGradingInwardDetailsDTO> result = new ApiResponse<GreensGradingInwardDetailsDTO>();
            GreensGradingInwardDetail.GreensGradingQuantityDetailsList.ForEach(x => x.Crop_Name_Code = GreensGradingInwardDetail.Crop_Name_Code);
            GreensGradingInwardDetail.GreensGradingWeighmentDetailsList.ForEach(x => x.Crop_Name_Code = GreensGradingInwardDetail.Crop_Name_Code);
            try
            {
                if (GreensGradingInwardDetail != null)
                {
                     int grdNo = 0;
                    if (GreensGradingInwardDetail.Greens_Grade_No == 0)
                    {

                        if (GreensGradingInwardDetail.GreensGradedHarvestGRNDetailsList.Count > 0)
                        {
                            GreensGradingInwardDetail.GreensGradedHarvestGRNDetailsList.ForEach(x => x.STATUS = true);
                        }
                        var config = new MapperConfiguration(cfg =>
                        {
                            cfg.CreateMap<GreensGradedHarvestGRNDetailsDTO, GreensGradedHarvestGRNDetails>();
                            cfg.CreateMap<GreensGradingQuantityDetailsDTO, GreensGradingQuantityDetails>();
                            cfg.CreateMap<GreensGradingWeighmentDetailsDTO, GreensGradingWeighmentDetails>();
                            cfg.CreateMap<GreensGradingInwardDetailsDTO, GreensGradingInwardDetails>()
                            .ForMember(dest => dest.GreensGradedHarvestGRNDetailsList, act => act.MapFrom(src => src.GreensGradedHarvestGRNDetailsList))
                            .ForMember(dest => dest.GreensGradingQuantityDetailsList, act => act.MapFrom(src => src.GreensGradingQuantityDetailsList))
                            .ForMember(dest => dest.GreensGradingWeighmentDetailsList, act => act.MapFrom(src => src.GreensGradingWeighmentDetailsList));
                        });

                        var mapper = new Mapper(config);
                        var greensGradingInwardDetails = new GreensGradingInwardDetails();
                        greensGradingInwardDetails = mapper.DefaultContext.Mapper.Map<GreensGradingInwardDetails>(GreensGradingInwardDetail);
                        var res = _context.GreensGradingInwardDetails.Add(greensGradingInwardDetails);
                        await _context.SaveChangesAsync();
                        grdNo = res.Greens_Grade_No;
                    }
                    else
                    {
                        var existigGradingInwardDetail = _context.GreensGradingInwardDetails.Where(a => a.Greens_Grade_No == GreensGradingInwardDetail.Greens_Grade_No).FirstOrDefault();
                        existigGradingInwardDetail.Org_office_No = GreensGradingInwardDetail.Org_office_No;
                        existigGradingInwardDetail.Graded_Total_Quantity = GreensGradingInwardDetail.Graded_Total_Quantity;
                        existigGradingInwardDetail.Weighment_Mode = GreensGradingInwardDetail.Weighment_Mode;
                        existigGradingInwardDetail.Crop_Name_Code = GreensGradingInwardDetail.Crop_Name_Code;
                        existigGradingInwardDetail.Crop_Group_Code = GreensGradingInwardDetail.Crop_Group_Code;
                        existigGradingInwardDetail.Graded_Total_Crates = GreensGradingInwardDetail.Graded_Total_Crates;
                        grdNo = existigGradingInwardDetail.Greens_Grade_No;

                        if (GreensGradingInwardDetail.GreensGradedHarvestGRNDetailsList.Count > 0)
                        {
                            var newHarvestGRNDetails = GreensGradingInwardDetail.GreensGradedHarvestGRNDetailsList.Where(x => x.Graded_Harvest_GRN_Nos == 0).ToList();
                            var existHarvestGRNDetails = _context.GreensGradedHarvestGRNDetails.Where(x => x.Greens_Grade_No == GreensGradingInwardDetail.Greens_Grade_No).ToList();
                            if (newHarvestGRNDetails.Count > 0)
                            {
                                newHarvestGRNDetails.ForEach(x => x.Greens_Grade_No = grdNo);
                                List<GreensGradedHarvestGRNDetails> model = Mapper.Map<List<GreensGradedHarvestGRNDetails>>(newHarvestGRNDetails);
                                _context.GreensGradedHarvestGRNDetails.AddRange(model);
                            }

                            var toUpdateHarvestGRNDetails = GreensGradingInwardDetail.GreensGradedHarvestGRNDetailsList.Where(x => x.Graded_Harvest_GRN_Nos != 0).ToList();
                            if (toUpdateHarvestGRNDetails.Count > 0)
                            {
                                var existListToUpdate = existHarvestGRNDetails.Where(p => !newHarvestGRNDetails.Any(p2 => p2.Graded_Harvest_GRN_Nos == p.Graded_Harvest_GRN_Nos && p2.Graded_Harvest_GRN_Nos != 0));
                                foreach (var item in existListToUpdate)
                                {
                                    var newVal = toUpdateHarvestGRNDetails.Find(x => x.Graded_Harvest_GRN_Nos == item.Graded_Harvest_GRN_Nos);
                                    item.Harvest_GRN_No = newVal.Harvest_GRN_No;
                                    item.Greens_Procurement_No = newVal.Greens_Procurement_No;
                                    item.Area_ID = newVal.Area_ID;
                                }
                            }
                        }

                        if (GreensGradingInwardDetail.GreensGradingQuantityDetailsList.Count > 0)
                        {
                            var newQuantityDetails = GreensGradingInwardDetail.GreensGradingQuantityDetailsList.Where(x => x.Greens_Grading_Qty_No == 0).ToList();
                            var existQuantityDetails = _context.GreensGradingQuantityDetails.Where(x => x.Greens_Grade_No == GreensGradingInwardDetail.Greens_Grade_No).ToList();
                            if (newQuantityDetails.Count > 0)
                            {
                                newQuantityDetails.ForEach(x => x.Greens_Grade_No = grdNo);
                                List<GreensGradingQuantityDetails> model = Mapper.Map<List<GreensGradingQuantityDetails>>(newQuantityDetails);
                                _context.GreensGradingQuantityDetails.AddRange(model);
                            }

                            var toUpdateQuantityDetails = GreensGradingInwardDetail.GreensGradingQuantityDetailsList.Where(x => x.Greens_Grading_Qty_No != 0).ToList();
                            if (existQuantityDetails.Count > 0)
                            {
                                var existListToUpdate = existQuantityDetails.Where(p => !newQuantityDetails.Any(p2 => p2.Greens_Grading_Qty_No == p.Greens_Grading_Qty_No && p2.Greens_Grading_Qty_No != 0));
                                foreach (var item in existListToUpdate)
                                {
                                    var newVal = toUpdateQuantityDetails.Find(x => x.Greens_Grading_Qty_No == item.Greens_Grading_Qty_No);
                                    item.Crop_Name_Code = GreensGradingInwardDetail.Crop_Name_Code;
                                    item.Crop_Scheme_Code = newVal.Crop_Scheme_Code;
                                    item.Grading_No_of_Crates = newVal.Grading_No_of_Crates;
                                    item.Quantity_After_Grading_Total = newVal.Quantity_After_Grading_Total;
                                }
                            }
                        }

                        if (GreensGradingInwardDetail.GreensGradingWeighmentDetailsList.Count > 0)
                        {
                            var newInwardDetails = GreensGradingInwardDetail.GreensGradingWeighmentDetailsList.Where(x => x.GM_Weight_No == 0).ToList();
                            var existInwardDetails = _context.GreensGradingWeighmentDetails.Where(x => x.Greens_Grade_No == GreensGradingInwardDetail.Greens_Grade_No).ToList();
                            if (newInwardDetails.Count > 0)
                            {
                                newInwardDetails.ForEach(x => x.Greens_Grade_No = grdNo);
                                List<GreensGradingWeighmentDetails> model = Mapper.Map<List<GreensGradingWeighmentDetails>>(newInwardDetails);
                                _context.GreensGradingWeighmentDetails.AddRange(model);
                            }

                            var toUpdateInwardDetails = GreensGradingInwardDetail.GreensGradingWeighmentDetailsList.Where(x => x.GM_Weight_No != 0).ToList();
                            if (toUpdateInwardDetails.Count > 0)
                            {
                                var existListToUpdate = existInwardDetails.Where(p => !newInwardDetails.Any(p2 => p2.GM_Weight_No == p.GM_Weight_No && p2.GM_Weight_No != 0));
                                foreach (var item in existListToUpdate)
                                {
                                    var newVal = toUpdateInwardDetails.Find(x => x.GM_Weight_No == item.GM_Weight_No);
                                    item.Crop_Name_Code = GreensGradingInwardDetail.Crop_Name_Code;
                                    item.Crop_Scheme_Code = newVal.Crop_Scheme_Code;
                                    item.GM_Weight_No_of_Crates = newVal.GM_Weight_No_of_Crates;
                                    item.GM_Weight_Gross_Weight = newVal.GM_Weight_Gross_Weight;
                                    item.GM_Weight_Tare_Weight = newVal.GM_Weight_Tare_Weight;
                                    item.HM_Weight_Net_Weight = newVal.HM_Weight_Net_Weight;
                                    item.GM_Crates_Tare_Weight = newVal.GM_Crates_Tare_Weight;
                                }
                            }
                        }
                    }
                    _context.SaveChanges();
                    result.IsSucceed = true;
                    result.Data = GetGreensGrading(grdNo);
                }
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

        public async Task<ApiResponse<GreensGradingInwardDetailsDTO>> GetGreensGradingByGrdNo(int greensGrdNo)
        {
            ApiResponse<GreensGradingInwardDetailsDTO> result = new ApiResponse<GreensGradingInwardDetailsDTO>();
            try
            {
                result.Data = GetGreensGrading(greensGrdNo);
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


        private GreensGradingInwardDetailsDTO GetGreensGrading(int greensGrdNo)
        {
            GreensGradingInwardDetailsDTO result = new GreensGradingInwardDetailsDTO();
            try
            {
                result = (from InwardDetail in _context.GreensGradingInwardDetails
                          join o in _context.OrganisationOfficeLocationDetails on InwardDetail.Org_office_No equals o.Org_Office_No
                          join cn in _context.Crops on InwardDetail.Crop_Name_Code equals cn.CropCode into a
                          from crpNme in a.DefaultIfEmpty()
                          join cg in _context.CropGroups on InwardDetail.Crop_Group_Code equals cg.CropGroupCode into b
                          from crpGrp in b.DefaultIfEmpty()
                          where InwardDetail.Greens_Grade_No == greensGrdNo
                          select new GreensGradingInwardDetailsDTO
                          {
                              Greens_Grade_No = InwardDetail.Greens_Grade_No,
                              Org_office_No = InwardDetail.Org_office_No,
                              Org_office_Name = o.Org_Office_Name,
                              Graded_Total_Quantity = InwardDetail.Graded_Total_Quantity,
                              Weighment_Mode = InwardDetail.Weighment_Mode,
                              Crop_Name_Code = InwardDetail.Crop_Name_Code,
                              Crop_Name = crpNme.Name,
                              Crop_Group_Code = InwardDetail.Crop_Group_Code,
                              Crop_Group_Name = crpGrp.Name,
                              Graded_Total_Crates = InwardDetail.Graded_Total_Crates,
                              GreensGradedHarvestGRNDetailsList = (from grnDetails in _context.GreensGradedHarvestGRNDetails
                                                                   join cn1 in _context.Crops on InwardDetail.Crop_Name_Code equals cn1.CropCode into a1
                                                                   from crpNme1 in a1.DefaultIfEmpty()
                                                                   join ar1 in _context.Areas on grnDetails.Area_ID equals ar1.Area_ID into c1
                                                                   from area1 in c1.DefaultIfEmpty()
                                                                   where grnDetails.Greens_Grade_No == greensGrdNo
                                                                   select new GreensGradedHarvestGRNDetailsDTO
                                                                   {
                                                                       Graded_Harvest_GRN_Nos = grnDetails.Graded_Harvest_GRN_Nos,
                                                                       Greens_Grade_No = grnDetails.Greens_Grade_No,
                                                                       Harvest_GRN_No = grnDetails.Harvest_GRN_No,
                                                                       Area_ID = grnDetails.Area_ID,
                                                                       Area_Name = area1.Area_Name,
                                                                       STATUS = grnDetails.STATUS,
                                                                       Greens_Procurement_No = grnDetails.Greens_Procurement_No
                                                                   }).ToList(),
                              GreensGradingQuantityDetailsList = (from qtyDetail in _context.GreensGradingQuantityDetails
                                                                  join cn2 in _context.Crops on InwardDetail.Crop_Name_Code equals cn2.CropCode into a2
                                                                  from crpNme2 in a2.DefaultIfEmpty()
                                                                  join schm in _context.CropSchemes on qtyDetail.Crop_Scheme_Code equals schm.Code into s1
                                                                  from scheme in s1.DefaultIfEmpty()
                                                                  where qtyDetail.Greens_Grade_No == greensGrdNo
                                                                  select new GreensGradingQuantityDetailsDTO
                                                                  {
                                                                      Greens_Grading_Qty_No = qtyDetail.Greens_Grading_Qty_No,
                                                                      Greens_Grade_No = qtyDetail.Greens_Grade_No,
                                                                      Crop_Name_Code = qtyDetail.Crop_Name_Code,
                                                                      Crop_Name = crpNme2.Name,
                                                                      Crop_Scheme_Code = qtyDetail.Crop_Scheme_Code,
                                                                      From = scheme.From,
                                                                      Sign = scheme.Sign,
                                                                      Count = scheme.Count,
                                                                      Grading_No_of_Crates = qtyDetail.Grading_No_of_Crates,
                                                                      Quantity_After_Grading_Total = qtyDetail.Quantity_After_Grading_Total
                                                                  }).ToList(),
                              GreensGradingWeighmentDetailsList = (from weiDetail in _context.GreensGradingWeighmentDetails
                                                                   join cn3 in _context.Crops on weiDetail.Crop_Name_Code equals cn3.CropCode into a3
                                                                   from crpNme3 in a3.DefaultIfEmpty()
                                                                   join schm2 in _context.CropSchemes on weiDetail.Crop_Scheme_Code equals schm2.Code into s2
                                                                   from scheme2 in s2.DefaultIfEmpty()
                                                                   where weiDetail.Greens_Grade_No == greensGrdNo
                                                                   select new GreensGradingWeighmentDetailsDTO
                                                                   {
                                                                       GM_Weight_No = weiDetail.GM_Weight_No,
                                                                       Greens_Grade_No = weiDetail.Greens_Grade_No,
                                                                       Crop_Name_Code = weiDetail.Crop_Name_Code,
                                                                       Crop_Name = crpNme3.Name,
                                                                       Crop_Scheme_Code = weiDetail.Crop_Scheme_Code,
                                                                       From = scheme2.From,
                                                                       Sign = scheme2.Sign,
                                                                       Count = scheme2.Count,
                                                                       GM_Weight_No_of_Crates = weiDetail.GM_Weight_No_of_Crates,
                                                                       GM_Weight_Gross_Weight = weiDetail.GM_Weight_Gross_Weight,
                                                                       GM_Weight_Tare_Weight = weiDetail.GM_Weight_Tare_Weight,
                                                                       HM_Weight_Net_Weight = weiDetail.HM_Weight_Net_Weight,
                                                                       GM_Crates_Tare_Weight = weiDetail.GM_Crates_Tare_Weight,

                                                                   }).ToList(),
                          }).FirstOrDefault();

            }
            catch (Exception ex)
            {

            }
            return result;
        }


        public async Task<ApiResponse<bool>> ChangeStatus(int greensGrdNo)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var val = await this._context.GreensGradedHarvestGRNDetails.Where(x => x.Greens_Grade_No == greensGrdNo).ToListAsync();

                if (val.Count > 0)
                {
                    val.ForEach(item => item.STATUS = false);
                }
                 _context.SaveChanges();
                result.IsSucceed = true;
                result.Data = true;
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