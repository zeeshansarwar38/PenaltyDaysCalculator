using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PenaltyCalc.Data;
using PenaltyCalc.Models;
using System.Linq;

namespace PenaltyCalc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PenaltyCalculatorController : Controller
    {
        private readonly ICountrySettingsRepository _repository;

        public PenaltyCalculatorController(ICountrySettingsRepository repository) => _repository = repository;

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PenaltyCalculate request)
        {
            var penaltyDates = new List<DateTime?>();
            decimal penaltyAmount = 0;
            try
            {
                var settingId = request.selectedCountry;
                var countrySettings = await _repository.GetCountrySettingsById(settingId);
                if (countrySettings != null)
                {
                    var startDate = request.checkoutDate;
                    var endDate = request.returnDate;
                    var weekendDays = countrySettings.weekendDays.Split(',');
                    var otherHolidays = countrySettings.otherHolidays?.Split(',').Select(holiday => Convert.ToDateTime(holiday));
                    var businessDay = 0;
                    for (var midstDate = startDate; midstDate <= endDate; midstDate = midstDate.AddDays(1)) //Since the checkout and return dates are inclusive so we will include them in iterations
                    {
                        if (!weekendDays.Contains(midstDate.DayOfWeek.ToString()) 
                            && (otherHolidays == null || otherHolidays.Count() == 0 
                            || (otherHolidays != null && otherHolidays.Count() > 0
                            && !otherHolidays.Contains(midstDate)))) // condition to check if it's business day for the country
                        {
                            businessDay++;
                            if (businessDay > 10) // condition to check if 10 business days have been passed then it's penalty day
                            {
                                penaltyDates.Add(midstDate);
                            }
                        }
                    }
                    penaltyAmount = penaltyDates.Count * countrySettings.penaltyAmount;
                }
                else
                {
                    return BadRequest(new { message = "Not meta-data found for the selected country. Unable to process request at the moment, please try later." });
                }
                return Ok(new { penalty = penaltyAmount, days = penaltyDates });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Something went wrong" });
            }


        }

    }
}
