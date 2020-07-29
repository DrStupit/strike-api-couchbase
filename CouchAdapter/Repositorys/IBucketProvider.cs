using Couchbase;

namespace CouchAdapter.Repositorys
{
    public interface IBucketProvider
    {
        IBucket GetBucket(string v);
    }
}