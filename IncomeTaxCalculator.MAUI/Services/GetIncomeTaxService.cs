using IncomeTaxCalculator.MAUI.Services.Interfaces;
using Microsoft.Extensions.Logging;


namespace IncomeTaxCalculator.MAUI.Services
{
    public class GetIncomeTaxService : IGetIncomeTaxService
    {
        #region Members

        private ILogger<GetIncomeTaxService> _logger;
        private IIncomeTaxCalculatorApiService _incomeTaxCalculatorApiService;

        #endregion

        #region Constructors

        public GetIncomeTaxService(ILogger<GetIncomeTaxService> logger, IIncomeTaxCalculatorApiService incomeTaxCalculatorApiService)
        {
            _logger = logger;
            _incomeTaxCalculatorApiService = incomeTaxCalculatorApiService;
        }

        #endregion

        #region Methods

        public async Task<double> GetIncomeTax(long annualIncome, CancellationToken ct)
        {
            return await _incomeTaxCalculatorApiService.GetIncomeTax(annualIncome, ct);
        }

        #endregion
    }
}
