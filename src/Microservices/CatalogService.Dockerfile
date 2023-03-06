FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 6000

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
COPY ["./CatalogService/API/Catalog.Api/Catalog.Api.csproj", "CatalogService/API/Catalog.Api/"]
COPY ["./CatalogService/Core/Application/Catalog.Application/Catalog.Application.csproj", "CatalogService/Core/Application/Catalog.Application/"]
COPY ["./CatalogService/Core/Domain/Catalog.Domain/Catalog.Domain.csproj", "CatalogService/Core/Domain/Catalog.Domain/"]
COPY ["./CatalogService/Infrastructure/Persistence/Catalog.Persistence/Catalog.Persistence.csproj", "CatalogService/Infrastructure/Persistence/Catalog.Persistence/"]
RUN dotnet restore "CatalogService/API/Catalog.Api/Catalog.Api.csproj"
COPY CatalogService /src/CatalogService
WORKDIR "/src/CatalogService/API/Catalog.Api/" 
RUN dotnet build "Catalog.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Catalog.Api.csproj" -c Release -o /app/publish

FROM publish AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN rm -rf /app/publish
RUN rm -rf /app/build
RUN rm -rf /src
ENTRYPOINT ["dotnet","Catalog.Api.dll"]