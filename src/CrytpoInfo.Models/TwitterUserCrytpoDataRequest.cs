using System.ComponentModel.DataAnnotations;

namespace CrytpoInfo.Models
{
    public class TwitterUserCrytpoDataRequest
    {
        [Required]
        public string CurrencyName { get; set; }

        [Required]
        public string TwitterUserName { get; set; }

        public int NumberOfTweets { get; set; } = 10;
    }
}
