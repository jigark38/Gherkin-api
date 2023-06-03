using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using AutoMapper;
using GherkinWebAPI.Core.MaterialInward;
using GherkinWebAPI.DTO.MaterialInward;
using GherkinWebAPI.Entities.MaterialInward;
using GherkinWebAPI.Persistence;
using GherkinWebAPI.Request.MaterialInward;
using System.Linq;
using GherkinWebAPI.Models.GoodsReceiptNote;
using GherkinWebAPI.Models.MaterialOutward;
using GherkinWebAPI.Models.InputTransferDetails;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.PurchageMgmt;

namespace GherkinWebAPI.Repository.MaterialInward
{
    public class MaterialInwardRepository : RepositoryBase<MaterialInwardEntity>, IMaterialRepository
    {
        private RepositoryContext _context;

        public MaterialInwardRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
            this._context = repositoryContext;
        }

        public async Task<MaterialOutwardDetails> UpMaterialOutwardDetails(MaterialOutwardDetails materialOutwardDetails)
        {

            var result = _context.MaterialOutwardDetails.Add(materialOutwardDetails);

            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<List<MaterialInwardDto>> FindMaterialInward(DateTime dateFrom, DateTime dateTo, string inwardType)
        {
            List<MaterialInwardDto> res = new List<MaterialInwardDto>();
            var data = this._context.MaterialInwardEntity.Where(x => x.Inward_Date_Time >= dateFrom && x.Inward_Date_Time <= dateTo && x.Inward_Type == inwardType);
            foreach (var l in data)
            {
                res.Add(Mapper.Map<MaterialInwardDto>(l));
            }
            return res;
        }

        public async  Task<List<MaterialOutwardDetails>> GetAllMaterialOutwardDetails()
        {

            var subselect = await _context.MaterialOutwardDetails.Select(i => i.RmTransferNo).ToListAsync();

            var inptTran = _context.InputTransferDetails.Where(j => !subselect.Contains(j.TransferNumber)).ToList();
            var list = new List<MaterialOutwardDetails>();

            if (inptTran != null)
            {
                foreach(var i in inptTran)
                {

                    var area = await _context.Areas.Where(j => j.Area_ID == i.AreaId).FirstOrDefaultAsync();
                    var gatepass = await _context.OutwardGatePassDetails.Where(j => j.transactionNo == i.TransferNumber).FirstOrDefaultAsync();
                    int? rawMatCount = await _context.RMInputMaterialTransferDetails.Where(j => j.rmTransferNo == i.TransferNumber)?.CountAsync();

                    list.Add(new MaterialOutwardDetails()
                    {
                        Id = gatepass.Id,
                        AreaId = i.AreaId,
                        RmTransferNo = i.TransferNumber,
                        AreaName = area.Area_Name,
                        OgpDate = gatepass.ogpDate,
                        OgpNumber = gatepass.ogpNo,
                        TotalMaterial = rawMatCount ?? 0
                    });
                }

                return list.OrderByDescending(X => X.Id).ToList();
            }
            
            return null;
        }

        public async Task<List<InwardDetail>> GetMaterialInwardDetailsAsync()
        {
            var inwardDetails = await _context.MaterialInwardEntity
                               .Where(r => !_context.RMGRNDetails
                               .Any(m => m.InwardGatePassNo == r.Inward_Gate_Pass_No) && r.Inward_Type.Equals("Other Raw Materials", StringComparison.InvariantCulture))
                               .Select(i => new InwardDetail
                               {
                                   InwardType = i.Inward_Type,
                                   InwardDateTime = i.Inward_Date_Time,
                                   InwardGatePassNo = i.Inward_Gate_Pass_No.Substring(4),
                                   SupplierTransporterName = i.Supplier_Transporter_Name,
                                   SupplierTransporterPlace = i.Supplier_Transporter_Place,
                                   InvDCNo = i.Inv_DC_No,
                                   InvDCDate = i.Inv_DC_Date,
                                   ReceivedMaterialName = i.Received_Material_Name,
                                   ReceivedQuantity = i.Received_Quantity,
                                   QCTest = null
                               }).ToListAsync();

            return inwardDetails;
        }

        public async Task<MaterialInwardDto> InsertMaterialInward(InsertMaterialInwardRequest insertMaterialInwardRequest)
        {
            var id = await this._context.Database.SqlQuery<int>(
                       "USP_InsertMaterialInward @Inward_Type, @Inward_Date_Time, @Supplier_Transporter_Name, @Supplier_Transporter_Place, @Inv_DC_No, @Inv_DC_Date, @Inv_Vehicle_No, @Driver_Name, @Employee_No, @Inward_Remarks, @Received_Material_Name, @Received_Quantity, @Org_Office_No",
                       new SqlParameter("Inward_Type", insertMaterialInwardRequest.Inward_Type),
                       new SqlParameter("Inward_Date_Time", insertMaterialInwardRequest.Inward_Date_Time),
                       new SqlParameter("Supplier_Transporter_Name", insertMaterialInwardRequest.Supplier_Transporter_Name),
                       new SqlParameter("Supplier_Transporter_Place", insertMaterialInwardRequest.Supplier_Transporter_Place),
                       new SqlParameter("Inv_DC_No", insertMaterialInwardRequest.Inv_DC_No),
                       new SqlParameter("Inv_DC_Date", insertMaterialInwardRequest.Inv_DC_Date),
                       new SqlParameter("Inv_Vehicle_No", insertMaterialInwardRequest.Inv_Vehicle_No),
                       new SqlParameter("Driver_Name", insertMaterialInwardRequest.Driver_Name),
                       new SqlParameter("Employee_No", insertMaterialInwardRequest.Employee_No),
                       new SqlParameter("Inward_Remarks", insertMaterialInwardRequest.Inward_Remarks),
                       new SqlParameter("Received_Material_Name", insertMaterialInwardRequest.Received_Material_Name),
                       new SqlParameter("Received_Quantity", insertMaterialInwardRequest.Received_Quantity),
                       new SqlParameter("Org_Office_No", insertMaterialInwardRequest.Org_Office_No)

                   ).FirstAsync();

            var r = await this._context.MaterialInwardEntity.FirstAsync(x => x.ID == id);

            var data = Mapper.Map<MaterialInwardDto>(r);

            return data;
        }

        public async Task<MaterialInwardDto> UpdateMaterialInward(int id, InsertMaterialInwardRequest materialInwardDto)
        {
            var r = await this._context.MaterialInwardEntity.FirstAsync(x => x.ID == id);

            r.Inv_DC_Date = materialInwardDto.Inv_DC_Date;
            r.Inv_DC_No = materialInwardDto.Inv_DC_No;
            r.Inv_Vehicle_No = materialInwardDto.Inv_Vehicle_No;
            r.Inward_Date_Time = materialInwardDto.Inward_Date_Time;
            r.Inward_Remarks = materialInwardDto.Inward_Remarks;
            r.Inward_Type = materialInwardDto.Inward_Type;
            r.Supplier_Transporter_Name = materialInwardDto.Supplier_Transporter_Name;
            r.Supplier_Transporter_Place = materialInwardDto.Supplier_Transporter_Place;
            r.Driver_Name = materialInwardDto.Driver_Name;
            r.Employee_No = materialInwardDto.Employee_No;
            r.Received_Material_Name = materialInwardDto.Received_Material_Name;
            r.Received_Quantity = materialInwardDto.Received_Quantity;
            r.Org_Office_No = materialInwardDto.Org_Office_No;

            _context.SaveChanges();

            var updated = await this._context.MaterialInwardEntity.FirstAsync(x => x.ID == id);

            var data = Mapper.Map<MaterialInwardDto>(updated);

            return data;
        }
    }
}