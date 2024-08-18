FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TechItEzEcommerce.sln", "/"]

# Copy Cart
COPY ["Cart/Cart-Core/Cart-Core.csproj", "Cart/Cart-Core/"]
COPY ["Cart/Cart-Data/Cart-Data.csproj", "Cart/Cart-Core/"]
COPY ["Cart/Cart-Api/Cart-Api.csproj", "Cart/Cart-Core/"]

# RUN dotnet restore User
RUN dotnet restore "Cart/Cart-Core/Cart-Core.csproj"
RUN dotnet restore "Cart/Cart-Core/Cart-Data.csproj"
RUN dotnet restore "Cart/Cart-Core/Cart-Api.csproj"

# Copy the remaining source code and build the application
COPY . .
WORKDIR "/src/Cart/Cart-Api"
RUN dotnet build Cart-Api.csproj -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cart-Api.dll"]
