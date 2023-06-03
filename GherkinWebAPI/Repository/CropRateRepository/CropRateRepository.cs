using GherkinWebAPI.Core;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.CropsRate;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Repository
{
    public class CropRateRepository : RepositoryBase<CropRate>, ICropRateRepository
    {
        private readonly RepositoryContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CropRepository"/> class.
        /// </summary>
        /// <param name="repositoryContext">The repositoryContext<see cref="RepositoryContext"/></param>
        public CropRateRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
            _context = repositoryContext;
        }
        public async Task<List<CropRateDTO>> GetAllCropRatesWithPSnumber()
        {
            List<CropRate> AllCropRate = await (from acr in this._context.CropRates
                                                orderby acr.Crop_Rate_No
                                                select acr).ToListAsync();
            List<CropAssociationRate> AllAssociatedCropRate = await (from acr in this._context.CropAssociationRates
                                                                     orderby acr.Crop_Rate_No
                                                                     select acr).ToListAsync();
            List<CropRateDTO> listObj = new List<CropRateDTO>();
            for (int k = 0; k < AllCropRate.Count; k++)
            {
                CropRateDTO obj1 = new CropRateDTO();
                obj1.Crop_Rate_No = AllCropRate[k].Crop_Rate_No;
                obj1.CropRateEntryDate = AllCropRate[k].CropRateEntryDate;
                obj1.CropRateEnteredByEmpId = AllCropRate[k].CropRateEnteredByEmpId;
                obj1.CropGroupCode = AllCropRate[k].CropGroupCode;
                obj1.CropGroupName = AllCropRate[k].CropGroupName;
                obj1.AllAreas = AllCropRate[k].AllAreas;
                obj1.AreaId = AllCropRate[k].AreaId;
                obj1.AllVillages = AllCropRate[k].AllVillages;
                obj1.VillageCode = AllCropRate[k].VillageCode;
                string cnc = AllCropRate[k].CropGroupName;
                string psn = await (from acr in this._context.PlantationSchedules
                                    where acr.CropNameCode == cnc
                                    select acr.PsNumber).FirstOrDefaultAsync();
                obj1.CropSchemeCode = psn;
                obj1.CropRateEffectiveDate = AllCropRate[k].CropRateEffectiveDate;
                obj1.CropSchemeCode = psn;
                obj1.CropCount = AllAssociatedCropRate[k].CropCount;
                obj1.CropSchemeFrom = AllAssociatedCropRate[k].CropSchemeFrom;
                obj1.CropSchemeSign = AllAssociatedCropRate[k].CropSchemeSign;
                obj1.CropRateAsperAssociation = AllAssociatedCropRate[k].CropRateAsperAssociation;
                obj1.CropRatePerUOM = AllAssociatedCropRate[k].CropRatePerUOM;
                listObj.Add(obj1);

            }

            return listObj;
        }


        public async Task<List<CropRateDTO>> GetAllCropRates()
        {
            List<CropRate> AllCropRate = await (from acr in this._context.CropRates
                                                orderby acr.Crop_Rate_No
                                                select acr).ToListAsync();
            List<CropAssociationRate> AllAssociatedCropRate = await (from acr in this._context.CropAssociationRates
                                                                     orderby acr.Crop_Rate_No
                                                                     select acr).ToListAsync();
            List<CropRateDTO> listObj = new List<CropRateDTO>();
            for (int k = 0; k < AllCropRate.Count; k++)
            {
                CropRateDTO obj1 = new CropRateDTO();
                obj1.Crop_Rate_No = AllCropRate[k].Crop_Rate_No;
                obj1.CropRateEntryDate = AllCropRate[k].CropRateEntryDate;
                obj1.CropRateEnteredByEmpId = AllCropRate[k].CropRateEnteredByEmpId;
                obj1.CropGroupCode = AllCropRate[k].CropGroupCode;
                obj1.CropGroupName = AllCropRate[k].CropGroupName;
                obj1.AllAreas = AllCropRate[k].AllAreas;
                obj1.AreaId = AllCropRate[k].AreaId;
                obj1.AllVillages = AllCropRate[k].AllVillages;
                obj1.VillageCode = AllCropRate[k].VillageCode;
                obj1.CropSchemeCode = AllCropRate[k].CropSchemeCode;
                obj1.CropRateEffectiveDate = AllCropRate[k].CropRateEffectiveDate;
                obj1.CropSchemeCode = AllAssociatedCropRate[k].CropSchemeCode;
                obj1.CropCount = AllAssociatedCropRate[k].CropCount;
                obj1.CropSchemeFrom = AllAssociatedCropRate[k].CropSchemeFrom;
                obj1.CropSchemeSign = AllAssociatedCropRate[k].CropSchemeSign;
                obj1.CropRateAsperAssociation = AllAssociatedCropRate[k].CropRateAsperAssociation;
                obj1.CropRatePerUOM = AllAssociatedCropRate[k].CropRatePerUOM;
                listObj.Add(obj1);

            }

            return listObj;
        }
        public async Task<List<GroupCode>> GetAllGroupName()
        {
            var AllGroupName = await (from grp in this._context.CropGroups
                                      orderby grp.Name
                                      select new GroupCode
                                      {
                                          CropGroupCode = grp.CropGroupCode,
                                          CropGroupName = grp.Name
                                      }).ToListAsync();

            return AllGroupName;
        }
        public async Task<List<CropName>> GetCropNameCode(string groupCode)
        {
            var AllCropName = await (from crp in this._context.Crops
                                     where crp.CropGroupCode == groupCode
                                     orderby crp.Name
                                     select new CropName
                                     {
                                         CropNameCode = crp.CropCode,
                                         Name = crp.Name
                                     }).ToListAsync();
            return AllCropName;
        }
        public async Task<List<AreaName>> GetAllAreas()
        {
            var AllAreaName = await (from ara in this._context.Areas
                                     orderby ara.Area_Name
                                     select new AreaName
                                     {
                                         AreaId = ara.Area_ID,
                                         Name = ara.Area_Name
                                     }).ToListAsync();
            return AllAreaName;
        }
        public async Task<List<VillageName>> GetVillageCode(string areaId)
        {
            var villages = await (from hav in _context.HarvestAreaVillages
                                  join v in _context.Villages on hav.Village_Code equals v.Village_Code
                                  where hav.Area_ID == areaId
                                  select new VillageName
                                  {
                                      VillageCode = hav.Village_Code,
                                      Name = v.Village_Name
                                  })
                                  .OrderBy(village => village.Name)
                                  .ToListAsync();

            return villages;
        }
        public async Task<List<CropFromTo>> GetSeasonFromTo(string CNameCode)
        {
            var AllCropFromTo = await (from cft in this._context.PlantationSchedules
                                       where cft.CropNameCode == CNameCode
                                       orderby cft.FromDate
                                       select new CropFromTo
                                       {
                                           PSNumber = cft.PsNumber,
                                           SeasonFrom = cft.FromDate,
                                           SeasonTo = cft.ToDate
                                       }).ToListAsync();
            return AllCropFromTo;
        }
        public async Task<List<CropCount>> GetCropCountMM(string PS_number)
        {
            int a = Convert.ToInt32(PS_number.Split('_')[1]);
            var CNameCode = await (from ps in this._context.PlantationSchedules
                                   where ps.Id == a
                                   select ps).FirstOrDefaultAsync();

            var AllCropCount = await (from cc in this._context.CropSchemes
                                      where cc.CropCode == CNameCode.CropNameCode
                                      select new CropCount
                                      {
                                          CropSchemeCode = cc.Code,
                                          CropCountMM = cc.Count,
                                          CropSchemeFrom = cc.From,
                                          CropSchemeSign = cc.Sign
                                      }).OrderByDescending(b => b.CropSchemeFrom).ToListAsync();

            return AllCropCount;
        }
        public async Task<List<FruitSizeCount>> GetFruitSizeCount(decimal CropCountMM)
        {
            var fruitsizeCount = await (from fs in this._context.CropSchemes
                                        where fs.Count == CropCountMM
                                        select new FruitSizeCount
                                        {
                                            CropSchemeFrom = fs.From,
                                            CropSchemeSign = fs.Sign
                                        }).ToListAsync();
            return fruitsizeCount;
        }
        public async Task<List<CropRateDTO>> AddCropRate(List<CropRateDTO> ListcropRate)
        {
            List<CropRateDTO> obj3 = new List<CropRateDTO>();

            try
            {
                //List<CropRateDTO> obj5 = new List<CropRateDTO>();
                obj3 = await GetAllCropRatesWithPSnumber();
                CropRate obj1 = new CropRate();
                foreach (CropRateDTO cropRate in ListcropRate)
                {
                    int a = Convert.ToInt32(cropRate.PsNumber.Split('_')[1]);

                    //int cnt = _context.CropRates.Count();
                    //var cropRatecheck = await (from cnc1 in this._context.CropAssociationRates
                    //                           where  cnc1.CropCount == cropRate.CropCount && cnc1.CropSchemeFrom == cropRate.CropSchemeFrom && cnc1.CropSchemeSign == cropRate.CropSchemeSign
                    //                           select cnc1).FirstOrDefaultAsync();
                    //if (cropRatecheck == null)
                    //{
                    string cnc = await (from cnc1 in this._context.PlantationSchedules
                                        where cnc1.Id == a
                                        select cnc1.CropNameCode).FirstOrDefaultAsync();
                    string csc = await (from csc1 in this._context.CropSchemes
                                        where csc1.CropCode == cropRate.CropSchemeCode
                                        select csc1.Code).FirstOrDefaultAsync();
                    List<string> cropRateNo = await (from cr in this._context.CropRates

                                                     select cr.Crop_Rate_No).ToListAsync();
                    List<int> num = new List<int>();
                    if (cropRateNo.Count == 0)
                    {
                        num.Add(0);
                    }
                    else
                    {
                        foreach (string i in cropRateNo)
                        {
                            num.Add(Convert.ToInt32(i.Split('_')[1]));
                        }
                    }
                    if (obj1.Crop_Rate_No == null)
                    {
                        obj1.Crop_Rate_No = "CRRN_" + (num.Max() + 1).ToString();
                        obj1.CropRateEntryDate = cropRate.CropRateEntryDate;
                        obj1.CropRateEnteredByEmpId = cropRate.CropRateEnteredByEmpId;
                        obj1.CropGroupCode = cropRate.CropGroupCode;
                        obj1.CropGroupName = cropRate.CropGroupName;
                        obj1.AllAreas = cropRate.AllAreas;
                        obj1.AreaId = cropRate.AreaId;
                        obj1.AllVillages = cropRate.AllVillages;
                        obj1.VillageCode = cropRate.VillageCode;
                        obj1.CropSchemeCode = cropRate.CropSchemeCode;
                        obj1.CropRateEffectiveDate = cropRate.CropRateEffectiveDate;
                        obj1.PSNumber = cropRate.PsNumber;
                        _context.CropRates.Add(obj1);
                        await _context.SaveChangesAsync();
                    }

                    CropAssociationRate obj2 = new CropAssociationRate();
                    obj2.Crop_Rate_No = obj1.Crop_Rate_No;
                    obj2.CropSchemeCode = cropRate.CropSchemeCode;
                    obj2.CropCount = cropRate.CropCount;
                    obj2.CropSchemeFrom = cropRate.CropSchemeFrom;
                    obj2.CropSchemeSign = cropRate.CropSchemeSign;
                    obj2.CropRateAsperAssociation = cropRate.CropRateAsperAssociation;
                    obj2.CropRatePerUOM = cropRate.CropRatePerUOM;
                    _context.CropAssociationRates.Add(obj2);
                    await _context.SaveChangesAsync();
                    //List<CropRateDTO> obj3 = new List<CropRateDTO>();
                    cropRate.Crop_Rate_No = obj2.Crop_Rate_No;
                    obj3.Add(cropRate);
                }
                //else
                //{
                //    //CropRateDTO obj4 = new CropRateDTO();
                //    cropRate.message = "Rate already exists";
                //    obj3.Add(cropRate);
                //}

                //}
            }
            catch (Exception e)
            {

            }
            //obj3.AddRange(obj5);
            return obj3;
        }
        public async Task<List<CropRateDTO>> FindCropRateAccordingToSeason(string psNumber)
        {
            var PS = await (from ps in this._context.PlantationSchedules
                            where ps.PsNumber.ToLower() == psNumber.ToLower()
                            select ps).FirstOrDefaultAsync();
            string CropSchemeCode = await (from ps in this._context.CropRates
                                           where ps.PSNumber == PS.PsNumber
                                           select ps.CropSchemeCode).FirstOrDefaultAsync();

            List<CropRateDTO> listObj = await (from cnc in this._context.CropRates
                                               join car in this._context.CropAssociationRates
                                               on cnc.Crop_Rate_No equals car.Crop_Rate_No
                                               where cnc.CropSchemeCode == CropSchemeCode
                                               select new CropRateDTO
                                               {
                                                   Crop_Rate_No = cnc.Crop_Rate_No,
                                                   CropRateEntryDate = cnc.CropRateEntryDate,
                                                   CropRateEnteredByEmpId = cnc.CropRateEnteredByEmpId,
                                                   CropGroupCode = cnc.CropGroupCode,
                                                   CropGroupName = cnc.CropGroupName,
                                                   AllAreas = cnc.AllAreas,
                                                   AreaId = cnc.AreaId,
                                                   AllVillages = cnc.AllVillages,
                                                   VillageCode = cnc.VillageCode,
                                                   CropSchemeCode = car.CropSchemeCode,
                                                   CropRateEffectiveDate = cnc.CropRateEffectiveDate,
                                                   CropCount = car.CropCount,
                                                   CropSchemeFrom = car.CropSchemeFrom,
                                                   CropSchemeSign = car.CropSchemeSign,
                                                   CropRateAsperAssociation = car.CropRateAsperAssociation,
                                                   CropRatePerUOM = car.CropRatePerUOM,
                                                   PsNumber = PS.PsNumber
                                               }).ToListAsync();
            return listObj;
        }
        public async Task<List<CropRateDTO>> ModifySelectedCropRate(CropRateDTO cropRate)
        {
            try
            {
                int a = Convert.ToInt32(cropRate.PsNumber.Split('_')[1]);
                string cnc = await (from cnc1 in this._context.PlantationSchedules
                                    where cnc1.Id == a
                                    select cnc1.CropNameCode).FirstOrDefaultAsync();
                string csc = await (from csc1 in this._context.CropSchemes
                                    where csc1.CropCode == cnc
                                    select csc1.Code).FirstOrDefaultAsync();
                CropRate CropRate = await (from acr in this._context.CropRates
                                           where acr.Crop_Rate_No == cropRate.Crop_Rate_No
                                           select acr).FirstOrDefaultAsync();
                CropAssociationRate AssociatedCropRate = await (from acr in this._context.CropAssociationRates
                                                                where acr.Crop_Rate_No == CropRate.Crop_Rate_No
                                                                select acr).FirstOrDefaultAsync();
                if (CropRate != null && AssociatedCropRate != null)
                {

                    CropRate.CropRateEntryDate = cropRate.CropRateEntryDate;
                    CropRate.CropRateEnteredByEmpId = cropRate.CropRateEnteredByEmpId;
                    CropRate.CropGroupCode = cropRate.CropGroupCode;
                    CropRate.CropGroupName = cropRate.CropGroupName;
                    CropRate.AllAreas = cropRate.AllAreas;
                    CropRate.AreaId = cropRate.AreaId;
                    CropRate.AllVillages = cropRate.AllVillages;
                    CropRate.VillageCode = cropRate.VillageCode;
                    CropRate.CropSchemeCode = cropRate.CropSchemeCode;
                    CropRate.CropRateEffectiveDate = cropRate.CropRateEffectiveDate;
                    CropRate.PSNumber = cropRate.PsNumber;
                    await _context.SaveChangesAsync();
                    AssociatedCropRate.CropSchemeCode = cropRate.CropSchemeCode;
                    AssociatedCropRate.CropCount = cropRate.CropCount;
                    AssociatedCropRate.CropSchemeFrom = cropRate.CropSchemeFrom;
                    AssociatedCropRate.CropSchemeSign = cropRate.CropSchemeSign;
                    AssociatedCropRate.CropRateAsperAssociation = cropRate.CropRateAsperAssociation;
                    AssociatedCropRate.CropRatePerUOM = cropRate.CropRatePerUOM;
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {

            }

            List<CropRateDTO> obj3 = new List<CropRateDTO>();
            obj3 = await GetAllCropRatesWithPSnumber();
            return obj3;

        }
        public async Task<List<string>> GetCropRateUOM(string cropRate)
        {
            var cropRates = await (from cr in _context.CropAssociationRates
                                   where cr.CropRatePerUOM.Contains(cropRate)
                                   select cr.CropRatePerUOM).Distinct().ToListAsync();
            return cropRates;
        }
        public async Task<List<CropRateDTO>> DeleteSelectedCropRate(string cropRateNo)
        {
            try
            {
                CropRate obj1 = await (from cr in _context.CropRates
                                       where cr.Crop_Rate_No == cropRateNo
                                       select cr).FirstOrDefaultAsync();
                CropAssociationRate obj2 = await (from cr in _context.CropAssociationRates
                                                  where cr.Crop_Rate_No == cropRateNo
                                                  select cr).FirstOrDefaultAsync();
                if (obj1 != null && obj2 != null)
                {
                    _context.CropAssociationRates.Remove(obj2);
                    await _context.SaveChangesAsync();
                    _context.CropRates.Remove(obj1);
                    await _context.SaveChangesAsync();

                }
            }
            catch (Exception e)
            {

            }

            List<CropRateDTO> obj3 = new List<CropRateDTO>();
            obj3 = await GetAllCropRatesWithPSnumber();
            return obj3;

        }

        public async Task<CropAssociationAndUOMRate> GetCropRateByCode(string cropSchemeCode, decimal cropCountMM)
        {
            var cropAssociationAndUOMRate = await (from cr in _context.CropAssociationRates
                                                   where cr.CropSchemeCode == cropSchemeCode && cr.CropCount == cropCountMM
                                                   select new CropAssociationAndUOMRate
                                                   {
                                                       CropRateAsPerAssociation = cr.CropRateAsperAssociation,
                                                       CropRatePerUOM = cr.CropRatePerUOM
                                                   }).SingleOrDefaultAsync();

            return cropAssociationAndUOMRate;
        }

        public async Task<List<VillageName>> GetAllVillageCode()
        {
            var villages = await (from hav in _context.HarvestAreaVillages
                                  join v in _context.Villages on hav.Village_Code equals v.Village_Code
                                  select new VillageName
                                  {
                                      VillageCode = hav.Village_Code,
                                      Name = v.Village_Name,
                                      Area_ID=hav.Area_ID
                                  })
                                  .OrderBy(village => village.Name)
                                  .ToListAsync();

            return villages;
        }
        public async Task<List<CropFromTo>> GetAllSeasonFromTo()
        {
            var AllCropFromTo = await (from cft in this._context.PlantationSchedules
                                       orderby cft.FromDate
                                       select new CropFromTo
                                       {
                                           PSNumber = cft.PsNumber,
                                           SeasonFrom = cft.FromDate,
                                           SeasonTo = cft.ToDate,
                                           CropGroupCode=cft.CropGroupCode,
                                           CropNameCode=cft.CropNameCode
                                       }).ToListAsync();
            return AllCropFromTo;
        }
    }
}