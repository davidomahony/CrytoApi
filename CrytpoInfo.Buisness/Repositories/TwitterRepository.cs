
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using CrytpoInfo.Buisness.Exceptions;
using CrytpoInfo.Core.Repositories;
using CrytpoInfo.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CrytpoInfo.Buisness.Repositories
{
    public class TwitterRepository : ITwitterRepository
    {
        private HttpClient client;
        private IConfiguration configuration;

        public TwitterRepository(HttpClient client, IConfiguration configuration)
        {
            this.client = client;
            this.configuration = configuration;
        }

        public string GetAccountId(TwitterUserCrytpoDataRequestInternal twitterUserCrytpoDataRequestInternal)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(string.Format(this.configuration["Twitter:GetAccountIdUrl"], twitterUserCrytpoDataRequestInternal.TwitterUserName)));
            request.Headers.Add("Authorization", "Twitter:BearerToken");
            var result = this.client.Send(request);

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseContent = result.Content.ReadAsStringAsync().Result;
                var deserializedResponse = JsonConvert.DeserializeObject<TwitterUserGetAccountIdResponse>(responseContent);
                // do some null checks here
                return deserializedResponse.Data.Id;
            }

            return string.Empty;
        }

        public IEnumerable<BaseTweet> GetNumberOfTweetsForAccountID(TwitterUserCrytpoDataRequestInternal twitterUserCrytpoDataRequestInternal)
        {
            twitterUserCrytpoDataRequestInternal.AccountId = this.GetAccountId(twitterUserCrytpoDataRequestInternal);
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri(
                    $"{string.Format(this.configuration["Twitter:GetUserTweets"], twitterUserCrytpoDataRequestInternal.AccountId)}/tweets?max_results={twitterUserCrytpoDataRequestInternal.NumberOfTweets}&tweet.fields=created_at,public_metrics"));
            request.Headers.Add("Authorization", "Bearer XX");

            try
            {
                var result = this.client.Send(request);
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var responseContent = result.Content.ReadAsStringAsync().Result;
                    // need to deserialize properly
                    var deserializedResponse = JsonConvert.DeserializeObject<TwitterUserGetTweetsReponse>(responseContent);
                    return deserializedResponse.Data.Select(itm => new BaseTweet()
                    {
                        Text = itm.Text,
                        Id = itm.Id
                    });
                }
            }
            catch (Exception ex)
            {
                throw new ApiException(System.Net.HttpStatusCode.InternalServerError, 50, ex.Message, twitterUserCrytpoDataRequestInternal.RequestId);
            }

            throw new ApiException(System.Net.HttpStatusCode.InternalServerError, 50, "Failed to fetch results from ", twitterUserCrytpoDataRequestInternal.RequestId);
        }
    }
}
