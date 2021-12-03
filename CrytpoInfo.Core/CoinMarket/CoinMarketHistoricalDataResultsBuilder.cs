using CrytpoInfo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrytpoInfo.Core.CoinMarket
{
    public class CoinMarketHistoricalDataResultsBuilder
    {
        private readonly CoinMarketHistoricalDataResponse reponse;

        public CoinMarketHistoricalDataResultsBuilder(CoinMarketHistoricalDataResponse response) => this.reponse = response;

        public HistoricalDataResults BuildHistoricalDataResponse()
        {
            return new HistoricalDataResults()
            {
                CurrencyName = this.reponse.Data.Name,
                Symbol = this.reponse.Data.Symbol,
                DailyFigures = this.reponse.Data.Quotes.Select(quote =>
                {
                    return new DailyFigure()
                    {
                        TimeStamp = quote.Quote.TimeStamp,
                        OpenPrice = quote.Quote.Open,
                        Fluctuation = quote.Quote.High - quote.Quote.Low
                    };
                }).ToArray()
            };
        }
    }
}
