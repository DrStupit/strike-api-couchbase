using System;
using System.Collections.Generic;
using CouchAdapter.Models;
using CouchAdapter.Repositorys;
using Couchbase;
using Couchbase.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

namespace strike_api_couchbase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        public ClientController()
        {
        }

        [HttpGet]
        [Route("getBalance")]
        public List<Balance> GetBalance(long punterId)
        {
            var repo = new BalanceRepository();

            return repo.GetBalanceAsync(punterId);
        }
    }
}