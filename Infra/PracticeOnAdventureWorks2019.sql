-- docker volume create sql_edge_data
-- docker volume create sql_edge_log
-- docker volume create sql_edge_backup

-- docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=YourStrongPassword123!' -e 'MSSQL_PID=Express' -p 1435:1433 --name sqlserver-container --hostname my-sql-edge-host --network my-sql-network -v sql_edge_backup:/var/opt/mssql/backup -d mcr.microsoft.com/mssql/server:2022-latest

-- cd downloads

-- docker cp AdventureWorks2019.bak sqlserver-container:/var/opt/mssql/backup/


-- List all employees’ first name, last name, and job title
SELECT FirstName, LastName, JobTitle
FROM HumanResources.Employee e
     JOIN Person.Person p ON e.BusinessEntityID=p.BusinessEntityID

-- Find all products with ListPrice > 1000
SELECT *
FROM Production.Product
Where ListPrice>1000

-- Get the TOP 10 most expensive products by StandardCost.
SELECT TOP 10
     *
FROM Production.Product
ORDER BY StandardCost DESC

--  List all departments in HumanResources.Department.
SELECT Name
FROM HumanResources.Department

--  Find all employees hired after 2010.
SELECT *
FROM HumanResources.Employee
WHERE YEAR(HireDate)>2010

--  Show all customers FROM the city of "Seattle".
SELECT p.FirstName, p.LastName
FROM Sales.Customer as c
     JOIN Person.Person p ON c.PersonID=p.BusinessEntityID
     JOIN Person.BusinessEntityAddress bea ON p.BusinessEntityID=bea.BusinessEntityID
     JOIN Person.Address a ON a.AddressID=bea.AddressID
WHERE a.City='Seattle'

--  Get all vendors with credit rating = 1.
SELECT *
FROM Purchasing.Vendor v
WHERE CreditRating=1

--  Show all products that are out of stock in Production.ProductInventory.
SELECT p.Name, 'Out of stock' as Quantity
FROM Production.ProductInventory i
     JOIN Production.Product p ON i.ProductID=p.ProductID
GROUP BY p.ProductID, Name
HAVING sum(Quantity)=0

--  List employees who are also salespeople.
SELECT p.FirstName, p.LastName, e.JobTitle, sp.SalesQuota, sp.SalesYTD
FROM HumanResources.Employee e
     JOIN Sales.SalesPerson sp ON e.BusinessEntityID=sp.BusinessEntityID
     JOIN Person.Person p ON sp.BusinessEntityID=p.BusinessEntityID

--  Show product names and their categories.
SELECT p.Name, pc.Name
FROM Production.Product p
     JOIN Production.ProductSubcategory psc ON p.ProductSubcategoryID=psc.ProductSubcategoryID
     JOIN Production.ProductCategory pc ON psc.ProductCategoryID=pc.ProductCategoryID

--  Find the total number of employees in each department.
SELECT d.Name Department, count(e.BusinessEntityID) EmployeeCount
FROM HumanResources.Employee e
     JOIN HumanResources.EmployeeDepartmentHistory ed ON e.BusinessEntityID=ed.BusinessEntityID
     JOIN HumanResources.Department d ON ed.DepartmentID = d.DepartmentID
GROUP BY d.DepartmentID, d.Name

--  List the TOP 5 best-selling products by total order quantity.
SELECT TOP 5
     sum(OrderQty) OrderQuantity, Name
FROM Sales.SalesOrderDetail sod
     JOIN Production.Product p ON sod.ProductID=p.ProductID
GROUP BY p.Name
ORDER BY OrderQuantity DESC

--  Find customers who placed more than 5 orders.
SELECT c.CustomerID, Count(soh.SalesOrderID) as NumberOfOrderPlaced
FROM Sales.Customer c
     JOIN Person.Person p ON c.PersonID=p.BusinessEntityID
     JOIN Sales.SalesOrderHeader soh ON c.CustomerID=soh.CustomerID
Group by c.CustomerID
HAVING Count(soh.SalesOrderID)>5
ORDER BY NumberOfOrderPlaced DESC

--  Get the average ListPrice for each product category.
SELECT pc.Name, AVG(p.ListPrice)
FROM Production.Product p
     JOIN Production.ProductSubcategory psc ON p.ProductSubcategoryID=psc.ProductSubcategoryID
     Join Production.ProductCategory pc ON psc.ProductCategoryID=pc.ProductCategoryID
WHERE p.ListPrice > 0
GROUP by pc.Name

--  List vendors who supply more than 10 products.
Select v.Name, count(ProductID) as numberOfProductSupply
FROM Purchasing.Vendor V
     JOIN Purchasing.ProductVendor pv ON v.BusinessEntityID=pv.BusinessEntityID
GROUP BY v.Name
HAVING count(ProductID)>10
ORDER BY numberOfProductSupply DESC

--  Find the highest-paid employee(s).
SELECT TOP 1
     p.FirstName, p.LastName, ep.Rate
FROM HumanResources.Employee e
     JOIN Person.Person p ON e.BusinessEntityID=p.BusinessEntityID
     JOIN HumanResources.EmployeePayHistory ep ON e.BusinessEntityID=ep.BusinessEntityID
ORDER BY ep.Rate DESC

SELECT
     p.FirstName,
     p.LastName,
     eph.Rate AS HourlyRate
FROM HumanResources.EmployeePayHistory eph
     JOIN HumanResources.Employee e
     ON eph.BusinessEntityID = e.BusinessEntityID
     JOIN Person.Person p
     ON e.BusinessEntityID = p.BusinessEntityID
WHERE eph.Rate = (SELECT MAX(Rate)
FROM HumanResources.EmployeePayHistory);

SELECT
     p.FirstName,
     p.LastName,
     eph.Rate,
     RANK() OVER (ORDER BY eph.Rate DESC) AS PayRank
FROM HumanResources.EmployeePayHistory eph
     JOIN HumanResources.Employee e
     ON eph.BusinessEntityID = e.BusinessEntityID
     JOIN Person.Person p
     ON e.BusinessEntityID = p.BusinessEntityID;

--  Find the employee with the earliest hire date.
SELECT p.FirstName, p.LastName, e.HireDate
FROM HumanResources.Employee e
     JOIN Person.Person p ON e.BusinessEntityID=p.BusinessEntityID
Where e.HireDate = (SELECT MIN(HireDate)
FROM HumanResources.Employee)

--  Get the total sales (TotalDue) per year.
SELECT
     YEAR(soh.OrderDate) YearOfSales,
     SUM(soh.TotalDue) TotalSales
FROM Sales.SalesOrderDetail sod
     JOIN Sales.SalesOrderHeader Soh ON sod.SalesOrderID=soh.SalesOrderID
GROUP BY YEAR(soh.OrderDate)
ORDER BY YearOfSales DESC

--  Find the products with no orders in Sales.SalesOrderDetail.
SELECT
     Name ProductName, p.ProductID
FROM Production.Product p
     LEFT JOIN Sales.SalesOrderDetail sod ON p.ProductID=sod.ProductID
Where sod.ProductID is null

--  Show employees who changed departments more than once.
SELECT
     p.FirstName,
     p.LastName,
     e.BusinessEntityID
FROM HumanResources.EmployeeDepartmentHistory edh
     JOIN HumanResources.Employee e ON edh.BusinessEntityID=e.BusinessEntityID
     JOIN Person.Person p ON p.BusinessEntityID=e.BusinessEntityID
GROUP BY e.BusinessEntityID, p.FirstName, p.LastName
Having COUNT(edh.DepartmentID)>1

--   Find the TOP 3 employees with the highest average order sales.
SELECT TOP 3
     AVG(TotalDue) AverageOrderSales, p.FirstName, p.LastName
FROM HumanResources.Employee E
     JOIN Person.Person p ON e.BusinessEntityID=p.BusinessEntityID
     JOIN Sales.SalesOrderHeader soh ON e.BusinessEntityID=soh.SalesPersonID
Group by soh.SalesPersonID, p.FirstName, p.LastName
ORDER BY AverageOrderSales DESC

--   Rank customers by their total spending (include ties).
SELECT
     p.FirstName, p.LastName,
     SUM(TotalDue) TotalSpendings,
     RANK() OVER (ORDER BY SUM(soh.TotalDue) DESC) rn
FROM Sales.Customer c
     JOIN Person.Person p ON c.PersonID=p.BusinessEntityID
     Join Sales.SalesOrderHeader soh ON c.CustomerID=soh.CustomerID
Group By p.FirstName, p.LastName, c.CustomerID
order BY rn

--   Find the running total of sales per month in 2013.
;With
     CTE
     AS

     (
          SELECT
               MONTH(OrderDate) as MonthOfOrder,
               --TotalDue
               SUM(TotalDue) AS TotalSalesOfMonth
          FROM Sales.SalesOrderHeader soh
               Join Sales.SalesOrderDetail sod ON soh.SalesOrderID=soh.SalesOrderID
          WHERE YEAR(OrderDate)=2013
          Group By MONTH(OrderDate)
          --ORDER BY MonthOfOrder
     )

Select
     MonthOfOrder,
     SUM(TotalSalesOfMonth) OVER(ORDER BY MonthOfOrder) RunningTotalSales
FROM CTE
ORDER BY MonthOfOrder

--   List products with prices higher than the average ListPrice of their category.
;WITH
     CTE
     AS

     (
          SELECT
               pc.ProductCategoryID,
               pc.Name Category,
               AVG(p.ListPrice) AverageCategoryPrice
          FROM Production.Product p
               JOIN Production.ProductSubcategory psc ON p.ProductSubcategoryID=psc.ProductSubcategoryID
               JOIN Production.ProductCategory pc ON psc.ProductCategoryID=pc.ProductCategoryID
          Group by pc.ProductCategoryID, pc.Name
     )

SELECT
     p.Name,
     c.Category,
     p.ListPrice ProductPrice,
     c.AverageCategoryPrice
FROM CTE C
     JOIN Production.ProductSubcategory psc ON C.ProductCategoryID=psc.ProductCategoryID
     JOIN Production.Product p ON psc.ProductSubcategoryID=p.ProductSubcategoryID
WHERE p.ListPrice>c.AverageCategoryPrice

--   Find customers who bought products FROM more than 3 different categories.
SELECT
     soh.CustomerID,
     COUNT(pc.ProductCategoryID) as CategoryCount
FROM Sales.Customer c
     JOIN Person.Person pr ON c.CustomerID=pr.BusinessEntityID
     JOIN Sales.SalesOrderHeader soh ON soh.CustomerID=c.CustomerID
     JOIN Sales.SalesOrderDetail sod ON soh.SalesOrderID=sod.SalesOrderID
     JOIN Production.Product p ON sod.ProductID=p.ProductID
     JOIN Production.ProductSubcategory psc ON p.ProductSubcategoryID=psc.ProductSubcategoryID
     JOIN Production.ProductCategory pc ON psc.ProductCategoryID=pc.ProductCategoryID
GROUP BY soh.CustomerID
HAVING COUNT(pc.ProductCategoryID)>3
ORDER BY CategoryCount

--   Show the TOP 10 most profitable products (Sales - Cost).
/*
We want to rank products by profitability:
. Sales = how much revenue was earned FROM selling the product.
     . This comes FROM Sales.SalesOrderDetail
     . LineTotal = OrderQty * UnitPrice (what customer paid for that product line).
. Cost = how much it cost the company to produce the product.
     . This comes FROM Production.Product
          StandardCost = cost per unit.
     . So, TotalCost = OrderQty * StandardCost.
. Profit = Sales – Cost
     . For each product, profit = SUM(OrderQty * (UnitPrice - StandardCost)).
*/
Select TOP 10
     p.Name,
     SUM(OrderQty*UnitPrice) SalesCost,
     SUM(p.StandardCost * OrderQty) ExactCost,
     SUM(OrderQty * (UnitPrice-StandardCost)) ProfitableCost
FROM Sales.SalesOrderDetail sod
     JOIN Production.Product p ON sod.ProductID=p.ProductID
GROUP BY p.ProductID, p.Name
ORDER BY ProfitableCost DESC

--   Find vendors who have not supplied products in the last year.
SELECT v.Name VendorName
FROM Purchasing.Vendor v
     LEFT Join Purchasing.PurchaseOrderHeader oh ON v.BusinessEntityID=oh.VendorID AND YEAR(OrderDate) >= DATEADD(Year, -1, GETDATE())
WHERE oh.PurchaseOrderID is null

--   Show employees who never made a sales order.
SELECT p.FirstName, p.LastName
FROM HumanResources.Employee e
     JOIN Person.Person p ON e.BusinessEntityID=p.BusinessEntityID
     LEFT JOIN Sales.SalesOrderHeader soh ON soh.SalesPersonID=e.BusinessEntityID
WHERE SalesPersonID is null

--   Find the month with the highest total sales in history.
SELECT
     SUM(TotalDue) TotalSales,
     Month(soh.OrderDate) [Month],
     YEAR(soh.OrderDate) as [Year]
FROM Sales.SalesOrderHeader soh
Group by Month(soh.OrderDate), YEAR(soh.OrderDate)
ORDER BY TotalSales DESC

--   Create a leaderboard of salespeople by sales amount, including their ranking.
SELECT
     SalesPersonID, p.FirstName, p.LastName,
     SUM(TotalDue) TotalSales,
     DENSE_RANK() OVER (ORDER BY SUM(TotalDue) DESC) SalesRank
FROM Sales.SalesOrderHeader soh
     JOIN HumanResources.Employee e ON soh.SalesPersonID=e.BusinessEntityID
     JOIN Person.Person p ON e.BusinessEntityID=p.BusinessEntityID
GROUP BY SalesPersonID, p.FirstName, p
.LastName
ORDER BY [SalesRank]

--   🔹 Employee & HR (HumanResources.*)
--   Find employees with the earliest hire date in each department.

select
     d.Name Department,
     p.FirstName+' '+p.LastName EmployeeName,
     HireDate
FROM HumanResources.Employee e
     join Person.Person p on e.BusinessEntityID=p.BusinessEntityID
     JOIN HumanResources.EmployeeDepartmentHistory edh on e.BusinessEntityID=edh.BusinessEntityID
     join HumanResources.Department d on edh.DepartmentID=d.DepartmentID
WHERE HireDate=(Select MIN(HireDate)
FROM HumanResources.Employee e
     JOIN HumanResources.EmployeeDepartmentHistory dh on e.BusinessEntityID=dh.BusinessEntityID
WHERE dh.DepartmentID=d.DepartmentID)

--   List employees who have worked in 3 or more departments.

--   Find employees whose pay rate increased the most over time.
--   Show employees who changed job titles more than once.
--   Rank employees by tenure (HireDate).
--   Get the top 5 highest-paid employees in each department.
--   Find employees who never had a pay rate change.
--   Show employees who had overlapping department assignments.
--   List the average tenure in each department.
--   Find employees who joined before their manager (self-join).

--   🔹 Person & Contact (Person.*)
--   Count how many addresses each person has.
--   Find the most common contact type across all persons.
--   List persons who have no email address.
--   Show top 10 cities with most persons living.
--   Identify duplicate names (same FirstName + LastName).
--   Find the top 5 states with the most addresses.
--   Show people who are both employees and vendors.
--   List contacts who are associated with more than one customer.
--   Show how many people share the same last name.
--   Rank the most popular AddressType by usage.

--   🔹 Product & Inventory (Production.*)
--   Find products never ordered.
--   Show products supplied by more than 3 vendors.
--   Rank products by inventory quantity in each location.
--   Find discontinued products that still have inventory.
--   Show products with the largest difference between ListPrice and StandardCost.
--   List top 10 most expensive products sold.
--   Find products with no unit measure assigned.
--   Rank vendors by average product cost they supply.
--   Find products that have been reordered the most.
--   Show the number of products per category/subcategory.

--   🔹 Sales (Sales.*)
--   Get total sales per year.
--   Find the top 5 customers by sales amount.
--   Show sales trends per quarter for the last 3 years.
--   Rank salespeople by sales in their territory.
--   Find customers who placed more than 10 orders.
--   Show repeat customers (customers who ordered in multiple years).
--   Identify orders that were never shipped.
--   Find salespeople with no sales in a given year.
--   Show total sales per product category.
--   Find customers whose orders always exceeded $1000.

--   🔹 Sales + Territory
--   Show sales amount per territory per year.
--   Find the territory with the highest average order size.
--   Rank territories by customer count.
--   Find customers who changed territory over time.
--   Show territories that had no sales in a given year.
--   Find salespeople handling more than 3 territories.
--   Show top 5 products sold in each territory.
--   Compare sales between territories (North America vs Europe).
--   Show sales growth % year-over-year per territory.
--   Find territory with the lowest variance in sales.

--   🔹 Purchasing & Vendors (Purchasing.*)
--   List vendors who supply products never sold.
--   Rank vendors by total purchase order value.
--   Find vendors supplying more than 20 unique products.
--   Show vendors with late deliveries.
--   Find vendors who increased prices over time.
--   Show average lead time per vendor.
--   Rank vendors by reliability (fewest late orders).
--   Show vendors who only supply 1 product.
--   Find vendors with highest average StandardPrice.
--   Show vendors that stopped supplying products.

--   🔹 Advanced Joins & CTEs
--   Find customers who ordered products from at least 3 categories.
--   Show employees whose department changed but pay decreased.
--   Find products sold both online and offline.
--   Identify orders where freight was >10% of SubTotal.
--   Show customers who placed orders in consecutive months.
--   Find employees whose tenure is above department average.
--   Show salespeople whose sales are below territory average.
--   Rank customers by lifetime value (LTV).
--   Find first order date per customer.
--   Show last 5 orders per customer.

--   🔹 Window Functions
--   Rank employees by salary within department.
--   Show customers’ order history with running total.
--   Find month-over-month sales growth.
--   Show moving average of sales over 3 months.
--   Rank products by order quantity within category.
--   Show dense ranks of vendors by supplied product count.
--   Identify top 3 products per year by sales.
--   Rank customers by frequency of orders.
--   Show cumulative sales by territory.
--   Find gaps in employee department history.

--   🔹 Complex Business Logic
--   Find “VIP customers” (top 1% of sales).
--   Identify products that contribute to 80% of sales (Pareto).
--   Find orders where discount > 15% of ListPrice.
--   Show customers inactive for more than 1 year.
--   Find products with declining sales for 3 consecutive years.
--   Identify employees with overlapping pay history records.
--   Show salespeople whose total sales never decreased year-over-year.
--   Find top 5 vendors by profit margin (ListPrice – StandardCost).
--   Rank territories by customer retention rate.
--   Show customers with orders in every year.

--   🔹 Challenge Problems
--   Find employees who are also customers.
--   Identify territories where sales doubled in 1 year.
--   Find products that were ordered together frequently (market basket).
--   Show vendors whose supplied products were never sold.
--   Find customers with the largest gap between orders.
--   Show products where StandardCost > ListPrice.
--   Find duplicate purchase orders.
--   Show customers whose average order value increased each year.
--   Rank employees by number of department changes.
--   Find customers who ordered from every product category.
