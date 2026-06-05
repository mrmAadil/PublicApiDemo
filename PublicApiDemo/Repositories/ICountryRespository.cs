using PublicApiDemo.Models;
namespace PublicApiDemo.Repositories;

public interface ICountryRespository
{
    Task<IEnumerable<Country>> GetAllCountriesAsync();
    Task<Country> GetCountryByIdAsync(int id);
    Task<int> AddCountryAsync(Country country);
    Task<bool> AnyExistAsync(int id);
}
