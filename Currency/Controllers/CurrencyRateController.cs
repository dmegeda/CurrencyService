using Currency.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Currency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyRateController : ControllerBase
    {
        private string _baseUrl = "https://api.cryptonator.com/api/ticker";

        [HttpGet("/{baseCurrency}-{targetCurrency}")]
        public async Task<IActionResult> GetRate(string baseCurrency, string targetCurrency)
        {
            string url = _baseUrl + $"/{baseCurrency}-{targetCurrency}";
            using HttpClient client = new HttpClient();
            using HttpResponseMessage message = await client.GetAsync(url);
            using HttpContent content = message.Content;
            string data = await content.ReadAsStringAsync();

            if (data != null)
            {
                var jsonObj = JObject.Parse(data);
                string baseCur = $"{jsonObj["ticker"]["base"]}";
                string targetCur = $"{jsonObj["ticker"]["target"]}";
                string price = $"{jsonObj["ticker"]["price"]}".Replace(".", ",");
                decimal parsedPrice = decimal.Parse(price);
                CurrencyRateInfo info = new CurrencyRateInfo(baseCur, targetCur, parsedPrice);
                return Ok(info);
            }

            return BadRequest("Something went wrong...");
        }
    }
}
