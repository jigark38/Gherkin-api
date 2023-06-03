using ESSWebAPI.Repository;
using GherkinWebAPI.Core;
using GherkinWebAPI.Core.Contractor;
using GherkinWebAPI.Core.Harvest_Area;
using GherkinWebAPI.Core.Harvest_Area_Village;
using GherkinWebAPI.Core.Villages;
using GherkinWebAPI.Core.PackageofPractice;
using GherkinWebAPI.Persistence;
using GherkinWebAPI.Repository.HarvestAreaAndVillage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GherkinWebAPI.Repository.PackageOfPracticesRepository;
using GherkinWebAPI.Core.Accounts_Master;
using GherkinWebAPI.Repository.Accounts_Master;
using GherkinWebAPI.Core.GRPAndroid.ProdProcessBOM;
using GherkinWebAPI.Repository.GRPAndroid.ProdProcessBOM;
using GherkinWebAPI.Core.DailyInputAndFeedingDetails;
using GherkinWebAPI.Repository.DailyInputAndFeedingDetails;
using GherkinWebAPI.Core.GoodsReceiptNote;
using GherkinWebAPI.Repository.GoodsReceiptNote;
using GherkinWebAPI.Core.Login;
using GherkinWebAPI.Repository.Login;
using GherkinWebAPI.Core.Mandals;
using GherkinWebAPI.Repository.Mandals;
using GherkinWebAPI.Core.AreaMaterialReceivedDetails;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Core.GRNAndMaterialClassification;
using GherkinWebAPI.Repository.GRNAndMaterialClassification;
using GherkinWebAPI.Core.MediaBatchDetails;
using GherkinWebAPI.Core.ProfessionalTaxRates;
using GherkinWebAPI.Repository.ProfessionalTaxRates;
using GherkinWebAPI.Repository.MediaBatchDetails;
using GherkinWebAPI.Core.ProvidentFund;
using GherkinWebAPI.Repository.ProvidentFund;
using GherkinWebAPI.Core.ShiftDetails;
using GherkinWebAPI.Repository.ShiftDetails;
using GherkinWebAPI.Core.GreensTransportVehicleSchedules;
using GherkinWebAPI.Repository.GreensTransportVehicleSchedules;
using GherkinWebAPI.Repository.DriverDetailRepository;
using GherkinWebAPI.Core.DriverDocuments;

using GherkinWebAPI.Core.TransportVehicleManagement;
using GherkinWebAPI.Repository.TransportVehicleManagement;
using GherkinWebAPI.Core.LoansAndAdvancesDetails;
using GherkinWebAPI.Models.LoansAndAdvancesDetails;
using GherkinWebAPI.Repository.LoansAndAdvancesDetails;
using GherkinWebAPI.Core.FarmersInputReturns;
using GherkinWebAPI.Repository.FarmersInputReturns;
using GherkinWebAPI.Core.DailyHarvestDetails;
using GherkinWebAPI.Repository.DailyHarvestDetails;
using GherkinWebAPI.Core.InputToFieldStaff;
using GherkinWebAPI.Repository.InputToFieldStaff;
using GherkinWebAPI.Core.StoresMasterDetails;
using GherkinWebAPI.Repository.StoresMasterDetails;
using GherkinWebAPI.Core.ManualAttendence;
using GherkinWebAPI.Repository.ManualAttendence;
using GherkinWebAPI.Core.MaterialIndentByDepartment;
using GherkinWebAPI.Repository.MaterialIndentByDepartment;
using GherkinWebAPI.Core.BuyingStaffDetails;
using GherkinWebAPI.Repository.BuyingStaffDetails;
using GherkinWebAPI.Core.GreensAgentSupplierDetails;
using GherkinWebAPI.Repository.GreensAgentSupplierDetails;
using GherkinWebAPI.Core.FarmerAccountDetailsFinalization;
using GherkinWebAPI.Repository.FarmerAccountDetailsFinalization;
using GherkinWebAPI.Core.AgentsGreensRecWeighment;
using GherkinWebAPI.Repository.AgentsGreensRecWeighment;
using GherkinWebAPI.Core.MediaProcessDetail;
using GherkinWebAPI.Repository.MediaProcessDetail;
using GherkinWebAPI.Core.ScheduleDetail;
using GherkinWebAPI.Repository.ScheduleDetail;
using GherkinWebAPI.Core.Reports.MonthlyAttendance;
using GherkinWebAPI.Repository.Reports.MonthlyAttendance;

namespace GherkinWebAPI.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private ICustomerRepository _customer;
        private IAccountRepository _account;
        private IDriverDetailRepository _driverDetailRepository;
        private IDriverDocumentRepository _driverDocumentRepository;
        private IPlantationHarvestRepository _plantation;
        private IRawMaterialRepository _rawMaterialGroupMaster;
        private ICropRepository _cropRepository;
        private ICropRateRepository _cropRateRepository;
        private IAreaRepository _areaRepository;
        private IDepartmentRepository _departmentRepository;
        private IDesignationRepository _designationRepository;
        private ISubDepartmentRepository _subDepartmentRepository;
        private IEmployeeRepository _employeeRepository;
        private IEmployeeBankDetailsMasterRepository _employeeBankDetailsMasterRepository;
        private ISupplierDetailsRepository _supplierDetails;
        private IHarvestAreaRepository _harvestArea;
        private IHarvestAreaVillageRepository _harvestAreaVillage;
        private IVillageRepository _village;
        private IFieldStaffDetailsRepository _fieldStaffDetailsRepository;
        private IConsgineeBuyersRepository _consgineeBuyersRepository;
        private IFarmerRepository _farmerRepository;
        private ISkillsInformationRepository _skillInformationRepository;
        private IContractorRepository _contractorRepository;
        private IOrganisationRepository _organisation;
        private IFarmersAgreementRepository _farmersAgreementRepository;
        private IFarmerAccountDetailsFinalizationRepository _farmerAccountDetailsFinalizationRepository;

        private IFarmersAgreementSizeRepository _farmersAgreementSizeRepository;
        private IPackageofPracticeRepository _packageofPracticesRepository;
        private IAccountsGroupRepository _accountsGroupRepository;
        private IBankAccountDetailsRepository _bankAccountDetailsRepository;
        private IAccountMasterRepository _accountMasterRepository;
        private IProdProcessBOMRepository _prodProcessBOMRepository;
        private IDailyInputRepository _dailyInputRepository;
        private IGRNRepository _grnRpository;
        private IGRNMaterialRepository _grnNMaterialRepository;
        private IMaterialTotalCostRepository _materialTotalCostRepository;
        public ILoginRepository _LoginRepository;
        public IMandalRepository _MandalRepository;
        public IInputIssuesRepository _InputIssuesRepository;
        public IAreaMaterialReceivedRepository _areaMaterialReceivedRepository;
        public IAreaMRInwardRepository _areaMRInwardRepository;
        public IHarvestGRNRepository _HarvestGRNRepository;
        public IHarvestGRNFarmerRepository _HarvestGRNFarmerRepository;
        public IHarvestGRNCrateRepository _HarvestGRNCrateRepository;
        public IInterfaceRepository _interfaceRepository;
        public IMediaBatchRepository _mediaBatchRepository;
        public IHarvestGRNWeightmentDetailsRepository _harvestGRNWeightmentDetails;
        public IShiftDetailsRepository _shiftDetailsRepository;
        public IFinishedSFOpeningStockRepository _finishedSFOpeningStockRepository;

        public IProvidentFundRateDetailsRepository _providentFundRateDetailsRepository;
        public IGradingWeightRepository _gradingWeightRepository;
        public IESICRepository _esicRepository;
        public IProfessionalTaxMasterRepository _professionalTaxMasterRepository;
        public IProfessionalTaxSlabRepository _professionalTaxSlabRepository;

        public IYearlyHolidayDetailsRepository _yearlyHolidayDetailsRepository;
        public IGreensTransportVehicleScheduleRepository _greensTransportVehicleScheduleRepository;

        public IVehicleRepository _vehicleRepository;
        public IGpsTrackingDeviceRepository _gpsTrackingDeviceRepository;
        private IHiredTransporterDetailRepository _hired_Transporter_DetailsRepository;
        private IHiredVehicleDetailRepository _hired_Vehicle_DetailsRepository;
        private IHiredVehicleDocumentRepository _hired_Vehicle_DocumentsRepository;
        private ILoansAdvancesRepository _loansAdvancesRepository;
        private IFarmersInputRatesSeasonWiseRepository _farmersInputRatesSeasonWiseRepository;
        private IFarmersInputReturnsRepository _farmersInputReturnsRepository;
        private IDailyHarvestRepository _dailyHarvestRepository;
        private IInputToFieldStaffRepository _inputToFieldStaffRepository;

        private IGSMaterialDetailRepository _gsMaterialDetailRepository;
        private IGSGroupDetailRepository _gsGroupDetailRepository;
        private IGSSubGroupDetailRepository _gsSubGroupDetailRepository;
        private IGSCUOMDetailRepository _gscUOMDetailRepository;

        private IBuyingStaffDetailsRepository _buyingStaffDetailsRepository;

        private IManualAttendenceDetailsRepository _manualAttendenceRepositor;

        private IStoreInternalIndentMasterRepository _storeInternalIndentMasterRepository;
        private IStoreInternalIndentDetailRepository _storeInternalIndentDetailRepository;
        private IAdvCashIssuedToFarmrRepository _advCashIssuedToFarmrRepository;

        private IGreensAgentSupplierDetailsRepository _greensAgentSupplierDetailsRepository;

        private IAgentsGreensReceivingWeighmentRepository _agentsGreensReceivingWeighmentRepository;

        private IMediaProcessDetailsRepository _mediaProcessDetailsRepository;

        private IScheduleDetailRepository _scheduleDetailRepository;
        private IMonthlyAttendanceRepository _monthlyAttendanceRepository;

        public IGradingWeightRepository GradingWeightRepository
        {
            get
            {
                if (_gradingWeightRepository == null)
                {
                    _gradingWeightRepository = new GradingWeightRepository(_repoContext);
                }

                return _gradingWeightRepository;
            }
        }

        public ICustomerRepository Customer
        {
            get
            {
                if (_customer == null)
                {
                    _customer = new CustomerRepository(_repoContext);
                }

                return _customer;
            }
        }

        public IAccountRepository Account
        {
            get
            {
                if (_account == null)
                {
                    _account = new AccountRepository(_repoContext);
                }

                return _account;
            }
        }

        public IPlantationHarvestRepository Plantation
        {
            get
            {
                if (_plantation == null)
                {
                    _plantation = new PlantationHarvestRepository(_repoContext);
                }

                return _plantation;
            }
        }

        public IRawMaterialRepository RawMaterialGroup
        {
            get
            {
                if (_rawMaterialGroupMaster == null)
                {
                    _rawMaterialGroupMaster = new RawMaterialRepository(_repoContext);
                }

                return _rawMaterialGroupMaster;
            }
        }

        public ICropRepository CropRepository
        {
            get
            {
                if (_cropRepository == null)
                {
                    _cropRepository = new CropRepository(_repoContext);
                }

                return _cropRepository;
            }
        }
        public ICropRateRepository CropRateRepository
        {
            get
            {
                if (_cropRateRepository == null)
                {
                    _cropRateRepository = new CropRateRepository(_repoContext);
                }

                return _cropRateRepository;
            }
        }

        public IAreaRepository AreaRepository
        {
            get
            {
                if (_areaRepository == null)
                {
                    _areaRepository = new AreaRepository(_repoContext);
                }

                return _areaRepository;
            }
        }

        public IDepartmentRepository DepartmentRepository
        {
            get
            {
                if (_departmentRepository == null)
                {
                    _departmentRepository = new DepartmentRepository(_repoContext);
                }

                return _departmentRepository;
            }
        }

        public ISubDepartmentRepository SubDepartmentRepository
        {
            get
            {
                if (_subDepartmentRepository == null)
                {
                    _subDepartmentRepository = new SubDepartmentRepository(_repoContext);
                }

                return _subDepartmentRepository;
            }
        }
        public IDesignationRepository DesignationRepository
        {
            get
            {
                if (_designationRepository == null)
                {
                    _designationRepository = new DesignationRepository(_repoContext);
                }

                return _designationRepository;
            }
        }
        public IEmployeeRepository EmployeeRepository
        {
            get
            {
                if (_employeeRepository == null)
                {
                    _employeeRepository = new EmployeeRepository(_repoContext);
                }

                return _employeeRepository;
            }
        }

        public IEmployeeBankDetailsMasterRepository EmployeeBankDetailsMasterRepository
        {
            get
            {
                if (_employeeBankDetailsMasterRepository == null)
                {
                    _employeeBankDetailsMasterRepository = new EmployeeBankDetailsMasterRepository(_repoContext);
                }

                return _employeeBankDetailsMasterRepository;
            }
        }
        public IFieldStaffDetailsRepository FieldStaffDetailsRepository
        {
            get
            {
                if (_fieldStaffDetailsRepository == null)
                {
                    _fieldStaffDetailsRepository = new FieldStaffDetailsRepository(_repoContext);
                }

                return _fieldStaffDetailsRepository;
            }
        }
        public IFarmerRepository FarmerRepository
        {
            get
            {
                if (_farmerRepository == null)
                {
                    _farmerRepository = new FarmerRepository(_repoContext);
                }

                return _farmerRepository;
            }
        }
        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        //public void Save()
        //{
        //    _repoContext.SaveChanges();
        //}
        public ISupplierDetailsRepository SupplierDetails
        {
            get
            {
                if (_supplierDetails == null)
                {
                    _supplierDetails = new SupplierDetailsRepository(_repoContext);
                }

                return _supplierDetails;
            }
        }

        public IHarvestAreaRepository HarvestArea
        {
            get
            {
                if (_harvestArea == null)
                {
                    _harvestArea = new HarvestAreaRepository(_repoContext);
                }

                return _harvestArea;
            }
        }

        public IHarvestAreaVillageRepository HarvestAreaVillage
        {
            get
            {
                if (_harvestAreaVillage == null)
                {
                    _harvestAreaVillage = new HarvestAreaVillageRepository(_repoContext);
                }

                return _harvestAreaVillage;
            }
        }


        public IVillageRepository Village
        {
            get
            {
                if (_village == null)
                {
                    _village = new VillageRepository(_repoContext);
                }

                return _village;
            }
        }
        public IConsgineeBuyersRepository ConsgineeBuyersRepository
        {
            get
            {
                if (_consgineeBuyersRepository == null)
                {
                    _consgineeBuyersRepository = new ConsgineeBuyersRepository(_repoContext);
                }

                return _consgineeBuyersRepository;
            }
        }

        public IOrganisationRepository Organisation
        {
            get
            {
                if (_organisation == null)
                {
                    _organisation = new OrganisationRepository(_repoContext);
                }

                return _organisation;
            }
        }
        public ISkillsInformationRepository SkillInformationRepository
        {
            get
            {
                if (_skillInformationRepository == null)
                {
                    _skillInformationRepository = new SkillsInformationRepository(_repoContext);
                }

                return _skillInformationRepository;
            }
        }

       

        public IFarmersAgreementRepository FarmersAgreementRepository
        {
            get
            {
                if (_farmersAgreementRepository == null)
                {
                    _farmersAgreementRepository = new FarmersAgreementRepository(_repoContext);
                }

                return _farmersAgreementRepository;
            }
        }

        public IFarmersAgreementSizeRepository FarmersAgreementSizeRepository
        {
            get
            {
                if (_farmersAgreementSizeRepository == null)
                {
                    _farmersAgreementSizeRepository = new FarmersAgreementSizeRepository(_repoContext);
                }

                return _farmersAgreementSizeRepository;
            }
        }

        public IContractorRepository ContractorRepository
        {
            get
            {
                if (_contractorRepository == null)
                {
                    _contractorRepository = new Repository.Contractor.ContractorRepository(_repoContext);
                }

                return _contractorRepository;
            }
        }

        public IPackageofPracticeRepository packageofPracticeRepository
        {
            get
            {
                if (_packageofPracticesRepository == null)
                {
                    _packageofPracticesRepository = new PackageofPracticeRepository(_repoContext);
                }

                return _packageofPracticesRepository;
            }
        }

        public IAccountsGroupRepository AccountsGroupRepository
        {
            get
            {
                if (_accountsGroupRepository == null)
                {
                    _accountsGroupRepository = new AccountsGroupRepository(_repoContext);
                }

                return _accountsGroupRepository;
            }
        }


        public IBankAccountDetailsRepository BankAccountDetailsRepository
        {
            get
            {
                if (_bankAccountDetailsRepository == null)
                {
                    _bankAccountDetailsRepository = new BankAccountDetailsRepository(_repoContext);
                }

                return _bankAccountDetailsRepository;
            }
        }
        public IAccountMasterRepository AccountMasterRepository
        {
            get
            {
                if (_accountMasterRepository == null)
                {
                    _accountMasterRepository = new AccountMasterRepository(_repoContext);
                }

                return _accountMasterRepository;
            }
        }

        public IGRNRepository GRNReposiory
        {
            get
            {
                if (_grnRpository == null)
                {
                    _grnRpository = new GRNRepository(_repoContext);
                }

                return _grnRpository;
            }
        }

        public IGRNMaterialRepository GRNMaterialRepository
        {
            get
            {
                if (_grnNMaterialRepository == null)
                {
                    _grnNMaterialRepository = new GRNMaterialRepository(_repoContext);
                }

                return _grnNMaterialRepository;
            }
        }

        public IProdProcessBOMRepository ProdProcessBOMRepository
        {
            get
            {
                if (_prodProcessBOMRepository == null)
                {
                    _prodProcessBOMRepository = new ProdProcessBOMRepository(_repoContext);
                }
                return _prodProcessBOMRepository;
            }
        }
        public IMaterialTotalCostRepository MaterialTotalCostRepository
        {
            get
            {
                if (_materialTotalCostRepository == null)
                {
                    _materialTotalCostRepository = new MaterialTotalCostRepository(_repoContext);
                }

                return _materialTotalCostRepository;
            }

        }



        public ILoginRepository LoginRepository
        {
            get
            {
                if (_LoginRepository == null)
                {
                    _LoginRepository = new LoginDetailsRepository(_repoContext);
                }
                return _LoginRepository;
            }
        }

        public IMandalRepository MandalRepository
        {
            get
            {
                if (_MandalRepository == null)
                {
                    _MandalRepository = new MandalRepository(_repoContext);
                }
                return _MandalRepository;
            }
        }

        public IAreaMaterialReceivedRepository AreaMaterialReceivedRepository
        {
            get
            {
                if (_areaMaterialReceivedRepository == null)
                {
                    _areaMaterialReceivedRepository = new AreaMaterialReceivedDetailRepository(_repoContext);
                }
                return _areaMaterialReceivedRepository;
            }
        }

        public IAreaMRInwardRepository AreaMRInwardRepository
        {
            get
            {
                if (_areaMRInwardRepository == null)
                {
                    _areaMRInwardRepository = new AreaMRInwardRepository(_repoContext);
                }
                return _areaMRInwardRepository;
            }
        }



        public IDailyInputRepository DailyInputRepository
        {
            get
            {
                if (_dailyInputRepository == null)
                {
                    _dailyInputRepository = new DailyInputRepository(_repoContext);
                }
                return _dailyInputRepository;
            }
        }

        public IInputIssuesRepository InputIssuesRepository
        {
            get
            {
                if (_InputIssuesRepository == null)
                {
                    _InputIssuesRepository = new InputIssuesRepository(_repoContext);
                }
                return _InputIssuesRepository;
            }
        }

        public IHarvestGRNRepository HarvestGRNRepository
        {
            get
            {
                if (_HarvestGRNRepository == null)
                {
                    _HarvestGRNRepository = new HarvestGRNRepository(_repoContext);
                }
                return _HarvestGRNRepository;
            }
        }

        public IHarvestGRNFarmerRepository HarvestGRNFarmerRepository
        {
            get
            {
                if (_HarvestGRNFarmerRepository == null)
                {
                    _HarvestGRNFarmerRepository = new HarvestGRNFarmerRepository(_repoContext);
                }
                return _HarvestGRNFarmerRepository;
            }
        }

        public IHarvestGRNWeightmentDetailsRepository HarvestGRNWeightmentDetails
        {
            get
            {
                if (_harvestGRNWeightmentDetails == null)
                {
                    _harvestGRNWeightmentDetails = new HarvestGRNWeightmentDetailsRepository(_repoContext);
                }
                return _harvestGRNWeightmentDetails;
            }
        }

        public IHarvestGRNCrateRepository HarvestGRNCrateRepository
        {
            get
            {
                if (_HarvestGRNCrateRepository == null)
                {
                    _HarvestGRNCrateRepository = new HarvestGRNCrateRepository(_repoContext);
                }
                return _HarvestGRNCrateRepository;
            }
        }
        public IInterfaceRepository InterfaceRepository
        {
            get
            {
                if (_interfaceRepository == null)
                {
                    _interfaceRepository = new InterfaceRepository(_repoContext);
                }
                return _interfaceRepository;
            }
        }

        public IMediaBatchRepository MediaBatchRepository
        {
            get
            {
                if (_mediaBatchRepository == null)
                {
                    _mediaBatchRepository = new MediaBatchRepository(_repoContext);
                }
                return _mediaBatchRepository;
            }
        }
        public IShiftDetailsRepository ShiftDetailsRepository
        {
            get
            {
                if (_shiftDetailsRepository == null)
                {
                    _shiftDetailsRepository = new ShiftDetailsRepository(_repoContext);
                }
                return _shiftDetailsRepository;
            }
        }


        public IProvidentFundRateDetailsRepository ProvidentFundRateDetailsRepository
        {
            get
            {
                if (_providentFundRateDetailsRepository == null)
                {
                    _providentFundRateDetailsRepository = new ProvidentFundRateDetailsRepository(_repoContext);
                }
                return _providentFundRateDetailsRepository;
            }
        }



        public IProfessionalTaxMasterRepository professionalTaxMasterRepository
        {
            get
            {
                if (_professionalTaxMasterRepository == null)
                {
                    _professionalTaxMasterRepository = new ProfessionalTaxMasterRepository(_repoContext);
                }
                return _professionalTaxMasterRepository;
            }
        }

        public IProfessionalTaxSlabRepository professionalTaxSlabRepository
        {
            get
            {
                if (_professionalTaxSlabRepository == null)
                {
                    _professionalTaxSlabRepository = new ProfessionalTaxSalbRepository(_repoContext);
                }
                return _professionalTaxSlabRepository;
            }
        }

        public async Task SaveAsync()
        {
            await _repoContext.SaveChangesAsync();
        }


        public IESICRepository ESICRepository
        {
            get
            {
                if (_esicRepository == null)
                {
                    _esicRepository = new ESICRepository(_repoContext);
                }

                return _esicRepository;
            }
        }

        public IYearlyHolidayDetailsRepository YearlyHolidayDetailsRepository
        {
            get
            {
                if (_yearlyHolidayDetailsRepository == null)
                {
                    _yearlyHolidayDetailsRepository = new YearlyHolidayDetailsRepository(_repoContext);
                }
                return _yearlyHolidayDetailsRepository;
            }
        }

        public IGreensTransportVehicleScheduleRepository GreensTransportVehicleScheduleRepository
        {
            get
            {
                if (_greensTransportVehicleScheduleRepository == null)
                {
                    _greensTransportVehicleScheduleRepository = new GreensTransportVehicleScheduleRepository(_repoContext);
                }
                return _greensTransportVehicleScheduleRepository;
            }
        }

        public IVehicleRepository VehicleRepository
        {
            get
            {
                if (_vehicleRepository == null)
                {
                    _vehicleRepository = new VehicleRepository(_repoContext);
                }
                return _vehicleRepository;
            }
        }

        public IGpsTrackingDeviceRepository GpsTrackingDeviceRepository
        {
            get
            {
                if (_gpsTrackingDeviceRepository == null)
                {
                    _gpsTrackingDeviceRepository = new GpsTrackingDeviceRepository(_repoContext);
                }
                return _gpsTrackingDeviceRepository;
            }
        }

        public IHiredTransporterDetailRepository Hired_Transporter_DetailsRepository
        {
            get
            {
                if (_hired_Transporter_DetailsRepository == null)
                {
                    _hired_Transporter_DetailsRepository = new HiredTransporterDetailRepository(_repoContext);
                }
                return _hired_Transporter_DetailsRepository;
            }
        }

        public IHiredVehicleDetailRepository Hired_Vehicle_DetailsRepository
        {
            get
            {
                if (_hired_Vehicle_DetailsRepository == null)
                {
                    _hired_Vehicle_DetailsRepository = new HiredVehicleDetailRepository(_repoContext);
                }
                return _hired_Vehicle_DetailsRepository;
            }
        }

        public IDriverDetailRepository DriverDetailRepository
        {
            get
            {
                if (_driverDetailRepository == null)
                {
                    _driverDetailRepository = new DriverDetailRepository.DriverDetailRepository(_repoContext);
                }

                return _driverDetailRepository;
            }
        }

        public IHiredVehicleDocumentRepository Hired_Vehicle_DocumentsRepository
        {
            get
            {
                if (_hired_Vehicle_DocumentsRepository == null)
                {
                    _hired_Vehicle_DocumentsRepository = new HiredVehicleDocumentRepository(_repoContext);
                }

                return _hired_Vehicle_DocumentsRepository;
            }
        }

        public IDriverDocumentRepository DriverDocumentRepository
        {
            get
            {
                if (_driverDocumentRepository == null)
                {
                    _driverDocumentRepository = new DriverDocumentRepository.DriverDocumentRepository(_repoContext);
                }
                return _driverDocumentRepository;
            }
        }

        public ILoansAdvancesRepository LoansAdvancesRepository
        {
            get
            {
                if (_loansAdvancesRepository == null)
                {
                    _loansAdvancesRepository = new LoansAdvancesRepository(_repoContext);
                }

                return _loansAdvancesRepository;
            }
        }

        public IFarmersInputRatesSeasonWiseRepository FarmersInputRatesSeasonWiseRepository
        {
            get
            {
                if (_farmersInputRatesSeasonWiseRepository == null)
                {
                    _farmersInputRatesSeasonWiseRepository = new FarmersInputRatesSeasonWiseRepository(_repoContext);
                }
                return _farmersInputRatesSeasonWiseRepository;
            }
        }

        public IFarmersInputReturnsRepository FarmersInputReturnsRepository
		{
            get
			{
                if (_farmersInputReturnsRepository == null)
				{
                    _farmersInputReturnsRepository = new FarmersInputReturnsRepository(_repoContext);
                }
                return _farmersInputReturnsRepository;
            }
		}

        public IDailyHarvestRepository DailyHarvestRepository
        {
            get
            {
                if (_dailyHarvestRepository == null)
                {
                    _dailyHarvestRepository = new DailyHarvestRepository(_repoContext);
                }
                return _dailyHarvestRepository;
            }
        }

        public IInputToFieldStaffRepository InputToFieldStaffRepository
        {
            get
            {
                if (_inputToFieldStaffRepository == null)
                {
                    _inputToFieldStaffRepository = new InputToFieldStaffRepository(_repoContext);
                }
                return _inputToFieldStaffRepository;
            }
        }

        public IGSMaterialDetailRepository GSMaterialDetailRepository
		{
			get
			{
                if (_gsMaterialDetailRepository == null)
				{
                    _gsMaterialDetailRepository = new GSMaterialDetailRepository(_repoContext);
                }
                return _gsMaterialDetailRepository;
            }
		}

        public IGSGroupDetailRepository GSGroupDetailRepository
		{
			get
			{
                if (_gsGroupDetailRepository == null)
				{
                    _gsGroupDetailRepository = new GSGroupDetailRepository(_repoContext);
                }
                return _gsGroupDetailRepository;
            }
		}

        public IGSSubGroupDetailRepository GSSubGroupDetailRepository
		{
			get
			{
                if (_gsSubGroupDetailRepository == null)
				{
                    _gsSubGroupDetailRepository = new GSSubGroupDetailRepository(_repoContext);
                }
                return _gsSubGroupDetailRepository;
            }
		}

        public IGSCUOMDetailRepository GSCUOMDetailRepository
		{
			get
			{
                if(_gscUOMDetailRepository == null)
				{
                    _gscUOMDetailRepository = new GSCUOMDetailRepository(_repoContext);
                }
                return _gscUOMDetailRepository;
            }
		}

        public IManualAttendenceDetailsRepository ManualAttendenceDetailsRepository
        {
            get
            {
                if (_manualAttendenceRepositor == null)
                {
                    _manualAttendenceRepositor = new ManualAttendenceDetailsRepository(_repoContext);
                }
                return _manualAttendenceRepositor;
            }
        }
        public IFinishedSFOpeningStockRepository FinishedSFOpeningStockRepository
        {
            get
            {
                if ( _finishedSFOpeningStockRepository == null)
                {
                    _finishedSFOpeningStockRepository = new FinishedSFOpeningStockRepository(_repoContext);
                }
                return _finishedSFOpeningStockRepository;
            }
        }

        public IStoreInternalIndentMasterRepository StoreInternalIndentMasterRepository
		{
			get
			{
                if (_storeInternalIndentMasterRepository == null)
				{
                    _storeInternalIndentMasterRepository = new StoreInternalIndentMasterRepository(_repoContext);
                }
                return _storeInternalIndentMasterRepository;
            }
		}

        public IStoreInternalIndentDetailRepository StoreInternalIndentDetailRepository
		{
			get
			{
                if (_storeInternalIndentDetailRepository == null)
				{
                    _storeInternalIndentDetailRepository = new StoreInternalIndentDetailRepository(_repoContext);
                }
                return _storeInternalIndentDetailRepository;
            }
		}

        public IBuyingStaffDetailsRepository BuyingStaffDetailsRepository
        {
            get
            {
                if (_buyingStaffDetailsRepository == null)
                {
                    _buyingStaffDetailsRepository = new BuyingStaffDetailsRepository(_repoContext);
                }
                return _buyingStaffDetailsRepository;
            }
        }

        public IAdvCashIssuedToFarmrRepository AdvCashIssuedToFarmrRepository
        {
            get
            {
                if(_advCashIssuedToFarmrRepository == null)
                {
                    _advCashIssuedToFarmrRepository = new AdvCashIssuedToFarmrRepository(_repoContext);
                }
                return _advCashIssuedToFarmrRepository;
            }
        }

        public IGreensAgentSupplierDetailsRepository GreensAgentSupplierDetailsRepository
        {
            get
            {
                if(_greensAgentSupplierDetailsRepository == null)
                {
                    _greensAgentSupplierDetailsRepository = new GreensAgentSupplierDetailsRepository(_repoContext);
                }
                return _greensAgentSupplierDetailsRepository;
            }
        }

        public IFarmerAccountDetailsFinalizationRepository FarmerAccountDetailsFinalizationRepository
        {
            get
            {
                if (_farmerAccountDetailsFinalizationRepository == null)
                {
                    _farmerAccountDetailsFinalizationRepository = new FarmerAccountDetailsFinalizationRepository(_repoContext);
                }

                return _farmerAccountDetailsFinalizationRepository;
            }
        }

        public IAgentsGreensReceivingWeighmentRepository AgentsGreensReceivingWeighmentRepository
        {
            get
            {
                if(_agentsGreensReceivingWeighmentRepository == null)
                {
                    _agentsGreensReceivingWeighmentRepository = new AgentsGreensReceivingWeighmentRepository(_repoContext);
                }

                return _agentsGreensReceivingWeighmentRepository;
            }
        }

        public IMediaProcessDetailsRepository MediaProcessDetailsRepository
        {
            get
            {
                if (_mediaProcessDetailsRepository == null)
                {
                    _mediaProcessDetailsRepository = new MediaProcessDetailsRepository(_repoContext);
                }

                return _mediaProcessDetailsRepository;
            }
        }

        public IScheduleDetailRepository ScheduleDetailRepository
        {
            get
            {
                if (_scheduleDetailRepository == null)
                {
                    _scheduleDetailRepository = new ScheduleDetailRepository(_repoContext);
                }

                return _scheduleDetailRepository;
            }
        }

        public IMonthlyAttendanceRepository MonthlyAttendanceRepository
        {
            get
            {
                
                if (_monthlyAttendanceRepository == null)
                {
                    _monthlyAttendanceRepository = new MonthlyAttendanceRepository(_repoContext);
                }

                return _monthlyAttendanceRepository;
            }
        }
    }
}
