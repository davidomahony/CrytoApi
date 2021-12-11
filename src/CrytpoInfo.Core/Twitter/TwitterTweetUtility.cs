using CrytpoInfo.Models;
using System;
using System.Collections.Generic;


namespace CrytpoInfo.Core.Twitter
{
    public class TwitterTweetUtility
    {
        public static IEnumerable<BaseTweet> FilterTweetsOnCondition(IEnumerable<BaseTweet> inputtedTweets, Func<BaseTweet, bool> filterCondition)
        {
            foreach (var tweet in inputtedTweets)
            {
                if (filterCondition(tweet))
                {
                    yield return tweet;
                }
            }
        }
    }
}
