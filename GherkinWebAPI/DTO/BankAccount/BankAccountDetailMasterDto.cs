using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO.BankAccount
{
	public class BankAccountDetailMasterDto
	{
		
		public int bankAccountId { get; set; }
		public long empAccountId { get; set; }
		public string empId { get; set; }
		public string bankName { get; set; }
		public string bankCode { get; set; }
		public string bankBranch { get; set; }
		public string bankIfsc { get; set; }
		public string bankAccountNumber { get; set; }
		public DateTime accountEffectiveDateFrom { get; set; }
		public string nomineeName { get; set; }
		public string nomineeRelationship { get; set; }
		public string preferredAccount { get; set; }
		public DateTime entryDate { get; set; }
		public string enteredEmployeeId { get; set; }
		public int orgOfficeNo { get; set; }
		public string modeOfAccount { get; set; }
	}
}