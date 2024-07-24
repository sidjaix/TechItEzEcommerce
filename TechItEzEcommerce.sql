IF NOT EXISTS (SELECT 1 FROM sys.databases WHERE name = 'TechItEzEcommerce')
BEGIN
    -- Create the database
    CREATE DATABASE TechItEzEcommerce;
END
ELSE
BEGIN
   DROP DATABASE TechItEzEcommerce;
END

Go
use TechItEzEcommerce
go


-- Users
CREATE TABLE Users (
    UserID INT IDENTITY(1, 1),
    Username NVARCHAR(100) NOT NULL,
    Password NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Address NVARCHAR(255),
    UserType NVARCHAR(50) NOT NULL, -- Assuming UserType can be 'Customer', 'Admin' or 'Support'
    CONSTRAINT PK_Users_UserID PRIMARY KEY (UserID),
);

--Roles
CREATE TABLE Roles (
    RoleID INT IDENTITY(1,1),    
    RoleName NVARCHAR(50) NOT NULL, --Admin, ReadOnly, Support, etc    
    CONSTRAINT PK_Roles_RoleID PRIMARY KEY (RoleID)    
);

-- UserRole Table
CREATE TABLE UserRoles (
    UserRoleID INT IDENTITY(1,1),
	RoleID INT NOT NULL,
    UserID INT NOT NULL,        
    CONSTRAINT PK_UserRole_UserRoleID PRIMARY KEY (UserRoleID),
    CONSTRAINT FK_UserRole_Users FOREIGN KEY (UserID) REFERENCES Users(UserID),
	CONSTRAINT FK_UserRole_Roles FOREIGN KEY (RoleID) REFERENCES Roles(RoleID)
);

-- Address Table (Combining Shipping and Billing Addresses)
CREATE TABLE Address (
    AddressID INT IDENTITY(1,1),
    CustomerID INT NOT NULL,
    Street NVARCHAR(255) NOT NULL,
    City NVARCHAR(50) NOT NULL,
    State NVARCHAR(50) NOT NULL,
    ZipCode NVARCHAR(20) NOT NULL,
    IsShippingAddress BIT NOT NULL, -- Indicates whether it's a shipping address
    -- Add other address-related fields as needed
    CONSTRAINT PK_Address_AddressID PRIMARY KEY (AddressID),
    CONSTRAINT FK_Address_Users FOREIGN KEY (CustomerID) REFERENCES Users(UserID)
);

-- Categories table
CREATE TABLE Categories (
    CategoryID INT IDENTITY(1,1),
    CategoryName NVARCHAR(100) NOT NULL,
    CONSTRAINT PK_Categories_CategoryID PRIMARY KEY (CategoryID),
);

-- Products table
CREATE TABLE Products (
    ProductID INT IDENTITY(1,1),
    ProductName NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX),
    Price DECIMAL(10, 2) NOT NULL,
    CategoryID INT NOT NULL,
    CONSTRAINT PK_Products_ProductID PRIMARY KEY (ProductID),
    CONSTRAINT FK_Products_Category FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID)
);

-- ProductImage Table
CREATE TABLE ProductImages (
    ImageID INT IDENTITY(1,1),
    ProductID INT NOT NULL,
    ImageUrl NVARCHAR(255) NOT NULL,
    -- Add other image-related fields as needed
    CONSTRAINT PK_ProductImages_ImageID PRIMARY KEY (ImageID),
    CONSTRAINT FK_ProductImages_Products FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

-- Orders table
CREATE TABLE Orders (
    OrderID INT IDENTITY(1,1),
    CustomerID INT NOT NULL,
    OrderDate DATETIME NOT NULL,
    Status NVARCHAR(50) NOT NULL, -- 'Pending', 'Shipped', 'Delivered', 'Cancelled'
    CONSTRAINT PK_Orders_OrderID PRIMARY KEY (OrderID),
    CONSTRAINT FK_Orders_Users FOREIGN KEY (CustomerID) REFERENCES Users(UserID)
);

-- OrderDetails table
CREATE TABLE OrderDetails (
    OrderDetailID INT IDENTITY(1,1),
    OrderID INT NOT NULL,
    ProductID INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(10, 2) NOT NULL,
    CONSTRAINT PK_OrderDetails_OrderDetailID PRIMARY KEY (OrderDetailID),
    CONSTRAINT FK_OrderDetails_Orders FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    CONSTRAINT FK_OrderDetails_Products FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

-- OrderStatus Table
CREATE TABLE OrderStatus (
    StatusID INT IDENTITY(1,1),
    OrderID INT NOT NULL,
    StatusName NVARCHAR(50) NOT NULL,
    -- Add other status-related fields as needed
    CONSTRAINT PK_OrderStatus_StatusID PRIMARY KEY (StatusID),
    CONSTRAINT FK_OrderStatus_Orders FOREIGN KEY (OrderID) REFERENCES Orders(OrderID)
);

-- Payments table
CREATE TABLE Payments (
    PaymentID INT IDENTITY(1,1),
    OrderID INT NOT NULL,
    Amount DECIMAL(10, 2) NOT NULL,
    PaymentDate DATETIME NOT NULL,
    PaymentMethod NVARCHAR(100),   -- Store the payment method (e.g., "Credit Card", "PayPal", etc.)
     CONSTRAINT PK_Payments_PaymentID PRIMARY KEY (PaymentID),
    CONSTRAINT FK_Payments_Orders FOREIGN KEY (OrderID) REFERENCES Orders(OrderID)
);

-- ProductReviews table
CREATE TABLE ProductReviews (
    ReviewID INT IDENTITY(1, 1),
    ProductID INT NOT NULL,
    CustomerID INT NOT NULL,
    ReviewText NVARCHAR(MAX) NOT NULL,
    Rating INT NOT NULL, -- Assuming rating scale 1-5
    CONSTRAINT PK_ProductReviews_ReviewID PRIMARY KEY (ReviewID),
    CONSTRAINT FK_ProductReviews_Products FOREIGN KEY (ProductID) REFERENCES Products(ProductID),
    CONSTRAINT FK_ProductReviews_Users FOREIGN KEY (CustomerID) REFERENCES Users(UserID)
);

-- Coupon Table
CREATE TABLE Coupons (
    CouponID INT IDENTITY(1,1),
    CouponCode NVARCHAR(20) NOT NULL,
    DiscountAmount DECIMAL(10, 2) NOT NULL,
    ExpiryDate DATETIME NOT NULL,
    ProductID INT, -- Nullable, to indicate product-specific coupon
    CategoryID INT, -- Nullable, to indicate category-specific coupon
    -- Add other coupon-related fields as needed
    CONSTRAINT PK_Coupons_CouponID PRIMARY KEY (CouponID),
    CONSTRAINT FK_Coupons_Products FOREIGN KEY (ProductID) REFERENCES Products(ProductID),
    CONSTRAINT FK_Coupons_Categories FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID)
);

-- OrderCoupon Table
CREATE TABLE OrderCoupons (
    OrderCouponID INT IDENTITY(1,1),
    OrderID INT NOT NULL,
    CouponID INT NOT NULL,
    -- Add other fields as needed, such as DiscountAmount
    CONSTRAINT PK_OrderCoupons_OrderCouponID PRIMARY KEY (OrderCouponID),
    CONSTRAINT FK_OrderCoupons_Orders FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    CONSTRAINT FK_OrderCoupons_Coupons FOREIGN KEY (CouponID) REFERENCES Coupons(CouponID)
);

-- Wishlist Table
CREATE TABLE Wishlist (
    WishlistID INT IDENTITY(1,1),
    CustomerID INT NOT NULL,
    ProductID INT NOT NULL,
    -- Add other wishlist-related fields as needed
    CONSTRAINT PK_Wishlist_WishlistID PRIMARY KEY (WishlistID),
    CONSTRAINT FK_Wishlist_Users FOREIGN KEY (CustomerID) REFERENCES Users(UserID),
    CONSTRAINT FK_Wishlist_Products FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

--Contact Us table
CREATE TABLE ContactUss (
    ContactUsId INT IDENTITY(1,1),
    UserName NVARCHAR(100) NOT NULL,
	UserEmail NVARCHAR(100) NOT NULL,
	MessageDetail NVARCHAR(2000) NOT NULL,    
    CONSTRAINT PK_ContactUs_ContactUsId PRIMARY KEY (ContactUsId)    
);

-- ShoppingCart table (optional)
CREATE TABLE ShoppingCart (
    CartID INT IDENTITY(1, 1),
    CustomerID INT NOT NULL,
    ProductID INT NOT NULL,
    Quantity INT NOT NULL,
    CONSTRAINT PK_ShoppingCart_CartID PRIMARY KEY (CartID),
    CONSTRAINT FK_ShoppingCart_Users FOREIGN KEY (CustomerID) REFERENCES Users(UserID),
    CONSTRAINT FK_ShoppingCart_Products FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

/*
--Here's a simplified example of how you might calculate the final order payment value:
-- Assume OrderId = 1 and the original order total is $100.00
DECLARE @OriginalOrderTotal DECIMAL(10, 2);
SET @OriginalOrderTotal = 100.00;

-- Calculate the total discount applied to the order based on applied coupons
DECLARE @TotalDiscount DECIMAL(10, 2);
SELECT @TotalDiscount = SUM(DiscountAmount)
FROM Coupons C
JOIN OrderCoupons oc ON c.CouponID = oc.CouponID
WHERE OrderId = 1;

-- Calculate the final order payment value
DECLARE @FinalOrderPayment DECIMAL(10, 2);
SET @FinalOrderPayment = @OriginalOrderTotal - @TotalDiscount;

-- Display the final order payment value
SELECT @FinalOrderPayment AS FinalOrderPayment;
*/

/*
-- Here's a suggested grouping of tables into microservices based on their functional relationships:
User Microservice:

AspNetUsers
AspNetUserRoles
AspNetUserLogins
Address (User addresses, including shipping and billing addresses)

Product Microservice:

Categories
Products
ProductReviews

Order Microservice:

Orders
OrderDetails
Payments
OrderCoupons
Coupons

Cart and Wishlist Microservices:

ShoppingCart
Wishlist
*/
-- Roles Table
INSERT INTO Roles (RoleName)
VALUES
('Admin'),
('ReadOnly'),
('Support'),
('User'),
('Guest');

INSERT INTO Users (Username, Password, Email, Address, UserType)
VALUES
('john_doe', 'password123', 'john.doe@example.com', '123 Main St, Anytown, USA', 'Customer'),
('jane_smith', 'securePass456', 'jane.smith@example.com', '456 Elm St, Othertown, USA', 'Admin'),
('sam_brown', 'myPassword789', 'sam.brown@example.com', '789 Oak St, Anycity, USA', 'Support');

INSERT INTO UserRoles (RoleID, UserID)
VALUES
(1, 1), -- Assigning 'Customer' role to 'john_doe'
(2, 2), -- Assigning 'Admin' role to 'jane_smith'
(3, 3), -- Assigning 'Support' role to 'sam_brown'
(1, 2), -- Assigning 'Customer' role to 'jane_smith'
(3, 1); -- Assigning 'Support' role to 'john_doe'

-- Inserting sample categories
INSERT INTO Categories (CategoryName)
VALUES
    ('Electronics'),
    ('Clothing'),
    ('Books'),
    ('Home & Kitchen');

-- Inserting sample products
INSERT INTO Products (ProductName, Description, Price, CategoryID)
VALUES
    ('Smartphone', 'Latest model with advanced features.', 799.99, 1),
    ('Laptop', 'Powerful laptop for work and gaming.', 1299.99, 1),
    ('T-shirt', 'Comfortable cotton t-shirt.', 19.99, 2),
    ('Jeans', 'Classic denim jeans.', 39.99, 2),
    ('Python Programming', 'Comprehensive guide to Python programming.', 49.99, 3);

INSERT INTO Address (CustomerID, Street, City, State, ZipCode, IsShippingAddress)
VALUES
(1, '123 Main St', 'Anytown', 'CA', '12345', 1), -- Shipping address for john_doe
(1, '124 Main St', 'Anytown', 'CA', '12345', 0), -- Billing address for john_doe
(2, '456 Elm St', 'Othertown', 'TX', '67890', 1), -- Shipping address for jane_smith
(3, '789 Oak St', 'Anycity', 'NY', '10112', 1); -- Shipping address for sam_brown


-- Inserting sample orders
INSERT INTO Orders (CustomerID, OrderDate, Status)
VALUES
    (1, '2024-07-23', 'Pending'),
    (1, '2024-07-22', 'Shipped'),
    (2, '2024-07-21', 'Delivered');  

-- Inserting sample order details
INSERT INTO OrderDetails (OrderID, ProductID, Quantity, UnitPrice)
VALUES
    (1, 1, 2, 799.99),
    (1, 3, 3, 19.99),
    (2, 2, 1, 1299.99),
    (3, 5, 1, 49.99);

-- Inserting sample order status
INSERT INTO OrderStatus (OrderId, StatusName)
VALUES
(1, 'Pending'),       -- Initial status for OrderId 1
(2, 'Shipped'),       -- Status for OrderId 2
(3, 'Delivered'),     -- Status for OrderId 3
(1, 'Completed'),     -- Status change for OrderId 1
(2, 'In Transit');    -- Status change for OrderId 2    

-- Inserting sample payments
INSERT INTO Payments (OrderID, Amount, PaymentDate, PaymentMethod)
VALUES
    (1, 1839.95, '2024-07-23', 'Credit Card'),
    (2, 1299.99, '2024-07-22', 'PayPal'),
    (3, 49.99, '2024-07-21', 'Debit Card');

-- Inserting sample product reviews
INSERT INTO ProductReviews (ProductID, CustomerID, ReviewText, Rating)
VALUES
    (1, 1, 'Great smartphone, fast delivery.', 5),
    (2, 1, 'Excellent laptop, exceeded expectations.', 4),
    (3, 1, 'Nice t-shirt, comfortable fabric.', 5),
    (5, 1, 'Very useful book, clear explanations.', 4);

-- Inserting sample shopping cart items (optional)
INSERT INTO ShoppingCart (CustomerID, ProductID, Quantity)
VALUES
    (1, 1, 1),
    (1, 3, 2);  

/*
select * from Users
select * from Roles
select * from UserRoles
select * from Address
select * from Categories
select * from Products
select * from ProductImages
select * from ProductReviews
select * from Orders
select * from OrderDetails
select * from Payments
select * from ContactUs
select * from Coupons
select * from OrderCoupons
select * from ShoppingCart
select * from Wishlist
```
*/
