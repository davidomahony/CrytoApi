﻿
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

        public string GetAccountId(TwitterUserCrytpoDataRequestInternal twitterUserCrytpoDataRequestInternal)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri($"https://api.twitter.com/2/users/by/username/{twitterUserCrytpoDataRequestInternal.TwitterUserName}"));
            request.Headers.Add("Authorization", "Bearer YouWillNeverGetThis");

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
            var request = this.GenerateRequestMessage(
                twitterUserCrytpoDataRequestInternal.AccountId,
                twitterUserCrytpoDataRequestInternal.NumberOfTweets);

            try
            {
                var result = this.client.Send(request);
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var responseContent = result.Content.ReadAsStringAsync().Result;
                    var deserializedResponse = JsonConvert.DeserializeObject<IEnumerable<BaseTweet>>(responseContent);
                    return deserializedResponse;
                }
            }
            catch (Exception ex)
            {
                throw new ApiException(System.Net.HttpStatusCode.InternalServerError, 50, ex.Message, twitterUserCrytpoDataRequestInternal.RequestId);
            }

            throw new ApiException(System.Net.HttpStatusCode.InternalServerError, 50, "Failed to fetch results from ", twitterUserCrytpoDataRequestInternal.RequestId);
        }

        private HttpRequestMessage GenerateRequestMessage(string accountId, int numberOfTweets)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri($"https://api.twitter.com/2/users/{accountId}/tweets?max_results={numberOfTweets}&tweet.fields=created_at,public_metrics"));
            request.Headers.Add("Authorization", "Bearer YouWillNeverGetThis");
            return request;
        }
    }
}