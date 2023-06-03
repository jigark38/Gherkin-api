using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models.Employee
{
	public class EmployeeBankAccountDetails
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("Bank_Account_ID")]
		public int bankAccountId { get; set; }

		[Column("Emp_Account_ID")]
		[Required]
		public long empAccountId { get; set; }

		[Column("Employee_ID")]
		[Required]
		[MaxLength(20)]
		public string empId { get; set; }

		[Column("Bank_Name")]
		[MaxLength(75)]
		public string bankName { get; set; }

		[Column("Bank_Code")]
		[MaxLength(10)]
		public string bankCode { get; set; }

		[Column("Bank_Branch")]
		[MaxLength(50)]
		public string bankBranch { get; set; }

		[Column("Bank_IFSC")]
		[MaxLength(15)]
		public string bankIfsc { get; set; }

		[Column("Bank_Account_Number")]
		[Required]
		[MaxLength(30)]
		public string bankAccountNumber { get; set; }

		[Column("Account_Effective_Date_From")]
		[Required]
		public DateTime accountEffectiveDateFrom { get; set; }

		[Column("Nominee_Name")]
		[Required]
		[MaxLength(50)]
		public string nomineeName { get; set; }

		[Column("Nominee_Relationship")]
		[Required]
		[MaxLength(30)]
		public string nomineeRelationship { get; set; }

		[Column("Preferred_Account")]
		[Required]
		[MaxLength(3)]
		public string preferredAccount { get; set; }
	}
}