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
            return Ok("Just using coin market ATM");
        }

        [HttpGet]
        [Route("FetchData")]
        public IActionResult FetchData([FromQuery] [Required] HistoricalDataRequest requestInfo)
        {
            requestInfo.RequestId = Guid.NewGuid();

            this.ThrowIfInvalidDatesInputted(requestInfo);

            var responseBody = this.historicalDataService.AcquireHistoricalData(requestInfo);
            return Ok(responseBody);
        }

        private void ThrowIfInvalidDatesInputted(HistoricalDataRequest request)
        {
            if (request.StartDate.CompareTo(request.EndDate) > 0)
            {
                throw new ClientException(5, "StartDate is after EndDate", request.RequestId);
            }
        }
    }
}
