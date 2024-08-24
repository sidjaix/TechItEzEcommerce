FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TechItEzEcommerce.sln", "/"]

# Copy Order
COPY ["Order/Order-Core/Order-Core.csproj", "Order/Order-Core/"]
COPY ["Order/Order-Data/Order-Data.csproj", "Order/Order-Core/"]
COPY ["Order/Order-Api/Order-Api.csproj", "Order/Order-Core/"]

# RUN dotnet restore User
RUN dotnet restore "Order/Order-Core/Order-Core.csproj"
RUN dotnet restore "Order/Order-Core/Order-Data.csproj"
RUN dotnet restore "Order/Order-Core/Order-Api.csproj"

# Copy the remaining source code and build the application
COPY . .
WORKDIR "/src/Order/Order-Api"
RUN dotnet build Order-Api.csproj -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order-Api.dll"]
