using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using CrytpoInfo.Buisness.Exceptions;
using CrytpoInfo.Core.CoinMarket;
using CrytpoInfo.Core.Repositories;
using CrytpoInfo.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CrytpoInfo.Buisness.Repositories
{
    public class CoinMarketHistoricalDataRepository : IHistoricalDataRepository<HistoricalDataResults>
    {
        private readonly HttpClient client;
        private readonly Dictionary<string, string> supportedCurrenciesIndex;

        public CoinMarketHistoricalDataRepository(HttpClient client, IConfiguration config)
        {
            this.client = client;
            var dictionaryOfCurrencies = config.GetSection("HistoricalData:Repositories:CoinMarket:SupportedCurrencies").GetChildren()
                .Select(itm => itm.Value.Split('-')).ToDictionary(itm => itm[0], itm => itm[1]);
            this.supportedCurrenciesIndex = new Dictionary<string, string>(dictionaryOfCurrencies, StringComparer.OrdinalIgnoreCase);
        }

        public HistoricalDataResults AcquireHistoricalData(HistoricalDataRequestInternal information)
        {
            var request = this.GenerateRequestMessage(information);

            try
            {
                var result = this.client.Send(request);
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var responseContent = result.Content.ReadAsStringAsync().Result;
                    var deserializedResponse = JsonConvert.DeserializeObject<CoinMarketHistoricalDataResponse>(responseContent);

                    var historicalResponseBuilder = new CoinMarketHistoricalDataResultsBuilder(deserializedResponse);
                    return historicalResponseBuilder.BuildHistoricalDataResponse();
                }
            }
            catch (Exception ex)
            {
                throw new ApiException(System.Net.HttpStatusCode.InternalServerError, 50, ex.Message, information.RequestId);
            }

            throw new ApiException(System.Net.HttpStatusCode.InternalServerError, 50, "Failed to fetch results from ", information.RequestId);
        }

        private HttpRequestMessage GenerateRequestMessage(HistoricalDataRequestInternal information)
        {
            var startTimeUnix = ((DateTimeOffset)information.StartDate).ToUnixTimeSeconds();
            var endTimeUnix = ((DateTimeOffset)information.EndDate).ToUnixTimeSeconds();

            if (this.supportedCurrenciesIndex.TryGetValue(information.CurrencyName, out string key))
            {
                var request = new HttpRequestMessage(HttpMethod.Get, new Uri($"https://api.coinmarketcap.com/data-api/v3/cryptocurrency/historical?id={key}&convertId=2781&timeStart={startTimeUnix}&timeEnd={endTimeUnix}"));
                request.Headers.Add("authority", "api.coinmarketcap.com");
                request.Headers.Add("path", $"/data-api/v3/cryptocurrency/historical?id={key}&convertId=2781&timeStart={startTimeUnix}&timeEnd={endTimeUnix}");
                request.Headers.Add("authority", "api.coinmarketcap.com");
                request.Headers.Add("Accept", "*/*");
                request.Headers.Add("User-Agent", "PostmanRuntime/7.28.4");
                request.Headers.Add("Connection", "keep-alive");

                return request;
            }

            throw new ClientException(50, "Inputted currency is not supported", information.RequestId); ;
        }
    }
}
