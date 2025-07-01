using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncomeTaxCalculator.MAUI.Configuration
{
    internal class IncomeTaxCalculatorApiConfiguration
    {
        public string BaseUrl { get; set; }

        public string GetIncomeTaxEndpoint { get; set; }

        public string GetIncomeTaxBandsEndpoint { get; set; }

        public string PostIncomeTaxBandsEndpoint { get; set; }
    }
}
