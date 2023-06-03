using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using GherkinWebAPI.Models.PackageOfPracticeModel;
using GherkinWebAPI.DTO.PackageofpracticeDto;
using GherkinWebAPI.Models;
using GherkinWebAPI.DTO;

namespace GherkinWebAPI.Core.PackageofPractice
{
   public interface IPackageOfPracticeService
    {
        // Task<PackageofPracticeMasterDto> CreatePracticeMaster(PackageofPracticeMasterDto practiceMaster);

        Task<List<PackageofPracticeDivisionDto>> GetCropNameByCropGroup(string CropGroupCode);
        Task<List<PackageofPracticeDivisionDto>> GetPSNOByCropName(string CropNameCode);
        Task<List<PackageofPracticeDivisionDto>> GetTransCodeByCropNameCode(string cropNameCode, string packageOfPractice);

        Task<List<PackageofPracticeDivisionDto>> GetCropPhaseCodeByPackageOfPractice(string packageOfPractice);

        Task<List<RawMaterialDetailsDto>> GetRawmaterialsDetailsByRawGroupcode(string rawGroupCode);

        Task<PackageofPracticeDivisionDto> GetHarevstByCropPhaseCode(string hcropPhaseCode);
        Task<List<PackageofPracticeMaterialsDto>> GetCropStageList(string psNO, string transCode);
        Task<PackagePracticeMaster> AddPracticeDeatils(PackagePracticeMaster packageofpratice);

        Task<PackagePracticeDivision> AddPracticeDivision(PackagePracticeDivision packageofpratice);

        Task<PackagePracticeMaterials> AddPracticeMaterials(PackagePracticeMaterials practiceNo);
        Task<List<PackageofPracticeMaterialsDto>> GetChemicalUOM();

        Task<List<PackagePracticeDivision>> GetPackagePracticeDivisions(string psNumber, string cropNameCode);

        Task<List<PackageofPracticeDivisionDto>> GetPSNoByCropAndHBOMDivisionForFind(string cropNameCode, string packageOfPractice);
    }
}
