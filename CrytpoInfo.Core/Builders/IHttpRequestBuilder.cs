using System.Net.Http;

namespace CrytpoInfo.Core.Builders
{
    public interface IHttpRequestBuilder
    {
        public HttpRequestMessage BuildRequest();

        public void AddQueryParameter(string key, string value);

        public void AddHeader(string key, string value);
    }
}
