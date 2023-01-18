using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PenaltyCalc.Models
{
    public class CountrySettings
    {
        [Key]
        public Guid id { get; set; }
        public string countryName { get; set; }
        public string weekendDays { get; set; }

        public string? otherHolidays { get; set; }
        public string currencyCode { get; set; }

        //Since it's mentioned in requirements that the currency code and the amount is country specific so I added this penaltyAmount column.
        [Precision(18, 2)]
        public decimal penaltyAmount { get; set; }
    }
}
