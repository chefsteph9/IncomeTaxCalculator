using IncomeTaxCalculator.MAUI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncomeTaxCalculator.MAUI.Services
{
    public class GetIncomeTaxServiceMock : IGetIncomeTaxService
    {
        public Task<double> GetIncomeTax(long annualIncome, CancellationToken ct)
        {
            if (annualIncome > 0)
            {
                return Task.FromResult(annualIncome / 10.0);
            }

            return Task.FromResult(0.0);
        }
    }
}
