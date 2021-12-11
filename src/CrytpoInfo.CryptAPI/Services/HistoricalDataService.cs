using CrytpoInfo.Buisness.Exceptions;
using CrytpoInfo.Core.Repositories;
using CrytpoInfo.Models;
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

        public HistoricalDataResponse AcquireHistoricalData(HistoricalDataRequestInternal requestInformation)
        {
            var historicalDataResult = this.repository.AcquireHistoricalData(requestInformation);
            if (historicalDataResult is null)
            {
                throw new ApiException(System.Net.HttpStatusCode.InternalServerError, 50, "No daily Figures returned", requestInformation.RequestId);
            }

            if (historicalDataResult?.DailyFigures == null || !historicalDataResult.DailyFigures.Any())
            {
                throw new ApiException(System.Net.HttpStatusCode.InternalServerError, 51, "No daily Figures returned", requestInformation.RequestId);
            }

            return new HistoricalDataResponse()
            {
                Results = historicalDataResult,
                Success = true,
                RequestId = requestInformation.RequestId
            };
        }
    }
}
