using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncomeTaxCalculator.MAUI.Services.Interfaces
{
    public interface IGetIncomeTaxService
    {
        Task<double> GetIncomeTax(long annualIncome, CancellationToken ct);
    }
}
