using GherkinWebAPI.Models.DailyInputAndFeedingDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.DailyInputAndFeedingDetails
{
    public interface IInputIssuesRepository
    {
        Task<InputIssuesModel> MaterialInputConsumed(DailyInputModel dailyInputModel);
        Task<FarmerInputConAndMatIssueModel> SaveFarmerInputDatails(FarmerInputConAndMatIssueModel ListModel);
    }
}
