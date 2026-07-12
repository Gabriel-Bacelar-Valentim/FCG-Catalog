FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["../FCG.Catalog/CatalogAPI/CatalogAPI.csproj", "FCG.Catalog/CatalogAPI/"]

COPY ["FCG.Contracts/FCG.Contracts.csproj", "FCG.Contracts/"]
COPY ["FCG.Domain/FCG.Domain.csproj", "FCG.Domain/"]
COPY ["FCG.Infrastructure/FCG.Infrastructure.csproj", "FCG.Infrastructure/"]

RUN dotnet restore "FCG.Catalog/CatalogAPI/CatalogAPI.csproj"

COPY . .

RUN dotnet publish "FCG.Catalog/CatalogAPI/CatalogAPI.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CatalogAPI.dll"]