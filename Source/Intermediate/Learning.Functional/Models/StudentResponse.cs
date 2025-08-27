namespace Learning.Functional.Models;

/// <summary>
/// Representa os dados de saída.
/// </summary>
internal sealed record StudentResponse
{
    /// <summary>
    /// Nome completo.
    /// </summary>
    internal string? FullName { get; init; }

    /// <summary>
    /// Nome da disciplina.
    /// </summary>
    internal string? Subject { get; init; }

    /// <summary>
    /// Média das notas.
    /// </summary>
    internal decimal? Average { get; init; }

    /// <summary>
    /// Maior nota.
    /// </summary>
    internal decimal? Highest { get; init; }

    /// <summary>
    /// Menor nota.
    /// </summary>
    internal decimal? Lowest { get; init; }
}