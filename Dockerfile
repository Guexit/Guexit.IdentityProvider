FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /publish
COPY . .
RUN dotnet publish src/Guexit.IdentityProvider.WebApi/Guexit.IdentityProvider.WebApi.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /publish/out .

ENTRYPOINT ["dotnet", "Guexit.IdentityProvider.WebApi.dll"]
