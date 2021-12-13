using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CrytpoInfo.Models
{
    public class TwitterUserGetTweetsReponse
    {
        public IEnumerable<DataObject> Data { get; set; }

        public MetaData Meta { get; set; }

        public class MetaData
        {
            public string OldestId { get; set; }

            public string NewestId { get; set; }

            public int ResultsCount { get; set; }

            public string NextToken { get; set; }
        }

        public class DataObject
        {
            public DateTime CreatedAt { get; set; }

            public string Id { get; set; }

            public string Text { get; set; }

            [JsonProperty(PropertyName = "public_metrics")]
            public PublicMetric PublicMetrics { get; set; }

            public class PublicMetric
            {
                [JsonProperty(PropertyName = "retweet_count")]
                public int RetweetCount { get; set; }

                [JsonProperty(PropertyName = "reply_count")]
                public int ReplyCount { get; set; }

                [JsonProperty(PropertyName = "like_count")]
                public int LikeCount { get; set; }

                [JsonProperty(PropertyName = "quote_count")]
                public int QuoteCount { get; set; }
            }
        }
    }
}
