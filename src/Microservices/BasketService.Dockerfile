FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS  base
WORKDIR /app
EXPOSE 6002

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./BasketService/Basket.Api/Basket.Api.csproj","BasketService/Basket.Api/"]
RUN dotnet Restore "BasketService/Basket.Api/Basket.Api.csproj"
COPY BasketService /src/BasketService
WORKDIR "/src/BasketService/Basket.Api/"
RUN dotnet build "Basket.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM publish AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN rm -rf /app/publish
RUN rm -rf /app/build
RUN rm -rf /src
ENTRYPOINT ["dotnet","Basket.Api.dll"]
