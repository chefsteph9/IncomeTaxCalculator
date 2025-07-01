using IncomeTaxCalculator.MAUI.PageViewModels;

namespace IncomeTaxCalculator.MAUI.Pages;

public partial class IncomeTaxCalculatorPage : ContentPage
{
	public IncomeTaxCalculatorPage(IncomeTaxCalculatorPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}