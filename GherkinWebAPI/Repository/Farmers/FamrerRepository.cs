using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using GherkinWebAPI.Core;
using GherkinWebAPI.CustomExceptions;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.Farmers;
using GherkinWebAPI.Models.FarmersInputReturns;
using GherkinWebAPI.Persistence;

namespace GherkinWebAPI.Repository
{
    public class FarmerRepository : RepositoryBase<Farmer>, IFarmerRepository
    {
        private RepositoryContext _context;
        public FarmerRepository(RepositoryContext repositoryContext)
           : base(repositoryContext)
        {
            _context = repositoryContext;
        }
        public async Task<Farmer> AddFarmer(FarmerDetailsDTO farmerDetail)
        {
            try
            {                
                var mapConfig = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<FarmerBankDetailsDTO, FarmerBankDetails>();
                    cfg.CreateMap<FarmerDetailsDTO, Farmer>();
                });
                var mapper = new Mapper(mapConfig);
                var farmer = new Farmer();
                farmer = Mapper.Map<Farmer>(farmerDetail);

                farmer.BankAccountNo = "0";
                farmer.BankBranch = "0";
                farmer.BankIFSC = "0";
                farmer.BankName = "0";


                var farmerBankDetails = new List<FarmerBankDetails>();
                farmerBankDetails = Mapper.Map<List<FarmerBankDetails>>(farmerDetail.FarmerBankDetails);                
                
                var farmers = await _context.Farmers.AsNoTracking().ToListAsync();
                if (farmers.Count > 0)
                {
                    var selectLastFarmer = farmers.OrderByDescending(e => e.ID).Take(1).FirstOrDefault().ID;
                    farmer.Farmer_Code = "FC_" + (Convert.ToInt16(selectLastFarmer) + 1).ToString();
                }

                else
                    farmer.Farmer_Code = "FC_" + "1";

                foreach (var item in farmerBankDetails)
                {
                    item.Farmer_Code = farmer.Farmer_Code;
                }

                _context.Farmers.Add(farmer);

                _context.FarmerBankDetails.AddRange(farmerBankDetails);
                await _context.SaveChangesAsync();

                return farmer;
            }           
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<List<Farmer>> GetAllFarmers()
        {
            return await FindAll().OrderBy(farmer => farmer.FarmerName).ToListAsync();
        }

        public async Task<List<FarmerDocument>> GetFarmerDocumentsbyFarmer(string farmerCode)
        {
            return await _context.FarmerDocuments.Where(f => f.Farmer_Code == farmerCode).ToListAsync();

        }

        public async Task<FarmerDetailsDTO> GetFarmersByCode(string code)
        {
            var farmer = (from f in _context.Farmers
                                 where f.Farmer_Code == code
                                 select new FarmerDetailsDTO
                                 {
                                     ID = f.ID,
                                     Farmer_Code = f.Farmer_Code,
                                     DateOfEntry = f.DateOfEntry,
                                     UserName = f.UserName,
                                     FarmerName = f.FarmerName,
                                     Farmer_Address = f.Farmer_Address,
                                     Country_Code = f.Country_Code,
                                     State_Code = f.State_Code,
                                     District_Code = f.District_Code,
                                     Mandal_Code = f.Mandal_Code,
                                     Village_Code = f.Village_Code,
                                     PINCode = f.PINCode,
                                     AlternativeContactPerson = f.AlternativeContactPerson,
                                     ContactNumber = f.ContactNumber,
                                     AadharCardNo = f.AadharCardNo,
                                     NoOfAcres = f.NoOfAcres,
                                     BankName = f.BankName,
                                     BankBranch = f.BankBranch,
                                     BankAccountNo = f.BankAccountNo,
                                     BankIFSC = f.BankIFSC,
                                     ApprovedBy = f.ApprovedBy,
                                     FarmerBankDetails = (from fb in _context.FarmerBankDetails
                                                          where fb.Farmer_Code == f.Farmer_Code 
                                                          select new FarmerBankDetailsDTO {
                                                              Farmer_Code = fb.Farmer_Code,
                                                              Farmer_Bank_Code = fb.Farmer_Bank_Code,
                                                              Farmer_Bank_Account_No = fb.Farmer_Bank_Account_No,
                                                              Farmer_Account_Holder_Name = fb.Farmer_Account_Holder_Name,
                                                              Farmer_Bank_Name = fb.Farmer_Bank_Name,
                                                              Farmer_Bank_Branch = fb.Farmer_Bank_Branch,
                                                              Farmer_Bank_IFSC = fb.Farmer_Bank_IFSC,
                                                              Preferred_Bank = fb.Preferred_Bank
                                                          }).ToList()

                                 }).FirstOrDefaultAsync();

            return await farmer;
        }

        public async Task<List<FarmersDetail>> GetFarmersByVillageCode(int villageCode)
        {
            var farmersDetail = (from f in _context.Farmers
                                 join c in _context.Countries on f.Country_Code equals c.Country_Code
                                 join s in _context.States on f.State_Code equals s.State_Code
                                 join d in _context.Districts on f.District_Code equals d.District_Code
                                 join m in _context.Mandals on f.Mandal_Code equals m.Mandal_Code
                                 join v in _context.Villages on f.Village_Code equals v.Village_Code
                                 where f.Village_Code == villageCode
                                 select new FarmersDetail
                                 {
                                     ID = f.ID,
                                     FarmerCode = f.Farmer_Code,
                                     DateOfEntry = f.DateOfEntry,
                                     UserName = f.UserName,
                                     FarmerName = f.FarmerName,
                                     FarmerAddress = f.Farmer_Address,
                                     CountryCode = f.Country_Code,
                                     CountryName = c.Country_Name,
                                     StateCode = f.State_Code,
                                     StateName = s.State_Name,
                                     DistrictCode = f.District_Code,
                                     DistrictName = d.District_Name,
                                     MandalCode = f.Mandal_Code,
                                     MandalName = m.Mandal_Name,
                                     VillageCode = f.Village_Code,
                                     VillageName = v.Village_Name,
                                     PINCode = f.PINCode,
                                     AlternativeContactPerson = f.AlternativeContactPerson,
                                     ContactNumber = f.ContactNumber,
                                     AadharCardNo = f.AadharCardNo,
                                     NoOfAcres = f.NoOfAcres,
                                     BankName = f.BankName,
                                     BankBranch = f.BankBranch,
                                     BankAccountNo = f.BankAccountNo,
                                     BankIFSC = f.BankIFSC,
                                     ApprovedBy = f.ApprovedBy
                                  
                                 }).ToListAsync();

            return await farmersDetail;

        }

        public async Task SaveFarmerDocument(FarmerDocument document)
        {
            _context.FarmerDocuments.Add(document);
            await _context.SaveChangesAsync();
        }

        public async Task AddBankAccountDetails(List<FarmerBankDetailsDTO> bankDetailList)
        {
            foreach (var item in bankDetailList)
            {
                if(item.Farmer_Bank_Code == 0)
                {
                    var farmerBankDetails = new FarmerBankDetails();
                    farmerBankDetails = Mapper.Map<FarmerBankDetails>(item);
                    _context.FarmerBankDetails.Add(farmerBankDetails);
                }
                else
                {
                    FarmerBankDetails _farmerBankDetails = _context.FarmerBankDetails.FirstOrDefault(f => f.Farmer_Bank_Code == item.Farmer_Bank_Code);
                    _farmerBankDetails.Farmer_Account_Holder_Name = item.Farmer_Account_Holder_Name;
                    _farmerBankDetails.Farmer_Bank_Account_No = item.Farmer_Bank_Account_No;
                    _farmerBankDetails.Farmer_Bank_Branch = item.Farmer_Bank_Branch;
                    _farmerBankDetails.Farmer_Bank_Code = item.Farmer_Bank_Code;
                    _farmerBankDetails.Farmer_Bank_IFSC = item.Farmer_Bank_IFSC;
                    _farmerBankDetails.Farmer_Bank_Name = item.Farmer_Bank_Name;
                    _farmerBankDetails.Preferred_Bank = item.Preferred_Bank;
                    _context.SaveChanges();
                }               

            }
            await _context.SaveChangesAsync();
        }

        public void UpdateFarmer(string id, FarmerDetailsDTO farmerDetail)
        {
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FarmerBankDetailsDTO, FarmerBankDetails>();
                cfg.CreateMap<FarmerDetailsDTO, Farmer>();
            });
            var mapper = new Mapper(mapConfig);
            var farmer = new Farmer();
            farmer = Mapper.Map<Farmer>(farmerDetail);

            farmer.BankAccountNo = "0";
            farmer.BankBranch = "0";
            farmer.BankIFSC = "0";
            farmer.BankName = "0";


            var farmerBankDetails = new List<FarmerBankDetails>();
            farmerBankDetails = Mapper.Map<List<FarmerBankDetails>>(farmerDetail.FarmerBankDetails);

            Farmer _farmerDetails = _context.Farmers.FirstOrDefault(f => f.Farmer_Code == id);            
            if (_farmerDetails != null)
            {
                _farmerDetails.Farmer_Address = farmer.Farmer_Address;
                _farmerDetails.NoOfAcres = farmer.NoOfAcres;
                _farmerDetails.FarmerName = farmer.FarmerName;
                _farmerDetails.AadharCardNo = farmer.AadharCardNo;
                _farmerDetails.AlternativeContactPerson = farmer.AlternativeContactPerson;
                _farmerDetails.BankName = farmer.BankName;
                _farmerDetails.BankBranch = farmer.BankBranch;
                _farmerDetails.BankAccountNo = farmer.BankAccountNo;
                _farmerDetails.BankIFSC = farmer.BankIFSC;
                _farmerDetails.ContactNumber = farmer.ContactNumber;
                _farmerDetails.Country_Code = farmer.Country_Code;
                _farmerDetails.State_Code = farmer.State_Code;
                _farmerDetails.District_Code = farmer.District_Code;
                _farmerDetails.Mandal_Code = farmer.Mandal_Code;
                _farmerDetails.Village_Code = farmer.Village_Code;
                _farmerDetails.PINCode = farmer.PINCode;
                _farmerDetails.ApprovedBy = farmer.ApprovedBy;
                _farmerDetails.DateOfEntry = farmer.DateOfEntry;
                _context.SaveChanges();

            }
            else
            {
                throw new CustomException("NO FARMER FOUND");
            }

            foreach(var item in farmerDetail.FarmerBankDetails)
            {
                FarmerBankDetails _farmerBankDetails = _context.FarmerBankDetails.FirstOrDefault(f => f.Farmer_Bank_Code == item.Farmer_Bank_Code);
                _farmerBankDetails.Farmer_Account_Holder_Name = item.Farmer_Account_Holder_Name;
                _farmerBankDetails.Farmer_Bank_Account_No = item.Farmer_Bank_Account_No;
                _farmerBankDetails.Farmer_Bank_Branch = item.Farmer_Bank_Branch;
                _farmerBankDetails.Farmer_Bank_Code = item.Farmer_Bank_Code;
                _farmerBankDetails.Farmer_Bank_IFSC = item.Farmer_Bank_IFSC;
                _farmerBankDetails.Farmer_Bank_Name = item.Farmer_Bank_Name;
                _farmerBankDetails.Preferred_Bank = item.Preferred_Bank;
                _context.SaveChanges();

            }
        }

        public async Task<FarmerDocument> GetFarmerDocumentbyID(int Id)
        {
            return await _context.FarmerDocuments.Where(x => x.ID == Id).FirstOrDefaultAsync();
        }
        public async Task DeleteFarmerDocumentbyID(int id)
        {
            FarmerDocument _farmerDocument = await _context.FarmerDocuments.FirstOrDefaultAsync(x => x.ID == id);
            if (_farmerDocument != null)
            {
                _context.FarmerDocuments.Remove(_farmerDocument);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new CustomException("Document not present!");
            }

        }

        public async Task<List<Farmer>> GetFarmerByStateCode(int stateCode)
        {
            return await this._context.Farmers.Where(x => x.State_Code.Equals(stateCode)).ToListAsync();
        }

        public async Task<List<FarmerAndVillage>> GetFarmerListByAreaEmployeePSNumberAndFarmerName(string farmerName, string areaId, string employeeId, string psNumber)
		{
            //var farmerList = await _context.FarmersAgreementDetails.Where(x => x.Area_ID == areaId && x.Employee_ID == employeeId && x.PS_Number == psNumber)
            //  .Select(x => x.Farmer_Code).ToListAsync();

            var farmerList = await _context.FarmersAgreementDetails.Where(x => x.Area_ID == areaId && x.PS_Number == psNumber)
                .Select(x => x.Farmer_Code).ToListAsync();

            var farmerAndVillageList = await _context.Farmers.Where(x => farmerList.Contains(x.Farmer_Code) && x.FarmerName.ToLower().StartsWith(farmerName.ToLower()))
                .Select(x => new FarmerAndVillage() {
                    FarmerCode = x.Farmer_Code,
                    FarmerName = x.FarmerName,
                    FarmerAltContactPerson = x.AlternativeContactPerson,
                    VillageCode = x.Village_Code
            }).ToListAsync();

            var villageCodeList = farmerAndVillageList.Select(y => y.VillageCode).ToList();
            var villageList = await _context.Villages.Where(x => villageCodeList.Contains(x.Village_Code)).ToListAsync();
            farmerAndVillageList.ForEach(x =>
            {
                x.VillageName = villageList.Where(y => y.Village_Code == x.VillageCode).FirstOrDefault().Village_Name;
            });

            return farmerAndVillageList.Distinct().ToList();
        }

        public async Task<List<FarmerAndVillage>> GetFarmerListByAreaEmployeePSNumberAndFarmerAltContactPerson(string farmerAltContactPerson, string areaId, string employeeId, string psNumber)
        {
        //    var farmerList = await _context.FarmersAgreementDetails.Where(x => x.Area_ID == areaId && x.Employee_ID == employeeId && x.PS_Number == psNumber)
        //        .Select(x => x.Farmer_Code).ToListAsync();
            var farmerList = await _context.FarmersAgreementDetails.Where(x => x.Area_ID == areaId && x.PS_Number == psNumber)
                .Select(x => x.Farmer_Code).ToListAsync();

            var farmerAndVillageList = await _context.Farmers.Where(x => farmerList.Contains(x.Farmer_Code) && x.AlternativeContactPerson.ToLower().StartsWith(farmerAltContactPerson.ToLower()))
                .Select(x => new FarmerAndVillage()
                {
                    FarmerCode = x.Farmer_Code,
                    FarmerName = x.FarmerName,
                    FarmerAltContactPerson = x.AlternativeContactPerson,
                    VillageCode = x.Village_Code
                }).ToListAsync();

            var villageCodeList = farmerAndVillageList.Select(y => y.VillageCode).ToList();
            var villageList = await _context.Villages.Where(x => villageCodeList.Contains(x.Village_Code)).ToListAsync();
            farmerAndVillageList.ForEach(x =>
            {
                x.VillageName = villageList.Where(y => y.Village_Code == x.VillageCode).FirstOrDefault().Village_Name;
            });

            return farmerAndVillageList.Distinct().ToList();
        }

        public async Task<List<FarmerAndVillage>> GetFarmerListByAreaAndVillageCodeAndPSNumber(string areaId, string psNumber, int villageCode)
        {
            var farmerList = await _context.FarmersAgreementDetails.Where(x => x.Area_ID == areaId && x.PS_Number == psNumber && x.Village_Code == villageCode).ToListAsync();
            var farmerListwithFarmerCode = farmerList.Select(x => x.Farmer_Code);
            var farmerAndVillageList = await _context.Farmers.Where(x => farmerListwithFarmerCode.Contains(x.Farmer_Code))
                .Select(x => new FarmerAndVillage()
                {
                    FarmerCode = x.Farmer_Code,
                    FarmerName = x.FarmerName,
                    FarmerAltContactPerson = x.AlternativeContactPerson,
                    VillageCode = x.Village_Code
                }).ToListAsync();

            var villageCodeList = farmerAndVillageList.Select(y => y.VillageCode).ToList();
            var villageList = await _context.Villages.Where(x => villageCodeList.Contains(x.Village_Code)).ToListAsync();
            farmerAndVillageList.ForEach(x =>
            {
                x.VillageName = villageList.Where(y => y.Village_Code == x.VillageCode).FirstOrDefault().Village_Name;
                x.AccountNo = farmerList.Where(y => y.Farmer_Code==x.FarmerCode).FirstOrDefault().Farmers_Account_No;
            });

            return farmerAndVillageList.Distinct().ToList();
        }

        public async Task<FarmerAndVillage> GetFarmerByAreaAndPSNumberAndAccountNo(string areaId, string psNumber, string accountNo)
        {
            var farmer = await _context.FarmersAgreementDetails.Where(x => x.Area_ID == areaId && x.PS_Number == psNumber && x.Farmers_Account_No == accountNo).FirstOrDefaultAsync();
            if (farmer != null)
            {
                 var farmerAndVillage = await _context.Farmers.Where(x => x.Farmer_Code == farmer.Farmer_Code)
                    .Select(x => new FarmerAndVillage()
                    {
                        FarmerCode = x.Farmer_Code,
                        FarmerName = x.FarmerName,
                        FarmerAltContactPerson = x.AlternativeContactPerson,
                        VillageCode = x.Village_Code,
                        AccountNo = farmer.Farmers_Account_No
                    }).FirstOrDefaultAsync();

                var villageList = await _context.Villages.Where(x => x.Village_Code == farmerAndVillage.VillageCode).FirstOrDefaultAsync();

                farmerAndVillage.VillageName = villageList.Village_Name;
                return farmerAndVillage;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<FarmerAndVillage>> GetAllFarmersWithAgreementDetail()
        {
            var result = await (from fa in _context.FarmersAgreementDetails
                         join f in _context.Farmers on fa.Farmer_Code equals f.Farmer_Code
                         join v in _context.Villages on fa.Village_Code equals v.Village_Code
                         select new FarmerAndVillage
                         {
                             FarmerCode=fa.Farmer_Code,
                             FarmerName=f.FarmerName,
                             FarmerAltContactPerson=f.AlternativeContactPerson,
                             AccountNo=fa.Farmers_Account_No,
                             VillageCode=fa.Village_Code,
                             VillageName=v.Village_Name,
                             Area_ID=fa.Area_ID,
                             PS_Number=fa.PS_Number
                         }).ToListAsync();

            return result;
        }
    }
}