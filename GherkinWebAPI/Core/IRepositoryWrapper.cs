using GherkinWebAPI.Core.Contractor;
using GherkinWebAPI.Core.Harvest_Area;
using GherkinWebAPI.Core.Harvest_Area_Village;
using GherkinWebAPI.Core.Villages;
using GherkinWebAPI.Core.PackageofPractice;
using System.Threading.Tasks;
using GherkinWebAPI.Core.Accounts_Master;
using GherkinWebAPI.Core.GoodsReceiptNote;
using GherkinWebAPI.Core.GRPAndroid.ProdProcessBOM;
using GherkinWebAPI.Core.DailyInputAndFeedingDetails;
using GherkinWebAPI.Core.Login;
using GherkinWebAPI.Core.Mandals;
using GherkinWebAPI.Core.AreaMaterialReceivedDetails;
using GherkinWebAPI.Core.GRNAndMaterialClassification;
using GherkinWebAPI.Core.MediaBatchDetails;
using GherkinWebAPI.Core.ProvidentFund;
using GherkinWebAPI.Core.ShiftDetails;
using GherkinWebAPI.Core.GreensTransportVehicleSchedules;
using GherkinWebAPI.Core.ProfessionalTaxRates;
using GherkinWebAPI.Repository.DriverDetailRepository;
using GherkinWebAPI.Core.DriverDocuments;

using GherkinWebAPI.Core.TransportVehicleManagement;
using GherkinWebAPI.Core.LoansAndAdvancesDetails;
using GherkinWebAPI.Core.FarmersInputReturns;
using GherkinWebAPI.Core.DailyHarvestDetails;
using GherkinWebAPI.Core.InputToFieldStaff;
using GherkinWebAPI.Core.StoresMasterDetails;
using GherkinWebAPI.Core.ManualAttendence;
using GherkinWebAPI.Core.MaterialIndentByDepartment;
using GherkinWebAPI.Core.BuyingStaffDetails;
using GherkinWebAPI.Core.GreensAgentSupplierDetails;
using GherkinWebAPI.Core.FarmerAccountDetailsFinalization;
using GherkinWebAPI.Core.AgentsGreensRecWeighment;
using GherkinWebAPI.Core.MediaProcessDetail;
using GherkinWebAPI.Core.ScheduleDetail;
using GherkinWebAPI.Core.Reports.MonthlyAttendance;

namespace GherkinWebAPI.Core
{
    public interface IRepositoryWrapper
    {
        ICustomerRepository Customer { get; }
        IAccountRepository Account { get; }
        IPlantationHarvestRepository Plantation { get; }
        IRawMaterialRepository RawMaterialGroup { get; }

        ICropRepository CropRepository { get; }
        IAreaRepository AreaRepository { get; }
        IDepartmentRepository DepartmentRepository { get; }
        ISubDepartmentRepository SubDepartmentRepository { get; }
        IDesignationRepository DesignationRepository { get; }
        ISkillsInformationRepository SkillInformationRepository { get; }

        IEmployeeRepository EmployeeRepository { get; }
        IEmployeeBankDetailsMasterRepository EmployeeBankDetailsMasterRepository { get; }
        IESICRepository ESICRepository { get; }
        ISupplierDetailsRepository SupplierDetails { get; }
        IHarvestAreaRepository HarvestArea { get; }
        IHarvestAreaVillageRepository HarvestAreaVillage { get; }
        IVillageRepository Village { get; }
        IFieldStaffDetailsRepository FieldStaffDetailsRepository { get; }
        IConsgineeBuyersRepository ConsgineeBuyersRepository { get; }
        IFarmerRepository FarmerRepository { get; }
        Task SaveAsync();
        ICropRateRepository CropRateRepository { get; }

        IOrganisationRepository Organisation { get; }

        IFarmersAgreementRepository FarmersAgreementRepository { get; }
        IFarmerAccountDetailsFinalizationRepository FarmerAccountDetailsFinalizationRepository { get; }
        IFarmersAgreementSizeRepository FarmersAgreementSizeRepository { get; }

        IContractorRepository ContractorRepository { get; }

        IPackageofPracticeRepository packageofPracticeRepository { get; }
        IBankAccountDetailsRepository BankAccountDetailsRepository { get; }

        IAccountsGroupRepository AccountsGroupRepository { get; }
        IAccountMasterRepository AccountMasterRepository { get; }
        IProdProcessBOMRepository ProdProcessBOMRepository { get; }
        IDailyInputRepository DailyInputRepository { get; }
        IGRNRepository GRNReposiory { get; }
        IGRNMaterialRepository GRNMaterialRepository { get; }
        IMaterialTotalCostRepository MaterialTotalCostRepository { get; }
        ILoginRepository LoginRepository { get; }
        IMandalRepository MandalRepository { get; }
        IAreaMaterialReceivedRepository AreaMaterialReceivedRepository { get; }
        IAreaMRInwardRepository AreaMRInwardRepository { get; }
        IInputIssuesRepository InputIssuesRepository { get; }
        IHarvestGRNRepository HarvestGRNRepository { get; }
        IHarvestGRNFarmerRepository HarvestGRNFarmerRepository { get; }
        IHarvestGRNWeightmentDetailsRepository HarvestGRNWeightmentDetails { get; }
        IHarvestGRNCrateRepository HarvestGRNCrateRepository { get; }
        IGradingWeightRepository GradingWeightRepository { get; }
        IMediaBatchRepository MediaBatchRepository { get; }
        IInterfaceRepository InterfaceRepository { get; }
        IProfessionalTaxMasterRepository professionalTaxMasterRepository { get; }
        IProfessionalTaxSlabRepository professionalTaxSlabRepository { get; }
        IProvidentFundRateDetailsRepository ProvidentFundRateDetailsRepository { get; }
        IShiftDetailsRepository ShiftDetailsRepository { get; }
        IYearlyHolidayDetailsRepository YearlyHolidayDetailsRepository { get; }
        IGreensTransportVehicleScheduleRepository GreensTransportVehicleScheduleRepository { get; }
        IVehicleRepository VehicleRepository { get; }
        IGpsTrackingDeviceRepository GpsTrackingDeviceRepository { get; }
        IHiredTransporterDetailRepository Hired_Transporter_DetailsRepository { get; }
        IHiredVehicleDetailRepository Hired_Vehicle_DetailsRepository { get; }
        IHiredVehicleDocumentRepository Hired_Vehicle_DocumentsRepository { get; }
        IDriverDetailRepository DriverDetailRepository { get; }
        IDriverDocumentRepository DriverDocumentRepository { get; }
        ILoansAdvancesRepository LoansAdvancesRepository { get; }
        IFarmersInputRatesSeasonWiseRepository FarmersInputRatesSeasonWiseRepository { get; }
        IFinishedSFOpeningStockRepository FinishedSFOpeningStockRepository { get; }
        IFarmersInputReturnsRepository FarmersInputReturnsRepository { get; }
        IDailyHarvestRepository DailyHarvestRepository { get; }
        IInputToFieldStaffRepository InputToFieldStaffRepository { get; }

        IGSMaterialDetailRepository GSMaterialDetailRepository { get; }
        IGSGroupDetailRepository GSGroupDetailRepository { get; }
        IGSSubGroupDetailRepository GSSubGroupDetailRepository { get; }
        IGSCUOMDetailRepository GSCUOMDetailRepository { get; }

        IManualAttendenceDetailsRepository ManualAttendenceDetailsRepository { get; }

        IAdvCashIssuedToFarmrRepository AdvCashIssuedToFarmrRepository { get; }

        IStoreInternalIndentMasterRepository StoreInternalIndentMasterRepository { get; }
        IStoreInternalIndentDetailRepository StoreInternalIndentDetailRepository { get; }
        IBuyingStaffDetailsRepository BuyingStaffDetailsRepository { get; }

        IGreensAgentSupplierDetailsRepository GreensAgentSupplierDetailsRepository { get; }

        IAgentsGreensReceivingWeighmentRepository AgentsGreensReceivingWeighmentRepository { get; }

        IMediaProcessDetailsRepository MediaProcessDetailsRepository { get; }

        IScheduleDetailRepository ScheduleDetailRepository { get; }
        IMonthlyAttendanceRepository MonthlyAttendanceRepository { get; }

    }
}
