using GherkinWebAPI.Core;
using GherkinWebAPI.Core.DailyInputAndFeedingDetails;
using GherkinWebAPI.Models.DailyInputAndFeedingDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service.DailyInputAndFeedingDetails
{
    public class InputIssuesService: IInputIssuesService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public InputIssuesService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public async Task<InputIssuesModel> MaterialInputConsumed(DailyInputModel dailyInputModel)
        {
            return await _repositoryWrapper.InputIssuesRepository.MaterialInputConsumed(dailyInputModel);
        }
        public async Task<FarmerInputConAndMatIssueModel> SaveFarmerInputDatails(FarmerInputConAndMatIssueModel ListModel)
        {
            return await _repositoryWrapper.InputIssuesRepository.SaveFarmerInputDatails(ListModel);
        }
    }
}