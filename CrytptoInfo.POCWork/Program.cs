using Binance.Net;
using Binance.Net.Objects;
using CryptoExchange.Net.Authentication;
using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace CrytptoInfo.POCWork
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Get, new Uri("https://api.twitter.com/2/users/44196397/tweets?max_results=10&tweet.fields=created_at,public_metrics"));
            request.Headers.Add("Authorization", "Bearer YouWillNeverGetThis");

            var result = client.Send(request);

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseContent = result.Content.ReadAsStringAsync().Result;
                var deserializedResponse = JsonConvert.DeserializeObject<TwitterResponse>(responseContent);

                if (deserializedResponse != null)
                {
                    Console.WriteLine("Success");
                    Console.ReadLine();
                }
            }
        }

        private void HistoricalDataTest()
        {
            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Get, new Uri("https://api.coinmarketcap.com/data-api/v3/cryptocurrency/historical?id=2&convertId=2781&timeStart=1535040000&timeEnd=1738144000"));
            request.Headers.Add("authority", "api.coinmarketcap.com");
            request.Headers.Add("path", "/data-api/v3/cryptocurrency/historical?id=1&convertId=2781&timeStart=1627516800&timeEnd=1638144000");
            request.Headers.Add("authority", "api.coinmarketcap.com");
            request.Headers.Add("Accept", "*/*");
            request.Headers.Add("User-Agent", "PostmanRuntime/7.28.4");
            request.Headers.Add("Connection", "keep-alive");

            var result = client.Send(request);

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseContent = result.Content.ReadAsStringAsync().Result;
                var deserializedResponse = JsonConvert.DeserializeObject<Response>(responseContent);

                if (deserializedResponse != null)
                {
                    Console.WriteLine("Success");
                    Console.ReadLine();
                }
            }
        }
    }
}
