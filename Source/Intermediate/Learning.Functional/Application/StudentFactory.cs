namespace Learning.Functional.Application;

/// <summary>
/// Fábrica estática que gera 
/// coleções de alunos para testes.
/// </summary>
internal static class StudentFactory
{
    /// <summary>
    /// Coleção estática de alunos.
    /// </summary>
    internal static HashSet<StudentRequest> Students =>
    [
        AverageStudent(),
        GoodStudent(),
        BadStudent(),
        MixedStudent(),
        VariedStudent(),
    ];

    /// <summary>
    /// Cria um aluno com notas medianas.
    /// </summary>
    private static StudentRequest AverageStudent()
        => new()
        {
            FullName = "Bob Jackson",
            Grades =
            [
                new() { Subject = "Math", Value = 7.0m },
                new() { Subject = "History", Value = 5.0m },
                new() { Subject = "Science", Value = 6.0m }
            ]
        };

    /// <summary>
    /// Cria um aluno com notas boas.
    /// </summary>
    private static StudentRequest GoodStudent()
        => new()
        {
            FullName = "Alice Smith",
            Grades =
            [
                new() { Subject = "Math", Value = 9.0m },
                new() { Subject = "History", Value = 8.5m },
                new() { Subject = "Science", Value = 9.5m }
            ]
        };

    /// <summary>
    /// Cria um aluno com notas baixas.
    /// </summary>
    private static StudentRequest BadStudent()
        => new()
        {
            FullName = "Charlie Brown",
            Grades =
            [
                new() { Subject = "Math", Value = 4.0m },
                new() { Subject = "History", Value = 3.5m },
                new() { Subject = "Science", Value = 5.0m }
            ]
        };

    /// <summary>
    /// Cria um aluno com notas mistas.
    /// </summary>
    private static StudentRequest MixedStudent()
        => new()
        {
            FullName = "Diana Prince",
            Grades =
            [
                new() { Subject = "Math", Value = 10.0m },
                new() { Subject = "History", Value = 6.0m },
                new() { Subject = "Science", Value = 8.0m },
                new() { Subject = "English", Value = 9.0m }
            ]
        };

    /// <summary>
    /// Cria um aluno com notas variadas.
    /// </summary>
    private static StudentRequest VariedStudent()
        => new()
        {
            FullName = "Edward Norton",
            Grades =
            [
                new() { Subject = "Math", Value = 6.5m },
                new() { Subject = "History", Value = 7.0m },
                new() { Subject = "Science", Value = 4.5m },
                new() { Subject = "English", Value = 8.0m },
                new() { Subject = "Arts", Value = 9.5m }
            ]
        };
}