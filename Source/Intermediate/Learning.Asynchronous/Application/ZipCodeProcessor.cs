namespace Learning.Asynchronous.Application;

/// <summary>
/// Representa o ponto 
/// de entrada da aplicação.
/// </summary>
internal static class ZipCodeProcessor
{
    /// <summary>
    /// Processa os CEPs fornecidos.
    /// </summary>
    /// <exception cref="FormatException"></exception>
    /// <exception cref="HttpRequestException"></exception>
    internal static async Task RunAsync(string[] zipCodes, CancellationToken token = default)
    {
        Benchmark.Start();
        ConcurrentBag<string> errors = [];
        using ViaCEPService service = new(new HttpClient());

        AddressResponse?[] responses = await Task
            .WhenAll(zipCodes
                .Select(async zipCode =>
                {
                    try { return await service.GetAddressAsync(zipCode, token); }
                    catch (Exception ex)
                    {
                        errors.Add($"Errors: {ex.Message}");
                        return null;
                    }
                }));

        errors.DisplayErrors();
        responses.DisplayProperties();

        Benchmark.Stop();
        Benchmark.DisplayElapsedTime();
    }
}