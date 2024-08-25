# Entity Framework Core Details

## To check the installed Entity Framework (EF) versions on your machine, you can use the following steps

Open a command prompt (cmd) or PowerShell window.
Use the following command to list all installed EF packages for all .NET Core/.NET projects on your machine:

- if you already have some version and wanted to upgrade use this cmd
  dotnet tool update --global dotnet-ef --version 7.0.9
  or
  dotnet tool update --global dotnet-ef (this installs latest version)

- To install latest EF use
  dotnet tool install --global dotnet-ef

```bash
dotnet tool list --global | findstr "dotnet-ef"
```

## To Scaffold databse as model to local project use below cmd

```bash
dotnet ef dbcontext scaffold "Server=localhost; Initial Catalog=TechItEzEcommerce; User ID=[username]; Password=[password]; TrustServerCertificate=true; MultipleActiveResultSets=true;" Microsoft.EntityFrameworkCore.SqlServer --context-dir ../User-Data --output-dir ./Entities
```

## Add Migration DbContextFolderIsRoot

```bash
dotnet ef migrations add AddedBaseEntityProperty -c UserDbContext --output-dir .Migrations -s ../User-Api/User-Api.csproj

dotnet ef migrations add InitialMigration -c ProductDbContext --output-dir .Migrations -s ../Product-Api/Product-Api.csproj

dotnet ef migrations add InitialMigration -c CartDbContext --output-dir .Migrations -s ../Cart-Api/Cart-Api.csproj

dotnet ef migrations add InitialMigration -c OrderDbContext --output-dir ./Migrations -s ../Order-Api/Order-Api.csproj
```

## Update Database DbContextFolderIsRoot =>

```bash
dotnet ef database update -s ../User-Api/User-Api.csproj

dotnet ef database update -s ../Product-Api/Product-Api.csproj

dotnet ef database update -s ../Cart-Api/Cart-Api.csproj

dotnet ef database update -s ../Order-Api/Order-Api.csproj
```

## Generate Script from migration

```bash
dotnet ef migrations script -s ../User-Api/User-Api.csproj
```

## Drop database from migration

```bash
dotnet ef database drop -s ../User-Api/User-Api.csproj
```
