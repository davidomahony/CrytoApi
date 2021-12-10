using CrytpoInfo.Core.Twitter;
using CrytpoInfo.Models;
using System.Collections.Generic;

namespace CrytpoInfo.Core.Repositories
{
    public interface ITwitterRepository
    {
        IEnumerable<BaseTweet> GetNumberOfTweetsForAccountID(string accountId, int numberOfTweets);
    }
}
