using IncomeTaxCalculator.Api.Models.DbEntities;
using IncomeTaxCalculator.Api.Models.Domain;
using IncomeTaxCalculator.Api.Repositories.DbContexts;
using IncomeTaxCalculator.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IncomeTaxCalculator.Api.Repositories
{
    public class IncomeTaxBandRepositorySqlServer : IIncomeTaxBandRepository
    {
        #region Members

        private ILogger<IncomeTaxBandRepositorySqlServer> _logger;
        private IncomeTaxBandSqlServerDbContext _dbContext;

        #endregion

        #region Constructor

        public IncomeTaxBandRepositorySqlServer(ILogger<IncomeTaxBandRepositorySqlServer> logger, IncomeTaxBandSqlServerDbContext dbContext)
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

