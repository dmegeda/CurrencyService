using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Currency.Services
{
    public class HttpService
    {
        public async Task<string> GetAsync(string url)
        {
            using HttpClient client = new HttpClient();
            using HttpResponseMessage message = await client.GetAsync(url);
            using HttpContent content = message.Content;
            string data = await content.ReadAsStringAsync();

            return data;
        }
    }
}
