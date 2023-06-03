use gherkin

create table dbo.MainMenu(Id int identity(1,1) not null primary key,ModuleName varchar(100) not null,ModuleShortCut varchar(10),CreatedDate datetime not null,CreatedBy varchar(100) not null,ModifiedDate datetime ,ModifiedBy varchar(100))

drop table dbo.MainMenu

Insert into dbo.MainMenu values ('Organisation Management','OCR',GETDATE(),'Sajeed Shaik',null,null)
Insert into dbo.MainMenu values ('Administration & Users','ADM',GETDATE(),'Sajeed Shaik',null,null)

Insert into dbo.MainMenu values ('Human Resource Management','HRM',GETDATE(),'Sajeed Shaik',null,null)

Insert into dbo.MainMenu values ('Plantations & Harvesting','PHM',GETDATE(),'Sajeed Shaik',null,null)

Insert into dbo.MainMenu values ('Purchase Management','PM',GETDATE(),'Sajeed Shaik',null,null)

Insert into dbo.MainMenu values ('Production Process Filling & Ware House Division','PPM',GETDATE(),'Sajeed Shaik',null,null)

Insert into dbo.MainMenu values ('Sales & CRM','SCRM',GETDATE(),'Sajeed Shaik',null,null)
Insert into dbo.MainMenu values ('Quality Control Management','QCM',GETDATE(),'Sajeed Shaik',null,null)
Insert into dbo.MainMenu values ('Packing & General Material Management','PGS',GETDATE(),'Sajeed Shaik',null,null)
Insert into dbo.MainMenu values ('Accounts & Finance Management','AFM',GETDATE(),'Sajeed Shaik',null,null)
Insert into dbo.MainMenu values ('Management Information Systems','MIS',GETDATE(),'Sajeed Shaik',null,null)
Insert into dbo.MainMenu values ('User Manual & Help','UMH',GETDATE(),'Sajeed Shaik',null,null)


select * from dbo.MainMenu

create table dbo.ModuleMenu(Id int identity(1,1) not null primary key,ModuleName varchar(100) not null,ModuleShortCut varchar(10),CreatedDate datetime not null,CreatedBy varchar(100) not null,ModifiedDate datetime ,ModifiedBy varchar(100),ParentId int foreign key references dbo.MainMenu(Id))

Insert into dbo.ModuleMenu values ('HRM Masters','HRM-M',GETDATE(),'Sajeed Shaik',null,null,3)

Insert into dbo.ModuleMenu values ('HRM Transactions','HRM-T',GETDATE(),'Sajeed Shaik',null,null,3)

Insert into dbo.ModuleMenu values ('HRM Reports','HRM-R',GETDATE(),'Sajeed Shaik',null,null,3)

Insert into dbo.ModuleMenu values ('PHM Masters','PHM-M',GETDATE(),'Sajeed Shaik',null,null,4)

Insert into dbo.ModuleMenu values ('PHM Transactions','PHM-T',GETDATE(),'Sajeed Shaik',null,null,4)

Insert into dbo.ModuleMenu values ('PHM Reports','PHM-R',GETDATE(),'Sajeed Shaik',null,null,4)

Insert into dbo.ModuleMenu values ('PM Masters','PM-M',GETDATE(),'Sajeed Shaik',null,null,5)

Insert into dbo.ModuleMenu values ('PM Transactions','PM-T',GETDATE(),'Sajeed Shaik',null,null,5)

Insert into dbo.ModuleMenu values ('PM Reports','PM-R',GETDATE(),'Sajeed Shaik',null,null,5)

Insert into dbo.ModuleMenu values ('PM Masters','PM-M',GETDATE(),'Sajeed Shaik',null,null,5)

Insert into dbo.ModuleMenu values ('PM Transactions','PM-T',GETDATE(),'Sajeed Shaik',null,null,5)

Insert into dbo.ModuleMenu values ('PM Reports','PM-R',GETDATE(),'Sajeed Shaik',null,null,5)

Insert into dbo.ModuleMenu values ('PPM Masters','PPM-M',GETDATE(),'Sajeed Shaik',null,null,6)

Insert into dbo.ModuleMenu values ('PPM Transactions','PPM-T',GETDATE(),'Sajeed Shaik',null,null,6)

Insert into dbo.ModuleMenu values ('PPM Reports','PPM-R',GETDATE(),'Sajeed Shaik',null,null,6)


Insert into dbo.ModuleMenu values ('SCRM Masters','SCRM-M',GETDATE(),'Sajeed Shaik',null,null,7)

Insert into dbo.ModuleMenu values ('SCRM Transactions','SCRM-T',GETDATE(),'Sajeed Shaik',null,null,7)

Insert into dbo.ModuleMenu values ('SCRM Reports','SCRM-R',GETDATE(),'Sajeed Shaik',null,null,7)


Insert into dbo.ModuleMenu values ('QCM Masters','QCM-M',GETDATE(),'Sajeed Shaik',null,null,8)

Insert into dbo.ModuleMenu values ('QCM Transactions','QCM-T',GETDATE(),'Sajeed Shaik',null,null,8)

Insert into dbo.ModuleMenu values ('QCM Reports','QCM-R',GETDATE(),'Sajeed Shaik',null,null,8)


Insert into dbo.ModuleMenu values ('PGS Masters','PGS-M',GETDATE(),'Sajeed Shaik',null,null,9)

Insert into dbo.ModuleMenu values ('PGS Transactions','PGS-T',GETDATE(),'Sajeed Shaik',null,null,9)

Insert into dbo.ModuleMenu values ('PGS Reports','PGS-R',GETDATE(),'Sajeed Shaik',null,null,9)


Insert into dbo.ModuleMenu values ('AFM Masters','AFM-M',GETDATE(),'Sajeed Shaik',null,null,10)

Insert into dbo.ModuleMenu values ('AFM Transactions','AFM-T',GETDATE(),'Sajeed Shaik',null,null,10)

Insert into dbo.ModuleMenu values ('AFM Reports','AFM-R',GETDATE(),'Sajeed Shaik',null,null,10)



Insert into dbo.ModuleMenu values ('MIS Masters','AFM-M',GETDATE(),'Sajeed Shaik',null,null,11)

Insert into dbo.ModuleMenu values ('MIS Transactions','AFM-T',GETDATE(),'Sajeed Shaik',null,null,11)

Insert into dbo.ModuleMenu values ('MIS Reports','AFM-R',GETDATE(),'Sajeed Shaik',null,null,11)


Insert into dbo.ModuleMenu values ('UMH Masters','UMH-M',GETDATE(),'Sajeed Shaik',null,null,12)

Insert into dbo.ModuleMenu values ('UMH Transactions','UMH-T',GETDATE(),'Sajeed Shaik',null,null,12)

Insert into dbo.ModuleMenu values ('UMH Reports','UMH-R',GETDATE(),'Sajeed Shaik',null,null,12)






select * from dbo.ModuleMenu



create table dbo.SubMenu(Id int identity(1,1) not null primary key,ModuleName varchar(100) not null,ModuleShortCut varchar(10) unique,CreatedDate datetime not null,CreatedBy varchar(100) not null,ModifiedDate datetime ,ModifiedBy varchar(100),ParentId int foreign key references dbo.ModuleMenu(Id))

drop table dbo.SubMenu

-- HRM Masters
Insert into dbo.SubMenu values ('Employee Details','EMPD',GETDATE(),'Sajeed Shaik',null,null,1)
Insert into dbo.SubMenu values ('Experience Details','ED',GETDATE(),'Sajeed Shaik',null,null,1)
Insert into dbo.SubMenu values ('Salary And CTC Approval','SACTCA',GETDATE(),'Sajeed Shaik',null,null,1)
Insert into dbo.SubMenu values ('Salary Deductions','SAD',GETDATE(),'Sajeed Shaik',null,null,1)
Insert into dbo.SubMenu values ('Loans & Advances','LA',GETDATE(),'Sajeed Shaik',null,null,1)
Insert into dbo.SubMenu values ('ESIC Rates','ER',GETDATE(),'Sajeed Shaik',null,null,1)
Insert into dbo.SubMenu values ('Provident Fund Rates','PFR',GETDATE(),'Sajeed Shaik',null,null,1)
Insert into dbo.SubMenu values ('Professional Tax Rates','PTR',GETDATE(),'Sajeed Shaik',null,null,1)
Insert into dbo.SubMenu values ('TDS Calculations','TDSC',GETDATE(),'Sajeed Shaik',null,null,1)
Insert into dbo.SubMenu values ('IT rebate Details','ITRD',GETDATE(),'Sajeed Shaik',null,null,1)
Insert into dbo.SubMenu values ('Bonus Calculations','BC',GETDATE(),'Sajeed Shaik',null,null,1)
Insert into dbo.SubMenu values ('Shift Details','SHD',GETDATE(),'Sajeed Shaik',null,null,1)
Insert into dbo.SubMenu values ('Employee Transfer','ET',GETDATE(),'Sajeed Shaik',null,null,1)
Insert into dbo.SubMenu values ('Bank Details','BD',GETDATE(),'Sajeed Shaik',null,null,1)
Insert into dbo.SubMenu values ('Statuary Holiday Master','SHM',GETDATE(),'Sajeed Shaik',null,null,1)

-- HRM Transactions

Insert into dbo.SubMenu values ('Shift Management','SM',GETDATE(),'Sajeed Shaik',null,null,2)
Insert into dbo.SubMenu values ('Manual Attendance','MA',GETDATE(),'Sajeed Shaik',null,null,2)
Insert into dbo.SubMenu values ('Leave Request - Employee','LREM',GETDATE(),'Sajeed Shaik',null,null,2)
Insert into dbo.SubMenu values ('Leave Process - Manager','LPM',GETDATE(),'Sajeed Shaik',null,null,2)
Insert into dbo.SubMenu values ('Attendance Finalization','ATTDF',GETDATE(),'Sajeed Shaik',null,null,2)
Insert into dbo.SubMenu values ('Loan & Adavance Approvals','LAA',GETDATE(),'Sajeed Shaik',null,null,2)
Insert into dbo.SubMenu values ('Deductions Adjustment','DEA',GETDATE(),'Sajeed Shaik',null,null,2)
Insert into dbo.SubMenu values ('Salary Computation & Finalization (Days/Monthly)','SCF',GETDATE(),'Sajeed Shaik',null,null,2)
Insert into dbo.SubMenu values ('Appraisal Process(180/ 360 Degrees)','AP',GETDATE(),'Sajeed Shaik',null,null,2)
Insert into dbo.SubMenu values ('Salary Increment','SI',GETDATE(),'Sajeed Shaik',null,null,2)
Insert into dbo.SubMenu values ('Incentives','ICN',GETDATE(),'Sajeed Shaik',null,null,2)
Insert into dbo.SubMenu values ('PF Return','PFR',GETDATE(),'Sajeed Shaik',null,null,2)
Insert into dbo.SubMenu values ('ESIC Return','ESIR',GETDATE(),'Sajeed Shaik',null,null,2)
Insert into dbo.SubMenu values ('Professional Tax Return','PFTR',GETDATE(),'Sajeed Shaik',null,null,2)
Insert into dbo.SubMenu values ('TDS Computation','TDSCOMP',GETDATE(),'Sajeed Shaik',null,null,2)
Insert into dbo.SubMenu values ('TDS Return','TDSR',GETDATE(),'Sajeed Shaik',null,null,2)
Insert into dbo.SubMenu values ('Bonus Approval','BA',GETDATE(),'Sajeed Shaik',null,null,2)

-- HRM Reports

Insert into dbo.SubMenu values ('Department Designation','DEPD',GETDATE(),'Sajeed Shaik',null,null,3)
Insert into dbo.SubMenu values ('Employee Information','EMPI' ,GETDATE(),'Sajeed Shaik',null,null,3)
Insert into dbo.SubMenu values ('Salary Details','SALD' ,GETDATE(),'Sajeed Shaik',null,null,3)
Insert into dbo.SubMenu values ('Deduction Details','DEDD',GETDATE(),'Sajeed Shaik',null,null,3)
Insert into dbo.SubMenu values ('Attendance Register','ATTDR',GETDATE(),'Sajeed Shaik',null,null,3)
Insert into dbo.SubMenu values ('Leave Application','LEAA',GETDATE(),'Sajeed Shaik',null,null,3)
Insert into dbo.SubMenu values ('Leave Register','LEAR',GETDATE(),'Sajeed Shaik',null,null,3)
Insert into dbo.SubMenu values ('Salary Register','SALR',GETDATE(),'Sajeed Shaik',null,null,3)
Insert into dbo.SubMenu values ('Summary Wage Register (Days / Monthly)','SUWR',GETDATE(),'Sajeed Shaik',null,null,3)
Insert into dbo.SubMenu values ('Employee Wise Salary details','EMPSD',GETDATE(),'Sajeed Shaik',null,null,3)
Insert into dbo.SubMenu values ('Pay slip','PS',GETDATE(),'Sajeed Shaik',null,null,3)
Insert into dbo.SubMenu values ('ESIC Reports','ESICR',GETDATE(),'Sajeed Shaik',null,null,3)
Insert into dbo.SubMenu values ('Statuary Rates','SATR',GETDATE(),'Sajeed Shaik',null,null,3)
Insert into dbo.SubMenu values ('ESI Register','ESIREG',GETDATE(),'Sajeed Shaik',null,null,3)
Insert into dbo.SubMenu values ('ESI Deduction Summary','ESIDEDS',GETDATE(),'Sajeed Shaik',null,null,3)
Insert into dbo.SubMenu values ('PF Reports','PFREP',GETDATE(),'Sajeed Shaik',null,null,3)
Insert into dbo.SubMenu values ('PF Register','PFREG',GETDATE(),'Sajeed Shaik',null,null,3)
Insert into dbo.SubMenu values ('Consolidated PF Register','CONPFREG',GETDATE(),'Sajeed Shaik',null,null,3)
Insert into dbo.SubMenu values ('Employee Wise Register','EMPR',GETDATE(),'Sajeed Shaik',null,null,3)
Insert into dbo.SubMenu values ('PT Reports','PTREP',GETDATE(),'Sajeed Shaik',null,null,3)
Insert into dbo.SubMenu values ('PT Register','PTR',GETDATE(),'Sajeed Shaik',null,null,3)
Insert into dbo.SubMenu values ('Employee Wise Register','EMPWREG',GETDATE(),'Sajeed Shaik',null,null,3)
Insert into dbo.SubMenu values ('Leave Register','LEAVR',GETDATE(),'Sajeed Shaik',null,null,3)
Insert into dbo.SubMenu values ('Leave Status','LEAVS',GETDATE(),'Sajeed Shaik',null,null,3)
Insert into dbo.SubMenu values ('Salary Increment','SALI',GETDATE(),'Sajeed Shaik',null,null,3)
Insert into dbo.SubMenu values ('Employee Wise History','EMPWHIS',GETDATE(),'Sajeed Shaik',null,null,3)
Insert into dbo.SubMenu values ('Approvals (180/ 360 Degree)','APPOV',GETDATE(),'Sajeed Shaik',null,null,3)
Insert into dbo.SubMenu values ('Department employee Wise Salary','DEPEMPWSAL',GETDATE(),'Sajeed Shaik',null,null,3)
Insert into dbo.SubMenu values ('Income Tax Details','INCTAXD',GETDATE(),'Sajeed Shaik',null,null,3)
Insert into dbo.SubMenu values ('IT Rates','ITR',GETDATE(),'Sajeed Shaik',null,null,3)
Insert into dbo.SubMenu values ('IT Deductions(TDS)','ITD',GETDATE(),'Sajeed Shaik',null,null,3)
Insert into dbo.SubMenu values ('TDS Return','TDSR',GETDATE(),'Sajeed Shaik',null,null,3)
Insert into dbo.SubMenu values ('Bonus Register','BONR',GETDATE(),'Sajeed Shaik',null,null,3)
Insert into dbo.SubMenu values ('Holiday List Details','HOLD',GETDATE(),'Sajeed Shaik',null,null,3)
Insert into dbo.SubMenu values ('Bank Account Details','BAD',GETDATE(),'Sajeed Shaik',null,null,3)
Insert into dbo.SubMenu values ('Loans & Advances','LOA',GETDATE(),'Sajeed Shaik',null,null,3)


--PHM Masters

Insert into dbo.SubMenu values ('Area & Villages','ARAVILL',GETDATE(),'Sajeed Shaik',null,null,4)
Insert into dbo.SubMenu values ('Field Staff','FS',GETDATE(),'Sajeed Shaik',null,null,4)
Insert into dbo.SubMenu values ('Crops & Schemes','CS',GETDATE(),'Sajeed Shaik',null,null,4)
Insert into dbo.SubMenu values ('Farmers Details','FAMD',GETDATE(),'Sajeed Shaik',null,null,4)
Insert into dbo.SubMenu values ('Crop Rates','CROR',GETDATE(),'Sajeed Shaik',null,null,4)
Insert into dbo.SubMenu values ('Harvesting Stages','HARVS',GETDATE(),'Sajeed Shaik',null,null,4)

--PHM Transactions

Insert into dbo.SubMenu values ('Farmers Agreement','FAMA',GETDATE(),'Sajeed Shaik',null,null,5)
Insert into dbo.SubMenu values ('Plantations Scheduling','PALNS',GETDATE(),'Sajeed Shaik',null,null,5)
Insert into dbo.SubMenu values ('Feed & Inputs Transfer','FEEIT',GETDATE(),'Sajeed Shaik',null,null,5)
Insert into dbo.SubMenu values ('Feed & Inputs Return','FEER',GETDATE(),'Sajeed Shaik',null,null,5)
Insert into dbo.SubMenu values ('Harvest Material Gate Entry','HARMGE',GETDATE(),'Sajeed Shaik',null,null,5)
Insert into dbo.SubMenu values ('Farmers Advance Indent','FAMAI',GETDATE(),'Sajeed Shaik',null,null,5)
Insert into dbo.SubMenu values ('Farmers Input Bill Finalization','FAMIBF',GETDATE(),'Sajeed Shaik',null,null,5)

--PHM Reports

Insert into dbo.SubMenu values ('Harvesting Material Receipt Report','HARMRR',GETDATE(),'Sajeed Shaik',null,null,6)
Insert into dbo.SubMenu values ('Input Materail Consumption Report','INPMCR',GETDATE(),'Sajeed Shaik',null,null,6)
Insert into dbo.SubMenu values ('Farmer wise Harvesting Report','FARWHR',GETDATE(),'Sajeed Shaik',null,null,6)
Insert into dbo.SubMenu values ('Farmer wise Summary Report','FARWSP',GETDATE(),'Sajeed Shaik',null,null,6)
Insert into dbo.SubMenu values ('Feed & Input Transfers Report','FEEITR',GETDATE(),'Sajeed Shaik',null,null,6)
Insert into dbo.SubMenu values ('Feed & Input Returns Report','FEEIRR',GETDATE(),'Sajeed Shaik',null,null,6)
Insert into dbo.SubMenu values ('Harvesting Analysis Report','HARAR',GETDATE(),'Sajeed Shaik',null,null,6)
Insert into dbo.SubMenu values ('Area wise Analysis Report','AWAR',GETDATE(),'Sajeed Shaik',null,null,6)
Insert into dbo.SubMenu values ('Village wise Analysis Report','VILLWAR',GETDATE(),'Sajeed Shaik',null,null,6)