using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GherkinWebAPI.Models;
using GherkinWebAPI.Core;
using System.Threading.Tasks;
using GherkinWebAPI.Persistence;
using GherkinWebAPI.CustomExceptions;
using System.Data.Entity;
using GherkinWebAPI.DTO;

namespace GherkinWebAPI.Repository
{
    public class FieldStaffDetailsRepository : RepositoryBase<FieldStaffDetails>, IFieldStaffDetailsRepository
    {
        private RepositoryContext _context;
        public FieldStaffDetailsRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
            this._context = repositoryContext;
        }
        #region Retrive
        public async Task<List<FieldStaffDetails>> GetAllFieldStaff()
        {
            try
            {
                return await FindAll().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message);
            }

        }
        public async Task<List<FieldStaffDetails>> GetFieldStaffbyArea(string area)
        {
            try
            {
                return await FindByCondition(staff => staff.Area_ID == area).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message);
            }

        }

        public async Task<FieldStaffDetails> GetFieldStaffbyID(int ID)
        {
            try
            {
                return await FindByCondition(staff => staff.FieldStaffID == ID).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message);
            }

        }
        #endregion
        public void CreateFieldStaff(FieldStaffDetails fieldStaffDetails)
        {
            _context.FieldStaffDetails.Add(fieldStaffDetails);
            this._context.SaveChanges();
        }
        public async Task<List<FieldStaffDetails>> CreateFieldStaffs(HarvestAreaFieldStaffDTO fieldStaffDetails)
        {

            List<FieldStaffDetails> _results = new List<FieldStaffDetails>();
            try
            {
                if (fieldStaffDetails.FieldStaffs.Count > 0)
                {


                    foreach (var staff in fieldStaffDetails.FieldStaffs)
                    {
                        FieldStaffDetails _fieldStaffDetails = new FieldStaffDetails();
                        _fieldStaffDetails.LoginUserName = fieldStaffDetails.LoginUserName;
                        _fieldStaffDetails.DateOfEntry = fieldStaffDetails.EntryDate;
                        _fieldStaffDetails.Area_ID = fieldStaffDetails.AreaID;
                        _fieldStaffDetails.AreaCode = fieldStaffDetails.AreaCode;

                        _fieldStaffDetails.EffectiveDate = staff.Effective_Date;
                        _fieldStaffDetails.DepartmentCode = staff.DepartmentCode;
                        _fieldStaffDetails.SubDepartmentCode = staff.SubDepartmentCode;
                        _fieldStaffDetails.DesignationCode = staff.DesignationCode;
                        _fieldStaffDetails.Employee_ID = staff.EmployeeID;
                        _fieldStaffDetails.StaffType = staff.StaffType;

                        _context.FieldStaffDetails.Add(_fieldStaffDetails);
                        await _context.SaveChangesAsync();

                        _results.Add(_fieldStaffDetails);
                    }

                }
                return _results;
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message);
            }
        }
        public void UpdateFieldStaff(int fieldStaffID, FieldStaffDetails fieldStaffDetails)
        {
            var fieldStaff = _context.FieldStaffDetails.FirstOrDefault(staff => staff.FieldStaffID == fieldStaffID);

            if (fieldStaff != null)
            {
                fieldStaff.Area_ID = fieldStaffDetails.Area_ID;
                fieldStaff.DateOfEntry = fieldStaffDetails.DateOfEntry;
                fieldStaff.EffectiveDate = fieldStaffDetails.EffectiveDate;
                fieldStaff.Employee_ID = fieldStaffDetails.Employee_ID;
                fieldStaff.StaffType = fieldStaffDetails.StaffType;
                fieldStaff.LoginUserName = fieldStaffDetails.LoginUserName;
                fieldStaff.DepartmentCode = fieldStaffDetails.DepartmentCode;
                fieldStaff.DesignationCode = fieldStaffDetails.DesignationCode;
                fieldStaff.SubDepartmentCode = fieldStaffDetails.SubDepartmentCode;
                fieldStaff.AreaCode = fieldStaffDetails.AreaCode;
                _context.SaveChanges();
            }

            else
            {
                throw new CustomException("Feild Staff Not Found!!");
            }


        }

        public async Task<List<FieldStaffDetails>> GetFieldStaffbyAreaAndStaff(string area, string staffType)
        {
            try
            {
                return await FindByCondition(staff => staff.Area_ID == area && staff.StaffType == staffType).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message);
            }

        }
    }
}