# IncomeTaxCalculator
A simple income tax calculator using a MAUI front end and an ASP.NET Core Web API backend

### Todo
* Add API tests
* Add ViewModel tests
* Add error handling
* Add UI for editing tax bands (Swagger page on the API works for now)
* Clean up UI
* <s>Add IncomeTaxBandInMemoryRepository to enable a user to test api without having a running SQL Server instance</s> Complete
* Add authentication to PostTaxBands endpoint
* Add documentation

### Future Enhancements
* Add some logging and use NLog to write logs to file
* Use AutoMapper
* Add the ability to have different tax bands per year and calculate taxes based on year

### Running the Calculator
Note: The API has 2 implementations of IIncomeTaxBandRepository, one connecting to a SQL Server instance with EF Core, and one using an in memory list for easy testing. You can switch between the implementations by switching build configurations. The Debug_InMemoryApiRepository configuration uses the in memory repository, all other configurations use the SQL Server one.

To quickly run the calculator:
* Open the solution in Visual Studio.
* Select the Debug_InMemoryApiRepository configuration from the dropdown.
* Select IncomeTaxCalculator.Api as the startup project.
* Run the API project.
* Open the solution again in another Visual Studio instance.
* Take note of the URL for the API and put the base url in the appsettings file in the IncomeTaxCalculator.MAUI project. It will be in this format: https://localhost:{portNumber}/api/IncomeTaxCalculator/
* Select IncomeTaxCalculator.MAUI as the startup project.
* Run the MAUI project

If you want to edit the tax bands, you can do so via the APIs swagger page. In the future I will add a UI to be able to modify the tax bands.

### Libraries Used
* IncomeTaxCalculator.Domain
  * Microsoft.EntityFrameworkCore
  * Microsoft.EntityFrameworkCore.SqlServer
* IncomeTaxCalculator.Domain.Tests
  * NUnit
  * Moq
* IncomeTaxCalculator.MAUI
  * CommunityToolkit.Mvvm (Handles a lot of viewmodel boilerplate code)
  * Microsoft.Extensions.ConfigurationsBinder
  * Microsoft.Extensions.Configuration.Json (This and above package allow using an appsetting.json file)
  * RestSharp (To connect to API)
  * Microsoft.Extensions.Configuration.UserSecrets (To manage user secrets)
