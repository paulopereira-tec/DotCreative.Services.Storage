namespace DotCreative.Services.Storage.Interfaces;

/// <summary>
/// Credenciais de acesso ao storage drive
/// </summary>
public interface IStorageCredential
{
  /// <summary>
  /// Chave de acesso
  /// </summary>
  public string Key { get; set; }

  /// <summary>
  /// Palavra passe
  /// </summary>
  public string Secret { get; set; }
}