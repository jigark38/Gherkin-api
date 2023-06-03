using AutoMapper;
using GherkinWebAPI.Core.PackageofPractice;
using GherkinWebAPI.DTO;
using GherkinWebAPI.DTO.PackageofpracticeDto;
using GherkinWebAPI.Models.PackageOfPracticeModel;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace GherkinWebAPI.Repository.PackageOfPracticesRepository
{
    public class PackageofPracticeRepository : RepositoryBase<PackageofPracticeDivisionDto>, IPackageofPracticeRepository
    {
        private RepositoryContext _context;

        public PackageofPracticeRepository(RepositoryContext repositoryContext)
               : base(repositoryContext)
        {
            this._context = repositoryContext;
        }

        public async Task<List<PackageofPracticeDivisionDto>> GetCropNameByCropGroup(string CropGroupCode)
        {
            var comments = await (from cropname in _context.Crops
                                  join cropgroup in _context.CropGroups on cropname.CropGroupCode equals cropgroup.CropGroupCode
                                  where cropname.CropGroupCode == CropGroupCode
                                  select new PackageofPracticeDivisionDto
                                  {
                                      CropName = cropname.Name,
                                      CropNameCode = cropname.CropCode,
                                      CropGroupCode = cropname.CropGroupCode,
                                  }).OrderBy(x => x.CropName).ToListAsync();
            return comments;
        }
        public async Task<List<PackageofPracticeDivisionDto>> GetPSNOByCropName(string CropNameCode)
        {
            var comments = await (from Pdivision in _context.PlantationSchedules
                                  join CropNameDetails in _context.Crops on Pdivision.CropNameCode equals CropNameDetails.CropCode
                                  where Pdivision.CropNameCode == CropNameCode
                                  select new PackageofPracticeDivisionDto
                                  {
                                      PSNO = Pdivision.PsNumber,
                                      CreatedDate = Pdivision.FromDate,
                                      ModifyDate = Pdivision.ToDate,
                                      CropNameCode = CropNameDetails.Name,
                                  }).OrderBy(x => x.CreatedDate).ToListAsync();

            var comm = comments.Select(div => new PackageofPracticeDivisionDto
            {
                PSNO = div.PSNO,
                Date = div.CreatedDate.Value.ToString("dd-MMM-yyyy") + "/" + div.ModifyDate.Value.ToString("dd-MMM-yyyy"),
                CropNameCode = div.CropNameCode,
            }).ToList();
            return comm;
        }

        public async Task<List<PackageofPracticeDivisionDto>> GetPSNoByCropAndHBOMDivisionForFind(string cropNameCode, string packageOfPractice)
        {
            List<PackageofPracticeDivisionDto> packageofPracticeList = new List<PackageofPracticeDivisionDto>();

            var psNoList = await _context.packagePracticeDivisions.Where(x => x.CropNameCode == cropNameCode && x.DivisionFor == packageOfPractice).Select(x => x.PSNO).ToListAsync();

            var itemList = await (from schedules in _context.PlantationSchedules
                                  where psNoList.Contains(schedules.PsNumber)
                                  select new PackageofPracticeDivisionDto
                                  {
                                      PSNO = schedules.PsNumber,
                                      CreatedDate = schedules.FromDate,
                                      ModifyDate = schedules.ToDate,
                                      CropNameCode = schedules.CropNameCode,
                                  }).OrderBy(x => x.CreatedDate).ToListAsync();

            packageofPracticeList = itemList.Select(div => new PackageofPracticeDivisionDto
            {
                PSNO = div.PSNO,
                Date = div.CreatedDate.Value.ToString("dd-MMM-yyyy") + "/" + div.ModifyDate.Value.ToString("dd-MMM-yyyy"),
                CropNameCode = div.CropNameCode,
            }).Distinct().ToList();
            return packageofPracticeList;
        }

        public async Task<List<PackageofPracticeDivisionDto>> GetTransCodeByCropNameCode(string cropNameCode, string packageOfPractice)
        {
            var comments = await (from Hmaster in _context.HarvestStageMaster
                                  join CropNameDetails in _context.Crops on Hmaster.Crop_Name_Code equals CropNameDetails.CropCode
                                  where Hmaster.Crop_Name_Code == cropNameCode && Hmaster.HBOMDivisionFor == packageOfPractice
                                  select new PackageofPracticeDivisionDto
                                  {
                                      TransCode = Hmaster.HS_Transaction_Code,
                                      PracticeEffectiveDate = Hmaster.HS_Effective_Date,
                                      CropNameCode = CropNameDetails.Name
                                  }).OrderBy(x => x.PracticeEffectiveDate).ToListAsync();
            var comm = comments.Select(data => new PackageofPracticeDivisionDto
            {
                TransCode = data.TransCode,
                PracticeEffectiveDateString = data.PracticeEffectiveDate.ToString("dd-MMM-yyyy"),
                CropNameCode = data.CropNameCode,
            }).ToList();

            return comm;
        }

        public async Task<List<PackageofPracticeDivisionDto>> GetCropPhaseCodeByPackageOfPractice(string packageOfPractice)
        {
            var comments = await (from HDetails in _context.HarvestStageDetails
                                  join HMaster in _context.HarvestStageMaster on HDetails.HS_Transaction_Code equals (HMaster.HS_Transaction_Code)
                                  where HMaster.HBOMDivisionFor == packageOfPractice
                                  select new PackageofPracticeDivisionDto
                                  {
                                      TransCode = HDetails.HS_Transaction_Code,
                                      CropPhaseName = HDetails.HS_Crop_Phase_Name,
                                      CropphaseCode = HDetails.HS_Crop_Phase_Code,
                                      Id = HDetails.ID
                                  }).OrderBy(x => x.Id).ToListAsync();


            return comments;
        }
        public async Task<List<PackageofPracticeMaterialsDto>> GetCropStageList(string psNO, string transCode)
        {
            var stages = await (from material in _context.packagePracticeMaterials
                                join division in _context.packagePracticeDivisions
                                 on material.PracticeNo equals division.PracticeNo
                                 into divGroup
                                from divisionGroup in divGroup.DefaultIfEmpty()

                                join hDetail in _context.HarvestStageDetails
                                on divisionGroup.CropphaseCode equals hDetail.HS_Crop_Phase_Code

                                join rmDetails in _context.RawMaterialDetails
                                on material.Raw_Material_Details_Code equals rmDetails.Raw_Material_Details_Code

                                where divisionGroup.PSNO == psNO && divisionGroup.TransCode == transCode
                                select new PackageofPracticeMaterialsDto
                                {
                                    CropPhaseCode = material.CropNameCode,
                                    CropPhaseName = hDetail.HS_Crop_Phase_Name,
                                    DaysApplicable = material.DaysApplicable,
                                    TradeName = material.TradeName,
                                    ChemicalName = rmDetails.Raw_Material_Details_Name,
                                    Chemicalvolume = material.Chemicalvolume,
                                    Sprayvolume = material.Sprayvolume,
                                    ChemicalQty = material.ChemicalQty,
                                    TragetPest = material.TragetPest,
                                    ChemicalUOM = material.ChemicalUOM
                                }).OrderBy(x => x.DaysApplicable).ToListAsync();
            return stages;
        }
        public async Task<PackageofPracticeDivisionDto> GetHarevstByCropPhaseCode(string hcropPhaseCode)
        {
            var comments = await (from HDetails in _context.HarvestStageDetails
                                  where HDetails.HS_Crop_Phase_Code == hcropPhaseCode
                                  select new PackageofPracticeDivisionDto
                                  {
                                      HS_Days_After_Sowing_From = HDetails.HS_Days_After_Sowing_From,
                                      HS_Days_After_Sowing_To = HDetails.HS_Days_After_Sowing_To,
                                      HarvestDetails = HDetails.HS_Harvest_Details
                                  }).SingleOrDefaultAsync();


            return comments;
        }

        public async Task<PackagePracticeMaster> AddPracticeDeatils(PackagePracticeMaster packageofPracticeMaster)
        {
            var result = _context.packagePracticeMasters.Add(packageofPracticeMaster);
            await _context.SaveChangesAsync();
            return result;

        }
        public async Task<PackagePracticeDivision> AddPracticeDivision(PackagePracticeDivision packageofdivison)
        {

            int? selectMaxDeptId = await _context.packagePracticeDivisions?.MaxAsync(e => (int?)e.Id);
            if (selectMaxDeptId != null)
                packageofdivison.PracticeNo = "HBPC_" + Convert.ToString(selectMaxDeptId + 1);
            else
                packageofdivison.PracticeNo = "HBPC_" + "1";

            var result = _context.packagePracticeDivisions.Add(packageofdivison);
            await _context.SaveChangesAsync();
            return result;

        }

        public async Task<PackagePracticeMaterials> AddPracticeMaterials(PackagePracticeMaterials practiceMaterials)
        {

            var result = _context.packagePracticeMaterials.Add(practiceMaterials);
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<List<RawMaterialMasterDto>> GetRawmaterialMaster()
        {
            var result = await FindAll().ToListAsync();

            List<RawMaterialMasterDto> list = new List<RawMaterialMasterDto>();

            foreach (var i in result)
            {
                list.Add(Mapper.Map<RawMaterialMasterDto>(i));
            }
            return list;
        }
        public async Task<List<RawMaterialDetailsDto>> GetRawmaterialsDetailsByRawGroupcode(string rawGroupCode)
        {
            var comments = await (from rawdetails in _context.RawMaterialDetails
                                  join rawmater in _context.RawMaterialGroupMaster on rawdetails.Raw_Material_Group_Code equals rawmater.Raw_Material_Group_Code
                                  where rawdetails.Raw_Material_Group_Code == rawGroupCode
                                  select new RawMaterialDetailsDto
                                  {
                                      Raw_Material_Details_Code = rawdetails.Raw_Material_Details_Code,
                                      Raw_Material_Details_Name = rawdetails.Raw_Material_Details_Name
                                  }).ToListAsync();


            return comments;
        }

        public async Task<List<PackageofPracticeMaterialsDto>> GetChemicalUOM()
        {
            var comments = await (from material in _context.packagePracticeMaterials
                                  select new PackageofPracticeMaterialsDto { ChemicalUOM = material.ChemicalUOM }).ToListAsync();

            return comments;
        }

        public async Task<List<PackagePracticeDivision>> GetPackagePracticeDivisions(string psNumber, string cropNameCode)
        {
            return await this._context.packagePracticeDivisions.Where(x => x.PSNO == psNumber && x.CropNameCode == cropNameCode).ToListAsync();
        }



        //public async Task<List<PackageofPracticeDivisionDto>> GetCropStageDetails()
        //{
        //    var comments = await Task.Run(() => (from pdiv in _context.packagePracticeDivisions
        //                                         join pmaterials in _context.packagePracticeMaterials on pdiv.PracticeNo equals pmaterials.PracticeNo
        //                                         select new PackageofPracticeDivisionDto
        //                                         {
        //                                             CropphaseCode = pdiv.CropphaseCode,
        //                                             Days = pmaterials.DaysApplicable.ToString(),
        //                                             PackagePracticeMaterials = (from f in pdiv.packagePracticeMaterials
        //                                                                         select new PackageofPracticeMaterialsDto
        //                                                                         {

        //                                                                             TradeName = pmaterials.TradeName,
        //                                                                             ChemicalName = pmaterials.ChemicalName,
        //                                                                             Chemicalvolume = pmaterials.Chemicalvolume,
        //                                                                             Sprayvolume = pmaterials.Sprayvolume,
        //                                                                             ChemicalQty = pmaterials.ChemicalQty,
        //                                                                             TragetPest = pmaterials.TragetPest
        //                                                                         }).ToList()
        //                                         }).ToList());


        //    return comments;
        //}

    }
}
