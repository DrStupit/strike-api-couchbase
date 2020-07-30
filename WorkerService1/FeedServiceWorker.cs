using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Couchbase;
using Couchbase.Authentication;
using Couchbase.Configuration.Client;
using Couchbase.Core;
using FeedService;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Strike.Common.Models.Feeds;
using Strike.Common.RestSharp;

namespace FeedServiceWorker
{
    public class FeedServiceWorker : BackgroundService
    {
        private readonly ILogger<FeedServiceWorker> _logger;
        private readonly IBucket _bucket;
        public FeedServiceWorker(ILogger<FeedServiceWorker> logger)
        {
            var couch = new CouchConnector();
            var cluster = couch.InitializeCouchDatabase("http://127.0.0.1:8091/", "Administrator", "Sugenthran@09");
            _bucket = cluster.OpenBucket("feeds");
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var getSportHelper = new Helper();

                var sportData = getSportHelper.GetSports();
                var sportList = sportData.Data;

                foreach (var sport in sportList)
                {
                    var document = new Document<Sport>
                    {
                        Id = sport.Key,
                        Content = sport
                    };
                    _logger.LogInformation($"Sport Inserted to couch: {sport}");
                    var result = _bucket.Upsert<Sport>(document);
                }

                await Task.Delay(3600000, stoppingToken);
            }
        }
    }
}
