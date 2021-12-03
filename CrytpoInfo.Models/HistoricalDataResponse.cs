namespace CrytpoInfo.Models
{
    public class HistoricalDataResponse
    {
        public bool Success { get; set; }

        public HistoricalDataResults Results { get; set; }

        public string ErrorMessage { get; set; } = null;
    }
}
