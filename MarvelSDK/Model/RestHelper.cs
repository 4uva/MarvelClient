using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MarvelSDK.Model
{
    // sends request and decodes response from json
    class RestHelper
    {
        HttpClient client;
       
        public RestHelper()
        {
            client = new HttpClient()
            {
                BaseAddress =
                    new Uri("https://gateway.marvel.com:443/v1/public/")
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<T> GetAsync<T>(
            string webApiAddress,
            Dictionary<string, object> parameters)
        {
            var queryString = ComposeQueryString(parameters);
            HttpResponseMessage response = await client.GetAsync(webApiAddress + queryString);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<T>();
        }

        private string ComposeQueryString(Dictionary<string, object> parameters)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var keyValue in parameters)
            {
                var key = WebUtility.HtmlEncode(keyValue.Key);
                var value = WebUtility.HtmlEncode(keyValue.Value.ToString());
                sb.AppendFormat("&{0}={1}", key, value);
            }
            if (sb.Length > 0)
                sb[0] = '?';
            return sb.ToString();
        }
    }
}
