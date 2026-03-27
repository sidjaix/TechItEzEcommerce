# Base image for the final runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80


# Why?
# Answer: Running applications as root inside a container can lead to security vulnerabilities. 
# If an attacker exploits a vulnerability in the application, they could potentially gain root access to the container and, by extension, the host system. 
# By creating a non-root user and group, we limit the potential damage that can be done if the application is compromised. 
# This is a best practice for container security, as it reduces the attack surface and helps to contain any potential breaches within the confines of the container. 
# Hence create a non-root user and group
RUN addgroup --system appgroup && adduser --system --ingroup appgroup appuser

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
COPY ["Shared/Api-Common/Api-Common.csproj", "Shared/Api-Common/"]

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

# Set ownership of the app directory to the non-root user
RUN chown -R appuser:appgroup /app

# Switch to the non-root user
USER appuser

# Healthcheck instruction
HEALTHCHECK --interval=30s --timeout=30s --start-period=5s --retries=3 CMD [ "curl", "-f", "http://localhost/health" ]

ENTRYPOINT ["dotnet", "User-Api.dll"]
