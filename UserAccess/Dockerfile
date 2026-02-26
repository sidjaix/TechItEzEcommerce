# Base image for the final runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

# Build stage with the full .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and all relevant project files to leverage Docker layer caching
COPY ["TechItEzEcommerce.sln", "./"]

# UserAccess Module Projects
COPY ["UserAccess/User-Api/User-Api.csproj", "UserAccess/User-Api/"]
COPY ["UserAccess/User.Application/User.Application.csproj", "UserAccess/User.Application/"]
COPY ["UserAccess/User.Infrastructure/User.Infrastructure.csproj", "UserAccess/User.Infrastructure/"]
COPY ["UserAccess/User-Core/User-Core.csproj", "UserAccess/User-Core/"]

# Shared Module Dependencies
COPY ["Shared/Logging/Logging.csproj", "Shared/Logging/"]

# Restore dependencies for the UserAccess module projects
RUN dotnet restore "UserAccess/User-Api/User-Api.csproj"

# Copy the rest of the source code
COPY . .

# Set workdir for build and publish
WORKDIR "/src/UserAccess/User-Api"
RUN dotnet build "User-Api.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "User-Api.csproj" -c Release -o /app/publish

# Final stage - create the small, final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "User-Api.dll"]
