using GherkinWebAPI.Core.FeedInputTransfer;
using GherkinWebAPI.DTO;
using GherkinWebAPI.DTO.FeedInputTransfer;
using GherkinWebAPI.Models.InputTransferDetails;
using GherkinWebAPI.Models.PurchageMgmt;
using GherkinWebAPI.Persistence;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using Unity.Injection;

namespace GherkinWebAPI.Repository.FeedInputTransfer
{
    public class FeedInputTransferRepository : RepositoryBase<HBOMDetailsDto>, IFeedInputTransferRepository
    {
        private RepositoryContext _context;

        public FeedInputTransferRepository(RepositoryContext repositoryContext)
               : base(repositoryContext)
        {
            this._context = repositoryContext;
        }
        public async Task<string> GenerateTransferNoPK()
        {

            int? MaxPOId = await _context.InputTransferDetails.MaxAsync(e => (int?)e.Id);
            if (MaxPOId != null)
            {
                int newId = (int)MaxPOId + 1;
                return "MTTB_" + newId;
            }
            else  // FIRST ENTRY
                return "MTTB_1";
        }

        public async Task<string> GetOutwardGatePassNo()
        {

            int? MAXiD = await _context.OutwardGatePassDetails.MaxAsync(e => (int?)e.Id);
            if (MAXiD != null)
            {
                int newId = (int)MAXiD + 1;
                return "OGP_" + newId;
            }
            else
            { // FIRST ENTRY
                return "OGP_1";

            }
        }

        public async Task<List<HBOMDetailsDto>> GetTranferDetails(string cropNameCode, string cropSchemeCode, string psNumber, string areaId)
        {
            List<HBOMDetailsDto> lstHbom = new List<HBOMDetailsDto>();
            HBOMDetailsDto hbom = new HBOMDetailsDto();
            hbom.HBOMDetails = new List<HBOMDetails>();
            try
            {
                if (areaId.Trim().Length > 1)
                {

                    hbom.HBOMDetails = await (from ppd in _context.packagePracticeDivisions
                                              join ppm in _context.packagePracticeMaterials on ppd.CropNameCode equals ppm.CropNameCode
                                              join rmd in _context.RawMaterialDetails on ppm.Raw_Material_Details_Code equals rmd.Raw_Material_Details_Code
                                              join rmgm in _context.RawMaterialGroupMaster on ppm.RawmaterialGroupcode equals rmgm.Raw_Material_Group_Code
                                              join cs in _context.CropSchemes on ppd.CropNameCode equals cs.CropCode
                                              join itd in _context.InputTransferDetails on cs.Code equals itd.CropSchemeCode
                                              join rimtd in _context.RMInputMaterialTransferDetails on itd.TransferNumber equals rimtd.rmTransferNo
                                              where ppd.CropNameCode == cropNameCode && cs.Code == cropSchemeCode
                                              && ppd.PSNO == psNumber && itd.AreaId == areaId //&& ppm.PracticeNo == hbomPracticeNum

                                              select new HBOMDetails()
                                              {
                                                  hbOMDivisonFor = ppd.DivisionFor,
                                                  hbomPracticePerAcrage = ppd.PracticePerAcre,
                                                  hSTransactionCode = ppd.TransCode,
                                                  hSCropPhaseCode = ppd.CropphaseCode,
                                                  hbomPracticeEffectiveDate = ppd.PracticeEffectiveDate,
                                                  hbomPracticeNo = ppd.PracticeNo,
                                                  rawMaterialGroupCode = ppm.RawmaterialGroupcode,
                                                  rawMaterialDetailsCode = ppm.Raw_Material_Details_Code,
                                                  hbomChemicalUOM = ppm.ChemicalUOM,
                                                  hbomChemicalVolume = ppm.Chemicalvolume,
                                                  rawMaterialGroup = rmgm.Raw_Material_Group,
                                                  RawMaterialDetailsName = rmd.Raw_Material_Details_Name,
                                                  transferredAmount = rimtd.rmMaterialTransferAmount

                                              }).GroupBy(l => l.hbOMDivisonFor)
                                                    .Select(c => new HBOMDetails
                                                    {

                                                        hbOMDivisonFor = c.Select(x => x.hbOMDivisonFor).FirstOrDefault(),
                                                        hbomPracticePerAcrage = c.Select(x => x.hbomPracticePerAcrage).FirstOrDefault(),
                                                        hSTransactionCode = c.Select(x => x.hSTransactionCode).FirstOrDefault(),
                                                        hSCropPhaseCode = c.Select(x => x.hSCropPhaseCode).FirstOrDefault(),
                                                        hbomPracticeEffectiveDate = c.Select(x => x.hbomPracticeEffectiveDate).FirstOrDefault(),
                                                        hbomPracticeNo = c.Select(x => x.hbomPracticeNo).FirstOrDefault(),
                                                        rawMaterialGroupCode = c.Select(x => x.rawMaterialGroupCode).FirstOrDefault(),
                                                        rawMaterialDetailsCode = c.Select(x => x.rawMaterialDetailsCode).FirstOrDefault(),
                                                        hbomChemicalUOM = c.Select(x => x.hbomChemicalUOM).FirstOrDefault(),
                                                        hbomChemicalVolume = c.Select(x => x.hbomChemicalVolume).FirstOrDefault(),
                                                        rawMaterialGroup = c.Select(x => x.rawMaterialGroup).FirstOrDefault(),
                                                        RawMaterialDetailsName = c.Select(x => x.RawMaterialDetailsName).FirstOrDefault(),
                                                        transferredAmount = c.Select(x => x.transferredAmount).FirstOrDefault(),
                                                    }).OrderBy(e => e.rawMaterialGroupCode).ToListAsync();
                }
                else
                {
                    hbom.HBOMDetails = await (from ppd in _context.packagePracticeDivisions
                                              join ppm in _context.packagePracticeMaterials on ppd.CropNameCode equals ppm.CropNameCode
                                              join rmd in _context.RawMaterialDetails on ppm.Raw_Material_Details_Code equals rmd.Raw_Material_Details_Code
                                              join rmgm in _context.RawMaterialGroupMaster on ppm.RawmaterialGroupcode equals rmgm.Raw_Material_Group_Code
                                              join cs in _context.CropSchemes on ppd.CropNameCode equals cs.CropCode
                                              join rimtd in _context.RMInputMaterialTransferDetails on rmgm.Raw_Material_Group_Code equals rimtd.rawMaterialGroupCode
                                              where ppd.CropNameCode == cropNameCode && cs.Code == cropSchemeCode
                                              && ppd.PSNO == psNumber //&& itd.AreaId == areaId //&& ppm.PracticeNo == hbomPracticeNum

                                              select new HBOMDetails()
                                              {
                                                  hbOMDivisonFor = ppd.DivisionFor,
                                                  hbomPracticePerAcrage = ppd.PracticePerAcre,
                                                  hSTransactionCode = ppd.TransCode,
                                                  hSCropPhaseCode = ppd.CropphaseCode,
                                                  hbomPracticeEffectiveDate = ppd.PracticeEffectiveDate,
                                                  hbomPracticeNo = ppd.PracticeNo,
                                                  rawMaterialGroupCode = ppm.RawmaterialGroupcode,
                                                  rawMaterialDetailsCode = ppm.Raw_Material_Details_Code,
                                                  hbomChemicalUOM = ppm.ChemicalUOM,
                                                  hbomChemicalVolume = ppm.Chemicalvolume,
                                                  rawMaterialGroup = rmgm.Raw_Material_Group,
                                                  RawMaterialDetailsName = rmd.Raw_Material_Details_Name,
                                                  transferredAmount = rimtd.rmMaterialTransferAmount

                                              }).GroupBy(l => l.hbOMDivisonFor)
                                .Select(c => new HBOMDetails
                                {

                                    hbOMDivisonFor = c.Select(x => x.hbOMDivisonFor).FirstOrDefault(),
                                    hbomPracticePerAcrage = c.Select(x => x.hbomPracticePerAcrage).FirstOrDefault(),
                                    hSTransactionCode = c.Select(x => x.hSTransactionCode).FirstOrDefault(),
                                    hSCropPhaseCode = c.Select(x => x.hSCropPhaseCode).FirstOrDefault(),
                                    hbomPracticeEffectiveDate = c.Select(x => x.hbomPracticeEffectiveDate).FirstOrDefault(),
                                    hbomPracticeNo = c.Select(x => x.hbomPracticeNo).FirstOrDefault(),
                                    rawMaterialGroupCode = c.Select(x => x.rawMaterialGroupCode).FirstOrDefault(),
                                    rawMaterialDetailsCode = c.Select(x => x.rawMaterialDetailsCode).FirstOrDefault(),
                                    hbomChemicalUOM = c.Select(x => x.hbomChemicalUOM).FirstOrDefault(),
                                    hbomChemicalVolume = c.Select(x => x.hbomChemicalVolume).FirstOrDefault(),
                                    rawMaterialGroup = c.Select(x => x.rawMaterialGroup).FirstOrDefault(),
                                    RawMaterialDetailsName = c.Select(x => x.RawMaterialDetailsName).FirstOrDefault(),
                                    transferredAmount = c.Select(x => x.transferredAmount).FirstOrDefault(),
                                }).OrderBy(e => e.rawMaterialGroupCode).ToListAsync();
                }

                hbom.transferredTillDate = hbom.HBOMDetails.DistinctBy(e => e.hbomPracticeNo).Sum(e => e.transferredAmount);
                lstHbom.Add(hbom);
                return lstHbom;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<decimal> GetFarmersNoOfAcres(string cropNameCode, string psNumber)
        {

            var farmersNoOfAcres = await (from fad in _context.FarmersAgreementDetails
                                          where fad.Crop_Name_Code == cropNameCode && fad.PS_Number == psNumber
                                          select fad.Farmers_No_of_Acres_Area)
                                                         .DefaultIfEmpty()
                                                         .SumAsync();

            return farmersNoOfAcres;
        }
        public async Task<List<string>> GetHBOMPracticePerAcreage(string cropNameCode, string psNumber)
        {
            var hBOMPracticePerAcreage = await (from fad in _context.packagePracticeDivisions
                                                where fad.CropNameCode == cropNameCode && fad.PSNO == psNumber
                                                select fad.PracticePerAcre)
                                                               .DefaultIfEmpty()
                                                               .ToListAsync();
            return hBOMPracticePerAcreage;
        }
        public async Task<List<string>> GetHBOMPracticeNo(string cropNameCode, string psNumber)
        {
            var hBOMPracticeNo = await (from fad in _context.packagePracticeDivisions
                                        where fad.CropNameCode == cropNameCode && fad.PSNO == psNumber
                                        select fad.PracticeNo)
                                                       .DefaultIfEmpty()
                                                       .ToListAsync();
            return hBOMPracticeNo;
        }

        public async Task<List<OrganisationOfficeLocationDetailsDto>> GetOrgofficelocationDetails()
        {
            List<OrganisationOfficeLocationDetailsDto> list = new List<OrganisationOfficeLocationDetailsDto>();
            list = await (from orgdetails in _context.OrganisationOfficeLocationDetails
                          select new OrganisationOfficeLocationDetailsDto
                          {
                              Org_Code = orgdetails.Org_Code,
                              Org_Office_No = orgdetails.Org_Office_No,
                              Org_Office_Name = orgdetails.Org_Office_Name
                          }).OrderBy(c => c.Org_Office_Name).ToListAsync();
            return list;
        }

        public async Task<StockAndBatchDetail> GetAllStockAndBatchDetails(string groupCode, string detailsCode, DateTime transferDate)
        {
            StockAndBatchDetail stockAndBatchDetail = new StockAndBatchDetail();
            stockAndBatchDetail.firstFields = new List<firstFilled>();
            stockAndBatchDetail.secondFields = new List<secondFilled>();
            firstFilled idetails = null;
            try
            {
                stockAndBatchDetail.firstFields = await (from rms in _context.RMStockDetails
                                                         join
                                                           rmsld in _context.RMStockLotDetails on rms.Stock_No equals rmsld.Stock_No
                                                         //join
                                                         //  rmimtd in _context.RMInputMaterialTransferDetails on rms.Stock_No equals rmimtd.stockno
                                                       join rmimtd in _context.RMInputMaterialTransferDetails on
                                                        new { x = rms.Stock_No, y = rms.Raw_Material_Details_Code, z = rms.Raw_Material_Group_Code }
                                                        equals new { x = rmimtd.stockno, y = detailsCode, z = groupCode }
                                                        into gj
                                                         from x in gj.DefaultIfEmpty()
                                                         where rms.Raw_Material_Group_Code == groupCode &&
                                                              rms.Raw_Material_Details_Code == detailsCode &&
                                                              rms.Stock_Date < transferDate
                                                         // && x.rmTransferDate < transferDate

                                                         select new firstFilled
                                                         {
                                                             stockNo = rms.Stock_No,
                                                             stockDate = rms.Stock_Date,
                                                             orgOfficeNo = rms.Org_office_No,
                                                             rawMaterialDetailsCode = rms.Raw_Material_Details_Code,
                                                             rawMaterialGroupCode = rms.Raw_Material_Group_Code,
                                                             rawMaterialUOM = rms.Raw_Material_UOM,
                                                             rmStockTotalDetailQty = rms.RM_Stock_Total_Detailed_Qty,
                                                             rawMaterialTotalQty = rms.Raw_Material_Total_QTY,
                                                             rawMaterialTotalAmount = rms.Raw_Material_Total_Amount,

                                                             rmStockLOTGRNDate = rmsld.RM_Stock_LOT_GRN_Date,
                                                             rmStockLOTGRNNo = rmsld.RM_Stock_LOT_GRN_No,
                                                             rmStockLotGrnQty = rmsld.RM_Stock_Lot_Grn_Qty,
                                                             rmStockLotGrnRate = rmsld.RM_Stock_Lot_Grn_Rate,
                                                             rmStockLotGrnAmount = rmsld.RM_Stock_Lot_Grn_Amount,

                                                             rmTransferNo = (x == null ? String.Empty : x.rmTransferNo),
                                                             rmTransferDate = (x == null ? DateTime.Now : x.rmTransferDate),
                                                             rmMaterialTransferQty = (x == null ? 0 : x.rmMaterialTransferQty)

                                                         }).ToListAsync();

                var sumOfMatTransferQty = await (from  rimtd in _context.RMInputMaterialTransferDetails 
                                                 where rimtd.rawMaterialDetailsCode.Equals(detailsCode) && rimtd.rawMaterialGroupCode.Equals(groupCode)
                                                 group rimtd by rimtd.stockno into k
                                                 select new
                                                 {
                                                     stockNo = k.Key,
                                                     SumOfMatTransferQty = k.Sum(x => x.rmMaterialTransferQty)
                                                 }).ToListAsync();


                foreach (var d in stockAndBatchDetail.firstFields)
                {
                    foreach (var m in sumOfMatTransferQty)
                    {
                        if (d.stockNo == m.stockNo)
                        {
                            d.sumRMMaterialTransferQty = m.SumOfMatTransferQty;
                        }
                    }
                }

                //var sumOfRM_Mat_Trnsfr_QTY = stockAndBatchDetail.firstFields.Sum(x => x.rmMaterialTransferQty);

                //stockAndBatchDetail.firstFields.ForEach(x => x.sumRMMaterialTransferQty = sumOfRM_Mat_Trnsfr_QTY);

                stockAndBatchDetail.flag = "A";

                if (stockAndBatchDetail.firstFields.Count > 0)
                {
                    stockAndBatchDetail.firstFields = stockAndBatchDetail.firstFields.GroupBy(l => l.rmStockLOTGRNNo)
                                                     .Select(m => m.FirstOrDefault())
                                                     .ToList();
                    return stockAndBatchDetail;
                }
                else
                {
                    stockAndBatchDetail.secondFields = await (from rmgd in _context.RMGRNDetails
                                                              join
                          rmmtcd in _context.RMMaterialTotalCostDetails on rmgd.RMGRNNo equals rmmtcd.rmGrnNo
                                                              join
                                    rmimtd in _context.RMInputMaterialTransferDetails on rmmtcd.rmGrnNo equals rmimtd.rmGrnNo
                                            into gj
                                                              from x in gj.DefaultIfEmpty()
                                                              where
                                                              rmmtcd.rawMaterialGroupCode == groupCode &&
                                                              rmmtcd.rawMaterialDetailsCode == detailsCode &&
                                                              //rmgd.RMGRNDate <= transferDate
                                                              // x.rmTransferDate <= transferDate

                                                              (rmgd.RMGRNDate <= transferDate || x.rmTransferDate <= transferDate)
                                                              select new secondFilled
                                                              {
                                                                  rmGrnDate = rmgd.RMGRNDate,
                                                                  rmGrnNo = rmgd.RMGRNNo,

                                                                  rawmaterialGroupCode = rmmtcd.rawMaterialGroupCode,
                                                                  rmBatchNo = rmmtcd.rmBatchNo,
                                                                  rmGRNreceivedQty = rmmtcd.rmGrnReceivedQty,
                                                                  rmGRNMaterialWiseTotalCost = rmmtcd.rmGrnMaterialwiseTotalCost,
                                                                  rmGRNMaterialWiseTotalRate = rmmtcd.rmGrnMaterialwiseTotalRate,

                                                                  rmTransferNo = (x == null ? String.Empty : x.rmTransferNo),
                                                                  rmTransferDate = (x == null ? DateTime.Now : x.rmTransferDate),
                                                                  rmMaterialTransferQty = (x == null ? 0 : x.rmMaterialTransferQty)
                                                              }).ToListAsync();

                    //var sum_RM_Mat_Trnsfr_QTY = stockAndBatchDetail.secondFields.Sum(x => x.rmMaterialTransferQty);
                    //stockAndBatchDetail.secondFields.ForEach(x => x.sumRMMaterialTransferQty = sum_RM_Mat_Trnsfr_QTY);

                    var sumOfMatTransferQty_batch = await (from rimtd in _context.RMInputMaterialTransferDetails
                                                     where rimtd.rawMaterialDetailsCode.Equals(detailsCode) && rimtd.rawMaterialGroupCode.Equals(groupCode)
                                                     group rimtd by rimtd.rmBatchNo into k
                                                     select new
                                                     {
                                                         rmBatchNo = k.Key,
                                                         SumOfMatTransferQty = k.Sum(x => x.rmMaterialTransferQty)
                                                     }).ToListAsync();


                    foreach (var d in stockAndBatchDetail.secondFields)
                    {
                        foreach (var m in sumOfMatTransferQty_batch)
                        {
                            if (d.rmBatchNo == m.rmBatchNo)
                            {
                                d.sumRMMaterialTransferQty = m.SumOfMatTransferQty;
                            }
                        }
                    }

                    stockAndBatchDetail.flag = "B";
                    return stockAndBatchDetail;
                }

            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<SaveStockAndBatchDetail> SaveStockAndBatchDetail(SaveStockAndBatchDetail saveStockAndBatchDetails)
        {
            SaveStockAndBatchDetail stockAndBatch = new SaveStockAndBatchDetail();
            // pReturn.rmGrnDetail = new List<RMGRNDetail>();
            stockAndBatch.rmInputTransferDetails = new InputTransferDetails();
            stockAndBatch.outwardGatePassDetails = new OutwardGatePassDetail();
            stockAndBatch.rMInputMaterialTransferDetails = new RMInputMaterialTransferDetail();
            using (var tran = _context.Database.BeginTransaction())
            {
                try
                {
                    if (saveStockAndBatchDetails != null)
                    {
                        if (saveStockAndBatchDetails.rmInputTransferDetails != null)
                        {

                            saveStockAndBatchDetails.rmInputTransferDetails.TransferNumber = await GenerateTransferNoPK();

                            //int? MaxPOId = await _context.InputTransferDetails.MaxAsync(e => (int?)e.Id);
                            //if (MaxPOId != null)
                            //{
                            //    int newId = (int)MaxPOId + 1;
                            //    saveStockAndBatchDetails.rmInputTransferDetails.TransferNumber = "MTTB_" + newId;
                            //}
                            //else
                            //    saveStockAndBatchDetails.rmInputTransferDetails.TransferNumber = "MTTB_1";

                            stockAndBatch.rmInputTransferDetails = saveStockAndBatchDetails.rmInputTransferDetails;
                            _context.InputTransferDetails.Add(stockAndBatch.rmInputTransferDetails);
                            await _context.SaveChangesAsync();

                            if (saveStockAndBatchDetails.outwardGatePassDetails != null)
                            {
                                saveStockAndBatchDetails.outwardGatePassDetails.ogpNo = await GetOutwardGatePassNo();
                                //int? MaxOGPId = await _context.OutwardGatePassDetails.MaxAsync(e => (int?)e.Id);
                                //if (MaxOGPId != null)
                                //{
                                //    int newId = (int)MaxOGPId + 1;
                                //    saveStockAndBatchDetails.outwardGatePassDetails.ogpNo = "OGP_" + newId;
                                //}
                                //else
                                //    saveStockAndBatchDetails.outwardGatePassDetails.ogpNo = "OGP_" + 1;

                                stockAndBatch.outwardGatePassDetails = saveStockAndBatchDetails.outwardGatePassDetails;
                                stockAndBatch.outwardGatePassDetails.transactionNo = saveStockAndBatchDetails.rmInputTransferDetails.TransferNumber;
                                stockAndBatch.outwardGatePassDetails.ogpNo = saveStockAndBatchDetails.outwardGatePassDetails.ogpNo;
                                _context.OutwardGatePassDetails.Add(stockAndBatch.outwardGatePassDetails);
                                await _context.SaveChangesAsync();
                            }
                            if (saveStockAndBatchDetails.rMInputMaterialTransferDetails != null)
                            {
                                stockAndBatch.rMInputMaterialTransferDetails = saveStockAndBatchDetails.rMInputMaterialTransferDetails;
                                stockAndBatch.rMInputMaterialTransferDetails.rmTransferNo = saveStockAndBatchDetails.rmInputTransferDetails.TransferNumber;
                                stockAndBatch.rMInputMaterialTransferDetails.ogpNo = saveStockAndBatchDetails.outwardGatePassDetails.ogpNo;

                                _context.RMInputMaterialTransferDetails.Add(stockAndBatch.rMInputMaterialTransferDetails);
                                await _context.SaveChangesAsync();
                            }
                        }

                    }

                    tran.Commit();
                    return stockAndBatch;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }

        public async Task<List<HBOMDetailsDto>> GetDetailsByCropAndPsNumber(string cropNameCode, string psNumber)
        {
            List<HBOMDetailsDto> lstHbom = new List<HBOMDetailsDto>();
            HBOMDetailsDto hbom = new HBOMDetailsDto();
            hbom.HBOMDetails = new List<HBOMDetails>();

            hbom.HBOMDetails = await (from ppd in _context.packagePracticeDivisions
                                      join
ppm in _context.packagePracticeMaterials on ppd.PracticeNo equals ppm.PracticeNo
                                      join
rmd in _context.RawMaterialDetails on ppm.Raw_Material_Details_Code equals rmd.Raw_Material_Details_Code
                                      join
rmgd in _context.RawMaterialGroupMaster on ppm.RawmaterialGroupcode equals rmgd.Raw_Material_Group_Code
//                                      join
//rmitd in _context.InputTransferDetails on ppm.CropNameCode equals rmitd.CropNameCode into gj
//                                      from x in gj.DefaultIfEmpty()
//                                      join
//rimtd in _context.RMInputMaterialTransferDetails on ppd.PracticeNo equals rimtd.hbomPracticeNo into gk
//                                      from y in gk.DefaultIfEmpty()

                                      where ppd.CropNameCode.Equals(cropNameCode) && ppd.PSNO.Equals(psNumber)
                                     
                                      select new HBOMDetails()
                                      {
                                          hbOMDivisonFor = ppd.DivisionFor,
                                          hbomPracticePerAcrage = ppd.PracticePerAcre,
                                          hSTransactionCode = ppd.TransCode,
                                          hSCropPhaseCode = ppd.CropphaseCode,
                                          hbomPracticeEffectiveDate = ppd.PracticeEffectiveDate,
                                          hbomPracticeNo = ppd.PracticeNo,
                                          rawMaterialGroupCode = ppm.RawmaterialGroupcode,
                                          rawMaterialDetailsCode = ppm.Raw_Material_Details_Code,
                                          hbomChemicalUOM = ppm.ChemicalUOM,
                                          hbomChemicalVolume = ppm.Chemicalvolume,
                                          rawMaterialGroup = rmgd.Raw_Material_Group,
                                          RawMaterialDetailsName = rmd.Raw_Material_Details_Name
                                          //transferredAmount = (y == null ? 0 : y.rmMaterialTransferAmount)

                                      }).ToListAsync();

            //.GroupBy(l => l.rawMaterialDetailsCode)
            //.Select(m => m.FirstOrDefault())
            //.ToListAsync();

            //.Select(c => new HBOMDetails
            //{

            //    hbOMDivisonFor = c.Select(x => x.hbOMDivisonFor).FirstOrDefault(),
            //    hbomPracticePerAcrage = c.Select(x => x.hbomPracticePerAcrage).FirstOrDefault(),
            //    hSTransactionCode = c.Select(x => x.hSTransactionCode).FirstOrDefault(),
            //    hSCropPhaseCode = c.Select(x => x.hSCropPhaseCode).FirstOrDefault(),
            //    hbomPracticeEffectiveDate = c.Select(x => x.hbomPracticeEffectiveDate).FirstOrDefault(),
            //    hbomPracticeNo = c.Select(x => x.hbomPracticeNo).FirstOrDefault(),
            //    rawMaterialGroupCode = c.Select(x => x.rawMaterialGroupCode).FirstOrDefault(),
            //    rawMaterialDetailsCode = c.Select(x => x.rawMaterialDetailsCode).FirstOrDefault(),
            //    hbomChemicalUOM = c.Select(x => x.hbomChemicalUOM).FirstOrDefault(),
            //    hbomChemicalVolume = c.Select(x => x.hbomChemicalVolume).FirstOrDefault(),
            //    rawMaterialGroup = c.Select(x => x.rawMaterialGroup).FirstOrDefault(),
            //    RawMaterialDetailsName = c.Select(x => x.RawMaterialDetailsName).FirstOrDefault(),
            //    transferredAmount = c.Select(x => x.transferredAmount).FirstOrDefault(),
            //})
            //.OrderBy(e => e.rawMaterialGroupCode)

            //hbom.transferredTillDate = hbom.HBOMDetails.DistinctBy(e => e.hbomPracticeNo).Sum(e => e.transferredAmount);

            if (hbom.HBOMDetails.Count > 0)
            {
                var groupedData = (from dtails in hbom.HBOMDetails
                                   group dtails by
                                   dtails.rawMaterialDetailsCode into hbomGroupsData
                                   select new
                                   {
                                       rawMaterialDetailsCode = hbomGroupsData.Key,
                                       SumOfhbomChemicalVolume = hbomGroupsData.Sum(x => x.hbomChemicalVolume)
                                   }).ToList();

                hbom.HBOMDetails = hbom.HBOMDetails.GroupBy(l => l.rawMaterialDetailsCode)
                                   .Select(m => m.FirstOrDefault())
                                   .ToList();


                foreach (var d in hbom.HBOMDetails)
                {
                    foreach (var m in groupedData)
                    {
                        if (d.rawMaterialDetailsCode == m.rawMaterialDetailsCode)
                        {
                            d.hbomChemicalVolume = m.SumOfhbomChemicalVolume;
                        }
                    }
                }

            }

            var sumOfMatTransferQty = await (from rmit in _context.InputTransferDetails
                                             join rimtd in _context.RMInputMaterialTransferDetails on
                                               rmit.TransferNumber equals rimtd.rmTransferNo into gj
                                             from x in gj.DefaultIfEmpty()
                                             where rmit.CropNameCode == cropNameCode
                                             group x by x.rawMaterialDetailsCode into k
                                             select new
                                             {
                                                 rawMaterialDetailsCode = k.Key,
                                                 SumOfMatTransferQty = k.Sum(x => x.rmMaterialTransferQty)
                                             }).ToListAsync();

            foreach(var d in hbom.HBOMDetails)
            {
                foreach(var m in sumOfMatTransferQty)
                {
                    if(d.rawMaterialDetailsCode == m.rawMaterialDetailsCode)
                    {
                        d.transferredAmount = m.SumOfMatTransferQty;
                    }
                }
            }

            //hbom.transferredTillDate = hbom.HBOMDetails.Sum(e => e.sumRmMaterialTransferQty);

            lstHbom.Add(hbom);
            return lstHbom;
        }

        public async Task<List<CropDetailsByGroupCode>> GetCropDetailsByCode(string cropGroupCode)
        {
            var cropDetails = await (from c in _context.Crops
                                     join cg in _context.CropGroups on
                                     c.CropGroupCode equals cg.CropGroupCode
                                     join
cs in _context.CropSchemes on c.CropCode equals cs.CropCode

                                     where c.CropGroupCode.Equals(cropGroupCode)

                                     select new CropDetailsByGroupCode
                                     {
                                         CropGroupCode = c.CropGroupCode,
                                         CropGroupName = cg.Name,
                                         CropNameCode = c.CropCode,
                                         Name = c.Name,
                                         CropSchemeCode = cs.Code,
                                         CropSchemeFrom = cs.From
                                     }).GroupBy(l => l.CropNameCode)
                                      .Select(m => m.FirstOrDefault())
                                      .OrderBy(c => c.Name)
                                      .ToListAsync();

            return cropDetails;
        }

    }
}