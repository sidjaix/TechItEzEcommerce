FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TechItEzEcommerce.sln", "/"]

# Copy Product
COPY ["Product/Product-Core/Product-Core.csproj", "Product/Product-Core/"]
COPY ["Product/Product-Data/Product-Data.csproj", "Product/Product-Core/"]
COPY ["Product/Product-Api/Product-Api.csproj", "Product/Product-Core/"]

# RUN dotnet restore User
RUN dotnet restore "Product/Product-Core/Product-Core.csproj"
RUN dotnet restore "Product/Product-Core/Product-Data.csproj"
RUN dotnet restore "Product/Product-Core/Product-Api.csproj"

# Copy the remaining source code and build the application
COPY . .
WORKDIR "/src/Product/Product-Api"
RUN dotnet build Product-Api.csproj -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Product-Api.dll"]
