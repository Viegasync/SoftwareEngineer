namespace Learning.Functional.Models;

/// <summary>
/// Representa os dados de entrada.
/// </summary>
internal sealed record GradeRequest
{
    /// <summary>
    /// Nome da disciplina.
    /// </summary>
    internal required string Subject { get; init; }

    /// <summary>
    /// Valor da média.
    /// </summary>
    internal required decimal Value { get; init; }
}