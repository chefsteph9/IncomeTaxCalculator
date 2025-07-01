using IncomeTaxCalculator.Api.Models.DbEntities;

namespace IncomeTaxCalculator.Api.Repositories.Interfaces
{
    public interface IIncomeTaxBandRepository
    {
        Task<IEnumerable<IncomeTaxBandEntity>> GetIncomeTaxBandsAsync(CancellationToken ct);

        Task<int> UpdateIncomeTaxBands(IEnumerable<IncomeTaxBandEntity> incomeTaxBands, CancellationToken ct);
    }
}
