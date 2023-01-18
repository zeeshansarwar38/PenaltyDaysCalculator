using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PenaltyCalc.Data;

namespace PenaltyCalc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountrySettingsController : Controller
    {

        private readonly ICountrySettingsRepository _repository;

        public CountrySettingsController(ICountrySettingsRepository repository) => _repository = repository;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var countrySettings = await _repository.GetAllCountrySettings();
                return Ok(countrySettings);
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
