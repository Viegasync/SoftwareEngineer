namespace Learning.Asynchronous.Extensions;

/// <summary>
/// Classe estática que contém métodos de 
/// extensão para <see cref="IEnumerable{T}"/>.
/// </summary>
internal static class EnumerableExtensions
{
    /// <summary>
    /// Exibe no console todas 
    /// as mensagens de erro da coleção.
    /// </summary>
    internal static void DisplayErrors<TSource>(this IEnumerable<TSource> source)
    {
        DisplayIfEmpty(source, "No errors.");
        foreach (TSource? item in source)
            Console.WriteLine(item);
    }

    /// <summary>
    /// Exibe no console todas as propriedades
    /// não nulas de cada item da coleção.
    /// </summary>
    internal static void DisplayProperties<TSource>(this IEnumerable<TSource> source)
    {
        DisplayIfEmpty(source);
        foreach (TSource item in source)
            Console.WriteLine(GetPropertyValue(item));
    }

    /// <summary>
    /// Imprime uma mensagem no console 
    /// caso a coleção esteja vazia.
    /// </summary>
    private static void DisplayIfEmpty<TSource>(IEnumerable<TSource> source, string message = "Empty")
    {
        if (!source.Any())
            Console.WriteLine(message);
    }

    /// <summary>
    /// Retorna o valor de todas
    /// as propriedades não nulas.
    /// </summary>
    private static StringBuilder GetPropertyValue<T>(T? item)
    {
        StringBuilder builder = new();

        PropertyInfo[] properties = [.. typeof(T)
            .GetProperties()
            .Where(property =>
                property.GetIndexParameters().Length == 0)
            ];

        foreach (PropertyInfo property in properties)
        {
            if (item is null) continue;

            object? currentValue = property.GetValue(item);

            if (currentValue is string value && !string.IsNullOrWhiteSpace(value))
                builder.AppendLine($"{property.Name}: {value}");
        }

        return builder;
    }
}