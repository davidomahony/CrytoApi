using System;
using System.Collections.Generic;

namespace CrytpoInfo.Core.CoinMarket
{
    public class CoinMarketHistoricalDataResponse
    {
        public DataObject Data { get; set; }

        public class DataObject
        {
            public string Id { get; set; }

            public string Name { get; set; }

            public string Symbol { get; set; }

            public IEnumerable<QuoteInformation> Quotes { get; set; }

            public class QuoteInformation
            {
                public DateTime TimeOpen { get; set; }

                public DateTime TimeClose { get; set; }

                public DateTime TimeLow { get; set; }

                public DateTime TimeHigh { get; set; }

                public QuoteObject Quote { get; set; }

                public class QuoteObject
                {
                    public float Open { get; set; }

                    public float High { get; set; }

                    public float Low { get; set; }

                    public float Close { get; set; }

                    public float Volume { get; set; }

                    public float MarketCap { get; set; }

                    public DateTime TimeStamp { get; set; }
                }
            }
        }
    }
}
