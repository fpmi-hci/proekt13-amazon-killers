FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base

WORKDIR /app

COPY . ./
RUN dotnet restore ./HelloWorld
RUN dotnet build -c Release -o out ./HelloWorld/HelloWorld/HelloWorld.csproj
RUN dotnet publish -c Release -o out ./HelloWorld/HelloWorld/HelloWorld.csproj

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=base /app/out .
ENTRYPOINT ["dotnet", "HelloWorld.dll"]
