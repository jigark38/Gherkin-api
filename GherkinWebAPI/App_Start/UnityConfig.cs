using GherkinWebAPI.Core;
using GherkinWebAPI.Repository;
using GherkinWebAPI.Service;
using System.Web.Http;
using GherkinWebAPI.Controllers.PlantationHarvest;
using Microsoft.Extensions.Logging;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;
using GherkinWebAPI.Core.Harvest_Area_Village;
using GherkinWebAPI.Core.Villages;
using GherkinWebAPI.Core.Harvest_Area;
using GherkinWebAPI.Core.Contractor;
using GherkinWebAPI.Service.Contractor;
using GherkinWebAPI.Repository.Contractor;
using GherkinWebAPI.Core.HarvestStage;
using GherkinWebAPI.Repository.HarvestStage;
using GherkinWebAPI.Core.PackageofPractice;
using GherkinWebAPI.Service.PackageofPracticeService;
using GherkinWebAPI.Repository.HarvestAreaAndVillage;
using GherkinWebAPI.Service.HarvestAreaAndVillage;
using GherkinWebAPI.Service.OverseasService;
using GherkinWebAPI.Repository.OverseasRepository;
using GherkinWebAPI.Core.Currencys;
using GherkinWebAPI.Repository.PackageOfPracticesRepository;
using GherkinWebAPI.Core.BranchIndent;
using GherkinWebAPI.Service.BranchIndent;
using GherkinWebAPI.Repository.BranchIndent;
using GherkinWebAPI.Repository.Accounts_Master;
using GherkinWebAPI.Core.Accounts_Master;
using GherkinWebAPI.Service.AccountsMaster;
using GherkinWebAPI.Core.PurchaseMgmt;
using GherkinWebAPI.Service.PurchaseMgmt;
using GherkinWebAPI.Repository.PurchaseMgmt;
using GherkinWebAPI.Service.Accounts_Master;
using GherkinWebAPI.Repository.MaterialInward;
using GherkinWebAPI.Core.MaterialInward;
using GherkinWebAPI.Service.MaterialInward;
using GherkinWebAPI.Core.GRPAndroid.ProdProcessBOM;
using GherkinWebAPI.Repository.GRPAndroid.ProdProcessBOM;
using GherkinWebAPI.Service.GRPAndroid.ProdProcessBOM;
using GherkinWebAPI.Core.DailyInputAndFeedingDetails;
using GherkinWebAPI.Repository.DailyInputAndFeedingDetails;
using GherkinWebAPI.Service.DailyInputAndFeedingDetails;
using GherkinWebAPI.Core.GoodsReceiptNote;
using GherkinWebAPI.Repository.GoodsReceiptNote;
using GherkinWebAPI.Service.GoodsReceiptNote;
using GherkinWebAPI.Core.Login;
using GherkinWebAPI.Service.Login;
using GherkinWebAPI.Repository.Login;
using GherkinWebAPI.Repository.Mandals;
using GherkinWebAPI.Core.Mandals;
using GherkinWebAPI.Service.Mandals;
using GherkinWebAPI.Core.AreaMaterialReceivedDetails;
using GherkinWebAPI.Core.FeedInputTransfer;
using GherkinWebAPI.Repository.FeedInputTransfer;
using GherkinWebAPI.Service.FeedInputTransfer;
using GherkinWebAPI.Core.SowingFarming;
using GherkinWebAPI.Core.HarvestDetails;
using GherkinWebAPI.Service.HarvestDetails;
using GherkinWebAPI.Repository.HarvestDetails;
using GherkinWebAPI.Repository.GRNAndMaterialClassification;
using GherkinWebAPI.Service.GRNAndMaterialClassification;
using GherkinWebAPI.Core.GRNAndMaterialClassification;
using GherkinWebAPI.Core.GreenReption;
using GherkinWebAPI.Repository.GreenReception;
using GherkinWebAPI.Service.GreenReception;
using GherkinWebAPI.Repository.SowingFarming;
using GherkinWebAPI.Core.BatchProduction;
using GherkinWebAPI.Repository.BatchProduction;
using GherkinWebAPI.Service.BatchProduction;
using GherkinWebAPI.Service.MediaBatchDetails;
using GherkinWebAPI.Core.MediaBatchDetails;
using GherkinWebAPI.Repository.MediaBatchDetails;
using GherkinWebAPI.Repository.CullingDetails;
using GherkinWebAPI.Core.CullingDetails;
using GherkinWebAPI.Service.CullingDetails;
using GherkinWebAPI.Core.Reports.DepAndDes;
using GherkinWebAPI.Repository.Reports.DepAndDes;
using GherkinWebAPI.Core.Reports.FarmerWiseSummary;
using GherkinWebAPI.Repository.Reports.FarmerWiseSummary;
using GherkinWebAPI.Core.ProfessionalTaxRates;
using GherkinWebAPI.Repository.ProfessionalTaxRates;
using GherkinWebAPI.Service.ProfessionalTaxRates;

using GherkinWebAPI.Core.ShiftDetails;
using GherkinWebAPI.Repository.ShiftDetails;
using GherkinWebAPI.Service.ShiftDetails;

using GherkinWebAPI.Core.ProvidentFund;
using GherkinWebAPI.Repository.ProvidentFund;
using GherkinWebAPI.Service.ProvidentFund;
using GherkinWebAPI.Core.EmployeeBankDetails;
using GherkinWebAPI.Service.EmployeeBankDetail;
using GherkinWebAPI.Core.GreensTransportVehicleSchedules;
using GherkinWebAPI.Repository.GreensTransportVehicleSchedules;
using GherkinWebAPI.Service.GreensTransportVehicleSchedules;
using GherkinWebAPI.Repository.DriverDetailRepository;
using GherkinWebAPI.Core.DriverDetail;
using GherkinWebAPI.Service.DriverDetail;
using GherkinWebAPI.Core.AttendanceDetails;
using GherkinWebAPI.Repository.AttendanceDetails;
using GherkinWebAPI.Service.AttendanceDetails;
using GherkinWebAPI.Core.DriverDocuments;
using GherkinWebAPI.Repository.DriverDocumentRepository;
using GherkinWebAPI.Service.DriverDocument;

using GherkinWebAPI.Core.TransportVehicleManagement;
using GherkinWebAPI.Repository.TransportVehicleManagement;
using GherkinWebAPI.Service.TransportVehicleManagement;
using GherkinWebAPI.Core.LoansAndAdvancesDetails;
using GherkinWebAPI.Repository.LoansAndAdvancesDetails;
using GherkinWebAPI.Service.LoansAndAdvancesDetails;
using GherkinWebAPI.Service.FarmersInputReturns;
using GherkinWebAPI.Core.FarmersInputReturns;
using GherkinWebAPI.Core.DailyHarvestDetails;
using GherkinWebAPI.Repository.DailyHarvestDetails;
using GherkinWebAPI.Service.DailyHarvestDetails;
using GherkinWebAPI.Core.InputToFieldStaff;
using GherkinWebAPI.Repository.InputToFieldStaff;
using GherkinWebAPI.Service.InputToFieldStaff;
using GherkinWebAPI.Repository.Reports.DialyGreensReceiving;
using GherkinWebAPI.Core.Reports.DialyGreensReceiving;
using GherkinWebAPI.Core.StoresMasterDetails;
using GherkinWebAPI.Service.StoresMasterDetails;
using GherkinWebAPI.Service.ManualAttendence;
using GherkinWebAPI.Core.ManualAttendence;
using GherkinWebAPI.Repository.ManualAttendence;
using GherkinWebAPI.Service.MaterialIndentByDepartment;
using GherkinWebAPI.Core.MaterialIndentByDepartment;
using GherkinWebAPI.Core.BuyingStaffDetails;
using GherkinWebAPI.Repository.BuyingStaffDetails;
using GherkinWebAPI.Service.BuyingStaffDetails;
using GherkinWebAPI.Repository.Reports.GreensReceiptsSummary;
using GherkinWebAPI.Core.Reports.GreensReceiptsSummary;
using GherkinWebAPI.Repository.GreensAgentSupplierDetails;
using GherkinWebAPI.Core.GreensAgentSupplierDetails;
using GherkinWebAPI.Service.GreensAgentSupplierDetails;
using GherkinWebAPI.Repository.Reports.DialyAttendance;
using GherkinWebAPI.Core.Reports.DailyAttendance;
using GherkinWebAPI.Core.FarmerAccountDetailsFinalization;
using GherkinWebAPI.Service.FarmerAccountDetailsFinalization;
using GherkinWebAPI.Repository.FarmerAccountDetailsFinalization;
using GherkinWebAPI.Core.AgentsGreensRecWeighment;
using GherkinWebAPI.Repository.AgentsGreensRecWeighment;
using GherkinWebAPI.Service.AgentsGreensRecWeighment;
using GherkinWebAPI.Repository.Reports.InWardDailyReport;
using GherkinWebAPI.Core.Reports.InWardDailyReport;
using GherkinWebAPI.Service.MediaProcessDetail;
using GherkinWebAPI.Core.MediaProcessDetail;
using GherkinWebAPI.Repository.MediaProcessDetail;
using GherkinWebAPI.Core.ScheduleDetail;
using GherkinWebAPI.Service.ScheduleDetail;
using GherkinWebAPI.Repository.ScheduleDetail;
using GherkinWebAPI.Repository.Reports.MonthlyAttendance;
using GherkinWebAPI.Core.Reports.MonthlyAttendance;

namespace GherkinWebAPI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            // register all your components with the container here
            var container = new UnityContainer();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
            container.RegisterType<IAccountManagementRepository, AccountManagementRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IAccountManagementService, AccountManagementService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryWrapper, RepositoryWrapper>(new HierarchicalLifetimeManager());
            container.RegisterType<IPlantationHarvestRepository, PlantationHarvestRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IPlantationHarvestService, PlantationHarvestService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRawMaterialService, RawMaterialService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRawMaterialRepository, RawMaterialRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ICropService, CropService>(new HierarchicalLifetimeManager());
            container.RegisterType<ICropRepository, CropRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IAreaService, AreaService>(new HierarchicalLifetimeManager());
            container.RegisterType<IAreaRepository, AreaRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IDepartmentService, DepartmentService>(new HierarchicalLifetimeManager());
            container.RegisterType<IDepartmentRepository, DepartmentRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ISubDepartmentRepository, SubDepartmentRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ISubDepartmentService, SubdepartmentService>(new HierarchicalLifetimeManager());
            container.RegisterType<IDesignationService, DesignationService>(new HierarchicalLifetimeManager());
            container.RegisterType<IDesignationRepository, IDesignationRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IEmployeeService, EmployeeService>(new HierarchicalLifetimeManager());
            container.RegisterType<IEmployeeRepository, EmployeeRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IFieldStaffDetailsRepository, FieldStaffDetailsRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IFieldStaffDetailsService, FieldStaffDetailsService>(new HierarchicalLifetimeManager());
            container.RegisterType<ISupplierDetailsRepository, SupplierDetailsRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ISupplierDetailsService, SupplierDetailsService>(new HierarchicalLifetimeManager());
            container.RegisterType<IHarvestAreaVillageRepository, HarvestAreaVillageRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IHarvestAreaVillageService, HarvestAreaVillageService>(new HierarchicalLifetimeManager());
            container.RegisterType<IVillageRepository, VillageRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IVillageService, VillageService>(new HierarchicalLifetimeManager());
            container.RegisterType<IHarvestAreaRepository, HarvestAreaRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IHarvestAreaService, HarvestAreaService>(new HierarchicalLifetimeManager());
            container.RegisterType<ICountryRepository, CountryRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ICountryService, CountryService>(new HierarchicalLifetimeManager());
            container.RegisterType<IStateRepository, StateRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IDriverDetailRepository, DriverDetailRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IStateService, StateService>(new HierarchicalLifetimeManager());
            container.RegisterType<IDriverDetailService, DriverDetailService>(new HierarchicalLifetimeManager());
            container.RegisterType<IDistrictRepository, DistrictRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IDistrictService, DistrictService>(new HierarchicalLifetimeManager());
            container.RegisterType<IConsgineeBuyersRepository, ConsgineeBuyersRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IConsgineeBuyersService, ConsgineeBuyersService>(new HierarchicalLifetimeManager());
            container.RegisterType<ICountyOverseasRepository, CountryOverseasRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ICountryOverseasService, CountryOverseasService>(new HierarchicalLifetimeManager());
            container.RegisterType<IStateOverseasRepository, StateOverseasRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IStateOverseasService, StateOverseasService>(new HierarchicalLifetimeManager());
            container.RegisterType<ICityOverseasService, CityOverseasService>(new HierarchicalLifetimeManager());
            container.RegisterType<ICityOverseasRepository, CityOverseasRepository>(new HierarchicalLifetimeManager());


            container.RegisterType<IFarmersAgreementSizeService, FarmersAgreementSizeService>(new HierarchicalLifetimeManager());
            container.RegisterType<IFarmersAgreementSizeRepository, FarmersAgreementSizeRepository>(new HierarchicalLifetimeManager());

            container.RegisterType<ISkillInformationService, SkillInformationService>(new HierarchicalLifetimeManager());
            container.RegisterType<ISkillsInformationRepository, SkillsInformationRepository>(new HierarchicalLifetimeManager());
            //--------
            container.RegisterType<IFarmerAccountDetailsFinalizationService, FarmerAccountDetailsFinalizationService>(new HierarchicalLifetimeManager());
            container.RegisterType<IFarmerAccountDetailsFinalizationRepository, FarmerAccountDetailsFinalizationRepository>(new HierarchicalLifetimeManager());
            //----------
            //-----------
            container.RegisterType<IFarmersAgreementService, FarmersAgreementService>(new HierarchicalLifetimeManager());
            container.RegisterType<IFarmersAgreementRepository, FarmersAgreementRepository>(new HierarchicalLifetimeManager());
            //-----------
            container.RegisterType<IOrganisationOfficeLocationDetialsRepository, OrganisationOfficeLocationDetialsRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IRMStockService, RMStockService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRmStockDetailsRepository, RmStockDetailsRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IFarmerRepository, FarmerRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IProductDetailsService, ProductService>(new HierarchicalLifetimeManager());
            container.RegisterType<IProductDetailsRepository, ProductRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IFarmerService, FarmerService>(new HierarchicalLifetimeManager());
            container.RegisterType<IOrganisationService, OrganisationService>(new HierarchicalLifetimeManager());
            container.RegisterType<IContratorService, ContractorService>(new HierarchicalLifetimeManager());
            container.RegisterType<IContractorRepository, ContractorRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IHarvestStageService, HarvestStageService>(new HierarchicalLifetimeManager());
            container.RegisterType<IHarvestStageRepository, HarvestStageRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IPackageOfPracticeService, PackagePracticeService>(new HierarchicalLifetimeManager());
            container.RegisterType<IPackageofPracticeRepository, PackageofPracticeRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IPlaceRepository, PlaceRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ICurrencyRepositroy, CurrecncyRepositroy>(new HierarchicalLifetimeManager());
            container.RegisterType<ICurrencyService, CurrencyService>(new HierarchicalLifetimeManager());
            container.RegisterType<IBranchIndentService, BranchIndentService>(new HierarchicalLifetimeManager());
            container.RegisterType<IBranchIndentRepository, BranchIndentRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IBankAccountDetailsRepository, BankAccountDetailsRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IBankAccountDetailsService, BankAccountDetailsService>(new HierarchicalLifetimeManager());
            container.RegisterType<IAccountsGroupRepository, AccountsGroupRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IAccountsGroupService, AccountsGroupService>(new HierarchicalLifetimeManager());
            container.RegisterType<IEmployeeBankDetailsMasterService, EmployeeBankDetailsMasterService>(new HierarchicalLifetimeManager());
            container.RegisterType<IPurchageManagementService, PurchaseManagementService>(new HierarchicalLifetimeManager());
            container.RegisterType<IPurchageManagementRepository, PurchaseManagementRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IMaterialRepository, MaterialInwardRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IMaterialService, MaterialInwardService>(new HierarchicalLifetimeManager());
            container.RegisterType<IProdProcessBOMRepository, ProdProcessBOMRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IProdProcessBOMService, ProdProcessBOMService>(new HierarchicalLifetimeManager());
            container.RegisterType<IProformaInvoiceDetailsRepository, ProformaInvoiceDetailsRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IProdProcessBOMRepository, ProdProcessBOMRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IProdProcessBOMService, ProdProcessBOMService>(new HierarchicalLifetimeManager());
            container.RegisterType<IGRNRepository, GRNRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IGRNService, GRNService>(new HierarchicalLifetimeManager());
            container.RegisterType<IGRNMaterialRepository, GRNMaterialRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IGRNMaterialService, GRNMaterialService>(new HierarchicalLifetimeManager());
            container.RegisterType<IMaterialTotalCostRepository, MaterialTotalCostRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IMaterialTotalCostService, MaterialTotalCostService>(new HierarchicalLifetimeManager());

            container.RegisterType<IAccountMasterRepository, AccountMasterRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IAccountMasterService, AccountMasterService>(new HierarchicalLifetimeManager());
            container.RegisterType<IHarvestGRNReportRepository, HarvestGRNReportRepository>(new HierarchicalLifetimeManager());

            container.RegisterType<ILoginService, LoginService>(new HierarchicalLifetimeManager());
            container.RegisterType<ILoginRepository, LoginDetailsRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IMandalRepository, MandalRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IMandalService, MandalService>(new HierarchicalLifetimeManager());
            container.RegisterType<IFeedInputTransferService, FeedInputTransferService>(new HierarchicalLifetimeManager());
            container.RegisterType<IFeedInputTransferRepository, FeedInputTransferRepository>(new HierarchicalLifetimeManager());

            container.RegisterType<IAreaMaterialReceivedRepository, AreaMaterialReceivedDetailRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IAreaMaterialReceivedService, AreaMaterialReceivedService>(new HierarchicalLifetimeManager());

            container.RegisterType<IAreaMRInwardRepository, AreaMRInwardRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IAreaMRInwardService, AreaMRInwardService>(new HierarchicalLifetimeManager());
            container.RegisterType<IDailyInputRepository, DailyInputRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IDailyInputService, DailyInputService>(new HierarchicalLifetimeManager());
            container.RegisterType<ISowingFarmingService, SowingFarmingService>(new HierarchicalLifetimeManager());
            container.RegisterType<IHarvestDetailsService, HarvestDetailsService>(new HierarchicalLifetimeManager());
            container.RegisterType<IHarvestDetailsRepository, HarvestDetailsRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IDriverDocumentRepository, DriverDocumentRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IDriverDocumentService, DriverDocumentService>(new HierarchicalLifetimeManager());
            container.RegisterType<IInputIssuesRepository, InputIssuesRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IInputIssuesService, InputIssuesService>(new HierarchicalLifetimeManager());
            container.RegisterType<IHarvestGRNRepository, HarvestGRNRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IHarvestGRNService, HarvestGRNService>(new HierarchicalLifetimeManager());
            container.RegisterType<IHarvestGRNFarmerRepository, HarvestGRNFarmerRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IHarvestGRNFarmerService, HarvestGRNFarmerService>(new HierarchicalLifetimeManager());
            container.RegisterType<IGreenReceptionQualityRepository, GreenReceptionQualityRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IGreenReceptionQualityService, GreenReceptionQualityService>(new HierarchicalLifetimeManager());
            container.RegisterType<IHarvestGRNCrateRepository, HarvestGRNCrateRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IHarvestGRNCrateService, HarvestGRNCrateService>(new HierarchicalLifetimeManager());
            container.RegisterType<ISowingFarmingRepository, SowingFarmingRepository>(new HierarchicalLifetimeManager());

            container.RegisterType<IHarvestGRNWeightmentDetailsRepository, HarvestGRNWeightmentDetailsRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IHarvestGRNWeightmentDetailsService, HarvestGRNWeightmentDetailsService>(new HierarchicalLifetimeManager());

            container.RegisterType<IGradingWeightService, GradingWeightService>(new HierarchicalLifetimeManager());
            container.RegisterType<IGradingWeightRepository, GradingWeightRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IBatchProductionPreparationRepository, BatchProductionPreparationRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IBatchProductionPreparationService, BatchProductionPreparationService>(new HierarchicalLifetimeManager());
            container.RegisterType<IInterfaceService, InterfaceService>(new HierarchicalLifetimeManager());
            container.RegisterType<IInterfaceRepository, InterfaceRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IMediaBatchService, MediaBatchService>(new HierarchicalLifetimeManager());
            container.RegisterType<IMediaBatchRepository, MediaBatchRepository>(new HierarchicalLifetimeManager());

            container.RegisterType<ICullingDetailsRepository, CullingDetailsRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ICullingDetailsService, CullingDetailsService>(new HierarchicalLifetimeManager());
            container.RegisterType<IDepartMentAndDesignationRepository, DepartMentAndDesignationRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IFarmerWiseSummaryRepository, FarmerWiseSummaryRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IProfessionalTaxMasterRepository, ProfessionalTaxMasterRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IProfessionalTaxSlabRepository, ProfessionalTaxSalbRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IProfessionalTaxMasterService, ProfessionalTaxMasterService>(new HierarchicalLifetimeManager());
            container.RegisterType<IProfessionalTaxSlabService, ProfessionalTaxSlabService>(new HierarchicalLifetimeManager());

            container.RegisterType<IShiftDetailsRepository, ShiftDetailsRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IShiftDetailsService, ShiftDetailsService>(new HierarchicalLifetimeManager());
            container.RegisterType<IEmployeeBankDetailsMasterRepository, EmployeeBankDetailsMasterRepository>(new HierarchicalLifetimeManager());


            container.RegisterType<IFarmerWiseSummaryRepository, FarmerWiseSummaryRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IProvidentFundRateDetailsRepository, ProvidentFundRateDetailsRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IGreensReceiptsSummaryRepository, GreensReceiptsSummaryRepository>(new HierarchicalLifetimeManager());

            container.RegisterType<IProvidentFundRateDetailsService, ProvidentFundRateDetailsService>(new HierarchicalLifetimeManager());
            container.RegisterType<IYearlyHolidayDetailsService, YearlyHolidayDetailsService>(new HierarchicalLifetimeManager());
            container.RegisterType<IYearlyHolidayDetailsRepository, YearlyHolidayDetailsRepository>(new HierarchicalLifetimeManager());

            container.RegisterType<IESICService, ESICService>(new HierarchicalLifetimeManager());
            container.RegisterType<IESICRepository, ESICRepository>(new HierarchicalLifetimeManager());

            container.RegisterType<IGreensTransportVehicleScheduleRepository, GreensTransportVehicleScheduleRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IGreensTransportVehicleScheduleService, GreensTransportVehicleScheduleService>(new HierarchicalLifetimeManager());

            container.RegisterType<IVehicleRepository, VehicleRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IVehicleService, VehicleService>(new HierarchicalLifetimeManager());

            container.RegisterType<IGpsTrackingDeviceRepository, GpsTrackingDeviceRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IGpsTrackingDeviceService, GpsTrackingDeviceService>(new HierarchicalLifetimeManager());

            container.RegisterType<IHiredTransporterDetailRepository, HiredTransporterDetailRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IHiredTransporterDetailService, HiredTransporterDetailService>(new HierarchicalLifetimeManager());
            container.RegisterType<IHiredVehicleDetailRepository, HiredVehicleDetailRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IHiredVehicleDetailService, HiredVehicleDetailService>(new HierarchicalLifetimeManager());
            container.RegisterType<IHiredVehicleDocumentService, HiredVehicleDocumentService>(new HierarchicalLifetimeManager());

            container.RegisterType<IAttendanceRepository, AttendanceRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IAttendanceService, AttendanceService>(new HierarchicalLifetimeManager());
            container.RegisterType<ILoansAdvancesRepository, LoansAdvancesRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ILoansAdvancesService, LoansAdvancesService>(new HierarchicalLifetimeManager());

            container.RegisterType<IFarmersInputRatesSeasonWiseService, FarmersInputRatesSeasonWiseService>(new HierarchicalLifetimeManager());
            container.RegisterType<IFarmersInputRatesSeasonWiseRepository, FarmersInputRatesSeasonWiseRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IDailyHarvestRepository, DailyHarvestRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IDailyHarvestService, DailyHarvestService>(new HierarchicalLifetimeManager());
            container.RegisterType<IFarmersInputReturnsService, FarmersInputReturnsService>(new HierarchicalLifetimeManager());

            container.RegisterType<IInputToFieldStaffRepository, InputToFieldStaffRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IInputToFieldStaffService, InputToFieldStaffService>(new HierarchicalLifetimeManager());
            container.RegisterType<IDialyGreensReceivingRepository, DialyGreensReceivingRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IDialyAttendanceRepository, DialyAttendanceRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IMonthlyAttendanceRepository, MonthlyAttendanceRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IManualAttendenceMasterRepository, ManualAttendenceMasterRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IManualAttendenceDetailsRepository, ManualAttendenceDetailsRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IManualAttendenceService, ManualAttendenceService>(new HierarchicalLifetimeManager());

            container.RegisterType<IFinishedSFOpeningStockService, FinishedSFOpeningStockService>(new HierarchicalLifetimeManager());
            container.RegisterType<IFinishedSFOpeningStockRepository, FinishedSFOpeningStockRepository>(new HierarchicalLifetimeManager());

            container.RegisterType<IGSGroupDetailService, GSGroupDetailService>(new HierarchicalLifetimeManager());
            container.RegisterType<IGSSubGroupDetailService, GSSubGroupDetailService>(new HierarchicalLifetimeManager());
            container.RegisterType<IGSCUOMDetailService, GSCUOMDetailService>(new HierarchicalLifetimeManager());
            container.RegisterType<IGSMaterialDetailService, GSMaterialDetailService>(new HierarchicalLifetimeManager());

            container.RegisterType<IStoreInternalIndentMasterService, StoreInternalIndentMasterService>(new HierarchicalLifetimeManager());
            container.RegisterType<IStoreInternalIndentDetailService, StoreInternalIndentDetailService>(new HierarchicalLifetimeManager());

            container.RegisterType<IBuyingStaffDetailsRepository, BuyingStaffDetailsRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IBuyingStaffDetailsService, BuyingStaffDetailsService>(new HierarchicalLifetimeManager());
            container.RegisterType<IAdvCashIssuedToFarmrRepository, AdvCashIssuedToFarmrRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IAdvCashIssuedToFarmrService, AdvanceCashIssuedToFarmersService>(new HierarchicalLifetimeManager());

            container.RegisterType<IGreensAgentSupplierDetailsRepository, GreensAgentSupplierDetailsRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IGreensAgentSupplierDetailsService, GreensAgentSupplierDetailsService>(new HierarchicalLifetimeManager());

            container.RegisterType<IAgentsGreensReceivingWeighmentRepository, AgentsGreensReceivingWeighmentRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IAgentsGreensReceivingWeighmentService, AgentsGreensReceivingWeighmentService>(new HierarchicalLifetimeManager());

            container.RegisterType<IMediaProcessDetailsService, MediaProcessDetailsService>(new HierarchicalLifetimeManager());
            container.RegisterType<IMediaProcessDetailsRepository, MediaProcessDetailsRepository>(new HierarchicalLifetimeManager());

            container.RegisterType<IScheduleDetailService, ScheduleDetailService>(new HierarchicalLifetimeManager());
            container.RegisterType<IScheduleDetailRepository, ScheduleDetailRepository>(new HierarchicalLifetimeManager());

            
                

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);

        }
    }
}