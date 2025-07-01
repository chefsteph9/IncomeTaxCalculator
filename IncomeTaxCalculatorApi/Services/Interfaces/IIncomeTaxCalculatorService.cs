using IncomeTaxCalculator.Api.Models.Domain;

namespace IncomeTaxCalculator.Api.Services.Interfaces
{
    public interface IIncomeTaxCalculatorService
    {
        Task<double> GetIncomeTaxAsync(long annualIncome, CancellationToken ct);

        Task<int> UpdateIncomeTaxBandsAsync(List<IncomeTaxBandDomainModel> incomeTaxBands, CancellationToken ct);

        Task<List<IncomeTaxBandDomainModel>> GetIncomeTaxBandsAsync(CancellationToken ct);
    }
}
