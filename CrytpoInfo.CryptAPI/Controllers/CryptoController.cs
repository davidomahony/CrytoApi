using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrytpoInfo.CryptAPI.Controllers
{
    [Route("v1.0/[controller]")]
    [ApiController]
    public class CryptoController : ControllerBase
    {

        [HttpGet]
        public IActionResult Info()
        {
            return Ok("I am alive");
        }
    }
}
