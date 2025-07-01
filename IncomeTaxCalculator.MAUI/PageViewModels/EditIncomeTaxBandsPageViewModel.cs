using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;

namespace IncomeTaxCalculator.MAUI.PageViewModels
{
    public partial class TaxBandModel : ObservableObject
    {
        [ObservableProperty]
        long lowerBound;

        [ObservableProperty]
        long? upperBound;

        [ObservableProperty]
        int taxRate;
    }

    public partial class EditIncomeTaxBandsPageViewModel : ObservableObject
    {
        #region Members

        private ILogger<EditIncomeTaxBandsPageViewModel> _logger;

        #endregion

        #region Constructors

        public EditIncomeTaxBandsPageViewModel(ILogger<EditIncomeTaxBandsPageViewModel> logger)
        {
            _logger = logger;
        }

        #endregion

        #region Properties

        [ObservableProperty]
        ObservableCollection<TaxBandModel> taxBands = new();

        #endregion

        #region Commands



        #endregion
    }
}
