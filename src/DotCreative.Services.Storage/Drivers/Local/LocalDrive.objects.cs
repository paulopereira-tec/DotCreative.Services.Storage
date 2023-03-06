using System.Security.Cryptography;
using System.Text;

namespace DotCreative.Services.Storage.Drivers.Local;

public partial class LocalDrive : IStorageObjects
{
  public async Task<IStorageObject?> Create(string fileName, string filePath, IStorageBucket bucket)
  {
    return await Task.Run(() =>
    {
      string destinyPath = Client.DirectoryBase + "/" + bucket.Name + "/" + fileName;
      File.Copy(filePath, destinyPath, true);

      IStorageObject obj = new StorageObject(fileName);

      return Get(obj, bucket);
    });
  }

  public async Task<IStorageObject?> Create(string fileName, byte[] fileBytes, IStorageBucket bucket)
  {
    string fullPath = Client.DirectoryBase + "/" + bucket.Name + "/" + fileName;

    if (Directory.Exists(fullPath))
      throw new Exception("O bucket informado não existe.");

    await File.WriteAllBytesAsync(fullPath, fileBytes);

    IStorageObject obj = new StorageObject(fileName);

    return await Get(obj, bucket);
  }

  public async Task<IStorageObject?> Create(string fileName, MemoryStream memoryStream, IStorageBucket bucket)
  {
    return await Task.Run(() => {
      string fullPath = Client.DirectoryBase + "/" + bucket.Name + "/" + fileName;

      if (Directory.Exists(fullPath))
        throw new Exception("O bucket informado não existe.");

      FileStream file = new FileStream(fullPath, FileMode.Create, FileAccess.Write);

      byte[] bytes = new byte[memoryStream.Length];
      memoryStream.Read(bytes, 0, (int)memoryStream.Length);
      file.Write(bytes, 0, bytes.Length);
      memoryStream.Close();

      IStorageObject obj = new StorageObject(fileName);

      return Get(obj, bucket);
    });
  }

  public async Task<bool> Delete(IStorageObject obj, IStorageBucket bucket)
  {
    await Task.Run(() =>
    {
      string fullPath = Client.DirectoryBase + "/" + bucket.Name + "/" + obj.Name;

      if (!File.Exists(fullPath))
        throw new Exception("O objeto especificado não existe.");

      File.Delete(fullPath);
    });

    return true;
  }

  public async Task<IStorageObject> Get(IStorageObject storageObject, IStorageBucket bucket)
  {
    return await Task.Run(() =>
    {
      string fullPath = Client.DirectoryBase + "/" + bucket.Name + "/" + storageObject.Name;

      if (!File.Exists(fullPath))
        throw new Exception("O objeto especificado não existe.");

      FileInfo fileInfo = new FileInfo(fullPath);

      string md5 = ComputeMD5(fullPath);
      DateTime dateFile = fileInfo.LastWriteTime;
      long filesize = fileInfo.Length;
      string url = Client.UrlBase + "/" + bucket.Name + "/" + storageObject.Name;

      IStorageObject obj = new StorageObject(storageObject.Name, dateFile, md5, filesize, url);

      return obj;
    });
  }

  public async Task<ICollection<IStorageObject>> List(string bucketName)
  {
    string fullPath = Client.DirectoryBase + "/" + bucketName;

    if (!Directory.Exists(fullPath))
      throw new Exception("O bucket informado não existe.");

    ICollection<IStorageObject> objects = new List<IStorageObject>();

    string[] files = Directory.GetFiles(fullPath);

    foreach (string file in files)
    {
      FileInfo fileInfo = new FileInfo(file);

      string md5 = ComputeMD5(file);
      DateTime dateFile = fileInfo.LastWriteTime;
      long filesize = fileInfo.Length;
      string url = Client.UrlBase + "/" + file;

      IStorageObject storageObject = new StorageObject(file, dateFile, md5, filesize, url);
      IStorageBucket bucket = new StorageBucket(bucketName);

      storageObject = await Get(storageObject, bucket);

      objects.Add(storageObject);
    }

    return objects;
  }

  public async Task<ICollection<IStorageObject>> List(IStorageBucket bucket)
  => await List(bucket.Name);

  /// <summary>
  /// Calcula o hash MD5 do arquivo a partir dos bytes.
  /// </summary>
  /// <param name="filePath">Caminho completo onde está o arquivo.</param>
  /// <returns>Hash MD5 do arquivo.</returns>
  private string ComputeMD5(string filePath)
  {
    byte[] fileBytes = File.ReadAllBytes(filePath);
    string base64File = Convert.ToBase64String(fileBytes);

    StringBuilder sb = new StringBuilder();

    // Initialize a MD5 hash object
    using (MD5 md5 = MD5.Create())
    {
      // Compute the hash of the given string
      byte[] hashValue = md5.ComputeHash(Encoding.UTF8.GetBytes(base64File));

      // Convert the byte array to string format
      foreach (byte b in hashValue)
      {
        sb.Append($"{b:X2}");
      }
    }

    return sb.ToString();
  }
}
