using Minio;
using Minio.DataModel;

namespace DotCreative.Services.Storage.Drivers.Minio;

public partial class MinioDrive : IStorageObjects
{
  public async Task<IStorageObject?> Create(string fileName, string filePath, IStorageBucket bucket)
  {
    if (!File.Exists(filePath))
      throw new InvalidOperationException("O arquivo não existe.");

    byte[] fileBytes = File.ReadAllBytes(filePath);

    return await Create(fileName, fileBytes, bucket);
  }

  public async Task<IStorageObject?> Create(string fileName, byte[] fileBytes, IStorageBucket bucket)
  {
    MemoryStream fileStream = new MemoryStream(fileBytes);
    return await Create(fileName, fileStream, bucket);
  }

  public async Task<IStorageObject?> Create(string fileName, MemoryStream fileStream, IStorageBucket bucket)
  {
    try
    {
      string code = Guid.NewGuid().ToString();
      long size = fileStream.Length;

      PutObjectArgs putObjectArgs = new PutObjectArgs()
        .WithBucket(bucket.Name)
        .WithObject(fileName)
        .WithStreamData(fileStream)
        .WithObjectSize(size)
        .WithContentType("application/octet-stream")
        .WithVersionId(code)
        ;

      await Client.PutObjectAsync(putObjectArgs);

      IStorageObject obj = new StorageObject(fileName, DateTime.Now, code, size);

      return await Get(obj, bucket);
    }
    catch
    {
      return null;
    }
  }

  public async Task<bool> Delete(IStorageObject obj, IStorageBucket bucket)
  {
    IStorageObject objToDelete = await Get(obj, bucket);

    if (objToDelete is null)
      throw new Exception("O objeto a ser apagado não existe.");

    RemoveObjectArgs args = new RemoveObjectArgs()
      .WithObject(obj.Name)
      .WithBucket(bucket.Name);

    await Client.RemoveObjectAsync(args);

    return true;
  }

  public async Task<IStorageObject> Get(IStorageObject storageObject, IStorageBucket bucket)
  {
    int minutes = 60;
    int seconds = 60;
    int hours = 1;

    PresignedGetObjectArgs presignedGetObjectArgs = new PresignedGetObjectArgs()
      .WithObject(storageObject.Name)
      .WithBucket(bucket.Name)
      .WithExpiry(seconds * minutes * hours);

    string signedUrl = await Client.PresignedGetObjectAsync(presignedGetObjectArgs);

    return new StorageObject(storageObject.Name, storageObject.LastModification, storageObject.Code, storageObject.Size, signedUrl);

  }

  public IStorageObject Get(string objectName)
  {
    throw new NotImplementedException();
  }

  public async Task<ICollection<IStorageObject>> List(string bucketName)
  {
    throw new NotImplementedException("Este recurso ainda não foi implementado.");
  }

  public async Task<ICollection<IStorageObject>> List(IStorageBucket bucket)
  => await List(bucket.Name);
}
