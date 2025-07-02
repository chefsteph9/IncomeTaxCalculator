using IncomeTaxCalculator.Domain.Models.DbEntities;

namespace IncomeTaxCalculator.Domain.Repositories.Interfaces
{
    public interface IIncomeTaxBandRepository
    {
        Task<IEnumerable<IncomeTaxBandEntity>> GetIncomeTaxBandsAsync(CancellationToken ct);

        Task<int> UpdateIncomeTaxBands(IEnumerable<IncomeTaxBandEntity> incomeTaxBands, CancellationToken ct);
    }
}
