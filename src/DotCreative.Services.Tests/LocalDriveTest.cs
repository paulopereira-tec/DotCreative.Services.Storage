using DotCreative.OpenSource.PrimitiveTypesValidations;
using DotCreative.Services.Storage.Drivers.Local;

namespace DotCreative.Services.Tests;

[TestClass]
public class LocalDriveTest
{
  // Arrange
  private readonly string _bucketName = "novo-bucket-a-ser-criado";
  private readonly string _objectName = "ObjetoNomeXYZ.txt";
  private readonly LocalDrive _drive;
  private readonly IStorageBucket _bucket;

  public LocalDriveTest()
  {
    string directoryBase = @"E:\Downloads";
    string urlBase = "https://localhost:7525/downloads";

    LocalClient client = new (directoryBase, urlBase);
    _drive = new LocalDrive(client);

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
  [TestMethod("Deve esperar sucesso ao enviar um objeto por upload - Cópia de arquivo.")]
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

  [Priority(4)]
  [TestMethod("Deve esperar sucesso ao enviar um objeto por upload - Bytes do arquivo.")]
  public async Task SouldExpectSuccessWhenUploadObject_BasedOnBytesOfFile()
  {
    // Arrange
    string filePath = @"C:\FileText.txt";
    string fileName = _objectName;

    byte[] fileBytes = File.ReadAllBytes(filePath);

    // Act
    IStorageObject? obj = await _drive.Create(fileName, fileBytes, _bucket);

    // Assert
    Assert.IsTrue(obj is not null);
  }

  [Priority(5)]
  [TestMethod("Deve esperar sucesso ao enviar um objeto por upload - Stream de bytes.")]
  public async Task SouldExpectSuccessWhenUploadObject_BasedOnStreamOfBytes()
  {
    // Arrange
    string filePath = @"C:\FileText.txt";
    string fileName = _objectName;

    byte[] fileBytes = File.ReadAllBytes(filePath);

    MemoryStream streamOfBytes = new MemoryStream(fileBytes);

    // Act
    IStorageObject? obj = await _drive.Create(fileName, streamOfBytes, _bucket);

    // Assert
    Assert.IsTrue(obj is not null);
  }

  [Priority(6)]
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

  [Priority(7)]
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

  [Priority(8)]
  [TestMethod("Deve esperar sucesso ao deletar um objeto.")]
  public async Task MustExpectSuccessWhenDeletingAnObject()
  {
    // Arrange - Configura
    IStorageObject obj = new StorageObject(_objectName);
    IStorageBucket bucket = new StorageBucket(_bucketName);

    // Act - Executa
    bool isDeleted = await _drive.Delete(obj, bucket);

    // Assert - Valida
    Assert.IsTrue(isDeleted);
  }

  [Ignore]
  [Priority(9)]
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
