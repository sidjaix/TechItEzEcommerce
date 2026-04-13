# Base image for the final runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

# Running applications as root inside a container can lead to security vulnerabilities.
# Hence create a non-root user and group.
RUN addgroup --system appgroup && adduser --system --ingroup appgroup appuser

# Build stage with the full .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and all relevant project files to leverage Docker layer caching
COPY ["TechItEzEcommerce.sln", "./"]

# AI Module Projects
COPY ["AI/AI.Api/AI.Api.csproj", "AI/AI.Api/"]

# Shared Module Dependencies
COPY ["Shared/Api-Common/Api-Common.csproj", "Shared/Api-Common/"]

# Restore dependencies for the AI module projects
RUN dotnet restore "AI/AI.Api/AI.Api.csproj"

# Copy the rest of the source code
COPY . .

# Set workdir for build and publish
WORKDIR "/src/AI/AI.Api"
RUN dotnet build "AI.Api.csproj" -c Release -o /app/build --no-restore

# Publish stage
FROM build AS publish
RUN dotnet publish "AI.Api.csproj" -c Release -o /app/publish --no-restore

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

ENTRYPOINT ["dotnet", "AI.Api.dll"]