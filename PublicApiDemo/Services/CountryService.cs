using PublicApiDemo.Models;

namespace PublicApiDemo.Services;

public class CountryService : ICountryService
{
    public Task<IEnumerable<Country>> GetAllCountriesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Country?> GetCountryByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}
