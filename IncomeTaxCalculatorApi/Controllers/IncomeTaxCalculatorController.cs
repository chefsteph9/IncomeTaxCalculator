using IncomeTaxCalculator.Api.Models.Domain;
using IncomeTaxCalculator.Api.Services.Interfaces;
using IncomeTaxCalculator.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace IncomeTaxCalculator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeTaxCalculatorController : ControllerBase
    {
        #region Members

        private ILogger<IncomeTaxCalculatorController> _logger;
        private IIncomeTaxCalculatorService _incomeTaxCalculatorService;

        #endregion

        #region Constructor

        public IncomeTaxCalculatorController(ILogger<IncomeTaxCalculatorController> logger, IIncomeTaxCalculatorService incomeTaxCalculatorService)
        {
            _logger = logger;
            _incomeTaxCalculatorService = incomeTaxCalculatorService;
        }

        #endregion

        #region Endpoints

        [Route("GetIncomeTax")]
        [HttpGet]
        public async Task<double> GetIncomeTax(long annualIncome, CancellationToken ct)
        {
            return await _incomeTaxCalculatorService.GetIncomeTaxAsync(annualIncome, ct);
        }

        [Route("GetIncomeTaxBands")]
        [HttpGet]
        public async Task<IEnumerable<IncomeTaxBandDto>> GetIncomeTaxBands(CancellationToken ct)
        {
            var incomeTaxBandDomainModels = await _incomeTaxCalculatorService.GetIncomeTaxBandsAsync(ct);

            var incomeTaxBands = incomeTaxBandDomainModels.ToList().ConvertAll(x => (IncomeTaxBandDto)x);

            return incomeTaxBands;
        }

        [Route("PostIncomeTaxBands")]
        [HttpPost]
        public async Task<int> PostIncomeTaxBands([FromBody] List<IncomeTaxBandDto> incomeTaxBands, CancellationToken ct)
        {
            var incomeTaxBandDomainModels = incomeTaxBands.ConvertAll<IncomeTaxBandDomainModel>(x => x);

            return await _incomeTaxCalculatorService.UpdateIncomeTaxBandsAsync(incomeTaxBandDomainModels, ct);
        }

        #endregion
    }
}
