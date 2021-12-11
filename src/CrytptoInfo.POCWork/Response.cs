using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrytptoInfo.POCWork
{
    public class Response
    {
        public DataObject Data { get; set; }

        public class DataObject
        {
            public string Id { get; set; }

            public string name { get; set; }
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
            // {"timeOpen":"2018-08-24T00:00:00.000Z","timeClose":"2018-08-24T23:59:59.999Z","timeHigh":"2018-08-24T17:29:00.000Z","timeLow":"2018-08-24T14:49:01.000Z",
            // "quote":{"open":57.6042000000,"high":58.2782000000,"low":55.9536000000,"close":57.9298000000,"volume":194676000.0000000000,"marketCap":3359443512.6700000000,"timestamp":"2018-08-24T23:59:59.999Z"}}
        }
    }
}
