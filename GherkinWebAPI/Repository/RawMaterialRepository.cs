using AutoMapper;
using GherkinWebAPI.Core;
using GherkinWebAPI.CustomExceptions;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Entities;
using GherkinWebAPI.Models;
using GherkinWebAPI.Persistence;
using GherkinWebAPI.Request;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Data.Entity.Migrations;

namespace GherkinWebAPI.Repository
{
    public class RawMaterialRepository : RepositoryBase<RawMaterialMaster>, IRawMaterialRepository
    {
        private RepositoryContext _context;

        public RawMaterialRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
            this._context = repositoryContext;
        }

        public async Task<RawMaterialDetailsDto> CreateRawMaterialDetails(RawMaterialDetails rawMaterialDetails)
        {
            var id = await this._context.Database.SqlQuery<int>(
                        "USP_InsertRawMaterialDetails @Raw_Material_Group_Code, @Raw_Material_Details_Name, @Raw_Material_QC_Norms, @Raw_Material_UOM, @Raw_Material_Reorder_Stock, @Raw_Material_HSN_CODE_No, @Raw_Material_IGST_Rate, @Raw_Material_CGST_Rate, @Raw_Material_SGST_Rate, @Raw_Material_Cess_Rate",
                        new SqlParameter("Raw_Material_Group_Code", rawMaterialDetails.Raw_Material_Group_Code),
                        new SqlParameter("Raw_Material_Details_Name", rawMaterialDetails.Raw_Material_Details_Name),
                        new SqlParameter("Raw_Material_QC_Norms", rawMaterialDetails.Raw_Material_QC_Norms),
                        new SqlParameter("Raw_Material_UOM", rawMaterialDetails.Raw_Material_UOM),
                        new SqlParameter("Raw_Material_Reorder_Stock", rawMaterialDetails.Raw_Material_Reorder_Stock),
                        new SqlParameter("Raw_Material_HSN_CODE_No", rawMaterialDetails.Raw_Material_HSN_CODE_No),
                        new SqlParameter("Raw_Material_IGST_Rate", rawMaterialDetails.Raw_Material_IGST_Rate),
                        new SqlParameter("Raw_Material_CGST_Rate", rawMaterialDetails.Raw_Material_CGST_Rate),
                        new SqlParameter("Raw_Material_SGST_Rate", rawMaterialDetails.Raw_Material_SGST_Rate),
                        new SqlParameter("Raw_Material_Cess_Rate", rawMaterialDetails.Raw_Material_Cess_Rate)
                    ).FirstAsync();

            var r = await this._context.RawMaterialDetails.FirstAsync(x => x.ID == id);

            return Mapper.Map<RawMaterialDetailsDto>(r);
        }

        public async Task CreateRawMaterialGroup(RawMaterialMaster rawMaterialGroupMaster)
        {
            var res = await this._context.RawMaterialGroupMaster.FirstOrDefaultAsync(x => x.Material_Purchases == rawMaterialGroupMaster.Material_Purchases
                                                           && x.Raw_Material_Group == rawMaterialGroupMaster.Raw_Material_Group);
            if (res == null)
            {
                await this._context.Database.SqlQuery<List<string>>(
                                "USP_InsertRawMaterialMaster @material_Purchases, @raw_Material_Group",
                                new SqlParameter("material_Purchases", rawMaterialGroupMaster.Material_Purchases),
                                new SqlParameter("raw_Material_Group", rawMaterialGroupMaster.Raw_Material_Group)
                            ).FirstOrDefaultAsync();
            }
            else
            {
                throw new CustomException("GROUP NAME ALREADY EXIST WITH MATERIAL PURCHASE");
            }
        }

        public async Task UpdateRawMaterialGroup(RawMaterialMaster rawMaterialGroupMaster)
        {
            try
            {
                _context.RawMaterialGroupMaster.AddOrUpdate(rawMaterialGroupMaster);
                var result = await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<RawMaterialDetailsDto>> GetRawmaterialDetails()
        {
            var result = await this._context.RawMaterialDetails.AsNoTracking().ToListAsync();

            List<RawMaterialDetailsDto> list = new List<RawMaterialDetailsDto>();

            foreach (var i in result)
            {
                list.Add(Mapper.Map<RawMaterialDetailsDto>(i));
            }
            return list;
        }

        public async Task<List<RawMaterialMasterDto>> GetRawMaterialMaster()
        {
            var result = await FindAll().ToListAsync();

            List<RawMaterialMasterDto> list = new List<RawMaterialMasterDto>();

            foreach (var i in result)
            {
                list.Add(Mapper.Map<RawMaterialMasterDto>(i));
            }
            return list;
        }

        public async Task<RawMaterialDetailsDto> UpdateRawMaterialDetails(string id, RawMaterialDetailsRequest rawMaterialDetailsReq)
        {
            var detail = await this._context.RawMaterialDetails.FirstOrDefaultAsync(x => x.Raw_Material_Details_Code == id);
            if (detail != null)
            {
                var master = await this._context.RawMaterialGroupMaster.FirstOrDefaultAsync(x => x.Raw_Material_Group_Code == rawMaterialDetailsReq.Raw_Material_Group_Code);
                if (master != null)
                {
                    detail.Raw_Material_Cess_Rate = rawMaterialDetailsReq.Raw_Material_Cess_Rate;
                    detail.Raw_Material_CGST_Rate = rawMaterialDetailsReq.Raw_Material_CGST_Rate;
                    detail.Raw_Material_Details_Name = rawMaterialDetailsReq.Raw_Material_Details_Name;
                    detail.Raw_Material_Group_Code = rawMaterialDetailsReq.Raw_Material_Group_Code;
                    detail.Raw_Material_HSN_CODE_No = rawMaterialDetailsReq.Raw_Material_HSN_CODE_No;
                    detail.Raw_Material_IGST_Rate = rawMaterialDetailsReq.Raw_Material_IGST_Rate;
                    detail.Raw_Material_QC_Norms = rawMaterialDetailsReq.Raw_Material_QC_Norms;
                    detail.Raw_Material_Reorder_Stock = rawMaterialDetailsReq.Raw_Material_Reorder_Stock;
                    detail.Raw_Material_SGST_Rate = rawMaterialDetailsReq.Raw_Material_SGST_Rate;
                    detail.Raw_Material_UOM = rawMaterialDetailsReq.Raw_Material_UOM;
                    this._context.SaveChanges();
                }
                else
                {
                    throw new CustomException("NO MATERIAL GROUP CODE FOUND");
                }
            }
            else
            {
                throw new CustomException("NO DETAILS FOUND FOR ID");
            }
            var r = await this._context.RawMaterialDetails.FirstAsync(x => x.ID == detail.ID);

            return Mapper.Map<RawMaterialDetailsDto>(r);
        }

        public async Task<List<RawMaterialDetailsDto>> GetRMDeatilsCodeNameByGroupCode(string rawMaterialGroupCode)
        {
            var _rawMaterialDeatils = from _rm in _context.RawMaterialDetails
                                      where _rm.Raw_Material_Group_Code == rawMaterialGroupCode
                                      select new RawMaterialDetailsDto()
                                      {
                                          Raw_Material_Details_Code = _rm.Raw_Material_Details_Code,
                                          Raw_Material_Details_Name = _rm.Raw_Material_Details_Name
                                      };

            return await _rawMaterialDeatils.ToListAsync();
        }

        public async Task<RMUom> CreateRawMaterialUOM(RMUom rMUomDto)
        {
            var res = this._context.RMUom.Count();
            var rmUomPk = res == 0 ? 1 : this._context.RMUom.Select(x => x.Raw_Material_UOM).ToList().Select(x => Convert.ToInt32(x)).Max() + 1;
            rMUomDto.Raw_Material_UOM = Convert.ToString(rmUomPk);
            this._context.RMUom.Add(rMUomDto);
            await _context.SaveChangesAsync();
            return rMUomDto;
        }

        public async Task<List<RMUom>> GetAllUom()
        {
            return await this._context.RMUom.ToListAsync();
        }
    }
}