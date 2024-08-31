FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TechItEzEcommerce.sln", "/"]

# Copy Product
COPY ["Order/Order-Core/Order-Core.csproj", "Order/Order-Core/"]
COPY ["Consumers/Order-Service/Order-Service.csproj", "Consumers/Order-Service/"]

# RUN dotnet restore User
RUN dotnet restore "Order/Order-Core/Order-Core.csproj"
RUN dotnet restore "Consumers/Order-Service/Order-Service.csproj"

# Copy the remaining source code and build the application
COPY . .
WORKDIR "/src/Consumers/Order-Service"
RUN dotnet build Order-Service.csproj -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order-Service.dll"]

