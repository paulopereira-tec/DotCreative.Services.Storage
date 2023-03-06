namespace DotCreative.Services.Storage.Drivers;

public class StorageObject : IStorageObject
{
  public string Name { get; set; }
  public DateTime? LastModification { get; set; }
  public string? Url { get; set; }
  public string Code { get; set; }
  public long Size { get; set; }

  public StorageObject(string name, DateTime? lastModification, string code, long size, string? url = null) : this(name, lastModification, url)
  {
    Code = code;
    Size = size;
  }

  public StorageObject(string name, DateTime? lastModification, string? url)
  {
    Name = GetName(name);
    LastModification = lastModification;
    Url = url;
  }

  public StorageObject(string name, string code)
  {
    Name = GetName(name);
    Code = code;
  }

  public StorageObject(string name)
  {
    Name = GetName(name);
  }

  private string GetName(string name)
  {
    string[] splitedName = new string[] {};

    if (name.Contains("/"))
    {
      splitedName  = name.Split("/");
    }

    if (name.Contains("\\"))
    {
      splitedName = name.Split("\\");
    }

    if (name.Contains("\\") || name.Contains("/"))
    {
      return name = splitedName[splitedName.Length - 1];
    }
    else
    {
      return name;
    }
  }
}
