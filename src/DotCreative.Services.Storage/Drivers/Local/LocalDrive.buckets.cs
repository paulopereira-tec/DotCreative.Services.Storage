namespace DotCreative.Services.Storage.Drivers.Local;

public partial class LocalDrive : IStorageBuckets
{
  public async Task<bool> Create(IStorageBucket bucket)
  {
    return await Task.Run(() =>
    {
      DirectoryInfo info = Directory.CreateDirectory(Client.DirectoryBase + "/" + bucket.Name);

      if (Directory.Exists(info.FullName))
      {
        return true;
      }
      else
      {
        return false;
      }
    });
  }

  public async Task<bool> Delete(IStorageBucket bucket)
  {
    return await Task.Run(() =>
    {
      string fullPath = Client.DirectoryBase + "/" + bucket.Name;

      if (Directory.Exists(fullPath))
      {
        Directory.Delete(fullPath);

        return true;
      }
      else
      {
        return false;
      }
    });
  }

  public async Task<ICollection<IStorageBucket>> List()
  {
    return await Task.Run(() =>
    {
      string fullPath = Client.DirectoryBase;
      ICollection<IStorageBucket> buckets = new List<IStorageBucket>();

      if (Directory.Exists(fullPath))
      {
        string[] directories = Directory.GetDirectories(fullPath);

        foreach(string directory in directories)
        {
          DateTime date = Directory.GetCreationTime(directory);
          StorageBucket bucket = new StorageBucket(directory, date);
          buckets.Add(bucket);
        }
      }

      return buckets;
    });
  }

}
