using System;

namespace CrytpoInfo.Models
{
    public class HistoricalDataRequest
    {
        public string CurrencyName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int TimeInterval { get; set; }

        public Guid RequestId { get; set; }
    }
}
