using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PenaltyCalc.Tests.Models
{
    internal class PenaltyResponse
    {
        public List<DateTime> days { get; set; }
        public decimal penalty { get; set; }
    }
}
