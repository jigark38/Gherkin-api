using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GherkinWebAPI.DTO.PackageofpracticeDto;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Models.PackageOfPracticeModel;

namespace GherkinWebAPI.Core.PackageofPractice
{
   public interface IPackageofPracticeRepository
    {
       
        Task<List<PackageofPracticeDivisionDto>> GetCropNameByCropGroup(string CropGroupCode);
        Task<List<PackageofPracticeDivisionDto>> GetPSNOByCropName(string CropNamecode);
        Task<List<PackageofPracticeDivisionDto>> GetTransCodeByCropNameCode(string CropNameCode, string packageOfPractice);

        Task<List<PackageofPracticeDivisionDto>> GetCropPhaseCodeByPackageOfPractice(string packageOfPractice);
        Task<List<PackageofPracticeMaterialsDto>> GetCropStageList(string psNO, string transCode);
        Task<PackageofPracticeDivisionDto> GetHarevstByCropPhaseCode(string hcropPhaseCode);
        Task<PackagePracticeMaster> AddPracticeDeatils(PackagePracticeMaster packageofpratice);

        Task<PackagePracticeDivision> AddPracticeDivision(PackagePracticeDivision packageofpratice);

        Task<PackagePracticeMaterials> AddPracticeMaterials(PackagePracticeMaterials practiceNo);

        Task<List<RawMaterialDetailsDto>> GetRawmaterialsDetailsByRawGroupcode(string rawGroupCode);

        Task<List<PackageofPracticeMaterialsDto>> GetChemicalUOM();

        Task<List<PackagePracticeDivision>> GetPackagePracticeDivisions(string psNumber, string cropNameCode);

        Task<List<PackageofPracticeDivisionDto>> GetPSNoByCropAndHBOMDivisionForFind(string cropNameCode, string packageOfPractice);
    }
}
