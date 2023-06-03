using GherkinWebAPI.Core.LoansAndAdvancesDetails;
using GherkinWebAPI.Models.LoansAndAdvancesDetails;
using GherkinWebAPI.Persistence;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using System.Data.Entity;
using System.Collections.Generic;

namespace GherkinWebAPI.Repository.LoansAndAdvancesDetails
{
    public class LoansAdvancesRepository : RepositoryBase<LoansAdvancesDetail>, ILoansAdvancesRepository
    {
        private readonly RepositoryContext _repositoryContext;

        public LoansAdvancesRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public async Task<LoansAdvancesDTO> CreateLoansAdvances(LoansAdvancesDTO loansAdvancesDTO)
        {
            try
            {
                var loansAdvancesDetail = _repositoryContext.LoansAdvancesDetails.SingleOrDefault(l => l.LoanAdvNo == loansAdvancesDTO.LoanAdvNo);

                if (loansAdvancesDetail == null)
                {
                    var lADetail = new LoansAdvancesDetail
                    {
                        LoanAdvNo = loansAdvancesDTO.LoanAdvNo,
                        LoginEmployeeID = loansAdvancesDTO.LoginEmployeeID,
                        EntryDate = loansAdvancesDTO.EntryDate,
                        OrgofficeNo = loansAdvancesDTO.OrgofficeNo,
                        EmployeeID = loansAdvancesDTO.EmployeeID,
                        LAType = loansAdvancesDTO.LAType,
                        LARequistionDate = loansAdvancesDTO.LARequistionDate,
                        LARequistionAmount = loansAdvancesDTO.LARequistionAmount,
                        LAApprovedAmount = loansAdvancesDTO.LAApprovedAmount,
                        LANoOfInstl = loansAdvancesDTO.LANoOfInstl,
                        LACondition = loansAdvancesDTO.LACondition,
                        LAInterestPercentage = loansAdvancesDTO.LAInterestPercentage,
                        LAMonthlyDeduction = loansAdvancesDTO.LAMonthlyDeduction,
                        LAApprovedEmployeeID = loansAdvancesDTO.LAApprovedEmployeeID,
                        LAApprovedDate = loansAdvancesDTO.LAApprovedDate,
                        LA_Deducted_Till_Date = loansAdvancesDTO.LaDeductedTillDate,
                        LA_Installments_Till_Paid = loansAdvancesDTO.LaInstallmentsTillPaid,

                    };

                    _repositoryContext.LoansAdvancesDetails.Add(lADetail);

                    var result = await _repositoryContext.SaveChangesAsync();

                    if (result == 1)
                    {
                        return loansAdvancesDTO;
                    }

                    return new LoansAdvancesDTO();
                }

                return new LoansAdvancesDTO();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> GetLoansAdvanceNo()
        {
            var loansAdvancesDetails = await _repositoryContext.LoansAdvancesDetails.AsNoTracking().ToListAsync();
            if (loansAdvancesDetails.Count > 0)
            {
                var maxLANo = loansAdvancesDetails.OrderByDescending(c => c.ID).Take(1).FirstOrDefault().ID;
                return "LA_" + Convert.ToString(maxLANo + 1);
            }
            return string.Concat("LA_", "1");
        }

        public async Task<List<LoansAdvancesDTO>> SearchLoansAdvances(int orgOfficeNo, string employeeId)
        {
            var lADetails = await (from la in _repositoryContext.LoansAdvancesDetails
                                   where la.OrgofficeNo == orgOfficeNo && la.EmployeeID == employeeId
                                   select new LoansAdvancesDTO
                                   {
                                       LoanAdvNo = la.LoanAdvNo,
                                       LAType = la.LAType,
                                       LARequistionDate = la.LARequistionDate,
                                       LARequistionAmount = la.LARequistionAmount,
                                       LAApprovedDate = la.LAApprovedDate,
                                       LAApprovedAmount = la.LAApprovedAmount,
                                       LANoOfInstl = la.LANoOfInstl,
                                       LACondition = la.LACondition,
                                       LAInterestPercentage = la.LAInterestPercentage,
                                       LAMonthlyDeduction = la.LAMonthlyDeduction,
                                       LAApprovedEmployeeID = la.LAApprovedEmployeeID,
                                       EmployeeID = la.EmployeeID,
                                       OrgofficeNo = la.OrgofficeNo,
                                       EntryDate = la.EntryDate,
                                       LoginEmployeeID = la.LoginEmployeeID,
                                       LaDeductedTillDate = la.LA_Deducted_Till_Date,
                                       LaInstallmentsTillPaid = la.LA_Installments_Till_Paid,
                                   }).ToListAsync();

            return lADetails;
        }

        public async Task<LoansAdvancesDTO> UpdateLoansAdvances(LoansAdvancesDTO loansAdvancesDTO)
        {
            try
            {
                var lADetails = _repositoryContext.LoansAdvancesDetails.Where(la => la.LoanAdvNo == loansAdvancesDTO.LoanAdvNo).SingleOrDefault();

                if (lADetails != null)
                {
                    lADetails.LAType = loansAdvancesDTO.LAType;
                    lADetails.LARequistionDate = loansAdvancesDTO.LARequistionDate;
                    lADetails.LARequistionAmount = loansAdvancesDTO.LARequistionAmount;
                    lADetails.LAApprovedDate = loansAdvancesDTO.LAApprovedDate;
                    lADetails.LAApprovedAmount = loansAdvancesDTO.LAApprovedAmount;
                    lADetails.LANoOfInstl = loansAdvancesDTO.LANoOfInstl;
                    lADetails.LACondition = loansAdvancesDTO.LACondition;
                    lADetails.LAInterestPercentage = loansAdvancesDTO.LAInterestPercentage;
                    lADetails.LAApprovedEmployeeID = loansAdvancesDTO.LAApprovedEmployeeID;
                    lADetails.LA_Deducted_Till_Date = loansAdvancesDTO.LaDeductedTillDate;
                    lADetails.LA_Installments_Till_Paid = loansAdvancesDTO.LaInstallmentsTillPaid;
                    _repositoryContext.LoansAdvancesDetails.AddOrUpdate(lADetails);

                    var result = await _repositoryContext.SaveChangesAsync();

                    if (result == 1)
                    {
                        return loansAdvancesDTO;
                    }

                    return new LoansAdvancesDTO();
                }

                return new LoansAdvancesDTO();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}