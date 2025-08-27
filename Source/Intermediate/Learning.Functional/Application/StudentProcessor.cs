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
        DisplayStudentByMaxAverage(students);
        DisplayStudentByMinAverage(students);
        Console.ReadKey();
    }

    /// <summary>
    /// Exibe o valor da
    /// média de cada aluno.
    /// </summary>
    private static void DisplayStudentAverages(IEnumerable<StudentRequest> students)
    {
        Console.WriteLine("***** Averages *****\n");

        IEnumerable<StudentResponse> averages = AnalysisService
            .CalculateAverage(students);

        foreach (StudentResponse student in averages)
            DisplayPropertyValue(student);
    }

    /// <summary>
    /// Exibe os alunos aprovados.
    /// </summary>
    private static void DisplayStudentApproved(IEnumerable<StudentRequest> students)
    {
        Console.WriteLine("\n***** Approved *****\n");

        IEnumerable<StudentRequest> approved = AnalysisService
            .GetApproved(students, 7.0m);

        foreach (StudentRequest student in approved)
            DisplayPropertyValue(student);
    }

    /// <summary>
    /// Exibe os alunos reprovados.
    /// </summary>
    private static void DisplayStudentDisapproved(IEnumerable<StudentRequest> students)
    {
        Console.WriteLine("\n***** Disapproved *****\n");

        IEnumerable<StudentRequest> approved = AnalysisService
            .GetDisapproved(students, 7.0m);

        foreach (StudentRequest student in approved)
            DisplayPropertyValue(student);
    }

    /// <summary>
    /// Exibe a estastiticas por disciplinas.
    /// </summary>
    private static void DisplayBySubject(IEnumerable<StudentRequest> students)
    {
        Console.WriteLine("\n***** By Subject *****\n");

        IEnumerable<StudentResponse> subjects = AnalysisService
            .GroupBySubject(students);

        foreach (StudentResponse student in subjects)
            DisplayPropertyValue(student);
    }

    /// <summary>
    /// Exibe os alunos 
    /// com a maior média.
    /// </summary>
    private static void DisplayStudentByMaxAverage(IEnumerable<StudentRequest> students)
    {
        Console.WriteLine("\n***** Average (Max) *****\n");

        StudentRequest? average = AnalysisService
            .FindStudentByAverage(students);

        DisplayPropertyValue(average);
    }

    /// <summary>
    /// Exibe os alunos 
    /// com a menor média.
    /// </summary>
    private static void DisplayStudentByMinAverage(IEnumerable<StudentRequest> students)
    {
        Console.WriteLine("\n***** Average (Min) *****\n");

        StudentRequest? average = AnalysisService
            .FindStudentByAverage(students, false);

        DisplayPropertyValue(average);
    }

    /// <summary>
    /// Exibe o valor das 
    /// propriedades não nulas.
    /// </summary>
    private static void DisplayPropertyValue<TSource>(TSource source)
    {
        PropertyInfo[] properties = typeof(TSource)
            .GetProperties(BindingFlags.Instance | BindingFlags.NonPublic);

        foreach (PropertyInfo property in properties)
        {
            object? currentValue = property.GetValue(source);

            if (currentValue is string value && !string.IsNullOrWhiteSpace(value))
                Console.WriteLine($"{property.Name}: {value}");

            if (currentValue is decimal number)
                Console.WriteLine($"{property.Name}: {number:F2}");
        }

        Console.WriteLine();
    }
}