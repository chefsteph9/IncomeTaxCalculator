using IncomeTaxCalculator.MAUI.Pages;
using IncomeTaxCalculator.MAUI.PageViewModels;
using IncomeTaxCalculator.MAUI.Services;
using IncomeTaxCalculator.MAUI.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace IncomeTaxCalculator.MAUI;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();

		var assembly = Assembly.GetExecutingAssembly();
		string appsettingsPath = $"appsettings.json";
		builder.Configuration.AddJsonFile(appsettingsPath);


		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});


		builder.Services.AddSingleton<IGetIncomeTaxService, GetIncomeTaxServiceMock>();
		builder.Services.AddSingleton<IIncomeTaxCalculatorApiService, IncomeTaxCalculatorApiService>();
		
		builder.Services.AddSingleton<IncomeTaxCalculatorPageViewModel>();
		builder.Services.AddSingleton<IncomeTaxCalculatorPage>();

		builder.Services.AddSingleton<EditIncomeTaxBandsPage>();
		builder.Services.AddSingleton<EditIncomeTaxBandsPageViewModel>();
#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
