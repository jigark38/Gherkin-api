using GherkinWebAPI.Models.Farmers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Core.Reports.FarmerWiseSummary
{
	public interface IFarmerWiseSummaryRepository
	{
		Task<List<FarmersDetail>> GetFarmersByAreaIdAndPsNumber(string areaId, string psNumber);
	}
}