namespace Learning.Asynchronous.Application;

/// <summary>
/// Classe estática que representa
/// o ponto de entrada da aplicação.
/// </summary>
internal static class ZipCodeProcessor
{
    /// <summary>
    /// Processa os CEPs fornecidos.
    /// </summary>
    /// <exception cref="FormatException"></exception>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="HttpRequestException"></exception>
    internal static async Task RunAsync(string[] zipCodes, CancellationToken token = default)
    {
        Benchmark.Start();
        ConcurrentBag<string> errors = [];
        using ViaCEPService service = new(new HttpClient());

        AddressResponse[] responses = await Task
            .WhenAll(zipCodes
                .Where(zipCode => !string.IsNullOrWhiteSpace(zipCode))
                .Select(async zipCode =>
                {
                    try { return await service.GetAddressAsync(zipCode, token); }
                    catch (Exception ex)
                    {
                        errors.Add($"Errors: {ex.Message}");
                        return null;
                    }
                }));

        responses
            .Where(address => address is not null)
            .DisplayProperties();

        errors.DisplayErrors();

        Benchmark.Stop();
        Benchmark.DisplayElapsedTime();
    }
}