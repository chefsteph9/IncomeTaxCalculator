using IncomeTaxCalculator.Domain.Models.DbEntities;
using IncomeTaxCalculator.Domain.Models.DomainModels;
using IncomeTaxCalculator.Domain.Repositories.Interfaces;
using IncomeTaxCalculator.Domain.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace IncomeTaxCalculator.Domain.Services
{
    public class UnitedKingdomIncomeTaxCalculatorService : IIncomeTaxCalculatorService
    {
        #region Members

        private ILogger<UnitedKingdomIncomeTaxCalculatorService> _logger;
        private IIncomeTaxBandRepository _taxBandRepository;

        #endregion

        #region Constructor

        public UnitedKingdomIncomeTaxCalculatorService(ILogger<UnitedKingdomIncomeTaxCalculatorService> logger, IIncomeTaxBandRepository taxBandRepository)
        {
            _logger = logger;
            _taxBandRepository = taxBandRepository;
        }

        #endregion

        #region Methods

        public async Task<double> GetIncomeTaxAsync(long annualIncome, CancellationToken ct)
        {
            var taxBands = await GetIncomeTaxBandsAsync(ct);

            double totalTaxes = 0;
            long untaxedAnnualIncome = annualIncome;

            taxBands = ValidateAndSortIncomeTaxBands(taxBands);
            taxBands.Reverse();

            foreach (var taxBand in taxBands)
            {
                if (untaxedAnnualIncome > taxBand.LowerBound)
                {
                    totalTaxes += ((untaxedAnnualIncome - taxBand.LowerBound) * (taxBand.TaxRate)) / 100.0;
                    untaxedAnnualIncome = taxBand.LowerBound;
                }
            }

            return totalTaxes;
        }

        public async Task<List<IncomeTaxBandDomainModel>> GetIncomeTaxBandsAsync(CancellationToken ct)
        {
            var taxBandEntities = await _taxBandRepository.GetIncomeTaxBandsAsync(ct);

            var taxBandModels = taxBandEntities.ToList().ConvertAll(x => (IncomeTaxBandDomainModel)x);

            return taxBandModels;
        }

        public async Task<int> UpdateIncomeTaxBandsAsync(List<IncomeTaxBandDomainModel> incomeTaxBandModels, CancellationToken ct)
        {
            incomeTaxBandModels = ValidateAndSortIncomeTaxBands(incomeTaxBandModels);

            var incomeTaxBandEntities = incomeTaxBandModels.ConvertAll(x => (IncomeTaxBandEntity)x);

            return await _taxBandRepository.UpdateIncomeTaxBands(incomeTaxBandEntities, ct);
        }

        private List<IncomeTaxBandDomainModel> ValidateAndSortIncomeTaxBands(List<IncomeTaxBandDomainModel> incomeTaxBandModels)
        {
            incomeTaxBandModels = incomeTaxBandModels ?? new();

            incomeTaxBandModels = incomeTaxBandModels.DistinctBy(x => x.LowerBound).ToList();
            incomeTaxBandModels.Sort();

            return incomeTaxBandModels;
        }

        #endregion
    }
}
