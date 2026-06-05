using Microsoft.Data.SqlClient;
using PublicApiDemo.Models;

namespace PublicApiDemo.Repositories;

public class CountryRepository : ICountryRespository
{
    private readonly string _connectionString;

    public CountryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    }
    public async Task<int> AddCountryAsync(Country country)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        using var command = new SqlCommand(
            @"INSERT INTO Countries (Name, Capital, Region, Subregion, Population, Area)
      OUTPUT INSERTED.Id
      VALUES (@Name, @Capital, @Region, @Subregion, @Population, @Area)",
            connection);

        command.Parameters.AddWithValue("@Name", country.Name);
        command.Parameters.AddWithValue("@Capital", country.Capital);
        command.Parameters.AddWithValue("@Region", country.Region);
        command.Parameters.AddWithValue("@Subregion", country.Subregion);
        command.Parameters.AddWithValue("@Population", country.Population);
        command.Parameters.AddWithValue("@Area", country.Area);

        var id = (int)await command.ExecuteScalarAsync();
        return id;
    }

    public async Task<bool> AnyExistAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        using var command = new SqlCommand("SELECT COUNT(1) FROM Countries", connection);
        var count = (int)await command.ExecuteScalarAsync();
        return count > 0;
    }

    public async Task<IEnumerable<Country>> GetAllCountriesAsync()
    {
        var countries = new List<Country>();

        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        using var command = new SqlCommand(
            "SELECT Id, Name, Capital, Region, Subregion, Population, Area, CurrencyCode, CurrencyName, Alpha3Code FROM Countries",
            connection);

        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            countries.Add(MapCountry(reader));
        }

        return countries;
    }

    public async Task<Country> GetCountryByIdAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        using var command = new SqlCommand(
            "SELECT Id, Name, Capital, Region, Subregion, Population, Area, CurrencyCode, CurrencyName, Alpha3Code FROM Countries WHERE Id = @Id",
            connection);

        command.Parameters.AddWithValue("@Id", id);

        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return MapCountry(reader);
        }

        return null;
    }

    private static Country MapCountry(SqlDataReader reader)
    {
        return new Country
        {
            Id = reader.GetInt32(0),
            Name = reader.GetString(1),
            Capital = reader.GetString(2),
            Region = reader.GetString(3),
            Subregion = reader.GetString(4),
            Population = reader.GetInt64(5),
            Area = reader.GetDouble(6)
        };
    }
}
