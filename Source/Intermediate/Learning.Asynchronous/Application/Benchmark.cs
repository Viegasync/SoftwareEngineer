namespace Learning.Asynchronous.Application;

/// <summary>
/// Permite medir o tempo de execução.
/// </summary>
internal static class Benchmark
{
    /// <summary>
    /// Tempo decorrido.
    /// </summary>
    private static Stopwatch? s_stopwatch;

    /// <summary>
    /// Inicia o cronômetro interno.
    /// </summary>
    internal static void Start() => s_stopwatch = Stopwatch.StartNew();

    /// <summary>
    /// Para o cronômetro interno.
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    internal static void Stop() => s_stopwatch?.Stop();

    /// <summary>
    /// Exibe o tempo decorrido em segundos 
    /// desde que o cronômetro foi iniciado.
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    internal static void DisplayElapsedTime()
    {
        Console.Write($"\nTime: {s_stopwatch?.Elapsed.TotalSeconds:F2}");
        Console.ReadKey();
    }
}