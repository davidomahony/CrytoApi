using System;

namespace CrytpoInfo.Models
{
    public class TwitterUserCrytpoDataRequestInternal : TwitterUserCrytpoDataRequest
    {
        public TwitterUserCrytpoDataRequestInternal(TwitterUserCrytpoDataRequest request)
        {
            this.CurrencyName = request.CurrencyName;
            this.NumberOfTweets = request.NumberOfTweets;
            this.TwitterUserName = request.TwitterUserName;
            this.RequestId = Guid.NewGuid();
        }

        public Guid RequestId { get; }

        public string AccountId { get; set; }
    }
}
