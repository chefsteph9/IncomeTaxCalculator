using System.ComponentModel.DataAnnotations.Schema;

namespace IncomeTaxCalculator.Api.Models.DbEntities
{
    [Table("IncomeTaxBand")]
    public class IncomeTaxBandEntity
    {
        public int ID { get; set; }

        public long LowerBound { get; set; }

        public int TaxRate { get; set; }

        public DateTime CreateTimestamp { get; set; }
    }
}
