# E-Commerce Microservices Application

This is a simple e-commerce application built using microservices architecture. The application is designed to manage user profiles, products, orders, and related functionalities. Each microservice corresponds to a specific domain, facilitating modular development and scalability.

## Table of Contents

### Microservices

1. User Microservice
2. Product Microservice
3. Order Microservice
4. Cart and Wishlist Microservices

### Database Schema

#### Microservices

1. User Microservice
   Manages user profiles, roles, and activity logs.
   Handles user addresses, including shipping and billing addresses.
2. Product Microservice
   Manages product categories, products, images, and reviews.
3. Order Microservice
   Manages orders, order details, order status, payments, and coupons.
   Supports the application of coupons to orders.
4. Cart and Wishlist Microservices
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

Users
Roles
UserRoles
Address
Categories
Products
ProductImages
ProductReviews
Orders
OrderDetails
Payments
ContactUs
Coupons
OrderCoupons
ShoppingCart
Wishlist

Refer to the database script for detailed schema and constraints.

### Database Connection

```
"Data Source=[servername];Initial Catalog=[database_name];Integrated Security=SSPI; MultipleActiveResultSets=true;"
```

when using localdb use "(LocalDb)\\MSSQLLocalDB" as server name

To Scaffold database as model to local project use below command.

```
dotnet ef dbcontext scaffold "Server=localhost; Initial Catalog=TechItEzEcommerce; User ID=[username]; Password=[password]; TrustServerCertificate=true; MultipleActiveResultSets=true;" Microsoft.EntityFrameworkCore.SqlServer --context-dir ../User-Data --output-dir ./Entities
```

## Here's a suggested grouping of tables into microservices based on their functional relationships:

#### User Microservice:

This Microservice is deployed to Azure and can be accessed here https://techitez-ecommerce-user.azurewebsites.net/swagger/index.html

- Users
- Roles
- UserRoles
- Address (User addresses, including shipping and billing addresses)

#### Product Microservice:

- Categories
- Products
- ProductImages
- ProductReviews

#### Order Microservice:

- Orders
- OrderDetails
- Payments
- OrderCoupons
- Coupons

#### Cart and Wishlist Microservices:

- ShoppingCart
- Wishlist

#### Contact Page

- ContactUs

## Database Scripts

Database script can be found at root level of this repository E.g. TechItEzEcommerce.sql
