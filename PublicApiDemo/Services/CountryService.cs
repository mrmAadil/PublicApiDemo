using System.Text.Json;
using PublicApiDemo.Models;
using PublicApiDemo.Repositories;

namespace PublicApiDemo.Services;

public class CountryService : ICountryService
{
    private readonly ICountryRespository _countryRepository;
    private readonly HttpClient _httpClient;
    private readonly ILogger<CountryService> _logger;

    private const string ExternalApiUrl = "https://restcountries.com/v3.1/all?fields=name,capital,region,subregion,population,area";

    public CountryService(ICountryRespository countryRepository, HttpClient httpClient, ILogger<CountryService> logger)
    {
        _countryRepository = countryRepository;
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<IEnumerable<Country>> GetAllCountriesAsync()
    {
        if(await _countryRepository.AnyExistAsync())
        {
            _logger.LogInformation("Fetching countries from repository.");
            return await _countryRepository.GetAllCountriesAsync();
        }

        _logger.LogInformation("No countries found in repository. Fetching from external API.");
        var countries = await FetchCountriesFromExternalApiAsync();

        foreach (var country in countries)
        {
            await _countryRepository.AddCountryAsync(country);
        }
        return countries;
    }

    public async Task<Country?> GetCountryByIdAsync(int id)
    {
        var country = await _countryRepository.GetCountryByIdAsync(id);
        if(country != null)
        {
            _logger.LogInformation($"Country with ID {id} found in repository.");
            return country;
        }

        if(!await _countryRepository.AnyExistAsync())
        {
            await GetAllCountriesAsync();
            return await _countryRepository.GetCountryByIdAsync(id);
        }

        return null;
    }

    private async Task<List<Country>> FetchCountriesFromExternalApiAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync(ExternalApiUrl);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var jsonParsed = JsonDocument.Parse(json);
            var countries = new List<Country>();

            foreach (var element in jsonParsed.RootElement.EnumerateArray())
            {
                var country = new Country
                {
                    Name = element.GetProperty("name").GetProperty("common").GetString() ?? string.Empty,
                    Capital = element.TryGetProperty("capital", out var capital) && capital.GetArrayLength() > 0
                        ? capital[0].GetString() ?? string.Empty
                        : string.Empty,
                    Region = element.TryGetProperty("region", out var region) ? region.GetString() ?? string.Empty : string.Empty,
                    Subregion = element.TryGetProperty("subregion", out var subregion) ? subregion.GetString() ?? string.Empty : string.Empty,
                    Population = element.TryGetProperty("population", out var population) ? population.GetInt64() : 0,
                    Area = element.TryGetProperty("area", out var area) ? area.GetDouble() : 0
                };

                countries.Add(country);
            }
            return countries;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching countries from external API.");
            throw;
        }
    }
}
