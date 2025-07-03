# IncomeTaxCalculator
A simple income tax calculator using a MAUI front end and an ASP.NET Core Web API backend

### Todo
* Add API tests
* Add ViewModel tests
* Add error handling
* Add UI for editing tax bands (Swagger page on the API works for now)
* Clean up UI
* Add IncomeTaxBandInMemoryRepository to enable a user to test api without having a running SQL Server instance
* Add authentication to PostTaxBands endpoint
* Add documentation

### Future Enhancements
* Add some logging and use NLog to write logs to file
* Use AutoMapper
* Add the ability to have different tax bands per year and calculate taxes based on year
