FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /publish
COPY . .
RUN dotnet publish src/Guexit.IdentityProvider.WebApi/Guexit.IdentityProvider.WebApi.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /publish/out .
# Run this to generate it: dotnet dev-certs https -ep cert.pfx -p Test1234!
COPY cert.pfx /https/cert.pfx
ENTRYPOINT ["dotnet", "Guexit.IdentityProvider.WebApi.dll"]
