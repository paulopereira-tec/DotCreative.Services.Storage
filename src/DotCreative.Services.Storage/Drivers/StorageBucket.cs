using DotCreative.Services.Storage.Interfaces;

namespace DotCreative.Services.Storage.Drivers;

public class StorageBucket : IStorageBucket
{
    public string Name { get; set; }

    public string Code { get; set; }

    public DateTime CreationDate { get; set; }

    public StorageBucket(string name)
    {
        Name = name;
    }

    public StorageBucket(string name, DateTime creationDate)
    {
        Name = name;
        CreationDate = creationDate;
    }

    public StorageBucket(string name, string code)
    {
        Name = name;
        Code = code;
    }
}
