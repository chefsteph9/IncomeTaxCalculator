using IncomeTaxCalculator.Domain.Models.DbEntities;
using IncomeTaxCalculator.Domain.Repositories.DbContexts;
using IncomeTaxCalculator.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IncomeTaxCalculator.Domain.Repositories
{
    public class IncomeTaxBandSqlServerRepository : IIncomeTaxBandRepository
    {
        #region Members

        private ILogger<IncomeTaxBandSqlServerRepository> _logger;
        private IncomeTaxBandSqlServerDbContext _dbContext;

        #endregion

        #region Constructor

        public IncomeTaxBandSqlServerRepository(ILogger<IncomeTaxBandSqlServerRepository> logger, IncomeTaxBandSqlServerDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        #endregion

        #region Methods

        public async Task<IEnumerable<IncomeTaxBandEntity>> GetIncomeTaxBandsAsync(CancellationToken ct)
        {
            var incomeTaxBands = await _dbContext.IncomeTaxBands.ToListAsync(ct);

            return incomeTaxBands;
        }

        public async Task<int> UpdateIncomeTaxBands(IEnumerable<IncomeTaxBandEntity> incomeTaxBands, CancellationToken ct)
        {
            var deleteTask = _dbContext.IncomeTaxBands.ExecuteDeleteAsync(ct);

            var createTime = DateTime.Now;
            incomeTaxBands = incomeTaxBands.Select(i => { i.CreateTimestamp = createTime; return i; });

            await _dbContext.AddRangeAsync(incomeTaxBands, ct);

            var rowsUpdated = await _dbContext.SaveChangesAsync(ct);

            return rowsUpdated;
        }

        #endregion
    }
}
