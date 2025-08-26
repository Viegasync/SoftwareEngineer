namespace Learning.Asynchronous.Application;

/// <summary>
/// Representa o ponto de entrada da aplicação.
/// </summary>
internal sealed class ZipCodeProcessor
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
            .WhenAll(zipCodes.Select(async zipCode =>
            {
                try { return await service.GetAddressAsync(zipCode, token); }
                catch (Exception ex)
                {
                    errors.Add($"Error: {ex.Message}");
                    return null;
                }
            }));

        AddressResponse?[] addresses = [.. responses
            .Where(address => address is not null)];

        foreach (AddressResponse? address in addresses)
            DisplayAddressData(address, Console.WriteLine);

        foreach (string error in errors)
            Console.WriteLine(error);

        Benchmark.Stop();
        Benchmark.DisplayElapsedTime();
    }

    /// <summary>
    /// Exibe as propriedades não nulas do endereço.
    /// </summary>
    private static void DisplayAddressData(AddressResponse? address, Action<string> console)
    {
        PropertyInfo[] properties = typeof(AddressResponse)
            .GetProperties();

        foreach (PropertyInfo property in properties)
        {
            object? currentValue = property.GetValue(address);
            if (currentValue is string value && !string.IsNullOrWhiteSpace(value))
                console($"{property.Name}: {currentValue}");
        }

        Console.WriteLine();
    }
}