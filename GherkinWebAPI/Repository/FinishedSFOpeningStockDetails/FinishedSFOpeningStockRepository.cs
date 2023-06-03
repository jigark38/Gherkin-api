using AutoMapper;
using GherkinWebAPI.Core;
using GherkinWebAPI.CustomExceptions;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.GRPAndroid.ProdProcessBOM;
using GherkinWebAPI.Persistence;
using GherkinWebAPI.Request.FinishedSFOpeningStockDetails;
using GherkinWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Repository
{
    public class FinishedSFOpeningStockRepository : RepositoryBase<FinishedSFStockProductDetails>, IFinishedSFOpeningStockRepository
    {
        private RepositoryContext _context;
        public FinishedSFOpeningStockRepository(RepositoryContext repositoryContext)
           : base(repositoryContext)
        {
            _context = repositoryContext;
        }

        public async Task<ApiResponse<List<OrganisationOfficeUnitDto>>> GetOrganisationOfficeUnitList()
        {
            ApiResponse<List<OrganisationOfficeUnitDto>> result = new ApiResponse<List<OrganisationOfficeUnitDto>>();
            try
            {
                var OrganisationOfficeUnitList = (from f in _context.OrganisationOfficeLocationDetails
                                                  select new OrganisationOfficeUnitDto
                                                  {
                                                      Org_Office_No = f.Org_Office_No,
                                                      Org_Code = f.Org_Code,
                                                      Org_Office_Name = f.Org_Office_Name.ToUpper(),
                                                  }).OrderBy(a => a.Org_Office_Name).ToListAsync();
                result.IsSucceed = true;
                result.Data = await OrganisationOfficeUnitList;
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

        public async Task<ApiResponse<List<Area>>> GetHarvestAreaList()
        {
            ApiResponse<List<Area>> result = new ApiResponse<List<Area>>();
            try
            {
                result.Data = await _context.Areas.OrderBy(a => a.Area_Name.ToUpper()).ToListAsync();
                result.IsSucceed = true;
                
            } catch(Exception ex)
            {
                result.IsSucceed = false;
                result.Exception = ex;
                result.ErrorMessages = new List<string>();
                result.ErrorMessages.Add(ex.Message);
            }


            return result;
        }

        public async Task<ApiResponse<List<CountryOverseas>>> GetCountryOverSeasList()
        {
            ApiResponse<List<CountryOverseas>> result = new ApiResponse<List<CountryOverseas>>();
            try
            {
                result.Data = await _context.countriesoverseas.OrderBy(a => a.W_Country_Name.ToUpper()).ToListAsync();
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

        public async Task<ApiResponse<List<ConsigneeBuyersDetails>>> GetConsigneeBuyersList(string overseasCountryId)
        {
            ApiResponse<List<ConsigneeBuyersDetails>> result = new ApiResponse<List<ConsigneeBuyersDetails>>();
            try
            {
                result.Data = await _context.Consignee_Buyers_Master.Where(c => c.W_Country_Id == overseasCountryId).OrderBy(a => a.C_B_Name.ToUpper()).ToListAsync();
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

        public async Task<ApiResponse<List<ProformaInvoiceDto>>> GetProformaInvoiceList(string cBCode)
        {
            ApiResponse<List<ProformaInvoiceDto>> result = new ApiResponse<List<ProformaInvoiceDto>>();
            try
            {
                var ProformaInvoiceList = (from p in _context.ProformaInvoiceDetails
                                      .Where(a => a.C_B_Code == cBCode)
                                           select new ProformaInvoiceDto
                                           {
                                               Prof_Inv_No = p.Prof_Inv_No,
                                               Prof_Invoice_Amount = p.Prof_Invoice_Amount,
                                               C_B_Code = p.C_B_Code
                                           }).OrderBy(a => a.Prof_Inv_No).ToListAsync();
                
                result.Data = await ProformaInvoiceList;
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

        public async Task<ApiResponse<List<ProductGroup>>> GetFinishedProductGroupList()
        {
            ApiResponse<List<ProductGroup>> result = new ApiResponse<List<ProductGroup>>();
            try
            {
                result.Data = await _context.ProductGroups.OrderBy(a => a.GrpName.ToUpper()).ToListAsync();
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

        public async Task<ApiResponse<List<ProductDetails>>> GetFinishedProductDetailsList(string GrpCode)
        {
            ApiResponse<List<ProductDetails>> result = new ApiResponse<List<ProductDetails>>();
            try
            {
                result.Data = await _context.ProductDetails.Where(p => p.GroupCode == GrpCode).OrderBy(a => a.VarietyName.ToUpper()).ToListAsync();
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

        public async Task<ApiResponse<List<ProductionProcessDetails>>> GetProductionProcessDetailsList(string VarietyCode)
        {
            ApiResponse<List<ProductionProcessDetails>> result = new ApiResponse<List<ProductionProcessDetails>>();
            try
            {
                result.Data = await _context.ProductionProcessDetail.Where(p => p.FPVarietyCode == VarietyCode).OrderBy(a => a.ProductionProcessName.ToUpper()).ToListAsync();
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

        public async Task<ApiResponse<List<MediaProcessDetails>>> GetMediaProcessDetailsList(string ProductionProcessCode)
        {
            ApiResponse<List<MediaProcessDetails>> result = new ApiResponse<List<MediaProcessDetails>>();
            try
            {
                result.Data = await _context.MediaProcessDetails.Where(p => p.ProductionProcessCode == ProductionProcessCode).OrderBy(a => a.MediaProcessName.ToUpper()).ToListAsync();
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

        public async Task<ApiResponse<List<GradeDetails>>> GetFPGradesDetailsList(string VarietyCode)
        {
            ApiResponse<List<GradeDetails>> result = new ApiResponse<List<GradeDetails>>();
            try
            {
                result.Data = await _context.gradeDetails.Where(p => p.VarietyCode == VarietyCode).OrderBy(a => a.GradeFrom).ToListAsync();
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

        public async Task<ApiResponse<List<ContainerPackingDetails>>> GetContainerPackingDetailsList()
        {
            ApiResponse<List<ContainerPackingDetails>> result = new ApiResponse<List<ContainerPackingDetails>>();
            try
            {
                result.Data = await _context.ContainerPackingDetails.OrderBy(a => a.Container_Name.ToUpper()).ToListAsync();
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

        public async Task<ApiResponse<List<GSCUomDetails>>> GetUOMDetailsList()
        {
            ApiResponse<List<GSCUomDetails>> result = new ApiResponse<List<GSCUomDetails>>();
            try
            {
                result.Data = await _context.GSCUomDetails.OrderBy(a => a.GSC_UOM_Name.ToUpper()).ToListAsync();
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

        public async Task<ApiResponse<object>> SaveFinishedSFOpeningStock(FinishedSFStockProductDetailsRequest finishedStkProdDetail)
        {

            ApiResponse<object> result = new ApiResponse<object>();
            try
            {
                foreach (var item in finishedStkProdDetail.FinishedSFStockQuantityDetailsList)
                {

                    var packDetail = _context.ContainerPackingDetails.Where(a => a.Container_Name.ToUpper() == item.ContainerName.ToUpper()).FirstOrDefault();
                    if (packDetail == null)
                    {
                        packDetail = new ContainerPackingDetails();
                        packDetail.Container_Name = item.ContainerName.ToUpper();
                        _context.ContainerPackingDetails.Add(packDetail);
                        _context.SaveChanges();
                    }
                    item.ContainerCode = packDetail.Container_Code;


                    var uomDetail = _context.GSCUomDetails.Where(a => a.GSC_UOM_Name.ToUpper() == item.GSCUOMName.ToUpper()).FirstOrDefault();
                    if (uomDetail == null)
                    {
                        uomDetail = new GSCUomDetails();
                        uomDetail.GSC_UOM_Name = item.GSCUOMName.ToUpper();
                        _context.GSCUomDetails.Add(uomDetail);
                        _context.SaveChanges();
                    }
                    item.GSCUOMCode = uomDetail.GSC_UOM_Code;
                }

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<FinishedSFStockQuantityDetailsRequest, FinishedSFStockQuantityDetails>();
                    cfg.CreateMap<FinishedSFStockProductDetailsRequest, FinishedSFStockProductDetails>().ForMember(dest => dest.FinishedSFStockQuantityDetailsList, act => act.MapFrom(src => src.FinishedSFStockQuantityDetailsList));
                });

                var mapper = new Mapper(config);
                var SFStockProductDetail = new FinishedSFStockProductDetails();
                SFStockProductDetail = mapper.DefaultContext.Mapper.Map<FinishedSFStockProductDetails>(finishedStkProdDetail);
                _context.FinishedSFStockProductDetails.Add(SFStockProductDetail);
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
               

        public async Task<ApiResponse<List<FinishedSFStockQuantityDetailsRequest>>> GetStockDetails(FinishedSFStockQuantityFindRequest finishedStkProdDetail)
        {

            ApiResponse<List<FinishedSFStockQuantityDetailsRequest>> result = new ApiResponse<List<FinishedSFStockQuantityDetailsRequest>>();
            try
            {
                var stockPoduct = (from a in _context.FinishedSFStockProductDetails
                                   join q in _context.FinishedSFStockQuantityDetails on a.FSFOSStockNo equals q.FSFOSStockNo
                                   where a.AreaID == finishedStkProdDetail.AreaID
                         && a.OrgOfficeNo == finishedStkProdDetail.OrgOfficeNo
                         && a.FSFStockType == finishedStkProdDetail.FSFStockType
                         && a.FSFPackingMode == finishedStkProdDetail.FSFPackingMode
                         && a.CBCode == finishedStkProdDetail.CBCode
                         && a.ProfInvNo == finishedStkProdDetail.ProfInvNo
                         && a.FPVarietyCode == finishedStkProdDetail.FPVarietyCode
                         && a.ProductionProcessCode == finishedStkProdDetail.ProductionProcessCode
                         && a.MediaProcessCode == finishedStkProdDetail.MediaProcessCode
                         && a.FPGradeCode == finishedStkProdDetail.FPGradeCode
                                   select new FinishedSFStockQuantityDetailsRequest
                                   {
                                       FSFStockQuantityNo = q.FSFStockQuantityNo,
                                       FSFStockProcessedDate = q.FSFStockProcessedDate,
                                       FSFOSStockNo = q.FSFOSStockNo,
                                       ContainerCode = q.ContainerCode,
                                       ContainerName = _context.ContainerPackingDetails.Where(c => c.Container_Code == q.ContainerCode).FirstOrDefault().Container_Name,
                                       QuantityContainer = q.FSFStockQuantityNo,
                                       GSCUOMCode = q.GSCUOMCode,
                                       GSCUOMName = _context.GSCUomDetails.Where(c => c.GSC_UOM_Code == q.GSCUOMCode).FirstOrDefault().GSC_UOM_Name,
                                       ContainerWeight = q.ContainerWeight,
                                       FSFNOofContainers = q.FSFNOofContainers,
                                       ContainerSlNoFrom = q.ContainerSlNoFrom,
                                       ContainerSlNoTo = q.ContainerSlNoTo,
                                       StockLocationDetails = q.StockLocationDetails,
                                       BarcodeOption = q.BarcodeOption,
                                   }).ToListAsync();


                result.Data = new List<FinishedSFStockQuantityDetailsRequest>();

                var data = await stockPoduct;
                result.Data = data;
                result.IsSucceed = true;
            }
            catch(Exception ex)
            {
                result.IsSucceed = false;
                result.Exception = ex;
            }


            return result;
        }

        public async Task<ApiResponse<object>> UpdateStockDetals(FinishedSFStockQuantityDetails finishedQtyDetail)
        {
            ApiResponse<object> result = new ApiResponse<object>();
            try
            {
                if(finishedQtyDetail.FSFStockQuantityNo != 0 || finishedQtyDetail != null)
                {
                    FinishedSFStockQuantityDetails quantityDetail = _context.FinishedSFStockQuantityDetails.FirstOrDefault(a => a.FSFStockQuantityNo == finishedQtyDetail.FSFStockQuantityNo);
                    if(quantityDetail != null)
                    {
                        quantityDetail.FSFStockProcessedDate = finishedQtyDetail.FSFStockProcessedDate;
                        quantityDetail.FSFOSStockNo = finishedQtyDetail.FSFOSStockNo;
                        quantityDetail.ContainerCode = finishedQtyDetail.ContainerCode;
                        quantityDetail.QuantityContainer = finishedQtyDetail.QuantityContainer;
                        quantityDetail.GSCUOMCode = finishedQtyDetail.GSCUOMCode;
                        quantityDetail.ContainerWeight = finishedQtyDetail.ContainerWeight;
                        quantityDetail.FSFNOofContainers = finishedQtyDetail.FSFNOofContainers;
                        quantityDetail.ContainerSlNoFrom = finishedQtyDetail.ContainerSlNoFrom;
                        quantityDetail.ContainerSlNoTo = finishedQtyDetail.ContainerSlNoTo;
                        quantityDetail.StockLocationDetails = finishedQtyDetail.StockLocationDetails;
                        quantityDetail.BarcodeOption = finishedQtyDetail.BarcodeOption;
                        _context.SaveChanges();
                        result.Data = quantityDetail;
                        result.IsSucceed = true;
                    }
                    
                }
                else
                {
                    result.IsSucceed = false;
                    result.ErrorMessages = new List<string>();
                    result.ErrorMessages.Add("Invalid Item Passed");
                }
                

            }
            catch(Exception ex)
            {
                result.IsSucceed = false;
                result.Exception = ex;
            }
            return result;
        }

        public async Task<ApiResponse<object>> DeleteStockDetals(int FSFStockQuantityNo)
        {
            ApiResponse<object> result = new ApiResponse<object>();
            try
            {
                FinishedSFStockQuantityDetails quantityDetail = _context.FinishedSFStockQuantityDetails.FirstOrDefault(a => a.FSFStockQuantityNo == FSFStockQuantityNo);
                if (quantityDetail != null)
                {
                    _context.FinishedSFStockQuantityDetails.Remove(quantityDetail);
                    _context.SaveChanges();
                    result.IsSucceed = true;
                }
            }
            catch (Exception ex)
            {
                result.IsSucceed = false;
                result.Exception = ex;
            }
            return result;
        }
    }
    

}