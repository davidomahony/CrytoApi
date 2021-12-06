using CrytpoInfo.Core.Repositories;
using CrytpoInfo.Models;
using System;
using System.Linq;

namespace CrytpoInfo.CryptAPI.Services
{
    public class HistoricalDataService
    {
        private readonly IHistoricalDataRepository<HistoricalDataResults> repository;

        public HistoricalDataService(IHistoricalDataRepository<HistoricalDataResults> repository)
        {
            this.repository = repository;
        }

        public HistoricalDataResponse AcquireHistoricalData(HistoricalDataRequest requestInformation, Guid requestId)
        {
            var historicalDataResult = this.repository.AcquireHistoricalData(requestInformation);
            if (historicalDataResult is null)
            {
                // will decide later how to handle
            }

            if (historicalDataResult?.DailyFigures == null || !historicalDataResult.DailyFigures.Any())
            {
                return new HistoricalDataResponse()
                {
                    Results = null,
                    Success = false,
                    RequestId = requestId,
                    ErrorMessage = "Big error"
                };
            }

            return new HistoricalDataResponse()
            {
                Results = historicalDataResult,
                Success = true,
                RequestId = requestId
            };
        }
    }
}
