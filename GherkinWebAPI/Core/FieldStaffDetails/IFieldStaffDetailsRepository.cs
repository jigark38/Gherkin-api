using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;
namespace GherkinWebAPI.Core
{
   public interface IFieldStaffDetailsRepository
    {
        Task<List<FieldStaffDetails>> GetAllFieldStaff();
        Task<List<FieldStaffDetails>> GetFieldStaffbyArea(string area);
        Task<FieldStaffDetails> GetFieldStaffbyID(int ID);
        void CreateFieldStaff(FieldStaffDetails fieldStaffDetails);
        Task<List<FieldStaffDetails>> CreateFieldStaffs(HarvestAreaFieldStaffDTO fieldStaffDetails);
        void  UpdateFieldStaff(int fieldStaffID, FieldStaffDetails fieldStaffDetails);
        Task<List<FieldStaffDetails>> GetFieldStaffbyAreaAndStaff(string area, string staffType);
    }
}
