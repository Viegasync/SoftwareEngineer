namespace Learning.Functional.Extensions;

/// <summary>
/// Método extensivo
/// </summary>
internal static class EnumerableExtensions
{
    /// <summary>
    /// Exibe no console todas as propriedades
    /// não nulas de cada item da coleção.
    /// </summary>
    internal static void DisplayProperties<TSource>(this IEnumerable<TSource>? source)
    {
        if (source is null) return;

        foreach (TSource item in source)
            Console.WriteLine(GetPropertyValue(item));
    }

    /// <summary>
    /// Exibe um título ou cabeçalho 
    /// no console para a seção atual.
    /// </summary>
    internal static IEnumerable<TSource> DisplayHeader<TSource>(this IEnumerable<TSource> source, string? title)
    {
        Console.WriteLine($"\n***** {title ?? "No Category"} *****\n\n");
        return source;
    }

    /// <summary>
    /// Retorna o valor de todas
    /// as propriedades não nulas.
    /// </summary>
    private static StringBuilder GetPropertyValue<T>(T? item)
    {
        StringBuilder builder = new();

        PropertyInfo[] properties = typeof(T)
            .GetProperties(BindingFlags.Instance | BindingFlags.NonPublic);

        foreach (PropertyInfo property in properties)
        {
            if (item is null) continue;

            object? currentValue = property.GetValue(item);

            if (currentValue is string value && !string.IsNullOrWhiteSpace(value))
                builder.AppendLine($"{property.Name}: {value}");

            if (currentValue is decimal number)
                builder.AppendLine($"{property.Name}: {number:F2}");
        }

        return builder;
    }
}