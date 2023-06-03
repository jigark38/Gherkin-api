using AutoMapper;
using GherkinWebAPI.Core.HarvestStage;
using GherkinWebAPI.DTO.HarvestStage;
using GherkinWebAPI.Entities.HarvestStage;
using GherkinWebAPI.Persistence;
using GherkinWebAPI.Request.HarvestStage;
using GherkinWebAPI.Response.HarvestStage;
using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using GherkinWebAPI.Models.HarvestStage;
using System.Collections.Generic;

namespace GherkinWebAPI.Repository.HarvestStage
{
	public class HarvestStageRepository : RepositoryBase<HarvestStageMaster>, IHarvestStageRepository
	{
		private RepositoryContext _context;

		public HarvestStageRepository(RepositoryContext repositoryContext)
			: base(repositoryContext)
		{
			this._context = repositoryContext;
		}

		public async Task<List<EffectiveDateForHarvestDetails>> GetEffectiveDateList(string cropNameCode)
		{
			List<EffectiveDateForHarvestDetails> effectiveDates = new List<EffectiveDateForHarvestDetails>();

			var entityList = await _context.HarvestStageMaster.Where(x => x.Crop_Name_Code == cropNameCode).OrderByDescending(x => x.HS_Effective_Date).ToListAsync();

			effectiveDates = entityList.Select(x => new EffectiveDateForHarvestDetails()
			{
				EffectiveDate = x.HS_Effective_Date.ToString("dd-MMM-yyyy"),
				HSTransactionCode = x.HS_Transaction_Code
			}).ToList();

			return effectiveDates;
		}

		public async Task<HarvestStageInsertRequest> GetHarvestStageDetails(string hsTransactionCode)
		{
			HarvestStageInsertRequest harvestStage = new HarvestStageInsertRequest();

			var harvestStageMaster = await _context.HarvestStageMaster.Where(x => x.HS_Transaction_Code == hsTransactionCode).FirstOrDefaultAsync();
			if (harvestStageMaster != null)
			{
				harvestStage.HarvestStageMaster = Mapper.Map<HarvestStageMasterModel>(harvestStageMaster);

				var harvestStageDetailList = await _context.HarvestStageDetails.Where(x => x.HS_Transaction_Code == harvestStageMaster.HS_Transaction_Code).ToListAsync();
				harvestStage.HarvestStageDetails = Mapper.Map<List<HarvestStageDetailsModel>>(harvestStageDetailList);
			}

			return harvestStage;
		}

		public async Task<HarvestStageResponse> InsertHarvestStages(HarvestStageInsertRequest harvestStageInsertRequest)
		{
			HarvestStageResponse harvestStageResponse = new HarvestStageResponse();

			using (DbContextTransaction transaction = _context.Database.BeginTransaction())
			{
				try
				{
					var masterId = await this._context.Database.SqlQuery<int>(
						"USP_InsertUpdateHarvestStageMaster @ID, @HS_Entry_Date, @HS_Entered_Employee_ID_By, @Crop_Group_Code, @Crop_Name_Code, @HS_Effective_Date, @HBOM_Division_For",
						new SqlParameter("ID", harvestStageInsertRequest.HarvestStageMaster.ID),
						new SqlParameter("HS_Entry_Date", harvestStageInsertRequest.HarvestStageMaster.HS_Entry_Date),
						new SqlParameter("HS_Entered_Employee_ID_By", harvestStageInsertRequest.HarvestStageMaster.HS_Entered_Employee_ID_By),
						new SqlParameter("Crop_Group_Code", harvestStageInsertRequest.HarvestStageMaster.Crop_Group_Code),
						new SqlParameter("Crop_Name_Code", harvestStageInsertRequest.HarvestStageMaster.Crop_Name_Code),
						new SqlParameter("HS_Effective_Date", harvestStageInsertRequest.HarvestStageMaster.HS_Effective_Date),
						new SqlParameter("HBOM_Division_For", harvestStageInsertRequest.HarvestStageMaster.HBOMDivisionFor)
					).FirstAsync();

					var masterEntity = await this._context.HarvestStageMaster.FirstAsync(x => x.ID == masterId);

					harvestStageResponse.HarvestStageMaster = Mapper.Map<HarvestStageMasterDto>(masterEntity);
					foreach (var lotDetail in harvestStageInsertRequest.HarvestStageDetails)
					{
						var detailId = await this._context.Database.SqlQuery<int>(
							"USP_InsertUpdateHarvestStageDetails @ID, @HS_Transaction_Code, @HS_Crop_Phase_Name, @HS_Days_After_Sowing_From, @HS_Days_After_Sowing_To, @HS_Harvest_Details",
							new SqlParameter("ID", lotDetail.ID),
							new SqlParameter("HS_Transaction_Code", masterEntity.HS_Transaction_Code),
							new SqlParameter("HS_Crop_Phase_Name", lotDetail.HS_Crop_Phase_Name),
							new SqlParameter("HS_Days_After_Sowing_From", lotDetail.HS_Days_After_Sowing_From),
							new SqlParameter("HS_Days_After_Sowing_To", lotDetail.HS_Days_After_Sowing_To),
							new SqlParameter("HS_Harvest_Details", lotDetail.HS_Harvest_Details)
						).FirstAsync();

						var detailEntity = await this._context.HarvestStageDetails.FirstAsync(x => x.ID == detailId);
						var lotDetailDto = Mapper.Map<HarvestStageDetailDto>(detailEntity);
						harvestStageResponse.HarvestStageDetails.Add(lotDetailDto);
					}

					transaction.Commit();
				}
				catch (Exception ex)
				{
					transaction.Rollback();
				}
			}

			return harvestStageResponse;
		}
	}
}