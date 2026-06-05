# Stage 1: Base Runtime Environment
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Stage 2: SDK Build Environment
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["E-Medic.csproj", "./"]
RUN dotnet restore "E-Medic.csproj"

COPY . .
RUN dotnet build "E-Medic.csproj" -c Release -o /app/build

# Stage 3: Publish the Application
FROM build AS publish
RUN dotnet publish "E-Medic.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 4: Final Production Image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

COPY Views ./Views
COPY wwwroot ./wwwroot

ENTRYPOINT ["dotnet", "E-Medic.dll"]