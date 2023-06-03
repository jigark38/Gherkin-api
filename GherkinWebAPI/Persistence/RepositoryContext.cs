using GherkinWebAPI.Entities;
using GherkinWebAPI.Entities.BranchIndent;
using GherkinWebAPI.Entities.HarvestStage;
using GherkinWebAPI.Entities.MaterialInward;
using GherkinWebAPI.Entities.RMStock;
using GherkinWebAPI.Entities.SowingFarming;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.Accounts_Master;
using GherkinWebAPI.Models.BatchProduction;
using GherkinWebAPI.Models.ConsigneeBuyersModel;
using GherkinWebAPI.Models.CullingDetails;
using GherkinWebAPI.Models.DailyInputAndFeedingDetails;
using GherkinWebAPI.Models.Employee;
using GherkinWebAPI.Models.GoodsReceiptNote;
using GherkinWebAPI.Models.GreenReception;
using GherkinWebAPI.Models.GRNAndMaterialClassification;
using GherkinWebAPI.Models.GRPAndroid.ProdProcessBOM;
using GherkinWebAPI.Models.HarvestDeatils;
using GherkinWebAPI.Models.InputTransferDetails;
using GherkinWebAPI.Models.Login;
using GherkinWebAPI.Models.Mandals;
using GherkinWebAPI.Models.MaterialOutward;
using GherkinWebAPI.Models.MediaBatchDetails;
using GherkinWebAPI.Models.PackageOfPracticeModel;
using GherkinWebAPI.Models.ProvidentFund;
using GherkinWebAPI.Models.PurchageMgmt;
using GherkinWebAPI.Models.ShiftDetail;
using GherkinWebAPI.Models.User;
using GherkinWebAPI.Request;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using GherkinWebAPI.Models.ProfessionalTaxRates;
using GherkinWebAPI.Models.GreensTransportVehicleSchedules;
using GherkinContext;
using GherkinWebAPI.Models.DriverDetail;
using GherkinWebAPI.Models.DriverDocument;
using GherkinWebAPI.Models.TransportVehicleManagement;
using GherkinWebAPI.Models.AttendanceDetails;
using GherkinWebAPI.Models.LoansAndAdvancesDetails;
using GherkinWebAPI.Models.FarmersInputReturns;
using GherkinWebAPI.Models.DailyHarvestDetails;
using GherkinWebAPI.Models.InputToFieldStaff;
using GherkinWebAPI.Models.StoresMasterDetails;
using GherkinWebAPI.Models.ManualAttendence;
using GherkinWebAPI.Models.MaterialIndentByDepartment;
using GherkinWebAPI.Models.GreensAgentSupplierDetails;
using GherkinWebAPI.Models.FarmerAccountDetailsFinalization;
using GherkinWebAPI.Models.AgentsGreensRecWeighment;

namespace GherkinWebAPI.Persistence
{
    public class RepositoryContext : GherkinDbContext
    {
        public RepositoryContext()
        {
            Database.SetInitializer<RepositoryContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Area>().ToTable("Harvest_Area_Details", "dbo");
            modelBuilder.Entity<Country>().ToTable("Country_Details", "dbo");
            modelBuilder.Entity<State>().ToTable("State_Details", "dbo");
            modelBuilder.Entity<District>().ToTable("District_Details", "dbo");
            //modelBuilder.Entity<MandalDetails>().ToTable("Mandal_Details", "dbo");
            modelBuilder.Entity<Mandal>().ToTable("Mandal_Details", "dbo");
            modelBuilder.Entity<Village>().ToTable("Village_Details", "dbo");
            modelBuilder.Entity<HarvestAreaVillage>().ToTable("Harvest_Area_Village_Details", "dbo");
            modelBuilder.Entity<ContainerPackingDetails>().ToTable("Container_Packing_Details", "dbo");
            modelBuilder.Entity<GSCUomDetails>().ToTable("GSC_UOM_Details", "dbo");
            modelBuilder.Entity<RawMaterialMaster>().ToTable("Raw_Material_Group_Master", "dbo");
            modelBuilder.Entity<CropGroup>().ToTable("Crop_Group_Details", "dbo");
            modelBuilder.Entity<Crop>().ToTable("Crop_Name_Details", "dbo");
            modelBuilder.Entity<CropScheme>().ToTable("Crop_Schemes_Details", "dbo");
            modelBuilder.Entity<RawMaterialDetails>().ToTable("Raw_Material_Details", "dbo");
            modelBuilder.Entity<SupplierDetails>().ToTable("Supplier_Information_Details", "dbo");
            modelBuilder.Entity<CropRate>().ToTable("Crop_Rate_Details", "dbo");
            modelBuilder.Entity<CropAssociationRate>().ToTable("Crop_Effective_Assocation_Rates", "dbo");
            modelBuilder.Entity<Department>().ToTable("Department_Information_Details", "dbo");//.Ignore(d => d.SubDepartments);
            modelBuilder.Entity<SubDepartment>().ToTable("Sub_Department_Information_Details", "dbo");//.Ignore(s => s.Designations);//.Property(e => e.Sub_Department_Code).
                                                                                                      //HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            modelBuilder.Entity<Designation>().ToTable("Designation_Information_Details", "dbo");//.Ignore(des => des.Employees);
            modelBuilder.Entity<Employee>().ToTable("Employee_Information_Details", "dbo");
            modelBuilder.Entity<EmployeeAttendanceUpdatedDetails>().ToTable("Employee_Attendance_Updated_Details", "dbo");
            modelBuilder.Entity<EmployeeBankAccountDetails>().ToTable("Employee_Bank_Account_Details", "dbo");
            modelBuilder.Entity<EmployeeBankDetailsMaster>().ToTable("Employee_Bank_Account_Master", "dbo");
            modelBuilder.Entity<EmployeeDocument>().ToTable("Employee_Documents_Details", "dbo");
            modelBuilder.Entity<FieldStaffDetails>().ToTable("Harvest_Area_Field_Staff_Details", "dbo");
            modelBuilder.Entity<ConsigneeBuyersDetails>().ToTable("Consignee_Buyers_Master", "dbo");
            modelBuilder.Entity<CountryOverseas>().ToTable("Country_Overseas", "dbo");
            modelBuilder.Entity<StateOverseas>().ToTable("State_Overseas", "dbo");
            modelBuilder.Entity<CityOverseas>().ToTable("City_Overseas", "dbo");
            modelBuilder.Entity<OrganisationOfficeLocationDetails>().ToTable("Organisation_Offices_Locations_Details", "dbo");
            modelBuilder.Entity<PlantationSchedule>().ToTable("Plantation_Sch_Details", "dbo");
            modelBuilder.Entity<FarmersAgreementDetail>().ToTable("Farmers_Agreement_Details", "dbo");
            modelBuilder.Entity<FarmersAccountSettlementDetail>().ToTable("Farmers_Account_Settlement_Details", "dbo");
            modelBuilder.Entity<FarmersAgreementSizeDetail>().ToTable("Farmers_Agreement_Size_Details", "dbo");
            modelBuilder.Entity<Farmer>().ToTable("Farmer_Information_Details", "dbo");
            modelBuilder.Entity<FarmerBankDetails>().ToTable("Farmer_Bank_Details", "dbo");
            modelBuilder.Entity<FarmerDocument>().ToTable("Farmer_Documents", "dbo");
            modelBuilder.Entity<RMStockDetails>().ToTable("RM_Stock_Details", "dbo");
            modelBuilder.Entity<RMStockLotDetails>().ToTable("RM_Stock_Lot_Details", "dbo");
            //modelBuilder.Entity<PlantationSchDetails>().ToTable("Plantation_Sch_Details", "dbo");
            modelBuilder.Entity<SkillInformation>().ToTable("Skills_Infomation_Details", "dbo");

            modelBuilder.Entity<Currency>().ToTable("Currency_Overseas", "dbo");
            modelBuilder.Entity<DocumentUpload>().ToTable("C_B_Documents", "dbo");
            modelBuilder.Entity<ProductGroup>().ToTable("Finished_Product_Group", "dbo");
            modelBuilder.Entity<ProductDetails>().ToTable("Finished_Product_Details", "dbo");
            modelBuilder.Entity<GradeDetails>().ToTable("FP_Grades_Details", "dbo");
            modelBuilder.Entity<Organisation>().ToTable("Organisation_Name_Details", "dbo");

            modelBuilder.Entity<Management>().ToTable("Org_Management_Details", "dbo");

            modelBuilder.Entity<Place>().ToTable("Place_Details", "dbo");

            //modelBuilder.Entity<OfficeLocation>().ToTable("Organisation_Offices_Locatons_Details", "dbo");
            modelBuilder.Entity<FarmersAgreementDetail>().ToTable("Farmers_Agreement_Details", "dbo");
            modelBuilder.Entity<FarmersAgreementSizeDetail>().ToTable("Farmers_Agreement_Size_Details", "dbo");
            modelBuilder.Entity<Contractor>().ToTable("Contractor_Information_Details", "dbo");
            modelBuilder.Entity<EmployeePayment>().ToTable("Employee_Payment_Details", "dbo");
            modelBuilder.Entity<HarvestStageMaster>().ToTable("harvest_stages_master", "dbo");
            modelBuilder.Entity<HarvestStageDetails>().ToTable("Harvest_Stages_Details", "dbo");
            modelBuilder.Entity<PackagePracticeMaster>().ToTable("HBOM_Package_Practice_Master", "dbo");
            modelBuilder.Entity<PackagePracticeDivision>().ToTable("HBOM_Package_Practice_Division", "dbo");
            modelBuilder.Entity<PackagePracticeMaterials>().ToTable("HBOM_Package_Practice_Materials", "dbo");
            modelBuilder.Entity<BranchIndentDetails>().ToTable("Branch_Indent_Details", "dbo");
            modelBuilder.Entity<BranchIndentMaterialDetails>().ToTable("Branch_Indent_Material_Details", "dbo");
            modelBuilder.Entity<BankAccountDetails>().ToTable("Org_Bank_Details", "dbo");
            modelBuilder.Entity<BankAccountClose>().ToTable("Bank_Account_Closing_Details", "dbo");
            modelBuilder.Entity<RMStockBranchDetails>().ToTable("RM_Branch_Stock_Details", "dbo");
            modelBuilder.Entity<RMStockBranchQuantityDetails>().ToTable("RM_Branch_Stock_Quantity_Details", "dbo");

            modelBuilder.Entity<AccountsMaster>().ToTable("Accounts_Master");
            modelBuilder.Entity<AccountsGroupMaster>().ToTable("Accounts_Group_Master");
            modelBuilder.Entity<AccountsSubGroupMaster>().ToTable("Accounts_Sub_Group_Master");
            modelBuilder.Entity<AccountHeadMaster>().ToTable("Account_Head_Master");
            modelBuilder.Entity<PurchaseOrderDetail>().ToTable("Purchase_Order_Details", "dbo");
            // modelBuilder.Entity<RawMaterialDetail>().ToTable("", "dbo");
            modelBuilder.Entity<RMPOMaterialCondition>().ToTable("RM_PO_Material_Condition", "dbo");
            modelBuilder.Entity<RMPOMaterialDetail>().ToTable("RM_PO_Material_Details", "dbo");
            modelBuilder.Entity<AreaMaterialReceivedDetailsModel>().ToTable("Area_Material_Received_Details", "dbo");
            modelBuilder.Entity<AreaMRInwardDetails>().ToTable("Area_MR_Inward_Details", "dbo");

            //  modelBuilder.Entity<RowMaterialGroupMaster>().ToTable("", "dbo");
            modelBuilder.Entity<MainMenu>().ToTable("MainMenu", "dbo");
            modelBuilder.Entity<ModuleMenu>().ToTable("ModuleMenu", "dbo");
            modelBuilder.Entity<SubMenu>().ToTable("SubMenu", "dbo");
            //modelBuilder.Entity<Customer>().ToTable("Users", "dbo");
            modelBuilder.Entity<MasterMenu>().ToTable("MasterMenu", "dbo");
            modelBuilder.Entity<Customer>().ToTable("User_Permission_Master", "dbo");
            modelBuilder.Entity<Permission>().ToTable("User_Permissions", "dbo");


            modelBuilder.Entity<ProformaInvoiceDetails>().ToTable("Proforma_Invoice_Details", "dbo");
            modelBuilder.Entity<ProductionDetails>().ToTable("Proforma_Invoice_Products", "dbo");

            modelBuilder.Entity<MaterialInwardEntity>().ToTable("Material_Inward_Gate_Pass", "dbo");
            modelBuilder.Entity<RMGRNDetail>().ToTable("RM_GRN_Details", "dbo");
            modelBuilder.Entity<RMGRNMaterialDetail>().ToTable("RM_GRN_Material_Details", "dbo");
            modelBuilder.Entity<BatchMaterialDetails>().ToTable("Batch_Material_Details", "dbo");
            modelBuilder.Entity<RMMaterialTotalCostDetail>().ToTable("RM_Material_Total_Cost_Details", "dbo");
            modelBuilder.Entity<RMPOIndentDetail>().ToTable("RM_PO_Indent_Details", "dbo");
            modelBuilder.Entity<PurchageReturnDetail>().ToTable("Purchase_Return_Details", "dbo");
            modelBuilder.Entity<RMInputMaterialTransferDetail>().ToTable("RM_Input_Material_Transfer_Details", "dbo");
            modelBuilder.Entity<PurchageReturnMaterialDetail>().ToTable("Purchase_Return_Material_Details", "dbo");
            modelBuilder.Entity<OutwardGatePassDetail>().ToTable("Outward_Gate_Pass_Details", "dbo");
            modelBuilder.Entity<ProductionProcessDetails>().ToTable("Production_Process_Details", "dbo");

            modelBuilder.Entity<User>().ToTable("Users", "login");
            //modelBuilder.Entity<LoginDetail>().ToTable("LoginDetails", "login");
            modelBuilder.Entity<ProductionStandardBOM>().ToTable("Production_Standard_BOM", "dbo");
            modelBuilder.Entity<ProductionProcessMaterialDetails>().ToTable("Production_Process_Material_Details", "dbo");
            modelBuilder.Entity<MediaProcessDetails>().ToTable("Media_Process_Details", "dbo");
            modelBuilder.Entity<MaterialOutwardDetails>().ToTable("Material_Outwad_Details", "dbo");
            modelBuilder.Entity<InputTransferDetails>().ToTable("RM_Input_Transfer_Details", "dbo");
            modelBuilder.Entity<HarvestProcurementDetails>().ToTable("Harvest_Procurement_Details", "dbo");
            modelBuilder.Entity<HarvestFarmersDetails>().ToTable("Harvest_Farmers_Details", "dbo");
            modelBuilder.Entity<HarvestCratewiseDetails>().ToTable("Harvest_Quantity_Cratewise_Details", "dbo");
            modelBuilder.Entity<HarvestGradewiseDetails>().ToTable("Harvest_Quantity_Gradewise_Details", "dbo");
            modelBuilder.Entity<FarmersInputConsumptionDetails>().ToTable("Farmers_Input_Consumption_Details", "dbo");
            modelBuilder.Entity<FarmersMaterialIssueDetails>().ToTable("Farmers_Material_Issue_Details", "dbo");
            modelBuilder.Entity<HarvestGRN>().ToTable("Harvest_GRN_Details", "dbo");
            modelBuilder.Entity<HarvestGRNFarmer>().ToTable("Harvest_GRN_Farmers_Details", "dbo");
            modelBuilder.Entity<HarvestGRNCrate>().ToTable("Harvest_GRN_Crates_Details", "dbo");
            modelBuilder.Entity<GreenReceptionQualityCheck>().ToTable("Greens_Reception_Quality_Check", "dbo");
            modelBuilder.Entity<GreenReceptionQualityDetails>().ToTable("Greens_Reception_Quality_Details", "dbo");
            modelBuilder.Entity<SupplierDocumentDetails>().ToTable("Supplier_Org_Documents", "dbo");
            modelBuilder.Entity<SowingFarmingDetails>().ToTable("Sowing_Farming_Details", "dbo");
            modelBuilder.Entity<FarmingStageDetails>().ToTable("Farming_Stage_Details", "dbo");

            modelBuilder.Entity<GreensGradingQuantityDetails>().ToTable("Greens_Grading_Quantity_Details", "dbo");
            modelBuilder.Entity<GreensGradingWeighmentDetails>().ToTable("Greens_Grading_Weighment_Details", "dbo");
            modelBuilder.Entity<GreensGradingInwardDetails>().ToTable("Greens_Grading_Inward_Details", "dbo");
            modelBuilder.Entity<GreensGradedHarvestGRNDetails>().ToTable("Greens_Graded_Harvest_GRN_Details", "dbo");


            modelBuilder.Entity<HarvestGRNInwardDetails>().ToTable("Harvest_GRN_Inward_Details", "dbo");
            modelBuilder.Entity<HarvestGRNInwardMaterialDetails>().ToTable("Harvest_GRN_Inward_Material_Details", "dbo");
            modelBuilder.Entity<HarvestGRNIMWeightDetails>().ToTable("Harvest_GRN_IM_Weight_Details", "dbo");
            modelBuilder.Entity<BatchScheduleGreensGRNDetails>().ToTable("Batch_Schedule_Greens_GRN_Details", "dbo");

            modelBuilder.Entity<ProductionScheduleDetails>().ToTable("Production_Schedule_Details", "dbo");
            modelBuilder.Entity<DirectProductionSchedule>().ToTable("PS_Through_Direct_Order_Qty_Details", "dbo");
            modelBuilder.Entity<SalesProductionSchedule>().ToTable("PS_Through_Sales_Order_Qty_Details", "dbo");
            modelBuilder.Entity<MediaBatchMaterialDetails>().ToTable("Media_Batch_Materials_Details", "dbo");
            modelBuilder.Entity<BatchScheduleOrderProduction>().ToTable("Batch_Schedule_Order_Production", "dbo");
            modelBuilder.Entity<BatchScheduleDummyProduction>().ToTable("Batch_Schedule_Dummy_Production", "dbo");
            modelBuilder.Entity<BatchScheduleDetails>().ToTable("Batch_Schedule_Details", "dbo");
            modelBuilder.Entity<GreensCullingBarrelsWeightDetails>().ToTable("Greens_Culling_Barrels_Weight_Details", "dbo");
            modelBuilder.Entity<GreensCullingInwardDetails>().ToTable("Greens_Culling_Inward_Details", "dbo");
            modelBuilder.Entity<MediaBatchProductionDetails>().ToTable("Media_Batch_Production_Details", "dbo");
            modelBuilder.Entity<RMUom>().ToTable("UOM_Details", "dbo");
            modelBuilder.Entity<ProvidentFundRateDetails>().ToTable("PF_Contribution_Master", "dbo");
            modelBuilder.Entity<ShiftDetailMaster>().ToTable("Shift_Details_Master", "dbo");

            modelBuilder.Entity<YearlyCalendar>().ToTable("Yearly_Holidays_Master", "dbo");
            modelBuilder.Entity<WeeklyHoliday>().ToTable("Weekly_Holidays_Details", "dbo");
            modelBuilder.Entity<StatutoryHoliday>().ToTable("Yearly_Statutory_Holidays_Details", "dbo");
            modelBuilder.Entity<WeekDay>().ToTable("Weekly_Weekdays_Master", "dbo");
            modelBuilder.Entity<ShiftStatusDetail>().ToTable("Shift_Status_Details", "dbo");
            modelBuilder.Entity<ESICRate>().ToTable("ESI_Contribution_Master", "dbo");
            modelBuilder.Entity<ProfessionalTaxMaster>().ToTable("Professional_Tax_Master", "dbo");
            modelBuilder.Entity<ProfessionalTaxSlabsDetail>().ToTable("Professional_Tax_Slabs_Details", "dbo");
            modelBuilder.Entity<GreensTransportVehicleSchedule>().ToTable("Greens_Transport_Vehicle_Schedule", "dbo");
            modelBuilder.Entity<GreensTransportMaterialDetail>().ToTable("Greens_Transport_Material_Details", "dbo");
            modelBuilder.Entity<ReturnableGatePassDetail>().ToTable("Returnable_Gate_Pass_Details", "dbo");

            modelBuilder.Entity<OwnVehiclesDetails>().ToTable("Own_Vehicles_Details", "dbo");
            modelBuilder.Entity<OwnVehicleDocuments>().ToTable("Own_Vehicle_Documents", "dbo");
            modelBuilder.Entity<GPSTrackingDevices>().ToTable("GPS_Tracking_Devices", "dbo");
            modelBuilder.Entity<HiredTransporterDetail>().ToTable("Hired_Transporter_Details", "dbo");
            modelBuilder.Entity<HiredVehicleDetail>().ToTable("Hired_Vehicle_Details", "dbo");
            modelBuilder.Entity<HiredVehicleDocument>().ToTable("Hired_Vehicle_Documents", "dbo");

            modelBuilder.Entity<Role>().ToTable("Roles", "login");
            modelBuilder.Entity<DriverDetail>().ToTable("Driver_Details", "dbo");
            modelBuilder.Entity<DriverDocument>().ToTable("Driver_Documents", "dbo");
            modelBuilder.Entity<BiometricUserLog>().ToTable("Biometric_User_Logs", "dbo");
            modelBuilder.Entity<LoansAdvancesDetail>().ToTable("Loans_Advances_Details", "dbo");
            modelBuilder.Entity<FarmersInputsIssueRatesMaster>().ToTable("Farmers_Inputs_Issue_Rates_Master", "dbo");
            modelBuilder.Entity<FarmersInputsAreaDetail>().ToTable("Farmers_Inputs_Area_Details", "dbo");
            modelBuilder.Entity<FarmersInputsMaterialRate>().ToTable("Farmers_Inputs_Material_Rates", "dbo");
            modelBuilder.Entity<GreensProcurement>().ToTable("Greens_Procurement_Details", "dbo");
            modelBuilder.Entity<GreensFarmersDetail>().ToTable("Greens_Farmers_Details", "dbo");
            modelBuilder.Entity<GreensQuantityCratewiseDetail>().ToTable("Greens_Quantity_Cratewise_Details", "dbo");
            modelBuilder.Entity<GreensQuantityCountwiseDetail>().ToTable("Greens_Quantity_Countwise_Details", "dbo");

            modelBuilder.Entity<FinishedSFStockProductDetails>().ToTable("Finished_SF_Stock_Product_Details", "dbo");
            modelBuilder.Entity<FinishedSFStockQuantityDetails>().ToTable("Finished_SF_Stock_Quantity_Details", "dbo");

            modelBuilder.Entity<AdvanceCashIssuedToFarmersModel>().ToTable("Advance_Cash_Issued_To_Farmers","dbo");

            modelBuilder.Entity<FarmersInputsMaterialMaster>().ToTable("Farmers_Inputs_Material_Master", "dbo");
            modelBuilder.Entity<FarmersInputsMaterialDetail>().ToTable("Farmers_Inputs_Material_Details", "dbo");

            modelBuilder.Entity<ManualAttendenceMaster>().ToTable("FS_Manual_Attendance_Master", "dbo");
            modelBuilder.Entity<ManualAttendenceDetails>().ToTable("FS_Manual_Attendance_Details", "dbo");

            modelBuilder.Entity<HarvestAreaBuyingStaffDetails>().ToTable("Harvest_Area_Buying_Staff_Details", "dbo");



            modelBuilder.Entity<GSMaterialDetail>().ToTable("GS_Material_Details", "dbo");
            modelBuilder.Entity<GSGroupDetail>().ToTable("GS_Group_Details", "dbo");
            modelBuilder.Entity<GSSubGroupDetail>().ToTable("GS_Sub_Group_Details", "dbo");

            modelBuilder.Entity<StoreInternalIndentMaster>().ToTable("Store_Internal_Indent_Master", "dbo");
            modelBuilder.Entity<StoreInternalIndentDetail>().ToTable("Store_Internal_Indent_Details", "dbo");

            modelBuilder.Entity<CWHarvestGRNCountWeightDetails>().ToTable("CW_Harvest_GRN_Count_Weight_Details", "dbo");
            modelBuilder.Entity<CWHarvestBuyerWeighingDetails>().ToTable("CW_Harvest_Buyer_Weighing_Details", "dbo");
            modelBuilder.Entity<CWHarvestGRNWeightSummaryDetails>().ToTable("CW_Harvest_GRN_Weight_Summary_Details", "dbo");

            modelBuilder.Entity<SupplierInformationDetails>().ToTable("Agent_Supplier_Information_Details", "dbo");
            modelBuilder.Entity<AgentBankDetails>().ToTable("Agent_Bank_Details", "dbo");
            modelBuilder.Entity<AgentOrgDocuments>().ToTable("Agent_Org_Documents", "dbo");

            modelBuilder.Entity<GreensAgentReceivedDetails>().ToTable("Greens_Agent_Received_Details", "dbo");
            modelBuilder.Entity<GreensAgentDespCountWeightDetails>().ToTable("Greens_Agent_Desp_Count_Weight_Details", "dbo");
            modelBuilder.Entity<GreensAgentGradesActualDetails>().ToTable("Greens_Agent_Grades_Actual_Details", "dbo");
            modelBuilder.Entity<GreensAgentActualWeightDetails>().ToTable("Greens_Agent_Actual_Weight_Details", "dbo");
            modelBuilder.Entity<BatchScheduleDrumsBarcodeDetails>().ToTable("Batch_Schedule_Drums_Barcode_Details", "dbo");
            modelBuilder.Entity<AttendanceSummaryDetails>().ToTable("Attendance_Summary_Details", "dbo");

            modelBuilder.Entity<MonthlyEmployeesSalariesFinalization>().ToTable("Monthly_Employees_Salaries_Finalization", "dbo");

            modelBuilder.Entity<MonthlyEmployerContributions>().ToTable("Monthly_Employer_Contributions", "dbo");
           
        }
        
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<UserPermissions> UserPermissions { get; set; }

        public DbSet<MainMenu> MainMenu { get; set; }

        public DbSet<ModuleMenu> ModuleMenu { get; set; }
        public DbSet<SubMenu> SubMenu { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }

        public DbSet<Place> Places { get; set; }
        public DbSet<District> Districts { get; set; }
        //public DbSet<MandalDetails> Mandals { get; set; }
        public DbSet<Mandal> Mandals { get; set; }
        public DbSet<Village> Villages { get; set; }
        public DbSet<HarvestAreaVillage> HarvestAreaVillages { get; set; }
        public DbSet<RawMaterialMaster> RawMaterialGroupMaster { get; set; }
        public DbSet<CropGroup> CropGroups { get; set; }
        public DbSet<Crop> Crops { get; set; }
        public DbSet<CropScheme> CropSchemes { get; set; }
        public DbSet<RawMaterialDetails> RawMaterialDetails { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<SubDepartment> SubDepartments { get; set; }
        public DbSet<SupplierDetails> SupplierDetails { get; set; }
        public DbSet<CropRate> CropRates { get; set; }
        public DbSet<CropAssociationRate> CropAssociationRates { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeAttendanceUpdatedDetails> EmployeeAttendanceUpdatedDetails { get; set; }
        public DbSet<EmployeeBankAccountDetails> EmployeeBankAccountDetails { get; set; }
        public DbSet<ContainerPackingDetails> ContainerPackingDetails { get; set; }
        public DbSet<GSCUomDetails> GSCUomDetails { get; set; }

        public DbSet<EmployeeBankDetailsMaster> EmployeeBankAccountMasterDetails { get; set; }

        public DbSet<Designation> Designations { get; set; }

        public DbSet<SkillInformation> SkillInformations { get; set; }

        public DbSet<EmployeeDocument> EmployeeDocument { get; set; }
        public DbSet<FieldStaffDetails> FieldStaffDetails { get; set; }
        public DbSet<ConsigneeBuyersDetails> Consignee_Buyers_Master { get; set; }
        public DbSet<CountryOverseas> countriesoverseas { get; set; }

        public DbSet<StateOverseas> StateOverseas { get; set; }
        public DbSet<CityOverseas> CityOverseas { get; set; }


        public DbSet<Currency> CurrenyOverseas { get; set; }
        public DbSet<DocumentUpload> DocumentUploads { get; set; }


        public DbSet<OrganisationOfficeLocationDetails> OrganisationOfficeLocationDetails { get; set; }

        public DbSet<PlantationSchedule> PlantationSchedules { get; set; }
        public DbSet<FarmersAccountSettlementDetail> FarmersAccountSettlementDetails { get; set; }
        public DbSet<FarmersAgreementDetail> FarmersAgreementDetails { get; set; }
        public DbSet<FarmersAgreementSizeDetail> FarmersAgreementSizeDetails { get; set; }
        public DbSet<Farmer> Farmers { get; set; }
        public DbSet<FarmerBankDetails> FarmerBankDetails { get; set; }
        public DbSet<FarmerDocument> FarmerDocuments { get; set; }

        public DbSet<RMStockDetails> RMStockDetails { get; set; }

        public DbSet<RMStockLotDetails> RMStockLotDetails { get; set; }
        //public DbSet<PlantationSchDetails> PlantationSchDetails { get; set; }

        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<ProductDetails> ProductDetails { get; set; }

        public DbSet<GradeDetails> gradeDetails { get; set; }
        public DbSet<Organisation> Organisations { get; set; }

        public DbSet<Management> Managements { get; set; }

        //public DbSet<OfficeLocation> OfficeLocations { get; set; }
        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<EmployeePayment> EmployeePayments { get; set; }

        public DbSet<HarvestStageMaster> HarvestStageMaster { get; set; }
        public DbSet<HarvestStageDetails> HarvestStageDetails { get; set; }

        public DbSet<PackagePracticeMaster> packagePracticeMasters { get; set; }
        public DbSet<PackagePracticeDivision> packagePracticeDivisions { get; set; }
        public DbSet<PackagePracticeMaterials> packagePracticeMaterials { get; set; }

        public DbSet<BranchIndentDetails> branchIndentDetails { get; set; }
        public DbSet<BranchIndentMaterialDetails> branchIndentMaterialDetails { get; set; }
        public DbSet<BankAccountDetails> BankAccountDetails { get; set; }
        public DbSet<BankAccountClose> BankAccountCloses { get; set; }

        public DbSet<RMStockBranchDetails> RMStockBranchDetails { get; set; }
        public DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public DbSet<RMPOMaterialCondition> RMPOMaterialConditions { get; set; }
        public DbSet<RMPOMaterialDetail> RMPOMaterialDetails { get; set; }

        public DbSet<RMStockBranchQuantityDetails> RMStockBranchQuantityDetails { get; set; }
        public DbSet<AccountsMaster> AccountsMasters { get; set; }
        public DbSet<AccountsGroupMaster> AccountsGroupMasters { get; set; }
        public DbSet<AccountsSubGroupMaster> AccountsSubGroupMasters { get; set; }
        public DbSet<AccountHeadMaster> AccountHeadMasters { get; set; }

        public DbSet<MaterialInwardEntity> MaterialInwardEntity { get; set; }


        public DbSet<ProformaInvoiceDetails> ProformaInvoiceDetails { get; set; }

        public DbSet<ProductionDetails> ProductionDetails { get; set; }
        public DbSet<RMGRNDetail> RMGRNDetails { get; set; }
        public DbSet<RMGRNMaterialDetail> RMGRNMaterialDetails { get; set; }
        public DbSet<BatchMaterialDetails> BatchMaterialDetails { get; set; }
        public DbSet<RMMaterialTotalCostDetail> RMMaterialTotalCostDetails { get; set; }
        public DbSet<PurchageReturnDetail> PurchageReturnDetails { get; set; }
        public DbSet<RMPOIndentDetail> RMPOIndentDetails { get; set; }
        public DbSet<RMInputMaterialTransferDetail> RMInputMaterialTransferDetails { get; set; }
        public DbSet<PurchageReturnMaterialDetail> PurchageReturnMaterialDetails { get; set; }
        public DbSet<OutwardGatePassDetail> OutwardGatePassDetails { get; set; }
        public DbSet<ProductionProcessDetails> ProductionProcessDetail { get; set; }

        public DbSet<User> UserDetail { get; set; }

        public DbSet<MasterMenu> MasterMenu { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        //public DbSet<UserPermissionsMaster> UserPermissionsMaster { get; set; }

        //public DbSet<LoginDetail> LoginDetail { get; set; }
        public DbSet<ProductionStandardBOM> productionStandardBOMs { get; set; }
        public DbSet<ProductionProcessMaterialDetails> ProductionProcessMaterialDetails { get; set; }
        public DbSet<MediaProcessDetails> MediaProcessDetails { get; set; }

        public DbSet<MaterialOutwardDetails> MaterialOutwardDetails { get; set; }

        public DbSet<InputTransferDetails> InputTransferDetails { get; set; }
        public DbSet<AreaMaterialReceivedDetailsModel> AreaMaterialReceivedDetails { get; set; }
        public DbSet<AreaMRInwardDetails> AreaMRInwardDetails { get; set; }

        public DbSet<HarvestProcurementDetails> HarvestProcurementDetails { get; set; }

        public DbSet<HarvestFarmersDetails> HarvestFarmersDetails { get; set; }

        public DbSet<HarvestCratewiseDetails> HarvestCratewiseDetails { get; set; }

        public DbSet<HarvestGradewiseDetails> HarvestGradewiseDetails { get; set; }
        public DbSet<FarmersInputConsumptionDetails> FarmersInputConsumptionDetails { get; set; }
        public DbSet<FarmersMaterialIssueDetails> FarmersMaterialIssueDetails { get; set; }
        public DbSet<HarvestGRN> HarvestGRNs { get; set; }
        public DbSet<HarvestGRNFarmer> HarvestGRNFarmers { get; set; }
        public DbSet<HarvestGRNCrate> HarvestGRNCrates { get; set; }
        public DbSet<GreenReceptionQualityCheck> greenReceptionQualityChecks { get; set; }
        public DbSet<GreenReceptionQualityDetails> GreenReceptionQualityDetails { get; set; }
        public DbSet<SupplierDocumentDetails> SupplierDocumentDetails { get; set; }

        public DbSet<HarvestGRNInwardDetails> HarvestGRNInwardDetails { get; set; }
        public DbSet<HarvestGRNInwardMaterialDetails> HarvestGRNInwardMaterialDetails { get; set; }
        public DbSet<HarvestGRNIMWeightDetails> HarvestGRNIMWeightDetails { get; set; }
        public DbSet<SowingFarmingDetails> SowingFarmingDetails { get; set; }
        public DbSet<FarmingStageDetails> FarmingStageDetails { get; set; }

        public DbSet<GreensGradingQuantityDetails> GreensGradingQuantityDetails { get; set; }
        public DbSet<GreensGradingWeighmentDetails> GreensGradingWeighmentDetails { get; set; }
        public DbSet<GreensGradingInwardDetails> GreensGradingInwardDetails { get; set; }
        public DbSet<GreensGradedHarvestGRNDetails> GreensGradedHarvestGRNDetails { get; set; }


        public DbSet<BatchScheduleGreensGRNDetails> BatchScheduleGreensGRNDetails { get; set; }

        public DbSet<ProductionScheduleDetails> ProductionScheduleDetails { get; set; }

        public DbSet<DirectProductionSchedule> DirectProductionSchedule { get; set; }

        public DbSet<SalesProductionSchedule> SalesProductionSchedule { get; set; }
        public DbSet<BatchScheduleDetails> BatchScheduleDetails { get; set; }
        public DbSet<MediaBatchMaterialDetails> MediaBatchMaterialDetails { get; set; }
        public DbSet<BatchScheduleOrderProduction> BatchScheduleOrderProductions { get; set; }
        public DbSet<BatchScheduleDummyProduction> BatchScheduleDummyProductions { get; set; }

        public DbSet<GreensCullingInwardDetails> GreensCullingInwardDetails { get; set; }

        public DbSet<GreensCullingBarrelsWeightDetails> GreensCullingBarrelsWeightDetails { get; set; }
        public DbSet<MediaBatchProductionDetails> MediaBatchProductionDetails { get; set; }
        public DbSet<ProvidentFundRateDetails> ProvidentFundRateDetails { get; set; }
        public DbSet<ShiftDetailMaster> ShiftDetailsMaster { get; set; }
        public DbSet<ShiftStatusDetail> ShiftStatusDetails { get; set; }
        public DbSet<ESICRate> ESICRates { get; set; }
        public DbSet<RMUom> RMUom { get; set; }
        public DbSet<ProfessionalTaxMaster> ProfessionalTaxMasters { get; set; }
        public DbSet<ProfessionalTaxSlabsDetail> ProfessionalTaxSlabsDetails { get; set; }

        public DbSet<YearlyCalendar> YearlyCalendar { get; set; }
        public DbSet<WeeklyHoliday> WeeklyHolidays { get; set; }
        public DbSet<StatutoryHoliday> StatutoryHolidays { get; set; }
        public DbSet<WeekDay> WeekDays { get; set; }

        public DbSet<GreensTransportVehicleSchedule> GreensTransportVehicleSchedules { get; set; }
        public DbSet<GreensTransportMaterialDetail> GreensTransportMaterialDetails { get; set; }
        public DbSet<ReturnableGatePassDetail> ReturnableGatePassDetails { get; set; }

        public DbSet<OwnVehiclesDetails> OwnVehiclesDetails { get; set; }
        public DbSet<OwnVehicleDocuments> OwnVehicleDocuments { get; set; }
        public DbSet<GPSTrackingDevices> GPSTrackingDevices { get; set; }
        public DbSet<HiredTransporterDetail> Hired_Transporter_Details { get; set; }
        public DbSet<HiredVehicleDetail> Hired_Vehicle_Details { get; set; }
        public DbSet<HiredVehicleDocument> Hired_Vehicle_Documents { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<DriverDetail> DriverDetails { get; set; }
        public DbSet<BiometricUserLog> BiometricUserLogs { get; set; }
        public DbSet<DriverDocument> DriverDocuments { get; set; }
        public DbSet<LoansAdvancesDetail> LoansAdvancesDetails { get; set; }

        public DbSet<FarmersInputsIssueRatesMaster> FarmersInputsIssueRatesMaster { get; set; }
        public DbSet<GreensProcurement> GreensProcurements { get; set; }
        public DbSet<GreensFarmersDetail> GreensFarmersDetails { get; set; }
        public DbSet<GreensQuantityCratewiseDetail> GreensQuantityCratewiseDetails { get; set; }
        public DbSet<GreensQuantityCountwiseDetail> GreensQuantityCountwiseDetails { get; set; }
        public DbSet<FarmersInputsAreaDetail> FarmersInputsAreaDetails { get; set; }
        public DbSet<FarmersInputsMaterialRate> FarmersInputsMaterialRates { get; set; }
        public DbSet<InputsIssuedToFieldstaffDetails> InputsIssuedToFieldstaffDetails { get; set; }
        public DbSet<InputsIssuedToFieldstaffMaterials> InputsIssuedToFieldstaffMaterials { get; set; }

        public DbSet<FinishedSFStockProductDetails> FinishedSFStockProductDetails { get; set; }
        public DbSet<FinishedSFStockQuantityDetails> FinishedSFStockQuantityDetails { get; set; }

        public DbSet<AdvanceCashIssuedToFarmersModel> AdvanceCashIssuedToFarmers { get; set; }

        public DbSet<FarmersInputsMaterialMaster> FarmersInputsMaterialMaster { get; set; }
        public DbSet<FarmersInputsMaterialDetail> FarmersInputsMaterialDetail { get; set; }
        public DbSet<ManualAttendenceMaster> ManualAttendenceMasters { get; set; }
        public DbSet<ManualAttendenceDetails> ManualAttendenceDetails { get; set; }

        public DbSet<HarvestAreaBuyingStaffDetails> HarvestAreaBuyingStaffDetails { get; set; }

        public DbSet<GSMaterialDetail> GSMaterialDetail { get; set; }
        public DbSet<GSGroupDetail> GSGroupDetail { get; set; }
        public DbSet<GSSubGroupDetail> GSSubGroupDetail { get; set; }
        //public DbSet<GSCUOMDetail> GSCUOMDetail { get; set; }

        public DbSet<StoreInternalIndentMaster> StoreInternalIndentMaster { get; set; }
        public DbSet<StoreInternalIndentDetail> StoreInternalIndentDetail { get; set; }
        public DbSet<CWHarvestGRNCountWeightDetails> CWHarvestGRNCountWeightDetails { get; set; }
        public DbSet<CWHarvestBuyerWeighingDetails> CWHarvestBuyerWeighingDetails { get; set; }
        public DbSet<CWHarvestGRNWeightSummaryDetails> CWHarvestGRNWeightSummaryDetails { get; set; }

        public DbSet<SupplierInformationDetails> SupplierInformationDetails { get; set; }
        public DbSet<AgentBankDetails> AgentBankDetails { get; set; }
        public DbSet<AgentOrgDocuments> AgentOrgDocuments { get; set; }

        public DbSet<GreensAgentReceivedDetails> GreensAgentReceivedDetails { get; set; }
        public DbSet<GreensAgentDespCountWeightDetails> GreensAgentDespCountWeightDetails { get; set; }
        public DbSet<GreensAgentGradesActualDetails> GreensAgentGradesActualDetails { get; set; }
        public DbSet<GreensAgentActualWeightDetails> GreensAgentActualWeightDetails { get; set; }

        public DbSet<BatchScheduleDrumsBarcodeDetails> BatchScheduleDrumsBarcodeDetails { get; set; }

        public DbSet<AttendanceSummaryDetails> AttendanceSummaryDetails { get; set; }

        public DbSet<MonthlyEmployeesSalariesFinalization> MonthlyEmployeesSalariesFinalizations { get; set; }
        public DbSet<MonthlyEmployerContributions> MonthlyEmployerContributions { get; set; }
    }
}
