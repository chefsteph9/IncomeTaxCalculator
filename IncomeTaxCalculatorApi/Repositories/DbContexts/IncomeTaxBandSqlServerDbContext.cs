using IncomeTaxCalculator.Api.Models.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace IncomeTaxCalculator.Api.Repositories.DbContexts
{
    public class IncomeTaxBandSqlServerDbContext : DbContext
    {
        public IncomeTaxBandSqlServerDbContext(DbContextOptions<IncomeTaxBandSqlServerDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<IncomeTaxBandEntity> IncomeTaxBands { get; set; }
    }
}
