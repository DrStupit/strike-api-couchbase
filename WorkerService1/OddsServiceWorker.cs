using Couchbase.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Strike.Common.RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FeedService
{
    public class OddsServiceWorker : BackgroundService
    {
        private readonly ILogger<OddsServiceWorker> _logger;
        private readonly IBucket _bucket;
        public OddsServiceWorker(ILogger<OddsServiceWorker> logger)
        {

            var couch = new CouchConnector();
            var cluster = couch.InitializeCouchDatabase("http://127.0.0.1:8091/", "Administrator", "Sugenthran@09");
            _bucket = cluster.OpenBucket("feeds");
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //var getOddsHelper = new Helper();

            //var oddsData = getOddsHelper.GetSports();
            //var sportList = sportData.Data;

            //foreach (var sport in sportList)
            //{
            //    var document = new Document<Sport>
            //    {
            //        Id = sport.Key,
            //        Content = sport
            //    };
            //    _logger.LogInformation($"Sport Inserted to couch: {sport}");
            //    var result = _bucket.Upsert<Sport>(document);
            //}
            await Task.Delay(1800000, stoppingToken);
        }
    }
}
