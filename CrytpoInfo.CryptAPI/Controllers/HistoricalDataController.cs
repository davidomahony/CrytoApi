using CrytpoInfo.CryptAPI.Services;
using CrytpoInfo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CrytpoInfo.CryptAPI.Controllers
{
    [Route("v1.0/[controller]")]
    [ApiController]
    public class HistoricalDataController : ControllerBase
    {
        private readonly HistoricalDataService historicalDataService;
        private readonly IEnumerable<string> supportedCurrencies;

        public HistoricalDataController(HistoricalDataService historicalDataService)
        {
            this.historicalDataService = historicalDataService;
            this.supportedCurrencies = Enumerable.Empty<string>();
        }

        [HttpGet]
        [Route("Info")]
        public IActionResult Info()
        {
            return Ok("I have some information");
        }

        [HttpGet]
        [Route("FetchData")]
        public IActionResult HistoricalData([FromQuery] HistoricalDataRequest requestInfo)
        {
            // maybe validate request
            if (!this.IsValidRequest(requestInfo, out string errMsg))
            {
                return BadRequest(errMsg);
            }

            var responseBody = this.historicalDataService.AcquireHistoricalData(requestInfo);
            if (responseBody?.DailyFigures == null || !responseBody.DailyFigures.Any())
            {
                return BadRequest(responseBody);
            }

            return Ok(responseBody);
        }

        private bool IsValidRequest(HistoricalDataRequest request, out string errMsg)
        {
            errMsg = string.Empty;

            if (request.StartDate.CompareTo(request.EndDate) > 0)
            {
                errMsg = "StartDate is after EndDate";
                return false;
            }

            if (request.TimeInterval < 1)
            {
                errMsg = "Please ensure time interval is a non negative value";
                return false;
            }

            // clean up once added
            if (this.supportedCurrencies.Contains(request.CurrencyName))
            {
                errMsg = "Inputted currency is not supported";
                return false;
            }

            return true;
        }
    }
}
