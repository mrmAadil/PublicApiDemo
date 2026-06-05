using Microsoft.AspNetCore.Mvc;
using PublicApiDemo.Services;

namespace PublicApiDemo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountriesController : ControllerBase
{
    private readonly ILogger<CountriesController> _logger;
    private readonly ICountryService _countryService;

    public CountriesController(ILogger<CountriesController> logger, ICountryService countryService)
    {
        _logger = logger;
        _countryService = countryService;
    }

    /// <summary>
    /// Gets all countries.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAllCountriesAsync()
    {
        try
        {
            var countries = await _countryService.GetAllCountriesAsync();
            return Ok(countries);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting all countries.");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    /// <summary>
    /// Gets a country by id.
    /// </summary>
    /// <param name="id">CountryId</param>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCountryById(int id)
    {
        try
        {
            var country = await _countryService.GetCountryByIdAsync(id);
            if (country == null)
            {
                return NotFound(new { error = $"Country with ID {id} not found" });
            }
            return Ok(country);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while getting country with ID {id}.");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

}
