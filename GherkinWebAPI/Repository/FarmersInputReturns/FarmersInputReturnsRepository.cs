using GherkinWebAPI.Core.FarmersInputReturns;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.FarmersInputReturns;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace GherkinWebAPI.Repository.FarmersInputReturns
{
	public class FarmersInputReturnsRepository : RepositoryBase<FarmersAgreementDetail>, IFarmersInputReturnsRepository
	{
		private RepositoryContext _context;
		public FarmersInputReturnsRepository(RepositoryContext repositoryContext) : base(repositoryContext)
		{
			_context = repositoryContext;
		}

		public async Task<FarmersInputsMaterialMaster> CreateInputReturnsFromFarmersMaster(FarmersInputsMaterialMaster farmersInputsMaterialMaster)
		{
			// generate FIMReturnVoucherNo
			var count = await _context.FarmersInputsMaterialMaster.CountAsync();
			var areaCode = (await _context.Areas.Where(x => x.Area_ID == farmersInputsMaterialMaster.AreaID).FirstOrDefaultAsync()).Area_Code;
			farmersInputsMaterialMaster.FIMReturnVoucherNo = areaCode + " / " + "MIR" + " / " + (count + 1) + " / " + farmersInputsMaterialMaster.FMIRDate.ToShortDateString();

			farmersInputsMaterialMaster = _context.FarmersInputsMaterialMaster.Add(farmersInputsMaterialMaster);
			await _context.SaveChangesAsync();
			return farmersInputsMaterialMaster;
		}

		public async Task<IEnumerable<FarmersInputsMaterialDetail>> CreateInputReturnsFromFarmersDetail(IEnumerable<FarmersInputsMaterialDetail> farmersInputsMaterialDetailList)
		{
			farmersInputsMaterialDetailList = _context.FarmersInputsMaterialDetail.AddRange(farmersInputsMaterialDetailList);
			await _context.SaveChangesAsync();
			return farmersInputsMaterialDetailList;
		}

		public async Task<FieldStaffCropGroupSeason> GetFieldStaffCropGroupSeasonByAreaId(string areaId)
		{
			FieldStaffCropGroupSeason fieldStaffCropGroupSeason = new FieldStaffCropGroupSeason();

			var fieldStaffList = await _context.FieldStaffDetails.Where(x => x.Area_ID == areaId && x.StaffType.ToUpper() == "FIELD STAFF" ).OrderByDescending(x => x.EffectiveDate).Select(x => x.Employee_ID).ToListAsync();
			fieldStaffCropGroupSeason.FieldStaffList = await _context.Employees.Where(x => fieldStaffList.Contains(x.employeeId)).ToListAsync();

			var cropGroupCodeList = await _context.FarmersAgreementDetails.Where(x => x.Area_ID == areaId).Select(x => x.Crop_Group_Code).ToListAsync();
			fieldStaffCropGroupSeason.CropGroupList = await _context.CropGroups.Where(x => cropGroupCodeList.Contains(x.CropGroupCode)).ToListAsync();

			var psNumberList = await _context.FarmersAgreementDetails.Where(x => x.Area_ID == areaId).Select(x => x.PS_Number).ToListAsync();
			var platationSchedules = await _context.PlantationSchedules.Where(x => psNumberList.Contains(x.PsNumber)).ToListAsync();
			fieldStaffCropGroupSeason.SeasonFromTo = platationSchedules.Select(x => new SeasonFromTo()
			{
				PSNumber = x.PsNumber,
				SeasonFromToDate = x.FromDate.ToShortDateString() + " - " + x.ToDate.ToShortDateString()
			}).ToList();

			return fieldStaffCropGroupSeason;
		}

		public async Task<IEnumerable<FarmersInputsMaterialDetail>> UpdateInputReturnsFromFarmersDetail(IEnumerable<FarmersInputsMaterialDetail> farmersInputsMaterialDetailList)
		{
			_context.FarmersInputsMaterialDetail.AddOrUpdate(farmersInputsMaterialDetailList.ToArray());
			await _context.SaveChangesAsync();
			return farmersInputsMaterialDetailList;
		}

		public async Task<FarmersInputsMaterialMaster> UpdateInputReturnsFromFarmersMaster(FarmersInputsMaterialMaster farmersInputsMaterialMaster)
		{
			_context.FarmersInputsMaterialMaster.AddOrUpdate(farmersInputsMaterialMaster);
			await _context.SaveChangesAsync();
			return farmersInputsMaterialMaster;
		}

		public async Task<List<FarmersInputsMaterialDetail>> GetFarmersInputsMaterialDetail(string psNumber, string farmerCode, string cropNameCode, string areaId, int fIMReturnNo)
		{
			List<FarmersInputsMaterialDetail> materialDetails = new List<FarmersInputsMaterialDetail>();

			if (fIMReturnNo == 0)
			{
				// searh by ps number and farmer code
				var farmersInputsConsumptionDetailsList = await _context.FarmersInputConsumptionDetails.Where(x => x.PSNumber == psNumber && x.FarmerCode == farmerCode && x.CropNameCode == cropNameCode && x.AreaID == areaId).ToListAsync();
				var mIfConsumptionNoList = farmersInputsConsumptionDetailsList.Select(x => x.MIFConsumptionNo).ToList();
				var issueDetails = await _context.FarmersMaterialIssueDetails.Where(x => mIfConsumptionNoList.Contains(x.MIFConsumptionNo)).ToListAsync();
				materialDetails = issueDetails.GroupBy(x => x.RawMaterialDetailsCode)
					.Select(x => new FarmersInputsMaterialDetail()
					{
						RawMaterialGroupCode = x.FirstOrDefault().RawMaterialGroupCode,
						RawMaterialDetailsCode = x.FirstOrDefault().RawMaterialDetailsCode,
						MIFConsumptionVoucherNo = farmersInputsConsumptionDetailsList.FirstOrDefault().MIFConsumptionVoucherNo
					}).ToList();

				// join with issued table (group by and sum)
				// group by material details code and aggregate issued qty
				var materialDetailAndIssuedQtyList = await _context.FarmersMaterialIssueDetails.Where(x => mIfConsumptionNoList.Contains(x.MIFConsumptionNo))
					.GroupBy(x => x.RawMaterialDetailsCode).Select(x => new { MaterialDetailCode = x.Key, IssuedQty = x.Sum(y => y.FarmersMaterialIssuedQty) }).ToListAsync();
				materialDetails.ForEach(x =>
				{
					x.FarmersMaterialIssuedQty = materialDetailAndIssuedQtyList.Where(y => y.MaterialDetailCode == x.RawMaterialDetailsCode).FirstOrDefault().IssuedQty;
				});

				// check if already returned anything (deduct)
				// check and deduct if already returned some qty
				var existingReturnedItems = await _context.FarmersInputsMaterialMaster.Where(x => x.PSNumber == psNumber && x.FarmerCode == farmerCode && x.CropNameCode == cropNameCode && x.AreaID == areaId).Select(x => x.FIMReturnNo).ToListAsync();
				var existingReturnedItemDetails = await _context.FarmersInputsMaterialDetail.Where(x => existingReturnedItems.Contains(x.FIMReturnNo))
					.GroupBy(x => x.RawMaterialDetailsCode).Select(x => new { MaterialDetailCode = x.Key, ReturnedQty = x.Sum(y => y.FarmersMaterialReturnQty) }).ToListAsync();
				if (existingReturnedItemDetails.Count > 0)
				{
					foreach(var md in materialDetails)
					{
						foreach(var erid in existingReturnedItemDetails)
						{
							if(md.RawMaterialDetailsCode == erid.MaterialDetailCode)
							{
								md.FarmersMaterialIssuedQty = md.FarmersMaterialIssuedQty - erid.ReturnedQty;
							}
						}
					}


					//materialDetails.ForEach(x =>
					//{
					//	x.FarmersMaterialIssuedQty -= existingReturnedItemDetails.Where(y => y.MaterialDetailCode == x.RawMaterialDetailsCode).FirstOrDefault().ReturnedQty;
					//});
				}
			}
			else
			{
				var existingReturnedItemsMasterList = await _context.FarmersInputsMaterialMaster.Where(x => x.FIMReturnNo == fIMReturnNo).ToListAsync();
				var fIMReturnNoList = existingReturnedItemsMasterList.Select(x => x.FIMReturnNo).ToList();
				materialDetails = await _context.FarmersInputsMaterialDetail.Where(x => fIMReturnNoList.Contains(x.FIMReturnNo)).ToListAsync();
				if (existingReturnedItemsMasterList.Count > 0)
				{
					materialDetails.ForEach(x =>
					{
						x.ReturnDate = existingReturnedItemsMasterList.Where(y => y.FIMReturnNo == x.FIMReturnNo).FirstOrDefault().FMIRDate;
					});
				}

				var farmersInputsConsumptionDetailsList = await _context.FarmersInputConsumptionDetails.Where(x => x.PSNumber == psNumber && x.FarmerCode == farmerCode && x.CropNameCode == cropNameCode && x.AreaID == areaId).ToListAsync();
				var mIfConsumptionNoList = farmersInputsConsumptionDetailsList.Select(x => x.MIFConsumptionNo).ToList();
				var materialDetailAndIssuedQtyList = await _context.FarmersMaterialIssueDetails.Where(x => mIfConsumptionNoList.Contains(x.MIFConsumptionNo))
					.GroupBy(x => x.RawMaterialDetailsCode).Select(x => new { MaterialDetailCode = x.Key, IssuedQty = x.Sum(y => y.FarmersMaterialIssuedQty) }).ToListAsync();
				if (materialDetailAndIssuedQtyList.Count > 0)
				{
					materialDetails.ForEach(x =>
					{
						x.FarmersMaterialIssuedQty = materialDetailAndIssuedQtyList.Where(y => y.MaterialDetailCode == x.RawMaterialDetailsCode).FirstOrDefault().IssuedQty;
					});
				}
			}

			// get material group name from group code
			var materialGroupCodeList = materialDetails.Select(x => x.RawMaterialGroupCode).ToList();
			var materialGroupList = await _context.RawMaterialGroupMaster.Where(x => materialGroupCodeList.Contains(x.Raw_Material_Group_Code)).ToListAsync();
			materialDetails.ForEach(x =>
			{
				x.RawMaterialGroup = materialGroupList.Where(y => y.Raw_Material_Group_Code == x.RawMaterialGroupCode).FirstOrDefault().Raw_Material_Group;
			});

			// get material detail name and uom from detail code
			var materialDetailCodeList = materialDetails.Select(x => x.RawMaterialDetailsCode).ToList();
			var materialDetailList = await _context.RawMaterialDetails.Where(x => materialDetailCodeList.Contains(x.Raw_Material_Details_Code)).ToListAsync();
			materialDetails.ForEach(x =>
			{
				var materialDetail = materialDetailList.Where(y => y.Raw_Material_Details_Code == x.RawMaterialDetailsCode).FirstOrDefault();
				x.RawMaterialDetailsName = materialDetail.Raw_Material_Details_Name;
				x.RawMaterialDetailsUOM = materialDetail.Raw_Material_UOM;
			});

			return materialDetails;
		}

		public async Task<FarmersInputsMaterialMaster> GetFarmersInputsMaterialMaster(string psNumber, string farmerCode, string cropNameCode, string areaId)
		{
			FarmersInputsMaterialMaster inputMaster = new FarmersInputsMaterialMaster();
			inputMaster = await _context.FarmersInputsMaterialMaster.Where(x => x.PSNumber == psNumber && x.FarmerCode == farmerCode && x.CropNameCode == cropNameCode && x.AreaID == areaId).FirstOrDefaultAsync();

			if (inputMaster != null)
			{
				inputMaster.FieldStaffAreaName = (await _context.Areas.Where(x => x.Area_ID == inputMaster.AreaID).FirstOrDefaultAsync()).Area_Name;
				inputMaster.FieldStaffEmployeeName = (await _context.Employees.Where(x => x.employeeId == inputMaster.EmployeeID).FirstOrDefaultAsync()).employeeName;
				inputMaster.CropGroupName = (await _context.CropGroups.Where(x => x.CropGroupCode == inputMaster.CropGroupCode).FirstOrDefaultAsync()).Name;
				inputMaster.CropName = (await _context.Crops.Where(x => x.CropCode == inputMaster.CropNameCode).FirstOrDefaultAsync()).Name;

				var seasonFromTo = await _context.PlantationSchedules.Where(x => x.PsNumber == inputMaster.PSNumber).FirstOrDefaultAsync();
				inputMaster.SeasonFromTo = seasonFromTo.FromDate.ToShortDateString() + " - " + seasonFromTo.ToDate.ToShortDateString();

				var farmer = await _context.Farmers.Where(x => x.Farmer_Code == inputMaster.FarmerCode).FirstOrDefaultAsync();
				inputMaster.FarmerName = farmer.FarmerName;
				inputMaster.FarmerAltContactPerson = farmer.AlternativeContactPerson;
				inputMaster.VillageCode = farmer.Village_Code;
				inputMaster.VillageName = (await _context.Villages.Where(x => x.Village_Code == farmer.Village_Code).FirstOrDefaultAsync()).Village_Name;

				if (inputMaster.OrgOfficeNo > 0)
				{
					inputMaster.OrgOfficeName = (await _context.OrganisationOfficeLocationDetails.Where(x => x.Org_Office_No == inputMaster.OrgOfficeNo).FirstOrDefaultAsync()).Org_Office_Name;
				}
				else
				{
					if (inputMaster.StockReturnStatus.Length == 0)
					{
						inputMaster.OrgOfficeName = "Area Branch";
					}
					else
					{
						inputMaster.OrgOfficeName = "With Field Staff";
					}
				}
			}
			return inputMaster;
		}

		public async Task<bool> CheckIfFarmerAlreadyReturnedItems(string psNumber, string farmerCode, string cropNameCode, string areaId)
		{
			return await _context.FarmersInputsMaterialMaster.CountAsync(x => x.PSNumber == psNumber && x.FarmerCode == farmerCode && x.CropNameCode == cropNameCode && x.AreaID == areaId) > 0;
		}
	}
}