#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AmazonKillers.Orders.Api/AmazonKillers.Orders.Api.csproj", "AmazonKillers.Orders.Api/"]
RUN dotnet restore "AmazonKillers.Orders.Api/AmazonKillers.Orders.Api.csproj"
COPY . .
WORKDIR "/src/AmazonKillers.Orders.Api"
RUN dotnet build "AmazonKillers.Orders.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AmazonKillers.Orders.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AmazonKillers.Orders.Api.dll"]