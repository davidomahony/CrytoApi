using System;
using System.ComponentModel.DataAnnotations;

namespace CrytpoInfo.Models
{
    public class HistoricalDataRequest
    {
        [Required]
        public string CurrencyName { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public Guid RequestId { get; set; }
    }
}
