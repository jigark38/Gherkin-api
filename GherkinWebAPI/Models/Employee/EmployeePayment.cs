using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace GherkinWebAPI.Models.Employee
{
    public class EmployeePayment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("Employee_ID")]
        public string employeeId { get; set; }

        [MaxLength(10)]
        [Column("Employee_Payment_Category")]
        public string employeePaymentCategory { get; set; }
        [Column("Employee_Basic_Salary")]
        public decimal employeeBasicSalary { get; set; }
        [Column("Employee_HRA")]
        public decimal employeeHRA { get; set; }
        [Column("Employee_DA")]
        public decimal employeeDA { get; set; }
        [Column("Employee_CA")]
        public decimal employeeCA { get; set; }
        [Column("Employee_MA")]
        public decimal employeeMA { get; set; }
        [Column("Employee_Incentives")]
        public decimal employeeIncentives { get; set; }
        [Column("Education_Allowance")]
        public decimal? educationAllowance { get; set; }
        [Column("Employee_OA")]
        public decimal employeeOA { get; set; }
        [Column("Employee_Gross_Salary")]
        public decimal employeeGrossSalary { get; set; }
    }
}