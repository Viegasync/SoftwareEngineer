namespace Learning.Functional.Extensions;

/// <summary>
/// Método extensivo
/// </summary>
internal static class ObjectExtensions
{
    /// <summary>
    /// Converte um objeto em 
    /// uma sequência enumerável.
    /// </summary>
    internal static IEnumerable<T> AsEnumerable<T>(this T? item)
    {
        if (item is null) yield break;
        yield return item;
    }
}