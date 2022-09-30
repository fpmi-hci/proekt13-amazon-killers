FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base

WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=base /app/publish .
ENTRYPOINT ["dotnet", "HelloWorld.dll"]
