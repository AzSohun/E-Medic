# 1. Build Stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build-env
WORKDIR /app

COPY . ./

RUN dotnet restore
RUN dotnet publish -c Release -o out

# 2. Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build-env /app/out .

ENV ASPNETCORE_URLS=http://+:10000

ENTRYPOINT ["dotnet", "E-Medic.dll"]