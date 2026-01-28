cd .\WebApi\GarageGenius.WebApi\
dotnet ef migrations add Initial --verbose --context CustomersDbContext --project ..\..\Modules\Customers\GarageGenius.Modules.Customers.Infrastructure\ --output-dir Persistance\Migrations
dotnet ef database update --context CustomersDbContext