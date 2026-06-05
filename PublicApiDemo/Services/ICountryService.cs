using PublicApiDemo.Models;

namespace PublicApiDemo.Services
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> GetAllCountriesAsync();
        Task<Country?> GetCountryByIdAsync(int id);
    }
}
