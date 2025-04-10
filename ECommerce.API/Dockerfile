# Use the base image with ASP.NET Core runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER root
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:8123
EXPOSE 8123

# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ./ECommerce.API/ECommerce.API.csproj ./ECommerce.API/
RUN dotnet restore ./ECommerce.API/ECommerce.API.csproj
COPY . .
WORKDIR /src/ECommerce.API
RUN dotnet build ECommerce.API.csproj -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ECommerce.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ./ECommerce.API/appsettings.Development.json ./appsettings.Development.json
ENTRYPOINT ["dotnet", "ECommerce.API.dll"]