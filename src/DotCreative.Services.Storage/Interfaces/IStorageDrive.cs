namespace DotCreative.Services.Storage.Interfaces;

/// <summary>
/// Implementa as regras de negócio a serem seguidas
/// para acesso aos principais storage drivers; criação,
/// exibição, deleção e listagem de buckets e objetos.
/// </summary>
public interface IStorageDrive<T>
{
  /// <summary>
  /// Cliente que manipular o storage
  /// </summary>
  public T Client { get; set; }
}