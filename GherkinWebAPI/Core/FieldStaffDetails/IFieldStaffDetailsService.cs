using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core
{
   public interface IFieldStaffDetailsService
    {
        #region Retrive
        Task<List<FieldStaffDetails>> GetAllFieldStaff();
        Task<List<FieldStaffDetails>> GetFieldStaffbyArea(string area);
        Task<FieldStaffDetails> GetFieldStaffbyID(int ID);

        #endregion
        Task CreateFieldStaff(FieldStaffDetails fieldStaffDetails);

        Task<List<FieldStaffDetails>> CreateFieldStaffs(HarvestAreaFieldStaffDTO fieldStaffDetails);
        Task UpdateFieldStaff(int fieldStaffID, FieldStaffDetails fieldStaffDetails);
        Task<object> GetFieldStaffWithEmployeeDetails(string areaid, string staffType);
    }
}
