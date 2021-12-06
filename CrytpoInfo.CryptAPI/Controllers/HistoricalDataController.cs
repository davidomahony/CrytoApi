using CrytpoInfo.CryptAPI.Services;
using CrytpoInfo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CrytpoInfo.CryptAPI.Controllers
{
    [Route("v1.0/[controller]")]
    [ApiController]
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
        public IActionResult FetchData([FromQuery] HistoricalDataRequest requestInfo)
        {
            var requestId = Guid.NewGuid();
            if (!this.IsValidRequest(requestInfo, out BaseResponse errorReponse))
            {
                errorReponse.RequestId = requestId;
                return BadRequest(errorReponse);
            }

            var responseBody = this.historicalDataService.AcquireHistoricalData(requestInfo, requestId);
            return Ok(responseBody);
        }

        private bool IsValidRequest(HistoricalDataRequest request, out BaseResponse errResponse)
        {
            errResponse = null;
            string errMsg = string.Empty;

            if (request.StartDate.CompareTo(request.EndDate) > 0)
            {
                errMsg = "StartDate is after EndDate";
            }

            if (request.TimeInterval < 1)
            {
                errMsg = "Please ensure time interval is a non negative value";
            }

            if (!string.IsNullOrEmpty(errMsg))
            {
                errResponse = new BaseResponse()
                {
                    Success = false,
                    ErrorMessage = errMsg
                };
                return false;
            }

            return true;
        }
    }
}
