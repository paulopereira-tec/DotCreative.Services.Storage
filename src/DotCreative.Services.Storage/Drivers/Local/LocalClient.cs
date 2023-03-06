namespace DotCreative.Services.Storage.Drivers.Local;

public class LocalClient
{
  public string DirectoryBase { get; private set; }
  public string UrlBase { get; private set; }

  public LocalClient(string directoryBase)
  {
    if (Directory.Exists(directoryBase))
    {
      DirectoryBase = directoryBase;
    }
    else
    {
      throw new ArgumentException("O diretório informado como base não existe.");
    }
  }

  public LocalClient(string directoryBase, string urlBase) : this(directoryBase)
  {
    UrlBase = urlBase;
  }
}
