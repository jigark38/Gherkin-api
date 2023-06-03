using GherkinWebAPI.Core;
using GherkinWebAPI.Core.PackageofPractice;
using GherkinWebAPI.DTO.PackageofpracticeDto;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.PackageOfPracticeModel;
using GherkinWebAPI.Persistence;
using GherkinWebAPI.ValidateModel;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;

namespace GherkinWebAPI.Controllers.PackageofPractice
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PackageofPracticeController : ApiController
    {
        private readonly IPackageOfPracticeService _service;
        private readonly string controller = nameof(PackageofPracticeController);
        private readonly RepositoryContext _repositoryContext;
        private readonly IRepositoryWrapper _repository;
        private readonly ILogger<PackagePracticeMaster> _logger;
        public PackageofPracticeController(IPackageOfPracticeService service, IRepositoryWrapper repository, RepositoryContext repositoryContext)
        {
            this._service = service;
                     this._repository = repository;
            this._repositoryContext = repositoryContext;
        }

        [HttpGet]
        [Route("GetCropNameByCropGroup")]
        public async Task<IHttpActionResult> GetCropNameByCropGroup(string CropGroupCode)
        {
            try
            {
                return Ok(await _service.GetCropNameByCropGroup(CropGroupCode));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(PackageofPracticeController.GetCropNameByCropGroup)}");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetPSNOByCropNameCode")]
        public async Task<IHttpActionResult> GetPSNOByCropNameCode(string CropNameCode)
        {
            try
            {
                var res = await _service.GetPSNOByCropName(CropNameCode);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(PackageofPracticeController.GetPSNOByCropNameCode)}");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetPSNoByCropAndHBOMDivisionForFind/{cropNameCode}/{packageOfPractice}")]
        public async Task<IHttpActionResult> GetPSNoByCropAndHBOMDivisionForFind(string cropNameCode, string packageOfPractice)
        {
            try
            {
                var res = await _service.GetPSNoByCropAndHBOMDivisionForFind(cropNameCode, packageOfPractice);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(PackageofPracticeController.GetPSNoByCropAndHBOMDivisionForFind)}");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetTransCodeByCropNameCode/{cropNameCode}/{packageOfPractice}")]
        public async Task<IHttpActionResult> GetTransCodeByCropNameCode(string cropNameCode, string packageOfPractice)
        {
            try
            {
                return Ok(await _service.GetTransCodeByCropNameCode(cropNameCode, packageOfPractice));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(PackageofPracticeController.GetTransCodeByCropNameCode)}");
                return InternalServerError();
            }
        }
        [Route("GetCropPhaseCodeByPackageOfPractice/{packageOfPractice}")]
        public async Task<IHttpActionResult> GetCropPhaseCodeByPackageOfPractice(string packageOfPractice)
        {
            try
            {
                return Ok(await _service.GetCropPhaseCodeByPackageOfPractice(packageOfPractice));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(PackageofPracticeController.GetCropPhaseCodeByPackageOfPractice)}");
                return InternalServerError();
            }
        }

        [Route("GetCropStageList")]
        public async Task<IHttpActionResult> GetCropStageList(string psNO, string transCode)
        {
            try
            {
                var data=await _service.GetCropStageList(psNO, transCode);
                return Ok(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(PackageofPracticeController.GetCropStageList)}");
                return InternalServerError();
            }
        }
        [Route("GetHarvestByCropPhaseCode")]
        public async Task<IHttpActionResult> GetHarvestByCropPhaseCode(string HcropPhasecode)
        {
            try
            {
                return Ok(await _service.GetHarevstByCropPhaseCode(HcropPhasecode));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(PackageofPracticeController.GetHarvestByCropPhaseCode)}");
                return InternalServerError();
            }
        }

        [Route("GetChemicalUOM")]
        public async Task<IHttpActionResult> GetChemicalUOM()
        {
            try
            {
                var res = await _service.GetChemicalUOM();
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(PackageofPracticeController.GetChemicalUOM)}");
                return InternalServerError();
            }
        }


        [Route("GetRawmaterialsDetailsByRawGroupcode")]
        public async Task<IHttpActionResult> GetRawmaterialsDetailsByRawGroupcode(string rawgroupCode)
        {
            try
            {
                return Ok(await _service.GetRawmaterialsDetailsByRawGroupcode(rawgroupCode));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(PackageofPracticeController.GetRawmaterialsDetailsByRawGroupcode)}");
                return InternalServerError();
            }
        }
        [CheckModelForNull]
        [ValidateModelState]
        [HttpPost]
        [Route("AddPracticeDeatils")]
        public async Task<IHttpActionResult> AddPracticeDeatils([FromBody]PackageofPracticeMasterDto packageofPracticeMaster)
        {
            //using (var transaction = _repositoryContext.Database.BeginTransaction())
            //{
            try
            {
                var praticeMaster = new PackagePracticeMaster
                {
                    EntryDate = packageofPracticeMaster.EntryDate,
                    EmployeeID = packageofPracticeMaster.EmployeeID,
                    CropGroupCode = packageofPracticeMaster.CropGroupCode,
                    CropNameCode = packageofPracticeMaster.CropNameCode,
                    CreatedDate = DateTime.Now
                };
                var details = await _service.AddPracticeDeatils(praticeMaster);
                var detailsdivision = new PackagePracticeDivision();

                foreach (var pm in packageofPracticeMaster.packageofMaterials)
                {
                    
                    if (detailsdivision.PracticeNo == null || detailsdivision.CropphaseCode != pm.CropPhaseCode)
                    {
                        var packagePracticeDivisions = new PackagePracticeDivision
                        {
                            DivisionFor = packageofPracticeMaster.DivisionFor,
                            CropNameCode = packageofPracticeMaster.CropNameCode,
                            PracticePerAcre = packageofPracticeMaster.PracticePerAcre,
                            PSNO = packageofPracticeMaster.PSNO,
                            TransCode = packageofPracticeMaster.TransCode,
                            CropphaseCode = pm.CropPhaseCode,
                            PracticeEffectiveDate = pm.PracticeEffectiveDate.Value,
                            CreatedDate = DateTime.Now

                        };
                        detailsdivision = await _service.AddPracticeDivision(packagePracticeDivisions);
                    }

                    var packageeofMaterials = new PackagePracticeMaterials
                    {
                        PracticeNo = detailsdivision.PracticeNo,
                        CropNameCode = praticeMaster.CropNameCode,
                        //CropNameCode = pm.CropPhaseCode,
                        DaysApplicable = pm.DaysApplicable,
                        TradeName = pm.TradeName,
                        Chemicalvolume = pm.Chemicalvolume,
                        ChemicalUOM = pm.ChemicalUOM,
                        Sprayvolume = pm.Sprayvolume,
                        ChemicalQty = pm.ChemicalQty,
                        TragetPest = pm.TragetPest,
                        RawmaterialGroupcode = pm.RawmaterialGroupcode,
                        Raw_Material_Details_Code = pm.Raw_Material_Details_Code,
                        CreatedDate = DateTime.Now

                    };
                    var detailsmaterials = await _service.AddPracticeMaterials(packageeofMaterials);
                }
                // transaction.Commit();
                return Ok(praticeMaster);
            }

            catch (Exception ex)
            {
                // transaction.Rollback();
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(PackageofPracticeController.AddPracticeDeatils)}");
                return InternalServerError();
                // }
            }
        }


        [Route("sowingFarming/formdetail")]
        public async Task<HttpResponseMessage> GetPraticeDivision(string psNumber, string cropNameCode)
        {
            try
            {
                var data = await _service.GetPackagePracticeDivisions(psNumber, cropNameCode);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
