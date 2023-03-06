using DotCreative.OpenSource.PrimitiveTypesValidations;
using DotCreative.Services.Storage.Drivers.Minio;

namespace DotCreative.Services.Tests;

[TestClass]
public class MinioDriveTest
{
  // Arrange
  private readonly string _bucketName = "novo-bucket-a-ser-criado";
  private readonly string _objectName = "ObjetoNomeXYZ";
  private readonly MinioDrive _drive;
  private readonly IStorageBucket _bucket;

  public MinioDriveTest()
  {
    string endpoint = "";
    IStorageCredential credential = new StorageCredential("", "");
    _drive = new MinioDrive(endpoint, credential);

    _bucket = new StorageBucket(_bucketName);
  }

  [Priority(1)]
  [TestMethod("Deve esperar sucesso ao criar um bucket.")]
  public async Task ShouldExpectSuccessWhenCreatingABucket()
  {
    // Arrange
    IStorageBucket bucket = new StorageBucket(_bucketName);

    // Act
    bool isCreated = await _drive.Create(bucket);

    // Assert
    Assert.IsTrue(isCreated);
  }

  [Priority(2)]
  [TestMethod("Deve alistar todos os buckets.")]
  public async Task MustListAllBuckets()
  {
    // Act
    ICollection<IStorageBucket> buckets = await _drive.List();
    bool isOk = buckets.Count.IsGreaterOrEqualsThan(0);

    // Assert
    Assert.IsTrue(isOk);
  }

  [Priority(3)]
  [TestMethod("Deve esperar sucesso ao enviar um objeto por upload.")]
  public async Task SouldExpectSuccessWhenUploadObject_BasedOnFilePath()
  {
    // Arrange
    string filePath = @"C:\FileText.txt";
    string fileName = _objectName;
    
    // Act
    IStorageObject? obj = await _drive.Create(fileName, filePath, _bucket);

    // Assert
    Assert.IsTrue(obj is not null);
  }

  [Priority(5)]
  [TestMethod("Espera sucesso ao recuperar um objeto.")]
  public async Task ShouldExpectSuccesWhenGetObject()
  {
    // Arrange
    IStorageObject storageObject = new StorageObject(_objectName);
    IStorageBucket bucket = new StorageBucket(_bucketName);

    // Act
    IStorageObject obj = await _drive.Get(storageObject, bucket);

    // Assert
    Assert.IsTrue(obj is not null);
  }

  [Ignore]
  [Priority(4)]
  [TestMethod("Deve esperar sucesso ao listar os objetos de um bucket.")]
  public async Task ShouldExpectSuccessWhenListingObjectsOfBucket()
  {
    // Arrange
    IStorageBucket bucket = new StorageBucket(_bucketName);

    // Act
    ICollection<IStorageObject> list = await _drive.List(bucket);

    bool isListed = list
      .Count
      .IsGreaterOrEqualsThan(0);

    // Assert
    Assert.IsTrue(isListed);
  }

  [Ignore]
  [Priority(5)]
  [TestMethod("Deve esperar sucesso ao apagar um bucket.")]
  public async Task ShouldExpectSuccessWhenDeletingABucket()
  {
    // Arrange
    IStorageBucket bucket = new StorageBucket(_bucketName);

    // Act
    bool isDeleted = await _drive.Delete(bucket);

    // Assert
    Assert.IsTrue(isDeleted);
  }

}
