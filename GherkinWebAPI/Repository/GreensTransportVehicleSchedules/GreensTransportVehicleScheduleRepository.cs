using System;
using GherkinWebAPI.Models.GreensTransportVehicleSchedules;
using GherkinWebAPI.Core.GreensTransportVehicleSchedules;
using GherkinWebAPI.Persistence;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using GherkinWebAPI.Models.Employee;

namespace GherkinWebAPI.Repository.GreensTransportVehicleSchedules
{
    public class GreensTransportVehicleScheduleRepository : RepositoryBase<GreensTransportVehicleSchedule>, IGreensTransportVehicleScheduleRepository
    {
        private RepositoryContext _context;
        public GreensTransportVehicleScheduleRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            _context = repositoryContext;
        }

        public async Task<GreensTransportMaterialDetail> AddGreensTransportMaterialDetail(GreensTransportMaterialDetail greensTransportMaterialDetail)
        {
            try
            {
                var gtmDetail = new GreensTransportMaterialDetail
                {
                    ID = greensTransportMaterialDetail.ID,
                    GreensTransVehicleDespNo = greensTransportMaterialDetail.GreensTransVehicleDespNo,
                    RawMaterialDetailsCode = greensTransportMaterialDetail.RawMaterialDetailsCode,
                    RawMaterialGroupCode = greensTransportMaterialDetail.RawMaterialGroupCode,
                    DescDetails = greensTransportMaterialDetail.DescDetails,
                    DespQty = greensTransportMaterialDetail.DespQty
                };

                _context.GreensTransportMaterialDetails.Add(gtmDetail);

                var result = await _context.SaveChangesAsync();

                if (result == 1)
                {
                    return greensTransportMaterialDetail;
                }

                return new GreensTransportMaterialDetail();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<GreensTransportVehicleSchedule> AddGreensTransportVehicleSchedule(GreensTransportVehicleSchedule greensTransportVehicleSchedule)
        {
            try
            {
                if (greensTransportVehicleSchedule.HiredTransID != null && greensTransportVehicleSchedule.HiredTransID > 0)
                {
                    var transporter = await _context.Hired_Transporter_Details.Where(x => x.HiredTransID == greensTransportVehicleSchedule.HiredTransID).FirstOrDefaultAsync();
                    greensTransportVehicleSchedule.GCTransporterName = transporter.TransporterName;
                }


                _context.GreensTransportVehicleSchedules.Add(greensTransportVehicleSchedule);

                var result = await _context.SaveChangesAsync();

                if (result == 1)
                {
                    greensTransportVehicleSchedule.GreensTransVehicleDespNo = greensTransportVehicleSchedule.GreensTransVehicleDespNo;
                    return greensTransportVehicleSchedule;
                }
                return new GreensTransportVehicleSchedule();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ReturnableGatePassDetail> AddReturnableGatePassDetail(ReturnableGatePassDetail returnableGatePassDetail)
        {
            try
            {
                var returnableGPDetail = new ReturnableGatePassDetail
                {
                    RGPNo = returnableGatePassDetail.RGPNo,
                    RGPDate = returnableGatePassDetail.RGPDate
                };

                _context.ReturnableGatePassDetails.Add(returnableGatePassDetail);

                var result = await _context.SaveChangesAsync();

                if (result == 1)
                {
                    return returnableGatePassDetail;
                }
                return new ReturnableGatePassDetail();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<GreensTransportVechicleScheduleDetail> SearchGreensTransportVechicleScheduleDetail(string rgpNo)
        {
            try
            {
                var greensTransportVehicalScheduleDetail = (from a in _context.GreensTransportVehicleSchedules
                                                            join b in _context.GreensTransportMaterialDetails on a.GreensTransVehicleDespNo equals b.GreensTransVehicleDespNo
                                                            join c in _context.ReturnableGatePassDetails on a.RGPNo equals c.RGPNo
                                                            join d in _context.Areas on a.AreaId equals d.Area_ID
                                                            join e in _context.Employees on a.BuyerEmpId equals e.employeeId
                                                            join f in _context.OrganisationOfficeLocationDetails on a.OrgofficeNo equals f.Org_Office_No
                                                            join g in _context.RawMaterialGroupMaster on b.RawMaterialGroupCode equals g.Raw_Material_Group_Code
                                                            join h in _context.RawMaterialDetails on b.RawMaterialDetailsCode equals h.Raw_Material_Details_Code
                                                            join i in _context.Hired_Transporter_Details on a.HiredTransID equals i.HiredTransID into ia
                                                            from i in ia.DefaultIfEmpty()
                                                            join j in _context.OwnVehiclesDetails on a.OwnVehicleID equals j.OwnVehicleID into ja
                                                            from j in ja.DefaultIfEmpty()
                                                            join k in _context.Hired_Vehicle_Details on a.HiredVehicleID equals k.HiredVehicleID into ka
                                                            from k in ka.DefaultIfEmpty()
                                                            join l in _context.DriverDetails on a.DriverID equals l.DriverID into la
                                                            from l in la.DefaultIfEmpty()
                                                            where a.RGPNo == rgpNo
                                                            select new GreensTransportVechicleScheduleDetail
                                                            {
                                                                GreensTransVehicleDespNo = a.GreensTransVehicleDespNo,
                                                                DateofEntry = a.EntryDate,
                                                                OrgOfficeNo = a.OrgofficeNo,
                                                                OrgOfficeName = f.Org_Office_Name,
                                                                AreaId = a.AreaId,
                                                                AreaName = d.Area_Name,
                                                                BuyerEmpId = a.BuyerEmpId,
                                                                BuyerEmpName = e.employeeName,
                                                                RGPNo = a.RGPNo,
                                                                ReturnableGatePassDate = c.RGPDate,
                                                                TransporterName = a.GCTransporterName ?? i.TransporterName,
                                                                HiredTransID = a.HiredTransID,
                                                                OwnVehicleID = a.OwnVehicleID,
                                                                HiredVehicleID = a.HiredVehicleID,
                                                                DriverID = a.DriverID,
                                                                VehicleNo = j.VehicleRegNumber ?? k.VehicleRegNumber,
                                                                DriverName = a.GCDriverName ?? _context.Employees.FirstOrDefault(m => m.employeeId == l.EmployeeID).employeeName,
                                                                DriverContactNo = a.GCDriverContactNo,
                                                                StartKMSReading = a.VehicleReading,
                                                                TimeofDespatch = a.TimeofDespatch,
                                                                Remarks = a.RGPRemarks,
                                                                GreenTransportMaterials = a.GreensTransportMaterialDetails.Select(gtm => new GreenTransportMaterial
                                                                {
                                                                    ID = gtm.ID,
                                                                    MaterailGroupCode = gtm.RawMaterialGroupCode,
                                                                    MaterailGroup = g.Raw_Material_Group,
                                                                    MaterialNameCode = gtm.RawMaterialDetailsCode,
                                                                    MaterialName = h.Raw_Material_Details_Name,
                                                                    DescDetails = gtm.DescDetails,
                                                                    TotalNo = gtm.DespQty
                                                                }).ToList()
                                                            }).FirstOrDefaultAsync();


                return await greensTransportVehicalScheduleDetail;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ReturnableGatePassDetail>> GetRGPDetails()
        {
            return await _context.ReturnableGatePassDetails.ToListAsync();
        }

        public async Task<string> GetRGPNo()
        {
            var returnableGatePassDetails = await _context.ReturnableGatePassDetails.AsNoTracking().ToListAsync();
            if (returnableGatePassDetails.Count > 0)
            {
                var maxRGPNo = returnableGatePassDetails.OrderByDescending(c => c.ID).Take(1).FirstOrDefault().ID;
                return "RGP_" + Convert.ToString(maxRGPNo + 1);
            }
            return string.Concat("RGP_", "1");
        }

        public async Task<GreensTransportVehicleSchedule> UpdateGreensTransportVehicleSchedule(GreensTransportVehicleSchedule greensTransportVehicleSchedule)
        {
            try
            {
                var gtvDetail = await _context.GreensTransportVehicleSchedules.SingleAsync(x => x.GreensTransVehicleDespNo == greensTransportVehicleSchedule.GreensTransVehicleDespNo);

                gtvDetail.EntryDate = greensTransportVehicleSchedule.EntryDate;
                gtvDetail.OrgofficeNo = greensTransportVehicleSchedule.OrgofficeNo;
                gtvDetail.AreaId = greensTransportVehicleSchedule.AreaId;
                gtvDetail.BuyerEmpId = greensTransportVehicleSchedule.BuyerEmpId;
                gtvDetail.RGPNo = greensTransportVehicleSchedule.RGPNo;
                gtvDetail.GCTransporterName = greensTransportVehicleSchedule.GCTransporterName;
                gtvDetail.HiredTransID = greensTransportVehicleSchedule.HiredTransID;
                gtvDetail.OwnVehicleID = greensTransportVehicleSchedule.OwnVehicleID;
                gtvDetail.HiredVehicleID = greensTransportVehicleSchedule.HiredVehicleID;
                gtvDetail.DriverID = greensTransportVehicleSchedule.DriverID;
                gtvDetail.GCDriverName = greensTransportVehicleSchedule.GCDriverName;
                gtvDetail.GCDriverContactNo = greensTransportVehicleSchedule.GCDriverContactNo;
                gtvDetail.VehicleReading = greensTransportVehicleSchedule.VehicleReading;
                gtvDetail.TimeofDespatch = greensTransportVehicleSchedule.TimeofDespatch;
                gtvDetail.RGPRemarks = greensTransportVehicleSchedule.RGPRemarks;

                var result = await _context.SaveChangesAsync();

                if (result == 1)
                {
                    return greensTransportVehicleSchedule;
                }

                return new GreensTransportVehicleSchedule();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<GreensTransportMaterialDetail> UpdateGreensTransportMaterialDetail(GreensTransportMaterialDetail greensTransportMaterialDetail)
        {
            try
            {
                var gtmDetail = await _context.GreensTransportMaterialDetails.SingleAsync(x => x.ID == greensTransportMaterialDetail.ID
                                                                                         && x.GreensTransVehicleDespNo == greensTransportMaterialDetail.GreensTransVehicleDespNo);

                gtmDetail.RawMaterialGroupCode = greensTransportMaterialDetail.RawMaterialGroupCode;
                gtmDetail.RawMaterialDetailsCode = greensTransportMaterialDetail.RawMaterialDetailsCode;
                gtmDetail.DescDetails = greensTransportMaterialDetail.DescDetails;
                gtmDetail.DespQty = greensTransportMaterialDetail.DespQty;

                var result = await _context.SaveChangesAsync();

                if (result == 1)
                {
                    return greensTransportMaterialDetail;
                }

                return new GreensTransportMaterialDetail();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ReturnableGatePassDetail> UpdateReturnableGatePassDetail(ReturnableGatePassDetail returnableGatePassDetail)
        {
            try
            {
                var rgpDetail = await _context.ReturnableGatePassDetails.SingleAsync(x => x.RGPNo == returnableGatePassDetail.RGPNo);

                rgpDetail.RGPDate = returnableGatePassDetail.RGPDate;

                var result = await _context.SaveChangesAsync();

                if (result == 1)
                {
                    return returnableGatePassDetail;
                }

                return new ReturnableGatePassDetail();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Employee>> GetGreensTransportVehicleBuyingSupervisor(string areaId, DateTime dateOfEntry)
        {
            var buyingSupervisor = from a in _context.HarvestAreaBuyingStaffDetails
                                   join b in _context.Employees on a.EmployeeID equals b.employeeId
                                   where a.AreaID == areaId && a.BSEffectiveDate <= dateOfEntry
                                   orderby b.employeeName
                                   select b;
            return await buyingSupervisor.ToListAsync();
        }
    }
}