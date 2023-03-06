namespace DotCreative.Services.Storage.Interfaces;

public interface IStorageObjects
{
  /// <summary>
  /// Alista os objetos presentes em um bucket.
  /// </summary>
  /// <param name="bucketName">Nome do bucket onde serão localizados os objetos.</param>
  /// <returns>Objetos localizados.</returns>
  public Task<ICollection<IStorageObject>> List(string bucketName);

  /// <summary>
  /// Alista os objetos presentes em um bucket.
  /// </summary>
  /// <param name="bucket">Bucket onde serão localizados os objetos.</param>
  /// <returns>Objetos localizados.</returns>
  public Task<ICollection<IStorageObject>> List(IStorageBucket bucket);

  /// <summary>
  /// Recupera um objeto assinado em específico
  /// </summary>
  /// <param name="objectName"></param>
  public Task<IStorageObject> Get(IStorageObject objectName, IStorageBucket bucket);

  /// <summary>
  /// Cria um objeto - a partir do local onde o arquivo está armazenado.
  /// </summary>
  /// <param name="fileName">Nome do arquivo.</param>
  /// <param name="filePath">Local onde o arquivo está.</param>
  /// <param name="bucket">Bucket onde o objeto deverá ser criado.</param>
  public Task<IStorageObject?> Create(string fileName, string filePath, IStorageBucket bucket);

  /// <summary>
  /// Cria um objeto - a partir dos bytes do arquivo.
  /// </summary>
  /// <param name="fileName">Nome do arquivo.</param>
  /// <param name="fileBytes">Bytes do arquivo.</param>
  /// <param name="bucket">Bucket onde o objeto deverá ser criado.</param>
  public Task<IStorageObject?> Create(string fileName, byte[] fileBytes, IStorageBucket bucket);

  /// <summary>
  /// Cria um objeto - a partir do stream de bytes do arquivo.
  /// </summary>
  /// <param name="fileName">Nome do arquivo.</param>
  /// <param name="fileStream">Stream de bytes do arquivo.</param>
  /// <param name="bucket">Bucket onde o objeto deverá ser criado.</param>
  public Task<IStorageObject?> Create(string fileName, MemoryStream fileStream, IStorageBucket bucket);

  /// <summary>
  /// Apaga um objeto específico.
  /// </summary>
  /// <param name="obj">Objeto a ser apagado.</param>
  /// <param name="bucket">Bucket onde o objeto está armazenado.</param>
  /// <returns></returns>
  public Task<bool> Delete(IStorageObject obj, IStorageBucket bucket);
}
