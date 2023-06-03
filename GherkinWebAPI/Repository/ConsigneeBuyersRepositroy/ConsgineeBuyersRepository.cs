using AutoMapper;
using GherkinWebAPI.Core;
using GherkinWebAPI.CustomExceptions;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;

namespace GherkinWebAPI.Repository
{
    public class ConsgineeBuyersRepository : RepositoryBase<ConsigneeBuyersDetails>, IConsgineeBuyersRepository
    {
        private RepositoryContext _context;

        public ConsgineeBuyersRepository(RepositoryContext repositoryContext)
               : base(repositoryContext)
        {
            this._context = repositoryContext;
        }

        /// <summary>
        /// The GetAllConsgineeBuyersDetails
        /// </summary>
        /// <returns>The <see cref="Task{List{ConsgineeBuyersDto}}"/></returns>
        public async Task<List<ConsgineeBuyersDto>> GetAllConsgineeBuyersDetails()
        {
            List<ConsgineeBuyersDto> list = new List<ConsgineeBuyersDto>();
            list = await (from CBCode in _context.Consignee_Buyers_Master
                          select new ConsgineeBuyersDto
                          {
                              ID = CBCode.ID,
                              C_B_Code = CBCode.C_B_Code,
                              Cosng_Buyer_Type = CBCode.Cosng_Buyer_Type,
                              C_B_Name = CBCode.C_B_Name,
                              C_B_Address = CBCode.C_B_Address,
                              W_Country_Id = CBCode.W_Country_Id,
                              W_State_Id = CBCode.W_State_Id,
                              W_City_Id = CBCode.W_City_Id,
                              W_Pincode = CBCode.W_Pincode,
                              Country_Area_Code = CBCode.Country_Area_Code,
                              Managment_Name = CBCode.Managment_Name,
                              Mang_Mobile_No = CBCode.Mang_Mobile_No,
                              Office_No = CBCode.Office_No,
                              Alternate_No = CBCode.Alternate_No,
                              Mail_id = CBCode.Mail_id,
                              Alt_Mail_id = CBCode.Alt_Mail_id,
                              License_No = CBCode.License_No,
                              Credit_Limited = CBCode.Credit_Limited,
                              Currency_Code = CBCode.Currency_Code,
                              Created_Date = CBCode.Created_Date,
                              Modified_Date = CBCode.Modified_Date,
                              documentUploadeds = (from f in _context.DocumentUploads
                                                   where CBCode.C_B_Code == f.C_B_Code
                                                   select new DcoumentUplodDto
                                                   {
                                                       Document_No = f.Document_No,
                                                       C_B_Code = f.C_B_Code,
                                                       Document_Name = f.Document_Name,
                                                       Document_Details = f.Document_Details,
                                                       ImagePreappendText = f.ImagePreappendText
                                                   }).ToList()


                          }).ToListAsync();


            foreach (var item in list)
            {
                foreach (var item2 in item.documentUploadeds)
                {
                    if (item2.Document_Details != null)
                    {
                        item2.DocDetails = Convert.ToBase64String(item2.Document_Details);
                        item2.DocDetails = item2.ImagePreappendText + item2.DocDetails;
                    }
                }
            }
            return list;
        }

        public async Task<List<ConsgineeBuyersDto>> GetConsgineeNameByConsType(string consgType)
        {
            List<ConsgineeBuyersDto> comments = null;
            comments = await (from CBCode in _context.Consignee_Buyers_Master
                              where CBCode.Cosng_Buyer_Type == consgType
                              select new ConsgineeBuyersDto
                              {
                                  C_B_Code = CBCode.C_B_Code,
                                  C_B_Name = CBCode.C_B_Name

                              }).ToListAsync();
            return comments;

        }

        public async Task<ConsgineeBuyersDto> GetConsgineeBuyersDetailsId(string consgType, string C_B_Code)
        {
            ConsgineeBuyersDto comments = null;
            try
            {

                comments = await (from CBCode in _context.Consignee_Buyers_Master
                                  where CBCode.C_B_Code == C_B_Code && CBCode.Cosng_Buyer_Type == consgType
                                  select new ConsgineeBuyersDto
                                  {
                                      ID = CBCode.ID,
                                      C_B_Code = CBCode.C_B_Code,
                                      Cosng_Buyer_Type = CBCode.Cosng_Buyer_Type,
                                      C_B_Name = CBCode.C_B_Name,
                                      C_B_Address = CBCode.C_B_Address,
                                      W_Country_Id = CBCode.W_Country_Id,
                                      W_Country_Name = (from name in _context.countriesoverseas.Where(c => c.W_Country_Id == CBCode.W_Country_Id)
                                                        select name.W_Country_Name).FirstOrDefault(),
                                      W_State_Id = CBCode.W_State_Id,
                                      W_State_Name = (from name in _context.StateOverseas.Where(c => c.W_State_id == CBCode.W_State_Id)
                                                      select name.W_State_Name).FirstOrDefault(),
                                      W_City_Id = CBCode.W_City_Id,
                                      W_City_Name = (from name in _context.CityOverseas.Where(c => c.W_City_Id == CBCode.W_City_Id)
                                                     select name.W_City_Name).FirstOrDefault(),
                                      W_Pincode = CBCode.W_Pincode,
                                      Country_Area_Code = CBCode.Country_Area_Code,
                                      Managment_Name = CBCode.Managment_Name,
                                      Mang_Mobile_No = CBCode.Mang_Mobile_No,
                                      Office_No = CBCode.Office_No,
                                      Alternate_No = CBCode.Alternate_No,
                                      Mail_id = CBCode.Mail_id,
                                      Alt_Mail_id = CBCode.Alt_Mail_id,
                                      License_No = CBCode.License_No,
                                      Credit_Limited = CBCode.Credit_Limited,
                                      Currency_Code = CBCode.Currency_Code,
                                      Currency_Name = (from name in _context.CurrenyOverseas.Where(c => c.Currency_Code == CBCode.Currency_Code)
                                                       select name.Currency_Name).FirstOrDefault(),
                                      Created_Date = CBCode.Created_Date,
                                      Modified_Date = CBCode.Modified_Date,
                                      documentUploadeds = (from f in _context.DocumentUploads
                                                           where CBCode.C_B_Code == f.C_B_Code
                                                           select new DcoumentUplodDto
                                                           {
                                                               Document_No = f.Document_No,
                                                               C_B_Code = f.C_B_Code,
                                                               Document_Name = f.Document_Name,
                                                               Document_Details = f.Document_Details,
                                                               ImagePreappendText = f.ImagePreappendText
                                                           }).ToList()


                                  }).SingleOrDefaultAsync();

                //item2.DocDetails = Convert.ToBase64String(item2.DocDetailstest);
                if (comments != null)
                {
                    var temp = comments.documentUploadeds.FirstOrDefault();

                    if (temp.Document_Details != null)
                    {
                        temp.DocDetails = Convert.ToBase64String(temp.Document_Details);
                        temp.DocDetails = temp.ImagePreappendText + temp.DocDetails;
                    }

                }
            }
            catch (DbEntityValidationException e)
            {
                throw new CustomException("Invalid Data");
            }
            return comments;

        }

        public async Task<ConsigneeBuyersDetails> AddConsgineeBuyersDetails(ConsigneeBuyersDetails consigneeBuyersDetails)
        {

            int? selectMaxDeptId = await _context.Consignee_Buyers_Master.MaxAsync(e => (int?)e.ID);
            if (selectMaxDeptId != null)
                consigneeBuyersDetails.C_B_Code = "CB_" + Convert.ToString(selectMaxDeptId + 1);
            else
                consigneeBuyersDetails.C_B_Code = "CB_" + "1";
            consigneeBuyersDetails.Created_Date = DateTime.Now;

            var result = _context.Consignee_Buyers_Master.Add(consigneeBuyersDetails);
            await _context.SaveChangesAsync();
            return result;
        }


        public async Task<DocumentUpload> AddDocumentDetails(DocumentUpload Documentupoload)
        {

            int? selectMaxDeptId = await _context.DocumentUploads.MaxAsync(e => (int?)e.Id);
            if (selectMaxDeptId != null)
                Documentupoload.Document_No = "C_B_Doc_" + Convert.ToString(selectMaxDeptId + 1);
            else
                Documentupoload.Document_No = "C_B_Doc_" + "1";

            var result = _context.DocumentUploads.Add(Documentupoload);
            await _context.SaveChangesAsync();
            return result;
        }


        public async Task<ConsgineeBuyersDto> UpdateConsgineeDetails(string id, ConsgineeBuyersDto consigneeBuyersViewModel)
        {
            var detail = await this._context.Consignee_Buyers_Master.FirstOrDefaultAsync(x => x.C_B_Code == id);
            //try
            //{
            if (detail != null)
            {
                detail.ID = consigneeBuyersViewModel.ID;
                detail.Cosng_Buyer_Type = consigneeBuyersViewModel.Cosng_Buyer_Type;
                detail.C_B_Name = consigneeBuyersViewModel.C_B_Name;
                detail.C_B_Code = consigneeBuyersViewModel.C_B_Code;
                detail.C_B_Address = consigneeBuyersViewModel.C_B_Address;
                detail.W_Country_Id = consigneeBuyersViewModel.W_Country_Id;
                detail.W_State_Id = consigneeBuyersViewModel.W_State_Id;
                detail.W_City_Id = consigneeBuyersViewModel.W_City_Id;
                detail.W_Pincode = consigneeBuyersViewModel.W_Pincode;
                detail.Country_Area_Code = consigneeBuyersViewModel.Country_Area_Code;
                detail.Managment_Name = consigneeBuyersViewModel.Managment_Name;
                detail.Mang_Mobile_No = consigneeBuyersViewModel.Mang_Mobile_No;
                detail.Office_No = consigneeBuyersViewModel.Office_No;
                detail.Alternate_No = consigneeBuyersViewModel.Alternate_No;
                detail.Mail_id = consigneeBuyersViewModel.Mail_id;
                detail.Alt_Mail_id = consigneeBuyersViewModel.Alt_Mail_id;
                detail.License_No = consigneeBuyersViewModel.License_No;
                detail.Credit_Limited = consigneeBuyersViewModel.Credit_Limited;
                detail.Currency_Code = consigneeBuyersViewModel.Currency_Code;
                detail.Modified_Date = DateTime.Now;
                if (consigneeBuyersViewModel.documentUploadeds != null && consigneeBuyersViewModel.documentUploadeds.Count > 0)
                {
                    foreach (var item in consigneeBuyersViewModel.documentUploadeds)
                    {
                        byte[] docImage = null;
                        if (!string.IsNullOrEmpty(item.DocDetails))
                        {
                            string ImgStr = Regex.Replace(item.DocDetails, "^data:(image|pdf|application)\\/[a-zA-Z]*-?[a-zA-Z]+;base64,", string.Empty, RegexOptions.IgnoreCase);
                            docImage = Convert.FromBase64String(ImgStr);

                            var regexMatches = Regex.Matches(item.DocDetails, "(^data:(image|pdf|application)\\/[a-zA-Z]*-?[a-zA-Z]+;base64,)", RegexOptions.IgnoreCase);
                            if (regexMatches != null)
                            {
                                var tt = regexMatches[0].Groups[0].Value;
                                item.ImagePreappendText = tt;
                            }

                        }
                        var DocumentUpload = await this._context.DocumentUploads.FirstOrDefaultAsync(x => x.C_B_Code == id);

                        var DocDetails = new DocumentUpload();
                        DocDetails.Document_Name = item.Document_Name;
                        DocDetails.Document_No = DocumentUpload.Document_No;
                        DocDetails.Id = DocumentUpload.Id;
                        DocDetails.C_B_Code = id;
                        DocDetails.Document_Details = docImage;
                        DocDetails.ImagePreappendText = item.ImagePreappendText;
                        _context.DocumentUploads.AddOrUpdate(DocDetails);
                        //  await _repositoryContext.SaveChangesAsync();
                        await _context.SaveChangesAsync();



                    }
                }
                // }


                await _context.SaveChangesAsync();

            }
            else
            {
                throw new CustomException("NO DETAILS FOUND FOR ID");
            }
            var r = await this._context.Consignee_Buyers_Master.FirstAsync(x => x.C_B_Code == detail.C_B_Code);
            return Mapper.Map<ConsgineeBuyersDto>(r);

            //  }
            //catch (DbEntityValidationException e)
            //{
            //    foreach (var eve in e.EntityValidationErrors)
            //    {
            //        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
            //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
            //                ve.PropertyName, ve.ErrorMessage);
            //        }
            //    }
            //    throw;
            //}

            //return null;

        }
    }
}