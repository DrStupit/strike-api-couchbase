using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Couchbase;
using Couchbase.Core;
using Couchbase.Extensions.DependencyInjection;
using Couchbase.N1QL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        public List<Balance> GetBalance(long punterId)
        {
            var records = new List<Balance>();
            var n1ql = "SELECT d.* FROM balance d WHERE punterID = $punterID";
            var qr = QueryRequest.Create(n1ql);
            qr.AddNamedParameter("$punterID", punterId);
            var queryResult = _bucket.Query<Balance>(qr);
            foreach(var row in queryResult.Rows)
            {
                
                records.Add(row);
            }
            return records;
        }

        [HttpPost]
        [Route("updateBalance")]
        public string UpdateBalance(Balance balance)
        {
            balance.ID = Guid.NewGuid().ToString();
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