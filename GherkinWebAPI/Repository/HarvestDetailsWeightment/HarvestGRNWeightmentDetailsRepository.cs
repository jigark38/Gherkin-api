using GherkinWebAPI.Core;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Repository
{
    public class HarvestGRNWeightmentDetailsRepository : RepositoryBase<HarvestGRNInwardDetails>, IHarvestGRNWeightmentDetailsRepository
    {
        private RepositoryContext _context;
        public HarvestGRNWeightmentDetailsRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            this._context = repositoryContext;
        }

        public async Task<IEnumerable<InwardDetailsDTO>> GetInwardDetails(int orgId)
        {
            try
            {
                var _harvestGRN = await _context.HarvestGRNInwardDetails.Select(x => x.Inward_Gate_Pass_No).ToListAsync();
                var _getInwards = _context.MaterialInwardEntity.Where(inw => inw.Inward_Type.ToLower() == "greens" && inw.Org_Office_No == orgId);
                var _inwardDetails = await (from inwards in _getInwards
                                            where !_harvestGRN.Contains(inwards.Inward_Gate_Pass_No)
                                            select new InwardDetailsDTO
                                            {
                                                InwardDate = inwards.Inward_Date_Time,
                                                InwardGatePassNo = inwards.Inward_Gate_Pass_No,
                                                VeichleNo = inwards.Inv_Vehicle_No,
                                                SupplierTransporter = inwards.Supplier_Transporter_Name,
                                                InWardType = inwards.Inward_Type

                                            }).ToListAsync();
                return _inwardDetails.OrderBy(x => x.InwardDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<IEnumerable<GreensReceptionDetailsDTO>> GetGreenReceptionDetails(int orgId)
        {
            try
            {
                List<GreensReceptionDetailsDTO> receptionDetailsDTOs = new List<GreensReceptionDetailsDTO>();
                List<GreensReceptionDetailsDTO> _greenReceptionDetails = new List<GreensReceptionDetailsDTO>();
                var _getInwardHarvestGRNProcurementNo = await _context.HarvestGRNInwardDetails.Select(x => new { x.Harvest_GRN_No, x.Greens_Procurement_No }).ToListAsync();
                var _getInwardHarvestGRN = _getInwardHarvestGRNProcurementNo.Select(x => x.Harvest_GRN_No).ToList();
                var _getInwardHarvestProcurementNumber = _getInwardHarvestGRNProcurementNo.Select(x => x.Greens_Procurement_No).ToList();
                var _greenReceptionHarvestDetails = await (from _harvestGRN in _context.HarvestGRNs
                                                           join _harvestBuyerWeighing in _context.CWHarvestBuyerWeighingDetails on _harvestGRN.HarvestGRNNo equals _harvestBuyerWeighing.HarvestGRNNo
                                                           join _cropName in _context.Crops on _harvestBuyerWeighing.CropNameCode equals _cropName.CropCode //into crop
                                                           //from _cropName in crop.DefaultIfEmpty()
                                                           join _cropScheme in _context.CropSchemes on _harvestBuyerWeighing.CropSchemeCode equals _cropScheme.Code into cropScheme
                                                           from _cropScheme in cropScheme.DefaultIfEmpty()
                                                           join _area in _context.Areas on _harvestGRN.AreaID equals _area.Area_ID into area
                                                           from _area in area.DefaultIfEmpty()
                                                           where _harvestGRN.LoadingCompleted==1 && _harvestGRN.OrgOfficeNo==orgId
                                                           && !_getInwardHarvestGRN.Contains(_harvestGRN.HarvestGRNNo)
                                                           select new GreensReceptionDetailsDTO
                                                           {
                                                               OrgID = _harvestGRN.OrgOfficeNo,
                                                               HarvestGRNDate = _harvestGRN.HarvestGRNDate,
                                                               HarvestGRNNo = _harvestGRN.HarvestGRNNo,
                                                               GreenProcurementNo = null,
                                                               AreaId = _harvestGRN.AreaID,
                                                               Area = _area.Area_Name,
                                                               VehicleNo = _harvestGRN.VehicleNo,
                                                               CropGroupCode = _harvestBuyerWeighing.CropGroupCode,
                                                               CropCode = _cropName.CropCode,
                                                               CropName = _cropName.Name,
                                                               CropSchemeCode = _harvestBuyerWeighing.CropSchemeCode,
                                                               CropSchemeFrom = _cropScheme.From,
                                                               CropSchemeSign = _cropScheme.Sign,
                                                               CropCountmm = _cropScheme.Count,
                                                               NoofCrates = _harvestBuyerWeighing.NoofCrates,
                                                               FarmerWiseTotalQuantity = _harvestBuyerWeighing.CWCountwiseTotalQuantity,
                                                               HarvestGRNTotalDespCrates = _harvestGRN.HarvestGRNTotalDespCrates,
                                                               VehicleStartingReading = _harvestGRN.VehicleStartingReading.ToString(),
                                                               VeichleStartTime = _harvestGRN.VehicleStartTime,
                                                               HarvestGRNTotalQuantity = _harvestGRN.HarvestGRNTotalQuantity
                                                           }).Where(x => x.OrgID == orgId).ToListAsync();
                _greenReceptionDetails.AddRange(_greenReceptionHarvestDetails);
                var _greenReceptionProcurementDetails = await (from _harvestGRN in _context.GreensProcurements
                                                               join _harvestFarmer in _context.GreensFarmersDetails on _harvestGRN.GreensProcurementNo equals _harvestFarmer.GreensProcurementNo
                                                               join _cropName in _context.Crops on _harvestFarmer.CropNameCode equals _cropName.CropCode into crop
                                                               from _cropName in crop.DefaultIfEmpty()
                                                               join _cropScheme in _context.CropSchemes on _harvestFarmer.CropSchemeCode equals _cropScheme.Code into cropScheme
                                                                from _cropScheme in cropScheme.DefaultIfEmpty()
                                                               join _area in _context.Areas on _harvestGRN.AreaID equals _area.Area_ID into area
                                                               from _area in area.DefaultIfEmpty()
                                                               join _greenTransportVehicleSchedule in _context.GreensTransportVehicleSchedules on _harvestGRN.GreensTransVehicleDespNo equals _greenTransportVehicleSchedule.GreensTransVehicleDespNo into vehicles
                                                               from _greenTransportVehicleSchedule in vehicles.DefaultIfEmpty()
                                                               join _ownVehicles in _context.OwnVehiclesDetails on _greenTransportVehicleSchedule.OwnVehicleID equals _ownVehicles.OwnVehicleID into ownDt
                                                               from _ownVehicles in ownDt.DefaultIfEmpty()
                                                               join _hiredVehicles in _context.Hired_Vehicle_Details on _greenTransportVehicleSchedule.HiredVehicleID equals _hiredVehicles.HiredVehicleID into hiredDt
                                                               from _hiredVehicles in hiredDt.DefaultIfEmpty()
                                                               where _harvestGRN.ReceivingCompleted == 1 && _harvestGRN.OrgofficeNo==orgId
                                                               && !_getInwardHarvestProcurementNumber.Contains(_harvestGRN.GreensProcurementNo)
                                                               select new GreensReceptionDetailsDTO
                                                               {
                                                                   OrgID = _harvestGRN.OrgofficeNo,
                                                                   HarvestGRNDate = _harvestGRN.HarvestDate,
                                                                   HarvestGRNNo = 0,
                                                                   GreenProcurementNo = _harvestGRN.GreensProcurementNo,
                                                                   AreaId = _harvestGRN.AreaID,
                                                                   Area = _area.Area_Name,
                                                                   VehicleNo = _greenTransportVehicleSchedule.OwnVehicleID != null ? _ownVehicles.VehicleRegNumber : _hiredVehicles.VehicleRegNumber,
                                                                   CropGroupCode = _harvestFarmer.CropGroupCode,
                                                                   CropCode = _cropName.CropCode,
                                                                   CropName = _cropName.Name,
                                                                   CropSchemeCode = _harvestFarmer.CropSchemeCode,
                                                                   CropSchemeFrom = _cropScheme.From,
                                                                   CropSchemeSign = _cropScheme.Sign,
                                                                   NoofCrates = _harvestFarmer.CountwiseTotalCrates,
                                                                   FarmerWiseTotalQuantity = _harvestFarmer.CountwiseTotalQuantity,
                                                                   HarvestGRNTotalDespCrates = _harvestGRN.TripTotalCrates,
                                                                   VehicleStartingReading = _greenTransportVehicleSchedule.VehicleReading,
                                                                   VeichleGreenProcurementStartTime = _greenTransportVehicleSchedule.TimeofDespatch,
                                                                   HarvestGRNTotalQuantity = _harvestGRN.TripTotalQuantity
                                                               }).Where(x => x.OrgID == orgId).ToListAsync();
                _greenReceptionDetails.AddRange(_greenReceptionProcurementDetails);
                var distinctCropSchemeCodes = _greenReceptionDetails.GroupBy(x => new { x.CropSchemeCode }).Select(x => x.FirstOrDefault()).OrderByDescending(x=>x.CropSchemeFrom).ThenByDescending(x=>x.CropSchemeSign);

                foreach (var item in _greenReceptionDetails)
                {
                    if (item.HarvestGRNNo != 0)
                    {
                        if (receptionDetailsDTOs.Any(x => x.HarvestGRNNo == item.HarvestGRNNo))
                        {
                            var receptionDetail = receptionDetailsDTOs.Where(x => x.HarvestGRNNo == item.HarvestGRNNo).FirstOrDefault();
                            var grades = receptionDetail.Grades;
                            var grade = grades.Where(x => x.CropSchemeCode == item.CropSchemeCode).FirstOrDefault();
                            grade.NoofCrates += item.NoofCrates;
                            grade.FarmerWiseTotalQuantity += item.FarmerWiseTotalQuantity;
                            grade.CropGradeData = grade.FarmerWiseTotalQuantity + " / " + grade.NoofCrates;
                        }
                        else
                        {
                            List<GradeDTO> _grades = new List<GradeDTO>();
                            foreach (var schemeInfo in distinctCropSchemeCodes)
                            {
                                _grades.Add(new GradeDTO()
                                {
                                    NoofCrates = 0,
                                    FarmerWiseTotalQuantity = 0,
                                    CropGradeData = null,
                                    CropSchemeCode = schemeInfo.CropSchemeCode,
                                    CropSchemeFromSign = schemeInfo.CropSchemeFrom + schemeInfo.CropSchemeSign + " / "+ schemeInfo.CropCountmm
                                });
                            }
                            item.Grades = _grades;
                            GreensReceptionDetailsDTO greensReceptionDetailsDTO = item;
                            var grade = item.Grades.Where(x => x.CropSchemeCode == item.CropSchemeCode).FirstOrDefault();
                            grade.NoofCrates += item.NoofCrates;
                            grade.FarmerWiseTotalQuantity += item.FarmerWiseTotalQuantity;
                            grade.CropGradeData = grade.FarmerWiseTotalQuantity + " / " + grade.NoofCrates;
                            receptionDetailsDTOs.Add(greensReceptionDetailsDTO);
                        }
                    }
                    else if(item.GreenProcurementNo!=null)
                    {
                        if (receptionDetailsDTOs.Any(x => x.GreenProcurementNo == item.GreenProcurementNo))
                        {
                            var receptionDetail = receptionDetailsDTOs.Where(x => x.GreenProcurementNo == item.GreenProcurementNo).FirstOrDefault();
                            var grades = receptionDetail.Grades;
                            var grade = grades.Where(x => x.CropSchemeCode == item.CropSchemeCode).FirstOrDefault();
                            grade.NoofCrates += item.NoofCrates;
                            grade.FarmerWiseTotalQuantity += item.FarmerWiseTotalQuantity;
                            grade.CropGradeData = grade.FarmerWiseTotalQuantity + " / " + grade.NoofCrates;
                        }
                        else
                        {
                            List<GradeDTO> _grades = new List<GradeDTO>();
                            foreach (var schemeInfo in distinctCropSchemeCodes)
                            {
                                _grades.Add(new GradeDTO()
                                {
                                    NoofCrates = 0,
                                    FarmerWiseTotalQuantity = 0,
                                    CropGradeData = null,
                                    CropSchemeCode = schemeInfo.CropSchemeCode,
                                    CropSchemeFromSign = schemeInfo.CropSchemeFrom + schemeInfo.CropSchemeSign + " / " + schemeInfo.CropCountmm
                                });
                            }
                            item.Grades = _grades;
                            GreensReceptionDetailsDTO greensReceptionDetailsDTO = item;
                            var grade = item.Grades.Where(x => x.CropSchemeCode == item.CropSchemeCode).FirstOrDefault();
                            grade.NoofCrates += item.NoofCrates;
                            grade.FarmerWiseTotalQuantity += item.FarmerWiseTotalQuantity;
                            grade.CropGradeData = grade.FarmerWiseTotalQuantity + " / " + grade.NoofCrates;
                            receptionDetailsDTOs.Add(greensReceptionDetailsDTO);
                        }
                    }
                }
                /*foreach (var item in _greenReceptionDetails)
                {
                    List<GradeDTO> _grades = new List<GradeDTO>();
                    var _CropsInfo = from _cropScheme in _context.CropSchemes
                                     join _harvestFarmer in _context.HarvestGRNFarmers on _cropScheme.Code equals _harvestFarmer.CropSchemeCode
                                     where _harvestFarmer.HarvestGRNNo == item.HarvestGRNNo
                                     group _harvestFarmer by new { _harvestFarmer.CropSchemeCode, _harvestFarmer.NoofCrates, _harvestFarmer.FarmerWiseTotalQuantity, _harvestFarmer.HarvestGRNNo }
                               into _info
                                     select new
                                     {
                                         CropSchemeCode = _info.Key.CropSchemeCode,
                                         TotNoOfCrates = _info.Sum(x => x.NoofCrates),
                                         SumOfFarmerWiseTotalQuantity = _info.Sum(i => i.FarmerWiseTotalQuantity),
                                         GRNNo = _info.Key.HarvestGRNNo
                                     };
                    var _iList = await (from a in _context.CropSchemes
                                        join b in _CropsInfo on a.Code equals b.CropSchemeCode
                                        into i
                                        from j in i.DefaultIfEmpty()
                                        select new
                                        {
                                            cropSchemeCode = a.Code,
                                            grnNo = (j.GRNNo == null) ? 0 : j.GRNNo,
                                            header = a.Code.ToString() + " " + a.Sign.ToString(),
                                            details = (j.SumOfFarmerWiseTotalQuantity == null) ? null : j.SumOfFarmerWiseTotalQuantity.ToString() + "/" + j.TotNoOfCrates.ToString()

                                        }).ToListAsync();
                    foreach (var getGrade in _iList)
                    {
                        GradeDTO gradeDTO = new GradeDTO();
                        if (item.HarvestGRNNo == getGrade.grnNo)
                        {
                            gradeDTO.CropGradeData = getGrade.details;
                        }
                        else
                        {
                            gradeDTO.CropGradeData = null;
                        }
                        gradeDTO.CropSchemeFromSign = getGrade.header;
                        gradeDTO.CropSchemeCode = getGrade.cropSchemeCode;

                        _grades.Add(gradeDTO);
                    }
                    item.Grades = _grades;
                }*/
                return receptionDetailsDTOs.OrderBy(o => o.HarvestGRNDate).ThenBy(o2 => o2.HarvestGRNNo);
                //return _greenReceptionDetails.OrderBy(o => o.HarvestGRNDate).ThenBy(o2 => o2.HarvestGRNNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<HarvestGRNWeighmentDetailsDTO> AddHarvestGRNDetails(HarvestGRNWeighmentDetailsDTO materialDetails)
        {
            var _details = materialDetails;

            HarvestGRNInwardDetails _inwardDetails;
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (_details == null)
                        return null;
                    else
                    {
                        _inwardDetails = new HarvestGRNInwardDetails
                        {
                            Harvest_GRN_No = _details.HarvestGRNNo,
                            Greens_Procurement_No=_details.Greens_Procurement_No,
                            Inward_Gate_Pass_No = _details.InwardGatePassNo,
                            Org_office_No = _details.OfficeOrgID,
                            Area_ID = _details.AreaId,
                            Employee_ID = _details.SupervisorId,
                            Harvest_GRN_Total_Desp_Crates = _details.HarvestGRNTotalDespCrates,
                            Harvest_GRN_Total_Quantity = _details.HarvestGRNTotalQuantity,
                            Unit_Harvest_Material_Inward_Date = _details.UnitHarvestMaterialInwardDate,
                            Vehicle_Reach_Reading = _details.VehicleReachReading,
                            Vehicle_Reach_time = _details.VehicleReachTime,
                            Vehicle_Transit_Duration = _details.VehicleTransitDuration,
                            Vehicle_Transit_Kms = _details.VehicleTransitKms,
                            Total_Received_Crates = _details.TotalReceivedCrates,
                            Total_Received_Qty = _details.TotalReceivedQty
                        };

                        _context.HarvestGRNInwardDetails.Add(_inwardDetails);
                        await _context.SaveChangesAsync();
                        var _inwardId = _inwardDetails.Unit_HM_Inward_No;
                        if (_details.SummaryReceivingDetails.Count() > 0)
                        {
                            foreach (var _material in _details.SummaryReceivingDetails)
                            {
                                HarvestGRNInwardMaterialDetails _materialDetails = new HarvestGRNInwardMaterialDetails
                                {
                                    Harvest_GRN_No = _inwardDetails.Harvest_GRN_No,
                                    Greens_Procurement_No=_inwardDetails.Greens_Procurement_No,
                                    Unit_HM_Inward_No = _inwardDetails.Unit_HM_Inward_No,
                                    Crop_Group_Code = _details.CropGroupCode,
                                    Crop_Name_Code = _details.CropNameCode,
                                    Crop_Scheme_Code = _material.CropSchemeCode,
                                    No_of_Crates = _material.NoofCrates,
                                    Grade_wise_Total_Quantity = _material.GradeWiseTotalQuantity
                                };
                                _context.HarvestGRNInwardMaterialDetails.Add(_materialDetails);
                            }
                            await _context.SaveChangesAsync();
                        }
                        if (_details.SummaryWeighmentDetails.Count() > 0)
                        {
                            foreach (var _weighmentDetails in _details.SummaryWeighmentDetails)
                            {
                                HarvestGRNIMWeightDetails _grnIMWeightDetails = new HarvestGRNIMWeightDetails
                                {
                                    Unit_HM_Inward_No = _inwardDetails.Unit_HM_Inward_No,
                                    Crop_Name_Code = _details.CropNameCode,
                                    Crop_Scheme_Code = _weighmentDetails.CropSchemeCode,
                                    Crop_Group_Code = _details.CropGroupCode,
                                    HM_Weight_No_of_Crates = _weighmentDetails.HMWeightNoofCrates,
                                    HM_Weight_Gross_Weight = _weighmentDetails.HMWeightGrossWeight,
                                    HM_Weight_Tare_Weight = _weighmentDetails.HMWeightTareWeight,
                                    HM_Weight_Net_Weight = _weighmentDetails.HMWeightNetWeight,
                                    HM_Weight_Crates_Tare_Weight = _weighmentDetails.HMCratesTareWeight
                                };
                                _context.HarvestGRNIMWeightDetails.Add(_grnIMWeightDetails);
                            }
                            await _context.SaveChangesAsync();
                        }
                    }
                    transaction.Commit();
                    return _details;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
    }
}