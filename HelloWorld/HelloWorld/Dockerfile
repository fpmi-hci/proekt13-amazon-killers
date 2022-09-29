FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

FROM base AS final
WORKDIR /app
COPY --from=publish /katyayermolayeva/amazon-killers .
ENTRYPOINT ["dotnet", "HelloWorld.dll"]
