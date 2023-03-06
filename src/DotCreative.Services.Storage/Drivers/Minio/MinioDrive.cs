using Minio;
using System.Net;

namespace DotCreative.Services.Storage.Drivers.Minio;

public partial class MinioDrive : IStorageDrive<MinioClient>
{
  public MinioClient Client { get; set; }

  public MinioDrive(string endpoint, IStorageCredential credential)
  {
    Client = new MinioClient()
      .WithEndpoint(endpoint)
      .WithCredentials(credential.Key, credential.Secret)
      .WithSSL(true)
      .Build();
  }

}
