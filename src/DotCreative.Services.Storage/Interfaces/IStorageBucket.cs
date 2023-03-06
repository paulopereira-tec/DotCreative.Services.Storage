namespace DotCreative.Services.Storage.Interfaces;

/// <summary>
/// Modelo unificado para manipulação de buckets.
/// </summary>
public interface IStorageBucket
{
  /// <summary>
  /// Nome do bucket.
  /// </summary>
  public string Name { get; set; }

  /// <summary>
  /// Código do bucket.
  /// </summary>
  public string Code { get; set; }

  /// <summary>
  /// Data de criação do bucket.
  /// </summary>
  public DateTime CreationDate { get; set; }
}