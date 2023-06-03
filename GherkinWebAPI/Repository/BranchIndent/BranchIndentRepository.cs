using AutoMapper;
using GherkinWebAPI.Core.BranchIndent;
using GherkinWebAPI.DTO.BranchIndent;
using GherkinWebAPI.Entities.BranchIndent;
using GherkinWebAPI.Models.Branch_Indent;
using GherkinWebAPI.Persistence;
using GherkinWebAPI.Request.BranchIndent;
using GherkinWebAPI.Response.BranchIndent;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Repository.BranchIndent
{
    public class BranchIndentRepository : RepositoryBase<BranchIndentDetails>, IBranchIndentRepository
    {
        private RepositoryContext _context;

        public BranchIndentRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
            this._context = repositoryContext;
        }

        public async Task<BranchIndentResponse> InsertBranchIndentDetails(BranchIndentInsertRequest branchIndentInsertRequest)
        {
            BranchIndentResponse branchIndentResponse = new BranchIndentResponse();
            using (DbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var id = await this._context.Database.SqlQuery<int>(
                        "USP_InsertBranchIndentDetail @RM_Indent_Entry_Date, @RM_Indent_Emp_ID, @Area_ID, @Request_To, @Org_Office_No",
                       new SqlParameter("RM_Indent_Entry_Date", branchIndentInsertRequest.BranchIndentDetails.RM_Indent_Entry_Date),
                       new SqlParameter("RM_Indent_Emp_ID", branchIndentInsertRequest.BranchIndentDetails.RM_Indent_Emp_ID),
                       new SqlParameter("Area_ID", branchIndentInsertRequest.BranchIndentDetails.Area_ID ?? SqlString.Null),
                       new SqlParameter("Org_Office_No", branchIndentInsertRequest.BranchIndentDetails.Org_Office_No ?? SqlString.Null),
                       new SqlParameter("Request_To", branchIndentInsertRequest.BranchIndentDetails.Request_To)
                   ).FirstAsync();

                    var r = await this._context.branchIndentDetails.FirstAsync(x => x.ID == id);

                    branchIndentResponse.BranchIndentDetails = Mapper.Map<BranchIndentDetailsDto>(r);


                    foreach (var item in branchIndentInsertRequest.BranchIndentMaterialDetails)
                    {
                        var id1 = await this._context.Database.SqlQuery<int>(
                         "USP_InsertBranchIndentMaterialDetail @RM_Indent_No, @Raw_Material_Group_Code, @Raw_Material_Details_Code, @RM_Stock_On_Date, @RM_Indent_Req_Qty, @RM_Require_Date, @RM_Remark, @RM_UOM",
                    new SqlParameter("RM_Indent_No", r.RM_Indent_No),
                    new SqlParameter("Raw_Material_Group_Code", item.Raw_Material_Group_Code),
                    new SqlParameter("Raw_Material_Details_Code", item.Raw_Material_Details_Code),
                    new SqlParameter("RM_Stock_On_Date", item.RM_Stock_On_Date),
                    new SqlParameter("RM_Indent_Req_Qty", item.RM_Indent_Req_Qty),
                    new SqlParameter("RM_Require_Date", item.RM_Require_Date),
                    new SqlParameter("RM_Remark", item.RM_Remarks),
                    new SqlParameter("RM_UOM", item.RM_UOM)
                     ).FirstAsync();
                        var r1 = await this._context.branchIndentMaterialDetails.FirstAsync(x => x.ID == id1);
                        var itemDto = Mapper.Map<BranchIndentMaterialDetailsDto>(r1);
                        branchIndentResponse.BranchIndentMaterialDetails.Add(itemDto);
                    }


                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }

            }

            return branchIndentResponse;
        }

        public async Task<List<BranchIndentMaterialDetailsDto>> GetRMUOM()
        {
            var uom = await (from material in _context.branchIndentMaterialDetails
                             select new BranchIndentMaterialDetailsDto { RM_UOM = material.RM_UOM }).Distinct().ToListAsync();

            return uom;
        }
        
        public async Task<List<BranchIndentResponse>> GetAllIndentRequest()
        {

            var result = new List<BranchIndentResponse>();
            var indents = await (from indent in _context.branchIndentDetails
                                 select new BranchIndentDetailsDto {
                                 ID = indent.ID,
                                 Area_ID = indent.Area_ID,
                                 Request_To = indent.Request_To,
                                 RM_Indent_Emp_ID = indent.RM_Indent_Emp_ID,
                                 RM_Indent_Entry_Date = indent.RM_Indent_Entry_Date,
                                 RM_Indent_No = indent.RM_Indent_No
                                 }).ToListAsync();

            var material = (from mat in _context.branchIndentMaterialDetails
                            join details in _context.RawMaterialDetails on mat.Raw_Material_Details_Code equals details.Raw_Material_Details_Code
                            select new BranchIndentMaterialDetailsDto
                            {
                                ID = mat.ID,
                                Raw_Material_Details_Code = mat.Raw_Material_Details_Code,
                                Raw_Material_Group_Code = mat.Raw_Material_Group_Code,
                                RM_Indent_No = mat.RM_Indent_No,
                                RM_Indent_Req_Qty = mat.RM_Indent_Req_Qty,
                                RM_Remarks = mat.RM_Remarks,
                                RM_Require_Date = mat.RM_Require_Date,
                                RM_Stock_On_Date = mat.RM_Stock_On_Date,
                                RM_UOM = mat.RM_UOM,
                                Raw_Material_Details_Name = details.Raw_Material_Details_Name
                            }).ToList();

            indents.ForEach(a =>
            {
                var indentRes = new BranchIndentResponse();
                indentRes.BranchIndentDetails = a;
               
                indentRes.BranchIndentMaterialDetails = material.Where(s=>s.RM_Indent_No == a.RM_Indent_No).ToList();
                result.Add(indentRes);
            });




            return result;
        }

        public async Task<bool> UpdateIndentMaterial(BranchIndentMaterialDetailsModel branchIndentMaterialDetailsModel)
        {
            var selectedItem = _context.branchIndentMaterialDetails.SingleOrDefault(a => a.ID == branchIndentMaterialDetailsModel.ID);
            selectedItem.Raw_Material_Details_Code = branchIndentMaterialDetailsModel.Raw_Material_Details_Code;
            selectedItem.Raw_Material_Group_Code = branchIndentMaterialDetailsModel.Raw_Material_Group_Code;
            //selectedItem.RM_Indent_No = branchIndentMaterialDetailsModel.RM_Indent_No;
            selectedItem.RM_Indent_Req_Qty = branchIndentMaterialDetailsModel.RM_Indent_Req_Qty;
            selectedItem.RM_Remarks = branchIndentMaterialDetailsModel.RM_Remarks;
            selectedItem.RM_Require_Date = branchIndentMaterialDetailsModel.RM_Require_Date;
            selectedItem.RM_Stock_On_Date = branchIndentMaterialDetailsModel.RM_Stock_On_Date;
            selectedItem.RM_UOM = branchIndentMaterialDetailsModel.RM_UOM;

            var res = await _context.SaveChangesAsync();
            return res > 0;
        }
    }
}