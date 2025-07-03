using IncomeTaxCalculator.Domain.Models.DbEntities;
using IncomeTaxCalculator.Domain.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncomeTaxCalculator.Domain.Repositories
{
    public class IncomeTaxBandInMemoryRepository : IIncomeTaxBandRepository
    {
        #region Members

        private List<IncomeTaxBandEntity> _entities = new();
        private ILogger<IncomeTaxBandInMemoryRepository> _logger;

        #endregion

        #region Constructors

        public IncomeTaxBandInMemoryRepository(ILogger<IncomeTaxBandInMemoryRepository> logger)
        {
            _logger = logger;

            DateTime now = DateTime.Now;
            _entities.Add(new IncomeTaxBandEntity() { ID = 1, LowerBound = 0, TaxRate = 0, CreateTimestamp = now });
            _entities.Add(new IncomeTaxBandEntity() { ID = 2, LowerBound = 5000, TaxRate = 20, CreateTimestamp = now });
            _entities.Add(new IncomeTaxBandEntity() { ID = 3, LowerBound = 20000, TaxRate = 40, CreateTimestamp = now });
        }

        #endregion

        #region Methods

        public Task<IEnumerable<IncomeTaxBandEntity>> GetIncomeTaxBandsAsync(CancellationToken ct)
        {
            return Task.FromResult<IEnumerable<IncomeTaxBandEntity>>(_entities);
        }

        public Task<int> UpdateIncomeTaxBands(IEnumerable<IncomeTaxBandEntity> incomeTaxBands, CancellationToken ct)
        {
            _entities.Clear();
            _entities.AddRange(incomeTaxBands);
            return Task.FromResult(_entities.Count);
        }

        #endregion
    }
}
