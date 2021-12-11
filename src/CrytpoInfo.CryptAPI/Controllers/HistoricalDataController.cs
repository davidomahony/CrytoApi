using CrytpoInfo.Buisness.Exceptions;
using CrytpoInfo.CryptAPI.Services;
using CrytpoInfo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace CrytpoInfo.CryptAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class HistoricalDataController : ControllerBase
    {
        private readonly HistoricalDataService historicalDataService;

        public HistoricalDataController(HistoricalDataService historicalDataService)
        {
            this.historicalDataService = historicalDataService;
        }

        [HttpGet]
        [Route("Info")]
        public IActionResult Info()
        {
            var basicInfo = new
            {
                InformationSource = "CoinMarket",
                Url = "https://api.coinmarketcap.com/data-api/v3/cryptocurrency/historical"
            };
            return Ok(basicInfo);
        }

        [HttpGet]
        [Route("FetchData")]
        public IActionResult FetchData([FromQuery] [Required] HistoricalDataRequest requestInfo)
        {
            var internalRequest = new HistoricalDataRequestInternal(requestInfo);

            ThrowIfInvalidDatesInputted(internalRequest);

            var responseBody = this.historicalDataService.AcquireHistoricalData(internalRequest);
            return Ok(responseBody);
        }

        private static void ThrowIfInvalidDatesInputted(HistoricalDataRequestInternal request)
        {
            if (request.StartDate.CompareTo(request.EndDate) > 0)
            {
                throw new ClientException(5, "StartDate is after EndDate", request.RequestId);
            }
        }
    }
}
