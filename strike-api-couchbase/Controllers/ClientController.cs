using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CouchAdapter.Models;
using CouchAdapter.Repositorys;
using Couchbase.Core;
using Couchbase.Extensions.DependencyInjection;
using Couchbase.N1QL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            var n1ql = "SELECT * FROM `balance` WHERE punterID = $punterID;";
            var query = QueryRequest.Create(n1ql);
            query.AddNamedParameter("$punterID", punterId);
            var result = _bucket.Query<Balance>(query);

            return result.Rows;
        }
    }
}