using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncomeTaxCalculator.Models.DTOs
{
    public class IncomeTaxBandDto
    {
        public long LowerBound { get; set; }

        public int TaxRate { get; set; }
    }
}
