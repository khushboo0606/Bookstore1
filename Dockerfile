# Stage 1: Build and Publish
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files and restore dependencies
COPY ["Bookstore.csproj", "./"]
RUN dotnet restore

# Copy everything else
COPY . .

# Build and publish the application
RUN dotnet build -c Release
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
EXPOSE 80
EXPOSE 443

#  Make app listen on all interfaces inside container
ENV ASPNETCORE_URLS=http://0.0.0.0:80

HEALTHCHECK CMD curl --fail http://localhost:80/health || exit 1

# Copy published app
COPY --from=build /app/publish .

# Default entry point
ENTRYPOINT ["dotnet", "Bookstore.dll"]
