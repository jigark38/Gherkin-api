using AutoMapper;
using GherkinWebAPI.DTO;
using GherkinWebAPI.DTO.BranchIndent;
using GherkinWebAPI.DTO.HarvestStage;
using GherkinWebAPI.DTO.MaterialInward;
using GherkinWebAPI.DTO.PackageofpracticeDto;
using GherkinWebAPI.DTO.Product_GradeDto;
using GherkinWebAPI.DTO.RMStock;
using GherkinWebAPI.Entities;
using GherkinWebAPI.Entities.BranchIndent;
using GherkinWebAPI.Entities.HarvestStage;
using GherkinWebAPI.Entities.MaterialInward;
using GherkinWebAPI.Entities.RMStock;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.Branch_Indent;
using GherkinWebAPI.Models.HarvestStage;
using GherkinWebAPI.Models.PackageOfPracticeModel;
using GherkinWebAPI.Models.RMStock;

namespace GherkinWebAPI.Automapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.MapEntities();
        }

        private void MapEntities()
        {
            this.CreateMap<RawMaterialMasterDto, RawMaterialMaster>().ReverseMap();
            this.CreateMap<RawMaterialDetailsDto, RawMaterialDetails>().ReverseMap();
            this.CreateMap<CropGroupDto, CropGroup>().ReverseMap();
            this.CreateMap<CropDto, Crop>().ReverseMap();
            this.CreateMap<SchemeDto, CropScheme>().ReverseMap();
            this.CreateMap<OrganisationOfficeLocationDetailsDto, OrganisationOfficeLocationDetails>().ReverseMap();
            this.CreateMap<PlantationScheduleDto, PlantationSchedule>().ForMember(i => i.Id, j => j.Ignore()).ReverseMap();
            this.CreateMap<RMStockLotDetailsDto, RMStockLotDetails>().ReverseMap();
         
            this.CreateMap<RMStockDetails, RMStockDetailsDto>().ReverseMap();
            this.CreateMap<RMStockLotDetailsDto, RMStockLotDetailsModel>().ReverseMap();
            this.CreateMap<ConsgineeBuyersDto, ConsigneeBuyersDetails>().ReverseMap();
            this.CreateMap<ProductVarietyDto, ProductDetails>().ReverseMap();
            this.CreateMap<GradeDto, GradeDetails>().ReverseMap();
            this.CreateMap<HarvestStageDetailDto, HarvestStageDetails>().ReverseMap();

            this.CreateMap<HarvestStageMaster, HarvestStageMasterDto>().ReverseMap();
            this.CreateMap<HarvestStageDetailDto, HarvestStageDetailsModel>().ReverseMap();            
            this.CreateMap<PackageofPracticeMasterDto, PackagePracticeMaster>().ReverseMap();
            this.CreateMap<PackageofPracticeDivisionDto, PackagePracticeDivision>().ReverseMap();

            this.CreateMap<SupplierDetailsDto, SupplierDetails>().ReverseMap();
            this.CreateMap<PackageofPracticeMaterialsDto, PackagePracticeMaterials>().ReverseMap();

            this.CreateMap<BranchIndentDetails, BranchIndentDetailsDto>().ReverseMap();
            this.CreateMap<BranchIndentMaterialDetailsDto, BranchIndentMaterialDetailsModel>().ReverseMap();
            this.CreateMap<BranchIndentMaterialDetailsDto, BranchIndentMaterialDetails>().ReverseMap();
            this.CreateMap<ProductGroupDto, ProductGroup>().ReverseMap();

            this.CreateMap<RMStockBranchQuantityDetailDTO, RMStockBranchQuantityDetails>().ReverseMap();
            this.CreateMap<RMStockBranchDto, RMStockBranchDetails>().ReverseMap();
            this.CreateMap<RMStockBranchQuantityDetailDTO, RMStockBranchQuantityModel>().ReverseMap();
            this.CreateMap<MaterialInwardDto, MaterialInwardEntity>().ReverseMap();


        }
    }
}