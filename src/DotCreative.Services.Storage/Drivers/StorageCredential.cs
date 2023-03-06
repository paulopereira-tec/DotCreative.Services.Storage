namespace DotCreative.Services.Storage.Drivers;

public class StorageCredential : IStorageCredential
{
  public string Key { get; set; }

  public string Secret { get; set; }

  public StorageCredential(string key, string secret)
  {
    Key = key;
    Secret = secret;
  }
}
