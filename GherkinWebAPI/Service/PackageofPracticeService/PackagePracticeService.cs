using GherkinWebAPI.Core;
using GherkinWebAPI.Core.PackageofPractice;
using GherkinWebAPI.DTO;
using GherkinWebAPI.DTO.PackageofpracticeDto;
using GherkinWebAPI.Models.PackageOfPracticeModel;
using GherkinWebAPI.Request;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Service.PackageofPracticeService
{
    /// <summary>
    /// Defines the <see cref="PackagePracticeService" />
    /// </summary>
    public class PackagePracticeService : IPackageOfPracticeService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public IPackageofPracticeRepository _repository { get; }


        /// <summary>
        /// Initializes a new instance of the <see cref="ConsgineeBuyersService"/> class.
        /// </summary>
        /// <param name="PackageofPracticeRepository">The PackageofPracticeRepository<see cref="IPackageofPracticeRepository"/></param>
        public PackagePracticeService(IRepositoryWrapper repositoryWrapper, IPackageofPracticeRepository repository)
        {
            _repositoryWrapper = repositoryWrapper;
            _repository = repository;
        }


        public async Task<List<PackageofPracticeDivisionDto>> GetCropNameByCropGroup(string CropGroupcode)
        {
            return await _repository.GetCropNameByCropGroup(CropGroupcode);
        }

        public async Task<List<PackageofPracticeDivisionDto>> GetPSNOByCropName(string CropNamecode)
        {
            return await _repository.GetPSNOByCropName(CropNamecode);
        }
        public async Task<List<PackageofPracticeDivisionDto>> GetTransCodeByCropNameCode(string cropNameCode, string packageOfPractice)
        {
            return await _repository.GetTransCodeByCropNameCode(cropNameCode, packageOfPractice);
        }

        public async Task<List<PackageofPracticeDivisionDto>> GetCropPhaseCodeByPackageOfPractice(string packageOfPractice)
        {
            return await _repository.GetCropPhaseCodeByPackageOfPractice(packageOfPractice);
        }
        public async Task<List<PackageofPracticeMaterialsDto>> GetCropStageList(string psNO, string transCode)
        {
            return await _repository.GetCropStageList(psNO, transCode);
        }
        public async Task<PackagePracticeMaster> AddPracticeDeatils(PackagePracticeMaster packageofPractice)
        {
            return await _repositoryWrapper.packageofPracticeRepository.AddPracticeDeatils(packageofPractice);
        }
        
        public async Task<PackagePracticeDivision> AddPracticeDivision(PackagePracticeDivision packageofdivison)
        {
            return await _repositoryWrapper.packageofPracticeRepository.AddPracticeDivision(packageofdivison);
        }

        public async Task<PackagePracticeMaterials> AddPracticeMaterials(PackagePracticeMaterials practiceMaterials)
        {
            return await _repositoryWrapper.packageofPracticeRepository.AddPracticeMaterials(practiceMaterials);
        }

        public async Task<PackageofPracticeDivisionDto> GetHarevstByCropPhaseCode(string hcropPhaseCode)
        {
            return await _repository.GetHarevstByCropPhaseCode(hcropPhaseCode);

        }

        public async Task<List<RawMaterialDetailsDto>> GetRawmaterialsDetailsByRawGroupcode(string rawGroupCode)
        {
            return await _repository.GetRawmaterialsDetailsByRawGroupcode(rawGroupCode);
        }

        public async Task<List<PackageofPracticeMaterialsDto>> GetChemicalUOM()
        {
            return await _repository.GetChemicalUOM();
        }

        public async Task<List<PackagePracticeDivision>> GetPackagePracticeDivisions(string psNumber, string cropNameCode)
        {
            return await _repositoryWrapper.packageofPracticeRepository.GetPackagePracticeDivisions(psNumber, cropNameCode);
        }

        public async Task<List<PackageofPracticeDivisionDto>> GetPSNoByCropAndHBOMDivisionForFind(string cropNameCode, string packageOfPractice)
        {
            return await _repositoryWrapper.packageofPracticeRepository.GetPSNoByCropAndHBOMDivisionForFind(cropNameCode, packageOfPractice);
        }
    }
}