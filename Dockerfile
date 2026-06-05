# Stage 1: Base Runtime Environment
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Stage 2: SDK Build Environment
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy solution and project files first for efficient caching
COPY ["E-Medic.sln", "./"]
COPY ["E-Medic/E-Medic.csproj", "E-Medic/"]

# Restore dependencies
RUN dotnet restore "E-Medic/E-Medic.csproj"

# Copy the entire source code
COPY . .
WORKDIR "/src/E-Medic"

# Build the application in Release mode
RUN dotnet build "E-Medic.csproj" -c Release -o /app/build

# Stage 3: Publish the Application
FROM build AS publish
RUN dotnet publish "E-Medic.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 4: Final Production Image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# 🎯 CRITICAL FIX: Copy Views and Static Files to Runtime Container
COPY E-Medic/Views ./Views
COPY E-Medic/wwwroot ./wwwroot

ENTRYPOINT ["dotnet", "E-Medic.dll"]