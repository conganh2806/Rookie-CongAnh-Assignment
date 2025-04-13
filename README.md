# Game Ecommerce

## Project Setup
### Note: All commands must be run from the ./Ecommerce directory.

## Development Environment
### 1. Copy the appsettings.example.json file.
### 2. Rename it to:
```sh
appsettings.Development.json
```

## Production Environment
### 1. Copy the appsettings.example.json file.
### 2. Rename it to:
```sh
appsettings.json
```

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

## -- Deploy to docker -- 
### Build
```sh
docker compose up --build
```
### Run 
```sh 
docker compose up -d 
```