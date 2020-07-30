using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Couchbase;
using Couchbase.Authentication;
using Couchbase.Configuration.Client;
using Couchbase.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Strike.Common.Models.Feeds;

namespace FeedServiceWorker
{
    public class FeedServiceWorker : BackgroundService
    {
        private readonly ILogger<FeedServiceWorker> _logger;
        private readonly IBucket _bucket;
        public FeedServiceWorker(ILogger<FeedServiceWorker> logger)
        {
            var cluster = InitializeCouchDatabase("http://127.0.0.1:8091/", "Administrator", "Sugenthran@09");
            _bucket = cluster.OpenBucket("feeds");
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var rnd = new Random();
                
                var testFeed = new TestFeed();
                testFeed.ID = Guid.NewGuid().ToString();
                testFeed.SiteName = "Mob HollywoodBets";
                testFeed.Teams = "Swindle Fc vs Trumps Wall United";
                testFeed.Odds = rnd.NextDouble() * 100;
                
                var document = new Document<TestFeed>
                {
                    Id = testFeed.ID,
                    Content = testFeed
                };
                var result = _bucket.Upsert<TestFeed>(document);

                _logger.LogInformation($"Added Event: {result}");
                await Task.Delay(2000, stoppingToken);
            }
        }

        public Cluster InitializeCouchDatabase(string server, string username, string password)
        {
            var cluster = new Cluster(new ClientConfiguration
            {
                Servers = new List<Uri> { new Uri(server) }
            });

            var authenticator = new PasswordAuthenticator(username, password);
            cluster.Authenticate(authenticator);

            return cluster;
        }

    }
}
