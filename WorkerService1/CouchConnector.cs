using Couchbase;
using Couchbase.Authentication;
using Couchbase.Configuration.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace FeedService
{
    public class CouchConnector
    {
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
