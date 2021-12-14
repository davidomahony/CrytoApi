using CrytpoInfo.Core.Builders;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Net.Http;

namespace CrytpoInfo.Buisness
{
    public class TwitterRequestBuilder : IHttpRequestBuilder
    {
        private HttpMethod method;
        private string baseUrl;
        private string completeUrl;

        public TwitterRequestBuilder(HttpMethod method, string baseUrl)
        {
            this.method = method;
            this.baseUrl = baseUrl;
            this.completeUrl = baseUrl;
        }

        public void AddHeader(string key, string value)
        {
            throw new NotImplementedException();
        }

        public void AddQueryParameter(string key, string value)
        {
            this.completeUrl = QueryHelpers.AddQueryString(this.completeUrl, key, value);
        }

        public HttpRequestMessage BuildRequest()
        {
            var request = new HttpRequestMessage(this.method, this.completeUrl);
            request.Headers.Add("Authorization", "Bearer");
            return request;
        }
    }
}
