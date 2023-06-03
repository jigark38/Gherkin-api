using AutoMapper;
using GherkinWebAPI.Core;
using GherkinWebAPI.DTO;
using GherkinWebAPI.DTO.RMStock;
using GherkinWebAPI.Entities.RMStock;
using GherkinWebAPI.Persistence;
using GherkinWebAPI.Request.RMStock;
using GherkinWebAPI.Response.RMStock;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using GherkinWebAPI.Utilities;
using System.Data.Entity.Infrastructure;

namespace GherkinWebAPI.Repository
{
    public class RmStockDetailsRepository : RepositoryBase<RMStockDetails>, IRmStockDetailsRepository
    {

        private RepositoryContext _context;

        public RmStockDetailsRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
            this._context = repositoryContext;
        }

        public async Task<List<RMStockBranchQuantityDetailDTO>> FindRMStockBranchDetails(string areaId)
        {
            List<RMStockBranchQuantityDetailDTO> listRes = new List<RMStockBranchQuantityDetailDTO>();
            var list = from d in _context.RMStockBranchQuantityDetails
                       join m in _context.RMStockBranchDetails
                       on d.Stock_No equals m.Stock_No
                       join rmg in _context.RawMaterialGroupMaster
                       on d.Raw_Material_Group_Code equals rmg.Raw_Material_Group_Code
                       join rdg in _context.RawMaterialDetails
                       on d.Raw_Material_Details_Code equals rdg.Raw_Material_Details_Code
                       where m.Area_ID == areaId
                       select new { m.Stock_Date, d.Raw_Material_Details_Code, d.Raw_Material_Group_Code, d.ID, d.Raw_Material_UOM, d.Stock_No, d.RM_Stock_Quantity, rmg.Raw_Material_Group, rdg.Raw_Material_Details_Name };

            foreach (var r in list)
            {
                listRes.Add(new RMStockBranchQuantityDetailDTO
                {
                    ID = r.ID,
                    Stock_Date = r.Stock_Date,
                    Stock_No = r.Stock_No,
                    Raw_Material_UOM = r.Raw_Material_UOM,
                    RM_Stock_Quantity = r.RM_Stock_Quantity,
                    Raw_Material_Group_Code_Name = r.Raw_Material_Group,
                    Raw_Material_Details_Code_Name = r.Raw_Material_Details_Name,
                    Raw_Material_Group_Code = r.Raw_Material_Group_Code,
                    Raw_Material_Details_Code = r.Raw_Material_Details_Code
                });
            }
            return listRes;
        }

        public async Task<RMStockBranchInsertResponse> InsertRMStockBranchDetails(RMStockBranchInsertRequest rMStockBranchInsertRequest)
        {
            RMStockBranchInsertResponse rmStockBranchInsertResponse = new RMStockBranchInsertResponse();

            var ifallUnique = rMStockBranchInsertRequest.RMStockBranchQuantityModel.Select(x => x.Raw_Material_Details_Code).Distinct().Count() == rMStockBranchInsertRequest.RMStockBranchQuantityModel.Select(x => x.Raw_Material_Details_Code).Count();

            if (!ifallUnique)
            {
                rmStockBranchInsertResponse.RMStockBranchDto = null;
                rmStockBranchInsertResponse.RMStockBranchQuantityDetailDTO = null;
                rmStockBranchInsertResponse.ResponseMessage = "Duplicate Raw Material Details Received";
                return rmStockBranchInsertResponse;
            }

            using (DbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var id = await this._context.Database.SqlQuery<int>(
                       "USP_InsertRMStockBranchDetails @Stock_Date, @Area_ID, @Area_Code",
                       new SqlParameter("Stock_Date", rMStockBranchInsertRequest.RMStockBranchDetailsModel.Stock_Date),
                       new SqlParameter("Area_ID", rMStockBranchInsertRequest.RMStockBranchDetailsModel.Area_ID),
                       new SqlParameter("Area_Code", rMStockBranchInsertRequest.RMStockBranchDetailsModel.Area_Code)
                   ).FirstAsync();

                    var r = await this._context.RMStockBranchDetails.FirstAsync(x => x.ID == id);

                    rmStockBranchInsertResponse.RMStockBranchDto = Mapper.Map<RMStockBranchDto>(r);
                    foreach (var lotDetail in rMStockBranchInsertRequest.RMStockBranchQuantityModel)
                    {
                        var id1 = await this._context.Database.SqlQuery<int>(
                        "USP_InsertRMStockBranchQuantityDetails @Stock_No, @Raw_Material_Group_Code, @Raw_Material_Details_Code, @Raw_Material_UOM, @RM_Stock_Quantity",
                        new SqlParameter("Stock_No", r.Stock_No),
                        new SqlParameter("Raw_Material_Group_Code", lotDetail.Raw_Material_Group_Code),
                        new SqlParameter("Raw_Material_Details_Code", lotDetail.Raw_Material_Details_Code),
                        new SqlParameter("Raw_Material_UOM", lotDetail.Raw_Material_UOM),
                        new SqlParameter("RM_Stock_Quantity", lotDetail.RM_Stock_Quantity)
                         ).FirstAsync();
                        var lotDetailDto = Mapper.Map<RMStockBranchQuantityDetailDTO>(lotDetail);
                        lotDetailDto.Stock_No = r.Stock_No;
                        lotDetailDto.ID = id1;
                        rmStockBranchInsertResponse.RMStockBranchQuantityDetailDTO.Add(lotDetailDto);
                    }


                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    rmStockBranchInsertResponse = null;
                    throw ex;
                }

            }

            return rmStockBranchInsertResponse;
        }

        public async Task<ApiResponse<RMStockFormInsertResponse>> InsertRMStockDetails(RMStockInsertRequest rMStockInsertRequest)
        {
            ApiResponse<RMStockFormInsertResponse> result = new ApiResponse<RMStockFormInsertResponse>();
            var item = await _context.RMStockDetails.Where(a => a.Raw_Material_Details_Code == rMStockInsertRequest.RMStocksDetails.Raw_Material_Details_Code).FirstOrDefaultAsync();
            if(item != null)
            {
                result.IsSucceed = false;
                result.ErrorMessages = new List<string>();
                result.ErrorMessages.Add("Material Name already inserted");
                return result;
            }
            RMStockFormInsertResponse rMStockFormInsertResponse = new RMStockFormInsertResponse();
            using (DbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var id = await this._context.Database.SqlQuery<int>(
                       "USP_InsertRMStockDetails @Stock_Date, @Org_office_No, @Nature_office_Details, @Raw_Material_Group_Code, @Raw_Material_Details_Code, @Raw_Material_UOM, @RM_Stock_Total_Detailed_Qty, @Raw_Material_Total_QTY, @Raw_Material_Total_Amount",
                       new SqlParameter("Stock_Date", rMStockInsertRequest.RMStocksDetails.Stock_Date),
                       new SqlParameter("Org_office_No", rMStockInsertRequest.RMStocksDetails.Org_office_No),
                       new SqlParameter("Nature_office_Details", rMStockInsertRequest.RMStocksDetails.Nature_office_Details),
                       new SqlParameter("Raw_Material_Group_Code", rMStockInsertRequest.RMStocksDetails.Raw_Material_Group_Code),
                       new SqlParameter("Raw_Material_Details_Code", rMStockInsertRequest.RMStocksDetails.Raw_Material_Details_Code),
                       new SqlParameter("Raw_Material_UOM", rMStockInsertRequest.RMStocksDetails.Raw_Material_UOM),
                       new SqlParameter("RM_Stock_Total_Detailed_Qty", rMStockInsertRequest.RMStocksDetails.RM_Stock_Total_Detailed_Qty),
                       new SqlParameter("Raw_Material_Total_QTY", rMStockInsertRequest.RMStocksDetails.Raw_Material_Total_QTY),
                       new SqlParameter("Raw_Material_Total_Amount", rMStockInsertRequest.RMStocksDetails.Raw_Material_Total_Amount)
                   ).FirstAsync();

                    var r = await this._context.RMStockDetails.FirstAsync(x => x.ID == id);

                    rMStockFormInsertResponse.RMStockDetailsDto = Mapper.Map<RMStockDetailsDto>(r);
                    foreach (var lotDetail in rMStockInsertRequest.RMStockLotDetails)
                    {
                        var id1 = await this._context.Database.SqlQuery<string>(
                        "USP_InsertRMStockLotDetails @Stock_No, @RM_Stock_LOT_GRN_Date, @RM_Stock_LOT_GRN_No, @RM_Stock_Lot_Grn_Qty, @RM_Stock_Lot_Grn_Rate, @RM_Stock_Lot_Grn_Amount",
                        new SqlParameter("Stock_No", r.Stock_No),
                        new SqlParameter("RM_Stock_LOT_GRN_Date", lotDetail.RM_Stock_LOT_GRN_Date),
                        new SqlParameter("RM_Stock_LOT_GRN_No", lotDetail.RM_Stock_LOT_GRN_No),
                        new SqlParameter("RM_Stock_Lot_Grn_Qty", lotDetail.RM_Stock_Lot_Grn_Qty),
                        new SqlParameter("RM_Stock_Lot_Grn_Rate", lotDetail.RM_Stock_Lot_Grn_Rate),
                        new SqlParameter("RM_Stock_Lot_Grn_Amount", lotDetail.RM_Stock_Lot_Grn_Amount)
                         ).FirstAsync();
                        var lotDetailDto = Mapper.Map<RMStockLotDetailsDto>(lotDetail);
                        lotDetailDto.Stock_No = r.Stock_No;
                        rMStockFormInsertResponse.RMStockLotDetailsDto.Add(lotDetailDto);
                    }

                    result.IsSucceed = true;
                    result.Data = rMStockFormInsertResponse;
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    result.IsSucceed = false;
                    result.Exception = ex;
                    result.ErrorMessages = new List<string>();
                    result.ErrorMessages.Add(ex.Message);
                    transaction.Rollback();
                }

            }

            return result;
        }

        public async Task UpdateRMStockBranchDetails(List<RMStockBranchQuantityDetailDTO> rMBranchUpdateDetailsModel)
        {
            using (DbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    foreach (var item in rMBranchUpdateDetailsModel)
                    {
                        var rmBranchQuanity = await _context.RMStockBranchQuantityDetails.FirstAsync(x => x.ID == item.ID);
                        rmBranchQuanity.Raw_Material_Details_Code = item.Raw_Material_Details_Code;
                        rmBranchQuanity.Raw_Material_Group_Code = item.Raw_Material_Group_Code;
                        rmBranchQuanity.Raw_Material_UOM = item.Raw_Material_UOM;
                        rmBranchQuanity.RM_Stock_Quantity = item.RM_Stock_Quantity;

                        await _context.SaveChangesAsync();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }

            }

        }

        public async Task<ApiResponse<RMStockDetailsDto>> FIndRMStockDetail(string Org_office_No, string Raw_Material_Group_Code, string Raw_Material_Details_Code)
        {
            ApiResponse<RMStockDetailsDto> result = new ApiResponse<RMStockDetailsDto>();
            try
            {
                var RMStockDetailsItem = await _context.RMStockDetails.Include(a => a.RMStockLotDetailsList).Where(s => s.Org_office_No == Org_office_No && s.Raw_Material_Details_Code == Raw_Material_Details_Code && s.Raw_Material_Group_Code == Raw_Material_Group_Code).FirstOrDefaultAsync();

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<RMStockLotDetails, RMStockLotDetailsDto>();
                    cfg.CreateMap<RMStockDetails, RMStockDetailsDto>().ForMember(dest => dest.RMStockLotDetailsList, act => act.MapFrom(src => src.RMStockLotDetailsList));
                });

                var mapper = new Mapper(config);
                var rMStockDetailsDto = new RMStockDetailsDto();
                rMStockDetailsDto = mapper.DefaultContext.Mapper.Map<RMStockDetailsDto>(RMStockDetailsItem);


                result.Data = rMStockDetailsDto;
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

        public async Task<ApiResponse<object>> UpdateRMStockDetail(RMStockDetailsDto newRMStockDetailsDtoItem)
        {
            ApiResponse<object> result = new ApiResponse<object>();
            try
            {
                var existingRMStockDetailsItem = _context.RMStockDetails.Include(a => a.RMStockLotDetailsList).Where(s => s.ID == newRMStockDetailsDtoItem.ID).FirstOrDefault();

                existingRMStockDetailsItem.RM_Stock_Total_Detailed_Qty = newRMStockDetailsDtoItem.RM_Stock_Total_Detailed_Qty;
                existingRMStockDetailsItem.Raw_Material_Total_QTY = newRMStockDetailsDtoItem.Raw_Material_Total_QTY;
                existingRMStockDetailsItem.Raw_Material_Total_Amount = newRMStockDetailsDtoItem.Raw_Material_Total_Amount;
                if (newRMStockDetailsDtoItem.RMStockLotDetailsList.Count > 0)
                {
                    foreach (var item in newRMStockDetailsDtoItem.RMStockLotDetailsList)
                    {
                        List<RMStockLotDetails> rMStockLotDetailList = new List<RMStockLotDetails>();
                        if (item.RM_stock_Lot_Details_ID > 0)
                        {
                            var existingItm = existingRMStockDetailsItem.RMStockLotDetailsList.Where(a => a.RM_stock_Lot_Details_ID == item.RM_stock_Lot_Details_ID).FirstOrDefault();
                            existingItm.RM_Stock_LOT_GRN_Date = item.RM_Stock_LOT_GRN_Date;
                            existingItm.RM_Stock_LOT_GRN_No = item.RM_Stock_LOT_GRN_No;
                            existingItm.RM_Stock_Lot_Grn_Qty = item.RM_Stock_Lot_Grn_Qty;
                            existingItm.RM_Stock_Lot_Grn_Rate = item.RM_Stock_Lot_Grn_Rate;
                            existingItm.RM_Stock_Lot_Grn_Amount = item.RM_Stock_Lot_Grn_Amount;
                        }
                        else
                        {
                            RMStockLotDetails rMStockLotDetails = new RMStockLotDetails()
                            {
                                Stock_No = newRMStockDetailsDtoItem.Stock_No,
                                RM_Stock_LOT_GRN_Date = item.RM_Stock_LOT_GRN_Date,
                                RM_Stock_LOT_GRN_No = item.RM_Stock_LOT_GRN_No,
                                RM_Stock_Lot_Grn_Qty = item.RM_Stock_Lot_Grn_Qty,
                                RM_Stock_Lot_Grn_Rate = item.RM_Stock_Lot_Grn_Rate,
                                RM_Stock_Lot_Grn_Amount = item.RM_Stock_Lot_Grn_Amount,                                
                            };
                            rMStockLotDetailList.Add(rMStockLotDetails);
                            _context.RMStockLotDetails.AddRange(rMStockLotDetailList);
                        }
                            
                    }

                   var response = await _context.SaveChangesAsync();

                }               


                result.Data = null;
                result.IsSucceed = true;

            }
            catch (DbUpdateException e)
            {
                var sqlException = e.GetBaseException() as SqlException;
                if (sqlException != null)
                {
                    if (sqlException.Errors.Count > 0)
                    {
                        switch (sqlException.Errors[0].Number)
                        {
                            //case 547: // Foreign Key violation
                            //    ModelState.AddModelError("CodeInUse", "Country code could not be deleted, because it is in use");
                            //    return View(viewModel.First());
                            //default:
                            //    throw;
                        }
                    }
                }
                else
                {
                    throw;
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
    }
}