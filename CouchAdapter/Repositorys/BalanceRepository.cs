using CouchAdapter.Models;
using Couchbase;
using Couchbase.KeyValue;
//using Couchbase.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CouchAdapter.Repositorys
{
    public class BalanceRepository
    {
        private readonly IBucket _bucket;

        public BalanceRepository(IBucketProvider bucketProvider)
        {
            _bucket = bucketProvider.GetBucket("balance");
        }
        public BalanceRepository()
        {

        }
        public async Task InsertBalanceAsync(Balance balance)
        {
            var collection = _bucket.DefaultCollection();
            balance.ID = Guid.NewGuid().ToString();
            var result = await collection.UpsertAsync(balance.ID, balance);
        }

        public async List<Balance> GetBalanceAsync(long punterId)
        {
            var result = new Balance();
            var queryResult = await _bucket.Cluster.QueryAsync<Balance>($"SELECT * FROM `balance` WHERE punterID = {punterId}", new Couchbase.Query.QueryOptions());
            await foreach(var row in queryResult)
            {
                result = row;
            }

            return result;
        }

    }
}
