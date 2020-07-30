using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Couchbase;
using Couchbase.Core;
using Couchbase.Extensions.DependencyInjection;
using Couchbase.N1QL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Strike.Common.Models.Feeds;
using Strike.Common.Models.Odds;
using Strike.Common.RestSharp;
using strike_api_couchbase.Models.Balance;

namespace strike_api_couchbase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportController : ControllerBase
    {
        private readonly ILogger<SportController> _logger;
        private readonly IBucket _bucket;
        public SportController(IBucketProvider bucketProvider, ILogger<ClientController> logger)
        {
            _bucket = bucketProvider.GetBucket("feeds");
        }

        #region Sports
        [HttpGet]
        [Route("getSports")]
        public List<Sport> GetSports()
        {
            var records = new List<Sport>();
            var n1ql = "SELECT s.* FROM feeds s WHERE active = true";
            var queryRequest = QueryRequest.Create(n1ql);
            var queryResult = _bucket.Query<Sport>(queryRequest);

            if (!queryResult.Success)
            {
                _logger.LogInformation($"Failed to get balance for {queryRequest}");
            }
            else
            {
                foreach (var row in queryResult.Rows)
                {
                    records.Add(row);
                }
            }
            return records;
        }

        [HttpGet]
        [Route("getSportsByName")]
        public List<ListingOptions> GetSportsByName(string sportKey, string region, string mkt) 
        {
            var restHelper = new Helper();
            var oddsData = restHelper.GetOddsBySportName(sportKey, region, mkt);
            var gameList = oddsData.Data;


            foreach (var game in gameList)
            {
                if (game.Sport_Key != null)
                {
                    var key = game.Sport_Key + "" + game.Home_Team + "" + game.Commence_Time;
                    RedisConnection connection = new RedisConnection();
                    connection.SaveKeyValueToDB(key, game.ToString(), 0, 30);
                }
            }
            return gameList;
        }
        #endregion
    }
}