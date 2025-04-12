# Stage 1: Build and Publish
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the csproj file from the project root and restore dependencies
COPY ["Bookstore.csproj", "."]
RUN dotnet restore "Bookstore.csproj"

# Copy the rest of the source code to the container
COPY . .

# Build and publish the application
RUN dotnet build "Bookstore.csproj" -c Release -o /app/build
RUN dotnet publish "Bookstore.csproj" -c Release -o /app/publish

# Stage 2: Create the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Copy the published output from the build stage
COPY --from=build /app/publish .

# Define the entrypoint for the container
ENTRYPOINT ["dotnet", "Bookstore.dll"]
