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

# Order Module Projects
COPY ["Order/Order-Api/Order-Api.csproj", "Order/Order-Api/"]
COPY ["Order/Order-Application/Order-Application.csproj", "Order/Order-Application/"]
COPY ["Order/Order-Data/Order-Data.csproj", "Order/Order-Data/"]
COPY ["Order/Order-Core/Order-Core.csproj", "Order/Order-Core/"]

# Shared Module Dependencies
COPY ["Shared/Api-Common/Api-Common.csproj", "Shared/Api-Common/"]

# Restore dependencies for the Order module projects
RUN dotnet restore "Order/Order-Api/Order-Api.csproj"

# Copy the rest of the source code
COPY . .

# Set workdir for build and publish
WORKDIR "/src/Order/Order-Api"
RUN dotnet build "Order-Api.csproj" -c Release -o /app/build --no-restore

# Publish stage
FROM build AS publish
RUN dotnet publish "Order-Api.csproj" -c Release -o /app/publish --no-restore

# Final stage - create the small, final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set ownership of the app directory to the non-root user
RUN chown -R appuser:appgroup /app

# Switch to the non-root user
USER appuser

# Healthcheck instruction
# Ensure your Order-Api has the ASP.NET Core HealthChecks middleware configured at the "/health" endpoint
HEALTHCHECK --interval=30s --timeout=30s --start-period=5s --retries=3 CMD [ "curl", "-f", "http://localhost/health" ]

ENTRYPOINT ["dotnet", "Order-Api.dll"]