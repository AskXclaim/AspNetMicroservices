FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 6006

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["/DiscountService/Discount.Shared/Discount.Shared.csproj","DiscountService/Discount.Shared/"]
RUN dotnet restore "DiscountService/Discount.Shared/Discount.Shared.csproj"
COPY ["/DiscountService/Discount.Grpc/Discount.Grpc.csproj", "DiscountService/Discount.Grpc/"]
RUN dotnet restore "DiscountService/Discount.Grpc/Discount.Grpc.csproj"
COPY DiscountService/Discount.Grpc /src/DiscountService/Discount.Grpc
COPY DiscountService/Discount.Shared /src/DiscountService/Discount.Shared
WORKDIR /src/DiscountService/Discount.Grpc/
RUN dotnet build "Discount.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM publish AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet","Discount.Grpc.dll"]
