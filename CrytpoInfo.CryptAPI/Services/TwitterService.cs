using CrytpoInfo.Core.Repositories;
using CrytpoInfo.Core.Twitter;
using CrytpoInfo.Models;

namespace CrytpoInfo.CryptAPI.Services
{
    public class TwitterService
    {
        private ITwitterRepository twitterRepository;

        public TwitterService(ITwitterRepository twitterRepository)
        {
            this.twitterRepository = twitterRepository;
        }

        public TwitterUserCrytpoDataResponse GetUsersCryptoTweetsResponse(TwitterUserCrytpoDataRequestInternal requestInternal)
        {
            var resultFromRepository = this.twitterRepository.GetNumberOfTweetsForAccountID(requestInternal.TwitterUserName, requestInternal.NumberOfTweets);

            var filteredTweets = TwitterTweetUtility.FilterTweetsOnCondition(
                resultFromRepository,
                tweet => tweet.Text.Contains(requestInternal.CurrencyName));

            return new TwitterUserCrytpoDataResponse()
            {
                RequestId = requestInternal.RequestId,
                Results = new TwitterUserCryptoResults()
                {
                    Results = filteredTweets
                },
                Success = true
            };
        }
    }
}
