using Currency.Entities;
using Currency.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Currency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyRateController : ControllerBase
    {
        private string _baseUrl = "https://api.cryptonator.com/api/ticker";

        [HttpGet]
        public IActionResult GetSomething()
        {
            return Ok("Working fine!");
        }

        //[Authorize]
        [HttpGet("{baseCurrency}-{targetCurrency}")]
        public async Task<IActionResult> GetRate(string baseCurrency, string targetCurrency)
        {
            string url = _baseUrl + $"/{baseCurrency}-{targetCurrency}";

            HttpService httpService = new HttpService();
            string data = await httpService.GetAsync(url);

            if (data != null)
            {
                return Ok(GetInfo(data));
            }

            return BadRequest("Something went wrong...");
        }

        private CurrencyRateInfo GetInfo(string data)
        {
            var jsonObj = JObject.Parse(data);
            var ticker = jsonObj["ticker"];
            string baseCur = $"{ticker["base"]}";
            string targetCur = $"{ticker["target"]}";
            string price = $"{ticker["price"]}".Replace(".", ",");
            decimal parsedPrice = decimal.Parse(price);
            
            return new CurrencyRateInfo(baseCur, targetCur, parsedPrice);
        }
    }
}
