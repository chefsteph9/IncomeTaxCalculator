using IncomeTaxCalculator.Domain.Models.DbEntities;
using IncomeTaxCalculator.Domain.Models.DomainModels;
using IncomeTaxCalculator.Domain.Repositories.Interfaces;
using IncomeTaxCalculator.Domain.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace IncomeTaxCalculator.Domain.Tests.ServiceTests
{
    public class UnitedKingdomIncomeTaxCalculatorServiceTests
    {
        #region Members

        private UnitedKingdomIncomeTaxCalculatorService _unitedKingdomIncomeTaxCalculatorService_SUT;
        private Mock<ILogger<UnitedKingdomIncomeTaxCalculatorService>> _loggerMock = new(MockBehavior.Strict);
        private Mock<IIncomeTaxBandRepository> _incomeTaxBandRepositoryMock = new(MockBehavior.Strict);

        private List<IncomeTaxBandEntity> _incomeTaxBandEntitiesHappy = new();

        private List<IncomeTaxBandDomainModel> _incomeTaxBandDomainModelsHappy = new();

        #endregion

        #region Methods

        [SetUp]
        public void Setup()
        {
            var now = DateTime.Now;
            _incomeTaxBandEntitiesHappy.Add(new IncomeTaxBandEntity() { ID = 1, LowerBound = 0, TaxRate = 0, CreateTimestamp = now });
            _incomeTaxBandEntitiesHappy.Add(new IncomeTaxBandEntity() { ID = 2, LowerBound = 5000, TaxRate = 20, CreateTimestamp = now });
            _incomeTaxBandEntitiesHappy.Add(new IncomeTaxBandEntity() { ID = 3, LowerBound = 20000, TaxRate = 40, CreateTimestamp = now });

            _incomeTaxBandDomainModelsHappy.Add(new IncomeTaxBandDomainModel() { LowerBound = 0, TaxRate = 0 });
            _incomeTaxBandDomainModelsHappy.Add(new IncomeTaxBandDomainModel() { LowerBound = 5000, TaxRate = 20 });
            _incomeTaxBandDomainModelsHappy.Add(new IncomeTaxBandDomainModel() { LowerBound = 20000, TaxRate = 40 });

            _unitedKingdomIncomeTaxCalculatorService_SUT = new(_loggerMock.Object, _incomeTaxBandRepositoryMock.Object);
        }

        #region GetIncomeTaxAsync Tests

        [TestCase(0, 0.0)]
        [TestCase(5000, 0.0)]
        [TestCase(10000, 1000.0)]
        [TestCase(40000, 11000.0)]
        [TestCase(100000, 35000.0)]
        public async Task GetIncomeTaxAsync_CalculatesIncomeTax_WithHappyIncomeTaxBands(long annualIncome, double expectedTaxesPaid)
        {
            _incomeTaxBandRepositoryMock
                .Setup(r => r.GetIncomeTaxBandsAsync(CancellationToken.None))
                .ReturnsAsync(_incomeTaxBandEntitiesHappy);

            var actualTaxesPaid = await _unitedKingdomIncomeTaxCalculatorService_SUT.GetIncomeTaxAsync(annualIncome, CancellationToken.None);

            Assert.That(actualTaxesPaid, Is.EqualTo(expectedTaxesPaid));
        }

        [TestCase(0, 0.0)]
        [TestCase(5000, 0.0)]
        [TestCase(10000, 1000.0)]
        [TestCase(40000, 11000.0)]
        [TestCase(100000, 35000.0)]
        public async Task GetIncomeTaxAsync_CalculatesIncomeTax_WithUnsortedIncomeTaxBands(long annualIncome, double expectedTaxesPaid)
        {
            DateTime now = DateTime.Now;
            List<IncomeTaxBandEntity> incomeTaxBandEntitiesUnsorted = new();
            incomeTaxBandEntitiesUnsorted.Add(new IncomeTaxBandEntity() { ID = 1, LowerBound = 5000, TaxRate = 20, CreateTimestamp = now });
            incomeTaxBandEntitiesUnsorted.Add(new IncomeTaxBandEntity() { ID = 2, LowerBound = 20000, TaxRate = 40, CreateTimestamp = now });
            incomeTaxBandEntitiesUnsorted.Add(new IncomeTaxBandEntity() { ID = 3, LowerBound = 0, TaxRate = 0, CreateTimestamp = now });

            _incomeTaxBandRepositoryMock
                .Setup(r => r.GetIncomeTaxBandsAsync(CancellationToken.None))
                .ReturnsAsync(incomeTaxBandEntitiesUnsorted);

            var actualTaxesPaid = await _unitedKingdomIncomeTaxCalculatorService_SUT.GetIncomeTaxAsync(annualIncome, CancellationToken.None);

            Assert.That(actualTaxesPaid, Is.EqualTo(expectedTaxesPaid));
        }

        #endregion

        #region UpdateIncomeTaxBandsAsync Tests

        [Test]
        public async Task GetIncomeTaxBandsAsync_ReturnsExpectedResult()
        {
            _incomeTaxBandRepositoryMock
                .Setup(r => r.GetIncomeTaxBandsAsync(CancellationToken.None))
                .ReturnsAsync(_incomeTaxBandEntitiesHappy);

            var result = await _unitedKingdomIncomeTaxCalculatorService_SUT.GetIncomeTaxBandsAsync(CancellationToken.None);

            Assert.That(_incomeTaxBandDomainModelsHappy.SequenceEqual(result));
        }

        #endregion

        #region UpdateIncomeTaxBandsAsync Tests

        [Test]
        public async Task UpdateIncomeTaxBandsAsync_ReturnsExpectedResult_HappyPath()
        {
            _incomeTaxBandRepositoryMock
                .Setup(r => r.UpdateIncomeTaxBands(It.IsAny<List<IncomeTaxBandEntity>>(), CancellationToken.None))
                .ReturnsAsync(_incomeTaxBandEntitiesHappy.Count);

            var result = await _unitedKingdomIncomeTaxCalculatorService_SUT.UpdateIncomeTaxBandsAsync(_incomeTaxBandDomainModelsHappy, CancellationToken.None);

            Assert.That(_incomeTaxBandEntitiesHappy.Count, Is.EqualTo(result));
        }

        [Test]
        public async Task UpdateIncomeTaxBandsAsync_ValidatesTaxBands()
        {
            // Due to strict mocking, _incomeTaxBandRepositoryMock will throw an exception if UpdateIncomeTaxBands() is called with different arguments than have been set up
            // Setup checks to make sure list passed in has the correct length and is sorted
            _incomeTaxBandRepositoryMock
                .Setup(r => r.UpdateIncomeTaxBands(It.Is<List<IncomeTaxBandEntity>>(l => l.Count == 3 && l[0].LowerBound == 0), CancellationToken.None))
                .ReturnsAsync(_incomeTaxBandEntitiesHappy.Count);

            var taxBandsWithDuplicatesAndUnsorted = new List<IncomeTaxBandDomainModel>();
            taxBandsWithDuplicatesAndUnsorted.Add(new IncomeTaxBandDomainModel() { LowerBound = 5000, TaxRate = 20 });
            taxBandsWithDuplicatesAndUnsorted.Add(new IncomeTaxBandDomainModel() { LowerBound = 20000, TaxRate = 40 });
            taxBandsWithDuplicatesAndUnsorted.Add(new IncomeTaxBandDomainModel() { LowerBound = 0, TaxRate = 0 });
            taxBandsWithDuplicatesAndUnsorted.Add(new IncomeTaxBandDomainModel() { LowerBound = 5000, TaxRate = 20 });

            var result = await _unitedKingdomIncomeTaxCalculatorService_SUT.UpdateIncomeTaxBandsAsync(taxBandsWithDuplicatesAndUnsorted, CancellationToken.None);

            Assert.That(_incomeTaxBandDomainModelsHappy.Count, Is.EqualTo(result));
        }

        #endregion

        #endregion
    }
}