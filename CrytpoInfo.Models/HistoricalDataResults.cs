using System;
using System.Collections.Generic;

namespace CrytpoInfo.Models
{
    public class HistoricalDataResults
    {

        public string CurrencyName { get; set; }

        public string Symbol { get; set; }

        public DailyFigure[] DailyFigures { get; set; }
    }

    public class DailyFigure
    {
        public DateTime TimeStamp { get; set; }

        public float OpenPrice { get; set; }

        public float Fluctuation { get; set; }
    }
}
