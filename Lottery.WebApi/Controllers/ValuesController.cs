using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Lottery.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Lottery.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ValuesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var requestUri = "https://www.baidu.com/";

            var response = await new HttpRequestFactory(_httpClientFactory).Get(requestUri);

            var outputModel = response.ContentAsString();

            return Content(outputModel);
        }
    }
}
