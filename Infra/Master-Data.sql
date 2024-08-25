-- ('Admin', 'Has full access to all system features and settings'),
-- ('User', 'Standard user with access to basic features'),
-- ('Manager', 'Can manage user activities and has limited admin rights'),
-- ('Guest', 'Limited access, mostly read-only permissions'),
-- ('Support', 'Provides customer support and resolves user issues');

USE ProductApiDb

GO
INSERT INTO Category (CategoryName, CategoryDescription, CategoryImageUrl) VALUES
('Electronics', 'Devices, gadgets, and accessories', '~/img/categories/electronics.jpeg'),
('Clothing', 'Apparel for men, women, and children', '~/img/categories/cloths.jpeg'),
('Home & Kitchen', 'Furniture, kitchenware, and home essentials', '~/img/categories/home_kitchen.jpeg'),
('Books', 'Fiction, non-fiction, and educational materials', '~/img/categories/books.jpeg'),
('Fruits', 'Fresh and dried fruits', '~/img/categories/mixed-fruits.jpg'),
('Vegetables', 'Fresh and canned vegetables', '~/img/categories/vegetables.jpeg');

GO
---------------Inserting Products Category 1--------------
INSERT INTO Product (CategoryId, ProductName, [Description], QuantityInStock, SellingPrice, OriginalPrice, ImageUrl) VALUES
(1, 'iPhone 15 pro', 'Latest model with advanced features', 10, 149000, 179000, '~/img/product/iphone15.jpeg'),
(1, 'Oppo Phone', 'Latest model with advanced features', 6, 49000, 59000, '~/img/product/oppo-mobile.jpeg'),
(1, 'Samsung S12 Pro', 'Latest model with advanced features', 15, 159000, 169000, '~/img/product/samsungs12pro.jpeg'),
(1, 'Macbook M2 Pro', 'High-performance laptop with 16GB RAM', 15, 124999, 159999, '~/img/product/macbook.jpeg'),
(1, 'Dell Laptop', 'Laptop for education purpose best in all.', 12, 79000, 89000, '~/img/product/dell-laptop.jpeg');


GO
INSERT INTO ProductImage(ProductId, ImageUrl, IsThumbnail) VALUES
(1, '~/img/product/iphone15.jpeg', 0),
(2, '~/img/product/oppo-mobile.jpeg', 0),
(3, '~/img/product/samsungs12pro.jpeg', 0),
(4, '~/img/product/macbook.jpeg', 0),
(5, '~/img/product/dell-laptop.jpeg', 0);

GO
-- Inserting Products into the Clothing Category
INSERT INTO Product (CategoryId, ProductName, Description, QuantityInStock, SellingPrice, OriginalPrice, ImageUrl) VALUES
(2, 'T-Shirt', 'Cotton T-shirt available in various colors', 10, 2999, 4999, '~/img/product/tshirt.jpeg'),
(2, 'Suit', 'Denim jeans with a comfortable fit', 6, 15000, 20000, '~/img/product/suite.jpeg'),
(2, 'Dress', 'Denim jeans with a comfortable fit', 15, 14000, 17000, '~/img/product/dress-2-5.jpeg'),
(2, 'Dress', 'Denim jeans with a comfortable fit', 15, 13000, 16000, '~/img/product/dress-2-5.jpeg'),
(2, 'Shirt', 'Waterproof jacket suitable for all seasons', 12, 3599, 4500, '~/img/product/shirt.jpeg');

GO
INSERT INTO ProductImage(ProductId, ImageUrl, IsThumbnail) VALUES
(6, '~/img/product/tshirt.jpeg', 0),
(7, '~/img/product/suite.jpeg', 0),
(8, '~/img/product/dress-2-5.jpeg', 0),
(9, '~/img/product/dress-2-5.jpeg', 0),
(10, '~/img/product/shirt.jpeg', 0);

GO
-- Inserting Products into the Home & Kitchen Category
INSERT INTO Product (CategoryId, ProductName, Description, QuantityInStock, SellingPrice, OriginalPrice, ImageUrl) VALUES
(3, 'Spatual', 'Wooden dining table with 6 chairs', 10, 399, 999, '~/img/product/spatula.jpeg'),
(3, 'Gas Stove', 'High-speed blender for smoothies and soups', 6, 8999, 10999, '~/img/product/gas-stove.jpeg'),
(3, 'Utensils', 'Compact microwave oven with multiple settings', 15, 499, 999, '~/img/product/utensils.jpeg');

GO
INSERT INTO ProductImage(ProductId, ImageUrl, IsThumbnail) VALUES
(11, '~/img/product/spatula.jpeg', 0),
(12, '~/img/product/gas-stove.jpeg', 0),
(13, '~/img/product/utensils.jpeg', 0);

GO
-- Inserting Products into the Books Category
INSERT INTO Product (CategoryId, ProductName, Description, QuantityInStock, SellingPrice, OriginalPrice, ImageUrl) VALUES
(4, 'Rich Dad Poor Dad', 'Bestselling novel in fiction genre', 10, 399, 999, '~/img/product/richdadpoordad.jpeg'),
(4, 'Ramcharit Manas', 'Epic book of hindus', 6, 8999, 10999, '~/img/product/ramcharitmanas.jpeg'),
(4, 'c# Programming', 'Best book of c# programming language.', 15, 499, 999, '~/img/product/csharp-programming.jpg'),
(4, 'Java Black Book', 'Best book of java programming language.', 15, 499, 999, '~/img/product/java.jpeg'),
(4, 'Shreemadbhagwad Geeta', 'Book covering various academic subjects', 15, 499, 999, '~/img/product/geeta.jpeg');

GO
INSERT INTO ProductImage(ProductId, ImageUrl, IsThumbnail) VALUES
(14, '~/img/product/richdadpoordad.jpeg', 0),
(15, '~/img/product/ramcharitmanas.jpeg', 0),
(16, '~/img/product/csharp-programming.jpg', 0),
(17, '~/img/product/java.jpeg', 0),
(18, '~/img/product/geeta.jpeg', 0);

-- Inserting Products into the Fruits Category
INSERT INTO Product (CategoryId, ProductName, Description, QuantityInStock, SellingPrice, OriginalPrice, ImageUrl) VALUES
(5, 'Apple', 'Fresh and juicy red apples', 10, 399, 999, '~/img/product/apples.jpg'),
(5, 'Banana', 'Ripe bananas high in potassium', 6, 8999, 10999, '~/img/product/banana.jpg'),
(5, 'Orange', 'Citrus fruit with a tangy flavor', 15, 499, 999, '~/img/product/orange.jpg'),
(5, 'Blueberry', 'Citrus fruit with a tangy flavor', 15, 499, 999, '~/img/product/blueberry.jpg'),
(5, 'Watermelon', 'Citrus fruit with a tangy flavor', 15, 499, 999, '~/img/product/watermelon.jpg'),
(5, 'Guava', 'Citrus fruit with a tangy flavor', 15, 499, 999, '~/img/product/guava.jpg');

GO
INSERT INTO ProductImage(ProductId, ImageUrl, IsThumbnail) VALUES
(19, '~/img/product/apples.jpg', 0),
(20, '~/img/product/banana.jpg', 0),
(21, '~/img/product/orange.jpeg', 0),
(22, '~/img/product/blueberry.jpg', 0),
(23, '~/img/product/watermelon.jpg', 0),
(24, '~/img/product/guava.jpg', 0);

GO
-- Inserting Products into the Vegetables Category
INSERT INTO Product (CategoryId, ProductName, Description, QuantityInStock, SellingPrice, OriginalPrice, ImageUrl) VALUES
(6, 'Carrot', 'Crunchy and nutritious orange carrots', 10, 399, 999, '~/img/product/carrot.jpeg'),
(6, 'Broccoli', 'Fresh broccoli florets rich in vitamins', 6, 8999, 10999, '~/img/product/broccoli.jpeg'),
(6, 'Beetroot', 'Fresh broccoli florets rich in vitamins', 15, 499, 999, '~/img/product/beetroot.jpeg'),
(6, 'Bitter-guard', 'Fresh broccoli florets rich in vitamins', 15, 499, 999, '~/img/product/bitterguard.jpeg'),
(6, 'Potato', 'Fresh broccoli florets rich in vitamins', 15, 499, 999, '~/img/product/potato.jpeg'),
(6, 'Onion', 'Fresh broccoli florets rich in vitamins', 15, 499, 999, '~/img/product/onion.jpeg'),
(6, 'Tomato', 'Juicy tomatoes suitable for salads and cooking', 15, 499, 999, '~/img/product/tomato.jpeg');

GO
INSERT INTO ProductImage(ProductId, ImageUrl, IsThumbnail) VALUES
(25, '~/img/product/carrot.jpeg', 0),
(26, '~/img/product/broccoli.jpeg', 0),
(27, '~/img/product/beetroot.jpeg', 0),
(28, '~/img/product/bitterguard.jpeg', 0),
(29, '~/img/product/potato.jpeg', 0),
(30, '~/img/product/onion.jpeg', 0),
(31, '~/img/product/tomato.jpeg', 0);



-----------------Thumbnail image insert----------------
use ProductApiDb
select  pi.ProductImageId, pi.productId, pi.ImageUrl, p.ImageUrl, pi.IsThumbnail, p.ProductName  from ProductImage PI
join Product p on pi.ProductId=p.ProductId


insert into ProductImage (ProductId, ImageUrl, IsThumbnail, ThumbBigImageUrl) VALUES
(1, '~/img/product/iphone-thumbnail.jpeg', 1, '~/img/product/iphone15.jpeg'),
(2, '~/img/product/oppo-thumbnail.jpeg',1, '~/img/product/oppo.jpeg'),
(3, '~/img/product/samsung-thumbnail.jpeg',1, '~/img/product/samsungs12pro.jpeg'),
(4, '~/img/product/macbook-thumbnail.jpeg',1, '~/img/product/macbook.jpeg'),
(5, '~/img/product/dell-thumbnail.jpeg',1, '~/img/product/dell-laptop.jpeg'),
(6, '~/img/product/tshirt-thumbnail.jpeg',1, '~/img/product/tshirt.jpeg'),
(7, '~/img/product/suit-thumbnail.jpeg',1, '~/img/product/suit.jpeg'),
(8, '~/img/product/dress-thumbnail.jpeg',1, '~/img/product/dress-2-5.jpeg'),
(9, '~/img/product/dress-thumbnail.jpeg',1, '~/img/product/dress-2-5.jpeg'),
(10, '~/img/product/shirt-thumbnail.jpeg',1, '~/img/product/shirt.jpeg'),
(11, '~/img/product/spatula-thumbnail.jpeg',1, '~/img/product/spatula.jpeg'),
(12, '~/img/product/gas-stove-thumbnail.jpeg',1, '~/img/product/gas-stove.jpeg'),
(13, '~/img/product/utensils-thumbnail.jpeg',1, '~/img/product/utensils.jpeg'),
(14, '~/img/product/richdadpoordad-thumbnail.jpeg',1, '~/img/product/richdadpoordad.jpeg'),
(15, '~/img/product/ramcharitmanas-thumbnail.jpeg',1, '~/img/product/ramcharitmanas.jpeg'),
(16, '~/img/product/csharp-thumbnail.jpeg',1, '~/img/product/csharp.jpeg'),
(17, '~/img/product/java-thumbnail.jpeg',1, '~/img/product/java.jpeg'),
(18, '~/img/product/geeta-thumbnail.jpeg',1, '~/img/product/geeta.jpeg'),
(19, '~/img/product/apple.jpeg',1, '~/img/product/apples.jpg'),
(20, '~/img/product/banana-thumbnail.jpeg',1, '~/img/product/banana.jpg'),
(21, '~/img/product/orange-thumbnail.jpeg',1, '~/img/product/orange.jpeg'),
(22, '~/img/product/blueberry-thumbnail.jpeg',1, '~/img/product/blueberry.jpg'),
(23, '~/img/product/watermelon-thumbnail.jpeg',1, '~/img/product/watermelon.jpg'),
(24, '~/img/product/guava-thumbnail.jpeg',1, '~/img/product/guava.jpg'),
(25, '~/img/product/carrot-thumbnail.jpeg',1, '~/img/product/carrot.jpeg'),
(26, '~/img/product/broccoli-thumbnail.jpeg',1, '~/img/product/broccoli.jpeg'),
(27, '~/img/product/beetroot-thumbnail.jpeg',1, '~/img/product/beetroot.jpeg'),
(28, '~/img/product/bittergourd-thumbnail.jpeg',1, '~/img/product/bitterguard.jpeg'),
(29, '~/img/product/potato-thumbnail.jpeg',1, '~/img/product/potato.jpeg'),
(30, '~/img/product/onion-thumbnail.jpeg',1, '~/img/product/onion.jpeg'),
(31, '~/img/product/tomato-thumbnail.jpeg',1, '~/img/product/tomato.jpeg');


GO
----------------Cart-----------------
USE CartApiDb
GO
INSERT INTO PaymentType (Value)
VALUES 
('Credit Card'),
('Debit Card'),
('Net Banking'),
('Cash on Delivery'),
('UPI');

GO
---------------Order-------------------
USE OrderApiDb
GO
INSERT INTO PaymentType (Value)
VALUES 
('Credit Card'),
('Debit Card'),
('Net Banking'),
('Cash on Delivery'),
('UPI');

INSERT INTO OrderStatus (Status)
VALUES 
('Pending'),
('Processing'),
('Shipped'),
('Delivered'),
('Cancelled');

