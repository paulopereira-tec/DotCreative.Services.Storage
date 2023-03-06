namespace DotCreative.Services.Storage.Interfaces;

/// <summary>
/// Define as propriedades presentes no objeto padrão;
/// </summary>
public interface IStorageObject
{
  /// <summary>
  /// Nome do objeto.
  /// </summary>
  public string Name { get; set; }

  /// <summary>
  /// Data da última modificação do objeto.
  /// </summary>
  public DateTime? LastModification { get; set; }

  /// <summary>
  /// URL para download do objeto. Poderá ser uma URL assinada.
  /// </summary>
  public string? Url { get; set; }

  /// <summary>
  /// Código do objeto armazenado. Em alguns casos, pode ser chamado de "versão".
  /// </summary>
  public string Code { get; set; }

  /// <summary>
  /// Tamanho do objeto.
  /// </summary>
  public long Size { get; set; }
}