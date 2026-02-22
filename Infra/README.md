# E-Commerce Microservices Application

This is a simple e-commerce application built using microservices architecture. The application is designed to manage user profiles, products, orders, and related functionalities. Each microservice corresponds to a specific domain, facilitating modular development and scalability.

## Table of Contents

### Microservices

1. User Microservice
2. Product Microservice
3. Order Microservice
4. Cart and Wishlist Microservices

### Database Schema

### Microservices

1. User - Microservice
   Manages user profiles, roles, and activity logs.
   Handles user addresses, including shipping and billing addresses.
2. Product - Microservice
   Manages product categories, products, images, and reviews.
3. Order - Microservice
   Manages orders, order details, order status, payments, and coupons.
   Supports the application of coupons to orders.
4. Cart and Wishlist - Microservices
   Manages user shopping carts and wishlists.

#### Database Schema

The application uses a shared database with the following tables:

```sql
1. select * from Users
2. select * from Roles
3. select * from UserRoles
4. select * from Address
5. select * from Categories
6. select * from Products
7. select * from ProductImages
8. select * from ProductReviews
9. select * from Orders
10. select * from OrderDetails
11. select * from Payments
12. select * from ContactUs
13. select * from Coupons
14. select * from OrderCoupons
15. select * from ShoppingCart
16. select * from Wishlist
```

Users,
,Roles
,UserRoles
,Address
,Categories
,Products
,ProductImages
,ProductReviews
,Orders
,OrderDetails
,Payments
,ContactUs
,Coupons
,OrderCoupons
,ShoppingCart
,Wishlist

Refer to the database script for detailed schema and constraints.

### Database Connection

```text
Data Source=[servername];Initial Catalog=[database_name];Integrated Security=SSPI; MultipleActiveResultSets=true;
```

- when using localdb use "(LocalDb)\\MSSQLLocalDB" as server name

## Here's a suggested grouping of tables into microservices based on their functional relationships

## Database Scripts

Database script can be found at root level of this repository E.g. ~/Infra/TechItEzEcommerce.sql

# Dockerfile Setup

- [Docker Hub](https://hub.docker.com/repositories/sidjaix)

- In copy command mention project's relative path from soltion folder. As you can see in this project Dockerfile.

# Docker Commands

## Build new image on tp of the earlier

```bash
docker build -t < ImageName : < TagName > .
```

## Remove image

```bash
docker rmi <First 3 Letter of ImageID>
```

## Show all Images

```bash
docker images
```

## Run the docker image on the container

docker run -d --name < ContainerName > -p 8080:80 < Imagename > : < TagName >

```bash
docker build -t techitez-sqlserver .
docker run -d --name techitez_sqlserver -e SA_PASSWORD='Admin@123' -e ACCEPT_EULA='1' -p 1433:1433 -v techitez-sqledge:/var/opt/mssql techitez-sqlserver
docker network connect techitez_network techitez_sqlserver
```

## Show all the container

```bash
docker ps -s
```

## Stop the container

```bash
docker stop <First 3 Letter of ContainerID>
```

## Start Container

```bash
docker start <First 3 Letter of ContainerID>
```

## Remove Container

```bash
docker rm <First 3 Letter of ContainerID>
```

## Create a Custom Docker Network (if you want multiple containers later)

```bash
docker network create my-sql-network
```

## Run SQL Server individually without docker network

```bash

   docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=YourStrongPassword123!' -e 'MSSQL_PID=Express' -p 1435:1433 --name sqlserver-container --hostname my-sql-edge-host --network my-sql-network -v sql_edge_backup:/var/opt/mssql/backup -d mcr.microsoft.com/mssql/server:2022-latest


   docker run -e 'ACCEPT_EULA=1' -e 'MSSQL_SA_PASSWORD=YourStrong!Passw0rd' -p 1433:1433 --name my-sql-edge-container --hostname my-sql-edge-host --network my-sql-network -v sql_edge_backup:/var/opt/mssql/backup -d mcr.microsoft.com/azure-sql-edge:latest

```

- Now other containers on my-sql-network can connect to this SQL Server using my-sql-edge-host,1433

## You can use environment variables to configure SQL Server on Linux Containers

- ACCEPT_EULA confirms your acceptance of the End-User Licensing Agreement.

- SA_PASSWORD is the database system administrator (userid = 'sa') password used to connect to SQL Server once the container is running. Important note: This
  password needs to include at least 8 characters of at least three of these four categories: uppercase letters, lowercase letters, numbers and non-alphanumeric symbols.

- MSSQL_PID is the Product ID (PID) or Edition that the container will run with. Acceptable values:

  -- Developer : This will run the container using the Developer Edition (this is the default if no MSSQL_PID environment variable is supplied)
  -- Express : This will run the container using the Express Edition
  -- Standard : This will run the container using the Standard Edition
  -- Enterprise : This will run the container using the Enterprise Edition
  -- EnterpriseCore : This will run the container using the Enterprise Edition Core

## Docker Push new build to Existing registry repo

```bash
docker build -t user-api:latest .
docker tag user-api:latest sidjaix/user-api:latest
docker push sidjaix/user-api:latest
```

## Docker Compose Setup

- [Deploy a containerized app to Azure](https://code.visualstudio.com/docs/containers/app-service)
- In Docker-Compose connection string server name should be "sqlserver"
- Set Environment variable as per you need i.e - "Development", "Production"
- If Pushing image to Docker Hub then image name should be followed by "<your registry or username>/<image name>:<tag>" in docker-compose file

## Azure Container Registry

- Pull from Azure registry

```bash
docker pull sidjaix.azurecr.io/user-api:latest
```

- Run From azure registry

```bash
docker run -p 10001:80 sidjaix.azurecr.io/user-api:latest -e "ConnectionStrings_AzureDB=Server=tcp:techitez.database.windows.net,1433;Initial Catalog=TechItEzEcommerce;Persist Security Info=False;User ID=sidjaix;Password=admin@123; MultipleActiveResultSets=True; Encrypt=True; TrustServerCertificate=True; Connection Timeout=60;" -rm
```

<!--
navigate to compose file location
docker-compose build
docker-compose up -d
-->
