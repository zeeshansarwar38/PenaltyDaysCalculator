using Microsoft.EntityFrameworkCore;
using PenaltyCalc.Models;

namespace PenaltyCalc.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<CountrySettings> CountrySettings { get; set; }
    }
}
