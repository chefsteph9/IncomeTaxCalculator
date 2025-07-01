using IncomeTaxCalculator.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncomeTaxCalculator.MAUI.Services.Interfaces
{
    public interface IIncomeTaxCalculatorApiService
    {
        Task<double> GetIncomeTax(long annualIncome, CancellationToken ct);

        Task<List<IncomeTaxBandDto>> GetIncomeTaxBands(CancellationToken ct);

        Task<int> PostIncomeTaxBands(List<IncomeTaxBandDto> incomeTaxBands, CancellationToken ct);
    }
}
