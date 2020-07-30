using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace strike_api_couchbase
{
    public class RedisConnection
    {
        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            return ConnectionMultiplexer.Connect("http://localhost:6379,abortConnect=false");
        });

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }

        public void SaveKeyValueToDB(string key, string value, int redisDBNo, int expiryTimeInMinutes)
        {
            var redisDb = Connection.GetDatabase(redisDBNo);
            redisDb.StringSet(key, value);
            redisDb.KeyExpire(key, DateTime.Now.AddMinutes(expiryTimeInMinutes));
        }

        public string GetValueFromKey(string key, int redisDBNo)
        {
            var redisDb = Connection.GetDatabase(redisDBNo);
            return redisDb.StringGet(key);
        }

        public bool DeleteKeyValue(string key, int redisDBNo)
        {
            var redisDb = Connection.GetDatabase(redisDBNo);
            return redisDb.KeyDelete(key);
        }
    }
}
