FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TechItEzEcommerce.sln", "/"]
# Copy User
COPY ["UI/E-Commerce/E-Commerce.csproj", "UI/E-Commerce/"]
COPY ["UI/ApiServices/ApiServices.csproj", "UI/ApiServices/"]

# RUN dotnet restore User
RUN dotnet restore "UI/ApiServices/ApiServices.csproj"
RUN dotnet restore "UI/E-Commerce/E-Commerce.csproj"


# Copy the remaining source code and build the application
COPY . .
WORKDIR "/src/UI/E-Commerce"
RUN dotnet build E-Commerce.csproj -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "E-Commerce.dll"]
