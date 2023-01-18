using PenaltyCalc.Models;
using System.Linq.Expressions;

namespace PenaltyCalc.Data
{
    public interface ICountrySettingsRepository
    {
        Task<CountrySettings?> GetCountrySettingsById(Guid id);
        Task<List<CountrySettings>> GetAllCountrySettings();
    }
}
