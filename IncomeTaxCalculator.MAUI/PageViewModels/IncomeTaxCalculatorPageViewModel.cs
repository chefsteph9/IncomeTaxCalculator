using CommunityToolkit.Mvvm.Input;
using IncomeTaxCalculator.MAUI.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System.ComponentModel;

namespace IncomeTaxCalculator.MAUI.PageViewModels
{
    public partial class IncomeTaxCalculatorPageViewModel : INotifyPropertyChanged
    {
        #region Members

        private ILogger<IncomeTaxCalculatorPageViewModel> _logger;
        private IGetIncomeTaxService _getIncomeTaxService;

        #endregion

        #region Constructors

        public IncomeTaxCalculatorPageViewModel(ILogger<IncomeTaxCalculatorPageViewModel> logger, IGetIncomeTaxService getIncomeTaxService)
        {
            _logger = logger;
            _getIncomeTaxService = getIncomeTaxService;
        }

        #endregion

        #region Properties

        #region Private Calculation Properties

        // Gotta make sure Elon Musk can enter his salary in too :)
        private long _grossAnnualSalary = 0;
        private long GrossAnnualSalary
        {
            get
            {
                return _grossAnnualSalary;
            }
            set
            {
                _grossAnnualSalary = value;

                //Update fields that depend on the gross annual salary
                OnPropertyChanged(nameof(GrossAnnualSalaryText));
                OnPropertyChanged(nameof(GrossMonthlySalaryText));
                OnPropertyChanged(nameof(NetAnnualSalaryText));
                OnPropertyChanged(nameof(NetMonthlySalaryText));
            }
        }

        private double GrossMonthlySalary
        {
            get
            {
                return Math.Round((GrossAnnualSalary / 12.0), 2);
            }
        }

        private double NetAnnualSalary
        {
            get
            {
                return Math.Round((GrossAnnualSalary - AnnualTaxesPaid), 2);
            }
        }

        private double NetMonthlySalary
        {
            get
            {
                return Math.Round((NetAnnualSalary / 12.0), 2);
            }
        }

        private double _annualTaxesPaid = 0;
        private double AnnualTaxesPaid
        {
            get
            {
                return _annualTaxesPaid;
            }
            set
            {
                _annualTaxesPaid = value;

                // Update fields that depend on annual taxes paid
                OnPropertyChanged(nameof(NetAnnualSalaryText));
                OnPropertyChanged(nameof(NetMonthlySalaryText));
                OnPropertyChanged(nameof(AnnualTaxesPaidText));
                OnPropertyChanged(nameof(MonthlyTaxesPaidText));
            }
        }

        private double MonthlyTaxesPaid
        {
            get
            {
                return Math.Round((AnnualTaxesPaid / 12.0), 2);
            }
        }

        #endregion

        #region UI Properties

        private string _grossAnnualSalaryInput = "";
        public string GrossAnnualSalaryInput
        {
            get
            {
                return _grossAnnualSalaryInput;
            }
            set
            {
                if (ValidateGrossAnnualSalaryInput(value) && _grossAnnualSalaryInput != value)
                {
                    _grossAnnualSalaryInput = value.Trim();
                }
                
                OnPropertyChanged(nameof(GrossAnnualSalaryInput));
            }
        }

        public string GrossAnnualSalaryText
        {
            get
            {
                return $"Gross annual salary:\t£{GrossAnnualSalary}";
            }
        }

        public string GrossMonthlySalaryText
        {
            get
            {
                return $"Gross monthly salary:\t£{GrossMonthlySalary}";
            }
        }

        public string NetAnnualSalaryText
        {
            get
            {
                return $"Net annual salary:\t\t£{NetAnnualSalary}";
            }
        }

        public string NetMonthlySalaryText
        {
            get
            {
                return $"Net monthly salary:\t£{NetMonthlySalary}";
            }
        }

        public string AnnualTaxesPaidText
        {
            get
            {
                return $"Annual taxes paid:\t£{AnnualTaxesPaid}";
            }
        }

        public string MonthlyTaxesPaidText
        {
            get
            {
                return $"Monthly taxes paid:\t£{MonthlyTaxesPaid}";
            }
        }

        #endregion

        #endregion

        #region Methods

        private bool ValidateGrossAnnualSalaryInput(string input)
        {
            return long.TryParse(input, out _) || string.IsNullOrEmpty(input);
        }

        [RelayCommand]
        public async Task CalculateTaxes(CancellationToken ct)
        {
            if (ValidateGrossAnnualSalaryInput(GrossAnnualSalaryInput))
            {
                long grossAnnualSalary = 0;
                long.TryParse(GrossAnnualSalaryInput, out grossAnnualSalary);
                var annualTaxPaid = await _getIncomeTaxService.GetIncomeTax(grossAnnualSalary, ct);
                GrossAnnualSalary = grossAnnualSalary;
                AnnualTaxesPaid = annualTaxPaid;
            }
        }

        #endregion

        #region Events

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
