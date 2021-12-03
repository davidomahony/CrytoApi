using CrytpoInfo.Core.Repositories;
using CrytpoInfo.Models;
using System.Net.Http;

namespace CrytpoInfo.Buisness.Repositories
{
    public class CoinMarketHistoricalDataRepository : IHistoricalDataRepository<HistoricalDataResults>
    {
        private HttpClient client;

        public CoinMarketHistoricalDataRepository(HttpClient client) => this.client = client;

        public HistoricalDataResults AcquireHistoricalData(HistoricalDataRequest information)
        {
            throw new System.NotImplementedException();
        }
    }
}
