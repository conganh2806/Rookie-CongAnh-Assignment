# Game Ecommerce

## Project Setup
### Note: All commands must be run from the ./Ecommerce directory.

# Entity Framework Core
## -- Add a new migration -- 
```sh
dotnet ef migrations add [Migration-Msg] --project ECommerce.Infrastructure --startup-project ECommerce.API --output-dir Data/Migrations
```

### Replace [MigrationName] with a descriptive name for your migration.

## -- Apply Migrations to the Database --
```sh
dotnet ef database update --project ECommerce.Infrastructure --startup-project ECommerce.API
```

## -- 