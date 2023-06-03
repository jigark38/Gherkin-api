using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models.Employee
{

	public class EmployeeBankDetailsMaster
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("Emp_Account_ID")]
		public long empAccountId { get; set; }
		[Column("Entry_Date")]
		[Required]
		public DateTime entryDate { get; set; }
		[Column("Entered_Emp_ID")]
		[Required]
		[MaxLength(10)]
		public string enteredEmployeeId { get; set; }
		[Column("Org_office_No")]
		[Required]
		public int orgOfficeNo { get; set; }
		[Column("Mode_of_Account")]
		[Required]
		[MaxLength(50)]
		public string modeOfAccount { get; set; }
		[Column("Employee_ID")]
		[Required]
		[MaxLength(20)]
		public string empId { get; set; }
	}
}