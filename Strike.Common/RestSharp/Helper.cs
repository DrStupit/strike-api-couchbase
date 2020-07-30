using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using Strike.Common.Models.Feeds;
using Strike.Common.Models.Odds;
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

        public OddsResponse GetOddsBySportName(string sport_key, string region, string mkt)
        {
            var client = new RestClient("https://api.the-odds-api.com/");
            var request = new RestRequest($"v3/odds/?apiKey=0ed1b389d971eb0c5c4de6618dd87484&sport={sport_key}&region={region}&mkt={mkt}", Method.GET);
            var response = client.Get<OddsResponse>(request);
            return JsonConvert.DeserializeObject<OddsResponse>(response.Content);
        }
    }
}
