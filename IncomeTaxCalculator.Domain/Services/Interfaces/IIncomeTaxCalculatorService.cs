using IncomeTaxCalculator.Domain.Models.DomainModels;

namespace IncomeTaxCalculator.Domain.Services.Interfaces
{
    public interface IIncomeTaxCalculatorService
    {
        Task<double> GetIncomeTaxAsync(long annualIncome, CancellationToken ct);

        Task<int> UpdateIncomeTaxBandsAsync(List<IncomeTaxBandDomainModel> incomeTaxBands, CancellationToken ct);

        Task<List<IncomeTaxBandDomainModel>> GetIncomeTaxBandsAsync(CancellationToken ct);
    }
}
