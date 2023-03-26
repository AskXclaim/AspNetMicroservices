FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 6004

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
COPY ["./DiscountService/Discount.Api/Discount.Api.csproj","DiscountService/Discount.Api/"]
RUN dotnet Restore "DiscountService/Discount.Api/Discount.Api.csproj"
COPY DiscountService /src/DiscountService
WORKDIR "/src/DiscountService/Discount.Api/"
RUN dotnet build "Discount.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM publish AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN rm -rf /app/publish
RUN rm -rf /app/build
RUN rm -rf /src
ENTRYPOINT ["dotnet","Discount.Api.dll"]