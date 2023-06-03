using AutoMapper;
using GherkinWebAPI.Core.AgentsGreensRecWeighment;
using GherkinWebAPI.DTO;
using GherkinWebAPI.DTO.AgentsGreensRecWeighment;
using GherkinWebAPI.DTO.MaterialInward;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.AgentsGreensRecWeighment;
using GherkinWebAPI.Persistence;
using GherkinWebAPI.Request.GreensAgentSupplierDetails;
using GherkinWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Repository.AgentsGreensRecWeighment
{
    public class AgentsGreensReceivingWeighmentRepository : RepositoryBase<GreensAgentReceivedDetails>, IAgentsGreensReceivingWeighmentRepository
    {
        private RepositoryContext _context;
        public AgentsGreensReceivingWeighmentRepository(RepositoryContext repositoryContext)
           : base(repositoryContext)
        {
            _context = repositoryContext;
        }
        public async Task<ApiResponse<List<OrganisationOfficeLocationDetailsDto>>> GetOrgOfficeLocation()
        {
            ApiResponse<List<OrganisationOfficeLocationDetailsDto>> result = new ApiResponse<List<OrganisationOfficeLocationDetailsDto>>();
            try
            {
                List<OrganisationOfficeLocationDetailsDto> list = new List<OrganisationOfficeLocationDetailsDto>();
                list = await (from orgdetails in _context.OrganisationOfficeLocationDetails
                              select new OrganisationOfficeLocationDetailsDto
                              {
                                  Org_Code = orgdetails.Org_Code,
                                  Org_Office_No = orgdetails.Org_Office_No,
                                  Org_Office_Name = orgdetails.Org_Office_Name
                              }).OrderBy(c => c.Org_Office_Name).ToListAsync();
                result.Data = list;
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

        public async Task<ApiResponse<List<MaterialInwardDto>>> GetInwardDetails(int officeOrgNumber)
        {
            ApiResponse<List<MaterialInwardDto>> result = new ApiResponse<List<MaterialInwardDto>>();
            try
            {
                List<MaterialInwardDto> list = new List<MaterialInwardDto>();
                list = await (from invDetail in _context.MaterialInwardEntity
                              join q in _context.GreensAgentReceivedDetails on invDetail.Inward_Gate_Pass_No equals q.InwardGatePassNo into recvDeail
                              from y in recvDeail.DefaultIfEmpty()
                              where invDetail.Org_Office_No == officeOrgNumber && invDetail.Inward_Type == "Greens Agent Supplier" && y.IsOnGoing != false
                              select new MaterialInwardDto
                              {
                                  Inward_Type = invDetail.Inward_Type,
                                  Inward_Date_Time = invDetail.Inward_Date_Time,
                                  Inward_Gate_Pass_No = invDetail.Inward_Gate_Pass_No,
                                  Supplier_Transporter_Name = invDetail.Supplier_Transporter_Name,
                                  Inv_Vehicle_No = invDetail.Inv_Vehicle_No,
                                  IsOnGoing = y.IsOnGoing,
                              }).ToListAsync();
                result.IsSucceed = true;
                result.Data = list;
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

        public async Task<ApiResponse<List<SupplierInformationDetailsRequest>>> GetSupplierInformationDetail()
        {
            ApiResponse<List<SupplierInformationDetailsRequest>> result = new ApiResponse<List<SupplierInformationDetailsRequest>>();
            try
            {
                var supplierInformationDetailList = await (from si in _context.SupplierInformationDetails
                                                           select new SupplierInformationDetailsRequest
                                                           {
                                                               AgentOrgID = si.AgentOrgID,
                                                               AgentOrganisationName = si.AgentOrganisationName,
                                                               placeCode = si.placeCode,
                                                               placeName = _context.Places.Where(a => a.PlaceCode == si.placeCode).FirstOrDefault().PlaceName,
                                                           }).OrderBy(c => c.AgentOrganisationName).ToListAsync();
                result.IsSucceed = true;
                result.Data = supplierInformationDetailList;
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

        public async Task<ApiResponse<List<CropScheme>>> GetCropSchemeDetails(string cropNameCode)
        {
            ApiResponse<List<CropScheme>> result = new ApiResponse<List<CropScheme>>();
            try
            {
                var cropschemedetails = await _context.CropSchemes.Where(a => a.CropCode == cropNameCode).OrderByDescending(b => b.From).ToListAsync();
                //var cropschemedetails = await (from cs in _context.CropSchemes
                //                               where cs.CropCode == cropNameCode
                //                               select new CropScheme
                //                               {
                //                                   Sign = cs.Sign,
                //                                   From = cs.From,
                //                                   Count = cs.Count,
                //                                   CropSchemeId = cs.CropSchemeId,

                //                               }).OrderBy(c => c.From).ToListAsync();
                result.IsSucceed = true;
                result.Data = cropschemedetails;
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

        public async Task<ApiResponse<GreensAgentReceivedDetailsDTO>> PartialSaveGreensRecvDetails(GreensAgentReceivedDetails recvDetail)
        {
            ApiResponse<GreensAgentReceivedDetailsDTO> result = new ApiResponse<GreensAgentReceivedDetailsDTO>();
            try
            {
                recvDetail.IsOnGoing = true;
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<GreensAgentDespCountWeightDetails, GreensAgentDespCountWeightDetails>();
                    cfg.CreateMap<GreensAgentReceivedDetails, GreensAgentReceivedDetails>().ForMember(dest => dest.GreensAgentDespCountWeightDetails, act => act.MapFrom(src => src.GreensAgentDespCountWeightDetails));
                });

                var mapper = new Mapper(config);
                var greensAgentReceivedDetails = new GreensAgentReceivedDetails();
                greensAgentReceivedDetails = mapper.DefaultContext.Mapper.Map<GreensAgentReceivedDetails>(recvDetail);
                var res = _context.GreensAgentReceivedDetails.Add(greensAgentReceivedDetails);
                await _context.SaveChangesAsync();
                             

                result.IsSucceed = true;
                result.Data = GetGreensRecvDetails(res.GreensAgentGRNNo);
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

        public async Task<ApiResponse<GreensAgentReceivedDetailsDTO>> GetGreensRecvDetails(string inwardGatepassNo)
        {
            ApiResponse<GreensAgentReceivedDetailsDTO> result = new ApiResponse<GreensAgentReceivedDetailsDTO>();
            try
            {
                var data = (from RecvDetail in _context.GreensAgentReceivedDetails
                            join plantation in _context.PlantationSchedules on RecvDetail.PSNumber equals plantation.PsNumber
                            join b in _context.GreensAgentDespCountWeightDetails on RecvDetail.GreensAgentGRNNo equals b.GreensAgentGRNNo into a
                            from countWeightDeail in a.DefaultIfEmpty()                            
                            join c in _context.GreensAgentActualWeightDetails on RecvDetail.GreensAgentGRNNo equals c.GreensAgentGRNNo into d
                            from actulWeit in d.DefaultIfEmpty()
                            join e in _context.GreensAgentGradesActualDetails on RecvDetail.GreensAgentGRNNo equals e.GreensAgentGRNNo into f
                            from actualDetail in f.DefaultIfEmpty()
                            where RecvDetail.InwardGatePassNo == inwardGatepassNo
                            select new GreensAgentReceivedDetailsDTO {
                                GreensAgentGRNNo = RecvDetail.GreensAgentGRNNo,
                                OrgOfficeNo = RecvDetail.OrgOfficeNo,
                                GreensAgentGRNDateTime = RecvDetail.GreensAgentGRNDateTime,
                                AgentOrgID = RecvDetail.AgentOrgID,
                                InvoiceDCNo = RecvDetail.InvoiceDCNo,
                                InvoiceDCDate = RecvDetail.InvoiceDCDate,
                                CropGroupCode = RecvDetail.CropGroupCode,
                                CropNameCode = RecvDetail.CropNameCode,
                                PSNumber = RecvDetail.PSNumber,
                                GreensAgentDespQty = RecvDetail.GreensAgentDespQty,
                                GreensAgentDespCrates = RecvDetail.GreensAgentDespCrates,
                                InwardGatePassNo = RecvDetail.InwardGatePassNo,
                                TotalQuantityReceived = RecvDetail.TotalQuantityReceived,
                                WeightMode = RecvDetail.WeightMode,
                                EmployeeID = RecvDetail.EmployeeID,
                                cropName = _context.Crops.Where(x => x.CropCode == RecvDetail.CropNameCode).FirstOrDefault().Name.ToString(),
                                CropGroupName = _context.CropGroups.Where(x => x.CropGroupCode == RecvDetail.CropGroupCode).FirstOrDefault().Name.ToString(),
                                OrgOfficeName = _context.OrganisationOfficeLocationDetails.Where(x => x.Org_Office_No == RecvDetail.OrgOfficeNo).FirstOrDefault().Org_Office_Name.ToString(),
                                // Date = plantation.FromDate.ToString("dd-MMM-yyyy") + "/" + plantation.ToDate.ToString("dd-MMM-yyyy").ToString(),
                                AgentOrganisationName = _context.SupplierInformationDetails.Where(x => x.AgentOrgID == RecvDetail.AgentOrgID).FirstOrDefault().AgentOrganisationName.ToString(),
                                GreensAgentDespCountWeightDetails = (from cd in RecvDetail.GreensAgentDespCountWeightDetails
                                                                     join cs in _context.CropSchemes on cd.CropSchemeCode equals cs.Code
                                                                     select new GreensAgentDespCountWeightDetailsDTO {
                                                                            AgentCropReceivedNo = cd.AgentCropReceivedNo,
                                                                            CropSchemeCode = cd.CropSchemeCode,
                                                                            From = cs.From,
                                                                            Sign = cs.Sign,
                                                                            Count = cs.Count,
                                                                            GreensAgentGRNNo = cd.GreensAgentGRNNo,
                                                                            AgentCropReceivedCrates = cd.AgentCropReceivedCrates,
                                                                            AgentCropReceivedQty = cd.AgentCropReceivedQty,
                                                                     }).ToList(),
                                ActualWeightDetails = (from aw in RecvDetail.GreensAgentActualWeightDetails
                                                       join cs in _context.CropSchemes on aw.CropSchemeCode equals cs.Code
                                                       select new ActualWeightDetailsDTO
                                                       {
                                                           ActualCountWeightNo = aw.ActualCountWeightNo,
                                                           GreensAgentGRNNo = aw.GreensAgentGRNNo,
                                                           CropNameCode = aw.CropNameCode,
                                                           cropName = _context.Crops.Where(a=>a.CropCode == aw.CropNameCode).FirstOrDefault().Name.ToString(),
                                                           ActualWeightNoofCrates = aw.ActualWeightNoofCrates,
                                                           ActualCratesTareWeight = aw.ActualCratesTareWeight,
                                                           SlNoFrom = aw.SlNoFrom,
                                                           SlNoTo = aw.SlNoTo,
                                                           ActualGrossWeight = aw.ActualGrossWeight,
                                                           ActualTareWeight = aw.ActualTareWeight,
                                                           ActualNetWeight = aw.ActualNetWeight,
                                                           CropSchemeCode = aw.CropSchemeCode,
                                                           From = cs.From,
                                                           Sign = cs.Sign,
                                                           Count = cs.Count,
                                                       }).ToList(),
                                ActualDetails = (from ad in RecvDetail.GreensAgentGradesActualDetails 
                                                 join cs in _context.CropSchemes on ad.CropSchemeCode equals cs.Code
                                                 select new ActualDetailsDTO {
                                                     AgentReceivedNo  = ad.AgentReceivedNo,
                                                     GreensAgentGRNNo = ad.GreensAgentGRNNo,
                                                     CropNameCode = ad.CropNameCode,
                                                     cropName = _context.Crops.Where(a => a.CropCode == ad.CropNameCode).FirstOrDefault().Name.ToString(),
                                                     CropSchemeCode = ad.CropSchemeCode,
                                                     From = cs.From,
                                                     Sign = cs.Sign,
                                                     Count = cs.Count,
                                                     CountTotalCrates = ad.CountTotalCrates,
                                                     CountTotalWeight = ad.CountTotalWeight
                                                 }).ToList(),

                            }).FirstOrDefault();                                  

               

                result.IsSucceed = true;
                result.Data = data;

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

        public async Task<ApiResponse<bool>> ChangeInGoingStatus(int GRNNo)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var grensAgent = await _context.GreensAgentReceivedDetails.Where(a => a.GreensAgentGRNNo == GRNNo).FirstOrDefaultAsync();
                grensAgent.IsOnGoing = false;
                _context.SaveChanges();
                result.IsSucceed = true;
                result.Data = true;
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

        public async Task<ApiResponse<GreensAgentReceivedDetailsDTO>> SaveRecvWeighmentDetails(GreensAgentReceivedDetails agentsGrnRecivWeigmtDetail)
        {
            
            ApiResponse<GreensAgentReceivedDetailsDTO> result = new ApiResponse<GreensAgentReceivedDetailsDTO>();
            try
            {                
                if (agentsGrnRecivWeigmtDetail != null)
                {
                    int grn = 0;
                    if (agentsGrnRecivWeigmtDetail.GreensAgentGRNNo == 0)
                    {
                        agentsGrnRecivWeigmtDetail.IsOnGoing = true;
                        var config = new MapperConfiguration(cfg =>
                        {
                            cfg.CreateMap<GreensAgentDespCountWeightDetails, GreensAgentDespCountWeightDetails>();
                            cfg.CreateMap<GreensAgentActualWeightDetails, GreensAgentActualWeightDetails>();
                            cfg.CreateMap<GreensAgentGradesActualDetails, GreensAgentGradesActualDetails>();
                            cfg.CreateMap<GreensAgentReceivedDetails, GreensAgentReceivedDetails>()
                            .ForMember(dest => dest.GreensAgentDespCountWeightDetails, act => act.MapFrom(src => src.GreensAgentDespCountWeightDetails))
                            .ForMember(dest => dest.GreensAgentActualWeightDetails, act => act.MapFrom(src => src.GreensAgentActualWeightDetails))
                            .ForMember(dest => dest.GreensAgentGradesActualDetails, act => act.MapFrom(src => src.GreensAgentGradesActualDetails));
                        });

                        var mapper = new Mapper(config);
                        var greensAgentReceivedDetails = new GreensAgentReceivedDetails();
                        greensAgentReceivedDetails = mapper.DefaultContext.Mapper.Map<GreensAgentReceivedDetails>(agentsGrnRecivWeigmtDetail);
                        var res = _context.GreensAgentReceivedDetails.Add(greensAgentReceivedDetails);                        
                        await _context.SaveChangesAsync();
                        grn = res.GreensAgentGRNNo;
                    }
                    else
                    {
                        var grensAgentDetail = _context.GreensAgentReceivedDetails.Where(a => a.GreensAgentGRNNo == agentsGrnRecivWeigmtDetail.GreensAgentGRNNo).FirstOrDefault();
                        grensAgentDetail.OrgOfficeNo = agentsGrnRecivWeigmtDetail.OrgOfficeNo;
                        grensAgentDetail.GreensAgentGRNDateTime = agentsGrnRecivWeigmtDetail.GreensAgentGRNDateTime;
                        grensAgentDetail.AgentOrgID = agentsGrnRecivWeigmtDetail.AgentOrgID;
                        grensAgentDetail.InvoiceDCNo = agentsGrnRecivWeigmtDetail.InvoiceDCNo;
                        grensAgentDetail.InvoiceDCDate = agentsGrnRecivWeigmtDetail.InvoiceDCDate;
                        grensAgentDetail.CropGroupCode = agentsGrnRecivWeigmtDetail.CropGroupCode;
                        grensAgentDetail.CropNameCode = agentsGrnRecivWeigmtDetail.CropNameCode;
                        grensAgentDetail.PSNumber = agentsGrnRecivWeigmtDetail.PSNumber;
                        grensAgentDetail.GreensAgentDespQty = agentsGrnRecivWeigmtDetail.GreensAgentDespQty;
                        grensAgentDetail.GreensAgentDespCrates = agentsGrnRecivWeigmtDetail.GreensAgentDespCrates;
                        grensAgentDetail.InwardGatePassNo = agentsGrnRecivWeigmtDetail.InwardGatePassNo;
                        grensAgentDetail.TotalQuantityReceived = agentsGrnRecivWeigmtDetail.TotalQuantityReceived;
                        grensAgentDetail.WeightMode = agentsGrnRecivWeigmtDetail.WeightMode;
                        grensAgentDetail.EmployeeID = agentsGrnRecivWeigmtDetail.EmployeeID;
                        grn = grensAgentDetail.GreensAgentGRNNo;

                        if (agentsGrnRecivWeigmtDetail.GreensAgentDespCountWeightDetails.Count > 0)
                        {
                            var newWeightDetails = agentsGrnRecivWeigmtDetail.GreensAgentDespCountWeightDetails.Where(x => x.AgentCropReceivedNo == 0).ToList();
                            var existWeightDetails = _context.GreensAgentDespCountWeightDetails.Where(x => x.GreensAgentGRNNo == grensAgentDetail.GreensAgentGRNNo).ToList();
                            if (newWeightDetails.Count > 0)
                            {
                                newWeightDetails.ForEach(x => x.GreensAgentGRNNo = grn);
                                List<GreensAgentDespCountWeightDetails> model = Mapper.Map<List<GreensAgentDespCountWeightDetails>>(newWeightDetails);
                                _context.GreensAgentDespCountWeightDetails.AddRange(model);
                            }

                            //var result = peopleList2.Where(p => !peopleList1.Any(p2 => p2.ID == p.ID));
                            var updateWeightDetails = agentsGrnRecivWeigmtDetail.GreensAgentDespCountWeightDetails.Where(x => x.AgentCropReceivedNo != 0).ToList();
                            if (existWeightDetails.Count > 0)
                            {
                                var existListToUpdate = existWeightDetails.Where(p => !newWeightDetails.Any(p2 => p2.AgentCropReceivedNo == p.AgentCropReceivedNo && p2.AgentCropReceivedNo !=0 ));
                                foreach (var item in existListToUpdate)
                                {
                                    //var itemToUpdate =_context.GreensAgentDespCountWeightDetails.Where(x => x.AgentCropReceivedNo == item.AgentCropReceivedNo).FirstOrDefault();

                                    item.AgentCropReceivedCrates = updateWeightDetails.Find(x => x.AgentCropReceivedNo == item.AgentCropReceivedNo).AgentCropReceivedCrates;
                                    item.AgentCropReceivedQty = updateWeightDetails.Find(x => x.AgentCropReceivedNo == item.AgentCropReceivedNo).AgentCropReceivedQty;

                                }
                            }
                        }

                        if (agentsGrnRecivWeigmtDetail.GreensAgentActualWeightDetails.Count > 0)
                        {
                            agentsGrnRecivWeigmtDetail.GreensAgentActualWeightDetails.ForEach(item => item.GreensAgentGRNNo = agentsGrnRecivWeigmtDetail.GreensAgentGRNNo);
                            var newActualWeightDetails = agentsGrnRecivWeigmtDetail.GreensAgentActualWeightDetails.Where(x => x.ActualCountWeightNo == 0).ToList();

                            var existActualWeightDetails = _context.GreensAgentActualWeightDetails.Where(x => x.GreensAgentGRNNo == grensAgentDetail.GreensAgentGRNNo).ToList();
                            if (newActualWeightDetails.Count > 0)
                            {
                                newActualWeightDetails.ForEach(x => x.GreensAgentGRNNo = grn);
                                List<GreensAgentActualWeightDetails> model = Mapper.Map<List<GreensAgentActualWeightDetails>>(newActualWeightDetails);
                                //model.ForEach(x => x.ActualCratesTareWeight = 10m);
                                _context.GreensAgentActualWeightDetails.AddRange(model);
                            }

                            var updateWeightDetails = agentsGrnRecivWeigmtDetail.GreensAgentActualWeightDetails.Where(x => x.ActualCountWeightNo != 0).ToList();
                            if (existActualWeightDetails.Count > 0)
                            {
                                var existActualListToUpdate = existActualWeightDetails.Where(p => !newActualWeightDetails.Any(p2 => p2.ActualCountWeightNo == p.ActualCountWeightNo && p2.ActualCountWeightNo != 0));
                                foreach (var item in existActualListToUpdate)
                                {
                                    var actualToUpdate = updateWeightDetails.Where(x => x.ActualCountWeightNo == item.ActualCountWeightNo).FirstOrDefault();
                                    if (actualToUpdate != null)
                                    {
                                        item.CropNameCode = actualToUpdate.CropNameCode;
                                        item.CropSchemeCode = actualToUpdate.CropSchemeCode;
                                        item.ActualWeightNoofCrates = actualToUpdate.ActualWeightNoofCrates;
                                        item.ActualCratesTareWeight = actualToUpdate.ActualCratesTareWeight;
                                        item.SlNoFrom = actualToUpdate.SlNoFrom;
                                        item.SlNoTo = actualToUpdate.SlNoTo;
                                        item.ActualGrossWeight = actualToUpdate.ActualGrossWeight;
                                        item.ActualTareWeight = actualToUpdate.ActualTareWeight;
                                        item.ActualNetWeight = actualToUpdate.ActualNetWeight;
                                    }

                                }
                            }
                        }

                        if (agentsGrnRecivWeigmtDetail.GreensAgentGradesActualDetails.Count > 0)
                        {
                            var newActualDetails = agentsGrnRecivWeigmtDetail.GreensAgentGradesActualDetails.Where(x => x.AgentReceivedNo == 0).ToList();
                            var existActualDetails = _context.GreensAgentGradesActualDetails.Where(x => x.GreensAgentGRNNo == grensAgentDetail.GreensAgentGRNNo).ToList();
                            if (newActualDetails.Count > 0)
                            {
                                newActualDetails.ForEach(x => x.GreensAgentGRNNo = grn);
                                List<GreensAgentGradesActualDetails> model = Mapper.Map<List<GreensAgentGradesActualDetails>>(newActualDetails);
                                _context.GreensAgentGradesActualDetails.AddRange(model);
                            }

                            var updateDetails = agentsGrnRecivWeigmtDetail.GreensAgentGradesActualDetails.Where(x => x.AgentReceivedNo != 0).ToList();
                            if (existActualDetails.Count > 0)
                            {
                                var existActualListToUpdate = existActualDetails.Where(p => !newActualDetails.Any(p2 => p2.AgentReceivedNo == p.AgentReceivedNo && p2.AgentReceivedNo != 0));
                                foreach (var item in existActualListToUpdate)
                                {
                                    var actualToUpdate = updateDetails.Where(x => x.AgentReceivedNo == item.AgentReceivedNo).FirstOrDefault();
                                    if(actualToUpdate != null)
                                    {
                                        item.CropNameCode = actualToUpdate.CropNameCode;
                                        item.CropSchemeCode = actualToUpdate.CropSchemeCode;
                                        item.CountTotalCrates = actualToUpdate.CountTotalCrates;
                                        item.CountTotalWeight = actualToUpdate.CountTotalWeight;
                                    }                                   

                                }
                            }
                        }
                    }
                    _context.SaveChanges();
                    result.IsSucceed = true;
                    result.Data = GetGreensRecvDetails(grn);
                }
                else
                {
                    result.IsSucceed = false;
                    result.ErrorMessages = new List<string>();
                    result.ErrorMessages.Add("Invalid Data Passed");
                }                
                
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in the state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }

                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                            ve.PropertyName,
                            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                            ve.ErrorMessage);
                    }
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

        private GreensAgentReceivedDetailsDTO GetGreensRecvDetails(int GRNNo)
        {
            var data = (from RecvDetail in _context.GreensAgentReceivedDetails
                        join plantation in _context.PlantationSchedules on RecvDetail.PSNumber equals plantation.PsNumber 
                        join supl in _context.SupplierInformationDetails on RecvDetail.AgentOrgID equals supl.AgentOrgID
                        join b in _context.GreensAgentDespCountWeightDetails on RecvDetail.GreensAgentGRNNo equals b.GreensAgentGRNNo into a
                        from countWeightDeail in a.DefaultIfEmpty()
                        join c in _context.GreensAgentActualWeightDetails on RecvDetail.GreensAgentGRNNo equals c.GreensAgentGRNNo into d
                        from actulWeit in d.DefaultIfEmpty()
                        join e in _context.GreensAgentGradesActualDetails on RecvDetail.GreensAgentGRNNo equals e.GreensAgentGRNNo into f
                        from actualDetail in f.DefaultIfEmpty()
                        where RecvDetail.GreensAgentGRNNo == GRNNo
                        select new GreensAgentReceivedDetailsDTO
                        {
                            GreensAgentGRNNo = RecvDetail.GreensAgentGRNNo,
                            OrgOfficeNo = RecvDetail.OrgOfficeNo,
                            GreensAgentGRNDateTime = RecvDetail.GreensAgentGRNDateTime,
                            AgentOrgID = RecvDetail.AgentOrgID,
                            InvoiceDCNo = RecvDetail.InvoiceDCNo,
                            InvoiceDCDate = RecvDetail.InvoiceDCDate,
                            CropGroupCode = RecvDetail.CropGroupCode,
                            CropNameCode = RecvDetail.CropNameCode,
                            PSNumber = RecvDetail.PSNumber,
                            GreensAgentDespQty = RecvDetail.GreensAgentDespQty,
                            GreensAgentDespCrates = RecvDetail.GreensAgentDespCrates,
                            InwardGatePassNo = RecvDetail.InwardGatePassNo,
                            TotalQuantityReceived = RecvDetail.TotalQuantityReceived,
                            WeightMode = RecvDetail.WeightMode,
                            EmployeeID = RecvDetail.EmployeeID,
                            cropName = _context.Crops.Where(x => x.CropCode == RecvDetail.CropNameCode).FirstOrDefault().Name,
                            CropGroupName = _context.CropGroups.Where(x => x.CropGroupCode == RecvDetail.CropGroupCode).FirstOrDefault().Name,
                            OrgOfficeName = _context.OrganisationOfficeLocationDetails.Where(x => x.Org_Office_No == RecvDetail.OrgOfficeNo).FirstOrDefault().Org_Office_Name,
                            Date = plantation.FromDate.ToString() + "/" + plantation.ToDate.ToString(),
                            AgentOrganisationName = supl.AgentOrganisationName,
                            Place = _context.Places.Where(p=>p.PlaceCode == supl.placeCode).FirstOrDefault().PlaceName,
                            GreensAgentDespCountWeightDetails = (from cd in RecvDetail.GreensAgentDespCountWeightDetails
                                                                 join cs in _context.CropSchemes on cd.CropSchemeCode equals cs.Code
                                                                 select new GreensAgentDespCountWeightDetailsDTO
                                                                 {
                                                                     AgentCropReceivedNo = cd.AgentCropReceivedNo,
                                                                     CropSchemeCode = cd.CropSchemeCode,
                                                                     From = cs.From,
                                                                     Sign = cs.Sign,
                                                                     Count = cs.Count,
                                                                     GreensAgentGRNNo = cd.GreensAgentGRNNo,
                                                                     AgentCropReceivedCrates = cd.AgentCropReceivedCrates,
                                                                     AgentCropReceivedQty = cd.AgentCropReceivedQty,
                                                                 }).ToList(),
                            ActualWeightDetails = (from aw in RecvDetail.GreensAgentActualWeightDetails
                                                   join cs in _context.CropSchemes on aw.CropSchemeCode equals cs.Code
                                                   select new ActualWeightDetailsDTO
                                                   {
                                                       ActualCountWeightNo = aw.ActualCountWeightNo,
                                                       GreensAgentGRNNo = aw.GreensAgentGRNNo,
                                                       CropNameCode = aw.CropNameCode,
                                                       cropName = _context.Crops.Where(a => a.CropCode == aw.CropNameCode).FirstOrDefault().Name,
                                                       ActualWeightNoofCrates = aw.ActualWeightNoofCrates,
                                                       ActualCratesTareWeight = aw.ActualCratesTareWeight,
                                                       SlNoFrom = aw.SlNoFrom,
                                                       SlNoTo = aw.SlNoTo,
                                                       ActualGrossWeight = aw.ActualGrossWeight,
                                                       ActualTareWeight = aw.ActualTareWeight,
                                                       ActualNetWeight = aw.ActualNetWeight,
                                                       CropSchemeCode = aw.CropSchemeCode,
                                                       From = cs.From,
                                                       Sign = cs.Sign,
                                                       Count = cs.Count,
                                                   }).ToList(),
                            ActualDetails = (from ad in RecvDetail.GreensAgentGradesActualDetails
                                             join cs in _context.CropSchemes on ad.CropSchemeCode equals cs.Code
                                             select new ActualDetailsDTO
                                             {
                                                 AgentReceivedNo = ad.AgentReceivedNo,
                                                 GreensAgentGRNNo = ad.GreensAgentGRNNo,
                                                 CropNameCode = ad.CropNameCode,
                                                 cropName = _context.Crops.Where(a => a.CropCode == ad.CropNameCode).FirstOrDefault().Name,
                                                 CropSchemeCode = ad.CropSchemeCode,
                                                 From = cs.From,
                                                 Sign = cs.Sign,
                                                 Count = cs.Count,
                                                 CountTotalCrates = ad.CountTotalCrates,
                                                 CountTotalWeight = ad.CountTotalWeight
                                             }).ToList(),

                        }).FirstOrDefault();
            return data;
        }
    }
}