FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TechItEzEcommerce.sln", "/"]
# Copy User
COPY ["User/User-Core/User-Core.csproj", "User/User-Core/"]
COPY ["User/User-Data/User-Data.csproj", "User/User-Data/"]
COPY ["User/User-Api/User-Api.csproj", "User/User-Api/"]

# RUN dotnet restore User
RUN dotnet restore "User/User-Core/User-Core.csproj"
RUN dotnet restore "User/User-Data/User-Data.csproj"
RUN dotnet restore "User/User-Api/User-Api.csproj"


# Copy the remaining source code and build the application
COPY . .
WORKDIR "/src/User/User-Api"
RUN dotnet build User-Api.csproj -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "User-Api.dll"]
