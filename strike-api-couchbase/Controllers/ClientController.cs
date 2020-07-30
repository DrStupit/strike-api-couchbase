using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Couchbase;
using Couchbase.Core;
using Couchbase.Extensions.DependencyInjection;
using Couchbase.N1QL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using strike_api_couchbase.Models.Balance;

namespace strike_api_couchbase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IBucket _bucket;
        public ClientController(IBucketProvider bucketProvider, ILogger<ClientController> logger)
        {
            _bucket = bucketProvider.GetBucket("balance");
            _logger = logger;
        }

        [HttpGet] 
        [Route("getBalance")]
        public List<Balance> GetBalance(long punterId)
        {
            var records = new List<Balance>();
            var n1ql = "SELECT d.* FROM balance d WHERE punterID = $punterID";
            var queryRequest = QueryRequest.Create(n1ql);
            queryRequest.AddNamedParameter("$punterID", punterId);
            var queryResult = _bucket.Query<Balance>(queryRequest);

            if(!queryResult.Success)
            {
                _logger.LogInformation($"Failed to get balance for {punterId}");
            } else
            {
                foreach (var row in queryResult.Rows)
                {

                    records.Add(row);
                }
            }
            return records;
        }

        [HttpPost]
        [Route("updateBalance")]
        public string UpdateBalance(Balance balance)
        {
            if(string.IsNullOrEmpty(balance.ID))
            {
                balance.ID = Guid.NewGuid().ToString();
            }
            var result = _bucket.Upsert<Balance>(balance.ID, balance);

            if(result.Success)
            {
                return balance.ID;
            }
            else
            {
                return "Failed to update/insert Punter Balance";
            }
        }
    }
}