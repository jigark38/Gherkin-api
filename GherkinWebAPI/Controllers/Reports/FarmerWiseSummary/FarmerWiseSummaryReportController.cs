using GherkinWebAPI.Core.Reports.FarmerWiseSummary;
using GherkinWebAPI.Models.Farmers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Net;
using System.Net.Http;

namespace GherkinWebAPI.Controllers.Reports.FarmerWiseSummary
{
	[RoutePrefix("FarmerWiseSummary")]
	public class FarmerWiseSummaryReportController : ApiController
	{
		private readonly IFarmerWiseSummaryRepository _repository;

		public FarmerWiseSummaryReportController(IFarmerWiseSummaryRepository repository)
		{
			this._repository = repository;
		}

		[HttpGet]
		[Route("GetFarmersByAreaIdAndpsNo")]
		public async Task<IHttpActionResult> GetFarmersByAreaIdAndPsNumber(string areaId, string psNo)
		{
			List<FarmersDetail> farmers = new List<FarmersDetail>();
			try
			{
				farmers = await _repository.GetFarmersByAreaIdAndPsNumber(areaId, psNo);
				return Ok(farmers);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message + " " + $"Exception in FarmerWiseSummaryReportController/{nameof(FarmerWiseSummaryReportController.GetFarmersByAreaIdAndPsNumber)}");
				return InternalServerError();
			}
		}
	}
}