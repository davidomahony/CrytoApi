using System;

namespace CrytpoInfo.Models
{
    public class HistoricalDataRequestInternal : HistoricalDataRequest
    {
        public HistoricalDataRequestInternal(HistoricalDataRequest request)
        {
            this.CurrencyName = request.CurrencyName;
            this.EndDate = request.EndDate;
            this.StartDate = request.StartDate;
            this.RequestId = Guid.NewGuid();
        }

        public Guid RequestId { get; }
    }
}
