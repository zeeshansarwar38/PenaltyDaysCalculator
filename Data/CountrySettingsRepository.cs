using Microsoft.EntityFrameworkCore;
using PenaltyCalc.Models;
using System.Linq.Expressions;

namespace PenaltyCalc.Data
{
    public class CountrySettingsRepository: ICountrySettingsRepository
    {
        private readonly ApplicationDbContext _context;

        public CountrySettingsRepository(ApplicationDbContext context) => _context = context;
        
        public async Task<CountrySettings?> GetCountrySettingsById(Guid id)
        {
            return await _context.CountrySettings.FirstOrDefaultAsync(cSetting => cSetting.id == id);
        }

        public async Task<List<CountrySettings>> GetAllCountrySettings()
        {
            return await _context.CountrySettings.ToListAsync();
        }
    }
}