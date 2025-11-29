# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the entire solution
COPY . .

# Restore all projects
RUN dotnet restore "LibraryManagementSystem.sln"

# Publish API project
RUN dotnet publish "LibraryManagementSystem.API/LibraryManagementSystem.API.csproj" -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Expose port
EXPOSE 80

# Entry point
ENTRYPOINT ["dotnet", "LibraryManagementSystem.API.dll"]
