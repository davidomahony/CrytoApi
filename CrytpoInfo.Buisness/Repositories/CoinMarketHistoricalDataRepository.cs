using CrytpoInfo.Core.CoinMarket;
using CrytpoInfo.Core.Repositories;
using CrytpoInfo.Models;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;

namespace CrytpoInfo.Buisness.Repositories
{
    public class CoinMarketHistoricalDataRepository : IHistoricalDataRepository<HistoricalDataResults>
    {
        private readonly HttpClient client;

        public CoinMarketHistoricalDataRepository(HttpClient client)
        {
            this.client = client;
        }

        public HistoricalDataResults AcquireHistoricalData(HistoricalDataRequest information)
        {
            var request = this.GenerateRequestMessage(information);

            var result = this.client.Send(request);

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseContent = result.Content.ReadAsStringAsync().Result;
                var deserializedResponse = JsonConvert.DeserializeObject<CoinMarketHistoricalDataResponse>(responseContent);

                var historicalResponseBuilder = new CoinMarketHistoricalDataResultsBuilder(deserializedResponse);
                return historicalResponseBuilder.BuildHistoricalDataResponse();
            }

            return null;
        }

        private HttpRequestMessage GenerateRequestMessage(HistoricalDataRequest information)
        {
            var startTimeUnix = ((DateTimeOffset)information.StartDate).ToUnixTimeSeconds();
            var endTimeUnix = ((DateTimeOffset)information.EndDate).ToUnixTimeSeconds();

            var request = new HttpRequestMessage(HttpMethod.Get, new Uri($"https://api.coinmarketcap.com/data-api/v3/cryptocurrency/historical?id=1&convertId=2781&timeStart={startTimeUnix}&timeEnd={endTimeUnix}"));
            request.Headers.Add("authority", "api.coinmarketcap.com");
            request.Headers.Add("path", $"/data-api/v3/cryptocurrency/historical?id=1&convertId=2781&timeStart={startTimeUnix}&timeEnd={endTimeUnix}");
            request.Headers.Add("authority", "api.coinmarketcap.com");
            request.Headers.Add("Accept", "*/*");
            request.Headers.Add("User-Agent", "PostmanRuntime/7.28.4");
            request.Headers.Add("Connection", "keep-alive");

            return request;
        }
    }
}
