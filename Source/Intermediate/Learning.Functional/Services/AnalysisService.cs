namespace Learning.Functional.Services;

/// <summary>
/// Serviço estático responsável 
/// por operações funcionais de análise.
/// </summary>
internal static class AnalysisService
{
    /// <summary>
    /// Calcula a média das 
    /// notas de cada aluno.
    /// </summary>
    internal static IEnumerable<StudentResponse> CalculateAverage(IEnumerable<StudentRequest> students)
        => students.Select(student => new StudentResponse
        {
            FullName = student.FullName,
            Average = student.Grades.Average(grade => grade.Value)
        });

    //// <summary>
    /// Retorna os alunos 
    /// aprovados (média >= 7).
    /// </summary>
    internal static IEnumerable<StudentRequest> GetApproved(IEnumerable<StudentRequest> students)
        => students.Where(s => s.Grades.Average(g => g.Value) >= 7);

    /// <summary>
    /// Retorna os alunos 
    /// reprovados (média < 7).
    /// </summary>
    internal static IEnumerable<StudentRequest> GetDisapproved(IEnumerable<StudentRequest> students)
        => students.Where(s => s.Grades.Average(g => g.Value) < 7);

    /// <summary>
    /// Retorna o aluno com a 
    /// maior ou menor média.
    /// </summary>
    internal static StudentRequest? FindStudentByAverage(IEnumerable<StudentRequest> students, bool max = true)
        => max
            ? students.MaxBy(student => student.Grades.Average(grade => grade.Value))
            : students.MinBy(student => student.Grades.Average(grade => grade.Value));

    /// <summary>
    /// Agrupa todas as notas por disciplina 
    /// e retorna estatísticas de cada grupo.
    /// </summary>
    internal static IEnumerable<StudentResponse> GroupBySubject(IEnumerable<StudentRequest> students)
        => students
            .SelectMany(student => student.Grades)
            .GroupBy(grade => grade.Subject)
            .Select(value => new StudentResponse
            {
                Subject = value.Key,
                Average = value.Average(g => g.Value),
                Highest = value.Max(g => g.Value),
                Lowest = value.Min(g => g.Value)
            });
}