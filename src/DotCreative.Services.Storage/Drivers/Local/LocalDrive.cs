namespace DotCreative.Services.Storage.Drivers.Local;

public partial class LocalDrive : IStorageDrive<LocalClient>
{
  public LocalClient Client { get; set; }

  public LocalDrive(string directory)
  {
    Client = new(directory);
  }

  public LocalDrive(LocalClient client)
  {
    Client = client;
  }
}
