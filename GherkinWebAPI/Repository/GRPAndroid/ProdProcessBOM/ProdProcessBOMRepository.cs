using GherkinWebAPI.Core.GRPAndroid.ProdProcessBOM;
using GherkinWebAPI.Models.GRPAndroid.ProdProcessBOM;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Repository.GRPAndroid.ProdProcessBOM
{
    public class ProdProcessBOMRepository : IProdProcessBOMRepository
    {
        private readonly RepositoryContext _context;
        public ProdProcessBOMRepository(RepositoryContext context)
        {
            _context = context;
        }
        public async Task<List<ProdGroupCode>> GetAllProductGroup()
        {
            try
            {
                var AllGroupName = await (from grp in this._context.ProductGroups
                                          orderby grp.GrpName
                                          select new ProdGroupCode
                                          {
                                              GroupCode = grp.GroupCode,
                                              GrpName = grp.GrpName
                                          }).ToListAsync();

                return AllGroupName;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<List<VarietyModel>> GetVariety(string grpCode)
        {
            try
            {
                var AllVariety = await (from grp in this._context.ProductDetails
                                        where grp.GroupCode == grpCode
                                        orderby grp.VarietyName
                                        select new VarietyModel
                                        {
                                            VarietyCode = grp.VarietyCode,
                                            VarietyName = grp.VarietyName
                                        }).ToListAsync();

                return AllVariety;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<List<GradeModel>> GetGradeDetails(string varCode)
        {
            try
            {
                var AllGrades = await (from grp in this._context.gradeDetails
                                       where grp.VarietyCode == varCode
                                       orderby grp.GradeCode
                                       select new GradeModel
                                       {
                                           GradeCode = grp.GradeCode,
                                           GradeFrom = grp.GradeFrom,
                                           GradeTo = grp.GradeTo
                                       }).ToListAsync();

                return AllGrades;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<List<RawMaterialGroupModel>> GetRawMaterialGroup()
        {
            try
            {
                var AllRawMaterialGroups = await (from grp in this._context.RawMaterialGroupMaster
                                                  orderby grp.Raw_Material_Group
                                                  select new RawMaterialGroupModel
                                                  {
                                                      RawMaterialGroup = grp.Raw_Material_Group,
                                                      RawMaterialGroupCode = grp.Raw_Material_Group_Code
                                                  }).ToListAsync();

                return AllRawMaterialGroups;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<List<RawMaterialDetailModel>> GetRawMaterialDetails(string rawMaterialGrpCode)
        {
            try
            {
                var AllRawMaterialDetails = await (from grp in this._context.RawMaterialDetails
                                                   where grp.Raw_Material_Group_Code == rawMaterialGrpCode
                                                   orderby grp.Raw_Material_Details_Name
                                                   select new RawMaterialDetailModel
                                                   {
                                                       RawMaterialDetailsCode = grp.Raw_Material_Details_Code,
                                                       RawMaterialDetailsName = grp.Raw_Material_Details_Name
                                                   }).ToListAsync();

                return AllRawMaterialDetails;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<ProductionProcessDetailsModel> SaveProductionProcess(ProductionProcessDetails prodProcessDetails)
        {
            var obj = await (from p in _context.ProductionProcessDetail
                             where p.ProductionProcessName.ToUpper() == prodProcessDetails.ProductionProcessName.ToUpper()
                             select p).FirstOrDefaultAsync();
            ProductionProcessDetailsModel prodModel = new ProductionProcessDetailsModel();
            prodModel.DateofCreation = prodProcessDetails.DateofCreation;
            prodModel.EmployeeID = prodProcessDetails.EmployeeID;
            prodModel.FPGroupCode = prodProcessDetails.FPGroupCode;
            prodModel.FPVarietyCode = prodProcessDetails.FPVarietyCode;
            prodModel.ProductionProcessCode = prodProcessDetails.ProductionProcessCode;
            prodModel.ProductionProcessDescription = prodProcessDetails.ProductionProcessDescription;
            prodModel.ProductionProcessName = prodProcessDetails.ProductionProcessName;
            try
            {
                if (obj == null)
                {

                    List<string> ppdno = await (from ppd in this._context.ProductionProcessDetail

                                                select ppd.ProductionProcessCode).ToListAsync();
                    List<int> num = new List<int>();
                    if (ppdno.Count == 0)
                    {
                        num.Add(0);
                    }
                    else
                    {
                        foreach (string i in ppdno)
                        {
                            num.Add(Convert.ToInt32(i.Split('_')[1]));
                        }
                    }
                    prodProcessDetails.ProductionProcessCode = "PP_" + (num.Max() + 1).ToString();
                    prodProcessDetails.ProductionProcessName = prodProcessDetails.ProductionProcessName.ToUpper();
                    _context.ProductionProcessDetail.Add(prodProcessDetails);
                    await _context.SaveChangesAsync();
                    prodModel.ProductionProcessCode = "PP_" + (num.Max() + 1).ToString();
                    prodModel.status = "Successfully Inserted";
                    return prodModel;
                }
                else
                {
                    prodModel.status = "Process Name Already Exists";
                    return prodModel;
                }
            }
            catch (Exception e)
            {
                prodModel.status = "Insertion Failed";
                return prodModel;
            }
        }
        public async Task<ProdProcessCombinedModel> SaveProductionProcessBOM(ProdProcessCombinedModel prodProcessCombine)
        {
            var obj = await (from m in _context.MediaProcessDetails
                             where m.MediaProcessName.ToUpper() == prodProcessCombine.MediaProcessDetails.MediaProcessName.ToUpper()
                             select m).FirstOrDefaultAsync();
            try
            {
                if (obj == null)
                {
                    List<string> ppdno = await (from ppd in this._context.MediaProcessDetails

                                                select ppd.MediaProcessCode).ToListAsync();
                    List<int> num = new List<int>();
                    if (ppdno.Count == 0)
                    {
                        num.Add(0);
                    }
                    else
                    {
                        foreach (string i in ppdno)
                        {
                            num.Add(Convert.ToInt32(i.Split('_')[1]));
                        }
                    }
                    prodProcessCombine.MediaProcessDetails.MediaProcessCode = "MPC_" + (num.Max() + 1).ToString();
                    prodProcessCombine.MediaProcessDetails.MediaProcessName = prodProcessCombine.MediaProcessDetails.MediaProcessName.ToUpper();
                    _context.MediaProcessDetails.Add(prodProcessCombine.MediaProcessDetails);
                    await _context.SaveChangesAsync();

                    List<string> ppdno1 = await  (from ppd in this._context.productionStandardBOMs
                                                                 select ppd.BOMCode).ToListAsync();
                    List<int> num1 = new List<int>();
                    if (ppdno1.Count == 0)
                    {
                        num1.Add(0);
                    }
                    else
                    {
                        foreach (string i in ppdno1)
                        {
                            num1.Add(Convert.ToInt32(i.Split('_')[1]));
                        }
                    }
                    prodProcessCombine.ProdStandardBOM.BOMCode = "PBOM_" + (num.Max() + 1).ToString();
                    prodProcessCombine.ProdStandardBOM.MediaProcessCode = "MPC_" + (num.Max() + 1).ToString();
                    prodProcessCombine.ProdStandardBOM.StandardUOM = prodProcessCombine.ProdStandardBOM.StandardUOM.ToUpper();
                    _context.productionStandardBOMs.Add(prodProcessCombine.ProdStandardBOM);
                    await _context.SaveChangesAsync();

                    foreach (ProductionProcessMaterialDetails item in prodProcessCombine.ProdProcessMaterialDetails)
                    {
                        item.BOMCode = "PBOM_" + (num.Max() + 1).ToString();
                        item.StandardUOM = item.StandardUOM.ToUpper();
                        _context.ProductionProcessMaterialDetails.Add(item);
                        await _context.SaveChangesAsync();
                    }
                    //_context.ProductionProcessMaterialDetails.AddRange(prodProcessCombine.ProdProcessMaterialDetails);
                    //await _context.SaveChangesAsync();
                    prodProcessCombine.status = "Successfully Inserted";
                    return prodProcessCombine;
                }
                else
                {
                    prodProcessCombine.status = "Media Process Name Already Exists";
                    return prodProcessCombine;
                }
            }
            catch (Exception e)
            {
                prodProcessCombine.status = "Insertion Failed";
                return prodProcessCombine;
            }
        }
        public async Task<List<string>> GetProductionUOM(string uomKey)
        {
            try
            {
                var ProductionUOM = await (from p in _context.productionStandardBOMs
                                           where p.StandardUOM.Contains(uomKey.ToUpper())
                                           select p.StandardUOM.ToUpper()).Distinct().ToListAsync();

                return ProductionUOM;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<List<string>> GetMaterialUOM(string uomKey)
        {
            try
            {
                var ProductionUOM = await (from p in _context.ProductionProcessMaterialDetails
                                           where p.StandardUOM.Contains(uomKey.ToUpper())
                                           select p.StandardUOM.ToUpper()).Distinct().ToListAsync();

                return ProductionUOM;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<List<ProdGroupCode>> GetAllSavedProductGroup()
        {
            try
            {
                var AllGroupName = await (from grp in this._context.ProductionProcessDetail
                                          join grp1 in _context.ProductGroups on grp.FPGroupCode equals grp1.GroupCode
                                          orderby grp1.GrpName
                                          select new ProdGroupCode
                                          {
                                              GroupCode = grp.FPGroupCode,
                                              GrpName = grp1.GrpName
                                          }).Distinct().ToListAsync();

                return AllGroupName;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<List<VarietyModel>> GetSavedVariety(string grpCode)
        {
            try
            {
                var AllVariety = await (from grp in this._context.ProductionProcessDetail
                                        join grp1 in _context.ProductDetails on grp.FPVarietyCode equals grp1.VarietyCode
                                        where grp.FPGroupCode == grpCode
                                        orderby grp.FPVarietyCode
                                        select new VarietyModel
                                        {
                                            VarietyCode = grp.FPVarietyCode,
                                            VarietyName = grp1.VarietyName
                                        }).Distinct().ToListAsync();

                return AllVariety;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<List<ProductionProcessDetails>> FetchProdProcess(string grpCode, string variety)
        {
            try
            {
                var prodProcess = await (from grp in this._context.ProductionProcessDetail
                                         where grp.FPGroupCode == grpCode && grp.FPVarietyCode == variety
                                         select grp
                                                        ).ToListAsync();

                return prodProcess;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}