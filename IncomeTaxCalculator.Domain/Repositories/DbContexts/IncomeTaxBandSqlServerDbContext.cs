using IncomeTaxCalculator.Domain.Models.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace IncomeTaxCalculator.Domain.Repositories.DbContexts
{
    public class IncomeTaxBandSqlServerDbContext : DbContext
    {
        public IncomeTaxBandSqlServerDbContext(DbContextOptions<IncomeTaxBandSqlServerDbContext> options)
            : base(options) { }

        public DbSet<IncomeTaxBandEntity> IncomeTaxBands { get; set; }
    }
}
