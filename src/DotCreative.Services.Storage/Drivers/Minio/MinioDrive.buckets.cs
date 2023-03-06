using Minio.DataModel;
using Minio;

namespace DotCreative.Services.Storage.Drivers.Minio;

public partial class MinioDrive: IStorageBuckets
{
  public async Task<ICollection<IStorageBucket>> List()
  {
    ListAllMyBucketsResult buckets = await Client.ListBucketsAsync();
    ICollection<IStorageBucket> myBuckets = new List<IStorageBucket>();

    foreach (Bucket bucket in buckets.Buckets)
    {
      IStorageBucket myBucket = new StorageBucket(bucket.Name, bucket.CreationDate);
      myBuckets.Add(myBucket);
    }

    return myBuckets;
  }

  public async Task<bool> Create(IStorageBucket bucket)
  {
    try
    {
      MakeBucketArgs args = new MakeBucketArgs().WithBucket(bucket.Name);
      await Client.MakeBucketAsync(args);

      return true;
    }
    catch
    {
      return false;
    }
  }

  public async Task<bool> Delete(IStorageBucket bucket)
  {
    try
    {
      await Client.RemoveBucketAsync(bucket.Name);
      return true;
    }
    catch
    {
      return false;
    }
  }
}
