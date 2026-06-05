using PublicApiDemo.Models;

namespace PublicApiDemo.Repositories;

public class CountryRepository : ICountryRespository
{
    public Task<int> AddCountryAsync(Country country)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AnyExistAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Country>> GetAllCountriesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Country> GetCountryByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}
