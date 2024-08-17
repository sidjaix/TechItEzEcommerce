To check the installed Entity Framework (EF) versions on your machine, you can use the following steps:

Open a command prompt (cmd) or PowerShell window.
Use the following command to list all installed EF packages for all .NET Core/.NET projects on your machine:

dotnet tool list --global | findstr "dotnet-ef"

if you already have some version and wanted to upgrade use this cmd
dotnet tool update --global dotnet-ef --version 7.0.9
or
dotnet tool update --global dotnet-ef (this installs latest version)

To install latest EF use
dotnet tool install --global dotnet-ef

---

To Scaffold databse as model to local project use below cmd.

```
dotnet ef dbcontext scaffold "Server=localhost; Initial Catalog=TechItEzEcommerce; User ID=[username]; Password=[password]; TrustServerCertificate=true; MultipleActiveResultSets=true;" Microsoft.EntityFrameworkCore.SqlServer --context-dir ../User-Data --output-dir ./Entities
```

1. DbContextFolderIsRoot => dotnet ef migrations add AddedBaseEntityProperty -c UserDbContext --output-dir ./Migrations -s ../User-Api/User-Api.csproj

- dotnet ef migrations add AddedBaseEntityProperty -c ProductDbContext --output-dir ./Migrations -s ../Product-Api/Product-Api.csproj

2. dotnet ef migrations script -s ../User-Api/User-Api.csproj
   (if needed as script)dotnet ef database update

3. DbContextFolderIsRoot => dotnet ef database update -s ../User-Api/User-Api.csproj
   dotnet ef database update -s ../Product-Api/Product-Api.csproj

4. dotnet ef database drop -s ../User-Api/User-Api.csproj
   (to drop database)
