using CrytpoInfo.Models;
using System.Collections.Generic;

namespace CrytpoInfo.Core.Repositories
{
    public interface ITwitterRepository
    {
        IEnumerable<BaseTweet> GetNumberOfTweetsForAccountID(TwitterUserCrytpoDataRequestInternal twitterUserCrytpoDataRequestInternal);

        string GetAccountId(TwitterUserCrytpoDataRequestInternal twitterUserCrytpoDataRequestInternal);
    }
}
