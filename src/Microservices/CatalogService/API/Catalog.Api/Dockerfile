FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["*.csproj", "/Services/Catalog/Catalog.Api/"]
RUN dotnet restore "/Services/Catalog/Catalog.Api/Catalog.Api.csproj"
COPY . ./
WORKDIR "/src/Services/Catalog.Api" 
RUN dotnet build "Catalog.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Catalog.Api.csproj" -c Release -o /app/publish

FROM publish AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet","Catalog.Api.dll"]