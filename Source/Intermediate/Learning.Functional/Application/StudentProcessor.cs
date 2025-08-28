namespace Learning.Functional.Application;

/// <summary>
/// Representa o ponto 
/// de entrada da aplicação.
/// </summary>
internal static class StudentProcessor
{
    /// <summary>
    /// Processa as análises acadêmicas.
    /// </summary>
    internal static void Run()
    {
        HashSet<StudentRequest> students = StudentFactory.Students;
        DisplayStudentAverages(students);
        DisplayStudentApproved(students);
        DisplayStudentDisapproved(students);
        DisplayBySubject(students);
        DisplayFailingSubject(students);
        DisplayStudentByMaxAverage(students);
        DisplayStudentByMinAverage(students);
        Console.ReadKey();
    }

    /// <summary>
    /// Exibe o valor da
    /// média de cada aluno.
    /// </summary>
    private static void DisplayStudentAverages(IEnumerable<StudentRequest> students) =>
        AnalysisService
            .CalculateAverage(students)
            .DisplayHeader("Averages")
            .DisplayProperties();

    /// <summary>
    /// Exibe os alunos aprovados.
    /// </summary>
    private static void DisplayStudentApproved(IEnumerable<StudentRequest> students) =>
        AnalysisService
            .GetApproved(students)
            .DisplayHeader("Approved")
            .DisplayProperties();

    /// <summary>
    /// Exibe os alunos reprovados.
    /// </summary>
    private static void DisplayStudentDisapproved(IEnumerable<StudentRequest> students) =>
        AnalysisService
            .GetDisapproved(students)
            .DisplayHeader("Disapproved")
            .DisplayProperties();

    /// <summary>
    /// Exibe a estastiticas por disciplinas.
    /// </summary>
    private static void DisplayBySubject(IEnumerable<StudentRequest> students) =>
        AnalysisService
            .GroupBySubject(students)
            .DisplayHeader("Grades by Subject")
            .DisplayProperties();

    /// <summary>
    /// Exibe as disciplinas abaixo da média.
    /// </summary>
    private static void DisplayFailingSubject(IEnumerable<StudentRequest> students) =>
        AnalysisService
            .GetFailingSubjects(students)
            .DisplayHeader("Failing Subject")
            .DisplayProperties();

    /// <summary>
    /// Exibe os alunos 
    /// com a maior média.
    /// </summary>
    private static void DisplayStudentByMaxAverage(IEnumerable<StudentRequest> students) =>
        AnalysisService
            .FindStudentByAverage(students)
            .AsEnumerable()
            .DisplayHeader("Average (max)")
            .DisplayProperties();

    /// <summary>
    /// Exibe os alunos 
    /// com a menor média.
    /// </summary>
    private static void DisplayStudentByMinAverage(IEnumerable<StudentRequest> students) =>
        AnalysisService
            .FindStudentByAverage(students, false)
            .AsEnumerable()
            .DisplayHeader("Average (min)")
            .DisplayProperties();
}