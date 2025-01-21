using Microsoft.AspNetCore.Mvc;
using CountriesApi.Application.Services;
using CountriesApi.Application.Interfaces;
using CountriesApi.Application.Common;
using CountriesApi.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CountriesApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountriesController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet("GetAllCountries")]
        public async Task<IActionResult> GetCountries()
        {
            var result = await _countryService.GetAllCountries();
            if (result != null)
            {
                return result?.Value?.Count > 0 ? Ok(result) : NotFound(result);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");

        }

        [HttpPost("UpdateAllCountries")]
        public async Task<IActionResult> UpdateAllCountries()
        {
            var updated = await _countryService.UpdateCountries();
            if (updated != null && updated.Value)
            {
                return Ok(updated);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, updated?.ErrorMessage ?? "Internal Server Error, please contact the support.");
        }
    }
}