namespace DotCreative.Services.Storage.Interfaces;

public interface IStorageBuckets
{
  /// <summary>
  /// Alista os buckets (discos) disponíveis no drive storage.
  /// </summary>
  /// <returns></returns>
  public Task<ICollection<IStorageBucket>> List();

  /// <summary>
  /// Cria um novo bucket.
  /// </summary>
  /// <param name="bucket">Dados do novo bucket a ser criado.</param>
  public Task<bool> Create(IStorageBucket bucket);

  /// <summary>
  /// Apaga um bucket.
  /// </summary>
  /// <param name="bucket">Bucket a ser deletado.</param>
  public Task<bool> Delete(IStorageBucket bucket);
}