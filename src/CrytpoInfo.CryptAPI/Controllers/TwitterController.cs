using CrytpoInfo.CryptAPI.Services;
using CrytpoInfo.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CrytpoInfo.CryptAPI.Controllers
{
    public class TwitterController : ControllerBase
    {
        private TwitterService twitterService;

        public TwitterController(TwitterService twitterService)
        {
            this.twitterService = twitterService;
        }

        [HttpGet]
        [Route("Info")]
        public IActionResult Info()
        {
            return Ok("I am Alive");
        }

        [HttpGet]
        [Route("GetUsersCryptoTweets")]
        public IActionResult GetUsersCryptoTweets([FromQuery][Required] TwitterUserCrytpoDataRequest requestInfo)
        {
            var internalRequest = new TwitterUserCrytpoDataRequestInternal(requestInfo);

            var responseBody = this.twitterService.GetUsersCryptoTweetsResponse(internalRequest);

            return Ok(responseBody);
        }
    }
}
