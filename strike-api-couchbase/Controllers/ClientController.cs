using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Couchbase;
using Couchbase.Core;
using Couchbase.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using strike_api_couchbase.Models.Balance;

namespace strike_api_couchbase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IBucket _bucket;
        public ClientController(IBucketProvider bucketProvider)
        {
            _bucket = bucketProvider.GetBucket("balance");
        }
        [HttpGet]
        [Route("getBalance")]
        public Task<List<Balance>> GetBalance(long punterId)
        {
            var result = new List<Balance>();
            var queryResult = _bucket.QueryAsync<dynamic>($"SELECT * FROM `balance` WHERE punterID = {punterId}").Result;
            foreach (var row in queryResult.Rows)
            {
                result = row;
            }
            return null;
        }
    }
}