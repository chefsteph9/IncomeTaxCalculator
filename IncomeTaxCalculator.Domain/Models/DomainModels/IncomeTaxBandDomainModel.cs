using IncomeTaxCalculator.Domain.Models.DbEntities;
using IncomeTaxCalculator.Models.DTOs;

namespace IncomeTaxCalculator.Domain.Models.DomainModels
{
    public class IncomeTaxBandDomainModel : IComparable<IncomeTaxBandDomainModel>
    {
        #region Properties

        public long LowerBound { get; set; }

        public int TaxRate { get; set; }

        #endregion

        #region Methods

        public int CompareTo(IncomeTaxBandDomainModel? obj)
        {
            if (obj == null)
                return -1;

            return this.LowerBound < obj.LowerBound ? -1 : this.LowerBound == obj.LowerBound ? 0 : 1;
        }

        #endregion

        #region Converters

        // DTO
        public static implicit operator IncomeTaxBandDto(IncomeTaxBandDomainModel m) => new IncomeTaxBandDto { LowerBound = m.LowerBound, TaxRate = m.TaxRate };
        public static implicit operator IncomeTaxBandDomainModel(IncomeTaxBandDto d) => new IncomeTaxBandDomainModel { LowerBound = d.LowerBound, TaxRate = d.TaxRate };

        // DB Entity
        public static explicit operator IncomeTaxBandEntity(IncomeTaxBandDomainModel m) => new IncomeTaxBandEntity { LowerBound = m.LowerBound, TaxRate = m.TaxRate };
        public static implicit operator IncomeTaxBandDomainModel(IncomeTaxBandEntity e) => new IncomeTaxBandDomainModel { LowerBound = e.LowerBound, TaxRate = e.TaxRate };

        #endregion
    }
}
