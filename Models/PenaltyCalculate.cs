namespace PenaltyCalc.Models
{
    public class PenaltyCalculate
    {
        public DateTime checkoutDate { get; set; }
        public DateTime returnDate { get; set; }
        public Guid selectedCountry { get; set; }
    }
}
