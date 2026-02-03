# ----- Build Stage -----
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy Project Files And Restore Dependencies
COPY ["Simple Game Store WEB-API.csproj", "./"]
RUN dotnet restore "./Simple Game Store WEB-API.csproj"

# Copy All Files And Build The Project
COPY . .
RUN dotnet publish "./Simple Game Store WEB-API.csproj" -c Release -o /app/publish

# ----- Runtime Stage -----
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Simple Game Store WEB-API.dll"]