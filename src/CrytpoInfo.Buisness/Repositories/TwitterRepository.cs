using CrytpoInfo.Buisness.Exceptions;
using CrytpoInfo.Core.Repositories;
using CrytpoInfo.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace CrytpoInfo.Buisness.Repositories
{
    public class TwitterRepository : ITwitterRepository
    {
        private IConfiguration configuration;
        private HttpClient client;

        public TwitterRepository(IConfiguration configuration, HttpClient client)
        {
            this.configuration = configuration;
            this.client = client;
        }

        public IEnumerable<BaseTweet> GetNumberOfTweetsForAccountID(string accountId, int numberOfTweets)
        {
            var request = this.GenerateRequestMessage(accountId, numberOfTweets);

            try
            {
                var result = this.client.Send(request);
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var responseContent = result.Content.ReadAsStringAsync().Result;
                    var deserializedResponse = JsonConvert.DeserializeObject<BaseTweet>(responseContent);

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

        private HttpRequestMessage GenerateRequestMessage(string accountId, int numberOfTweets)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri($"https://api.twitter.com/2/users/{accountId}/tweets?max_results={numberOfTweets}&tweet.fields=created_at,public_metrics"));
            request.Headers.Add("Authorization", "Bearer YouWillNeverGetThis");
            return request;
        }
    }
}
