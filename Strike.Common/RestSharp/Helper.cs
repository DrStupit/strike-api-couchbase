using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using Strike.Common.Models.Feeds;
using System.Collections.Generic;

namespace Strike.Common.RestSharp
{
    public class Helper
    {
        public SportResponse GetSports()
        {
            var client = new RestClient("https://api.the-odds-api.com/");
            var request = new RestRequest("v3/sports/?apiKey=0ed1b389d971eb0c5c4de6618dd87484", Method.GET);
            var response = client.Get<SportResponse>(request);
            return JsonConvert.DeserializeObject<SportResponse>(response.Content);
        }
    }
}
