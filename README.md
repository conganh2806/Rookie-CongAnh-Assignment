dotnet ef migrations add InitialCreate --project ECommerce.Infrastructure --startup-project ECommerce.API --output-dir Data/Migrations

dotnet ef database update --project ECommerce.Infrastructure --startup-project ECommerce.API