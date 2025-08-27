namespace Learning.Functional.Models;

/// <summary>
/// Representa os dados de entrada.
/// </summary>
internal sealed record StudentRequest
{
    /// <summary>
    /// Nome completo.
    /// </summary>
    internal required string FullName { get; init; }

    /// <summary>
    /// Coleção de disciplinas.
    /// </summary>
    internal required ICollection<GradeRequest> Grades { get; init; } = [];
}