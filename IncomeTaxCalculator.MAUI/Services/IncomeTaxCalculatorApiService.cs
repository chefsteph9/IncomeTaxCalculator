using IncomeTaxCalculator.MAUI.Configuration;
using IncomeTaxCalculator.MAUI.Services.Interfaces;
using IncomeTaxCalculator.Models.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace IncomeTaxCalculator.MAUI.Services
{
    public class IncomeTaxCalculatorApiService : IIncomeTaxCalculatorApiService
    {
        #region Members

        private ILogger<IncomeTaxCalculatorApiService> _logger;
        private IncomeTaxCalculatorApiConfiguration _apiConfig;
        private RestClient _restClient;

        #endregion

        #region Constructors

        public IncomeTaxCalculatorApiService(ILogger<IncomeTaxCalculatorApiService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _apiConfig = configuration.GetSection("IncomeTaxCalculatorApi").Get<IncomeTaxCalculatorApiConfiguration>();

            InitializeRestClient();
        }

        private void InitializeRestClient()
        {
            var options = new RestClientOptions(_apiConfig.BaseUrl);
            _restClient = new RestClient(options);
        }

        #endregion

        #region Methods

        public async Task<double> GetIncomeTax(long annualIncome, CancellationToken ct)
        {
            double annualIncomeTax = 0;

            var request = new RestRequest(_apiConfig.GetIncomeTaxEndpoint, Method.Get);
            request.AddOrUpdateParameter("annualIncome", annualIncome);

            var response = await _restClient.GetAsync(request, ct);

            if (response.IsSuccessStatusCode && !string.IsNullOrEmpty(response.Content))
            {
                var success = double.TryParse(response.Content, out annualIncomeTax);
            }

            return annualIncomeTax;
        }

        public async Task<List<IncomeTaxBandDto>> GetIncomeTaxBands(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public async Task<int> PostIncomeTaxBands(List<IncomeTaxBandDto> incomeTaxBands, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
