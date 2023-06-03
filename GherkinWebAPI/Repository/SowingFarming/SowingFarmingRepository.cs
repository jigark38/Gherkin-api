using GherkinWebAPI.Core.SowingFarming;
using GherkinWebAPI.DTO.SowingFarming;
using GherkinWebAPI.Entities.SowingFarming;
using GherkinWebAPI.Persistence;
using GherkinWebAPI.Request.SowingFarming;
using GherkinWebAPI.Response.SowingFarming;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace GherkinWebAPI.Repository.SowingFarming
{
    public class SowingFarmingRepository : RepositoryBase<SowingFarmingDetails>, ISowingFarmingRepository
    {

        private readonly RepositoryContext _context;

        public SowingFarmingRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
            _context = repositoryContext;
        }

        public async Task<SowingFarmingDataForFormRequiredForGrid> GetSowingFarmingDataForFormRequiredForGrid(DateTime sowingDate, string cropNameCode, string psNumber)
        {
            SowingFarmingDataForFormRequiredForGrid sowingFarmingDataForFormRequiredForGrid = new SowingFarmingDataForFormRequiredForGrid();
            var masterHarvest = await this._context.HarvestStageMaster.Where(x => x.HS_Effective_Date >= sowingDate && x.Crop_Name_Code.Equals(cropNameCode)).ToListAsync();

            foreach (var l in masterHarvest)
            {
                var details = await this._context.HarvestStageDetails.Where(x => x.HS_Transaction_Code.Equals(l.HS_Transaction_Code)).ToListAsync();
                foreach (var x in details)
                {
                    sowingFarmingDataForFormRequiredForGrid.HarvestDataForSowingFarrmings.Add(new HarvestDataForSowingFarrmingDto
                    {
                        HS_Transaction_Code = x.HS_Transaction_Code,
                        HS_Crop_Phase_Code = x.HS_Crop_Phase_Code,
                        HS_Crop_Phase_Name = x.HS_Crop_Phase_Name,
                        HS_Days_After_Sowing_From = x.HS_Days_After_Sowing_From,
                        HS_Days_After_Sowing_To = x.HS_Days_After_Sowing_To,
                        HS_Effective_Date = l.HS_Effective_Date
                    });
                }
            }

            var practiceDiv = await this._context.packagePracticeDivisions.Where(x => x.CropNameCode.Equals(cropNameCode) && x.PSNO.Equals(psNumber)).ToListAsync();

            foreach (var p in practiceDiv)
            {
                var practiceMaterial = await this._context.packagePracticeMaterials.Where(x => x.PracticeNo.Equals(p.PracticeNo)).ToListAsync();

                foreach (var m in practiceMaterial)
                {
                    var crop = await this._context.RawMaterialDetails.FirstAsync(x => x.Raw_Material_Details_Code.Equals(m.Raw_Material_Details_Code));

                    if (!this._context.FarmingStageDetails.Any(x => x.ID == m.Id))
                    {
                        sowingFarmingDataForFormRequiredForGrid.HBOMPracticeForSowingFarmings.Add(new HBOMPracticeForSowingFarming
                        {
                            Id = m.Id,
                            Raw_Material_Details_Code = m.Raw_Material_Details_Code,
                            HBOM_Days_Applicable = m.DaysApplicable,
                            HBOM_Division_For = p.DivisionFor,
                            HBOM_Practice_No = p.PracticeNo,
                            HBOM_Practice_Per_Acreage = p.PracticePerAcre,
                            HBOM_Trade_Name = m.TradeName,
                            HS_Crop_Phase_Code = p.CropphaseCode,
                            Raw_Material_Details_Name = crop.Raw_Material_Details_Name
                        });
                    }
                }
            }

            return sowingFarmingDataForFormRequiredForGrid;
        }

        public async Task<SowingFarmingInsert> InserSowingFarmingDetails(SowingFarmingInsert sowingFarmingInsert)
        {
            using (DbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    this._context.SowingFarmingDetails.Add(sowingFarmingInsert.SowingFarmingDetails);
                    await _context.SaveChangesAsync();
                    sowingFarmingInsert.FarmingStageDetails.Sowing_No = sowingFarmingInsert.SowingFarmingDetails.Sowing_No;
                    this._context.FarmingStageDetails.Add(sowingFarmingInsert.FarmingStageDetails);
                    await _context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
            return sowingFarmingInsert;
        }
    }
}