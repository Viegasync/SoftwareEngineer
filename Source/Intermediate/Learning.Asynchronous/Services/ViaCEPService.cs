namespace Learning.Asynchronous.Services;

/// <summary>
/// Serviço que fornece acesso à API.
/// </summary>
internal sealed class ViaCEPService(HttpClient http) : IDisposable
{
    /// <summary>
    /// Realiza uma chamada assíncrona à API e, 
    /// conforme o CEP informado, obtém os dados.
    /// </summary>
    /// <exception cref="FormatException"></exception>
    /// <exception cref="HttpRequestException"></exception>
    internal async Task<AddressResponse?> GetAddressAsync(string zipCode, CancellationToken token = default)
    {
        ValidateZipCode(zipCode);
        HttpResponseMessage response = await http
            .GetAsync($"https://viacep.com.br/ws/{zipCode}/json/", token);

        AddressResponse? address = await response.Content
            .ReadFromJsonAsync<AddressResponse>(token);

        return !response.IsSuccessStatusCode || address?.Error == "true"
            ? throw new HttpRequestException("Address not found.")
            : address;
    }

    /// <summary>
    /// Valida e formata um CEP, 
    /// removendo caracteres não numéricos.
    /// </summary>
    /// <exception cref="FormatException"></exception>
    private static string ValidateZipCode(string zipCode)
    {
        string zipCodeFormatted = new([.. zipCode
            .Where(char.IsDigit)]);

        return zipCodeFormatted.Length != 8
            ? throw new FormatException("ZipCode is invalid.")
            : zipCodeFormatted;
    }

    /// <summary>
    /// Libera os recursos utilizados
    /// pelo <see cref="HttpClient"/> interno.
    /// </summary>
    public void Dispose() => http.Dispose();
}