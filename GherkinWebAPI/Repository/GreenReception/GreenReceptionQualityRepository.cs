using GherkinWebAPI.Core.GreenReption;
using GherkinWebAPI.Models.GreenReception;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Repository.GreenReception
{
    public class GreenReceptionQualityRepository : IGreenReceptionQualityRepository
    {
        private RepositoryContext _context;
        public GreenReceptionQualityRepository(RepositoryContext repositoryContext)
        {
            _context = repositoryContext;
        }
        public async Task<List<OrganisationOfficeLocUnit>> GetAllUnit()
        {
            return await _context.OrganisationOfficeLocationDetails.OrderBy(e => e.Org_Office_Name)
                         .Select(x => new OrganisationOfficeLocUnit { orgCode = x.Org_Code, orgOfficeName = x.Org_Office_Name, orgOfficeNo = x.Org_Office_No })
                         .OrderBy(o => o.orgOfficeName)
                         .ToListAsync();
        }

        public async Task<List<GreenInwardsDetail>> GetInwardDetailsByOrgOfficeNo(int orgOfficeNo)
        {
            var materialInwardGatePass = _context.MaterialInwardEntity.Where(e => !_context.greenReceptionQualityChecks.Any(a => a.inwardGatePassNo == e.Inward_Gate_Pass_No));

            return await (from migp in materialInwardGatePass
                          join hgid in _context.HarvestGRNInwardDetails on migp.Inward_Gate_Pass_No equals hgid.Inward_Gate_Pass_No
                          where migp.Org_Office_No == orgOfficeNo && migp.Inward_Type.ToUpper() == "GREENS"
                          select new GreenInwardsDetail()
                          {
                              employeeNo = migp.Employee_No,
                              areaId = hgid.Area_ID,
                              harvestGRNNo = hgid.Harvest_GRN_No,
                              invoiceDCDate = migp.Inv_DC_Date,
                              invoiceDCNo = migp.Inv_DC_No,
                              invVehicleNo = migp.Inv_Vehicle_No,
                              inwardDateTime = migp.Inward_Date_Time,
                              inwardGatePassNo = migp.Inward_Gate_Pass_No,
                              inwardType = migp.Inward_Type,
                              supplierTransporterName = migp.Supplier_Transporter_Name,
                              supplierTransporterPlace = migp.Supplier_Transporter_Place
                          }).OrderBy(e => e.inwardDateTime).ThenBy(e => e.inwardGatePassNo).ToListAsync();

        }


        public async Task<List<GreensReceptionDetail>> GetGreenReceptionByOrgOfficeNo(int orgOfficeNo)
        {
            //var harvestGrnsDetails = _context.HarvestGRNs.Where(e => !_context.greenReceptionQualityChecks.Any(a => a.harvestGRNNo == e.HarvestGRNNo));
            var harvestGrnsDetails = _context.HarvestGRNs;
            var greenReceptionQualityChecks = await _context.greenReceptionQualityChecks.ToListAsync();
            var result = await (from hgd in harvestGrnsDetails
                              //join hgf in _context.HarvestGRNFarmers on hgd.HarvestGRNNo equals hgf.HarvestGRNNo
                          where hgd.OrgOfficeNo == orgOfficeNo
                          select new GreensReceptionDetail()
                          {
                              areaId = hgd.AreaID,
                              //cropNameCode = hgf.CropNameCode,
                              //farmerWiseTotalQty = hgd.FarmerWiseTotalQuantity,
                              harvestGrnDate = hgd.HarvestGRNDate,
                              harvestGRNNo = hgd.HarvestGRNNo,
                              harvestGRNTotalQty = hgd.HarvestGRNTotalQuantity,
                              vehicalNo = hgd.VehicleNo
                          }).OrderBy(e => e.harvestGrnDate).ThenBy(e => e.harvestGRNNo).ToListAsync();

            //Update Crop Name Code
            if (result !=null && result.Any())
            {
                var harvestGRNFarmers = await _context.HarvestGRNFarmers.ToListAsync();
                foreach (var item in result)
                {
                    var crop = harvestGRNFarmers.FirstOrDefault(_ => _.HarvestGRNNo == item.harvestGRNNo);
                    if (crop !=null)
                    {
                        item.cropNameCode = crop.CropNameCode;
                    }
                }
            }

            if (greenReceptionQualityChecks.Any())
            {
                foreach (var item in greenReceptionQualityChecks)
                {
                    var removeItem = result.FirstOrDefault(_ => _.harvestGRNNo == item.harvestGRNNo);
                    if (removeItem != null)
                    {
                        result.Remove(removeItem);
                    }
                }
            }

            return result;
        }

        public async Task<CreateQualityCheckAndInspection> CreateQualityCheckAndInspection(CreateQualityCheckAndInspection createQCAndInspection)
        {
            CreateQualityCheckAndInspection cQCInspection = new CreateQualityCheckAndInspection();
            cQCInspection.greenReceptionQualityCheck = new GreenReceptionQualityCheck();
            cQCInspection.greenReceptionQualityDetails = new GreenReceptionQualityDetails();
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (createQCAndInspection == null)
                        return null;

                    if (createQCAndInspection.greenReceptionQualityCheck != null)
                    {
                        if (createQCAndInspection.greenReceptionQualityCheck.orgofficeNo > 0
                            && createQCAndInspection.greenReceptionQualityCheck.inwardGatePassNo != null && createQCAndInspection.greenReceptionQualityCheck.areaId != null &&
                            createQCAndInspection.greenReceptionQualityCheck.harvestGRNNo > 0)
                        {
                            cQCInspection.greenReceptionQualityCheck = createQCAndInspection.greenReceptionQualityCheck;
                            if (cQCInspection.greenReceptionQualityCheck.greenQualityCheckNo > 0)
                            {
                                var greenReceptionQualityCheck = await _context.greenReceptionQualityChecks.Where(x => x.greenQualityCheckNo == cQCInspection.greenReceptionQualityCheck.greenQualityCheckNo).FirstOrDefaultAsync();
                                if (greenReceptionQualityCheck != null)
                                {
                                    greenReceptionQualityCheck.areaId = cQCInspection.greenReceptionQualityCheck.areaId;
                                    greenReceptionQualityCheck.greensCheckedEmployeeId = cQCInspection.greenReceptionQualityCheck.greensCheckedEmployeeId;
                                    greenReceptionQualityCheck.greensRecvAGNo = cQCInspection.greenReceptionQualityCheck.greensRecvAGNo;
                                    greenReceptionQualityCheck.greensRecvRemarks = cQCInspection.greenReceptionQualityCheck.greensRecvRemarks;
                                    greenReceptionQualityCheck.greensRecvSampleDate = cQCInspection.greenReceptionQualityCheck.greensRecvSampleDate;
                                    greenReceptionQualityCheck.greensRecvSampleQty = cQCInspection.greenReceptionQualityCheck.greensRecvSampleQty;
                                    greenReceptionQualityCheck.greensRecvTrunkCondition = cQCInspection.greenReceptionQualityCheck.greensRecvTrunkCondition;
                                    greenReceptionQualityCheck.greensVerifiedEmployeeId = cQCInspection.greenReceptionQualityCheck.greensVerifiedEmployeeId;
                                    greenReceptionQualityCheck.orgofficeNo= cQCInspection.greenReceptionQualityCheck.orgofficeNo;
                                }
                                else
                                {
                                    _context.greenReceptionQualityChecks.Add(cQCInspection.greenReceptionQualityCheck);
                                }
                            }
                            else
                            {
                                _context.greenReceptionQualityChecks.Add(cQCInspection.greenReceptionQualityCheck);
                            }
                            await _context.SaveChangesAsync();
                        }
                    }

                    if (createQCAndInspection.greenReceptionQualityDetails != null)
                    {
                        cQCInspection.greenReceptionQualityDetails = createQCAndInspection.greenReceptionQualityDetails;
                        if (cQCInspection.greenReceptionQualityDetails.greensQCDetailsNo > 0)
                        {
                            var greenReceptionQualityDetails = await _context.GreenReceptionQualityDetails.Where(x => x.greensQCDetailsNo == cQCInspection.greenReceptionQualityDetails.greensQCDetailsNo).FirstOrDefaultAsync();
                            if (greenReceptionQualityDetails != null)
                            {
                                greenReceptionQualityDetails.borrerQCQty = cQCInspection.greenReceptionQualityDetails.borrerQCQty;
                                greenReceptionQualityDetails.borrerQCUOM = cQCInspection.greenReceptionQualityDetails.borrerQCUOM;
                                greenReceptionQualityDetails.calQCQty = cQCInspection.greenReceptionQualityDetails.calQCQty;
                                greenReceptionQualityDetails.calQCUOM = cQCInspection.greenReceptionQualityDetails.calQCUOM;
                                greenReceptionQualityDetails.endcorpQCQty = cQCInspection.greenReceptionQualityDetails.endcorpQCQty;
                                greenReceptionQualityDetails.endcorpQCUOM = cQCInspection.greenReceptionQualityDetails.endcorpQCUOM;
                                greenReceptionQualityDetails.ffQCQty = cQCInspection.greenReceptionQualityDetails.ffQCQty;
                                greenReceptionQualityDetails.ffQCUOM = cQCInspection.greenReceptionQualityDetails.ffQCUOM;
                                greenReceptionQualityDetails.flowersQCQty = cQCInspection.greenReceptionQualityDetails.flowersQCQty;
                                greenReceptionQualityDetails.flowersQCUOM = cQCInspection.greenReceptionQualityDetails.flowersQCUOM;
                                greenReceptionQualityDetails.fungusQCQty = cQCInspection.greenReceptionQualityDetails.fungusQCQty;
                                greenReceptionQualityDetails.fungusQCUOM = cQCInspection.greenReceptionQualityDetails.fungusQCUOM;
                                greenReceptionQualityDetails.muddyQCQty = cQCInspection.greenReceptionQualityDetails.muddyQCQty;
                                greenReceptionQualityDetails.muddyQCUOM = cQCInspection.greenReceptionQualityDetails.muddyQCUOM;
                                greenReceptionQualityDetails.peanutQCQty = cQCInspection.greenReceptionQualityDetails.peanutQCQty;
                                greenReceptionQualityDetails.peanutQCUOM = cQCInspection.greenReceptionQualityDetails.peanutQCUOM;
                                greenReceptionQualityDetails.rottenQCQty = cQCInspection.greenReceptionQualityDetails.rottenQCQty;
                                greenReceptionQualityDetails.rottenQCUOM = cQCInspection.greenReceptionQualityDetails.rottenQCUOM;
                                greenReceptionQualityDetails.softQCQty = cQCInspection.greenReceptionQualityDetails.softQCQty;
                                greenReceptionQualityDetails.softQCUOM = cQCInspection.greenReceptionQualityDetails.softQCUOM;
                                greenReceptionQualityDetails.stemsQCQty = cQCInspection.greenReceptionQualityDetails.stemsQCQty;
                                greenReceptionQualityDetails.stemsQCUOM = cQCInspection.greenReceptionQualityDetails.stemsQCUOM;
                                greenReceptionQualityDetails.virusQCQty = cQCInspection.greenReceptionQualityDetails.virusQCQty;
                                greenReceptionQualityDetails.virusQCUOM = cQCInspection.greenReceptionQualityDetails.virusQCUOM;
                            }
                            else
                            {
                                cQCInspection.greenReceptionQualityDetails.greensQualityCheckNo = cQCInspection.greenReceptionQualityCheck.greenQualityCheckNo;
                                _context.GreenReceptionQualityDetails.Add(cQCInspection.greenReceptionQualityDetails);
                            }
                        }
                        else
                        {
                            cQCInspection.greenReceptionQualityDetails.greensQualityCheckNo = cQCInspection.greenReceptionQualityCheck.greenQualityCheckNo;
                            _context.GreenReceptionQualityDetails.Add(cQCInspection.greenReceptionQualityDetails);
                        }
                        await _context.SaveChangesAsync();
                    }

                    transaction.Commit();
                    return cQCInspection;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public async Task<CreateQualityCheckAndInspection> GetQualityCheckAndInspection(long harvestGRNNo)
        {
            GreenReceptionQualityCheck greenReceptionQualityCheck = await _context.greenReceptionQualityChecks.Where(x => x.harvestGRNNo == harvestGRNNo).FirstOrDefaultAsync();
            GreenReceptionQualityDetails greenReceptionQualityDetails = null;
            if (greenReceptionQualityCheck != null)
            {
                greenReceptionQualityDetails = await _context.GreenReceptionQualityDetails.Where(x => x.greensQualityCheckNo == greenReceptionQualityCheck.greenQualityCheckNo).FirstOrDefaultAsync();
            }
            CreateQualityCheckAndInspection createQualityCheckAndInspection = new CreateQualityCheckAndInspection()
            {
                greenReceptionQualityCheck = greenReceptionQualityCheck,
                greenReceptionQualityDetails = greenReceptionQualityDetails
            };
            return createQualityCheckAndInspection;
        }
    }
}