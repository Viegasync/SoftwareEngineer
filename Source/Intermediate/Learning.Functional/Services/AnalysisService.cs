namespace Learning.Functional.Services;

/// <summary>
/// Serviço estático responsável 
/// por operações funcionais de análise.
/// </summary>
internal static class AnalysisService
{
    /// <summary>
    /// O valor da média
    /// </summary>
    private static readonly decimal s_passingGrade = 7.0m;

    /// <summary>
    /// Calcula a média das 
    /// notas de cada aluno.
    /// </summary>
    internal static IEnumerable<StudentResponse> CalculateAverage(IEnumerable<StudentRequest> students) =>
        students.Select(student => new StudentResponse
        {
            FullName = student.FullName,
            Average = CalculateAverage(student)
        });

    /// <summary>
    /// Retorna os alunos 
    /// aprovados (média >= 7).
    /// </summary>
    internal static IEnumerable<StudentRequest> GetApproved(IEnumerable<StudentRequest> students) =>
        students.Where(student => CalculateAverage(student) >= s_passingGrade);

    /// <summary>
    /// Retorna os alunos 
    /// reprovados (média < 7).
    /// </summary>
    internal static IEnumerable<StudentRequest> GetDisapproved(IEnumerable<StudentRequest> students) =>
        students.Where(student => CalculateAverage(student) < s_passingGrade);

    /// <summary>
    /// Retorna o aluno com a 
    /// maior ou menor média.
    /// </summary>
    internal static StudentRequest? FindStudentByAverage(IEnumerable<StudentRequest> students, bool max = true)
        => max ? students.MaxBy(CalculateAverage) : students.MinBy(CalculateAverage);

    /// <summary>
    /// Agrupa todas as notas por disciplina 
    /// e retorna estatísticas de cada grupo.
    /// </summary>
    internal static IEnumerable<StudentResponse> GroupBySubject(IEnumerable<StudentRequest> students) =>
        students
            .SelectMany(student => student.Grades)
            .GroupBy(grade => grade.Subject)
            .Select(value => new StudentResponse
            {
                Subject = value.Key,
                Average = value.Average(grade => grade.Value),
                Highest = value.Max(grade => grade.Value),
                Lowest = value.Min(grade => grade.Value)
            });

    /// <summary>
    /// Retorna as disciplinas em que 
    /// o aluno está abaixo da média.
    /// </summary>
    internal static IEnumerable<StudentResponse> GetFailingSubjects(IEnumerable<StudentRequest> students) =>
        students
            .SelectMany(student => student.Grades
            .Where(grade => grade.Value < s_passingGrade)
            .Select(grade => new StudentResponse
            {
                FullName = student.FullName,
                Subject = grade.Subject,
                Average = grade.Value
            }));

    /// <summary>
    /// Calcula a média das notas do aluno.
    /// </summary>
    private static decimal CalculateAverage(StudentRequest student) =>
        student.Grades.Count != 0 ? student.Grades.Average(g => g.Value) : 0m;
}