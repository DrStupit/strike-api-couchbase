using CouchAdapter.Models;
using Couchbase.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace CouchAdapter.Repositorys
{
    public class BalanceRepository
    {

        public BalanceRepository()
        {
        }
        //public void InsertBalance(Balance balance)
        //{
        //    balance.ID = Guid.NewGuid().ToString();
        //    _bucket.Upsert(balance.ID, balance);
        //}

        //public List<Balance> GetBalance(long punterId)
        //{
        //    var n1ql = "SELECT * FROM `balance` WHERE punterID = $punterID;";
        //    var query = QueryRequest.Create(n1ql);
        //    query.AddNamedParameter("$punterID", punterId);
        //    var result = _bucket.Query<Balance>(query);

        //    return result.Rows;
        //}

    }
}
